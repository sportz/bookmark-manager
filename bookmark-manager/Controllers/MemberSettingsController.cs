using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace bookmark_manager.Controllers
{
    public class MemberSettingsController : SurfaceController
    {
        /// <summary>
        /// Delete the member.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult HandleMemberDelete()
        {
            var memberService = ApplicationContext.Services.MemberService;
            var memberId = Members.GetCurrentMemberId();
            var currentMember = memberService.GetById(memberId);

            Members.Logout();

            memberService.Delete(currentMember);

            return Redirect("/");
        }
    }
}