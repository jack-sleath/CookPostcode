using CookPostcode.Interfaces;
using CookPostcode.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CookPostcode
{
    public class PostcodeLookup : IPostcodeLookup
    {
        public string[] GetValidDeliveryOptions(string postcode)
        {
            //This would come from a DB in production, but currently stored in a seperate class.
            var postCodeDeliveries = MapPostcodeDeliveries(new PostcodeData().PostCodeDataSet);
            var cleanPostcode = RemoveWhiteSpace(postcode);
            var trimmedPostcode = cleanPostcode;

            var message = "";

            while (trimmedPostcode.Length > 0 && message == "")
            {
                var matchingPostCode = postCodeDeliveries.Where(postCodeDelivery => trimmedPostcode.Contains(postCodeDelivery.PostCode)).FirstOrDefault();
                if (matchingPostCode != null)
                {
                    message = matchingPostCode.Delivery;
                }
                trimmedPostcode = trimmedPostcode.Substring(0, trimmedPostcode.Length - 1);
            }

            if (message == "")
            {
                string[] deafultMessage = { postcode, cleanPostcode, "Delivery by Courier" };
                return deafultMessage;
            }

            string[] customMessage = { postcode, cleanPostcode, message };
            return customMessage;
        }

        private string RemoveWhiteSpace(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !char.IsWhiteSpace(c))
                .ToArray()).ToUpper();
        }

        private List<PostcodeDelivery> MapPostcodeDeliveries(DataSet postCodeDataSet)
        {
            if (postCodeDataSet.Tables.Count != 1)
            {
                throw new Exception("Incorrect tables supplied.");
            }

            var postCodeTable = postCodeDataSet.Tables[0];

            var listOfPostCodeDeliveries = postCodeTable.AsEnumerable().Select(row => new PostcodeDelivery
            {
                PostCode = RemoveWhiteSpace(row["Postcode"].ToString()),
                Delivery = row["Delivery"].ToString()
            }).ToList();

            return listOfPostCodeDeliveries.OrderByDescending(postCodeDelivery => postCodeDelivery.PostCode.Length).ToList();
        }
    }
}
