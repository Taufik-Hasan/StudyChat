using StudyChat.Core.Entities;
using StudyChat.DataAccess.Interface;
using StudyChat.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyChat.Services
{
	public class QuestionService:IQuestionService
	{
		private readonly IQuestionRepository _questionRepository;

		public QuestionService(IQuestionRepository questionRepository)
		{
			_questionRepository = questionRepository;
		}

		public void CreateQuestion(Question question)
		{
			_questionRepository.CreateQuestion(question);
		}

		public async Task<IEnumerable<Question>> GetAllQuestions()
		{
			var questions = await _questionRepository.GetAllQuestions();
			return questions;
		}

		public async Task<IEnumerable<Question>?> GetAllQuestionsByUserID(string UserID)
		{
			var questions = await _questionRepository.GetAllQuestionsByUserID(UserID);
			return questions;
		}
	}
}
