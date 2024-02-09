using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using StudyChat.DataAccess.Interface;
using StudyChat.Services.Interface;

namespace StudyChat.Services
{

    public class UserService: IUserService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IUserRepository _userRepository;

		public UserService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
		{
			_httpContextAccessor = httpContextAccessor;
			_userRepository = userRepository;
			//I add x for test purpose and there is no user information here.
			var x = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

		}


		public string? GetUserId
		{
			get
			{
				var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
				return userIdClaim;
			}
		}

		public bool? IsAuthenticated => GetUserId != null;


		public string? GetUserName(string UserID)
		{
			string? name = _userRepository.GetUserName(UserID);
			return name;
		}


	}
}
