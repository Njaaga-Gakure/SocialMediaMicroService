using AutoMapper;
using CommentsService.Models;
using CommentsService.Models.DTOs;
using CommentsService.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CommentsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IComment _commentService;
        private readonly IPost _postService;
        private readonly ResponseDTO _response;

        public CommentsController(IMapper mapper, IComment commentService, IPost postService)
        {
            _mapper = mapper;
            _commentService = commentService;
            _response = new ResponseDTO();
            _postService = postService; 

        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddComment(AddCommentDTO comment)
        {
            try
            {
                var name = User.Claims.FirstOrDefault(claim => claim.Type == "name")?.Value;
                var email = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
                if (name == null || email == null)
                {
                    _response.ErrorMessage = "You are not authorized";
                    return StatusCode(403, _response);
                }

                // check if the post you are trying to add a comment to exists
                var post = await _postService.GetPostById(comment.PostId);
               if (post == null) 
                {
                    _response.ErrorMessage = "Post not Found";
                    return BadRequest(_response);
                }

                var newComment = _mapper.Map<Comment>(comment);
                newComment.UserName = name;
                newComment.UserEmail = email;

                var response = await _commentService.CreateComment(newComment);
                _response.Result = response;
                return Created($"api/Comments/{newComment.Id}", _response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, _response);
            }
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetAllCommentsOfPost(Guid postId)
        {
            try
            {
                var comments = await _commentService.GetAllComments(postId);
                _response.Result = comments;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.ErrorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, _response);
            }
        }
        [HttpGet("{commentId}")]
        public async Task<ActionResult<ResponseDTO>> GetSingleComment(Guid commentId)
        {
            try
            {
                var comment = await _commentService.GetCommentById(commentId);
                if (comment == null)
                {
                    _response.ErrorMessage = "Comment Not Found :(";
                    return NotFound(_response);
                }
                _response.Result = comment;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.ErrorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, _response);
            }
        }

        [HttpPatch("{commentId}")]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> UpdateComment(Guid commentId, AddCommentDTO commentUpdate)
        {

            try
            {
                var email = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
                if (email == null)
                {
                    _response.ErrorMessage = "You are not authorized";
                    return StatusCode(403, _response);
                }
                var comment = await _commentService.GetCommentById(commentId);
                if (comment == null)
                {
                    _response.ErrorMessage = "Comment Not Found :(";
                    return NotFound(_response);
                }
                if (comment.UserEmail != email)
                {
                    _response.ErrorMessage = "You are not authorized";
                    return StatusCode(403, _response);
                }
                await _commentService.UpdateComment(commentId, commentUpdate);
                _response.Result = "Comment Updated Successfully :)";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, _response);
            }
        }

        [HttpDelete("{commentId}")]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> DeleteComment(Guid commentId)
        {

            try
            {
                var email = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
                if (email == null)
                {
                    _response.ErrorMessage = "You are not authorized";
                    return StatusCode(403, _response);
                }
                var comment = await _commentService.GetCommentById(commentId);
                if (comment == null)
                {
                    _response.ErrorMessage = "Comment Not Found :(";
                    return NotFound(_response);
                }
                if (comment.UserEmail != email)
                {
                    _response.ErrorMessage = "You are not authorized";
                    return StatusCode(403, _response);
                }
                await _commentService.DeleteComment(commentId);
                _response.Result = "Comment Delete Successfully :)";
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
