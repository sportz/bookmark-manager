﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Models;

namespace bookmark_manager.Classes
{
    public static class BookmarkManagerHelpers
    {
        public const string BOOKMARKS_ROOT = "bookmarks";
        public const string BOOKMARK = "bookmark";
        public const string BOOKMARK_FORM = "bookmarkForm";
        public const string BOOKMARKS_MEMBER = "bookmarksMember";
        public const string LANDING_PAGE = "landingPage";
        
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
    }
}