using System.ComponentModel.DataAnnotations;

namespace StudyChat.Web.Models
{
	public class LoginVM
	{
		[Required(ErrorMessage = "Email is required")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "Remember Me")]
		public bool RememberMe { get; set; }

		[Required]
		[Display(Name = "Account Type")]
		public string UserRole { get; set; }
	}
}
