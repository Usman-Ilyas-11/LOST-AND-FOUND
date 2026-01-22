namespace LostAndFound
{
    public class User
    {
        public int Id { get; set; } // db PK
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // "Admin" or "Student"
        public string StudentId { get; set; } // optional id assigned by admin
    }
}
