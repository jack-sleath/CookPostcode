using CookPostcode.Exceptions;
using CookPostcode.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CookPostcode.Services
{
    public class PostcodeCleanupService : IPostcodeCleanupService
    {
        public string CleanPostcode(string input)
        {
            return input.Trim().ToUpper();
        }

        public bool IsValidPostcode(string input)
        {
            //Regex being used comes from this StackOverflow thread https://stackoverflow.com/a/51885364
            return Regex.IsMatch(input, "^([A-Za-z][A-Ha-hJ-Yj-y]?[0-9][A-Za-z0-9]? ?[0-9][A-Za-z]{2}|[Gg][Ii][Rr] ?0[Aa]{2})$");
        }
    }
}
