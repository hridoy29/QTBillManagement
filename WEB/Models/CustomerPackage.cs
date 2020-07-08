using System;
using System.Text;

namespace QtIspMgntEntity
{
	public class CustomerPackage
	{
		public Int32 CustomerPackageId { get; set; }
		public Int32 CustomerId { get; set; }
		public Int32 PackageId { get; set; }
		public DateTime? IssueDate { get; set; }
		public DateTime? ValidDate { get; set; }
	}
}
