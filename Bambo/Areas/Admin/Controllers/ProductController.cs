using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bambo.Areas.Admin.AdminServices;
using Bambo.Areas.Admin.Data;
using Bambo.Models.Databsae;
using Bambo.Models.ViewModel;

namespace Bambo.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult Productlist(int id = 0)
        {
            var ShownProduct = new ProductViewModel();
            {
                ShownProduct.products = new RepoProductSevices().GetAllProduct().ToList();
                ShownProduct.product = new RepoProductSevices().GetProductById(id);
                ShownProduct.brands = new RepoProductSevices().GetAllBrand().ToList();

            }
            return View(ShownProduct);

        }
        [HttpGet]
        public ActionResult AddProduct()
        {
            var model = new ProductViewModel()
            {
                categories= new RepoProductSevices().GetAllCategories().ToList(),
                brands=new RepoProductSevices().GetAllBrand().ToList(),
                product=new Product()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult NewProduct(ProductViewModel model, HttpPostedFileBase ProductImage)
        {
            if (ModelState.IsValid)
            {
                using (var reader = new System.IO.BinaryReader(ProductImage.InputStream))
                {
                    model.product.Image = reader.ReadBytes(ProductImage.ContentLength);

                }
                var res = new RepoProductSevices().UpdateProduct(model.product, Commen.Insert);
                TempData["Success"] = true;
                return RedirectToAction("Productlist", model);
            }
            else
            {
                TempData["Failed"] = true;
                return RedirectToAction("AddProduct");
            }

        }
        [HttpPost]

        public ActionResult GetproductImage(int id)
        {
            if (id != 0)
            {
                var result = new RepoProductSevices().GetProductById(id);
                if (result.Image != null)
                {
                    byte[] imagedata = result.Image;
                    string convertimage = "Image / png";
                    return File(imagedata, convertimage);
                }
                else
                {
                    return File(Server.MapPath("~/Image/signup.svg"), "Image/png");
                }
            }
            else
            {
                return null;
            }
        }

    }
}