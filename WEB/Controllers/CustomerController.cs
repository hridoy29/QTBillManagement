using QtIspMgntDAL;
using QtIspMgntEntity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using ZXing;

namespace WEB.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public JsonResult Get()
        {
            try
            {
                var list = Facade.Customer.Get();
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
                var list = Facade.Customer.GetDynamic(where, orderBy);
                string contentType = "application/json";
                return Json(list, contentType, Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public string Post(Customer obj, List<AdditionalFee> additionalFeeList, string transactionType)
        {
            AdditionalFee additionalFee = new AdditionalFee();
            string ret = string.Empty;
            //obj.TeamFlagUrl = string.Empty;

            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    ret = Facade.Customer.Post(obj, transactionType);

                    if (ret.Contains("successfully"))
                    {
                        if (transactionType == "INSERT")
                        {
                            string[] retArr = ret.Split(':');
                            int CustomerId = Convert.ToInt32(retArr[1]);

                            //Insert CustomerMikrotikUser
                            CustomerMikrotikUser customerMikrotikUser = new CustomerMikrotikUser();
                            customerMikrotikUser.CustomerId = CustomerId;
                            customerMikrotikUser.UserId = obj.MKUserId;
                            Facade.CustomerMikrotikUser.Post(customerMikrotikUser, transactionType);

                            //Insert CustomerPackage
                            CustomerPackage customerPackage = new CustomerPackage();
                            customerPackage.CustomerId = CustomerId;
                            customerPackage.PackageId = obj.PackageId;
                            Facade.CustomerPackage.Post(customerPackage, transactionType);



                            //Insert CustomerAdditionalFee
                            CustomerAdditionalFee customerAdditionalFee = new CustomerAdditionalFee();
                              foreach (var item in additionalFeeList)
                            {
                                customerAdditionalFee.CustomerId = CustomerId;
                                customerAdditionalFee.AdditionalFeesId = item.AdditionalFeeId;
                                customerAdditionalFee.Amount = item.Amount;
                                Facade.CustomerAdditionalFee.Post(customerAdditionalFee, transactionType);
                            }

                        }

                    }

                    ts.Complete();
                    return ret;
                }
                catch (Exception)
                {
                    return ret;
                }
            }
        }

        public string QrCode(string MobileNumber)
        {
            // MobileNumber = "01832738014";
            string folderPath = "~/Images/";
            string imagePath = "~/Images/QrCode.jpg";
            // If the directory doesn't exist then create it.
            if (!Directory.Exists(Server.MapPath(folderPath)))
            {
                Directory.CreateDirectory(Server.MapPath(folderPath));
            }

            var barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            var result = barcodeWriter.Write(MobileNumber);

            string barcodePath = Server.MapPath(imagePath);
            var barcodeBitmap = new Bitmap(result);
            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(barcodePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
            return imagePath;
        }

        public void Billing()
        {
            Facade.Customer.Billing();
        }
    }
}