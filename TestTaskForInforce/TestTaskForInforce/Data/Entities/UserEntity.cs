namespace TestTaskForInforce.Data.Entities
{
    public class UserEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public RoleEntity Role { get; set; }

        public int RoleId { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string LastName { get; set; } = null!;
    }
}
