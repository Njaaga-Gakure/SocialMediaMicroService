using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostsService.Models;
using PostsService.Models.DTOs;
using PostsService.Service.IService;
using System.Security.Claims;

namespace PostsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPost _postService;
        private readonly IComment _commentService;
        private readonly ResponseDTO _response;
        public PostsController(IMapper mapper, IPost postService, IComment commentService)
        {
            _mapper = mapper;
            _postService = postService;
            _response = new ResponseDTO();
            _commentService = commentService;   
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> AddPost(AddPostDTO post)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    _response.ErrorMessage = "You are not authorized";
                    return StatusCode(403, _response);
                }
                var newPost = _mapper.Map<Post>(post);
                newPost.UserId = new Guid(userId);
                var response = await _postService.CreatePost(newPost);
                _response.Result = response; 
                return Created($"api/Posts/{newPost.Id}", _response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, _response);
            }

        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetAllPosts()
        {
            try
            {
                var posts = await _postService.GetAllPosts();
                _response.Result = posts;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.ErrorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, _response);
            }

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO>> GetPostById(Guid id)
        {

            try
            {
                var post = await _postService.GetPostById(id);
                if (post == null)
                {
                    _response.ErrorMessage = "Post Does not exists :(";
                    return NotFound(_response);
                }
                var comments = await _commentService.GetCommentsOfPost(id);
                var postDTO = new PostResponseDTO()
                {
                   PostId = post.Id,
                   UserId = post.UserId,    
                   Title = post.Title,  
                   Body = post.Body,    
                   Comments = comments
                };
                _response.Result = postDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, _response);
            }

        }

        [HttpPatch("{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> UpdatePost(Guid id, AddPostDTO postUpdate)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    _response.ErrorMessage = "You are not authorized";
                    return StatusCode(403, _response);
                }
                var post = await _postService.GetPostById(id);
                if (post == null)
                {
                    _response.ErrorMessage = "Post Does not exist :(";
                    return NotFound(_response);
                }
                if (post.UserId != new Guid(userId))
                {
                    _response.ErrorMessage = "You are not authorized to perform this operation";
                    return StatusCode(403, _response);
                }
                await _postService.UpdatePost(id, postUpdate);
                _response.Result = "Post Updated Successfully :)";
                return Ok(_response);    

            }
            catch (Exception ex)
            {
                _response.ErrorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, _response);
            }
             
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> DeletePost(Guid id)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    _response.ErrorMessage = "You are not authorized";
                    return StatusCode(403, _response);
                }
                var post = await _postService.GetPostById(id);
                if (post == null)
                {
                    _response.ErrorMessage = "Post Does not exist :(";
                    return NotFound(_response);
                }
                if (post.UserId != new Guid(userId))
                {
                    _response.ErrorMessage = "You are not authorized to perform this operation";
                    return StatusCode(403, _response);
                }
                await _postService.DeletePost(id);
                _response.Result = "Post Deleted Successfully :)";
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.ErrorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, _response);
            }
        }

    }
}
