using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SearchServer.Services.TextSearcher;

namespace SearchServer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TextSearchController : ControllerBase
    {

        private readonly ITextSearcher _service;
        private readonly ILogger<TextSearchController> _logger;

        public TextSearchController(ILogger<TextSearchController> logger, ITextSearcher service)
        {
            _logger = logger;
            _service = service;
        }

        //example : https://localhost:44379/TextSearch/juntcion
        [HttpGet("{term}")]
        public async Task<string> GetEntitiesGlobally(string term)
        {
            _logger.LogTrace($"searching for term {term}");
            return await _service.getEntitiesGlobally(term); 
        }


        //example : https://localhost:44379/TextSearch?term=Junction&market=san%20francicso
        [HttpGet]
        public async Task<string> GetEntities(string term, string market)
        {
            _logger.LogTrace($"searching for term {term} inside market {market}");
            return await _service.getEntitiesInsideMarket(term, market);
        }
    }
}
