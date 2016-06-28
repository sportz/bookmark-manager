using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace bookmark_manager.Classes {
    public class umbracoStartup : IApplicationEventHandler {

        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext) {
            //throw new NotImplementedException();

            // EventHandler registieren
            MemberService.Created += createUserBookmarkNodeAfterRegister;

            // TODO Eventhandler wenn Member gelöscht wird
           // MemberService.Deleted += deleteUserBookmarkContent;
        }



        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext) {
            //throw new NotImplementedException();

        }

        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext) {
            //throw new NotImplementedException();

        }


        private void createUserBookmarkNodeAfterRegister(IMemberService sender, NewEventArgs<IMember> e) {
            // Type of the subnode to create
            var contentTypeAlias = "userBookmark";

            // User information
            var memberName = e.Entity.Name;
            var memberId = e.Entity.Id;

            var contentService = ApplicationContext.Current.Services.ContentService;

            // Get the root node for the bookmarks
            IContent bookmarksRoot = null;
            IEnumerable<IContent> rootNodes = contentService.GetRootContent();
            foreach (IContent node in rootNodes) {
                if(node.ContentType.Alias == "bookmarks") {
                    bookmarksRoot = node;
                    break;
                }
            }

            // TODO besser Fehler einbauen, wenn nicht vorhanden
            if(bookmarksRoot == null) {
                bookmarksRoot = contentService.CreateContent("All Bookmarks", -1, "bookmarks");
                contentService.Save(bookmarksRoot);
            }

            IContent userBookmarksNode = contentService.CreateContent(memberName, bookmarksRoot, contentTypeAlias, memberId);
            userBookmarksNode.SetValue("memberId", memberId);
            contentService.Save(userBookmarksNode);

        }

    }


}