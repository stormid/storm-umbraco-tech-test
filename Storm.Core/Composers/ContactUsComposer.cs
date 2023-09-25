using Storm.Core.Mappers;
using Storm.Core.Migrations;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Core.Notifications;

namespace Storm.Core.Composers
{
    public class ContactUsComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.AddNotificationHandler<UmbracoApplicationStartingNotification, RunContactUsMigration>();
            builder.WithCollectionBuilder<MapDefinitionCollectionBuilder>().Add<ContactUsMappingDefinition>();
        }
    }
}
