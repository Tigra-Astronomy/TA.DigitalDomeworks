// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: AsciiExtensions.cs  Last modified: 2018-03-30@01:48 by Tim Long

using System;
using System.Diagnostics.Contracts;
using System.Text;

namespace TA.DigitalDomeworks.SharedTypes
    {
    public static class AsciiExtensions
        {
        /// <summary>
        ///     Utility function. Expands non-printable ASCII characters into mnemonic human-readable form.
        /// </summary>
        /// <returns>
        ///     Returns a new string with non-printing characters replaced by human-readable mnemonics.
        /// </returns>
        public static string ExpandAscii(this string text)
            {
            Contract.Requires(text != null);
            Contract.Ensures(Contract.Result<string>() != null);
            var expanded = new StringBuilder();
            foreach (var c in text)
                {
                var b = (byte) c;
                var strAscii = Enum.GetName(typeof(AsciiSymbols), b);
                if (strAscii != null)
                    expanded.Append("<" + strAscii + ">");
                else
                    expanded.Append(c);
                }

            return expanded.ToString();
            }

        public static string ExpandAscii(this char c)
            {
            Contract.Ensures(Contract.Result<string>() != null);
            return c.ToString().ExpandAscii();
            }
        }
    }