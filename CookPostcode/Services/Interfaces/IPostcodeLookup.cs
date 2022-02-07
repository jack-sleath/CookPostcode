using CookPostcode.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookPostcode.Services.Interfaces
{
    public interface IPostcodeLookup
    {
        string[] GetValidDeliveryOptions(string postcode);
    }
}
