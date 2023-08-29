using Backend.Models;
using Backend.Services.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TasksController : Controller
    {
        private readonly TasksService tasksService;

        public TasksController(TasksService tasksService) {
            this.tasksService = tasksService;
        }

        [HttpGet]
        public IActionResult GetTaskByUser() {
            try {
                var httpUser = HttpContext.User;

                var userIdClaim = httpUser.FindFirst(ClaimTypes.Name);

                if (userIdClaim == null) {
                    throw new Exception("Token inválido!");
                }

                var userId = int.Parse(userIdClaim.Value);
                return Ok(tasksService.GetTaskByUser(userId));
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("id/{taskId}")]
        public IActionResult GetOneTaskByUser(int taskId) {
            try {
                var httpUser = HttpContext.User;

                var userIdClaim = httpUser.FindFirst(ClaimTypes.Name);

                if (userIdClaim == null) {
                    throw new Exception("Token inválido!");
                }

                var userId = int.Parse(userIdClaim.Value);

                return Ok(tasksService.GetOneTaskByUser(userId, taskId));
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("status/{taskStatus}")]
        public IActionResult SearchStatusTaskByUser(string taskStatus) {
            try {
                var httpUser = HttpContext.User;

                var userIdClaim = httpUser.FindFirst(ClaimTypes.Name);

                if (userIdClaim == null) {
                    throw new Exception("Token inválido!");
                }

                var userId = int.Parse(userIdClaim.Value);

                return Ok(tasksService.SearchStatusTaskByUser(userId, taskStatus));
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("name/{taskName}")]
        public IActionResult SearchNameTaskByUser(string taskName) {
            try {

                var httpUser = HttpContext.User;

                var userIdClaim = httpUser.FindFirst(ClaimTypes.Name);

                if (userIdClaim == null) {
                    throw new Exception("Token inválido!");
                }

                var userId = int.Parse(userIdClaim.Value);
                return Ok(tasksService.SearchNameTaskByUser(userId, taskName));
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateTastByUser(Tasks task) {
            try {
                var httpUser = HttpContext.User;

                var userIdClaim = httpUser.FindFirst(ClaimTypes.Name);

                if (userIdClaim == null) {
                    throw new Exception("Token inválido!");
                }

                var userId = int.Parse(userIdClaim.Value);
                tasksService.CreateTastByUser(userId, task);
                return Ok(task);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{taskId}")]
        public IActionResult UpdateTastByUser(Tasks task, int taskId) {
            try {
                var httpUser = HttpContext.User;

                var userIdClaim = httpUser.FindFirst(ClaimTypes.Name);

                if (userIdClaim == null) {
                    throw new Exception("Token inválido!");
                }

                var userId = int.Parse(userIdClaim.Value);

                tasksService.UpdateTastByUser(userId, taskId, task);
                return Ok(task);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{taskId}")]
        public IActionResult DeleteTaskByUser(int taskId) {
            try {
                var httpUser = HttpContext.User;

                var userIdClaim = httpUser.FindFirst(ClaimTypes.Name);

                if (userIdClaim == null) {
                    throw new Exception("Token inválido!");
                }

                var userId = int.Parse(userIdClaim.Value);

                tasksService.DeleteTaskByUser(userId, taskId);
                return Ok();
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
