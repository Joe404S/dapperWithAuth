namespace SigninSignupDapper.Models
{
    public class User
    {
        public required long ID { get; set; }
        public required String FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
