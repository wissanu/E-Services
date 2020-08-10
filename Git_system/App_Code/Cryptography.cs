using System;
using System.Collections.Generic;
using System.Linq;

namespace Git_system.App_Code
{
    public class Cryptography
    {
        public static string SHA1(string value)
        {
            var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(value ?? "");
            return BitConverter.ToString(hash.ComputeHash(combined)).Replace("-", "");
        }

        public static string SHA512(string value)
        {
            var hash = System.Security.Cryptography.SHA512.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(value ?? "");
            return BitConverter.ToString(hash.ComputeHash(combined)).Replace("-", "");
        }

        public static string SHA512Salt(string value)
        {
            string salt = "git-eservice";
            string passAndSalt = String.Concat(value, salt);
            System.String Hashedcx = BitConverter.ToString(((System.Security.Cryptography.SHA512)new System.Security.Cryptography.SHA512Managed()).ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(passAndSalt))).Replace("-", "");
            return Hashedcx;
        }

        public static string SHA512SaltAddSalt(string value, string inputSalt)
        {
            string salt = string.Concat("git-eservice", inputSalt.ToUpper());
            string passAndSalt = String.Concat(value, salt);
            System.String Hashedcx = BitConverter.ToString(((System.Security.Cryptography.SHA512)new System.Security.Cryptography.SHA512Managed()).ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(passAndSalt))).Replace("-", "");
            return Hashedcx;
        }
    }

    /// <summary>
    /// A Base36 De- and Encoder
    /// </summary>
    public static class Base36
    {
        private const string CharList = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// Encode the given number into a Base36 string
        /// </summary>
        /// <param name="input">number</param>
        /// <returns>Base36</returns>
        public static String Encode(long input)
        {
            if (input < 0) throw new ArgumentOutOfRangeException("input", input, "input cannot be negative");

            char[] clistarr = CharList.ToCharArray();
            var result = new Stack<char>();
            while (input != 0)
            {
                result.Push(clistarr[input % 36]);
                input /= 36;
            }
            return new string(result.ToArray());
        }

        /// <summary>
        /// Decode the Base36 Encoded string into a number
        /// </summary>
        /// <param name="input">Base36</param>
        /// <returns>number</returns>
        public static Int64 Decode(string input)
        {
            var reversed = input.ToUpper().Reverse();
            long result = 0;
            int pos = 0;
            foreach (char c in reversed)
            {
                result += CharList.IndexOf(c) * (long)Math.Pow(36, pos);
                pos++;
            }
            return result;
        }
    }
}