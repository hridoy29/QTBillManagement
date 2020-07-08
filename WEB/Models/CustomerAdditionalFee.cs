using System;
using System.Text;

namespace QtIspMgntEntity
{
	public class CustomerAdditionalFee
	{
		public Int32 CustomerAdditionalFeeId { get; set; }
		public Int32 CustomerId { get; set; }
		public Int32 AdditionalFeesId { get; set; }
		public Decimal Amount { get; set; }
	}
}
