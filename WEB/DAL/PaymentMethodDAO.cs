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
	public class PaymentMethodDAO //: IDisposible
	{
		private static volatile PaymentMethodDAO instance;
		private static readonly object lockObj = new object();
		public static PaymentMethodDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new PaymentMethodDAO();
			}
			return instance;
		}
		public static PaymentMethodDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new PaymentMethodDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public PaymentMethodDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<PaymentMethod> Get(Int32? paymentMethodId = null)
		{
			try
			{
				List<PaymentMethod> PaymentMethodLst = new List<PaymentMethod>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramPaymentMethodId", paymentMethodId, DbType.Int32, ParameterDirection.Input)
				};
				PaymentMethodLst = dbExecutor.FetchData<PaymentMethod>(CommandType.StoredProcedure, "wsp_PaymentMethod_Get", colparameters);
				return PaymentMethodLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<PaymentMethod> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<PaymentMethod> PaymentMethodLst = new List<PaymentMethod>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				PaymentMethodLst = dbExecutor.FetchData<PaymentMethod>(CommandType.StoredProcedure, "wsp_PaymentMethod_GetDynamic", colparameters);
				return PaymentMethodLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(PaymentMethod _PaymentMethod, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[7]{
				new Parameters("@paramPaymentMethodId", _PaymentMethod.PaymentMethodId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCompanyId", _PaymentMethod.CompanyId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramPaymentMethodName", _PaymentMethod.PaymentMethodName, DbType.String, ParameterDirection.Input),
				new Parameters("@paramIsActive", _PaymentMethod.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramUpdateBy", _PaymentMethod.UpdateBy, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramUpdateDate", _PaymentMethod.UpdateDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_PaymentMethod_Post", colparameters, true);
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
