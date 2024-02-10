using StudyChat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyChat.Services
{
	public interface IUserService
	{
		string? GetUserId { get; }
		bool? IsAuthenticated { get; }
		string? GetUserName(string UserID);
		public Task<List<(string, string, string)>> GetRespondedQA(string UserID);
	}
}
