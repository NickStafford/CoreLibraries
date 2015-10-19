#region � Copyright Web Applications (UK) Ltd, 2015.  All rights reserved.
// Copyright (c) 2015, Web Applications UK Ltd
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

using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using WebApplications.Utilities.Annotations;
using WebApplications.Utilities.Configuration;

namespace WebApplications.Utilities.Cryptography.Configuration
{
    /// <summary>
    /// A crypto provider element.
    /// </summary>
    [PublicAPI]
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    public class ProviderElement : XmlConfigurationElement
    {
        /// <summary>
        /// Gets or sets the provider ID.
        /// </summary>
        /// <value>
        /// The provider ID.
        /// </value>
        [ConfigurationProperty("id", IsRequired = true, IsKey = true)]
        [NotNull]
        public string Id
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            get { return GetProperty<string>("id"); }
            set { SetProperty("id", value); }
        }

        /// <summary>
        /// Gets or sets the type of the provider.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [ConfigurationProperty("name", IsRequired = true, DefaultValue = "Rijndael")]
        [NotNull]
        public string Name
        {
            get { return GetProperty<string>("name"); }
            set { SetProperty("name", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this provider is enabled.
        /// </summary>
        /// <value>
        /// <see langword="true" /> if this provider is enabled; otherwise, <see langword="false" />.
        /// </value>
        [ConfigurationProperty("enabled", DefaultValue = true, IsRequired = false)]
        public bool IsEnabled
        {
            get { return GetProperty<bool>("enabled"); }
            set { SetProperty("enabled", value); }
        }

        /// <summary>
        ///   Gets or sets the parameters to be passed to the constructor.
        /// </summary>
        /// <value>
        ///   The <see cref="WebApplications.Utilities.Configuration.ParameterCollection"/>,
        ///   which is all of the child elements within the parameters element in the configuration file.
        /// </value>
        [ConfigurationProperty("parameters", IsRequired = false, IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(ParameterCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        [NotNull]
        // ReSharper disable once VirtualMemberNeverOverriden.Global
        public ParameterCollection Parameters
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            get { return GetProperty<ParameterCollection>("parameters"); }
            set { SetProperty("parameters", value); }
        }

        /// <summary>
        /// Gets the <see cref="CryptographyProvider"/>.
        /// </summary>
        /// <returns>A <see cref="CryptographyProvider"/> if <see cref="IsEnabled">enabled</see>; otherwise <see langword="null"/>.</returns>
        public CryptographyProvider GetProvider() => CryptographyProvider.Create(this);

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        public XElement Configuration
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            get { return GetElement("configuration"); }
            set { SetElement("configuration", value); }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Configuration.ConfigurationElement" /> object is read-only.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Configuration.ConfigurationElement" /> object is read-only; otherwise, false.
        /// </returns>
        public override bool IsReadOnly()
        {
            return false;
        }

        /// <summary>
        /// Gets the parameters as an object array.
        /// </summary>
        /// <returns>An <see cref="T:object[]"/>.</returns>
        [NotNull]
        internal object[] GetParameters() => Parameters.Count < 1 ? Array<object>.Empty : Parameters.Select(p => (object)p).ToArray();
    }
}