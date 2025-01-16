using RegistroUsuario.Dto;
using RegistroUsuario.Helpers;
using RegistroUsuario.Models;

namespace RegistroUsuario.Interfaces
{
    public interface IUserService
    {
        public Task<Users> userRegister(UserDto userDto);
        public ApiResponseMessage<string> validateUser(UserDto userDto);
    }
}
