using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SampleRefactorApi.Data;
using SampleRefactorApi.Models;
using System;
using System.Threading.Tasks;

namespace SampleRefactorApi.Controllers
{
    /// <summary>
    /// A truely RESTful endpoint for all <see cref="User"/> related actions
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User, int> _userRepository;
        private readonly IRepository<Role, int> _roleRepository;

        public IConfiguration Configuration { get; }

        public UserController(IConfiguration configuration)
        {
            _userRepository = new UserRepository(configuration["ConnectionStrings:0:Identity"]);
            _roleRepository = new RoleRepository(configuration["ConnectionStrings:0:Identity"]);
            Configuration = configuration;
        }

        /// <summary>
        /// Gets a customer based on their id.
        /// </summary>
        /// <param name="idNumber">The id of the customer to find.</param>
        /// <returns>A JSON encoded <see cref="User"/> object.</returns>
        [HttpGet(nameof(GetUser) + "/{idNumber}")]
        public IActionResult GetUser(string idNumber)
        {
            HttpContext.Session.SetString("UserId", idNumber);
            return new JsonResult(_userRepository.Get(Convert.ToInt32(idNumber)));
        }

        /// <summary>
        /// Creates a customer based on the values provided.
        /// </summary>
        /// <param name="user">The <see cref="User"/> object to add to the database.</param>
        /// <param name="fast">Boolean, to use async operations or not</param>
        /// <returns>An <see cref="StatusCodes.Status201Created"/> result</returns>
        [HttpGet(nameof(CreateUser))]
        public IActionResult CreateUser([FromQuery] User user, bool fast)
        {
            if (fast)
            {
                ((UserRepository)_userRepository).AddFaster(user);
            }
            else
            {
                _userRepository.Add(user);
            }

            return CreatedAtAction(nameof(GetUser), new { idNumber = user.Id.ToString() }, user);
        }

        /// <summary>
        /// <inheritdoc cref="CreateUser(User, bool)"/>
        /// </summary>
        /// <param name="user">The <see cref="User"/> object to add to the database.</param>
        /// <returns>An <see cref="StatusCodes.Status202Accepted"/> result</returns>
        [HttpGet(nameof(CreateUserFaster))]
        public IActionResult CreateUserFaster(User user)
        {
            Task.Run(() => ((UserRepository)_userRepository).AddFaster(user));

            return Accepted();
        }

        /// <summary>
        /// Returns all of the <see cref="Role"/>'s for the user.
        /// </summary>
        /// <returns>A JSON encoded collection of <see cref="Role"/>s</returns>
        [HttpGet(nameof(GetRoleForUser))]
        public IActionResult GetRoleForUser()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var repo = (RoleRepository)_roleRepository;

            return new JsonResult(repo.GetUserRole(_userRepository.Get(Convert.ToInt32(userId))));
        }
    }
}