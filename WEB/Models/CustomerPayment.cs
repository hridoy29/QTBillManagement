using System;
using System.Text;

namespace QtIspMgntEntity
{
	public class CustomerPayment
	{
		public Int64 CustomerPaymentId { get; set; }
		public Int32 CustomerId { get; set; }
		public string PaymentDate { get; set; }
		public Int32 UpdateBy { get; set; }
		public DateTime UpdateDate { get; set; }
	}
}
