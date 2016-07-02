using bookmark_manager.Classes;
using bookmark_manager.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace bookmark_manager.Controllers
{
    public class TagRuleController : SurfaceController
    {
        /// <summary>
        /// Create a new rule to color tags based on the model. Existing rules for the same tag will be overwritten.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult HandleFormSubmit(TagRuleModel model)
        {
            // Cancel if model is invalid
            if (!ModelState.IsValid)
                return CurrentUmbracoPage();

            // Get member node and tag rules
            var memberNode = BookmarkManagerHelpers.GetBookmarkNodeForMember(Members.GetCurrentMemberId());
            var ruleSet = getRuleSetFromUser(memberNode);
            var rules = ruleSet.Tables["tags"];

            // Replace existing rules (if any)
            var done = false;
            for (var i = 0; i < rules.Rows.Count && !done; i++)
            {
                if (rules.Rows[i]["title"].Equals(model.title))
                {
                    rules.Rows[i]["color"] = model.color;
                    done = true;
                }
            }

            // Create new if no rule was replaced
            if (!done)
            {
                DataRow newRule = rules.NewRow();
                newRule["title"] = model.title;
                newRule["color"] = model.color;

                rules.Rows.Add(newRule);
            }

            rules.AcceptChanges();
            ruleSet.AcceptChanges();

            // Serialize back to JSON string
            var result = JsonConvert.SerializeObject(ruleSet);

            // Save
            memberNode.SetValue(BookmarkManagerHelpers.TAG_RULES_ALIAS, result);
            ApplicationContext.Services.ContentService.Save(memberNode);

            return CurrentUmbracoPage();
        }

        /// <summary>
        /// Delete tag color rule.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult HandleDeleteTag()
        {
            // Get member node and tag rules
            var memberNode = BookmarkManagerHelpers.GetBookmarkNodeForMember(Members.GetCurrentMemberId());
            var ruleSet = getRuleSetFromUser(memberNode);
            var rules = ruleSet.Tables["tags"];

            // Find and delete rule
            var tag = ControllerContext.RequestContext.RouteData.Values["tagTitle"].ToString();

            var done = false;
            for (var i = 0; i < rules.Rows.Count && !done; i++)
            {
                if (rules.Rows[i]["title"].Equals(tag))
                {
                    rules.Rows.RemoveAt(i);
                }
            }

            rules.AcceptChanges();
            ruleSet.AcceptChanges();

            // Serialize back to JSON string
            var result = JsonConvert.SerializeObject(ruleSet);

            // Save
            memberNode.SetValue(BookmarkManagerHelpers.TAG_RULES_ALIAS, result);
            ApplicationContext.Services.ContentService.Save(memberNode);

            return new RedirectToUmbracoPageResult(BookmarkManagerHelpers.GetFirstNodeIdForContentType(BookmarkManagerHelpers.TAG_RULE_FORM));
        }

        /// <summary>
        /// Helper method returning all tag coloring rules stored for the current member.
        /// </summary>
        /// <param name="userNode"></param>
        /// <returns></returns>
        private DataSet getRuleSetFromUser(IContent userNode)
        {
            var json = userNode.GetValue<string>(BookmarkManagerHelpers.TAG_RULES_ALIAS).Replace("&quot;", "\"");

            DataSet ruleSet = JsonConvert.DeserializeObject<DataSet>(json);
            DataTable rules = ruleSet.Tables["tags"];

            // Initialize table if it does not exist
            if (rules.Rows.Count == 0)
            {
                DataColumn titleColumn = new DataColumn("title");
                DataColumn colorColumn = new DataColumn("color");

                rules.Columns.Add(titleColumn);
                rules.Columns.Add(colorColumn);
            }

            return ruleSet;
        }
    }
}