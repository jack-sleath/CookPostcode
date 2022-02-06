using System;
using System.Collections.Generic;
using System.Text;

namespace CookPostcode.Models
{
    public class PostcodeResults
    {
        public string Entered{ get; set; }
        public string Cleaned { get; set; }
        public string Matched { get; set; }
        public string DeliveryOption { get; set; }
    }
}
