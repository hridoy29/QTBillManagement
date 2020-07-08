using System;
using System.Text;

namespace QtIspMgntEntity
{
    public class Customer
    {
        public Int32 CustomerId { get; set; }
        public Int32 CompanyId { get; set; }
        public string CustomerNo { get; set; }
        public string CustomerName { get; set; }
        public string BillingAddress { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public Int32 BillGenerationDay { get; set; }
        public Int32 BillDueInDay { get; set; }
        public Int32 Notification { get; set; }
        public Int32 PhotoIdTypeId { get; set; }
        public string PhotoIdNo { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public Int32 UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public string AdditionalFeeName { get; set; }
        public string UserId { get; set; }
        public string MKUserId { get; set; }
        public int PackageId { get; set; }
        public string PackageName { get; set; }
    }
}
