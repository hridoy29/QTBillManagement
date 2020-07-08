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
	public class CustomerPaymentMethodDAO //: IDisposible
	{
		private static volatile CustomerPaymentMethodDAO instance;
		private static readonly object lockObj = new object();
		public static CustomerPaymentMethodDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new CustomerPaymentMethodDAO();
			}
			return instance;
		}
		public static CustomerPaymentMethodDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new CustomerPaymentMethodDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public CustomerPaymentMethodDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<CustomerPaymentMethod> Get(Int64? customerPaymentMethodId = null)
		{
			try
			{
				List<CustomerPaymentMethod> CustomerPaymentMethodLst = new List<CustomerPaymentMethod>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramCustomerPaymentMethodId", customerPaymentMethodId, DbType.Int64, ParameterDirection.Input)
				};
				CustomerPaymentMethodLst = dbExecutor.FetchData<CustomerPaymentMethod>(CommandType.StoredProcedure, "wsp_CustomerPaymentMethod_Get", colparameters);
				return CustomerPaymentMethodLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<CustomerPaymentMethod> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<CustomerPaymentMethod> CustomerPaymentMethodLst = new List<CustomerPaymentMethod>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				CustomerPaymentMethodLst = dbExecutor.FetchData<CustomerPaymentMethod>(CommandType.StoredProcedure, "wsp_CustomerPaymentMethod_GetDynamic", colparameters);
				return CustomerPaymentMethodLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(CustomerPaymentMethod _CustomerPaymentMethod, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[5]{
				new Parameters("@paramCustomerPaymentMethodId", _CustomerPaymentMethod.CustomerPaymentMethodId, DbType.Int64, ParameterDirection.Input),
				new Parameters("@paramCustomerPaymentId", _CustomerPaymentMethod.CustomerPaymentId, DbType.Int64, ParameterDirection.Input),
				new Parameters("@paramPaymentMethodId", _CustomerPaymentMethod.PaymentMethodId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramPaymentAmount", _CustomerPaymentMethod.PaymentAmount, DbType.Decimal, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_CustomerPaymentMethod_Post", colparameters, true);
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
