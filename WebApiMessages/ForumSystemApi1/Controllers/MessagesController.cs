namespace ForumSystemApi1.Controllers
{
    using ForumSystemApi1.Data;
    using ForumSystemApi1.Models;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController:ControllerBase
    {

        private readonly ApplicationDbContext context;

        public MessagesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("/all")]
        public async Task<ActionResult<IEnumerable<Message>>> AllOrderedByCreatedOnAscending()
        {
            return this.context.Messages
                .OrderBy(message => message.CreatedOn)
                .ToList();;
        }
        

        [HttpPost]
        [Route("/create")]
        public async Task<ActionResult> Create(MessageCreateBindingModel messageCreateBindingModel)
        {

            var userFromDb = this.context.Users
                .SingleOrDefault(user => user.Username == messageCreateBindingModel.User);


            Message message = new Message
            {
                Content = messageCreateBindingModel.Content,
                User = userFromDb,
                CreatedOn = DateTime.UtcNow
            };

            await this.context.Messages.AddAsync(message);
            await this.context.SaveChangesAsync();

            return this.Ok();
        }

    }
}
