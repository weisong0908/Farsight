using System;
using System.Collections.Generic;

namespace Farsight.Backend.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        public Guid AuthorId { get; set; }
        public IList<PostReply> Replies { get; set; }
    }
}