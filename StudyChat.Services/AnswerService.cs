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
	public class AnswerService: IAnswerService
	{
		private readonly IAnswerRepository _answerRepository;
		public AnswerService(IAnswerRepository answerRepository)
		{
			_answerRepository = answerRepository;
		}

		public void CreateAnswer(Answer answer)
		{
			_answerRepository.CreateAnswer(answer);
		}

		public async Task<List<Answer>> GetAnswerByQuestionId(int QuestionId)
		{
			 var answer = await _answerRepository.GetAnswerByQuestionId(QuestionId);
			return answer;
		}
	}
}
