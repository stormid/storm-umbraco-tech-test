using Storm.Core.Constants.BackOffice;
using Umbraco.Cms.Core.Sections;

namespace Storm.BackOffice.Extensions
{
    public class MessagesSection : ISection
    {
        public string Alias => Constants.SectionAlias;

        public string Name => Constants.SectionName;
    }
}
