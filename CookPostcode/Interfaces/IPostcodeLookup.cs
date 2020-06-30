using System;
using System.Collections.Generic;
using System.Text;

namespace CookPostcode.Interfaces
{
    interface IPostcodeLookup
    {
        string[] GetValidDeliveryOptions(string postcode);
    }
}
