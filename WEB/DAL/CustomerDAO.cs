using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Data.Common;
using DbExecutor;
using QtIspMgntEntity;
using WEB.Models;

namespace QtIspMgntDAL
{
	public class CustomerDAO //: IDisposible
	{
		private static volatile CustomerDAO instance;
		private static readonly object lockObj = new object();
		public static CustomerDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new CustomerDAO();
			}
			return instance;
		}
		public static CustomerDAO GetInstanceThreadSafe
		{
			get
			{
				if (instance == null)
				{
					lock (lockObj)
					{
						if (instance == null)
						{
							instance = new CustomerDAO();
						}
					}
				}
				return instance;
			}
		}

		DBExecutor dbExecutor;

		public CustomerDAO()
		{
			//dbExecutor = DBExecutor.GetInstanceThreadSafe;
			dbExecutor = new DBExecutor();
		}

		public List<Customer> Get(Int32? customerId = null)
		{
			try
			{
				List<Customer> CustomerLst = new List<Customer>();
				Parameters[] colparameters = new Parameters[1]{
				new Parameters("@paramCustomerId", customerId, DbType.Int32, ParameterDirection.Input)
				};
				CustomerLst = dbExecutor.FetchData<Customer>(CommandType.StoredProcedure, "wsp_Customer_Get", colparameters);
				return CustomerLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public List<Customer> GetDynamic(string whereCondition,string orderByExpression)
		{
			try
			{
				List<Customer> CustomerLst = new List<Customer>();
				Parameters[] colparameters = new Parameters[2]{
				new Parameters("@paramWhereCondition", whereCondition, DbType.String, ParameterDirection.Input),
				new Parameters("@paramOrderByExpression", orderByExpression, DbType.String, ParameterDirection.Input),
				};
				CustomerLst = dbExecutor.FetchData<Customer>(CommandType.StoredProcedure, "wsp_Customer_GetDynamic", colparameters);
				return CustomerLst;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public string Post(Customer _Customer, string transactionType)
		{
			string ret = string.Empty;
            _Customer.UpdateDate = DateTime.Now;
            _Customer.CustomerNo = "1";
            _Customer.PhotoIdNo = "1";
            _Customer.CompanyId = 1;

            try
			{
				Parameters[] colparameters = new Parameters[18]{
				new Parameters("@paramCustomerId", _Customer.CustomerId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCompanyId", _Customer.CompanyId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramCustomerNo", _Customer.CustomerNo, DbType.String, ParameterDirection.Input),
				new Parameters("@paramCustomerName", _Customer.CustomerName, DbType.String, ParameterDirection.Input),
				new Parameters("@paramBillingAddress", _Customer.BillingAddress, DbType.String, ParameterDirection.Input),
				new Parameters("@paramMobileNo", _Customer.MobileNo, DbType.String, ParameterDirection.Input),
				new Parameters("@paramEmailId", _Customer.EmailId, DbType.String, ParameterDirection.Input),
				new Parameters("@paramPassword", _Customer.Password, DbType.String, ParameterDirection.Input),
				new Parameters("@paramBillGenerationDay", _Customer.BillGenerationDay, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramBillDueInDay", _Customer.BillDueInDay, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramNotification", _Customer.Notification, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramPhotoIdTypeId", _Customer.PhotoIdTypeId, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramPhotoIdNo", _Customer.PhotoIdNo, DbType.String, ParameterDirection.Input),
				new Parameters("@paramRemarks", _Customer.Remarks, DbType.String, ParameterDirection.Input),
				new Parameters("@paramIsActive", _Customer.IsActive, DbType.Boolean, ParameterDirection.Input),
				new Parameters("@paramUpdateBy", _Customer.UpdateBy, DbType.Int32, ParameterDirection.Input),
				new Parameters("@paramUpdateDate", _Customer.UpdateDate, DbType.DateTime, ParameterDirection.Input),
				new Parameters("@paramTransactionType", transactionType, DbType.String, ParameterDirection.Input)
				};
				dbExecutor.ManageTransaction(TransactionType.Open);
				ret = dbExecutor.ExecuteScalarString(true, CommandType.StoredProcedure, "wsp_Customer_Post", colparameters, true);
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
        public void Billing()
        {
            try
            {
                 
                  dbExecutor.FetchData<Customer>(CommandType.StoredProcedure, "wsp_GenerateBill");
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CustomerBill> GetCustomerBill(string fromdate,string  todate)
        {
            try
            {
                List<CustomerBill> CustomerBillLst = new List<CustomerBill>();
                Parameters[] colparameters = new Parameters[2]{
                new Parameters("@fromdate", fromdate, DbType.String, ParameterDirection.Input),
                new Parameters("@todate", todate, DbType.String, ParameterDirection.Input),
                };
                CustomerBillLst = dbExecutor.FetchData<CustomerBill>(CommandType.StoredProcedure, "wsp_rpt_CustomerDues_New", colparameters);
                return CustomerBillLst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
