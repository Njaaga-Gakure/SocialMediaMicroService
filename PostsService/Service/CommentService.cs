using Newtonsoft.Json;
using PostsService.Models.DTOs;
using PostsService.Service.IService;

namespace PostsService.Service
{
    public class CommentService : IComment
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CommentService(IHttpClientFactory clientFactory)
        {
           _httpClientFactory = clientFactory;
        }
        public async Task<List<CommentDTO>> GetCommentsOfPost(Guid postId)
        {
            var client = _httpClientFactory.CreateClient("Comments");
            var response = await client.GetAsync($"?postId={postId}");
            var content = await response.Content.ReadAsStringAsync();
            var responseDTO = JsonConvert.DeserializeObject<ResponseDTO>(content);

            if (response.IsSuccessStatusCode)
            {
                var comments = JsonConvert.DeserializeObject<List<CommentDTO>>(Convert.ToString(responseDTO.Result));
                return comments;
            }
            return new List<CommentDTO>();
        }
    }
}
