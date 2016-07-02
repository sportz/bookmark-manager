using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace bookmark_manager.Classes
{
    public class umbracoStartup : IApplicationEventHandler
    {
        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            // register EventHandlers registieren
            MemberService.Created += createUserBookmarkNodeAfterRegister;
            MemberService.Created += addMemberToGroup;
            MemberService.Deleting += deleteUserBookmarkContent;
        }

        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {}

        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {}

        /// <summary>
        /// Creates a node for every newly registered user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createUserBookmarkNodeAfterRegister(IMemberService sender, NewEventArgs<IMember> e)
        {
            // Empty Tags
            var tagsContent = "{\r\n  \"tags\": [\r\n  ]\r\n}";

            // User information
            var memberName = e.Entity.Name;
            var memberId = e.Entity.Id;

            var contentService = ApplicationContext.Current.Services.ContentService;

            // Get the root node for the bookmarks
            var bookmarksRoot = BookmarkManagerHelpers.GetFirstNodeForContentType(BookmarkManagerHelpers.BOOKMARKS_ROOT);

            // Create the BookmarksMember Content Node and assign values for memberId and Taglist
            IContent userBookmarksNode = contentService.CreateContent(memberName, bookmarksRoot, BookmarkManagerHelpers.BOOKMARKS_MEMBER, memberId);
            userBookmarksNode.SetValue(BookmarkManagerHelpers.MEMBER_ID_ALIAS, memberId);
            userBookmarksNode.SetValue(BookmarkManagerHelpers.TAG_RULES_ALIAS, tagsContent);
            contentService.Save(userBookmarksNode);
        }

        /// <summary>
        /// Delete all bookmarks of the member that will be deleted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteUserBookmarkContent(IMemberService sender, DeleteEventArgs<IMember> e)
        {
            var contentService = ApplicationContext.Current.Services.ContentService;

            var member = e.DeletedEntities.First();
            int memberId = member.Id;

            var userBookmarks = BookmarkManagerHelpers.GetBookmarkNodeForMember(memberId);
            contentService.Delete(userBookmarks);
        }

        /// <summary>
        /// Add member to BookmarksUser group so he can access the coressponding pages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addMemberToGroup(IMemberService sender, NewEventArgs<IMember> e)
        {
            var bookmarksUserGroupAlias = "BookmarksUser";

            var memberId = e.Entity.Id;
            sender.AssignRole(memberId, bookmarksUserGroupAlias);
        }
    }
}