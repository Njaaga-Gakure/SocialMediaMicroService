using CommentsService.Models.DTOs;
using CommentsService.Service.IService;
using Newtonsoft.Json;

namespace CommentsService.Service
{
    public class PostService : IPost
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PostService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<PostDTO> GetPostById(Guid postId)
        {
            // creating a client
           var client = _httpClientFactory.CreateClient("Posts");
            // performing a get request
            var response = await client.GetAsync($"{postId}");
            // content as a string
            var content = await response.Content.ReadAsStringAsync();
            // deserialize string to a ResponseDto as the response from the url
            // is a responseDTO not a post
            var responseDTO = JsonConvert.DeserializeObject<ResponseDTO>(content);

            if (response.IsSuccessStatusCode) 
            {
                return JsonConvert.DeserializeObject<PostDTO>(Convert.ToString(responseDTO.Result));
            }
            return null;
        }
    }
}
