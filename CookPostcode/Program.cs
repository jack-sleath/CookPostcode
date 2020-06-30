using System;
using System.Data;

namespace CookPostcode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public DataSet PostCodeDataSet { get {
                var dataSet = new DataSet();

                var dataTable = new DataTable();

                dataTable.Columns.Add("Postcode", typeof(string));
                dataTable.Columns.Add("Delivery_Type", typeof(string));

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
    }
}
