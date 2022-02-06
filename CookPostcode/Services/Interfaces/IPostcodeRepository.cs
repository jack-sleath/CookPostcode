using CookPostcode.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookPostcode.Services.Interfaces
{
    internal interface IPostcodeRepository
    {
        List<PostcodeDelivery> GetPostcodeDeliveries();
    }
}
