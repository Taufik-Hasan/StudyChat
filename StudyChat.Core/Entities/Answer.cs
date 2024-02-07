using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyChat.Core.Entities
{
	public class Answer
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public int QuestionId { get; set; }
		public string UserId { get; set; } // User who answer the question , identity User Id

	}
}
