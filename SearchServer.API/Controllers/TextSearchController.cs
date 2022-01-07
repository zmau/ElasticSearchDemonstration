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

        [HttpGet("{text}")]
        public string GetWordCount(string text)
        {
            return _service.getApartments(text); 
        }
    }
}
