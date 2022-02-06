using CookPostcode.Services;
using System;
using Microsoft.Extensions.DependencyInjection;
using CookPostcode.Services.Interfaces;

namespace CookPostcode
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setting up dependancy injection
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IPostcodeCleanupService, PostcodeCleanupService>()
                .AddSingleton <IPostcodeLookup, PostcodeLookup>()
                .AddSingleton<IPostcodeRepository, PostcodeRepository>()
                .BuildServiceProvider();

            var postcodeLookupService = serviceProvider.GetService<IPostcodeLookup>();

            var postcodeEntered = "";
            while (postcodeEntered.ToUpper() != "FINISHED")
            {
                Console.WriteLine("Please enter a postcode or finished when done:");
                postcodeEntered = Console.ReadLine();
                var results = postcodeLookupService.GetValidDeliveryOptions(postcodeEntered);
                Console.WriteLine("Results:");
                Console.WriteLine($"    Entered Value: {results[0]}");
                Console.WriteLine($"    Cleaned Value: {results[1]}");
                Console.WriteLine($"    Matched Postcode: {results[2]}");
                Console.WriteLine($"    Delivery Option: {results[3]}");
                Console.WriteLine("");
            }
        }
    }
}

