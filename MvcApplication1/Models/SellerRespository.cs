using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Models
{
    public class SellerRespository : ISellerRepository
    {
        public Company Add(Company seller)
        {
            var cx = new DB();
            int id;
            try
            {
                id = cx.Companies.Max(x => x.Id) + 1;

            }
            catch (Exception e)
            {
                id = 0;
            }
            seller.Id = id;
            cx.Companies.Add(seller);
            cx.SaveChanges();
            return seller;
        }
        public void Delete(int id)
        {
            var cx = new DB();
            cx.Products.Remove(cx.Products.Find(id));
            cx.SaveChanges();
        }

        public Company isSellerExist(Company seller)
        {
            var cx = new DB();
            Company r;
            try
            {
                r = cx.Companies.First(x => (x.Email.Equals(seller.Email) && x.Password.Equals(seller.Password)));
            }
            catch(Exception e)
            {   r=null;
            }
            return r;
        }

      public bool isAllowChanges(int cid, int pid)
        {
            var cx = new DB();
            if (cx.Products.Where(x => (x.Id == pid) && (x.CompanyId == cid)).Count() > 0)
                return true;
            else return false;
        }

        public void ChangeProduct(Product P)
        {
            var cx = new DB();
            cx.Products.Attach(P);
            cx.Entry(P).State = EntityState.Modified;
            cx.SaveChanges();
        }

        public Product AddProduct(Product p)
        {
            var cx = new DB();
            int id;
            try
            {
                id = cx.Products.Max(x => x.Id) + 1;

            }
            catch (Exception e)
            {
                id = 0;
            }
            p.Id = id;
            cx.Products.Add(p);
            cx.SaveChanges();
            return p;
        }
        public Product getProduct(int id)
        {
            var cx = new DB();
            return cx.Products.First(x => x.Id == id);
        }

        public List<Product> getSellerProd(int cid)
        {
            var cx = new DB();
            List<Product> r;
            try
            {
                r = cx.Products.Where(x => x.CompanyId == cid).ToList();
            }
            catch (Exception e)
            {
                r = null; 
            }
            return r;
        }
        public bool check(String email)
        {
            var cx = new DB();
            if (cx.Companies.Where(x => x.Email.Equals(email)).Count() == 0)
                return true;
            else return false;

        }
    }
}