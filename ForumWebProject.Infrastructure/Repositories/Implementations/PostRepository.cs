﻿using ForumWebProject.Infrastructure.Context;
using ForumWebProject.Infrastructure.Entities;
using ForumWebProject.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ForumWebProject.Infrastructure.Repositories.Implementations
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        private readonly ForumContext _forumContext;
        public PostRepository(ForumContext forumContext) : base(forumContext)
        {
            this._forumContext = forumContext;
        }

        public async Task<IEnumerable<Post>> GetByTopicId(Guid topicId)
        {
            return await _forumContext.Posts
                .Where(p => p.TopicId == topicId).OrderBy(p => p.DatePosted).ToListAsync();
        }
        
        public async Task<IEnumerable<Post>> GetByTopicIdSlice(Guid topicId, int offset, int count)
        {
            return await _forumContext.Posts
                .Where(p => p.TopicId == topicId).OrderBy(p => p.DatePosted).Skip(offset).Take(count).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetByUserId(Guid userId)
        {
            return await _forumContext.Posts
                .Where(p => p.UserIdCreated == userId).ToListAsync();
        }

        public async Task<IEnumerable<Post>> FindByContainingText(string textFilter)
        {
            return await _forumContext.Posts
                .Where(p => p.Text!.Contains(textFilter))
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> FindByDatePeriod(DateTime dateStart, DateTime dateEnd)
        {
            return await _forumContext.Posts.Where(p =>
                    p.DatePosted >= dateStart && dateEnd >= p.DatePosted)
                .ToListAsync();
        }

        public async Task<Post?> GetLastPostAsync(Guid topicId)
        {
            return await _forumContext.Posts.OrderBy(p => p.DatePosted).LastOrDefaultAsync(t => t.TopicId == topicId);
        }
        
        public virtual async Task<int> CountPostsAsync(Guid topicId)
        {
            return await _forumContext.Posts.CountAsync(t => t.TopicId == topicId);
        }
    }
}
