using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Models
{
    public class ProductRepository : IProductRepository
    {
        public List<Product> getAll()
        {
            var cx = new DB();
            return cx.Products.ToList();
        }

        public Product get(int id)
        {
            var cx = new DB();
            return cx.Products.Find(id);
        }
        public List<Product> getRecent()
        {
            var cx = new DB();
            List<Product> result = new List<Product>();
            int max;
            try
            {
                max = cx.Products.Max(x => x.Id);
            }catch(Exception e)
            {
                max = 0;
            }
            for (int i = 0; max-i >= 0 && i < 8; ++i)
            {
                Product p = cx.Products.Find(max - i);
                result.Add(p);
            }
            return result;
        }
        public List<Company> getBrands()
        {
            var cx = new DB();
            List<Company> result = new List<Company>();
            int max;
            try
            {
                max = cx.Companies.Max(x => x.Id);
            }
            catch (Exception e)
            {
                max = 0;
            } 
            for (int i = 0; max - i >= 0 && i < 5; ++i)
            {
                result.Add(cx.Companies.Find(max - i));
            }
            return result;
        }

        public string getCompany(int id)
        {
            var cx = new DB();
            return cx.Companies.First(x => x.Id == id).Name;
        }
        public List<Product> search(string search)
        {
            var cx = new DB();
            return cx.Products.Where(x => x.Name.Equals(search)).ToList();
        }

    }
}