using System;
using System.Security.Cryptography;
using System.Text;

namespace Server.Utils
{
    public class HashingUtility
    {
        public static string HashString(string input) 
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                //Convert the input string in bytes
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);

                //Copute the ahsh
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                //Convert the hash byte in hexadecimal string
                StringBuilder hashStringBuilder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashStringBuilder.Append(b.ToString("x2"));
                }

                return hashStringBuilder.ToString();
            }
        }
    }
}
