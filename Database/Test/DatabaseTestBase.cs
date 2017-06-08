﻿#region © Copyright Web Applications (UK) Ltd, 2011.  All rights reserved.
// Solution: Utilities.Database 
// Project: Utilities.Database.Test
// File: TestBase.cs
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
// © Copyright Web Applications (UK) Ltd, 2011.  All rights reserved.
#endregion

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using WebApplications.Testing;
using WebApplications.Utilities.Annotations;
using WebApplications.Utilities.Logging;

namespace WebApplications.Utilities.Database.Test
{
    [DeploymentItem("Data", "Data")]
    [DeploymentItem("Resources\\", "Resources")]
    public abstract class DatabaseTestBase : TestBase
    {
        public const string LocalDatabaseConnectionName = "LocalData";
        public const string LocalDatabaseCopyConnectionName = "LocalDataCopy";
        public const string DifferentLocalDatabaseConnectionName = "DifferentLocalData";

        [NotNull]
        protected static readonly Random Random = new Random();

        [NotNull]
        public static string GetConnectionString([NotNull] string connectionName)
        {
            switch (connectionName?.ToLowerInvariant())
            {
                case "localdata": return LocalDatabaseConnectionString;
                case "localdatacopy": return LocalDatabaseCopyConnectionString;
                case "differentlocaldata": return DifferentLocalDatabaseConnectionString;
                default: return connectionName;
            }
        }

        [NotNull]
        protected static readonly string LocalDatabaseConnectionString = CreateConnectionString(LocalDatabaseConnectionName);

        [NotNull]
        protected static readonly string LocalDatabaseCopyConnectionString = CreateConnectionString(LocalDatabaseCopyConnectionName);

        [NotNull]
        protected static readonly string DifferentLocalDatabaseConnectionString = CreateConnectionString(DifferentLocalDatabaseConnectionName);

        [NotNull]
        protected static readonly Connection LocalDatabaseConnection = new Connection(LocalDatabaseConnectionString);

        [NotNull]
        protected static readonly Connection LocalDatabaseCopyConnection = new Connection(LocalDatabaseCopyConnectionString);

        [NotNull]
        protected static readonly Connection DifferentLocalDatabaseConnection = new Connection(DifferentLocalDatabaseConnectionString);

        [NotNull]
        private static readonly ConcurrentDictionary<(string con, string prog), string> _programText =
            new ConcurrentDictionary<(string, string), string>();

        private double _testStartTicks, _testEndTicks;

        /// <summary>
        /// Lazy loader for database connection
        /// </summary>
        [NotNull]
        private readonly Lazy<LoadBalancedConnection> _conn;

        /// <summary>
        /// Static constructor of the <see cref="T:System.Object"/> class, used to initialize the locatoin of the data directory for all tests.
        /// </summary>
        /// <remarks></remarks>
        static DatabaseTestBase()
        {
            // Find the data directory
            string path = Path.GetDirectoryName(typeof (DatabaseTestBase).Assembly.Location);
            string root = Path.GetPathRoot(path);
            string dataDirectory;
            do
            {
                // Look recursively for directory called Data containing mdf files.
                dataDirectory = Directory.GetDirectories(path, "Data", SearchOption.AllDirectories)
                    .SingleOrDefault(d => Directory.GetFiles(d, "*.mdf", SearchOption.TopDirectoryOnly).Any());

                // Move up a directory
                path = Path.GetDirectoryName(path);
            } while ((dataDirectory == null) &&
                     !String.IsNullOrWhiteSpace(path) &&
                     !path.Equals(root, StringComparison.CurrentCultureIgnoreCase));

            if (dataDirectory == null)
                throw new InvalidOperationException("Could not find the data directory.");

            // Set the DataDirectory data in the current AppDomain for use in connection strings.
            AppDomain.CurrentDomain.SetData("DataDirectory", dataDirectory);
        }

        protected DatabaseTestBase()
        {
            _conn = new Lazy<LoadBalancedConnection>(() => new LoadBalancedConnection(LocalDatabaseConnectionString));
        }

        /// <summary>
        /// Returns a database connection
        /// </summary>
        /// <value>A database connection.</value>
        protected LoadBalancedConnection Connection
        {
            get { return _conn.Value; }
        }

        /// <summary>
        /// Creates the connection string.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        /// <returns>System.String.</returns>
        [NotNull]
        protected static string CreateConnectionString(string databaseName)
        {
            return
                $@"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\{databaseName
                    }.mdf;Integrated Security=True;Connect Timeout=30;";
        }

        /// <summary>
        /// Gets the program text.
        /// </summary>
        /// <param name="programName">Name of the program.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        [NotNull]
        public static string GetProgramText([NotNull] string programName, [NotNull] string connectionString)
        {
            connectionString = GetConnectionString(connectionString);

            return _programText.GetOrAdd(
                (connectionString, programName.ToLowerInvariant()),
                _ =>
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        const string sql = "SELECT [definition] FROM sys.sql_modules WHERE [object_id] = OBJECT_ID(@name);";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@name", programName);
                            string def = (string)command.ExecuteScalar();
                            int start = def.IndexOf("AS", StringComparison.InvariantCultureIgnoreCase);
                            Debug.Assert(start > 0);
                            return def.Substring(start + 2);
                        }
                    }
                });
        }

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            Trace.WriteLine($"Begin test: {TestContext.TestName}");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            _testStartTicks = Stopwatch.GetTimestamp();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _testEndTicks = Stopwatch.GetTimestamp();
            Trace.WriteLine(
                $"Ending test: {TestContext.TestName}, time taken {1000 * (_testEndTicks - _testStartTicks) / Stopwatch.Frequency:N3}ms");
            Log.Flush().GetAwaiter().GetResult();
        }

    }
}