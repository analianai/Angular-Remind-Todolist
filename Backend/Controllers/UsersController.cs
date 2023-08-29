using Backend.Models;
using Backend.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UsersService usersService;

        public UsersController(UsersService usersService) {
            this.usersService = usersService;
        }

        [HttpGet]
        public IActionResult GetOne() {
            try {
                var httpUser = HttpContext.User;

                var userIdClaim = httpUser.FindFirst(ClaimTypes.Name);

                if (userIdClaim == null) {
                    throw new Exception("Token inválido!");
                }

                var userId = int.Parse(userIdClaim.Value);
                               
                return Ok(usersService.GetOne(userId));
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Create(Users user) {
            try {
                if (String.IsNullOrEmpty(user.Name))
                {
                    throw new Exception("Insira um Nome Válido");
                }
                if (String.IsNullOrEmpty(user.Email))
                {
                    throw new Exception("Insira um Email válido com @ e ponto");
                }
                if (String.IsNullOrEmpty(user.Password))
                {
                    throw new Exception("Insira um Senha válida");
                }
                return Ok(usersService.Create(user));
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Update(Users user) {
            try {
                var httpUser = HttpContext.User;

                var userIdClaim = httpUser.FindFirst(ClaimTypes.Name);

                if (userIdClaim == null) {
                    throw new Exception("Token inválido!");
                }

                var userId = int.Parse(userIdClaim.Value);

                usersService.Update(userId, user);
                return Ok(user);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("auth")]
        [AllowAnonymous]
        public IActionResult Auth(Users user) {
            try {
                return Ok(new { token = usersService.Auth(user) });
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
