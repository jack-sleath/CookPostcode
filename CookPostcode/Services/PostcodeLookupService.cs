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
        private IPostcodeCleanupService _postcodeCleanupService;
        private string defaultMessage = "Delivery by Courier";

        public PostcodeLookupService(IPostcodeCleanupService postcodeCleanupService)
        {
            _postcodeCleanupService = postcodeCleanupService;
        }

        public PostcodeResults GetValidDeliveryOptions(string postcode, List<PostcodeDelivery> postCodeDeliveries)
        {
            var cleanPostcode = _postcodeCleanupService.CleanPostcode(postcode);
            var trimmedPostcode = cleanPostcode;
            var matchedPostcode = "All others";

            var message = defaultMessage;

            while (trimmedPostcode.Length > 0 && message == defaultMessage)
            {
                var matchingPostCode = postCodeDeliveries.Where(postCodeDelivery => trimmedPostcode.Contains(postCodeDelivery.PostCode)).FirstOrDefault();
                if (matchingPostCode != null)
                {
                    message = matchingPostCode.Delivery;
                    matchedPostcode = matchingPostCode.PostCode;
                }
                trimmedPostcode = trimmedPostcode.Substring(0, trimmedPostcode.Length - 1);
            }

            return new PostcodeResults { Entered = postcode, Cleaned = cleanPostcode, Matched = matchedPostcode, DeliveryOption = message }; ;
        }
    }
}
