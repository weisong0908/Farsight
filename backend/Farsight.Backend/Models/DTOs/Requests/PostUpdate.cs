using System;

namespace Farsight.Backend.Models.DTOs.Requests
{
    public class PostUpdate
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
    }
}