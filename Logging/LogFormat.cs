﻿using System;
using System.ComponentModel;

namespace WebApplications.Utilities.Logging
{
    /// <summary>
    /// Log formatting flags
    /// </summary>
    [Flags]
    public enum LogFormat
    {
        /// <summary>
        /// Nothing displayed.
        /// </summary>
        [Description("Nothing displayed.")]
        None = 0,

        /// <summary>
        /// Include the message.
        /// </summary>
        [Description("Include the message.")]
        Message = 1 << 0,
        /// <summary>
        /// Include the message.
        /// </summary>
        [Description("Include the message.")]
        M = Message,

        /// <summary>
        /// Include the time stamp.
        /// </summary>
        [Description("Include the time stamp.")]
        TimeStamp = 1 << 1,
        /// <summary>
        /// Include the time stamp.
        /// </summary>
        [Description("Include the time stamp.")]
        D = TimeStamp,

        /// <summary>
        /// Include the level.
        /// </summary>
        [Description("Include the level.")]
        Level = 1 << 2,
        /// <summary>
        /// Include the level.
        /// </summary>
        [Description("Include the level.")]
        L = Level,

        /// <summary>
        /// Include the GUID.
        /// </summary>
        [Description("Include the GUID.")]
        Guid = 1 << 3,
        /// <summary>
        /// Include the GUID.
        /// </summary>
        [Description("Include the GUID.")]
        I = Guid,

        /// <summary>
        /// Include the Group.
        /// </summary>
        [Description("Include the Group.")]
        Group = 1 << 4,
        /// <summary>
        /// Include the Group.
        /// </summary>
        [Description("Include the Group.")]
        G = Group,

        /// <summary>
        /// Incldude additional context.
        /// </summary>
        [Description("Include additional context.")]
        Context = 1 << 5,
        /// <summary>
        /// Incldude additional context.
        /// </summary>
        [Description("Include additional context.")]
        C = Context,

        /// <summary>
        /// Include the exception type.
        /// </summary>
        [Description("Include the exception type.")]
        Exception = 1 << 6,
        /// <summary>
        /// Include the exception type.
        /// </summary>
        [Description("Include the exception type.")]
        E = Exception,

        /// <summary>
        /// Include the SQL Exception info.
        /// </summary>
        [Description("Include the SQL Exception info.")]
        SQLException = 1 << 7,
        /// <summary>
        /// Include the SQL Exception info.
        /// </summary>
        [Description("Include the SQL Exception info.")]
        S = SQLException,

        /// <summary>
        /// Include the stack trace.
        /// </summary>
        [Description("Include the stack trace.")]
        StackTrace = 1 << 8,
        /// <summary>
        /// Include the stack trace.
        /// </summary>
        [Description("Include the stack trace.")]
        T = StackTrace,

        /// <summary>
        /// Include the thread ID.
        /// </summary>
        [Description("Include the thread ID.")]
        ThreadID = 1 << 9,
        /// <summary>
        /// Include the thread ID.
        /// </summary>
        [Description("Include the thread ID.")]
        TD = ThreadID,

        /// <summary>
        /// Include the thread Name.
        /// </summary>
        [Description("Include the thread name.")]
        ThreadName = 1 << 10,
        /// <summary>
        /// Include the thread Name.
        /// </summary>
        [Description("Include the thread name.")]
        TN = ThreadName,

        /// <summary>
        /// Include the thread culture.
        /// </summary>
        [Description("Include the thread culture.")]
        ThreadCulture = 1 << 11,
        /// <summary>
        /// Include the thread culture.
        /// </summary>
        [Description("Include the thread culture.")]
        TC = ThreadCulture,

        /// <summary>
        /// Include the thread UI culture.
        /// </summary>
        [Description("Include the thread UI culture.")]
        ThreadUICulture = 1 << 12,
        /// <summary>
        /// Include the thread UI culture.
        /// </summary>
        [Description("Include the thread UI culture.")]
        TU = ThreadUICulture,

        /// <summary>
        /// Include a header and footer.
        /// </summary>
        [Description("Include a header and footer")]
        Header = 1 << 29,
        /// <summary>
        /// Include a header and footer.
        /// </summary>
        [Description("Include a header and footer")]
        H = Header,

        /// <summary>
        /// When set will include elements even if missing.
        /// </summary>
        [Description("When set will include elements even if missing.")]
        IncludeMissing = 1 << 30,
        /// <summary>
        /// When set will include elements even if missing.
        /// </summary>
        [Description("When set will include elements even if missing.")]
        X=IncludeMissing,

        /// <summary>
        /// Includes everything (except missing elements).
        /// </summary>
        [Description("Includes everything (except missing elements).")]
        Verbose = Message | TimeStamp | Level | Guid | Group | ThreadID | ThreadName | ThreadCulture | ThreadUICulture | Context | Exception | SQLException | StackTrace | Header,
        /// <summary>
        /// Includes everything (except missing elements).
        /// </summary>
        [Description("Includes everything (except missing elements).")]
        V = Verbose,

        /// <summary>
        /// Includes essential information.
        /// </summary>
        [Description("Includes main information.")]
        General = Message | TimeStamp | Level | Group | ThreadName | Exception | SQLException,
        /// <summary>
        /// Includes essential information.
        /// </summary>
        [Description("Includes main information.")]
        R = General,

        /// <summary>
        /// Includes essential information.
        /// </summary>
        [Description("Includes essential information.")]
        Basic = Message | TimeStamp | Level | Exception | SQLException,
        /// <summary>
        /// Includes essential information.
        /// </summary>
        [Description("Includes essential information.")]
        B = Basic,

        /// <summary>
        /// Includes everything (even when missing).
        /// </summary>
        [Description("Includes everything (even when missing).")]
        All = Verbose | IncludeMissing
    }
}