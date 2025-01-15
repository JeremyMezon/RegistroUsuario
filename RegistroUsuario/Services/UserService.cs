using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RegistroUsuario.Context;
using RegistroUsuario.Dto;
using RegistroUsuario.Interfaces;
using RegistroUsuario.Models;
using System.Text.RegularExpressions;

namespace RegistroUsuario.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext context;
        public UserService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<dynamic> UserRegister(UserDto userDto)
        {
            if (!validateUser(userDto)) return null;
            var user = new Users
            {
                name = userDto.name,
                email = userDto.email,
                password = userDto.password,
                phones = userDto.phone.Select(ph => new Models.Phone
                {
                    number = ph.number,
                    citycode = ph.citycode,
                    countrycode = ph.countrycode,
                }).ToList(),
                created = DateTime.Now,
                modified = DateTime.Now,
                last_login = DateTime.Now,
                token = Guid.NewGuid().ToString(),
                is_active = true
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return await context.Users
            .Include(u => u.phones)
            .Select(p => new { p.id,p.created,p.modified,p.last_login,p.token,p.is_active })
            .FirstOrDefaultAsync(u => u.id == user.id);
        }

        private bool validateEmailFormat(string email)
        {
            string expression = "^[^@]+@[^@]+\\.[a-zA-Z]";
            return Regex.IsMatch(email, expression);
        }

        private bool validatePasswordFormat(string password)
        {
            string expression = Environment.GetEnvironmentVariable("PASSWORD_REGEXP");
            return Regex.IsMatch(password, expression);
        }

        private bool emailAlreadyExists(UserDto userDto)
        {
            var user = context.Users.Where(u => u.email == userDto.email).FirstOrDefault();
            if (user != null)
                return true;
            return false;
        }

        private bool validateUser(UserDto userDto)
        {
            if (!validateEmailFormat(userDto.email)) return false;
            if (!validatePasswordFormat(userDto.password)) return false;
            if (emailAlreadyExists(userDto)) return false;
            return true;
        }
    }
}
