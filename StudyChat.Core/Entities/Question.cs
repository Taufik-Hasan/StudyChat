using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyChat.Core.Entities
{
	public class Question
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public String UserId { get; set; }    // User who asked the question , identity User Id
		public bool IsAnswered { get; set; }
	}
}
