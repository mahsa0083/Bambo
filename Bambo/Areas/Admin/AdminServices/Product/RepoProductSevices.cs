using Bambo.Models.Databsae;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bambo.Areas.Admin.AdminServices
{
    public class RepoProductSevices : IRepo
    {
        public List<Brand> GetAllBrand()
        {
            using (var context = new BamboDataBase())
            {
                return context.Brand.ToList(); 
            }


        }

        public List<Category> GetAllCategories()
        {
            using (var context = new BamboDataBase())
            {
                return context.Category.ToList();
            }
        }

        public List<Product> GetAllProduct()
        {

            using (var context = new BamboDataBase())
            {
                return context.Product.ToList();
            }
        }

        public Brand GetBrandById(int id)
        {
            using(var context = new BamboDataBase())
            {
                if(id != 0)
                {
                    var res=context.Brand.SingleOrDefault(u=>u.BrandID == id);
                    return res;
                }
                return null;
            }
        }

        public Category GetCategoryById(int id)
        {
            if (id != 0)
            {
                using (var context = new BamboDataBase())
                {
                    var model = context.Category.FirstOrDefault(u => u.CategoryID == id);
                    return model;
                }
            }
            else return null;
        }

        public Product GetProductById(int id)
        {
            if (id != 0)
            {
                using (var context = new BamboDataBase())
                {
                    var model = context.Product.Where(u=>u.ProductID==id).FirstOrDefault();
                    return model;
                }
            }
            else return null;
        }

        public bool UpdateProduct(Product customer, Commen state)
        {
            if (customer != null)
            {
                using (var context = new BamboDataBase())
                {
                    switch (state)
                    {
                        case Commen.Insert:
                            if(customer != null)
                            {
                                context.Product.Add(customer);

                                context.SaveChanges();
                                return true;
                            }
                            return false;
                          


                        case Commen.Update:
                            var model = context.Product.FirstOrDefault(u => u.ProductID == customer.ProductID);
                            model.ProductName = customer.ProductName;
                            model.Price = customer.Price;
                            model.ProductName = customer.ProductName;
                            model.Image = customer.Image;
                            model.ProductName = customer.ProductName;
                            model.Granantee = customer.Granantee;
                            model.IsAvaible = customer.IsAvaible;
                            context.SaveChanges();
                            return true;
                        case Commen.Delete:
                            var model2 = context.Product.FirstOrDefault(u => u.ProductID == customer.ProductID);
                            if (model2 != null)
                            {
                                context.Product.Remove(model2);
                                context.SaveChanges();
                            }
                            return true;
                    }
                    return false;
                }
            }
            return false;
        }
    }
}