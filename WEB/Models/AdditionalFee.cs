using System;
using System.Text;

namespace QtIspMgntEntity
{
	public class AdditionalFee
	{
		public Int32 AdditionalFeeId { get; set; }
		public Int32 CompanyId { get; set; }
		public string AdditionalFeeName { get; set; }
        public Decimal Amount { get; set; }
        public bool IsActive { get; set; }
		public Int32 UpdateBy { get; set; }
		public DateTime UpdateDate { get; set; }
	}
}
