using AutoMapper;
using DesafioEncodeBDDO.Models;
using DesafioEncodeBDDO.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace DesafioEncodeBDDO.Services
{
    public class LoginService
    {
        public readonly ApplicationContext _dbContext;

        public LoginService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetUser(UserDto user)
        {
            var usr = new User();
            usr.NameUser = user.NameUser;
            usr.Password = user.Password;

            return await _dbContext.User
                .SingleOrDefaultAsync(us => us.NameUser == user.NameUser && us.Password == user.Password);
        }
    }
}
