using CookPostcode.Models;
using CookPostcode.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookPostcode.Services
{
    public class PostcodeLookupService : IPostcodeLookupService
    {
        public string[] GetValidDeliveryOptions(string postcode, List<PostcodeDelivery> postCodeDeliveries)
        {
            //This would come from a DB in production, but currently stored in a seperate class.
            var cleanPostcode = RemoveWhiteSpace(postcode);
            var trimmedPostcode = cleanPostcode;
            var matchedPostcode = "All others";

            var message = "";

            while (trimmedPostcode.Length > 0 && message == "")
            {
                var matchingPostCode = postCodeDeliveries.Where(postCodeDelivery => trimmedPostcode.Contains(postCodeDelivery.PostCode)).FirstOrDefault();
                if (matchingPostCode != null)
                {
                    message = matchingPostCode.Delivery;
                    matchedPostcode = matchingPostCode.PostCode;
                }
                trimmedPostcode = trimmedPostcode.Substring(0, trimmedPostcode.Length - 1);
            }

            if (message == "")
            {
                string[] deafultMessage = { postcode, cleanPostcode, matchedPostcode, "Delivery by Courier" };
                return deafultMessage;
            }

            string[] customMessage = { postcode, cleanPostcode, matchedPostcode, message };
            return customMessage;
        }

        private string RemoveWhiteSpace(string input)
        {
            return new string(input.Where(c => !char.IsWhiteSpace(c))
                .ToArray()).ToUpper();
        }
    }
}
