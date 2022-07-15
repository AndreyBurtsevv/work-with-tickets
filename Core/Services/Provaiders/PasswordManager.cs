namespace BoxOffice.Core.Services.Provaiders
{
    public static class PasswordManager
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
        }

        public static bool VerifyPassword(string password, string hash)
        {
            if (password == null || hash == null) return false;
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
