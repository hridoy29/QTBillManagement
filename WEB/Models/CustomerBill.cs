using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class CustomerBill
    {
        public string MobileNo { get; set; }
        public string CustomerName { get; set; }
        public string BillingAddress { get; set; }
        public string BillingId { get; set; }
        public string BillingDate { get; set; }
        public decimal Billable { get; set; }



        
    }
}