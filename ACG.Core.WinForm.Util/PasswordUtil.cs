using System;
using System.Security.Cryptography;
using System.Text;

namespace ACG.Core.WinForm.Util
{
    public class PasswordUtil
    {
        public static string GetMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        public static bool VerifyMd5Hash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(input);

            // Create a StringComparer an comare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private const string Pbkdf2Prefix = "pbkdf2$";
        private const int Pbkdf2Iterations = 10000;
        private const int Pbkdf2SaltSize = 16;
        private const int Pbkdf2HashSize = 32;

        /// <summary>
        /// Băm mật khẩu bằng PBKDF2 có salt ngẫu nhiên, mật khẩu mã hóa UTF-8.
        /// Kết quả: "pbkdf2$&lt;số vòng&gt;$&lt;salt base64&gt;$&lt;hash base64&gt;".
        /// </summary>
        public static string HashPassword(string password)
        {
            byte[] salt = new byte[Pbkdf2SaltSize];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            byte[] hash = pbkdf2(password, salt, Pbkdf2Iterations, Pbkdf2HashSize);

            return string.Format("{0}{1}${2}${3}", Pbkdf2Prefix, Pbkdf2Iterations,
                Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        /// <summary>
        /// true nếu giá trị đang lưu là mật khẩu kiểu cũ (MD5, chưa có tiền tố "pbkdf2$").
        /// </summary>
        public static bool IsLegacyMd5Hash(string storedHash)
        {
            return storedHash != null && !storedHash.StartsWith(Pbkdf2Prefix, StringComparison.Ordinal);
        }

        /// <summary>
        /// Kiểm tra mật khẩu với giá trị đang lưu, hỗ trợ cả kiểu mới (PBKDF2) và kiểu cũ (MD5).
        /// </summary>
        public static bool VerifyPassword(string password, string storedHash)
        {
            if (password == null || StringUtil.IsEmpty(storedHash))
            {
                return false;
            }

            if (IsLegacyMd5Hash(storedHash))
            {
                // Mật khẩu cũ: MD5 với Encoding.Default, giữ nguyên cách tính để người dùng cũ vẫn đăng nhập được.
                return VerifyMd5Hash(password, storedHash);
            }

            string[] parts = storedHash.Split('$');

            if (parts.Length != 4)
            {
                return false;
            }

            int iterations;

            if (!int.TryParse(parts[1], out iterations) || iterations <= 0)
            {
                return false;
            }

            byte[] salt;
            byte[] expectedHash;

            try
            {
                salt = Convert.FromBase64String(parts[2]);
                expectedHash = Convert.FromBase64String(parts[3]);
            }
            catch (FormatException)
            {
                return false;
            }

            if (expectedHash.Length == 0)
            {
                return false;
            }

            byte[] actualHash = pbkdf2(password, salt, iterations, expectedHash.Length);

            return fixedTimeEquals(actualHash, expectedHash);
        }

        private static byte[] pbkdf2(string password, byte[] salt, int iterations, int size)
        {
            using (Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(password), salt, iterations))
            {
                return deriveBytes.GetBytes(size);
            }
        }

        // So sánh không dừng sớm, để thời gian so sánh không lộ số byte trùng nhau.
        private static bool fixedTimeEquals(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
            {
                return false;
            }

            int diff = 0;

            for (int i = 0; i < a.Length; i++)
            {
                diff |= a[i] ^ b[i];
            }

            return diff == 0;
        }
    }
}
