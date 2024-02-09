using StudyChat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyChat.DataAccess.Interface
{
	public interface IUserRepository
	{
		string? GetUserName(string UserID);
	}
}
