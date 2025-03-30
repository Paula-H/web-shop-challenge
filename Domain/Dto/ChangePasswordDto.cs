namespace Domain.Dto
{
    public class ChangePasswordDto
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string OldPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}
