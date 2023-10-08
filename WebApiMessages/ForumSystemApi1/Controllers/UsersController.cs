using ForumSystemApi1.Data;
using ForumSystemApi1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ForumSystemApi1.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
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

        [HttpPost]
        [Route("register")]
        public ActionResult Register([FromBody] UsersBindingModel usersBindingModel)
        {
            if (usersBindingModel == null)
            {
                throw new ArgumentNullException(nameof(usersBindingModel.Username));
            }

            this.context.Users.Add(new User
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

        [HttpPost]
        [Route("login")]
        public ActionResult Login([FromBody] UsersBindingModel usersBindingModel)
        {

            if (usersBindingModel == null)
            {
                return this.BadRequest("Username or password is invalid.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(this.jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject=new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier,User.Identities.ToString())
                }),
                Expires=DateTime.UtcNow.AddDays(7),
                SigningCredentials=new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                    )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            //return this.Ok();

            return this.Ok(tokenHandler.WriteToken(token));

            

        }

        [HttpGet]
        [Route("allUsers")]
        public ActionResult<IEnumerable<User>> AllUsers()
        {
            return this.context.Users.ToList();

        }



        [HttpGet]
        [Route("getme")]
        public async Task<ActionResult> GetMe()
        {
            return this.Ok(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

    }
}
