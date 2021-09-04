using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Farsight.Backend.Models;
using Farsight.Backend.Models.DTOs.Listings;
using Farsight.Backend.Models.DTOs.Requests;
using Farsight.Backend.Models.DTOs.Responses;
using Farsight.Backend.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Farsight.Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PostsController(IPostRepository postRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var posts = await _postRepository.GetPosts(new Guid(userId));

            return Ok(_mapper.Map<IList<PostListItem>>(posts));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(Guid id)
        {
            var post = await _postRepository.GetPost(id);

            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(PostCreate postCreate)
        {
            var userId = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var post = _mapper.Map<Post>(postCreate);
            post.AuthorId = userId;

            _postRepository.CreatePost(post);

            await _unitOfWork.SaveChanges();

            return CreatedAtAction(nameof(GetPost), new { Id = post.Id }, _mapper.Map<PostCreated>(post));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(Guid id, PostUpdate postUpdate)
        {
            if (id != postUpdate.Id)
                return BadRequest();

            var userId = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var post = _mapper.Map<Post>(postUpdate);

            _postRepository.UpdatePost(post);

            await _unitOfWork.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(Guid id)
        {
            var post = await _postRepository.GetPost(id);
            if (post == null)
                return NotFound();

            _postRepository.DeletePost(post);

            await _unitOfWork.SaveChanges();

            return NoContent();
        }

        [HttpPost("{postId}")]
        public async Task<IActionResult> CreateReply(Guid postId, PostReplyCreate postReplyCreate)
        {
            var post = await _postRepository.GetPost(postId);
            if (post == null)
                return BadRequest();

            var userId = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var postReply = _mapper.Map<PostReply>(postReplyCreate);
            postReply.AuthorId = userId;
            postReply.PostId = post.Id;

            _postRepository.CreatePostReply(postReply);

            await _unitOfWork.SaveChanges();

            return CreatedAtAction(nameof(GetPost), new { Id = post.Id }, _mapper.Map<PostReplyCreated>(postReply));
        }
    }
}