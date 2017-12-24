using System;
using System.Security.Cryptography;
using System.Text;

namespace ReqManager.Utils.Hashing
{
    public static class CryptographySHA1
    {
        public static string GeneratePassword(int length)
        {
            try
            {
                const string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
                var randNum = new Random();
                var chars = new char[length];
                var allowedCharCount = allowedChars.Length;
                for (var i = 0; i <= length - 1; i++)
                    chars[i] = allowedChars[Convert.ToInt32((allowedChars.Length) * randNum.NextDouble())];
                return new string(chars);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string EncodePassword(string pass, string salt)
        {
            try
            {
                byte[] bytes = Encoding.Unicode.GetBytes(pass);
                byte[] src = Encoding.Unicode.GetBytes(salt);
                byte[] dst = new byte[src.Length + bytes.Length];
                System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
                System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
                HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
                byte[] inArray = algorithm.ComputeHash(dst);
                return EncodePasswordMd5(Convert.ToBase64String(inArray));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string EncodePasswordMd5(string pass)
        {
            try
            {
                Byte[] originalBytes;
                Byte[] encodedBytes;
                MD5 md5;
                md5 = new MD5CryptoServiceProvider();
                originalBytes = ASCIIEncoding.Default.GetBytes(pass);
                encodedBytes = md5.ComputeHash(originalBytes);
                return BitConverter.ToString(encodedBytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
