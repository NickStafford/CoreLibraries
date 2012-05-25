﻿#region © Copyright Web Applications (UK) Ltd, 2012.  All rights reserved.
// Solution: WebApplications.Utilities 
// Project: WebApplications.Utilities
// File: LimitedConcurrentQueue.cs
// 
// This software, its object code and source code and all modifications made to
// the same (the “Software”) are, and shall at all times remain, the proprietary
// information and intellectual property rights of Web Applications (UK) Limited. 
// You are only entitled to use the Software as expressly permitted by Web
// Applications (UK) Limited within the Software Customisation and
// Licence Agreement (the “Agreement”).  Any copying, modification, decompiling,
// distribution, licensing, sale, transfer or other use of the Software other than
// as expressly permitted in the Agreement is expressly forbidden.  Web
// Applications (UK) Limited reserves its rights to take action against you and
// your employer in accordance with its contractual and common law rights
// (including injunctive relief) should you breach the terms of the Agreement or
// otherwise infringe its copyright or other intellectual property rights in the
// Software.
// 
// © Copyright Web Applications (UK) Ltd, 2012.  All rights reserved.
#endregion

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using JetBrains.Annotations;

namespace WebApplications.Utilities.Caching
{
    /// <summary>
    /// Represents a thread-safe first in-first out (FIFO) collection.  As the size exceeds <see cref="Capacity"/> the queue automatically throws away head items.
    /// </summary>
    /// <typeparam name="T">The type of the elements contained in the queue.</typeparam>
    [ComVisible(false)]
    [DebuggerDisplay("Count = {Count}, Capacity = {Capacity}")]
    [Serializable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
    public class CyclicConcurrentQueue<T> : IProducerConsumerCollection<T>
    {
        /// <summary>
        /// The maximum capacity which can be created.
        /// </summary>
        public const long MaxCapacity = int.MaxValue*(long)4096;

        /// <summary>
        /// Holds a chunk.
        /// </summary>
        /// <remarks></remarks>
        private class Chunk
        {
            /// <summary> 
            /// The spin lock for this chunk.
            /// </summary>
            public SpinLock SpinLock = new SpinLock();

            /// <summary>
            /// The array.
            /// </summary>
            [NotNull]
            public readonly T[] Array;

            /// <summary>
            /// The chunk size.
            /// </summary>
            public readonly long Size;

            /// <summary>
            /// Initializes a new instance of the <see cref="CyclicConcurrentQueue&lt;T&gt;.Chunk"/> class.
            /// </summary>
            /// <param name="size">The size.</param>
            /// <remarks></remarks>
            public Chunk(long size)
            {
                Size = size;
                Array = new T[size];
            }
        }

        /// <summary>
        /// Underlying array of chunks.
        /// </summary>
        [NotNull]
        private readonly Chunk[] _chunks;

        /// <summary>
        /// Points at the start of the elements that are being written to.
        /// </summary>
        private long _head;

        /// <summary>
        /// Point at the next element to read from the queue.
        /// </summary>
        private long _tail;

        /// <summary>
        /// The maximum capacity of the queue.  As items are queued beyond the capacity, items are dequeued automatically.
        /// </summary>
        public readonly long Capacity;

        /// <inheritdoc/>
        bool ICollection.IsSynchronized
        {
            get
            {
                return false;
            }
        }

        /// <inheritdoc/>
        object ICollection.SyncRoot
        {
            get
            {
                throw new NotSupportedException(Resources.LimitedConcurrentQueue_SyncRoot_ICollection_SyncRoot_Unsupported);
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the <see cref="CyclicConcurrentQueue{T}"/> is empty.
        /// </summary>
        /// 
        /// <returns>
        /// true if the <see cref="CyclicConcurrentQueue{T}"/> is empty; otherwise, false.
        /// </returns>
        public bool IsEmpty
        {
            get { return _head == _tail; }
        }

        /// <inheritdoc/>
        int ICollection.Count
        {
            get
            {
                long count;
                unchecked
                {
                    count = _head - _tail;
                }
                if (count > int.MaxValue)
                    throw new InvalidOperationException(String.Format("Count '{0}' is too big to be expressed as an interger.", count));
                return (int)count;
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.ICollection"/>.
        /// </summary>
        /// <returns>The number of elements contained in the <see cref="T:System.Collections.ICollection"/>.</returns>
        /// <remarks></remarks>
        public long Count
        {
            get
            {
                unchecked
                {
                    return _head - _tail;
                }
            }
        }

        /// <summary>
        /// This holds the Chunk size for each array chunk.
        /// </summary>
        private readonly int _chunkSize;

        /// <summary>
        /// This holds the Chunk size for each array chunk, log 2.
        /// </summary>
        private readonly int _chunkSizeLog2;

        /// <summary>
        /// Initializes a new instance of the <see cref="CyclicConcurrentQueue{T}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <remarks></remarks>
        public CyclicConcurrentQueue(long capacity)
        {
            if (capacity > MaxCapacity - 1)
                throw new ArgumentOutOfRangeException("capacity",
                                                      Resources.
                                                          LimitedConcurrentQueue_LimitedConcurrentQueue_Maximum_Capacity,
                                                      MaxCapacity.ToString());
            if (capacity < 1)
                throw new ArgumentOutOfRangeException("capacity",
                                                      Resources.
                                                          LimitedConcurrentQueue_LimitedConcurrentQueue_Minimum_Capacity);
            Capacity = capacity;
            _tail = 0;
            _head = 0;

            // We need to chunk up the capacity, to no more than 4096 chunks and no less than 8 elements per chunk.
            // For speed Chunks are powers of 2.
            _chunkSize = 8;
            long numChunks = capacity >> 3;
            _chunkSizeLog2 = 3;
            while (numChunks > 4096)
            {
                _chunkSize <<= 1;
                numChunks >>= 1;
                _chunkSizeLog2++;
            }
            if ((numChunks * _chunkSize) < capacity)
                numChunks++;

            _chunks = new Chunk[numChunks];
            int i = 0;
            for (; i < (numChunks - 1); i++)
            {
                _chunks[i] = new Chunk(_chunkSize);
                capacity -= _chunkSize;
            }
            _chunks[i] = new Chunk(capacity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CyclicConcurrentQueue{T}"/> class that contains elements copied from the specified collection
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="capacity">The capacity.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="collection"/> argument is null.</exception>
        /// <remarks></remarks>
        public CyclicConcurrentQueue([NotNull]IEnumerable<T> collection, long capacity)
            : this(capacity)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");
            int chunkNum = 0;
            long index = 0;
            foreach (T item in collection)
            {
                _chunks[chunkNum].Array[index] = item;
                index = (index + 1) % _chunkSize;
                if (index == 0)
                    chunkNum++;
                _head++;
                if( _head % capacity == 0 )
                {
                    index = 0;
                    chunkNum = 0;
                }
            }
            // If a wrap around occurred, ensure the tail catches up
            if( _head > capacity )
            {
                _tail = _head - capacity;
            }
        }

        /// <inheritdoc/>
        void ICollection.CopyTo(Array array, int index)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            // We snapshot the head now.
            long head = _head;

            // Grab the tail - this can jump forward during enumeration.
            long tail = _tail;

            long indexLong = index;
            unchecked
            {
                // Equivalent to tail < head using overflow arithmetic
                while ((head - tail) > 0)
                {
                    // Calculate the current Chunk
                    long tailIndex = tail.Mod(Capacity);
                    long slChunkNum = tailIndex >> _chunkSizeLog2;

                    // Grab the Chunk's spinlock.
                    Chunk chunk = _chunks[slChunkNum];
                    bool taken = false;
                    chunk.SpinLock.Enter(ref taken);

                    // If the tail has moved on, catch up.
                    if ((tail - _tail) < 0)
                        tail = _tail;

                    long count = head - tail;

                    // Recalculate current Chunk.
                    tailIndex = tail.Mod(Capacity);
                    long chunkNum = tailIndex >> _chunkSizeLog2;

                    // Ensure we're in the same Chunk and we still have items in the queue
                    if ((chunkNum == slChunkNum) &&
                        (count > 0))
                    {
                        // Calculate start index
                        long start = tailIndex & (_chunkSize - 1);

                        // Grab array Chunk
                        long length = chunk.Size - start;

                        // Calculate length to copy.
                        if (count < length) length = count;

                        Array.Copy(chunk.Array, start, array, indexLong, length);
                        indexLong += (int)length;
                        tail += length;
                    }

                    if (taken)
                        chunk.SpinLock.Exit();
                }
            }
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <inheritdoc/>
        bool IProducerConsumerCollection<T>.TryAdd(T item)
        {
            this.Enqueue(item);
            return true;
        }

        /// <inheritdoc/>
        bool IProducerConsumerCollection<T>.TryTake(out T item)
        {
            return this.TryDequeue(out item);
        }

        /// <inheritdoc/>
        [NotNull]
        public T[] ToArray()
        {
            return ((IEnumerable<T>)this).ToArray();
        }

        /// <inheritdoc/>
        public void CopyTo(T[] array, int index)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            ((ICollection)this).CopyTo(array, index);
        }

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator()
        {
            // We snapshot the head now.
            long head = _head;

            // Grab the tail - this can jump forward during enumeration.
            long tail = _tail;
            unchecked
            {
                // Equivalent to tail < head using overflow arithmetic
                while ((head - tail) > 0)
                {
                    // Calculate the current Chunk
                    long index = tail.Mod(Capacity);
                    long slChunkNum = index >> _chunkSizeLog2;

                    // Grab the Chunk's spinlock.
                    Chunk chunk = _chunks[slChunkNum];
                    bool taken = false;
                    chunk.SpinLock.Enter(ref taken);

                    // If the tail has moved on, catch up.
                    if ((tail - _tail) < 0)
                        tail = _tail;

                    long count = head - tail;

                    // Recalculate current Chunk.
                    index = tail.Mod(Capacity);
                    long chunkNum = index >> _chunkSizeLog2;

                    T[] chunkCopy;
                    // Ensure we're in the same Chunk and we still have items in the queue
                    if ((chunkNum == slChunkNum) &&
                        (count > 0))
                    {
                        // Calculate start index
                        long start = index & (_chunkSize - 1);

                        // Grab array Chunk
                        long length = chunk.Size - start;

                        // Calculate length to copy.
                        if (count < length) length = count;

                        chunkCopy = new T[length];
                        Array.Copy(chunk.Array, start, chunkCopy, 0, length);
                        tail += length;
                    }
                    else chunkCopy = null;

                    if (taken)
                        chunk.SpinLock.Exit();

                    // Yield the result
                    if (chunkCopy != null)
                        foreach (T item in chunkCopy)
                            yield return item;
                }
            }
        }

        /// <summary>
        /// Adds an object to the end of the <see cref="CyclicConcurrentQueue{T}"/>.
        /// </summary>
        /// <param name="item">The object to add to the end of the <see cref="CyclicConcurrentQueue{T}"/>. The value can be a null reference (Nothing in Visual Basic) for reference types.</param>
        public void Enqueue(T item)
        {
            do
            {
                // Calculate the current Chunk
                long index = _head.Mod(Capacity);
                long slChunkNum = index >> _chunkSizeLog2;

                // Grab the head Chunk's spinlock.
                Chunk chunk = _chunks[slChunkNum];
                bool taken = false;
                chunk.SpinLock.Enter(ref taken);

                long head = _head;
                long tail = _tail;

                index = head.Mod(Capacity);
                long chunkNum = index >> _chunkSizeLog2;

                // Ensure we're still in the same Chunk.
                bool queued;
                if (chunkNum == slChunkNum)
                {
                    long count;
                    unchecked
                    {
                        count = head - tail;
                    }

                    // If we catch the tail, move the tail on (no need to clear as we're about to overwrite anyway).
                    // Note in this scenario, tail must be in the same Chunk, so we don't need a new spin lock.
                    if (count >= Capacity)
                        _tail++;

                    // Add the item into the underlying array.
                    long elementNumberInChunk = index & (_chunkSize - 1);
                    chunk.Array[elementNumberInChunk] = item;

                    queued = true;
                    _head++;
                }
                else
                    queued = false;

                if (taken)
                    chunk.SpinLock.Exit();

                if (queued)
                    return;
            } while (true);
        }

        /// <summary>
        /// Attempts to remove and return the object at the beginning of the <see cref="CyclicConcurrentQueue{T}"/>.
        /// </summary>
        /// 
        /// <returns>
        /// true if an element was removed and returned from the beggining of the <see cref="CyclicConcurrentQueue{T}"/> succesfully; otherwise, false.
        /// </returns>
        /// <param name="result">When this method returns, if the operation was successful, <paramref name="result"/> contains the object removed. If no object was available to be removed, the value is unspecified.</param>
        public bool TryDequeue(out T result)
        {
            result = default(T);
            if (_head == _tail)
                return false;

            do
            {
                // Calculate the current Chunk
                long index = _tail.Mod(Capacity);
                long slChunkNum = index >> _chunkSizeLog2;

                // Grab the tail Chunk's spinlock.
                Chunk chunk = _chunks[slChunkNum];
                bool taken = false;
                chunk.SpinLock.Enter(ref taken);

                long tail = _tail;
                int found;
                if (_head == tail)
                    // Empty
                    found = 0;
                else
                {
                    index = tail.Mod(Capacity);
                    long chunkNum = index >> _chunkSizeLog2;

                    // Ensure we're still in the same Chunk.
                    if (chunkNum == slChunkNum)
                    {
                        found = 1;
                        long elementNumberInChunk = index & (_chunkSize - 1);
                        result = chunk.Array[elementNumberInChunk];
                        chunk.Array[elementNumberInChunk] = default(T);
                        _tail++;
                    }
                    else
                        // Need to loop to grab new spinlock.
                        found = -1;
                }

                if (taken)
                    chunk.SpinLock.Exit();

                if (found == 0) return false;
                if (found == 1) return true;
            } while (true);
        }

        /// <summary>
        /// Attempts to return an object from the beginning of the <see cref="CyclicConcurrentQueue{T}"/> without removing it.
        /// </summary>
        /// 
        /// <returns>
        /// true if and object was returned successfully; otherwise, false.
        /// </returns>
        /// <param name="result">When this method returns, <paramref name="result"/> contains an object from the beginning of the <see cref="CyclicConcurrentQueue{T}"/> or an unspecified value if the operation failed.</param>
        public bool TryPeek(out T result)
        {
            result = default(T);
            if (_head == _tail)
                return false;

            do
            {
                // Calculate the current Chunk
                long index = _tail.Mod(Capacity);
                long slChunkNum = index >> _chunkSizeLog2;

                // Grab the tail Chunk's spinlock.
                Chunk chunk = _chunks[slChunkNum];
                bool taken = false;
                chunk.SpinLock.Enter(ref taken);

                long tail = _tail;
                int found;
                if (_head == tail)
                    // Empty
                    found = 0;
                else
                {
                    index = tail.Mod(Capacity);
                    long chunkNum = index >> _chunkSizeLog2;

                    // Ensure we're still in the same Chunk.
                    if (chunkNum == slChunkNum)
                    {
                        found = 1;
                        long elementNumberInChunk = index & (_chunkSize - 1);
                        result = chunk.Array[elementNumberInChunk];
                    }
                    else
                        // Need to loop to grab new spinlock.
                        found = -1;
                }

                if (taken)
                    chunk.SpinLock.Exit();

                if (found == 0) return false;
                if (found == 1) return true;
            } while (true);
        }
    }
}