using System;
using System.Collections.Generic;
using System.Text;

namespace IMedXModels.Input
{
   public class InputRequestData
    {
        public List<string> icdFeedData { get; set; }
        public List<string> icdColumnNames { get; set; }
        public List<string> ndcFeedData { get; set; }
        public List<string> ndcColumnNames { get; set; }
    }
}
