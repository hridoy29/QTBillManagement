using System;
using System.Text;

namespace QtIspMgntEntity
{
	public class BillingAdditionalFee
	{
		public Int64 BillingAdditionalFeeId { get; set; }
		public Int64 BillingId { get; set; }
		public Int32 AdditionalFeeId { get; set; }
		public Decimal Amount { get; set; }
	}
}
