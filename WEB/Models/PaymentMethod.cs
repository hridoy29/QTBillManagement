using System;
using System.Text;

namespace QtIspMgntEntity
{
	public class PaymentMethod
	{
		public Int32 PaymentMethodId { get; set; }
		public Int32 CompanyId { get; set; }
		public string PaymentMethodName { get; set; }
		public bool IsActive { get; set; }
		public Int32 UpdateBy { get; set; }
		public DateTime UpdateDate { get; set; }
	}
}
