using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bookmark_manager.Models
{
    public class BookmarkFormViewModel
    {
        public string title { get; set; }
        public string url { get; set; }

        // TODO
        public List<string> tags { get; set; }
    }
}