using Backend.Data;
using Backend.Models;
using System.Threading.Tasks;

namespace Backend.Services.Tasks
{
    public class TasksService
    {
        private readonly ApplicationDbContext context;
        private IConfiguration Configuration { get; }


        public TasksService(ApplicationDbContext context, IConfiguration configuration) {
            this.context = context;
            Configuration = configuration;
        }

        public List<Models.Tasks> GetTaskByUser(int userId) {
            try {
                var tasks = context.Tasks.Where(t => t.UserId == userId).ToList();

                if (!tasks.Any()) {
                    throw new Exception($"Não existem tarefas para o usuário com esse id {userId}");
                }

                return tasks;
            }
            catch (Exception error) {
                throw new Exception($"Ocorreu um erro ao buscar a tarefa. {error.Message}");
            }
        }

        public Models.Tasks GetOneTaskByUser(int userId, int taskId) {
            try {
                var taskExists = context.Tasks.FirstOrDefault(t => t.TaskId == taskId && t.UserId == userId);

                if (taskExists == null) {
                    throw new Exception($"Não foi encontrado nenhuma tarefa para esse id: {taskId}");
                }

                return taskExists;
            }
            catch (Exception error) {
                throw new Exception($"Ocorreu um erro ao buscar a tarefa. {error.Message}");
            }
        }

        public Models.Tasks SearchStatusTaskByUser(int userId, string taskStatus) {
            try {
                var taskExists = context.Tasks.FirstOrDefault(t => t.Status == taskStatus && t.UserId == userId);
                if (taskExists == null) {
                    throw new Exception($"Não foi encontrado nenhuma tarefa para esse status: {taskStatus}");
                }

                return taskExists;
            }
            catch (Exception error) {
                throw new Exception($"Ocorreu um erro ao buscar a tarefa. {error.Message}");
            }
        }

        public Models.Tasks SearchNameTaskByUser(int userId, string taskName) {
            try {
                var taskExists = context.Tasks.FirstOrDefault(t => t.Title == taskName && t.UserId == userId);
                if (taskExists == null) {
                    throw new Exception($"Não foi encontrado nenhuma tarefa para esse nome: {taskName}");
                }

                return taskExists;
            }
            catch (Exception error) {
                throw new Exception($"Ocorreu um erro ao buscar a tarefa. {error.Message}");
            }
        }


        public Models.Tasks CreateTastByUser(int userId, Models.Tasks task) {
            try {
                var taskExists = context.Tasks.FirstOrDefault(t => t.Title == task.Title && t.UserId == userId);
                if (taskExists != null) {
                    throw new Exception($"Já existe uma tarefa cadastrada com esse titulo: {task.Title}");
                }

                task.UserId = userId;
                task.Status = "Planejado";

                context.Tasks.Add(task);
                context.SaveChanges();

                return task;
            }
            catch (Exception error) {
                throw new Exception($"Ocorreu um erro ao buscar a tarefa. {error.Message}");
            }
        }

        public void UpdateTastByUser(int userId, int taskId, Models.Tasks task) {
            try {
                var taskExists = context.Tasks.FirstOrDefault(t => t.TaskId == taskId && t.UserId == userId);
                if (taskExists == null) {
                    throw new Exception($"Não foi encontrado nenhuma tarefa para esse id: {taskId}");
                }

                var taskExistsTitle = context.Tasks.FirstOrDefault(t => t.TaskId != taskId && t.UserId == userId && t.Title == task.Title);
                if (taskExistsTitle != null) {
                    throw new Exception($"Já existe uma tarefa cadastrada com esse titulo: {task.Title}");
                }

                taskExists.Title = task.Title;
                taskExists.Description = task.Description;
                taskExists.Status = task.Status;
                
                context.SaveChanges();
            }
            catch (Exception error) {
                throw new Exception($"Ocorreu um erro ao buscar a tarefa. {error.Message}");
            }
        }

        public void DeleteTaskByUser(int userId, int taskId) {
            try {
                var taskExists = context.Tasks.FirstOrDefault(t => t.TaskId == taskId && t.UserId == userId);
                if (taskExists == null) {
                    throw new Exception($"Não foi encontrado nenhuma tarefa para esse id: {taskId}");
                }

                context.Remove(taskExists);
                context.SaveChanges();
            }
            catch (Exception error) {
                throw new Exception($"Ocorreu um erro ao buscar a tarefa. {error.Message}");
            }
        }
    }
}
