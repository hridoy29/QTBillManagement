using System;
using System.Text;

namespace QtIspMgntEntity
{
	public class Package
	{
		public Int32 PackageId { get; set; }
		public Int32 CompanyId { get; set; }
		public string PackageName { get; set; }
		public Decimal PackageRate { get; set; }
		public Int32 PackageMB { get; set; }
		public bool IsActive { get; set; }
		public Int32 UpdateBy { get; set; }
		public DateTime UpdateDate { get; set; }
	}
}
