using CookPostcode.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookPostcode.Services.Interfaces
{
    public interface IPostcodeRepository
    {
        List<PostcodeDelivery> GetPostcodeDeliveries();
    }
}
