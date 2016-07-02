using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Models;

namespace bookmark_manager.Classes
{
    public static class BookmarkManagerHelpers
    {
        // Root node for all nodes used by the Bookmark Manager
        public const string ROOT = "bookmarkManager";

        // Bookmark List
        public const string BOOKMARKS_ROOT = "bookmarks";

        // Bookmark Document Type
        public const string BOOKMARK = "bookmark";

        // Bookmark edit/create form
        public const string BOOKMARK_FORM = "bookmarkForm";

        // Node containing all bookmarks of a member
        public const string BOOKMARKS_MEMBER = "bookmarksMember";

        // Landing page
        public const string LANDING_PAGE = "landingPage";

        // Member Settings Form
        public const string MEMBER_SETTINGS = "memberSettings";

        // Tag Rules Form
        public const string TAG_RULE_FORM = "tagForm";

        // Member ID alias
        public const string MEMBER_ID_ALIAS = "memberId";

        // Tag Rule Alias
        public const string TAG_RULES_ALIAS = "tags";

        // Colors available for tags
        private static string[] _availableColors;
        
        /// <summary>
        /// Returns the id of the first node that has the specified content type alias
        /// </summary>
        /// <param name="contentTypeAlias"></param>
        /// <returns></returns>
        public static int GetFirstNodeIdForContentType(string contentTypeAlias)
        {
            var node = GetFirstNodeForContentType(contentTypeAlias);
            return node.Id;
        }

        /// <summary>
        /// Returns the first node that has the specified content type alias. Creates an empty node with that content type
        /// if none exists.
        /// </summary>
        /// <param name="contentTypeAlias"></param>
        /// <returns></returns>
        public static IContent GetFirstNodeForContentType(string contentTypeAlias)
        {
            var contentService = ApplicationContext.Current.Services.ContentService;
            var rootNodes = contentService.GetRootContent();

            IContent node;

            try
            {
                node = rootNodes.Where(x => x.ContentType.Alias == contentTypeAlias).First();
            }
            catch (Exception)
            {
                node = contentService.CreateContent(contentTypeAlias, -1, contentTypeAlias);
                contentService.Save(node);
            }

            return node;
        }

        /// <summary>
        /// Returns the member node containing all bookmarks.
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public static IContent GetBookmarkNodeForMember(int memberId)
        {
            var bookmarksRoot = GetFirstNodeForContentType(BOOKMARKS_ROOT);
            var memberBookmarkNode = bookmarksRoot.Children().Where(x => x.GetValue("memberId").Equals(memberId)).First();

            return memberBookmarkNode;
        }

        /// <summary>
        /// Returns a list of all bookmarks of the specified member. The list can be filtered to include only the bookmarks that
        /// have the specified tag.
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="tagFilter"></param>
        /// <returns></returns>
        public static IEnumerable<IContent> GetBookmarksForCurrentMember(int memberId, string tagFilter = "")
        {
            var contentService = ApplicationContext.Current.Services.ContentService;
            var bookmarkNode = GetBookmarkNodeForMember(memberId);
            var result = bookmarkNode.Children();

            if (!string.IsNullOrEmpty(tagFilter))
            {
                result = result.Where(x => x.GetValue("tags").ToString().Contains(tagFilter));
            }

            return result;
        }

        /// <summary>
        /// Returns a list of available colors for the labels.
        /// </summary>
        public static string[] AvailableColors
        {
            get
            {
                if (_availableColors == null)
                    _availableColors = new string[] { "grey", "blue", "sky", "green", "yellow", "red" };

                return _availableColors;
            }
        }
    }
}