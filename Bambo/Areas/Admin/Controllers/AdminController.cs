using Bambo.Areas.Admin.AdminServices;
using Bambo.Areas.Admin.AdminServices.Admin;
using Bambo.Areas.Admin.Data;
using Bambo.Models.Databsae;
using Bambo.Models.ViewModel;
using Bambo.Serives;
using Microsoft.Win32;
using System.Security.Policy;
using System;
using System.Web;
using System.Web.Mvc;
using System.Runtime.CompilerServices;

namespace Bambo.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        public ActionResult Index(int id = 0)
        {
            if (id != 0)
            {
                var res = new AdminRepository().GetAdminById(id);
                if (res != null)
                {
                    var EnterPerson = new AdminViewModel()
                    {
                        Admin = res,
                        IsLogin = true
                    };
                }
                else
                {
                   
                    return View();
                }
               
            }
            
             
            return View();
        }
        



        public ActionResult Register()
        {
            var model =new AdminViewModel();
            return View(model);
        }
        public ActionResult NewAdmin(AdminViewModel model)
        {
            if(ModelState.IsValid)
            {
                var checkUser=new AdminRepository().LoginAdmin(model.Admin.Username, model.Admin.Password);
                if (checkUser == true)
                {
                    TempData["haveAccount"] = true;
                    return RedirectToAction("Register");
                }
                else
                {
                    var newAdmin = new AdminRepository().UpdateAdmins(model.Admin, 0, AdminServices.Status.Insert);
                    if (newAdmin == true)
                    {
                        TempData["Succees"] = true;
                        return RedirectToAction("Index" , new {Id= AesEncryption.Encrypt(newAdmin.ToString())});
                    }
                }
                TempData["Faild"] = true;
                return View("Register");
            }
            TempData["Error"] = true;
            return View("Register");
        }
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]

        public ActionResult EnterPerson(AdminViewModel item)
        {
            if (ModelState.IsValid)
            {
                bool res = new AdminRepository().LoginAdmin(item.Admin.Username, item.Admin.Password);
                if (res == true)
                {
                    TempData["Succeed"] = true;
                    return RedirectToAction("Index", new { Id = AesEncryption.Encrypt(res.ToString()) });

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
        public ActionResult AdminProfile(int id)
        {
            var item = new AdminRepository().GetAdminById(id);
            if(item != null)
            {
                AdminViewModel Profile = new AdminViewModel()
                {
                    Admin = item,
                    IsLogin = true


                };
                return View(Profile);
            }
           


            else
            {


                AdminViewModel Profile = new AdminViewModel()
                {
                    Admin = new Tbl_Admin(),
                    IsLogin = false


                };
                return View(Profile);
            }
        }
   


        public ActionResult ViewProfile( AdminViewModel model , HttpPostedFileBase AccountImage)
        {
            if (!ModelState.IsValid)
            {
                using (var reader = new System.IO.BinaryReader(AccountImage.InputStream))
                {
                    model.Admin.Image = reader.ReadBytes(AccountImage.ContentLength);

                }
                var item = new AdminRepository().UpdateAdmins(model.Admin, 0, Status.Insert);
                if (item == true)
                {
                     return RedirectToAction("Index" , new {Id = AesEncryption.Encrypt(item.ToString())});
                    
                }
                return RedirectToAction("ViewProfile");
            }
            return RedirectToAction("ViewProfile");

        }
    }
}