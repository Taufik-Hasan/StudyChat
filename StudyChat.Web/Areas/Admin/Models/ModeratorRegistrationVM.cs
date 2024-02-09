using System.ComponentModel.DataAnnotations;

namespace StudyChat.Web.Areas.Admin.Models
{
	public class ModeratorRegistrationVM
	{
		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }
		[Required]
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
