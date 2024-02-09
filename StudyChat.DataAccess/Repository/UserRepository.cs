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
	public class UserRepository: IUserRepository
	{
		private readonly ApplicationDbContext _context;
		private readonly DbSet<ApplicationUser> _users;

		public UserRepository(ApplicationDbContext context)
		{
			_context = context;
			_users = _context.Set<ApplicationUser>();
		}

		public string? GetUserName(string UserID)
		{
			string? name = _users.Where(u => u.Id == UserID).Select(u => u.UserName).FirstOrDefault();
			return name;
		}
	}
}
