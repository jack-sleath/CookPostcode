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
            var trimmedPostcode = RemoveWhitespace(postcode);

            string[] valuesToReturn = {postcode, trimmedPostcode, "" };

            return valuesToReturn;
        }

        private string RemoveWhitespace(string input)
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

            var listOfPostCodesDeliveries = postCodeTable.AsEnumerable().Select(row => new PostcodeDelivery
            {
                PostCode = RemoveWhitespace(row["Postcode"].ToString()),
                Delivery = row["Delivery"].ToString()
            }).ToList();

            return listOfPostCodesDeliveries;
        }
    }
}
