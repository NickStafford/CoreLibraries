﻿using System;
using System.Data.SqlClient;
using JetBrains.Annotations;

namespace WebApplications.Testing.Data.Exceptions
{
    /// <summary>
    /// A SqlException indicating the connection specified in the connection string is invalid.
    /// </summary>
    public class SqlInvalidConnectionException : SqlExceptionPrototype
    {
        /// <summary>
        /// Gets the error text.
        /// </summary>
        private static string ErrorText
        {
            get
            {
                return "A network-related or instance-specific error occurred while establishing a connection to SQL Server. " +
                    "The server was not found or was not accessible. Verify that the instance name is correct and that SQL " +
                    "Server is configured to allow remote connections. (provider: SQL Network Interfaces, error: 26 - Error Locating Server/Instance Specified)";
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlInvalidConnectionException"/> class.
        /// </summary>
        /// <param name="conId">The con id.</param>
        public SqlInvalidConnectionException(Guid conId = default(Guid))
            : base(GenerateCollection(-1, 1, 20, ErrorText), "9.0.0.0", conId)
        { }

        /// <summary>
        /// Prevents a default instance of the <see cref="SqlInvalidConnectionException"/> class from being created.
        /// </summary>
        /// <param name="exception">The exception.</param>
        private SqlInvalidConnectionException([NotNull] SqlException exception)
            : base(exception)
        { }

        /// <summary>
        /// Implicit conversion from <see cref="SqlInvalidConnectionException" /> to <see cref="SqlException" />.
        /// </summary>
        /// <param name="prototype">The prototype.</param>
        /// <returns>The result of the operator.</returns>
        /// <remarks></remarks>
        public static implicit operator SqlException(SqlInvalidConnectionException prototype)
        {
            return prototype != null
                       ? prototype.SqlException
                       : null;
        }

        /// <summary>
        /// Implicit conversion from <see cref="SqlException" /> to <see cref="SqlInvalidConnectionException" />.
        /// </summary>
        /// <param name="exception">The SQL exception.</param>
        /// <returns>The result of the operator.</returns>
        /// <remarks></remarks>
        public static implicit operator SqlInvalidConnectionException(SqlException exception)
        {
            return exception != null
                       ? new SqlInvalidConnectionException(exception)
                       : null;

        }
    }
}