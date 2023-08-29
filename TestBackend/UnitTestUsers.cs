using Backend.Data;
using Backend.Models;
using Backend.Services.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace TestBackend
{
    public class UnitTestUsers
    {
        private UsersService usersService;
        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext applicationDbContext;
        public UnitTestUsers() {
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            applicationDbContext = new ApplicationDbContext(dbContextOptions);
                        
            usersService = new UsersService(applicationDbContext, configuration);
        }

        [Fact]
        public void CreateMockOk() {
            Users user = new Users() {
                Name = "Test",
                Email = "test6@test.com",
                Password = "test"
            };

            var mock = new Mock<UsersService>(applicationDbContext, configuration);

            mock.Setup(u => u.Create(user)).Returns(new Users {
                Name = "Test",
                Email = "test6@test.com",
                Password = "test"
            });

            usersService = mock.Object;

            var response1 = usersService.Create(user);

            Assert.Equal(user.Email, response1.Email);
        }

        [Fact]
        public void CreateOk() {
            Users user = new Users() {
                Name = "Test",
                Email = "test1@test.com",
                Password = "test"
            };

            var response = usersService.Create(user);

            Assert.Equal(user.Email, response.Email);
        }

        [Fact]
        public void CreateEmailExist() {
            Users user = new Users() {
                Name = "Test",
                Email = "test2@test.com",
                Password = "test"
            };

            var response1 = usersService.Create(user);
                      
            Assert.Equal(user.Email, response1.Email);
            Assert.Throws<Exception>(() => usersService.Create(user));
        }

        [Fact]
        public void CreateUserVoid() {
            Users user1 = new Users();            

            Assert.Throws<Exception>(() => usersService.Create(user1));           
        }

        [Fact]
        public void UpdateOk() {
            Users user = new Users() {
                Name = "Test",
                Email = "test3@test.com",
                Password = "test"
            };

            var responseCreate = usersService.Create(user);
            Assert.NotNull(responseCreate);
            Assert.Equal(user.Email, responseCreate.Email);

            user.Email = "test4@test.com";

            var responseUpdate = usersService.Update(responseCreate.UserId, user);

            Assert.NotNull(responseUpdate);
            Assert.Equal(user.Email, responseUpdate.Email);
        }

        
    }
}