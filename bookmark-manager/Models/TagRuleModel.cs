using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bookmark_manager.Models
{
    public class TagRuleModel
    {
        [Required]
        public string title { get; set; }

        [Required]
        public string color { get; set; }
    }
}