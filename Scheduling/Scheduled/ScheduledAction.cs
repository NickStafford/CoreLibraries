﻿#region © Copyright Web Applications (UK) Ltd, 2014.  All rights reserved.
// Copyright (c) 2014, Web Applications UK Ltd
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of Web Applications UK Ltd nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL WEB APPLICATIONS UK LTD BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using WebApplications.Utilities.Caching;
using WebApplications.Utilities.Logging;

namespace WebApplications.Utilities.Scheduling.Scheduled
{
    /// <summary>
    /// Holds a scheduled action.
    /// </summary>
    /// <remarks></remarks>
    public abstract class ScheduledAction : IEquatable<ScheduledAction>
    {
        #region Delegates
        /// <summary>
        /// Delegate describing a schedulable action.
        /// </summary>
        /// <returns>An awaitable task.</returns>
        public delegate void SchedulableAction();

        /// <summary>
        /// Delegate describing a schedulable action which accepts a due date and time.
        /// </summary>
        /// <param name="due">The due date and time (UTC) which indicates when the action was scheduled to run.</param>
        /// <returns>An awaitable task.</returns>
        public delegate void SchedulableDueAction(DateTime due);

        /// <summary>
        /// Delegate describing an asynchronous schedulable action.
        /// </summary>
        /// <returns>An awaitable task.</returns>
        public delegate Task SchedulableActionAsync();

        /// <summary>
        /// Delegate describing an asynchronous schedulable action which accepts a due date and time.
        /// </summary>
        /// <param name="due">The due date and time (UTC) which indicates when the action was scheduled to run.</param>
        /// <returns>An awaitable task.</returns>
        public delegate Task SchedulableDueActionAsync(DateTime due);

        /// <summary>
        /// Delegate describing an asynchronous schedulable action which supports cancellation.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns>An awaitable task.</returns>
        public delegate Task SchedulableCancellableActionAsync(CancellationToken token);

        /// <summary>
        /// Delegate describing an asynchronous schedulable action which accepts a due date and time and supports cancellation.
        /// </summary>
        /// <param name="due">The due date and time (UTC) which indicates when the action was scheduled to run.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>An awaitable task.</returns>
        public delegate Task SchedulableDueCancellableActionAsync(DateTime due, CancellationToken token);
        #endregion

        /// <summary>
        /// Unique identifier for the action.
        /// </summary>
        internal readonly CombGuid ID = CombGuid.NewCombGuid();

        /// <summary>
        /// Holds the scheduler.
        /// </summary>
        [NotNull]
        [PublicAPI]
        public readonly Scheduler Scheduler;

        /// <summary>
        /// Internal list of results.
        /// </summary>
        [CanBeNull]
        protected readonly CyclicConcurrentQueue<ScheduledActionResult> HistoryQueue;

        /// <summary>
        /// The maximum history.
        /// </summary>
        [PublicAPI]
        public readonly int MaximumHistory;

        /// <summary>
        /// The return type (if a function).
        /// </summary>
        [PublicAPI]
        [CanBeNull]
        public readonly Type ReturnType;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduledAction" /> class.
        /// </summary>
        /// <param name="scheduler">The scheduler.</param>
        /// <param name="schedule">The schedule.</param>
        /// <param name="maximumHistory">The maximum history.</param>
        protected ScheduledAction(
            [NotNull] Scheduler scheduler,
            [NotNull] ISchedule schedule,
            int maximumHistory,
            Type returnType)
        {
            Contract.Requires(scheduler != null);
            Contract.Requires(schedule != null);
            Enabled = true;
            Scheduler = scheduler;
            _lastExecutionFinished = DateTime.MinValue;
            _schedule = schedule;
            MaximumHistory = maximumHistory;
            ReturnType = returnType;
            HistoryQueue = MaximumHistory > 0 ? new CyclicConcurrentQueue<ScheduledActionResult>(MaximumHistory) : null;
            RecalculateNextDue();
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a function.
        /// </summary>
        /// <value><see langword="true" /> if this instance is function; otherwise, <see langword="false" />.</value>
        public bool IsFunction {get { return !ReferenceEquals(ReturnType, null); }}

        /// <summary>
        /// Gets the execution history.
        /// </summary>
        /// <value>The history.</value>
        [PublicAPI]
        [NotNull]
        public IEnumerable<ScheduledActionResult> History
        {
            get { return HistoryQueue ?? Enumerable.Empty<ScheduledActionResult>(); }
        }

        /// <summary>
        /// Holds the schedule.
        /// </summary>
        [NotNull]
        private ISchedule _schedule;

        /// <summary>
        /// Gets or sets the schedule.
        /// </summary>
        /// <value>The schedule.</value>
        [PublicAPI]
        [NotNull]
        public ISchedule Schedule
        {
            get { return _schedule; }
            set
            {
                Contract.Requires(value != null);
                if (ReferenceEquals(_schedule, value))
                    return;
                _schedule = value;
                RecalculateNextDue();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ScheduledAction"/> is enabled.
        /// </summary>
        /// <value><see langword="true" /> if enabled; otherwise, <see langword="false" />.</value>
        [PublicAPI]
        public bool Enabled;

        
        /// <summary>
        /// Execution counter indicates how many concurrent executions are occurring.
        /// </summary>
        private int _executing;

        /// <summary>
        /// Executes the action asynchronously, so long as it is enabled and not already running (unless the <see cref="Schedule">schedules</see>
        /// <see cref="ISchedule.Options"/> is set to <see cref="ScheduleOptions.AllowConcurrent"/>.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>And awaitable task containing the result, or <see langword="null"/> if the action was not run.</returns>
        [NotNull]
        public Task<ScheduledActionResult> ExecuteAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            if (!Enabled)
                return TaskResult<ScheduledActionResult>.Default;

            // Grab schedule as the property can be changed.
            ISchedule schedule = _schedule;
            
            // Mark this action as executing.
            int executing = Interlocked.Increment(ref _executing);
            // Only execute if we allow concurrency or we're not executing already.
            if (!schedule.Options.HasFlag(ScheduleOptions.AllowConcurrent) &&
                (executing > 1))
            {
                Interlocked.Decrement(ref _executing);
                return TaskResult<ScheduledActionResult>.Default;
            }

            // Get the due date, and set to not due.
            long ndt = Interlocked.Exchange(ref _nextDueTicks, Scheduler.MaxTicks);
            DateTime due = new DateTime(ndt, DateTimeKind.Utc);

            // Add continuation task to store result on completion.
            return DoExecuteAsync(due, cancellationToken)
                .ContinueWith(
                    t =>
                    {
                        Debug.Assert(t != null);

                        // Decrement the execution counter.
                        Interlocked.Decrement(ref _executing);

                        // Increment the execution count.
                        Interlocked.Increment(ref _executionCount);

                        // Mark execution finish (and calculate next due).
                        LastExecutionFinished = DateTime.UtcNow;

                        // Enqueue history item.
                        if (HistoryQueue != null &&
                            t.IsCompleted)
                            HistoryQueue.Enqueue(t.Result);
                        return t.Result;
                    },
                    TaskContinuationOptions.ExecuteSynchronously);
        }

        /// <summary>
        /// Performs the asynchronous execution.
        /// </summary>
        /// <param name="due">The due date and time (UTC).</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;ScheduledActionResult&gt;.</returns>
        [NotNull]
        protected abstract Task<ScheduledActionResult> DoExecuteAsync(DateTime due, CancellationToken cancellationToken);

        /// <summary>
        /// The next time the action is due.
        /// </summary>
        private long _nextDueTicks;

        /// <summary>
        /// Gets the next due date and time (UTC).
        /// </summary>
        /// <value>The next due date and time.</value>
        [PublicAPI]
        public DateTime NextDue
        {
            get
            {
                long ndt = Interlocked.Read(ref _nextDueTicks);
                return new DateTime(ndt, DateTimeKind.Utc);
            }
        }

        private DateTime _lastExecutionFinished;

        /// <summary>
        /// Gets the date and time (UTC) that the last execution finished.
        /// </summary>
        /// <value>The last execution finished date and time.</value>
        [PublicAPI]
        public DateTime LastExecutionFinished
        {
            get { return _lastExecutionFinished; }
            private set
            {
                if (_lastExecutionFinished >= value) return;
                _lastExecutionFinished = value;
                RecalculateNextDue();
            }
        }

        private long _executionCount;

        /// <summary>
        /// Gets the execution count.
        /// </summary>
        /// <value>The execution count.</value>
        [PublicAPI]
        public long ExecutionCount
        {
            get { return _executionCount; }
        }

        /// <summary>
        /// Lock used in the case optimistic setting fails.
        /// </summary>
        private SpinLock _calculatorLock = new SpinLock();

        /// <summary>
        /// Recalculates the next due date.
        /// </summary>
        /// <param name="withLock">if set to <see langword="true"/> uses a lock.</param>
        /// <remarks></remarks>
        private void RecalculateNextDue(bool withLock = false)
        {
            bool hasLock = false;
            if (withLock)
                _calculatorLock.Enter(ref hasLock);

            DateTime now = DateTime.UtcNow;
            // Optimistic update strategy, to avoid locks.

            // Grab current value for nextDue.
            long ndt = Interlocked.Read(ref _nextDueTicks);
            DateTime nd = new DateTime(ndt, DateTimeKind.Utc);

            // If we're due now, we're done.
            if (nd == now) return;

            // Ask schedule for next due time.
            DateTime newNextDue = Schedule.Next(now > _lastExecutionFinished ? now : _lastExecutionFinished);
            
            // If the next due is in the past, set it to due now.
            if (newNextDue < now) newNextDue = now;

            // If the new due date is the existing one we're done.
            if (newNextDue == nd)
                return;

            long nndt = newNextDue.Ticks;

            // If we successfully update next ticks we're done.
            if (Interlocked.CompareExchange(ref _nextDueTicks, nndt, ndt) != ndt)
            {
                if (!withLock)
                {
                    // Try again with a spin lock.
                    RecalculateNextDue(true);
                    return;
                }
                Log.Add(LoggingLevel.Critical, () => Resource.ScheduledAction_RecalculateNextDue_Failed);
            }

            if (hasLock)
                _calculatorLock.Exit();

            // Notify the scheduler that we've changed our due date.
            if (nndt < Scheduler.MaxTicks)
            {
                Trace.WriteLine(string.Format("Recalc: {0:ss.fffffff}", now, NextDue));
                Scheduler.CheckSchedule();
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><see langword="true" /> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <see langword="false" />.</returns>
        public override bool Equals([CanBeNull]object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            ScheduledAction a = obj as ScheduledAction;
            return !ReferenceEquals(a, null) && Equals(a);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals([CanBeNull]ScheduledAction other)
        {
            if (ReferenceEquals(null, other)) return false;
            return ReferenceEquals(this, other) || ID.Equals(other.ID);
        }

        /// <summary>
        /// Indicates whether the left object is equal to the right object of the same type.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>true if the <paramref name="left" /> parameter is equal to the <paramref name="right" /> parameter; otherwise, false.</returns>
        [PublicAPI]
        public static bool Equals([CanBeNull] ScheduledAction left, [CanBeNull] ScheduledAction right)
        {
            if (ReferenceEquals(null, left)) return ReferenceEquals(null, right);
            if (ReferenceEquals(null, right)) return false;
            return ReferenceEquals(left, right) || left.ID.Equals(right.ID);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        /// <summary>
        /// Implements the ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==([CanBeNull]ScheduledAction left, [CanBeNull] ScheduledAction right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Implements the !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(ScheduledAction left, ScheduledAction right)
        {
            return !Equals(left, right);
        }
    }
}