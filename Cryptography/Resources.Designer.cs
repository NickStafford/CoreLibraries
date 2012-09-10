﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17626
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplications.Utilities.Cryptography {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WebApplications.Utilities.Cryptography.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to No enabled AESCryptographer providers were found in configuration..
        /// </summary>
        internal static string AESCryptographer_Constructor_Configuration_NoProviderFound {
            get {
                return ResourceManager.GetString("AESCryptographer_Constructor_Configuration_NoProviderFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The provided input was not a Base32 encoded string: &apos;{0}&apos;.
        /// </summary>
        internal static string AESCryptographer_Decrypt_DecryptFailed_InputNotBase32String {
            get {
                return ResourceManager.GetString("AESCryptographer_Decrypt_DecryptFailed_InputNotBase32String", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The input failed to decrypt with the current collection of keys..
        /// </summary>
        internal static string AESCryptographer_Decrypt_DecryptFailed_KeyNotFound {
            get {
                return ResourceManager.GetString("AESCryptographer_Decrypt_DecryptFailed_KeyNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot decrypt a node of type &apos;{0}&apos;..
        /// </summary>
        internal static string Cryptographer_Decrypt_CannotDecryptNode {
            get {
                return ResourceManager.GetString("Cryptographer_Decrypt_CannotDecryptNode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot encrypt a node of type &apos;{0}&apos;..
        /// </summary>
        internal static string Cryptographer_Encrypt_CannotEncryptNode {
            get {
                return ResourceManager.GetString("Cryptographer_Encrypt_CannotEncryptNode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No embedded key files were found..
        /// </summary>
        internal static string Cryptographer_InitializeEmbeddedKeys_NoFilesFound {
            get {
                return ResourceManager.GetString("Cryptographer_InitializeEmbeddedKeys_NoFilesFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The provider with id: &apos;{0}&apos; wasn&apos;t enabled in the configuration..
        /// </summary>
        internal static string CryptoProviderWrapper_Constructor_ProviderNotEnabled {
            get {
                return ResourceManager.GetString("CryptoProviderWrapper_Constructor_ProviderNotEnabled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No enabled RSACryptographer providers were found in configuration..
        /// </summary>
        internal static string RSACryptographer_Constructor_Configuration_NoProviderFound {
            get {
                return ResourceManager.GetString("RSACryptographer_Constructor_Configuration_NoProviderFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Decryption failed. No key attempted was successful..
        /// </summary>
        internal static string RSACryptographer_Decrypt_DecryptionFailed {
            get {
                return ResourceManager.GetString("RSACryptographer_Decrypt_DecryptionFailed", resourceCulture);
            }
        }
    }
}
