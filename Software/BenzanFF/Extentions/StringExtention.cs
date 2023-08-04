using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace BenzanFF.Extentions
{
    public static class StringExtention
    {
        public static string Encriptar(this string texto)
        {
            using SHA256 sha256Hash = SHA256.Create();
            byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));

            // Utilizamos un formato más conciso para convertir el array de bytes en una cadena hexadecimal
            return BitConverter.ToString(data).Replace("-", "").ToLower();
        }
    }
}
