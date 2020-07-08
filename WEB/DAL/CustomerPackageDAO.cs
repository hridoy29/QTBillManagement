using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Data.Common;
using DbExecutor;
using QtIspMgntEntity;

namespace QtIspMgntDAL
{
	public class CustomerPackageDAO //: IDisposible
	{
		private static volatile CustomerPackageDAO instance;
		private static readonly object lockObj = new object();
		public static CustomerPackageDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new CustomerPackageDAO();
			}
			return instance;
		}
		public static CustomerPackageDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new CustomerPackageDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public CustomerPackageDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<CustomerPackage> Get(Int32? customerPackageId = null)
		{
			try
			{
				List<CustomerPackage> CustomerPackageLst = new List<CustomerPackage>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramCustomerPackageId", customerPackageId, DbType.Int32, ParameterDirection.Input)
				};
				CustomerPackageLst = dbExecutor.FetchData<CustomerPackage>(CommandType.StoredProcedure, "wsp_CustomerPackage_Get", colparameters);
				return CustomerPackageLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<CustomerPackage> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<CustomerPackage> CustomerPackageLst = new List<CustomerPackage>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				CustomerPackageLst = dbExecutor.FetchData<CustomerPackage>(CommandType.StoredProcedure, "wsp_CustomerPackage_GetDynamic", colparameters);
				return CustomerPackageLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(CustomerPackage _CustomerPackage, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[4]{
				new Parameters("@paramCustomerPackageId", _CustomerPackage.CustomerPackageId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCustomerId", _CustomerPackage.CustomerId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramPackageId", _CustomerPackage.PackageId, DbType.Int32, ParameterDirection.Input),
				//new Parameters("@paramIssueDate", _CustomerPackage.IssueDate, DbType.DateTime, ParameterDirection.Input),
				//new Parameters("@paramValidDate", _CustomerPackage.ValidDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_CustomerPackage_Post", colparameters, true);
				dbExecutor.ManageTransaction(TransactionType.Commit);
			}
			catch (DBConcurrencyException except)
			{
				dbExecutor.ManageTransaction(TransactionType.Rollback);
				throw except;
			}
			catch (Exception ex)
			{
				dbExecutor.ManageTransaction(TransactionType.Rollback);
				throw ex;
			}
			return ret;
		}
	}
}
