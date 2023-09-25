using Umbraco.Cms.Core.Models.PublishedContent;

namespace Storm.Core.Services
{
    public interface IExamineSearchService
    {
        IEnumerable<T> Search<T>(string searchTerm, int page = 0, int pageSize = 10) where T : class;
    }
}