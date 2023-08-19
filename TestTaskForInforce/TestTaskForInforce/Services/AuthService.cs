using AutoMapper;
using Microsoft.VisualBasic;
using System.Security.Claims;
using TestTaskForInforce.Services.Abstractions;

namespace TestTaskForInforce.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public AuthService(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<ClaimsIdentity> LoginUserAsync(string email, string password)
        {
            var user = await _userService.GetUserByEmailAndPasswordAsync(email, password);

            if (user == null)
            {
                throw new Exception("User not found");
            };

            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role!.Name)
                };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);

            return id;
        }
    }
}
