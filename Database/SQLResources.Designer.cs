﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17626
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplications.Utilities.Database {
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
    internal class SQLResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SQLResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WebApplications.Utilities.Database.SQLResources", typeof(SQLResources).Assembly);
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
        ///   Looks up a localized string similar to SELECT	schema_id [SchemaID],
        ///	name [Name]
        ///FROM sys.schemas
        ///
        ///SELECT	schema_id [SchemaID],
        ///	user_type_id [ID],
        ///	name [Name],
        ///	CASE WHEN system_type_id&lt;&gt;user_type_id THEN CONVERT(int, system_type_id) ELSE NULL END [BaseID],
        ///	max_length [MaxLength],
        ///	precision [Precision],
        ///	scale [Scale],
        ///	is_nullable [IsNullable],
        ///	is_user_defined [IsUserDefined],
        ///	is_assembly_type [IsCLR],
        ///	is_table_type [IsTable]
        ///FROM sys.types T
        ///ORDER BY BaseID
        ///
        ///SELECT	obj.type [ObjectType],
        ///	obj.schema_id [SchemaID],
        ///	o [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string RetrieveSchema10 {
            get {
                return ResourceManager.GetString("RetrieveSchema10", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT	schema_id [SchemaID],
        ///	name [Name]
        ///FROM sys.schemas
        ///
        ///SELECT	schema_id [SchemaID],
        ///	user_type_id [ID],
        ///	name [Name],
        ///	CASE WHEN system_type_id&lt;&gt;user_type_id THEN CONVERT(int, system_type_id) ELSE NULL END [BaseID],
        ///	max_length [MaxLength],
        ///	precision [Precision],
        ///	scale [Scale],
        ///	is_nullable [IsNullable],
        ///	is_user_defined [IsUserDefined],
        ///	is_assembly_type [IsCLR],
        ///	CAST(0 as bit) [IsTable]
        ///FROM sys.types T
        ///ORDER BY BaseID
        ///
        ///SELECT	obj.type [ObjectType],
        ///	obj.schema_id [SchemaID],
        ///	 [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string RetrieveSchema9 {
            get {
                return ResourceManager.GetString("RetrieveSchema9", resourceCulture);
            }
        }
    }
}
