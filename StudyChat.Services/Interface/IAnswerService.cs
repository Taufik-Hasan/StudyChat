using StudyChat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyChat.Services.Interface
{
	public interface IAnswerService
	{
		public void CreateAnswer(Answer answer);
		public Task<List<Answer>> GetAnswerByQuestionId(int QuestionId);
	}
}
