using CookPostcode.Models;
using CookPostcode.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CookPostcode.Services
{
    public class PostcodeRepository : IPostcodeRepository
    {
        private IPostcodeCleanupService _postcodeCleanupService;
        public PostcodeRepository(IPostcodeCleanupService postcodeCleanupService)
        {
            _postcodeCleanupService = postcodeCleanupService;
        }
        private DataSet postCodeDataSet
        {
            get
            {
                var dataSet = new DataSet();
                var dataTable = new DataTable();

                dataTable.Columns.Add("Postcode", typeof(string));
                dataTable.Columns.Add("Delivery", typeof(string));

                dataTable.Rows.Add("TN9", "Delivery from Warehouse");
                dataTable.Rows.Add("TN9 1AP", "No Deliveries");
                dataTable.Rows.Add("TN8", "Delivery from Warehouse");
                dataTable.Rows.Add("TN11", "Delivery from Warehouse");
                dataTable.Rows.Add("TN1", "Van Delivery, Collect from Tunbridge Wells");
                dataTable.Rows.Add("TN2", "Van Delivery, Collect from Tunbridge Wells");
                dataTable.Rows.Add("TN10", "Van Delivery");
                dataTable.Rows.Add("TN13", "Delivery from Sevenoaks, Collect from Sevenoaks");
                dataTable.Rows.Add("TN14", "Delivery from Sevenoaks, Collect from Sevenoaks");
                dataTable.Rows.Add("TN15", "Collect from Sevenoaks");
                dataTable.Rows.Add("ME", "No Deliveries");
                dataTable.Rows.Add("ME10", "Collect from Kitchen");
                dataTable.Rows.Add("ME10 3", "3 Collect from Kitchen, Delivery from Sittingbourne");
                dataTable.Rows.Add("IV", "No Deliveries");

                dataSet.Tables.Add(dataTable);

                return dataSet;
            }
        }

        public List<PostcodeDelivery> GetPostcodeDeliveries()
        {
            //This is where a stored procedure would be called to get postCodeDataSet

            if (postCodeDataSet.Tables.Count != 1)
            {
                throw new Exception("Incorrect tables supplied.");
            }

            var postCodeTable = postCodeDataSet.Tables[0];

            var listOfPostCodeDeliveries = postCodeTable.AsEnumerable().Select(row => new PostcodeDelivery
            {
                PostCode = _postcodeCleanupService.CleanPostcode(row["Postcode"].ToString()),
                Delivery = row["Delivery"].ToString()
            }).ToList();

            return listOfPostCodeDeliveries.OrderByDescending(postCodeDelivery => postCodeDelivery.PostCode.Length).ToList();
        }
    }
}
