namespace JRamedia.Models
{
    public static class CurrUser
    {
        public static int Id;
        public static string Username;
        public static string Email;
        public static string Role;

        public static void setUser(int id, string username, string email, string role)
        {
            Id = id;
            Username = username;
            Email = email;
            Role = role;   
        }
    }
}
