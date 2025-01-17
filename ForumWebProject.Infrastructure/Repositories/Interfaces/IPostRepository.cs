﻿using ForumWebProject.Infrastructure.Entities;

namespace ForumWebProject.Infrastructure.Repositories.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> GetByTopicId(Guid topicId);
        Task<IEnumerable<Post>> GetByTopicIdSlice(Guid topicId, int offset, int count);
        Task<IEnumerable<Post>> GetByUserId(Guid userId);
        Task<IEnumerable<Post>> FindByContainingText(string textFilter);
        Task<IEnumerable<Post>> FindByDatePeriod(DateTime dateStart, DateTime dateEnd);
        Task<Post?> GetLastPostAsync(Guid topicId);
        Task<int> CountPostsAsync(Guid topicId);
    }
}
