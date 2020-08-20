using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManga.Models
{
    public class Order
    {
        public enum StateCode
        {
            Waiting = 1,
            Ready = 2,
            Shipped = 3,
            Finished = 4
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string AddressShippingSteelName { get; set; }
        public string AddressShippingPostalCode { get; set; }
        public string AddressShippingCountry { get; set; }
        public string AddressShippingCity { get; set; }
        public string AddressShippingOption1 { get; set; }
        public string AddressShippingOption2 { get; set; }
        public string AddressInvoiceSteelName { get; set; }
        public string AddressInvoiceCountry { get; set; }
        public string AddressInvoiceCity { get; set; }
        public string AddressInvoicePostalCode { get; set; }
        public string AddressInvoiceOption1 { get; set; }
        public string AddressInvoiceOption2 { get; set; }
        public double TotalWeight { get; set; }
        public double ShippingTax { get; set; }
        public double TotalPrice { get; set; }
        public int State { get; set; }
        public virtual User User { get; set; }
        public virtual List<MangaOrder> MangaOrders { get; set; }
        
    }
}
