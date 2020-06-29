using System.Security.Cryptography;
using System.Text;

namespace DebtsWebApi.Helpers
{
    public sealed class Md5BasedEncryptor
    {
        public string Encrypt(string encriptingString)
        {
            byte[] hash = new MD5CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(encriptingString));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte num in hash)
                stringBuilder.Append(string.Format("{0:x02}", (object)num));
            return stringBuilder.ToString();
        }
    }
}
