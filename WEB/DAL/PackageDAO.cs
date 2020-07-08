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
	public class PackageDAO //: IDisposible
	{
		private static volatile PackageDAO instance;
		private static readonly object lockObj = new object();
		public static PackageDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new PackageDAO();
			}
			return instance;
		}
		public static PackageDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new PackageDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public PackageDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<Package> Get(Int32? packageId = null)
		{
			try
			{
				List<Package> PackageLst = new List<Package>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramPackageId", packageId, DbType.Int32, ParameterDirection.Input)
				};
				PackageLst = dbExecutor.FetchData<Package>(CommandType.StoredProcedure, "wsp_Package_Get", colparameters);
				return PackageLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<Package> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<Package> PackageLst = new List<Package>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				PackageLst = dbExecutor.FetchData<Package>(CommandType.StoredProcedure, "wsp_Package_GetDynamic", colparameters);
				return PackageLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(Package _Package, string transactionType)
		{
			string ret = string.Empty;
            _Package.UpdateDate = DateTime.Now;
            _Package.CompanyId = 1;

            try
			{
				Parameters[] colparameters = new Parameters[9]{
				new Parameters("@paramPackageId", _Package.PackageId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCompanyId", _Package.CompanyId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramPackageName", _Package.PackageName, DbType.String, ParameterDirection.Input),
				new Parameters("@paramPackageRate", _Package.PackageRate, DbType.Decimal, ParameterDirection.Input),
				new Parameters("@paramPackageMB", _Package.PackageMB, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramIsActive", _Package.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramUpdateBy", _Package.UpdateBy, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramUpdateDate", _Package.UpdateDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_Package_Post", colparameters, true);
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
