using System.IO;
using System.Security.Cryptography;

namespace PassGuardianWS.Utils
{
    public static class Encryption
    {
        public static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;

            using (Aes aes = Aes.Create())

            {
               
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
               
                using (MemoryStream ms = new MemoryStream())
                {
                     
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                           
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
              
            return encrypted;
        }

        public static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;


            using (Aes aes = Aes.Create())

            {
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                    
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                        
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                           
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }

    }
}