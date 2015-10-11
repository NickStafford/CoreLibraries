﻿#region © Copyright Web Applications (UK) Ltd, 2012.  All rights reserved.
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
using System.Diagnostics;
using System.Security.Cryptography;
using WebApplications.Testing;

namespace WebApplications.Utilities.Cryptography.Test
{
    [TestClass]
    public class RSACryptographerTests : CryptographyTestBase
    {
        private const string InputString = "I do not like them sam-I-am I do not like green eggs and ham.";

        [TestMethod]
        public void Encrypt_InputString_SuccessfulEncryption()
        {
            string encrypted = RSA.Encrypt(InputString);

            Trace.WriteLine(encrypted);
            Assert.IsTrue(encrypted != null, "Encrypt method did not return a valid non-null string");
        }

        [TestMethod]
        public void Encrypt_WhiteSpace_SuccessfulEncryption()
        {
            string encrypted = RSA.Encrypt(" ");

            Trace.WriteLine(encrypted);
            Assert.IsTrue(encrypted != null, "Encrypt method did not return a valid non-null string");
        }

        [TestMethod]
        public void Encrypt_LongString_SuccessfulEncryption()
        {
            string input = Random.RandomString(5000, false);
            string encrypted = RSA.Encrypt(input);

            Trace.WriteLine(encrypted);
            Assert.IsTrue(encrypted != null, "Encrypt method did not return a valid non-null string");
        }

        [TestMethod]
        public void Encrypt_UnicodeString_SuccessfulEncryption()
        {
            string input = Random.RandomString(10);
            string encrypted = RSA.Encrypt(input);

            Trace.WriteLine(encrypted);
            Assert.IsTrue(encrypted != null, "Encrypt method did not return a valid non-null string");
        }

        [TestMethod]
        public void Encrypt_OneCharacterString_SuccessfulEncryption()
        {
            string encrypted = RSA.Encrypt("!");

            Trace.WriteLine(encrypted);
            Assert.IsTrue(encrypted != null, "Encrypt method did not return a valid non-null string");
        }

        [TestMethod]
        public void Encrypt_NullInput_EmptyStringResult()
        {
            Assert.IsNull(RSA.Encrypt(null));
        }

        [TestMethod]
        public void Encrypt_EmptyString()
        {
            Assert.AreEqual(RSA.Encrypt(string.Empty), string.Empty);
        }

        [TestMethod]
        public void Encrypt_SameTwoInputStrings_DifferentEcryptionResult()
        {
            string encryptedResult1 = RSA.Encrypt(InputString);
            Trace.WriteLine(encryptedResult1);

            string encryptedResult2 = RSA.Encrypt(InputString);
            Trace.WriteLine(encryptedResult2);

            Assert.IsFalse(encryptedResult1 == encryptedResult2,
                           "The same string should result in a different encryption result");
        }

        [TestMethod]
        public void Encrypt_SameTwoUnicodeInputStrings_DifferentEcryptionResult()
        {
            string input = Random.RandomString(10);

            string encryptedResult1 = RSA.Encrypt(input);
            Trace.WriteLine(encryptedResult1);

            string encryptedResult2 = RSA.Encrypt(input);
            Trace.WriteLine(encryptedResult2);

            Assert.IsFalse(encryptedResult1 == encryptedResult2,
                           "The same string should result in a different encryption result");
        }

        [TestMethod]
        public void Decrypt_SameTwoInputStrings_SameDecryptionResult()
        {
            string input = Random.RandomString(10, false);

            string encryptedResult1 = RSA.Encrypt(input);
            Trace.WriteLine("Encrypted A: " + encryptedResult1);

            string encryptedResult2 = RSA.Encrypt(input);
            Trace.WriteLine("Encrypted B: " + encryptedResult2 + Environment.NewLine);

            bool isLatestKey;

            string decryptedResult1 = RSA.Decrypt(encryptedResult1, out isLatestKey);
            Trace.WriteLine("Decrypted A: " + decryptedResult1);

            string decryptedResult2 = RSA.Decrypt(encryptedResult2, out isLatestKey);
            Trace.WriteLine("Decrypted B: " + decryptedResult2);

            Assert.AreEqual(decryptedResult1, decryptedResult2,
                            "The same input strings should result in the same decryption result");
        }

        [TestMethod]
        public void Decrypt_SameTwoUnicodeInputStrings_SameDecryptionResult()
        {
            string input = Random.RandomString(10);

            string encryptedResult1 = RSA.Encrypt(input);
            Trace.WriteLine("Encrypted A: " + encryptedResult1);

            string encryptedResult2 = RSA.Encrypt(input);
            Trace.WriteLine("Encrypted B: " + encryptedResult2 + Environment.NewLine);

            bool isLatestKey;

            string decryptedResult1 = RSA.Decrypt(encryptedResult1, out isLatestKey);
            Trace.WriteLine("Decrypted A: " + decryptedResult1);

            string decryptedResult2 = RSA.Decrypt(encryptedResult2, out isLatestKey);
            Trace.WriteLine("Decrypted B: " + decryptedResult2);

            Assert.AreEqual(decryptedResult1, decryptedResult2,
                            "The same input strings should result in the same decryption result");
        }

        [TestMethod]
        public void Decrypt_DecryptedResult_MatchesInputString()
        {
            string encrypted = RSA.Encrypt(InputString);

            bool isLatestKey;
            string decrypted = RSA.Decrypt(encrypted, out isLatestKey);

            Trace.WriteLine(decrypted);
            Assert.AreEqual(InputString, decrypted, "decrypted text did not match the provided input");
        }

        [TestMethod]
        public void Decrypt_UnicodeString_SuccessfulDecryption()
        {
            string input = Random.RandomString(10);
            string encrypted = RSA.Encrypt(input);

            bool isLatestKey;
            string decrypted = RSA.Decrypt(encrypted, out isLatestKey);

            Trace.WriteLine(input);
            Trace.WriteLine(decrypted);
            Assert.AreEqual(input, decrypted, "decrypted text did not match the provided input");
        }

        [TestMethod]
        public void Decrypt_NullInput_ArgumentNullException()
        {
            bool isLatestKey;
            Assert.IsNull(RSA.Decrypt(null, out isLatestKey));
        }

        [TestMethod]
        public void Decrypt_EmptyString()
        {
            bool isLatestKey;
            Assert.AreEqual(RSA.Decrypt(string.Empty, out isLatestKey), string.Empty);
        }
        
        [TestMethod]
        [ExpectedException(typeof (CryptographicException))]
        public void Decrypt_InputEncryptedWithKeyNotInConfig_CryptographicException()
        {
            const string encryptedStringNotUsingKeyInConfiguration =
                "I0Qu6rZYA2OB1FXhhGB8J6+VyeEVVAvZkaYBKi8j4XY23ecgN1Zpprhzcrql7U6eUKZWtPaxfDwoEa9g6Bq0f1uE1pVMhrlxQDw3n6KFI5chvFLFpMA85APth08F2yzEh1yjXj6iynV9ZZlGyUQ/+lMJY0fjg45fNnv23C4aFbM=";

            bool isLatestKey;
            string decrypted = RSA.Decrypt(encryptedStringNotUsingKeyInConfiguration, out isLatestKey);

            Trace.WriteLine("Result: " + decrypted);

            Assert.Fail(
                "CryptographicException was expected when using an encrypted string that does not exist within our configuration");
        }

        // TODO This test is unstable, this needs looking into
        [TestMethod]
        [Ignore]
        public void TryDecrypt_ReturnValue_ReturnsFalseWhenKeyIsNotLatestKey()
        {
            const string encryptedStringUsingExpiredKeyInConfiguration =
                "CkTj6c4LPWXjfAxHKpuITqXFaerCiZ9rfAzyf8FS/5qYWbQ1HMGsADO6rF7fuAljjvfCM5HoYvZe7zBAjxU2kVfuVmaHKGGYJyrtjwKvRURXwkXgUUO8HanpJtU4UjvO0AU3sBgJCc5NUXS/tU9oT4D/SbaHcvQUtFfThAiuT0w=";

            string decryptedString;
            bool? isLatestKey;

            RSA.Encrypt("a new key will be made now");
            bool decrypted = RSA.TryDecrypt(encryptedStringUsingExpiredKeyInConfiguration,
                                                         out decryptedString, out isLatestKey);

            Assert.IsTrue(decrypted, "'decrypted' should return true");
            Assert.IsFalse(isLatestKey.Value, "IsLatestKey should return false");
        }
    }
}