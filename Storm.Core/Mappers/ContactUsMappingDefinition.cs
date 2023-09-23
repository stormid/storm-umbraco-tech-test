using Storm.Core.Migrations;
using Storm.Core.Models.Requests;
using Umbraco.Cms.Core.Mapping;

namespace Storm.Core.Mappers
{
    public class ContactUsMappingDefinition : IMapDefinition
    {
        public void DefineMaps(IUmbracoMapper mapper)
        {
            mapper.Define<ContactModel, ContactUsSchema>((source, target) => new ContactUsSchema(), Map);
        }

        private static void Map(ContactModel source, ContactUsSchema target, MapperContext context)
        {
            target.FirstName = source.FirstName;
            target.LastName = source.LastName;
            target.EmailAddress = source.EmailAddress;
            target.Message = source.Message;
        }
    }
}
