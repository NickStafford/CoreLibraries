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

using System.Configuration;
using JetBrains.Annotations;
using WebApplications.Utilities.Configuration;

namespace WebApplications.Utilities.Database.Configuration
{
    /// <summary>
    ///   A collection of <see cref="LoadBalancedConnectionElement">load balanced</see> connections.
    /// </summary>
    [UsedImplicitly]
    [ConfigurationCollection(typeof (ParameterElement),
        CollectionType = ConfigurationElementCollectionType.BasicMapAlternate)]
    public class LoadBalancedConnectionCollection :
        ConfigurationElementCollection<string, LoadBalancedConnectionElement>
    {
        /// <summary>
        ///   Gets the name used to identify the element.
        /// </summary>
        /// <value>
        ///   The element name.
        /// </value>
        protected override string ElementName
        {
            get { return "connection"; }
        }

        /// <summary>
        ///   Gets the type of the <see cref="LoadBalancedConnectionCollection"/>.
        /// </summary>
        /// <value>
        ///   The <see cref="ConfigurationElementCollectionType"/> of this collection.
        /// </value>
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMapAlternate; }
        }

        /// <summary>
        ///   Gets the element key.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The load balanced connection <see cref="LoadBalancedConnectionElement.Id">ID</see>.</returns>
        /// <exception cref="ConfigurationErrorsException">
        ///   The property is read-only or locked.
        /// </exception>
        protected override string GetElementKey(LoadBalancedConnectionElement element)
        {
            return element.Id;
        }
    }
}