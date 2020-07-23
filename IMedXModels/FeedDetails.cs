using System;
using System.Collections.Generic;
using System.Text;

namespace IMedXModels
{
    public class FeedDetails
    {
        public string FeedName { get; set; }
        public List<string> FeedDataLines { get; set; }
        public List<string> FeedColumnNames { get; set; }
    }
}
