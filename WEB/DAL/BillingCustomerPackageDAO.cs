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
	public class BillingCustomerPackageDAO //: IDisposible
	{
		private static volatile BillingCustomerPackageDAO instance;
		private static readonly object lockObj = new object();
		public static BillingCustomerPackageDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new BillingCustomerPackageDAO();
			}
			return instance;
		}
		public static BillingCustomerPackageDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new BillingCustomerPackageDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public BillingCustomerPackageDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<BillingCustomerPackage> Get(Int64? billingCustomerPackageId = null)
		{
			try
			{
				List<BillingCustomerPackage> BillingCustomerPackageLst = new List<BillingCustomerPackage>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramBillingCustomerPackageId", billingCustomerPackageId, DbType.Int64, ParameterDirection.Input)
				};
				BillingCustomerPackageLst = dbExecutor.FetchData<BillingCustomerPackage>(CommandType.StoredProcedure, "wsp_BillingCustomerPackage_Get", colparameters);
				return BillingCustomerPackageLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<BillingCustomerPackage> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<BillingCustomerPackage> BillingCustomerPackageLst = new List<BillingCustomerPackage>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				BillingCustomerPackageLst = dbExecutor.FetchData<BillingCustomerPackage>(CommandType.StoredProcedure, "wsp_BillingCustomerPackage_GetDynamic", colparameters);
				return BillingCustomerPackageLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(BillingCustomerPackage _BillingCustomerPackage, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@paramBillingCustomerPackageId", _BillingCustomerPackage.BillingCustomerPackageId, DbType.Int64, ParameterDirection.Input),
				new Parameters("@paramBillingId", _BillingCustomerPackage.BillingId, DbType.Int64, ParameterDirection.Input),
				new Parameters("@paramCustomerPackageId", _BillingCustomerPackage.CustomerPackageId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramPackageRate", _BillingCustomerPackage.PackageRate, DbType.Decimal, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_BillingCustomerPackage_Post", colparameters, true);
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
