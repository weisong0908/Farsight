using System;
using System.Collections.Generic;

namespace Farsight.Backend.Models.DTOs.Listings
{
    public class PostListItem
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        public Guid AuthorId { get; set; }
        public IList<PostListItemReply> Replies { get; set; }
    }
}