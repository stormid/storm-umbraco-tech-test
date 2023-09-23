using NPoco;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace Storm.Core.Migrations
{
    [TableName("ContactUsMessages")]
    [PrimaryKey("Id", AutoIncrement = true)]
    [ExplicitColumns]
    public class ContactUsSchema
    {
        public ContactUsSchema()
        {

        }

        [PrimaryKeyColumn(AutoIncrement = true, IdentitySeed = 1)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("FirstName")]
        public  string FirstName { get; set; } = "";

        [Column("LastName")]
        public  string LastName { get; set; } = "";

        [Column("EmailAddress")]
        public  string EmailAddress { get; set; } = "";


        [Column("Message")]
        [SpecialDbType(SpecialDbTypes.NVARCHARMAX)]
        public string Message { get; set; } = "";
    }
}