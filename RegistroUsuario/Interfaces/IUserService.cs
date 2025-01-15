using RegistroUsuario.Dto;

namespace RegistroUsuario.Interfaces
{
    public interface IUserService
    {
        public Task<dynamic> UserRegister(UserDto userDto);
    }
}
