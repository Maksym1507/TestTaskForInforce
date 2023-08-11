namespace TestTaskForInforce.Data.Entities
{
    public class RoleEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public List<UserEntity> Users { get; set; } = new ();
    }
}
