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
	public class CustomerPaymentDAO //: IDisposible
	{
		private static volatile CustomerPaymentDAO instance;
		private static readonly object lockObj = new object();
		public static CustomerPaymentDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new CustomerPaymentDAO();
			}
			return instance;
		}
		public static CustomerPaymentDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new CustomerPaymentDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public CustomerPaymentDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<CustomerPayment> Get(Int64? customerPaymentId = null)
		{
			try
			{
				List<CustomerPayment> CustomerPaymentLst = new List<CustomerPayment>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramCustomerPaymentId", customerPaymentId, DbType.Int64, ParameterDirection.Input)
				};
				CustomerPaymentLst = dbExecutor.FetchData<CustomerPayment>(CommandType.StoredProcedure, "wsp_CustomerPayment_Get", colparameters);
				return CustomerPaymentLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<CustomerPayment> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<CustomerPayment> CustomerPaymentLst = new List<CustomerPayment>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				CustomerPaymentLst = dbExecutor.FetchData<CustomerPayment>(CommandType.StoredProcedure, "wsp_CustomerPayment_GetDynamic", colparameters);
				return CustomerPaymentLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(CustomerPayment _CustomerPayment, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[6]{
				new Parameters("@paramCustomerPaymentId", _CustomerPayment.CustomerPaymentId, DbType.Int64, ParameterDirection.Input),
				new Parameters("@paramCustomerId", _CustomerPayment.CustomerId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramPaymentDate", _CustomerPayment.PaymentDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramUpdateBy", _CustomerPayment.UpdateBy, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramUpdateDate", _CustomerPayment.UpdateDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_CustomerPayment_Post", colparameters, true);
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
