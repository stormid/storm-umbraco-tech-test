using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Storm.Core.Constants.BackOffice;
using Storm.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Actions;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models.Trees;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Trees;
using Umbraco.Cms.Web.BackOffice.Trees;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Cms.Web.Common.ModelBinders;

namespace Storm.BackOffice.Extensions
{
    [Tree(Constants.SectionAlias, Constants.TreeAlias, TreeTitle = Constants.TreeTitle, TreeGroup = Constants.TreeGroup, SortOrder = -1)]
    [PluginController("messages")]
    public class MessagesTreeController : TreeController
    {
        private readonly IMenuItemCollectionFactory menuItemCollectionFactory;
        
        public MessagesTreeController(ILocalizedTextService localizedTextService, 
            Umbraco.Cms.Core.UmbracoApiControllerTypeCollection umbracoApiControllerTypeCollection,
            IMenuItemCollectionFactory menuItemCollectionFactory,
            IEventAggregator eventAggregator,
            AppCaches appCaches) : base(localizedTextService, umbracoApiControllerTypeCollection, eventAggregator)
        {
            this.menuItemCollectionFactory = menuItemCollectionFactory ?? throw new ArgumentNullException(nameof(menuItemCollectionFactory));
            ;
        }


        protected override ActionResult<TreeNodeCollection> GetTreeNodes(string id, [ModelBinder(typeof(HttpQueryStringModelBinder))] FormCollection queryStrings)
        {
            var treeNodes = new TreeNodeCollection();

            if (id == Umbraco.Cms.Core.Constants.System.Root.ToInvariantString())
            {
                // you can get your custom nodes from anywhere, and they can represent anything...
                Dictionary<int, string> nodes = new Dictionary<int, string>();
                nodes.Add(1, "Contact Requests");

                // loop through our favourite things and create a tree item for each one
                foreach (var item in nodes)
                {
                    // add each node to the tree collection using the base CreateTreeNode method
                    // it has several overloads, using here unique Id of tree item,
                    // -1 is the Id of the parent node to create, eg the root of this tree is -1 by convention
                    // - the querystring collection passed into this route
                    // - the name of the tree node
                    // - css class of icon to display for the node
                    // - and whether the item has child nodes
                    var node = CreateTreeNode(item.Key.ToString(), "-1", queryStrings, item.Value, "icon-mailbox", false);
                    treeNodes.Add(node);
                }
            }


            return treeNodes;
        }

        protected override ActionResult<MenuItemCollection> GetMenuForNode(string id, [ModelBinder(typeof(HttpQueryStringModelBinder))] FormCollection queryStrings)
        {
            // create a Menu Item Collection to return so people can interact with the nodes in your tree
            var menu = menuItemCollectionFactory.Create();

            if (id == Umbraco.Cms.Core.Constants.System.Root.ToInvariantString())
            {
                // root actions, perhaps users can create new items in this tree, or perhaps it's not a content tree, it might be a read only tree, or each node item might represent something entirely different...
                // add your menu item actions or custom ActionMenuItems
                menu.Items.Add(new CreateChildEntity(LocalizedTextService));
                // add refresh menu item (note no dialog)
                menu.Items.Add(new RefreshNode(LocalizedTextService, true));
            }
            else
            {
                // add a delete action to each individual item
                menu.Items.Add<ActionDelete>(LocalizedTextService, true, opensDialog: true);
            }

            return menu;
        }

        protected override ActionResult<TreeNode?> CreateRootNode(FormCollection queryStrings)
        {
            var rootResult = base.CreateRootNode(queryStrings);
            if (!(rootResult.Result is null))
            {
                return rootResult;
            }

            var root = rootResult.Value;

            // set the icon
            root.Icon = "icon-hearts";
            // could be set to false for a custom tree with a single node.
            root.HasChildren = true;
            //url for menu
            root.MenuUrl = null;

            return root;
        }
    }
}
