using Microsoft.AspNetCore.Mvc;
using Storm.Core.Migrations;
using Umbraco.Cms.Web.BackOffice.Controllers;
using Umbraco.Cms.Web.Common.Controllers;

namespace Storm.Core.Controllers
{
    //~/Umbraco/backoffice/Api/ContactUsApi/GetAll
    public class ContactUsApiController : UmbracoAuthorizedApiController // only allow authorised backoffice requests
    {
        private readonly Umbraco.Cms.Infrastructure.Scoping.IScopeProvider scopeProvider;

        public ContactUsApiController(Umbraco.Cms.Infrastructure.Scoping.IScopeProvider scopeProvider)
        {
            this.scopeProvider = scopeProvider;
        }


        [HttpGet]
        public IEnumerable<ContactUsSchema> GetAll()
        {
            using var scope = scopeProvider.CreateScope();
            var queryResults = scope.Database.Fetch<ContactUsSchema>();
            scope.Complete();
            return queryResults;
        }
    }
}
