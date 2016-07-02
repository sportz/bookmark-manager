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
        public const string BOOKMARKS_ROOT = "memberList";
        public const string BOOKMARK = "bookmark";
        public const string BOOKMARK_FORM = "bookmarkForm";
        public const string BOOKMARKS_MEMBER = "memberNode";
//        public const string LANDING_PAGE = "landingPage";
        public const string ROOT = "bookmarkManager";
        public const string MEMBER_SETTINGS = "memberSettings";
        public const string TAG_RULE_FORM = "tagRuleForm";

        public const string MEMBER_ID_ALIAS = "memberId";
        public const string TAG_ALIAS = "tags";

        private static string[] _availableColors;
        
        public static int GetIdOfFirstRootContentNode(string contentTypeAlias)
        {
            var node = GetFirstRootContentNode(contentTypeAlias);
            return node.Id;
        }

        public static IContent GetFirstRootContentNode(string contentTypeAlias)
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

        public static IContent GetBookmarkNodeForMember(int memberId)
        {
            var bookmarksRoot = GetFirstRootContentNode(BOOKMARKS_ROOT);
            var memberBookmarkNode = bookmarksRoot.Children().Where(x => x.GetValue("memberId").Equals(memberId)).First();

            return memberBookmarkNode;
        }

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