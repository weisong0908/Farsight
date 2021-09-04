using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farsight.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Farsight.Backend.Persistence
{
    public class PostRepository : IPostRepository
    {
        private readonly FarsightBackendDbContext _dbContext;

        public PostRepository(FarsightBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<Post>> GetPosts(Guid authorId)
        {
            return await _dbContext.Posts
                .Where(p => p.AuthorId == authorId)
                .Include(p => p.Replies.OrderBy(r => r.DateTime))
                .OrderByDescending(p => p.DateTime)
                .ToListAsync();
        }

        public async Task<Post> GetPost(Guid postId)
        {
            return await _dbContext.Posts
                .Include(p => p.Replies.OrderBy(r => r.DateTime))
                .SingleOrDefaultAsync(p => p.Id == postId);
        }

        public void CreatePost(Post post)
        {
            _dbContext.Add<Post>(post);
        }

        public void UpdatePost(Post post)
        {
            _dbContext.Entry<Post>(post).State = EntityState.Modified;
        }

        public void DeletePost(Post post)
        {
            _dbContext.Remove<Post>(post);
        }

        public void CreatePostReply(PostReply postReply)
        {
            _dbContext.Add<PostReply>(postReply);
        }
    }
}