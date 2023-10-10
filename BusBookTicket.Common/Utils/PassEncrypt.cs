namespace BusBookTicket.Common.Utils
{
    public class PassEncrypt
    {
        public static string hashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            string hashPass = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return hashPass;
        }

        public static bool verifyPassword (string password, string hashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashPassword);
        }
    }
}
