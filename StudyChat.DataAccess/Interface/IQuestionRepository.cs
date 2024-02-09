using StudyChat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyChat.DataAccess.Interface
{
	public interface IQuestionRepository
	{
		public void CreateQuestion(Question question);
		public void DeleteQuestion(Question question);
		public Task<IEnumerable<Question>?> GetAllQuestionsByUserID(string UserID);
		public Task<IEnumerable<Question>> GetAllQuestions();
	}
}
