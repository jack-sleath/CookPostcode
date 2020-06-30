using System;


namespace CookPostcode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a postcode:");
            var postcodeEntered = Console.ReadLine();
            Console.WriteLine($"You wrote {postcodeEntered}");
        }
  
    }
}
