using System;
using System.Text;

namespace QtIspMgntEntity
{
	public class Billing
	{
		public Int64 BillingId { get; set; }
		public Int32 CompanyId { get; set; }
		public Int32 CustomerId { get; set; }
		public string BillingDate { get; set; }
		public string InvoiceNo { get; set; }
		public Int32 UpdateBy { get; set; }
		public DateTime UpdateDate { get; set; }
	}
}
