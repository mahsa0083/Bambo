using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bambo.Models.Databsae;

namespace Bambo.Areas.Admin.Data
{
    public class ProductViewModel
    {
        public Product product { get; set; }
        public List<Product> products { get; set; }
        public List<Discount> discounts { get; set; }
        public Discount discount { get; set; }



        public bool IsAvaible { get; set; }
        public bool HasGarantee { get; set; }


        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }




        public List<Category> categories { get; set; }
        public Category category { get; set; }

        public List<Brand> brands { get; set; }
        public Brand brand { get; set; }


    }
}