using System;

namespace Farsight.Backend.Models
{
    public class PostReply
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        public Guid AuthorId { get; set; }
        public Post Post { get; set; }
        public Guid PostId { get; set; }
    }
}