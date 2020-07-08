using System;
using System.Text;

namespace QtIspMgntEntity
{
	public class BillingCustomerPackage
	{
		public Int64 BillingCustomerPackageId { get; set; }
		public Int64 BillingId { get; set; }
		public Int32 CustomerPackageId { get; set; }
		public Decimal? PackageRate { get; set; }
	}
}
