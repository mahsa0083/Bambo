using Bambo.Areas.Admin.AdminServices;
using Bambo.Areas.Admin.Data;
using Bambo.Models.ViewModel;
using Bambo.Serives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Bambo.Models.Databsae;
using System.Drawing.Printing;


namespace Bambo.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Index( int id=0)
        {
            if(id > 0)
            {
                var res=new ServiceRepo().GetCustomerById(id);
                var model = new UserViewModel()
                {
                    CurrentCustomer = res,
                    IsLogin = true,

                };
                return View(model);
            }
            else
            {
                var model = new UserViewModel()
                {
                    CurrentCustomer = null,
                    IsLogin = true,

                };
                return View(model);
            }

           
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EnterUser( UserViewModel user) 
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
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]  
        public async Task <ActionResult> NewUserSignUp( UserViewModel user)
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
        

        public ActionResult Product(int page = 1, int pageSize = 10)
        {
            var repo = new RepoProductSevices();

            var products = repo.GetAllProduct()
            .OrderBy(p => p.ProductID)
                               .Skip((page - 1) * pageSize)
                               .Take(pageSize)
                               .ToList();

            int totalProducts = repo.GetAllProduct().Count();

            var model = new ProductViewModel
            {
                products = products,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)totalProducts / pageSize)
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult GetProduct(int id)
        {
            if(id == 0)
            {
                throw new Exception("Id id null");
            }
            var SelectProduct = new ProductViewModel()
            {
                product =  new RepoProductSevices().GetProductById(id),
            };
             return View(SelectProduct);
        }
        
    }
}