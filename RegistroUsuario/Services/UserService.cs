using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using RegistroUsuario.Context;
using RegistroUsuario.Dto;
using RegistroUsuario.Helpers;
using RegistroUsuario.Interfaces;
using RegistroUsuario.Models;
using System.Text.RegularExpressions;

namespace RegistroUsuario.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext context;
        private readonly TokenGenerator tokenGenerator;
        public UserService(AppDbContext context, TokenGenerator tokenGenerator)
        {
            this.context = context;
            this.tokenGenerator = tokenGenerator;
        }

        public async Task<Users> userRegister(UserDto userDto)
        {
            string token = tokenGenerator.generateToken(userDto);
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
                token = token,
                is_active = true
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return await context.Users
            .Include(u => u.phones)
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

        public ApiResponseMessage<string> validateUser(UserDto userDto)
        {
            var test = new ApiResponseMessage<string> { success = false, message = "Mensaje"};
            ApiResponseMessage<string> apiResponse = new ApiResponseMessage<string>();
            if (!validateEmailFormat(userDto.email))
                return new ApiResponseMessage<string> { success = false, message = "Formato de email no es correcto" };
            if (!validatePasswordFormat(userDto.password)) 
                return new ApiResponseMessage<string> { success = false, message = "Formato de contraseña no es correcto" };
            if (emailAlreadyExists(userDto)) 
                return new ApiResponseMessage<string> { success = false, message = "El correo ya esta registrado" }; ;
            return new ApiResponseMessage<string> { success = true, message = "Usuario validado" }; ;
        }
    }
}
