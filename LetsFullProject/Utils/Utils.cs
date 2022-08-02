using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsFullProject.Utils
{
    public class Utils
    {
        public static string DecryptValue(string valueDecrypt)
        {

            Byte[] b = Convert.FromBase64String(valueDecrypt);

            string decryptedConnection = System.Text.ASCIIEncoding.ASCII.GetString(b);
            return decryptedConnection;

        }

        public static string EncryptValue(string valueEncrypt)
        {
            Byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(valueEncrypt);
            string encryptedConnection = Convert.ToBase64String(b);
            return encryptedConnection;

        }
    }
}
