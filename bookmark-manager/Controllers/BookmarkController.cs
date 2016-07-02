using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace bookmark_manager.Controllers
{
    public class BookmarkController : SurfaceController
    {
        public ActionResult Index()
        {
            var bookmarkId = this.ControllerContext.RouteData.Values["id"];
            
            return PartialView("BookmarkFormView");
        }
    }
}