using Backend.Data;
using Backend.Models;
using Backend.Services.Tasks;
using Backend.Services.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;



namespace TestBackend
{
    public class UnitTestsTasks
    {
        public readonly TasksService tasksService;
        public readonly UsersService usersService;//Apontou para a classe Service  do User
        public readonly IConfiguration configuration;
        public readonly ApplicationDbContext applicationaDBContext;

        public UnitTestsTasks()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            applicationaDBContext = new ApplicationDbContext(dbContextOptions);

            tasksService = new TasksService(applicationaDBContext, configuration);
            usersService = new UsersService(applicationaDBContext, configuration);
        }

        [Fact]
        public void GetOneTasksByOK()
        {
            Users user = new Users()
            {
                UserId = 21,
                Name = "Test",
                Email = "test1test.com",
                Password = "test"
            };

            var resultCreate = usersService.Create(user);

            Assert.NotNull(resultCreate);
            Assert.Equal(user.Email, resultCreate.Email);

            Tasks tasks = new Tasks()
            {
                Title = "Alteração no User 1",
                Description = "Remover funcionalidades extras",
                Status = "Planejado",
                UserId = 21
            };

            var resultCreateTask = tasksService.CreateTastByUser(tasks, resultCreate.UserId);

            Assert.NotNull(resultCreateTask);

            var result = tasksService.GetOneTaskBy(resultCreateTask.TaskId, resultCreate.UserId);
            Assert.Equal(resultCreateTask.UserId, result?.UserId);
        }


        [Fact]
        public void GetOneTaskByUserOK()
        {
            Users user = new Users()
            {
                UserId = 20,
                Name = "Test",
                Email = "test.2@test.com",
                Password = "test"
            };

            var resultCreate = usersService.Create(user);

            Assert.NotNull(resultCreate);
            Assert.Equal(user.Email, resultCreate.Email);

            Tasks tasks = new Tasks()
            {
                Title = "Alteração 2 no User",
                Description = "Remover funcionalidades extras",
                Status = "Planejado",
                UserId = 20
            };

            var resultCreateTask = tasksService.CreateTastByUser(tasks, resultCreate.UserId);

            Assert.NotNull(resultCreateTask);

            var result = tasksService.GetOneTaskByUser(resultCreate.UserId);
            Assert.Equal(tasks.UserId, result[0].UserId);
        }

        [Fact]
        public void SearchStatusTaskByUserOK()
        {
            Users user = new Users()
            {
                UserId = 3,
                Name = "Test",
                Email = "test3@test.com",
                Password = "test"
            };

            var resultCreate = usersService.Create(user);

            Assert.NotNull(resultCreate);
            Assert.Equal(user.Email, resultCreate.Email);

            Tasks tasks = new Tasks()
            {
                Title = "User 03",
                Description = "Remover funcionalidades extras",
                Status = "Planejado",
                UserId = 3
            };

            var resultCreateTaskStatus = tasksService.CreateTastByUser(tasks, resultCreate.UserId);

            Assert.NotNull(resultCreateTaskStatus);

            var result = tasksService.SearchStatusTaskByUser(resultCreateTaskStatus.Status, resultCreate.UserId);
            Assert.Equal(tasks.Status, result.Status);
        }

        [Fact]
        public void SearchNameTaskByUserOK()
        {
            Users user = new Users()
            {
                UserId = 4,
                Name = "Test",
                Email = "test44@test.com",
                Password = "test"
            };

            var resultCreate = usersService.Create(user);

            Assert.NotNull(resultCreate);
            Assert.Equal(user.Email, resultCreate.Email);

            Tasks tasks = new Tasks()
            {
                Title = "Alteração no User 4",
                Description = "Remover funcionalidades extras",
                Status = "Planejado",
                UserId = 4
            };

            var resultCreateTaskName = tasksService.CreateTastByUser(tasks, resultCreate.UserId);

            Assert.NotNull(resultCreateTaskName);

            var result = tasksService.SearchNameTaskByUser(resultCreateTaskName.Title, resultCreate.UserId);
            Assert.Equal(tasks.Title, result.Title);
        }

        [Fact]
        public void CreateTastByUserOK()
        {
            Users user = new Users()
            {
                UserId = 5,
                Name = "Test",
                Email = "test.5@test.com",
                Password = "test"
            };

            var resultCreate = usersService.Create(user);

            Assert.NotNull(resultCreate);
            Assert.Equal(user.Email, resultCreate.Email);

            Tasks tasks = new Tasks()
            {
                Title = "Alteração no User 5",
                Description = "Remover funcionalidades extras",
                Status = "Planejado",
                UserId = 5
            };

            var resultCreateTask = tasksService.CreateTastByUser(tasks, resultCreate.UserId);

            Assert.NotNull(resultCreateTask);
            Assert.Equal(resultCreateTask.Status, tasks.Status);
        }

        [Fact]
        public void UpdateTastByUserOK()
        {
            Users user = new Users()
            {
                UserId = 6,
                Name = "Test",
                Email = "test5@test.com",
                Password = "test"
            };

            var resultCreate = usersService.Create(user);

            Assert.NotNull(resultCreate);
            Assert.Equal(user.Email, resultCreate.Email);

            Tasks tasks = new Tasks()
            {
                Title = "Alteração no User 1",
                Description = "Remover funcionalidades extras",
                Status = "Planejado",
                UserId = 6
            };

            var resultCreateTask = tasksService.CreateTastByUser(tasks, resultCreate.UserId);

            Assert.NotNull(resultCreateTask);
            Assert.Equal(resultCreateTask.Status, tasks.Status);


            tasks.Title = "Alteração no User 5";
            tasks.Description = "Remover funcionalidades extras";
            tasks.Status = "Planejado";
            tasks.UserId = 6;

            var resultUpdate = tasksService.UpdateTastByUser(tasks.TaskId, tasks, resultCreate.UserId);

            Assert.NotNull(resultUpdate);
            Assert.Equal(tasks, resultUpdate);
        }

        [Fact]
        public void DeleteTaskByUserOK()
        {
            Users user = new Users()
            {
                UserId = 7,
                Name = "Test",
                Email = "test7@test.com",
                Password = "test"
            };

            var resultCreate = usersService.Create(user);

            Assert.NotNull(resultCreate);
            Assert.Equal(user.Email, resultCreate.Email);

            Tasks tasks = new Tasks()
            {
                Title = "Alteração no User 7",
                Description = "Remover funcionalidades extras",
                Status = "Planejado",
                UserId = 7
            };

            var resultCreateTask = tasksService.CreateTastByUser(tasks, resultCreate.UserId);

            Assert.NotNull(resultCreateTask);
            Assert.Equal(resultCreateTask.Status, tasks.Status);


            var resultDelete = tasksService.DeleteTaskByUser(tasks.TaskId, resultCreate.UserId);

            Assert.NotNull(resultDelete);
            Assert.Equal(resultDelete.Status, tasks.Status);
        }

    }
}
