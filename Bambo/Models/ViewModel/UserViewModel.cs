using Bambo.Models.Databsae;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bambo.Models;
using System.Web.Configuration;

namespace Bambo.Models.ViewModel
{
    public class UserViewModel
    {
       public Customer CurrentCustomer { get; set; }
        public bool IsLogin { get; set; }

        public List<Product> products { get; set; }
        public Product Product { get; set; }
        public Category Category { get; set; }
     
    }
}