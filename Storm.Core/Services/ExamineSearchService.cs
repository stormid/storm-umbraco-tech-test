using Examine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Infrastructure;
using Umbraco.Cms.Infrastructure.Examine;

namespace Storm.Core.Services
{
    public class ExamineSearchService : IExamineSearchService
    {
        private readonly IPublishedContentQuery publishedContentQuery;
        private readonly IExamineManager examineManager;

        public ExamineSearchService(IPublishedContentQuery publishedContentQuery, IExamineManager examineManager)
        {
            this.publishedContentQuery = publishedContentQuery;
            this.examineManager = examineManager;
        }

        public IEnumerable<T> Search<T>(string searchTerm, int page = 0, int pageSize = 10) where T: class
        {
            //I could've created a custom Examine index for this, but I'm reluctant to do that (let's discuss why)
            var docTypeName = typeof(T).Name;

            if (!examineManager.TryGetIndex(Umbraco.Cms.Core.Constants.UmbracoIndexes.ExternalIndexName, out IIndex index))
            {
                throw new InvalidOperationException($"No index found by name{Umbraco.Cms.Core.Constants.UmbracoIndexes.ExternalIndexName}");
            }

            var query = index.Searcher.CreateQuery(IndexTypes.Content);
            var queryExecutor = query.NodeTypeAlias(docTypeName).And().ManagedQuery(searchTerm);

            foreach (var result in publishedContentQuery.Search(queryExecutor, page, pageSize, out _))
            {
                yield return result.Content as T;
            }
        }
    }
}
