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

		public async Task<List<Answer>> GetAnswerByQuestionId(int QuestionId)
		{
			List<Answer> answersList = new List<Answer>();
			Answer answers = new Answer();
			
			//var query = _context.Answers.FirstOrDefaultAsync(a => a.QuestionId == QuestionId);

			var query = await _context.Answers.Where(a => a.QuestionId == QuestionId).ToListAsync();

			foreach (var item in query)
			{
				answers.Id = item.Id;
				answers.Content = item.Content;
				answers.UserId = item.UserId;
				answers.QuestionId = item.QuestionId;

				answersList.Add(answers);
			}

			return answersList;
		}

		public void UpdateAnswer(Answer answer)
		{
			throw new NotImplementedException();
		}
	}
}
