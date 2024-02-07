using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyChat.Core.Entities
{
	public class Student:ApplicationUser
	{
		public string InstituteName { get; set; }
		public int InstituteIdCardNumber { get; set; }
	}
}
