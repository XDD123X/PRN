using System.ComponentModel.DataAnnotations;

namespace Matcher.Models
{
    public class UserRegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public string DateOfBirth { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Gender { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Location { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string AgreeTerm { get; set; }
    }
}