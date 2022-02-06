using CookPostcode.Models;
using CookPostcode.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookPostcode.Services
{
    public class PostcodeLookup : IPostcodeLookup
    {
        private IPostcodeCleanupService _postcodeCleanupService;
        private IPostcodeRepository _postcodeRepository;
        private string defaultMessage = "Delivery by Courier";

        public PostcodeLookup(IPostcodeCleanupService postcodeCleanupService, IPostcodeRepository postcodeRepository)
        {
            _postcodeCleanupService = postcodeCleanupService;
            _postcodeRepository = postcodeRepository;
        }

        public string[] GetValidDeliveryOptions(string postcode)
        {
            var postcodeDeliveries = _postcodeRepository.GetPostcodeDeliveries();
            var cleanPostcode = _postcodeCleanupService.CleanPostcode(postcode);
            var trimmedPostcode = cleanPostcode;
            var matchedPostcode = "All others";

            var message = defaultMessage;

            while (trimmedPostcode.Length > 0 && message == defaultMessage)
            {
                var matchingPostCode = postcodeDeliveries.Where(postCodeDelivery => trimmedPostcode.Contains(postCodeDelivery.Postcode)).FirstOrDefault();
                if (matchingPostCode != null)
                {
                    message = matchingPostCode.Delivery;
                    matchedPostcode = matchingPostCode.Postcode;
                }
                trimmedPostcode = trimmedPostcode.Substring(0, trimmedPostcode.Length - 1);
            }

            return new string[] { postcode, cleanPostcode, matchedPostcode, message }; ;
        }
    }
}
