namespace authTest.Dto
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Email { get; set; } = null!;
        public string? Name { get; set; }
        public string? RoleName { get; set; }
        public string? Parent { get; set; }
        public bool? IsActive { get; set; }
    }
}
