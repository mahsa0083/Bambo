using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Bambo.Serives
{
    public class AesEncryption
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("1234567890123456"); // کلید 16 بایتی
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("1234567890123456"); // مقدار اولیه 16 بایتی

        public static string Encrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }

        class Program
        {
            static void Main()
            {
                string original = "Hello, World!";
                string encrypted = AesEncryption.Encrypt(original);
                string decrypted = AesEncryption.Decrypt(encrypted);

                Console.WriteLine($"Original: {original}");
                Console.WriteLine($"Encrypted: {encrypted}");
                Console.WriteLine($"Decrypted: {decrypted}");
            }
        }
    }
}