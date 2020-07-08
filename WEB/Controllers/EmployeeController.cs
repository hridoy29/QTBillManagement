using QtIspMgntDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        //public JsonResult Get()
        //{
        //    try
        //    {
        //        var list = Facade.Employee.Get();
        //        string contentType = "application/json";
        //        return Json(list, contentType, Encoding.UTF8, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(null, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public JsonResult GetDynamic(string where, string orderBy)
        //{
        //    try
        //    {
        //        var list = Facade.Employee.GetDynamic(where, orderBy);
        //        string contentType = "application/json";
        //        return Json(list, contentType, Encoding.UTF8, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(null, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[HttpPost]
        //public string Post(Employee obj, string transactionType)
        //{
        //    string ret = string.Empty;

        //    try
        //    {
        //        ret = Facade.Employee.Post(obj, transactionType);
        //        return ret;
        //    }
        //    catch (Exception)
        //    {
        //        return ret;
        //    }
        //}
    }
}