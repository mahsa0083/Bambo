using Bambo.Models.Databsae;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace Bambo.Areas.Admin.AdminServices.Admin
{
    public class AdminRepository : IRepository
    {
        public Tbl_Admin GetAdminById(int id)
        {
            using (var context = new BamboDataBase())
            {
                var Result = context.Tbl_Admin.SingleOrDefault(d => d.AdminId == id);
                return Result;
            }
        }

        public List<Tbl_Admin> GetAllAdmins()
        {

            using (var context = new BamboDataBase())
            {
                var res = context.Tbl_Admin.ToList();
                return res;
            }
        }

        public bool LoginAdmin(string username, string password)
        {

            if (username != null && password != null)
            {
                using (var context = new BamboDataBase())
                {
                    bool result = context.Tbl_Admin.Where(u => u.Username == username && u.Password == password).Any();
                    if (result == true)
                    {
                        return true;
                    }
                    return false;
                };
            }
            return false;

        }




        public bool UpdateAdmins(Tbl_Admin admin, int id, Status state)
        {
            using (var context = new BamboDataBase())
            {
                switch (state)
                {
                    case Status.Insert:
                        if (admin != null)
                        {
                            context.Tbl_Admin.Add(admin);
                            context.SaveChanges();
                            return true;
                        }
                        return false;
                    case Status.Update:
                        if (admin != null && id == 0)
                        {
                            var res = context.Tbl_Admin.SingleOrDefault(v => v.AdminId == id);
                            if (res != null)
                            {
                                res.Firstname = admin.Firstname;
                                res.Lastname = admin.Lastname;
                                res.Email = admin.Email;
                                res.Password = admin.Password;
                                res.Address = admin.Address;
                                res.Image = admin.Image;
                                res.Username = admin.Username;
                                res.Phone = admin.Phone;
                                res.Melicode = admin.Melicode;
                                context.SaveChanges();
                                return true;

                            }
                            return false;
                        }
                        return false;
                    case Status.Delete:
                        if (id != 0)
                        {
                            var res = context.Tbl_Admin.SingleOrDefault(v => v.AdminId == id);
                            if (res != null)
                            {
                                context.Tbl_Admin.Remove(res);
                                context.SaveChanges();
                                return true;
                            }
                            return false;

                        }

                        return false;



                }
                return false;
            }
        }
    }
}