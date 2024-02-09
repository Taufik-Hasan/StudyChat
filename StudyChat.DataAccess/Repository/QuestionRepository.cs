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
	public class QuestionRepository: IQuestionRepository
	{
		private readonly ApplicationDbContext _context;
		private DbSet<Question> _table;

		public QuestionRepository(ApplicationDbContext context)
		{
			_context = context;
			_table = _context.Set<Question>();
		}

		public void CreateQuestion(Question question)
		{
			_table.Add(question);
			_context.SaveChanges();
		}

		public void DeleteQuestion(Question question)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Question>> GetAllQuestions()
		{
			var questions = new List<Question>();
			var query = await _context.Questions.OrderByDescending(s => s.Id).ToListAsync();

			if (query.Any() == true)
			{
				foreach (var item in query)
				{
					questions.Add(new Question()
					{
						Id = item.Id,
						Content = item.Content,
						UserId = item.UserId,
						IsAnswered = item.IsAnswered
					});
				}
			}

			return questions;
		}

		public async Task<IEnumerable<Question>?> GetAllQuestionsByUserID(string userId)
		{
			var questions = new List<Question>();
			var query = await _context.Questions.Where(s => s.UserId.ToString().ToLower() == userId.ToLower()).ToListAsync();
			if (query.Any() == true)
			{
				foreach (var item in query)
				{
					questions.Add(new Question()
					{
						Id = item.Id,
						Content = item.Content,
						UserId = item.UserId,
						IsAnswered = item.IsAnswered
					});
				}
			}

			return questions;
		}

	}
}
