using Microsoft.EntityFrameworkCore;
using StudyChat.Core.Entities;
using StudyChat.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyChat.DataAccess.Repository
{
	public class AnswerRepository : IAnswerRepository
	{
		private readonly ApplicationDbContext _context;
		private DbSet<Answer> _answerTable;
		private DbSet<Question> _questionTable;

        public AnswerRepository(ApplicationDbContext context)
        {
			_context = context;
			_answerTable = _context.Set<Answer>();
			_questionTable = _context.Set<Question>();
		}

        public void CreateAnswer(Answer answer)
		{
			_answerTable.Add(answer);
			_questionTable.FirstOrDefault(q => q.Id == answer.QuestionId).IsAnswered=true;
			_context.SaveChanges();
		}

		public void DeleteAnswer(int id)
		{
			throw new NotImplementedException();
		}

		public Task<Answer> GetAnswerById(int id)
		{
			throw new NotImplementedException();
		}

		public Task<Answer> GetAnswerByQuestionId(int QuestionId)
		{
			Answer answers = new Answer();
			var query = _context.Answers.FirstOrDefaultAsync(a => a.QuestionId == QuestionId);
			

			if (query != null)
			{
				answers.Id = query.Result.Id;
				answers.Content = query.Result.Content;
				answers.UserId = query.Result.UserId;
				answers.QuestionId = query.Result.QuestionId;
			}
			return Task.FromResult(answers);
		}

		public void UpdateAnswer(Answer answer)
		{
			throw new NotImplementedException();
		}
	}
}
