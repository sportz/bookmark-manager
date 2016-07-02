using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bookmark_manager.Models
{
    public class BookmarkFormModel
    {
        [Required]
        public string title { get; set; }

        [Required]
        [Url]
        public string link { get; set; }

        public string tagsString { get; set; }

        public int id { get; set; }
    }
}