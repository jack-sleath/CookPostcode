using CookPostcode.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookPostcode.Services.Interfaces
{
    public interface IPostcodeLookupService
    {
        PostcodeResults GetValidDeliveryOptions(string postcode, List<PostcodeDelivery> postCodeDeliveries);
    }
}
