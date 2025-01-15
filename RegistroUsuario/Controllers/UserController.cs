using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegistroUsuario.Dto;
using RegistroUsuario.Interfaces;

namespace RegistroUsuario.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService userService;
        private IConfiguration configuration;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        // POST: UserController/Create
        [HttpPost]
        public async Task<ActionResult> UserRegister([FromBody]UserDto user )
        {
            var User = await userService.UserRegister(user);
            return Created("/",new { mensaje = "Hola" ,data = User });
        }

    }
}
