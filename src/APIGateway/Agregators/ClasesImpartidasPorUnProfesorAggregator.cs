using APIGateway.DTOs;
using Newtonsoft.Json;
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using System.Net;
using System.Net.Http.Headers;

namespace APIGateway.Agregators
{
    public class ClasesImpartidasPorUnProfesorAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            var claseResponseContext = await responses[0].Items.DownstreamResponse().Content.ReadAsStringAsync();
            var profesorResponseContext = await responses[1].Items.DownstreamResponse().Content.ReadAsStringAsync();

            var clases = JsonConvert.DeserializeObject<List<Clase>>(claseResponseContext);
            var profesor = JsonConvert.DeserializeObject<Profesor>(profesorResponseContext);

            if (profesor != null)
                profesor.Clases.AddRange(clases ?? new List<Clase>());
            
            var postsByUserString = JsonConvert.SerializeObject(profesor);

            var stringContent = new StringContent(postsByUserString)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            };

            return new DownstreamResponse(stringContent, HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");
        }
    }
}
