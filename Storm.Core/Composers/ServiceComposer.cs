using Microsoft.Extensions.DependencyInjection;
using Storm.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Composing;

namespace Storm.Core.Composers
{
    public class ServiceComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddTransient<IExamineSearchService, ExamineSearchService>();
        }
    }
}
