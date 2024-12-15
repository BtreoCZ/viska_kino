using System.ComponentModel.DataAnnotations;

namespace viska_kino_mvc.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name must be between 2 and 50 characters.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(30, ErrorMessage = "Username must be between 3 and 30 characters.", MinimumLength = 3)]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password must be at least 6 characters.", MinimumLength = 6)]
        public string Password { get; set; }

    }
}
