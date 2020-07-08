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
	public class BillingAdditionalFeeDAO //: IDisposible
	{
		private static volatile BillingAdditionalFeeDAO instance;
		private static readonly object lockObj = new object();
		public static BillingAdditionalFeeDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new BillingAdditionalFeeDAO();
			}
			return instance;
		}
		public static BillingAdditionalFeeDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new BillingAdditionalFeeDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public BillingAdditionalFeeDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<BillingAdditionalFee> Get(Int64? billingAdditionalFeeId = null)
		{
			try
			{
				List<BillingAdditionalFee> BillingAdditionalFeeLst = new List<BillingAdditionalFee>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramBillingAdditionalFeeId", billingAdditionalFeeId, DbType.Int64, ParameterDirection.Input)
				};
				BillingAdditionalFeeLst = dbExecutor.FetchData<BillingAdditionalFee>(CommandType.StoredProcedure, "wsp_BillingAdditionalFee_Get", colparameters);
				return BillingAdditionalFeeLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<BillingAdditionalFee> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<BillingAdditionalFee> BillingAdditionalFeeLst = new List<BillingAdditionalFee>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				BillingAdditionalFeeLst = dbExecutor.FetchData<BillingAdditionalFee>(CommandType.StoredProcedure, "wsp_BillingAdditionalFee_GetDynamic", colparameters);
				return BillingAdditionalFeeLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(BillingAdditionalFee _BillingAdditionalFee, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@paramBillingAdditionalFeeId", _BillingAdditionalFee.BillingAdditionalFeeId, DbType.Int64, ParameterDirection.Input),
				new Parameters("@paramBillingId", _BillingAdditionalFee.BillingId, DbType.Int64, ParameterDirection.Input),
				new Parameters("@paramAdditionalFeeId", _BillingAdditionalFee.AdditionalFeeId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramAmount", _BillingAdditionalFee.Amount, DbType.Decimal, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_BillingAdditionalFee_Post", colparameters, true);
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
