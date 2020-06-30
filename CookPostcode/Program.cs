using System;


namespace CookPostcode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a postcode:");
            var postcodeEntered = Console.ReadLine();
            var returnedValues = new PostcodeLookup().GetValidDeliveryOptions(postcodeEntered);
            Console.WriteLine($"Results:");
            Console.WriteLine(returnedValues[0]);
            Console.WriteLine(returnedValues[1]);
            Console.WriteLine(returnedValues[2]);
        }
    }

}

