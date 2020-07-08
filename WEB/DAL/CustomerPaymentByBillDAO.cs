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
	public class CustomerPaymentByBillDAO //: IDisposible
	{
		private static volatile CustomerPaymentByBillDAO instance;
		private static readonly object lockObj = new object();
		public static CustomerPaymentByBillDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new CustomerPaymentByBillDAO();
			}
			return instance;
		}
		public static CustomerPaymentByBillDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new CustomerPaymentByBillDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public CustomerPaymentByBillDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<CustomerPaymentByBill> Get(Int64? customerPaymentByBillId = null)
		{
			try
			{
				List<CustomerPaymentByBill> CustomerPaymentByBillLst = new List<CustomerPaymentByBill>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramCustomerPaymentByBillId", customerPaymentByBillId, DbType.Int64, ParameterDirection.Input)
				};
				CustomerPaymentByBillLst = dbExecutor.FetchData<CustomerPaymentByBill>(CommandType.StoredProcedure, "wsp_CustomerPaymentByBill_Get", colparameters);
				return CustomerPaymentByBillLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<CustomerPaymentByBill> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<CustomerPaymentByBill> CustomerPaymentByBillLst = new List<CustomerPaymentByBill>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				CustomerPaymentByBillLst = dbExecutor.FetchData<CustomerPaymentByBill>(CommandType.StoredProcedure, "wsp_CustomerPaymentByBill_GetDynamic", colparameters);
				return CustomerPaymentByBillLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		    public string Post(CustomerPaymentByBill _CustomerPaymentByBill, string transactionType)
		    {
			    string ret = string.Empty;
			    try
			    {
				    Parameters[] colparameters = new Parameters[4]{
				    new Parameters("@paramCustomerPaymentByBillId", _CustomerPaymentByBill.CustomerPaymentByBillId, DbType.Int64, ParameterDirection.Input),
				    new Parameters("@paramCustomerPaymentId", _CustomerPaymentByBill.CustomerPaymentId, DbType.Int64, ParameterDirection.Input),
				    new Parameters("@paramBillingId", _CustomerPaymentByBill.BillingId, DbType.Int64, ParameterDirection.Input),
				    new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				    };
				    dbExecutor.ManageTransaction(TransactionType.Open);
				    ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_CustomerPaymentByBill_Post", colparameters, true);
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
