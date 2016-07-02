using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bookmark_manager.Models
{
    public class BookmarkFormModel
    {
        public string title { get; set; }
        public string link { get; set; }
        public string tagsString { get; set; }

        public int id { get; set; }
    }
}