using Bambo.Models.Databsae;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Bambo.Serives
{
    public class ServiceRepo : IRepository
    {
        public bool CheckLoginUser(string phone, string password)
        {
            if (phone != null && password != null)
            {
                using (var context = new BamboDataBase())
                {
                    bool result = context.Customer.Where(u => u.Phone == phone && u.PasswordHashed == password).Any();
                    if (result == true)
                    {
                        return true;
                    }
                    return false;
                };
            }
            return false;


        }

        public bool CheckUserbynumber(string number)
        {
            if (number != null)
            {
                try
                {
                    using (var  context = new BamboDataBase())
                    {
                        bool result = context.Customer.Where(u => u.Phone == number).Any();
                        if (result == true)
                        {
                            return true;
                        }
                    }
                    return true;
                }
                catch { return false; }
            }
            else
            {
                return false;
            }
        }

        public List<Customer> CheckVerificationCode(string phoneNumber, string verificationCode)
        {
            if (verificationCode != null)
            {
                using (var context = new BamboDataBase())
                {
                    return context.Customer.Where(u => u.Phone == phoneNumber && u.VerficationCode == verificationCode).ToList();
                }
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            using (var context = new BamboDataBase())
            {
                return context.Customer .ToList();
            }
        }

        

        public Customer GetCustomerById(int id)
        {
            if (id != 0 )
            {
                using (var context = new BamboDataBase())
                {
                    var model = context.Customer.FirstOrDefault(u => u.CustomerId == id);
                    return model;
                }
            }
            else return null;

        }

        

        public bool UpdateCustomer(Customer customer, Commen state)
        {
            if (customer != null)
            {
                using (var context = new BamboDataBase())
                {
                    switch (state)
                    {
                        case Commen.Insert:
                            context.Customer.Add(customer);

                            context.SaveChanges();
                            return true;


                        case Commen.Update:
                            var model = context.Customer.FirstOrDefault(u => u.CustomerId == customer.CustomerId);
                            model.Firstname = customer.Firstname;
                            model.Lastname = customer.Lastname;
                            model.Email = customer.Email;
                            model.PasswordHashed = customer.PasswordHashed;
                            model.Phone = customer.Phone;
                            context.SaveChanges();
                            return true;
                        case Commen.Delete:
                            var model2 = context.Customer.FirstOrDefault(u => u.CustomerId == customer.CustomerId);
                            if (model2 != null)
                            {
                                context.Customer.Remove(model2);
                                context.SaveChanges();
                            }
                            return true;
                    }
                    return false;
                }
            }
            return false;
        }

        public bool UpdateVerificationCode(string phone, string verificationCode)
        {
            if (phone != null && verificationCode != null)
            {
                using (var context = new BamboDataBase())
                {
                    var res = context.Customer.Where(u => u.Phone == phone).FirstOrDefault();
                    if (res != null)
                    {
                        res.VerficationCode = verificationCode;
                        res.Phone = phone;
                    }
                    context.SaveChanges();
                    return true;

                }
            }
            else
            {
                return false;
            }
        }
    }
}