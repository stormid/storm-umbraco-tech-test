using Org.BouncyCastle.Ocsp;
using StackExchange.Profiling.Data;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.WebAssets;

namespace Storm.Core.Bundling
{
    public class CreateBundlesNotificationHandler : INotificationHandler<UmbracoApplicationStartingNotification>
    {
        private readonly IRuntimeMinifier _runtimeMinifier;
        private readonly IRuntimeState _runtimeState;

        public CreateBundlesNotificationHandler(IRuntimeMinifier runtimeMinifier, IRuntimeState runtimeState)
        {
            _runtimeMinifier = runtimeMinifier;
            _runtimeState = runtimeState;
        }
        public void Handle(UmbracoApplicationStartingNotification notification)
        {
            if (_runtimeState.Level == RuntimeLevel.Run)
            {
                // Javascript files
                _runtimeMinifier.CreateJsBundle("registered-js-bundle",
                    BundlingOptions.NotOptimizedAndComposite,
                    new[] {
                        "~/assets/js/jquery.min.js",
                       // "~/assets/js/config.js",
                        "~/assets/js/skel.min.js",
                        "~/assets/js/skel-panels.min.js"
                    });

                _runtimeMinifier.CreateJsBundle("registered-js-bundle-lt-ie8",
                    BundlingOptions.NotOptimizedAndComposite,
                    new[] {
                        "~/assets/js/html5shiv.js"
                    });


                // CSS files
                _runtimeMinifier.CreateCssBundle("registered-css-bundle",
                    BundlingOptions.NotOptimizedAndComposite,
                    new[] {
                        "~/assets/css/skel-noscript.css",
                        "~/assets/css/style.css",
                        "~/assets/css/style-desktop.css",
                        "~/assets/css/bootstrap.css"
                    });

                _runtimeMinifier.CreateCssBundle("registered-css-bundle-lte-ie9",
                    BundlingOptions.NotOptimizedAndComposite,
                    new[] {
                        "~/assets/css/ie9.css"
                    });

                _runtimeMinifier.CreateCssBundle("registered-css-bundle-lte-ie8",
                    BundlingOptions.NotOptimizedAndComposite,
                    new[] {
                        "~/assets/css/ie8.css"
                    });

                _runtimeMinifier.CreateCssBundle("registered-css-bundle-lte-ie7",
                    BundlingOptions.NotOptimizedAndComposite,
                    new[] {
                        "~/assets/css/ie7.css"
                    });
            }
        }
    }
}
