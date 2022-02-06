using CookPostcode.Services;
using System;


namespace CookPostcode
{
    class Program
    {
        static void Main(string[] args)
        {
            var postcodeEntered = "";
            while (postcodeEntered.ToUpper() != "FINISHED")
            {
                Console.WriteLine("Please enter a postcode or finished when done:");
                postcodeEntered = Console.ReadLine();
                var listOfPostcode = new PostcodeRepository().GetPostcodeDeliveries();
                var returnedValues = new PostcodeLookupService().GetValidDeliveryOptions(postcodeEntered, listOfPostcode);
                Console.WriteLine("Results:");
                Console.WriteLine($"Entered Value: {returnedValues[0]}");
                Console.WriteLine($"Cleaned Value: {returnedValues[1]}");
                Console.WriteLine($"Matched Postcode: {returnedValues[2]}");
                Console.WriteLine($"Delivery Option: {returnedValues[3]}");
            }
        }
    }
}

