using ForumSystemApi1.Data;
using ForumSystemApi1.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForumSystemApi1.Controllers
{

    [Route("api/[controller")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly JwtSettings jwtSettings;

        public UsersController(ApplicationDbContext context, JwtSettings jwtSettings = null)
        {
            this.context = context;
            this.jwtSettings = jwtSettings;
        }

        /*
                [HttpPost("register")]
                public ActionResult Register(string username,string password)
                {
                    this.context.Users.Add(new Data.User
                    {
                        Username = username,
                        Password=password
                    });

                    this.context.SaveChanges();

                    return this.Ok();
                }
        */

        [HttpPost("register")]
        public ActionResult Register([FromBody] UsersBindingModel usersBindingModel)
        {
            if (usersBindingModel == null)
            {
                throw new ArgumentNullException(nameof(usersBindingModel.Username));
            }

            this.context.Users.Add(new Data.User
            {
                Username = usersBindingModel.Username,
                Password = usersBindingModel.Password
            });

            this.context.SaveChanges();

            return this.Ok();
        }




        /*

        [HttpPost("login")]
        public ActionResult Login(string username,string password)
        {
            var userFromDb = this.context.Users.SingleOrDefault(user =>
            user.Username == username && user.Password == password);


            if (userFromDb==null)
            {
                return this.BadRequest("Username or password is invalid.");
            }

            // var tokenHandler =   TODO:

            return this.Ok();
        }
        */

        [HttpPost("login")]
        public ActionResult Login([FromBody] UsersBindingModel usersBindingModel)
        {

            if (usersBindingModel == null)
            {
                return this.BadRequest("Username or password is invalid.");
            }

            // var tokenHandler =   TODO:

            return this.Ok();

        }

        public async Task<ActionResult> GetMe()
        {
            return this.Ok(this.Users.FindFirst(claimTypes.NameIdentifier).value);
        }

    }
}
