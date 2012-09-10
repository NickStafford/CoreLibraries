#region � Copyright Web Applications (UK) Ltd, 2012.  All rights reserved.
// Copyright (c) 2012, Web Applications UK Ltd
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
using JetBrains.Annotations;
using WebApplications.Utilities.Logging;

namespace WebApplications.Utilities.Database.Exceptions
{
    /// <summary>
    ///   Exceptions thrown during schema parsing.
    /// </summary>
    public class DatabaseSchemaException : LoggingException
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="LoggingException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="parameters">
        ///   The parameters, which are formatted by the <paramref name="message"/> <see cref="string"/>.
        /// </param>
        public DatabaseSchemaException([NotNull] string message, [NotNull] params object[] parameters)
            : base(message, parameters)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="LoggingException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="level">The severity of the exception being logged.</param>
        /// <param name="parameters">
        ///   The parameters, which are formatted by the <paramref name="message"/> <see cref="string"/>.
        /// </param>
        public DatabaseSchemaException([NotNull] string message, LogLevel level, [NotNull] params object[] parameters)
            : base(message, level, parameters)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="LoggingException"/> class.
        /// </summary>
        /// <param name="innerException">
        ///   The exception that occurred during parsing.
        /// </param>
        /// <param name="level">The severity of the exception being logged.</param>
        public DatabaseSchemaException([NotNull] Exception innerException, LogLevel level) : base(innerException, level)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="LoggingException"/> class.
        /// </summary>
        /// <param name="innerException">
        ///   The exception that occurred during parsing.
        /// </param>
        /// <param name="message">The exception message.</param>
        /// <param name="parameters">
        ///   The parameters, which are formatted by the <paramref name="message"/> <see cref="string"/>.
        /// </param>
        public DatabaseSchemaException([CanBeNull] Exception innerException, [NotNull] string message,
                                       [NotNull] params object[] parameters) : base(innerException, message, parameters)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="LoggingException"/> class.
        /// </summary>
        /// <param name="innerException">
        ///   The exception that occurred during parsing.
        /// </param>
        /// <param name="message">The exception message.</param>
        /// <param name="level">The severity of the exception being logged.</param>
        /// <param name="parameters">
        ///   The parameters, which are formatted by the <paramref name="message"/> <see cref="string"/>.
        /// </param>
        public DatabaseSchemaException([CanBeNull] Exception innerException, [NotNull] string message, LogLevel level,
                                       [NotNull] params object[] parameters)
            : base(innerException, message, level, parameters)
        {
        }
    }
}