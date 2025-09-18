using Bambo.Models.Databsae;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bambo.Serives
{
    public interface IRepository
    {
        #region Customer
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);
        bool UpdateCustomer(Customer customer, Commen state);
        bool CheckLoginUser(string phone, string password);
        bool UpdateVerificationCode(string phone, string verificationCode);
        List<Customer> CheckVerificationCode(string phoneNumber, string verificationCode);
        bool CheckUserbynumber(string number);
        #endregion





    }
}