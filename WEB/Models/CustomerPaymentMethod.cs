using System;
using System.Text;

namespace QtIspMgntEntity
{
	public class CustomerPaymentMethod
	{
		public Int64 CustomerPaymentMethodId { get; set; }
		public Int64 CustomerPaymentId { get; set; }
		public Int32 PaymentMethodId { get; set; }
		public Decimal PaymentAmount { get; set; }
	}
}
