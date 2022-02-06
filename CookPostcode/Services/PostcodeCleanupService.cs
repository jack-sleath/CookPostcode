using CookPostcode.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookPostcode.Services
{
    public class PostcodeCleanupService : IPostcodeCleanupService
    {
        public string CleanPostcode(string input)
        {
            return new string(input.Where(c => !char.IsWhiteSpace(c))
                .ToArray()).ToUpper();
        }
    }
}
