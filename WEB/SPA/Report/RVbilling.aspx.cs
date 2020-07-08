using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.SPA.Report
{
    public partial class RVbilling : System.Web.UI.Page
    {
        public string FromDate, ToDate;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    FromDate = Convert.ToString(Request.QueryString["FromDate"]);
                    ToDate = Convert.ToString(Request.QueryString["ToDate"]);
                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["fromdate"] = FromDate;
            e.InputParameters["todate"] = ToDate;
        }
    }
}