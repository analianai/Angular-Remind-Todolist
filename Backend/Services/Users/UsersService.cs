using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Backend.Services.Users
{
    public class UsersService
    {
        private readonly ApplicationDbContext context;
        private IConfiguration Configuration { get; }
               

        public UsersService(ApplicationDbContext context, IConfiguration configuration) {
            this.context = context;
            Configuration = configuration;
        }

        public Models.Users GetOne(int userId) {
            try {
                var userExists = context.Users.FirstOrDefault(u => u.UserId == userId);

                if (userExists == null) {
                    throw new Exception($"Não foi encontrado usuário para esse id: {userId}");
                }
                return userExists;
            }
            catch (Exception error) {
                throw new Exception($"Ocorreu um erro ao buscar o usuário. {error.Message}");
            }
        }

        public virtual Models.Users Create(Models.Users user) {
            try {
                var userExists = context.Users.FirstOrDefault(u => u.Email == user.Email);

                if (userExists != null) {
                    throw new Exception($"Já existe um usuário com esse e-mail: {user.Email}");
                }
                    context.Users.Add(user);
                    context.SaveChanges();
                    return user;           
            }
            catch (Exception error) {
                throw new Exception($"Ocorreu um erro ao cadastrar um usuário. {error.Message}");
            }
        }

        public Models.Users Update(int userId, Models.Users user) {
            try {
                var userExists = context.Users.FirstOrDefault(u => u.UserId == userId);

                if (userExists == null) {
                    throw new Exception($"Não foi encontrado usuário para esse id: {userId}");
                }

                var userExistsEmail = context.Users.FirstOrDefault(u => u.UserId != userId && u.Email == user.Email);

                if (userExistsEmail != null) {
                    throw new Exception($"Já existe um usuário com esse e-mail: {user.Email}");
                }

                userExists.Name = user.Name;
                userExists.Email = user.Email;
                userExists.Password = user.Password;

                context.SaveChanges();

                return userExists;
            }
            catch (Exception error) {
                throw new Exception($"Ocorreu um erro ao atualizar o usuário. {error.Message}");
            }
        }

        public string Auth(Models.Users user) {
            try {
                var userExists = context.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
                if (userExists == null) {
                    throw new Exception("Credenciais inválidas!");
                }

                return GetToken(userExists);
            }
            catch (Exception error) {
                throw new Exception($"Ocorreu um erro ao autenticar. {error.Message}");
            }
        }

        public string GetToken(Models.Users user) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtKey = Configuration.GetSection("JWT:Key").Value;
            var key = Encoding.ASCII.GetBytes(jwtKey);

            var claims = new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.Name, user.UserId.ToString())
            });

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = claims,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
