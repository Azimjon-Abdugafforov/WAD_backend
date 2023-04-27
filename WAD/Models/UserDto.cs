using System.ComponentModel.DataAnnotations;

namespace WAD.Models
{
    public class UserDto
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get ; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;

    }
}
