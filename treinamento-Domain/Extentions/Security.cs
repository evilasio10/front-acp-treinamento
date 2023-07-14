using System.Text;

namespace treinamento_Domain.Extentions
{
    public static class Security
    {
        public static String SHA256(string input)
        {
            return SHA256(UTF8Encoding.UTF8.GetBytes(input));
        }

        private static string SHA256(Byte[] input)
        {
            using (System.Security.Cryptography.SHA256 hash = System.Security.Cryptography.SHA256.Create())
            {
                return BitConverter.ToString(hash.ComputeHash(input)).Replace("-", "").ToLower();
            }
        }
    }
}
