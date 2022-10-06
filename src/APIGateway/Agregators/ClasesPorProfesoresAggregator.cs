using Newtonsoft.Json;
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using System.Net;
using System.Net.Http.Headers;

namespace APIGateway.Agregators
{
    public class ClasesPorProfesoresAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            var userResponseContext = await responses[0].Items.DownstreamResponse().Content.ReadAsStringAsync();
            var postResponseContext = await responses[1].Items.DownstreamResponse().Content.ReadAsStringAsync();

            var users = JsonConvert.DeserializeObject<List<User>>(userResponseContext);
            var posts = JsonConvert.DeserializeObject<List<Post>>(postResponseContext);

            foreach (var user in users)
            {
                var userPost = posts.Where(p => p.IdUser == user.Id).ToList();
                user.Posts.AddRange(userPost);
            }

            var postsByUserString = JsonConvert.SerializeObject(users);

            var stringContent = new StringContent(postsByUserString)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            };

            return new DownstreamResponse(stringContent, HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");
        }
    }
}
