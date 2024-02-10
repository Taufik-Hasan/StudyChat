using StudyChat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyChat.Services.Interface
{
	public interface IQuestionService
	{
		public void CreateQuestion(Question question);
		public Task DeleteQuestionByQuestionID(int QuestionId);
		public Task<IEnumerable<Question>?> GetAllQuestionsByUserID(string UserID);
		public Task<IEnumerable<Question>> GetAllQuestions();
		public Task<Question> GetQuestionById(int id);
	}
}
