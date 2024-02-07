using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyChat.Core.Entities
{
	public class ApplicationUser:IdentityUser
	{
		[StringLength(100)]
		[MaxLengthAttribute(100)]
		[Required]
		public string Name { get; set; } // Full Name of the user. Every user must have a name. This is common property for all user types
	}
}
