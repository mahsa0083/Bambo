using Bambo.Models.Databsae;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bambo.Areas.Admin.AdminServices
{
    public interface IRepo
    {
        #region Product
        List<Product> GetAllProduct();
        Product GetProductById(int id);
        bool UpdateProduct(Product customer, Commen state);

        List<Category> GetAllCategories();
        Category GetCategoryById(int id);


        List<Brand> GetAllBrand();
        Brand GetBrandById(int id);




        #endregion
    }
}