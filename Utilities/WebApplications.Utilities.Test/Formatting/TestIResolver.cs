﻿#region © Copyright Web Applications (UK) Ltd, 2014.  All rights reserved.
// Copyright (c) 2014, Web Applications UK Ltd
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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplications.Utilities.Formatting;

namespace WebApplications.Utilities.Test.Formatting
{
    [TestClass]
    public class TestIResolver
    {
        [TestMethod]
        public void TestNestedResolver()
        {
            DictionaryResolvable resolvable = new DictionaryResolvable
            {
                {
                    "A", new DictionaryResolvable
                    {
                        {"A", "aa"},
                        {"B", "ab"}
                    }
                },
                {
                    "B", new DictionaryResolvable
                    {
                        {"A", "ba"},
                        {"B", "bb"}
                    }
                }
            };

            FormatBuilder builder = new FormatBuilder().AppendFormat("{0:{A:{B:{A}}} {B:{A:{B}}}}", resolvable);
            Assert.AreEqual("aa bb", builder.ToString());
            /* WHY????
             * Step 1: {0:...} is resolved to 'resolvable'
             * Step 2: There's a format, so the resolvable is called to resolve it.
             * Step 3: {A:...} and {B:...} are resolved to two new Resolvables (the two inside the dictionary above).
             *    This bits, important, these are the last two resolvables on the stack...
             * Step 4: {B:{A}} is resolved to "ab" (note not a resolvable), and {A:{B}} is resolved to "ba".
             * Step 5: {A} is resolved by the resolvable on the stack at that point (the one belonging to 'A') as the B
             *    is not a resolvable itself.  Therefore the A is "aa".  Similarly for the {B}.
             *    
             * {Context:\r\n{Key}\t: {Value}} =>
             * {Context.Resource:\r\n{Key}\t: {Value}
             */
        }

        [TestMethod]
        public void TestResolvableEnumeration()
        {
            DictionaryResolvable resolvable = new DictionaryResolvable
            {
                {
                    "A", new List<IResolvable>
                    {
                        new DictionaryResolvable
                        {
                            {"A", "A"},
                            {"B", "B"},
                            {"C", "C"},
                        },
                        new DictionaryResolvable
                        {
                            {"A", "D"},
                            {"B", "E"},
                            {"C", "F"},
                        },
                        new DictionaryResolvable
                        {
                            {"A", "G"},
                            {"B", "H"},
                            {"C", "J"},
                            {"<INDEX>", "I"}
                        }
                    }
                }
            };

            FormatBuilder builder =
                new FormatBuilder().AppendFormat("{0:{A:[{<ITEMS>:{<INDEX>} {A} {B} {C}}{<JOIN>:, }]}}", resolvable);
            Assert.AreEqual("[0 A B C, 1 D E F, I G H J]", builder.ToString());
        }

        [TestMethod]
        public void TestEnumerations()
        {
            FormatBuilder builder = new FormatBuilder().AppendFormat(
                "{0:[{<Items>:0.00}{<JOIN>:, }]}",
                new[] {1, 2, 3, 4});
            Assert.AreEqual("[1.00, 2.00, 3.00, 4.00]", builder.ToString());
        }

        [TestMethod]
        public void TestEnumerationsWithItemIndex()
        {
            FormatBuilder builder = new FormatBuilder().AppendFormat(
                "{0:[{<items>:{<Index>}-{<Item>:0.00}}{<JOIN>:, }]}",
                new[] {1, 2, 3, 4});
            Assert.AreEqual("[0-1.00, 1-2.00, 2-3.00, 3-4.00]", builder.ToString());
        }

        /* TODO This test is no longer valid as resolution does not occur for ToString("F")
        [TestMethod]
        public void TestReplaceColor()
        {
            FormatBuilder builder = new FormatBuilder().AppendForegroundColor(Color.Red).AppendForegroundColor(Color.Green);
            Assert.AreEqual(
                "{" + FormatBuilder.ForegroundColorTag + ":Red}{" + FormatBuilder.ForegroundColorTag + ":Green}",
                builder.ToString("F"));

            builder.Resolve(
                (_, c) =>
                    string.Equals(
                        c.Tag,
                        FormatBuilder.ForegroundColorTag,
                        StringComparison.CurrentCultureIgnoreCase) &&
                    string.Equals(
                        c.Format,
                        "Green",
                        StringComparison.CurrentCultureIgnoreCase)
                        ? new FormatChunk(null, FormatBuilder.ForegroundColorTag, 0, "Blue", Color.Blue)
                        : Resolution.UnknownYet);
            Assert.AreEqual(
                "{" + FormatBuilder.ForegroundColorTag + ":Red}{" + FormatBuilder.ForegroundColorTag + ":Blue}",
                builder.ToString("F"));
        }
         */

        [TestMethod]
        public void TestReplaceControl()
        {
            FormatBuilder builder = new FormatBuilder("{!control:text}");
            Assert.AreEqual("{!control:text}", builder.ToString("f"));
            Assert.AreEqual(
                "text",
                builder.ToString(
                    (_, c) => c.IsControl && string.Equals(c.Tag, "!control", StringComparison.CurrentCultureIgnoreCase)
                        ? new FormatChunk(null, null, 0, null, c.Format)
                        : Resolution.Unknown));
        }
    }
}