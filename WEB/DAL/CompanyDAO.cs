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
	public class CompanyDAO //: IDisposible
	{
		private static volatile CompanyDAO instance;
		private static readonly object lockObj = new object();
		public static CompanyDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new CompanyDAO();
			}
			return instance;
		}
		public static CompanyDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new CompanyDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public CompanyDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<Company> Get(Int32? companyId = null)
		{
			try
			{
				List<Company> CompanyLst = new List<Company>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramCompanyId", companyId, DbType.Int32, ParameterDirection.Input)
				};
				CompanyLst = dbExecutor.FetchData<Company>(CommandType.StoredProcedure, "wsp_Company_Get", colparameters);
				return CompanyLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<Company> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<Company> CompanyLst = new List<Company>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				CompanyLst = dbExecutor.FetchData<Company>(CommandType.StoredProcedure, "wsp_Company_GetDynamic", colparameters);
				return CompanyLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(Company _Company, string transactionType)
		{
			string ret = string.Empty;
			try
			{
				Parameters[] colparameters = new Parameters[4]{
				new Parameters("@paramCompanyId", _Company.CompanyId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCompanyName", _Company.CompanyName, DbType.String, ParameterDirection.Input),
				new Parameters("@paramCompanyAddress", _Company.CompanyAddress, DbType.String, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_Company_Post", colparameters, true);
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
