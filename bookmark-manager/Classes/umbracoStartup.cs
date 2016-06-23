using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Services;

namespace bookmark_manager.Classes {
    public class umbracoStartup : IApplicationEventHandler {

        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext) {
            //throw new NotImplementedException();

            // Hier EventHandler registieren
            //ContentTypeService.SavedContentType += createUserBookmarkNodeAfterRegister;

        }

        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext) {
            //throw new NotImplementedException();

        }

        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext) {
            //throw new NotImplementedException();

        }

        private void createUserBookmarkNodeAfterRegister() {

        }
    }
}