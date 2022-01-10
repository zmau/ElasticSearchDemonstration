using System.Threading.Tasks;

namespace SearchServer.Services.TextSearcher
{
    public interface ITextSearcher
    {
        Task<string> getEntitiesGlobally(string text);
        Task<string> getEntitiesInsideMarket(string term, string market);
    }
}
