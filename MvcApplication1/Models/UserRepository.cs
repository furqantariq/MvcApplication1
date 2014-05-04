using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MvcApplication1.Models
{
  
    public class UserRepository : IUserRepository
    {
        public Account Add(Account user)
        {
            var cx = new DB();
            int id;
            try
            {
                id = cx.Accounts.Max(x => x.Id)+1;

            }
            catch (Exception e)
            {
                id  = 0;
            }

            user.Id = id;
            cx.Accounts.Add(user);
            cx.SaveChanges();
            return user;
        }
        public Account isUserExist(Account user)
        {
            var cx = new DB();
              Account r;
            try
            {
               r= cx.Accounts.First(x => (x.Username.Equals(user.Username) && x.Password.Equals(user.Password)));
            }
            catch (Exception)
            {
                r = null;
            }
            return r;
         }

        public Account ChangeUser(Account user)
        {
            var cx = new DB();
            cx.Accounts.Attach(user);
            cx.Entry(user).State = EntityState.Modified;
            cx.SaveChanges();
            return user;
           
        }
        public bool check(String username)
        {
            var cx = new DB();
            if (cx.Accounts.Where(x => x.Username.Equals(username)).Count() == 0)
                return true;
            else return false;

        }
    }
}