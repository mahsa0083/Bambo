using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bambo.Models.Databsae;

namespace Bambo.Areas.Admin.AdminServices.Admin
{
    public interface IRepository
    {
        List<Tbl_Admin> GetAllAdmins();
        Tbl_Admin GetAdminById(int id);
         bool UpdateAdmins(Tbl_Admin admin , int id  ,  Status state); 
        bool LoginAdmin(string username, string password );
        
    }
}