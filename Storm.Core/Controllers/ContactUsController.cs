using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Profiling.Internal;
using Storm.Core.Migrations;
using Storm.Core.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Infrastructure.Scoping;
using Umbraco.Cms.Web.Common.Filters;
using Umbraco.Cms.Web.Website.Controllers;

namespace Storm.Core.Controllers
{
    public class ContactUsController : SurfaceController
    {
        private readonly ILogger<ContactUsController> logger;
        private readonly IUmbracoMapper mapper;
        private readonly Umbraco.Cms.Infrastructure.Scoping.IScopeProvider scopeProvider;
        private readonly IUmbracoContextAccessor umbracoContextAccessor;
        private readonly IUmbracoDatabaseFactory databaseFactory;
        private readonly ServiceContext services;

        public ContactUsController(ILogger<ContactUsController> logger, IUmbracoMapper mapper, Umbraco.Cms.Infrastructure.Scoping.IScopeProvider scopeProvider, IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.scopeProvider = scopeProvider;
            this.umbracoContextAccessor = umbracoContextAccessor;
            this.databaseFactory = databaseFactory;
            this.services = services;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateUmbracoFormRouteString]    // only allow requests from the backoofice
        public IActionResult SubmitForm(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                SaveMessage(model);
                if (model.RedirectToPageId.HasValue)
                {
                    var newPage = UmbracoContext?.Content?.GetById(model.RedirectToPageId.Value);
                    if(newPage != null)
                    {
                        return Redirect(newPage.Url());
                    }
                    else
                    {
                        TempData["ContactSuccess"] = true;
                        return RedirectToCurrentUmbracoPage();
                    }
                }
                else
                {
                    TempData["ContactSuccess"] = true;
                    return RedirectToCurrentUmbracoPage();
                }
            }
            return CurrentUmbracoPage();
        }

        private void SaveMessage(ContactModel model)
        {
            if (model == null) return;
            try
            {
                using var scope = scopeProvider.CreateScope();
                var message = mapper.Map<ContactUsSchema>(model);
                if(message != null)
                {
                    _ = scope.Database.Insert(message);
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(new EventId(), ex, ex.Message);
            }
        }
    }
}