using QtIspMgntDAL;
using QtIspMgntEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WEB.Controllers
{
    public class PackageController : Controller
    {
        // GET: Package
        public JsonResult Get()
        {
            try
            {
                var list = Facade.Package.Get();
                string contentType = "application/json";
                return Json(list, contentType, Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetDynamic(string where, string orderBy)
        {
            try
            {
                var list = Facade.Package.GetDynamic(where, orderBy);
                string contentType = "application/json";
                return Json(list, contentType, Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public string Post(Package obj, string transactionType)
        {
            string ret = string.Empty;

            try
            {
                ret = Facade.Package.Post(obj, transactionType);
                return ret;
            }
            catch (Exception)
            {
                return ret;
            }
        }
    }
}