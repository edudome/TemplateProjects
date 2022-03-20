using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Template.Application.Utils
{
    public static class CryptGenerator
    {
        private static byte[] key = new byte[8] { 1, 4, 0, 4, 2, 7, 0, 0 };
        private static byte[] iv = new byte[8] { 1, 3, 2, 7, 6, 4, 9, 0 };

        public static string EncriptarModel<T>(T modelo)
        {
            string param = JsonConvert.SerializeObject(modelo);
            param = Crypt(param);
            return param;
        }

        public static T? DesencriptarModel<T>(string param)
        {
            T? modelo = default(T);

            if (!string.IsNullOrEmpty(param))
            {
                string paramDescrypt = Decrypt(param);
                modelo = JsonConvert.DeserializeObject<T>(paramDescrypt);
            }

            return modelo;
        }

        public static string ComputeSha256Hash(string text)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.Unicode.GetBytes(text));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString().ToUpper();
            }
        }

        private static string Crypt(string text)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateEncryptor(key, iv);
            byte[] inputbuffer = Encoding.Unicode.GetBytes(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Convert.ToBase64String(outputBuffer);
        }

        private static string Decrypt(string text)
        {
            if (String.IsNullOrEmpty(text)) return String.Empty;
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateDecryptor(key, iv);
            byte[] inputbuffer = Convert.FromBase64String(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Encoding.Unicode.GetString(outputBuffer);
        }
    }
}
