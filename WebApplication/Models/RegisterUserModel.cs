using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class RegisterUserModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Password Must Be Strong", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#.?&])[A-Za-z\d@$!%*#.?&]{8,}$", ErrorMessage = "Password Must Be Strong")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(50, ErrorMessage ="Password Must Be Strong", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#.?&])[A-Za-z\d@$!%*#.?&]{8,}$", ErrorMessage = "Password Must Be Strong")]
        [DataType(DataType.Password)]
        public string RePassword { get; set; }

        [Required]

        public bool Terms { get; set; }
    }
}
