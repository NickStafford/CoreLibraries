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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WebApplications.Testing;
using WebApplications.Utilities.Annotations;
using WebApplications.Utilities.Cryptography.Configuration;

namespace WebApplications.Utilities.Cryptography.Test
{
    /// <summary>
    /// Serialization Test Base.
    /// </summary>
    public abstract class CryptographyTestBase : TestBase
    {
        [NotNull]
        protected static readonly Random Random = new Random();

        [NotNull]
        protected static CryptographyConfiguration Configuration { get; private set; }

        [NotNull]
        protected static ICryptoProvider RSA { get; private set; }

        [NotNull]
        protected static ICryptoProvider AES { get; private set; }

        [NotNull]
        protected static ICryptoProvider AES2 { get; private set; }

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            Configuration = CryptographyConfiguration.Active;
            Assert.IsNotNull(Configuration);
            RSA = Configuration.Provider("RSA");
            Assert.IsNotNull(RSA);
            AES = Configuration.Provider("AES");
            Assert.IsNotNull(AES);
            AES2 = Configuration.Provider("AES2");
            Assert.IsNotNull(AES2);
        }
    }
}