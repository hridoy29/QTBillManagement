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
	public class BillingDAO //: IDisposible
	{
		private static volatile BillingDAO instance;
		private static readonly object lockObj = new object();
		public static BillingDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new BillingDAO();
			}
			return instance;
		}
		public static BillingDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new BillingDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public BillingDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<Billing> Get(Int64? billingId = null)
		{
			try
			{
				List<Billing> BillingLst = new List<Billing>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramBillingId", billingId, DbType.Int64, ParameterDirection.Input)
				};
				BillingLst = dbExecutor.FetchData<Billing>(CommandType.StoredProcedure, "wsp_Billing_Get", colparameters);
				return BillingLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<Billing> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<Billing> BillingLst = new List<Billing>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				BillingLst = dbExecutor.FetchData<Billing>(CommandType.StoredProcedure, "wsp_Billing_GetDynamic", colparameters);
				return BillingLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(Billing _Billing, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[8]{
				new Parameters("@paramBillingId", _Billing.BillingId, DbType.Int64, ParameterDirection.Input),
				new Parameters("@paramCompanyId", _Billing.CompanyId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCustomerId", _Billing.CustomerId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramBillingDate", _Billing.BillingDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramInvoiceNo", _Billing.InvoiceNo, DbType.String, ParameterDirection.Input),
				new Parameters("@paramUpdateBy", _Billing.UpdateBy, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramUpdateDate", _Billing.UpdateDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_Billing_Post", colparameters, true);
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
