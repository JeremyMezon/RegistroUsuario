using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegistroUsuario.Dto;
using RegistroUsuario.Interfaces;

namespace RegistroUsuario.Controllers
{
    [ApiController]
    [Route("user")]
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
            if (!ModelState.IsValid) { 
                return BadRequest(new { message = "El modelo de request no es valido" });
            }
            var userValidation = userService.validateUser(user);
            if(!userValidation.success) return BadRequest(userValidation);

            var User = await userService.userRegister(user);
            return Created("/", User);
        }

    }
}
