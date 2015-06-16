﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplications.Utilities.Logging {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WebApplications.Utilities.Logging.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Application must be running in the console to use the ConsoleLogger..
        /// </summary>
        internal static string ConsoleLogger_NotConsole {
            get {
                return ResourceManager.GetString("ConsoleLogger_NotConsole", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Contract failed. {0}.
        /// </summary>
        internal static string Contract_Failed {
            get {
                return ResourceManager.GetString("Contract_Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {1}. {0}.
        /// </summary>
        internal static string Contract_Failed_Message {
            get {
                return ResourceManager.GetString("Contract_Failed_Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The event logger event log name cannot be empty..
        /// </summary>
        internal static string EventLogger_EventLogCannotBeNull {
            get {
                return ResourceManager.GetString("EventLogger_EventLogCannotBeNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The machine name can not be null.
        /// </summary>
        internal static string EventLogger_MachineNameCannotBeNull {
            get {
                return ResourceManager.GetString("EventLogger_MachineNameCannotBeNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The event logger name cannot be empty..
        /// </summary>
        internal static string EventLogger_NameCannotBeNull {
            get {
                return ResourceManager.GetString("EventLogger_NameCannotBeNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The file buffer size &apos;{0}&apos; cannot exceed the maximum value of an int..
        /// </summary>
        internal static string FileLogger_BufferSize_Too_Big {
            get {
                return ResourceManager.GetString("FileLogger_BufferSize_Too_Big", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The file  buffer size &apos;{0}&apos; cannot be less than 128 bytes..
        /// </summary>
        internal static string FileLogger_BufferSize_Too_Small {
            get {
                return ResourceManager.GetString("FileLogger_BufferSize_Too_Small", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not create file logger as error occurred when trying to create/access directory &apos;{0}&apos;..
        /// </summary>
        internal static string FileLogger_DirectoryAccessOrCreationError {
            get {
                return ResourceManager.GetString("FileLogger_DirectoryAccessOrCreationError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Finished writing logs to &apos;{0}&apos;.
        /// </summary>
        internal static string FileLogger_Ended_File {
            get {
                return ResourceManager.GetString("FileLogger_Ended_File", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot create file logger as extension &apos;{0}&apos; has invalid characters (only letters and digits allowed)..
        /// </summary>
        internal static string FileLogger_Extension_Invalid_Char {
            get {
                return ResourceManager.GetString("FileLogger_Extension_Invalid_Char", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot create file logger as extension &apos;{0}&apos; has more than five characters..
        /// </summary>
        internal static string FileLogger_ExtensionLongerThanFiveCharacters {
            get {
                return ResourceManager.GetString("FileLogger_ExtensionLongerThanFiveCharacters", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred whilst trying to create the &apos;{0}&apos; log file..
        /// </summary>
        internal static string FileLogger_File_Creation_Failed {
            get {
                return ResourceManager.GetString("FileLogger_File_Creation_Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Trying to find a unique log file name failed too many times..
        /// </summary>
        internal static string FileLogger_File_Creation_Retry_Failed {
            get {
                return ResourceManager.GetString("FileLogger_File_Creation_Retry_Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot create file logger as maximum log file duration &apos;{0}&apos; was less than 10s, which would cause too many log files to be created..
        /// </summary>
        internal static string FileLogger_FileDurationLessThanTenSeconds {
            get {
                return ResourceManager.GetString("FileLogger_FileDurationLessThanTenSeconds", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot create file logger as file name format &apos;{0}&apos; contains invalid characters..
        /// </summary>
        internal static string FileLogger_FileNameFormatInvalid {
            get {
                return ResourceManager.GetString("FileLogger_FileNameFormatInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot create file logger as file name format not specified..
        /// </summary>
        internal static string FileLogger_FileNameFormatNotSpecified {
            get {
                return ResourceManager.GetString("FileLogger_FileNameFormatNotSpecified", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot create file logger as file name format &apos;{0}&apos; led to invalid path creation..
        /// </summary>
        internal static string FileLogger_InvalidPathCreation {
            get {
                return ResourceManager.GetString("FileLogger_InvalidPathCreation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot create file logger as maximum logs per file &apos;{0}&apos; was less than 10, which would cause too many log files to be created..
        /// </summary>
        internal static string FileLogger_MaximumLogsLessThanTen {
            get {
                return ResourceManager.GetString("FileLogger_MaximumLogsLessThanTen", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot create file logger as file name format &apos;{0}&apos; led to invalid path creation. The file cannot be created outside the specified directory..
        /// </summary>
        internal static string FileLogger_PathCreatedOutsideDirectory {
            get {
                return ResourceManager.GetString("FileLogger_PathCreatedOutsideDirectory", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Began writing logs to &apos;{0}&apos; with &apos;{1}&apos; buffer..
        /// </summary>
        internal static string FileLogger_Started_File {
            get {
                return ResourceManager.GetString("FileLogger_Started_File", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Application &apos;{0}&apos; [{1}] exiting..
        /// </summary>
        internal static string Log_Application_Exiting {
            get {
                return ResourceManager.GetString("Log_Application_Exiting", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Finished logging clean up..
        /// </summary>
        internal static string Log_Cleanup_Finished {
            get {
                return ResourceManager.GetString("Log_Cleanup_Finished", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Application &apos;{0}&apos; [{1}] configured..
        /// </summary>
        internal static string Log_Configured {
            get {
                return ResourceManager.GetString("Log_Configured", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot use both a format string and a resource..
        /// </summary>
        internal static string Log_FormatAndResource {
            get {
                return ResourceManager.GetString("Log_FormatAndResource", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Fatal error trying to retrieve resource manager for the &apos;{0}&apos; type.
        /// </summary>
        internal static string Log_GetResourceManager_FatalError {
            get {
                return ResourceManager.GetString("Log_GetResourceManager_FatalError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unexpected singular format &apos;{0}&apos;..
        /// </summary>
        internal static string Log_Invalid_Format_Singular {
            get {
                return ResourceManager.GetString("Log_Invalid_Format_Singular", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid log format - cannot output as both XML &amp; JSON..
        /// </summary>
        internal static string Log_Invalid_Format_XML_JSON {
            get {
                return ResourceManager.GetString("Log_Invalid_Format_XML_JSON", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The &apos;{0}&apos; logger cannot be added as there is a logger present of the same type that does not support multiple instances..
        /// </summary>
        internal static string Log_Logger_Multiple_Instances {
            get {
                return ResourceManager.GetString("Log_Logger_Multiple_Instances", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A format or resource, and/or an exception must be specified..
        /// </summary>
        internal static string Log_NoMessage {
            get {
                return ResourceManager.GetString("Log_NoMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An unhandled exception has occured. See inner exception for details..
        /// </summary>
        internal static string Log_OnUnhandledException {
            get {
                return ResourceManager.GetString("Log_OnUnhandledException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Performance counters cannot be accessed..
        /// </summary>
        internal static string Log_PerfCategory_Access_Denied {
            get {
                return ResourceManager.GetString("Log_PerfCategory_Access_Denied", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Performance counters can be found with instance GUID &apos;{0}&apos;..
        /// </summary>
        internal static string Log_PerfCategory_GUID {
            get {
                return ResourceManager.GetString("Log_PerfCategory_GUID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The &apos;{0}&apos; format is not valid for a log.  See the LogFormat enumeration for valid values..
        /// </summary>
        internal static string Log_ToString_Invalid_Format {
            get {
                return ResourceManager.GetString("Log_ToString_Invalid_Format", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The reservation cannot be Guid.Empty..
        /// </summary>
        internal static string LogContext_Empty_Reservation {
            get {
                return ResourceManager.GetString("LogContext_Empty_Reservation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The LogContext reservation cannot be Guid.Empty..
        /// </summary>
        internal static string LogContext_Invalid_Reservation {
            get {
                return ResourceManager.GetString("LogContext_Invalid_Reservation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The &apos;{0}&apos; key has already been reserved with a different reservation..
        /// </summary>
        internal static string LogContext_Key_Already_Reserved {
            get {
                return ResourceManager.GetString("LogContext_Key_Already_Reserved", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Keys can only contain letters, digits or spaces..
        /// </summary>
        internal static string LogContext_Key_Invalid_Char {
            get {
                return ResourceManager.GetString("LogContext_Key_Invalid_Char", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Key must start with a letter..
        /// </summary>
        internal static string LogContext_Key_Invalid_First_Char {
            get {
                return ResourceManager.GetString("LogContext_Key_Invalid_First_Char", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The &apos;{0}&apos; key matches the &apos;{1}&apos; prefix - which has already been reserved..
        /// </summary>
        internal static string LogContext_Key_Reservation_Failed_Prefix_Match {
            get {
                return ResourceManager.GetString("LogContext_Key_Reservation_Failed_Prefix_Match", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The log context key &apos;{0}&apos; is longer than {1} characters (too long)..
        /// </summary>
        internal static string LogContext_Key_Too_Long {
            get {
                return ResourceManager.GetString("LogContext_Key_Too_Long", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The log context key &apos;{0}&apos; is less than {1} characters (too short)..
        /// </summary>
        internal static string LogContext_Key_Too_Short {
            get {
                return ResourceManager.GetString("LogContext_Key_Too_Short", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot change the values in a locked LogContext..
        /// </summary>
        internal static string LogContext_Locked {
            get {
                return ResourceManager.GetString("LogContext_Locked", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The log context key cannot be null..
        /// </summary>
        internal static string LogContext_Null_Key {
            get {
                return ResourceManager.GetString("LogContext_Null_Key", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The log context key prefix cannot be null..
        /// </summary>
        internal static string LogContext_Null_Prefix {
            get {
                return ResourceManager.GetString("LogContext_Null_Prefix", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The &apos;{0}&apos; key prefix has already been reserved with a different reservation..
        /// </summary>
        internal static string LogContext_Prefix_Already_Reserved {
            get {
                return ResourceManager.GetString("LogContext_Prefix_Already_Reserved", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The &apos;{0}&apos; key prefix collides with the &apos;{1}&apos; key reservation..
        /// </summary>
        internal static string LogContext_Prefix_Reservation_Failed_Key_Match {
            get {
                return ResourceManager.GetString("LogContext_Prefix_Reservation_Failed_Key_Match", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The &apos;{0}&apos; key prefix matches the &apos;{1}&apos; prefix - which has already been reserved..
        /// </summary>
        internal static string LogContext_Prefix_Reservation_Failed_Prefix_Match {
            get {
                return ResourceManager.GetString("LogContext_Prefix_Reservation_Failed_Prefix_Match", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The log context key prefix &apos;{0}&apos; is longer than {1} characters (too long)..
        /// </summary>
        internal static string LogContext_Prefix_Too_Long {
            get {
                return ResourceManager.GetString("LogContext_Prefix_Too_Long", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The log context key prefix &apos;{0}&apos; is less than {1} characters (too short)..
        /// </summary>
        internal static string LogContext_Prefix_Too_Short {
            get {
                return ResourceManager.GetString("LogContext_Prefix_Too_Short", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The &apos;{0}&apos; key is reserved and cannot be added to a log context explicitly..
        /// </summary>
        internal static string LogContext_Reserved_Key {
            get {
                return ResourceManager.GetString("LogContext_Reserved_Key", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {!fgcolor:Teal}{time:{Value:HH:mm:ss.ffff}} {!fgcolor:LogLevel}{level:{Value}} {!fgcolor}	{message:{Value}}
        ///.
        /// </summary>
        internal static string LogFormat_Short {
            get {
                return ResourceManager.GetString("LogFormat_Short", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {!fgcolor:LogLevel}{!header}{!layout:aCentre}{level:{Value}}{!fgcolor}{!layout}{message:
        ///{!fgcolor:Teal}{Key}{!fgcolor}	: {!fgcolor:White}{Value}{!fgcolor}}{time}{guid}{thread:
        ///{!fgcolor:Teal}{Key}{!fgcolor}	: {Value}{threadid: ({Value})}}{exception}{innerexception}{sproc}{context}{stack:
        ///{!fgcolor:Gray}{!header:-}{!layout:aCentre}{!fgcolor:Teal}{Key}{!fgcolor}{!layout:i6;f3;aLeft}
        ///{!fgcolor:Gray}{Value}{!fgcolor}{!layout}}
        ///{!fgcolor:LogLevel}{!header}{!fgcolor}.
        /// </summary>
        internal static string LogFormat_Verbose {
            get {
                return ResourceManager.GetString("LogFormat_Verbose", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;Log&gt;
        ///{!layout:f4}{message:{xml}}{level:{xml}}{time:{xml}}{guid:{xml}}{thread:{xml}}{threadid:{xml}}{exception:{xml}}{innerexception:{xml}}{sproc:{xml}}{context:{xml}}{stack:{xml}}{!layout}&lt;/Log&gt;.
        /// </summary>
        internal static string LogFormat_XML {
            get {
                return ResourceManager.GetString("LogFormat_XML", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} logger does not support log retrieval..
        /// </summary>
        internal static string LoggerBase_DoesNotSupportRetrieval {
            get {
                return ResourceManager.GetString("LoggerBase_DoesNotSupportRetrieval", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} logger has not yet implemented the Get(endDate, limit) method..
        /// </summary>
        internal static string LoggerBase_GetEndDateLimit_NotImplemented {
            get {
                return ResourceManager.GetString("LoggerBase_GetEndDateLimit_NotImplemented", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} logger has not yet implemented the Get(endDate, startDate) method..
        /// </summary>
        internal static string LoggerBase_GetEndDateStartDate_NotImplemented {
            get {
                return ResourceManager.GetString("LoggerBase_GetEndDateStartDate_NotImplemented", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} logger has not yet implemented the Get(startDate, limit) method..
        /// </summary>
        internal static string LoggerBase_GetForwardStartDateLimit_NotImplemented {
            get {
                return ResourceManager.GetString("LoggerBase_GetForwardStartDateLimit_NotImplemented", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parameter &apos;innerException&apos; can not be null.
        /// </summary>
        internal static string LoggingException_InnerExceptionCannotBeNull {
            get {
                return ResourceManager.GetString("LoggingException_InnerExceptionCannotBeNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parameter &apos;message&apos; can not be null.
        /// </summary>
        internal static string LoggingException_MessageCannotBeNull {
            get {
                return ResourceManager.GetString("LoggingException_MessageCannotBeNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parameter &apos;parameters&apos; can not be null.
        /// </summary>
        internal static string LoggingException_ParametersCannotBeNull {
            get {
                return ResourceManager.GetString("LoggingException_ParametersCannotBeNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Application GUID.
        /// </summary>
        internal static string LogKeys_ApplicationGuid {
            get {
                return ResourceManager.GetString("LogKeys_ApplicationGuid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Application.
        /// </summary>
        internal static string LogKeys_ApplicationName {
            get {
                return ResourceManager.GetString("LogKeys_ApplicationName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Context.
        /// </summary>
        internal static string LogKeys_Context {
            get {
                return ResourceManager.GetString("LogKeys_Context", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Culture.
        /// </summary>
        internal static string LogKeys_Culture {
            get {
                return ResourceManager.GetString("LogKeys_Culture", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Exception type.
        /// </summary>
        internal static string LogKeys_Exception {
            get {
                return ResourceManager.GetString("LogKeys_Exception", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to GUID.
        /// </summary>
        internal static string LogKeys_Guid {
            get {
                return ResourceManager.GetString("LogKeys_Guid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Inner Exception.
        /// </summary>
        internal static string LogKeys_InnerException {
            get {
                return ResourceManager.GetString("LogKeys_InnerException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Inner Exceptions.
        /// </summary>
        internal static string LogKeys_InnerExceptions {
            get {
                return ResourceManager.GetString("LogKeys_InnerExceptions", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Level.
        /// </summary>
        internal static string LogKeys_Level {
            get {
                return ResourceManager.GetString("LogKeys_Level", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Message.
        /// </summary>
        internal static string LogKeys_Message {
            get {
                return ResourceManager.GetString("LogKeys_Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Resource.
        /// </summary>
        internal static string LogKeys_Resource {
            get {
                return ResourceManager.GetString("LogKeys_Resource", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Stack trace.
        /// </summary>
        internal static string LogKeys_StackTrace {
            get {
                return ResourceManager.GetString("LogKeys_StackTrace", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Stored Procedure.
        /// </summary>
        internal static string LogKeys_StoredProcedure {
            get {
                return ResourceManager.GetString("LogKeys_StoredProcedure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Thread ID.
        /// </summary>
        internal static string LogKeys_ThreadID {
            get {
                return ResourceManager.GetString("LogKeys_ThreadID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Thread Name.
        /// </summary>
        internal static string LogKeys_ThreadName {
            get {
                return ResourceManager.GetString("LogKeys_ThreadName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Timestamp.
        /// </summary>
        internal static string LogKeys_TimeStamp {
            get {
                return ResourceManager.GetString("LogKeys_TimeStamp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Can not copy logs over from {0} logger as log retrieval is not supported!.
        /// </summary>
        internal static string LogStatic_AddOrUpdateLogger_RetrievalNotSupported {
            get {
                return ResourceManager.GetString("LogStatic_AddOrUpdateLogger_RetrievalNotSupported", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot retrieve logs, limit is not greater than zero!.
        /// </summary>
        internal static string LogStatic_Get_LimitLessThanZero {
            get {
                return ResourceManager.GetString("LogStatic_Get_LimitLessThanZero", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No log cache available..
        /// </summary>
        internal static string LogStatic_Get_NoLogCacheAvailable {
            get {
                return ResourceManager.GetString("LogStatic_Get_NoLogCacheAvailable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} logger does not support log querying..
        /// </summary>
        internal static string LogStatic_Get_RetrievalNotSupported {
            get {
                return ResourceManager.GetString("LogStatic_Get_RetrievalNotSupported", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot retrieve logs, start date is greater or equal than current date!.
        /// </summary>
        internal static string LogStatic_Get_StartGreaterThanOrEqualToCurrent {
            get {
                return ResourceManager.GetString("LogStatic_Get_StartGreaterThanOrEqualToCurrent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot retrieve logs, start date is greater or equal to end date!.
        /// </summary>
        internal static string LogStatic_Get_StartGreaterThanOrEqualToEnd {
            get {
                return ResourceManager.GetString("LogStatic_Get_StartGreaterThanOrEqualToEnd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error occurred whilst creating logger &apos;{0}&apos; as specified in configuration file..
        /// </summary>
        internal static string LogStatic_LoadConfiguration_ErrorCreatingLogger {
            get {
                return ResourceManager.GetString("LogStatic_LoadConfiguration_ErrorCreatingLogger", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Fatal error occurred whilst running the &apos;{0}&apos; logger, disabling..
        /// </summary>
        internal static string LogStatic_LogBatch_FatalErrorOccured {
            get {
                return ResourceManager.GetString("LogStatic_LogBatch_FatalErrorOccured", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Fatal error whilst logging - aborting..
        /// </summary>
        internal static string LogStatic_LoggerThread_FatalErrorWhilstLogging {
            get {
                return ResourceManager.GetString("LogStatic_LoggerThread_FatalErrorWhilstLogging", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not create memory logger as cache expiry &apos;{0}&apos; was less than 10s..
        /// </summary>
        internal static string MemoryLogger_CacheExpiryLessThanTenSeconds {
            get {
                return ResourceManager.GetString("MemoryLogger_CacheExpiryLessThanTenSeconds", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not create memory logger as maximum log entries &apos;{0}&apos; was less than 1..
        /// </summary>
        internal static string MemoryLogger_MaximumLogsLessThanOne {
            get {
                return ResourceManager.GetString("MemoryLogger_MaximumLogsLessThanOne", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not register service &apos;{0}&apos;.
        /// </summary>
        internal static string SqlLogger_CouldNotRegisterService {
            get {
                return ResourceManager.GetString("SqlLogger_CouldNotRegisterService", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot expire log entries in less than one second..
        /// </summary>
        internal static string Sqllogger_LogExpiryLessThanOneSecond {
            get {
                return ResourceManager.GetString("Sqllogger_LogExpiryLessThanOneSecond", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not create SQL Logger as no connection string was supplied..
        /// </summary>
        internal static string SqlLogger_NoConnectionStringProvided {
            get {
                return ResourceManager.GetString("SqlLogger_NoConnectionStringProvided", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The specified SQL timeout ({0}) was less than 1s..
        /// </summary>
        internal static string Sqllogger_TimeoutLessThanOneSecond {
            get {
                return ResourceManager.GetString("Sqllogger_TimeoutLessThanOneSecond", resourceCulture);
            }
        }
    }
}
