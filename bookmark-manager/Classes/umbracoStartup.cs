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

        private void createUserBookmarkNodeAfterRegister(IMemberService sender, NewEventArgs<IMember> e)
        {
            // Type of the subnode to create
            var bookmarksRootAlias = "bookmarks";
            var bookmarksMemberAlias = "bookmarksMember";

            // Property Names
            var memberIdAlias = "memberId";
            var tagsAlias = "tags";


            // Empty Tags
            var tagsContent = "{\r\n  \"tags\": [\r\n  ]\r\n}";
            // User information
            var memberName = e.Entity.Name;
            var memberId = e.Entity.Id;

            var contentService = ApplicationContext.Current.Services.ContentService;

            // Get the root node for the bookmarks
            IEnumerable<IContent> rootNodes = contentService.GetRootContent();
            IContent bookmarksRoot;
            try {
                bookmarksRoot = rootNodes.Where(x => x.ContentType.Alias == bookmarksRootAlias).First();
            }
            catch (System.Exception error){
                bookmarksRoot = contentService.CreateContent("Bookmarks Root", -1, bookmarksRootAlias);
                contentService.SaveAndPublishWithStatus(bookmarksRoot);
            }

            // Create the BookmarksMember Content Node and assign values for memberId and Taglist
            IContent userBookmarksNode = contentService.CreateContent(memberName, bookmarksRoot, bookmarksMemberAlias, memberId);
            userBookmarksNode.SetValue(memberIdAlias, memberId);
            userBookmarksNode.SetValue(tagsAlias, tagsContent);
            contentService.Save(userBookmarksNode);
        }
    }
}