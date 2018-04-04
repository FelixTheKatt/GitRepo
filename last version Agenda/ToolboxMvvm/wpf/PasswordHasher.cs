using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ToolboxMvvm.wpf
{
    public static class PasswordHasher
    {
        public static string Hash(string password, Guid guid)
        {
            SHA512 sha512 = SHA512Managed.Create();

            byte[] passwordByteArray = Encoding.UTF8.GetBytes(password);

            byte[] saltByteArray = guid.ToByteArray();

            //Concatenate passwordByteArray and saltByteArray. See method below "Combine".
            byte[] finalByteArray = Combine(passwordByteArray, saltByteArray);

            byte[] result = sha512.ComputeHash(finalByteArray);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("X2"));
            }

            return sb.ToString();
        }

        private static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }


    }
}
