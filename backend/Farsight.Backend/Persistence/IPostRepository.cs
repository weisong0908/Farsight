using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Farsight.Backend.Models;

namespace Farsight.Backend.Persistence
{
    public interface IPostRepository
    {
        Task<IList<Post>> GetPosts(Guid authorId);
        Task<Post> GetPost(Guid postId);
        void CreatePost(Post post);
        void UpdatePost(Post post);
        void DeletePost(Post post);
        void DeletePostReply(PostReply postReply);
        Task<PostReply> GetPostReply(Guid postReplyId);
        void CreatePostReply(PostReply postReply);
    }
}