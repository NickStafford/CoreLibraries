﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplications.Utilities.Service {
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
    public class ContractResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ContractResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WebApplications.Utilities.Service.ContractResources", typeof(ContractResources).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The service runner command was based an instance of the wrong type..
        /// </summary>
        public static string Bad_Instance {
            get {
                return ResourceManager.GetString("Bad_Instance", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Contract failed. {0}.
        /// </summary>
        public static string Contract_Failed {
            get {
                return ResourceManager.GetString("Contract_Failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The service runner does not accept generic methods..
        /// </summary>
        public static string Method_Generic {
            get {
                return ResourceManager.GetString("Method_Generic", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The maximum number of connections must be greater than zero..
        /// </summary>
        public static string NamedPipeServer_MaxConnections {
            get {
                return ResourceManager.GetString("NamedPipeServer_MaxConnections", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This class can only be created when running in a console..
        /// </summary>
        public static string Not_In_Console {
            get {
                return ResourceManager.GetString("Not_In_Console", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A required GUID paramter cannot be empty..
        /// </summary>
        public static string Parameter_Guid_Empty {
            get {
                return ResourceManager.GetString("Parameter_Guid_Empty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A required parameter was set to null..
        /// </summary>
        public static string Parameter_Null {
            get {
                return ResourceManager.GetString("Parameter_Null", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The service description must be between 1 and 80 characters long..
        /// </summary>
        public static string Service_DescriptionLength {
            get {
                return ResourceManager.GetString("Service_DescriptionLength", resourceCulture);
            }
        }
    }
}
