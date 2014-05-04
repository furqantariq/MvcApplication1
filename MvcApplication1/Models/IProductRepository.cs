using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcApplication1.Models;

namespace MvcApplication1.Models
{
    public interface IProductRepository
    {
        List<Product> getAll();
        Product get(int id);
        List<Product> getRecent();
        List<Company> getBrands();
        string getCompany(int id);
        List<Product> search(string search);


    }
}
