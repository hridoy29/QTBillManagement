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
	public class CustomerAdditionalFeeDAO //: IDisposible
	{
		private static volatile CustomerAdditionalFeeDAO instance;
		private static readonly object lockObj = new object();
		public static CustomerAdditionalFeeDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new CustomerAdditionalFeeDAO();
			}
			return instance;
		}
		public static CustomerAdditionalFeeDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new CustomerAdditionalFeeDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public CustomerAdditionalFeeDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<CustomerAdditionalFee> Get(Int32? customerAdditionalFeeId = null)
		{
			try
			{
				List<CustomerAdditionalFee> CustomerAdditionalFeeLst = new List<CustomerAdditionalFee>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramCustomerAdditionalFeeId", customerAdditionalFeeId, DbType.Int32, ParameterDirection.Input)
				};
				CustomerAdditionalFeeLst = dbExecutor.FetchData<CustomerAdditionalFee>(CommandType.StoredProcedure, "wsp_CustomerAdditionalFee_Get", colparameters);
				return CustomerAdditionalFeeLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<CustomerAdditionalFee> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<CustomerAdditionalFee> CustomerAdditionalFeeLst = new List<CustomerAdditionalFee>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				CustomerAdditionalFeeLst = dbExecutor.FetchData<CustomerAdditionalFee>(CommandType.StoredProcedure, "wsp_CustomerAdditionalFee_GetDynamic", colparameters);
				return CustomerAdditionalFeeLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(CustomerAdditionalFee _CustomerAdditionalFee, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@paramCustomerAdditionalFeeId", _CustomerAdditionalFee.CustomerAdditionalFeeId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCustomerId", _CustomerAdditionalFee.CustomerId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramAdditionalFeesId", _CustomerAdditionalFee.AdditionalFeesId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramAmount", _CustomerAdditionalFee.Amount, DbType.Decimal, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_CustomerAdditionalFee_Post", colparameters, true);
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
