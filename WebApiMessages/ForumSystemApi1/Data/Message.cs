namespace ForumSystemApi1.Data
{
    using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
    using System;

    public class Message
    {
        public Message()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }

        public string? Content { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public DateTime CreatedOn { get; set; }

    }
}
