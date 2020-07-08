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
	public class CustomerMikrotikUserDAO //: IDisposible
	{
		private static volatile CustomerMikrotikUserDAO instance;
		private static readonly object lockObj = new object();
		public static CustomerMikrotikUserDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new CustomerMikrotikUserDAO();
			}
			return instance;
		}
		public static CustomerMikrotikUserDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new CustomerMikrotikUserDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public CustomerMikrotikUserDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<CustomerMikrotikUser> Get(Int32? customerMikrotikUserId = null)
		{
			try
			{
				List<CustomerMikrotikUser> CustomerMikrotikUserLst = new List<CustomerMikrotikUser>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramCustomerMikrotikUserId", customerMikrotikUserId, DbType.Int32, ParameterDirection.Input)
				};
				CustomerMikrotikUserLst = dbExecutor.FetchData<CustomerMikrotikUser>(CommandType.StoredProcedure, "wsp_CustomerMikrotikUser_Get", colparameters);
				return CustomerMikrotikUserLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<CustomerMikrotikUser> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<CustomerMikrotikUser> CustomerMikrotikUserLst = new List<CustomerMikrotikUser>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				CustomerMikrotikUserLst = dbExecutor.FetchData<CustomerMikrotikUser>(CommandType.StoredProcedure, "wsp_CustomerMikrotikUser_GetDynamic", colparameters);
				return CustomerMikrotikUserLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(CustomerMikrotikUser _CustomerMikrotikUser, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[4]{
				new Parameters("@paramCustomerMikrotikUserId", _CustomerMikrotikUser.CustomerMikrotikUserId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCustomerId", _CustomerMikrotikUser.CustomerId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramUserId", _CustomerMikrotikUser.UserId, DbType.String, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_CustomerMikrotikUser_Post", colparameters, true);
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
