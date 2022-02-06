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
                .AddSingleton <IPostcodeLookupService, PostcodeLookupService>()
                .AddSingleton<IPostcodeRepository, PostcodeRepository>()
                .BuildServiceProvider();

            var postcodeLookupService = serviceProvider.GetService<IPostcodeLookupService>();
            var postcodeRepository = serviceProvider.GetService<IPostcodeRepository>();

            var postcodeEntered = "";
            while (postcodeEntered.ToUpper() != "FINISHED")
            {
                Console.WriteLine("Please enter a postcode or finished when done:");
                postcodeEntered = Console.ReadLine();
                var listOfPostcode = postcodeRepository.GetPostcodeDeliveries();
                var results = postcodeLookupService.GetValidDeliveryOptions(postcodeEntered, listOfPostcode);
                Console.WriteLine("Results:");
                Console.WriteLine($"    Entered Value: {results.Entered}");
                Console.WriteLine($"    Cleaned Value: {results.Cleaned}");
                Console.WriteLine($"    Matched Postcode: {results.Matched}");
                Console.WriteLine($"    Delivery Option: {results.DeliveryOption}");
                Console.WriteLine("");
            }
        }
    }
}

