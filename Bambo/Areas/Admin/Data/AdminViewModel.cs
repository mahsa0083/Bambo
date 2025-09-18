using Bambo.Models.Databsae;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Bambo.Areas.Admin.Data
{
    public class AdminViewModel
    {

        public Tbl_Admin Admin { get; set; }
        public List<Tbl_Admin> AdminList { get; set; }
        public bool IsLogin { get; set; }
        public List <Role> Roles { get; set; }
        public Role Role { get; set; }
        public List<Address> addresses { get; set; }

    }
}