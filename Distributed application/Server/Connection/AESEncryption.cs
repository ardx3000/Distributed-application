using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Server.Connection
{
    public class AESEncryption
    {
        private byte[] key;
        private byte[] iv;

        public AESEncryption(byte[] key, byte[] iv)
        {
            if (key == null || key.Length != 16)
                throw new ArgumentNullException(nameof(key), "Key must be 16 bytes long.");

            if (iv == null || iv.Length != 16)
                throw new ArgumentNullException(nameof(iv), "IV must be 16 bytes long.");

            this.key = key;
            this.iv = iv;
        }

        public byte[] EncryptString(string plainText)
        {
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using(CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        return msEncrypt.ToArray();
                    }
                }
            }
        }

        public string DecryptBytes(byte[] cipherText)
        {
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    } 
                }
            }
        }
    }
}   