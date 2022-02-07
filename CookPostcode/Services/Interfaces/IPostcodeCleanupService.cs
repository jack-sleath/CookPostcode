using System;
using System.Collections.Generic;
using System.Text;

namespace CookPostcode.Services.Interfaces
{
    public interface IPostcodeCleanupService
    {
        string CleanPostcode(string input);
        bool IsValidPostcode(string input);
    }
}
