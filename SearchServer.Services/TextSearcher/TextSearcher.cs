using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SearchServer.Services.TextSearcher
{
    public class TextSearcher : ITextSearcher
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiURL;

        public TextSearcher(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiURL = ConfigurationFactory.GetConfiguration().GetValue<string>("elastic:apiUrl");
        }

        public async Task<string> getEntitiesGlobally(string text)
        {
            string scriptTemplate = ScriptRetriever.getJSON("searchGlobally");
            var script = string.Format(scriptTemplate, text);
            var content = new StringContent(script, Encoding.UTF8, "application/json");

            var httpResponse = await _httpClient.PostAsync(_apiURL, content);
            var result = await httpResponse.Content.ReadAsStringAsync();
            return result;
        }
        public async Task<string> getEntitiesInsideMarket(string term, string market)
        {
            string scriptTemplate = ScriptRetriever.getJSON("searchInsideMarket");
            var script = string.Format(scriptTemplate, term, market);
            var content = new StringContent(script, Encoding.UTF8, "application/json");

            var httpResponse = await _httpClient.PostAsync(_apiURL, content);
            var result = await httpResponse.Content.ReadAsStringAsync();
            return result;
        }
    }
}
