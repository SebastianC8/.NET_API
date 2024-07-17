using Core.Contracts;
using Newtonsoft.Json;
using Repository.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Implementation
{
    public class PostImplCore : IPostCore
    {
        private HttpClient _httpClient;

        public PostImplCore(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PostDTO>> Get()
        {
            // BaseAddress comes from Program.cs
            var result = await _httpClient.GetAsync(_httpClient.BaseAddress);
            var body = await result.Content.ReadAsStringAsync();
            
            var post = JsonConvert.DeserializeObject<IEnumerable<PostDTO>>(body);

            return post!;
        }
    }
}
