using bookmark_manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using bookmark_manager.Classes;

namespace bookmark_manager.Controllers
{
    public class BookmarkController : SurfaceController
    {
        [HttpPost]
        public ActionResult HandleFormSubmit(BookmarkFormModel model)
        {
            var contentService = ApplicationContext.Services.ContentService;
            
            IContent bookmark;

            if (model.id == 0)
            {
                var bookmarkNode = BookmarkManagerHelpers.GetBookmarkNodeForMember(Members.GetCurrentMemberId());
                bookmark = contentService.CreateContent(model.title, bookmarkNode, BookmarkManagerHelpers.BOOKMARK);
            }
            else
            {
                bookmark = contentService.GetById(model.id);
            }

            bookmark.SetValue("title", model.title);
            bookmark.SetValue("link", model.link);
            bookmark.SetValue("tags", model.tagsString);

            contentService.Save(bookmark);

            return new RedirectToUmbracoPageResult(BookmarkManagerHelpers.GetIdOfFirstRootContentNode(BookmarkManagerHelpers.BOOKMARKS_ROOT));
        }
    }
}