using System;
using System.Text;

namespace QtIspMgntEntity
{
	public class CustomerPaymentByBill
	{
		public Int64 CustomerPaymentByBillId { get; set; }
		public Int64 CustomerPaymentId { get; set; }
		public Int64 BillingId { get; set; }
	}
}
