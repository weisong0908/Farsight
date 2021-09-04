using System;

namespace Farsight.Backend.Models.DTOs.Listings
{
    public class PostListItemReply
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        public Guid AuthorId { get; set; }
    }
}