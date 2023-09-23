using Microsoft.Extensions.Logging;
using Umbraco.Cms.Infrastructure.Migrations;

namespace Storm.Core.Migrations
{
    public class AddContactUsTable : MigrationBase
    {
        public AddContactUsTable(IMigrationContext context) : base(context)
        {
        }

        protected override void Migrate()
        {
            Logger.LogDebug("Running migration {MigrationStep}", "AddContactUsMessagesTable");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (TableExists("ContactUsMessages") == false)
            {
                Create.Table<ContactUsSchema>().Do();
            }
            else
            {
                Logger.LogDebug("The database table {DbTable} already exists, skipping", "ContactUsMessages");
            }
        }
    }
}