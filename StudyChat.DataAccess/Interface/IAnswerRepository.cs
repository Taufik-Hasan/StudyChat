using StudyChat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyChat.DataAccess.Interface
{
	public interface IAnswerRepository
	{
		public void CreateAnswer(Answer answer);
		public void UpdateAnswer(Answer answer);
		public void DeleteAnswer(int id);
		public Task<Answer> GetAnswerById(int id);
		public Task<Answer> GetAnswerByQuestionId(int id);
	}
}
