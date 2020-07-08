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
	public class AdditionalFeeDAO //: IDisposible
	{
		private static volatile AdditionalFeeDAO instance;
		private static readonly object lockObj = new object();
		public static AdditionalFeeDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new AdditionalFeeDAO();
			}
			return instance;
		}
		public static AdditionalFeeDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new AdditionalFeeDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public AdditionalFeeDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<AdditionalFee> Get(Int32? additionalFeeId = null)
		{
			try
			{
				List<AdditionalFee> AdditionalFeeLst = new List<AdditionalFee>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramAdditionalFeeId", additionalFeeId, DbType.Int32, ParameterDirection.Input)
				};
				AdditionalFeeLst = dbExecutor.FetchData<AdditionalFee>(CommandType.StoredProcedure, "wsp_AdditionalFee_Get", colparameters);
				return AdditionalFeeLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<AdditionalFee> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<AdditionalFee> AdditionalFeeLst = new List<AdditionalFee>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				AdditionalFeeLst = dbExecutor.FetchData<AdditionalFee>(CommandType.StoredProcedure, "wsp_AdditionalFee_GetDynamic", colparameters);
				return AdditionalFeeLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(AdditionalFee _AdditionalFee, string transactionType)
		{
			string ret = string.Empty;
            _AdditionalFee.CompanyId = 1;
            _AdditionalFee.UpdateBy = 1;
            _AdditionalFee.UpdateDate = DateTime.Now;


            try
			{
				Parameters[] colparameters = new Parameters[7]{
				new Parameters("@paramAdditionalFeeId", _AdditionalFee.AdditionalFeeId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCompanyId", _AdditionalFee.CompanyId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramAdditionalFeeName", _AdditionalFee.AdditionalFeeName, DbType.String, ParameterDirection.Input),
				new Parameters("@paramIsActive", _AdditionalFee.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramUpdateBy", _AdditionalFee.UpdateBy, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramUpdateDate", _AdditionalFee.UpdateDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_AdditionalFee_Post", colparameters, true);
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
