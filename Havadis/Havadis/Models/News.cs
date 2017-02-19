using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Havadis.Models
{
    public class News
    {
        public string Title { get; set; }
        public string TextContent { get; set; }
        public string HtmlContent { get; set; }
        public bool HasVideo { get; set; } = false;
        public string VideoUrl { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
