using Bambo.Areas.Admin.AdminServices;
using Bambo.Areas.Admin.Data;
using Bambo.Models.ViewModel;
using Bambo.Serives;
using IPE.SmsIrClient;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace Bambo.Controllers
{
    public class HomeController : Controller
    {




        public ActionResult Index(string hashId = "")
        {
            if (!String.IsNullOrEmpty(hashId))
            {
                string dec = AesEncryption.Decrypt(hashId);
                if (int.TryParse(dec, out int id))
                {
                    var result = new ServiceRepo().GetCustomerById(id);
                    if (result != null)
                    {
                        UserViewModel model = new UserViewModel()
                        {
                            CurrentCustomer = result,
                            IsLogin = true
                        };
                        return View(model);
                    }
                    else
                    {
                        UserViewModel model = new UserViewModel()
                        {
                            CurrentCustomer = null,
                            IsLogin = false
                        };
                        return View(model);
                    }
                }
                else
                {
                    return View();
                }
            }
            else
            {
                UserViewModel model = new UserViewModel()
                {
                    CurrentCustomer = null,
                    IsLogin = false
                };
                return View(model);
            }
        }



        [HttpPost]
        public ActionResult EnterPerson(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                bool res = new ServiceRepo().CheckLoginUser(user.CurrentCustomer.Phone, user.CurrentCustomer.PasswordHashed);
                if (res == true)
                {
                    TempData["Succeed"] = true;
                    return RedirectToAction("Index", new { id = user.CurrentCustomer.CustomerId });

                }
                else
                {
                    TempData["Failed"] = true;
                    return View();

                }

            }
            TempData["Error"] = true;
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public ActionResult NewRegister(UserViewModel user)
        {
            if (ModelState.IsValid)
            {

                var res = new ServiceRepo().UpdateCustomer(user.CurrentCustomer, Commen.Insert);
                if (res == true)
                {

                    return RedirectToAction("Index", new { Hashid = AesEncryption.Encrypt(res.ToString()) });

                }
                else
                {
                    return View("Register");
                }
            }
            else
            {
                return View("Register");
            }
        }
        [HttpGet]
        public ActionResult ShowProduct()
        {
            var res = new UserViewModel()
            {
                products = new RepoProductSevices().GetAllProduct()
            };
            return View(res);
        }

        public JsonResult ProductInfo(int Id)
        {
            if(Id!= 0)
            {
                var res= new RepoProductSevices().GetProductById(Id);
                if(res != null)
                {
                    return Json(res);
                }
                else
                {
                    return Json(new { success = true, message = "محصول " + res.ProductName + " در دسترس نیست" }, JsonRequestBehavior.AllowGet);
                }
               
            }
            return Json(new { success = true, message = "لطفا دوباره تلاش کنید" }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ViewProductInfo(ProductViewModel model)
        {
            
            return Json(model);
        }

        public async Task<ActionResult> SendVerificationCode(string phoneNumber)
        {

            if (phoneNumber != null)
            {
                bool result = new ServiceRepo().CheckUserbynumber(phoneNumber);
                if (result == true)
                {
                    // ۲. تولید و ذخیره کد تأیید در دیتابیس
                    var verificationCode = new System.Random().Next(10, 99).ToString();
                    bool res = new ServiceRepo().UpdateVerificationCode(phoneNumber, verificationCode);

                    // ۳. ارسال کد از طریق پنل پیامکی
                    var smsIr = new SmsIr("gENUXaHrcwSFne5LALfDxSRwjGjxl9zT8RWlE2HZmsCvuUkG");

                    long linenumber = 30002108005789;

                    string message = $"کد ورود شما:{verificationCode}";
                    var sendmessage = await smsIr.BulkSendAsync(linenumber, message, new string[] { phoneNumber });
                }

                return Json(new { success = true, message = "کد تأیید با موفقیت ارسال شد. لطفاً کد را وارد کنید." });
            }
            else
            {
                return Json(new { success = false, message = "خطا در ارسال پیامک: " });
            }


        }

        // این اکشن مسئول بررسی کد ورودی و ورود کاربر است
        [HttpPost]
        public ActionResult VerifyCode(string phoneNumber, string verificationCode)
        {
            var Isvalid = new ServiceRepo().CheckVerificationCode(phoneNumber, verificationCode).FirstOrDefault();

            if (Isvalid.Phone == phoneNumber && Isvalid.VerficationCode == verificationCode)
            {

                return RedirectToAction("Index", new { Hashid = AesEncryption.Encrypt(Isvalid.CustomerId.ToString()) });
            }

            else
            {
                return View("Register");
            }

        }



    }




}
