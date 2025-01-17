﻿using ForumWebProject.Infrastructure.Identity;

namespace ForumWebProject.Infrastructure.Entities
{
    public class Post : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string? Text { get; set; }
        public DateTime DatePosted { get; set; }

        public Guid UserIdCreated { get; set; }
        public virtual User User { get; set; }

        public Guid? TopicId { get; set; }
        public virtual Topic? Topic { get; set; }
        
        public Guid? ReplyToPostId { get; set; }
        public virtual Post? ReplyToPost { get; set; }
        
        public virtual ICollection<Post>? Replies { get; set; }
    }
}
