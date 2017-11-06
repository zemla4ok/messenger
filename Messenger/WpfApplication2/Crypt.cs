using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2
{
    static class Crypt
    {
        public static string CryptPassword(string str)
        {
            string result = "";
            byte[] hash = Encoding.Unicode.GetBytes(str);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashenc = md5.ComputeHash(hash);
            foreach (byte b in hashenc)
                result += b.ToString("x2");
            return result;
        }
    }
}