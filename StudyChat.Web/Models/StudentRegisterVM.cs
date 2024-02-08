using System.ComponentModel.DataAnnotations;

namespace StudyChat.Web.Models
{
    public class StudentRegisterVM
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Institute Name is required")]
        [Display(Name = "Institute Name")]
        public string InstituteName { get; set; }

        [Required(ErrorMessage = "Institute ID Card Number is required")]
        [Display(Name = "Institute ID Card Number")]
        public int InstituteIdCardNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
