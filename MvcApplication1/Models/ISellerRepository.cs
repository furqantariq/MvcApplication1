using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcApplication1.Models
{
    public interface ISellerRepository
    {
        Company isSellerExist(Company seller);
        Company Add(Company user);
        bool isAllowChanges(int cid, int pid);
        void ChangeProduct(Product P);
        Product AddProduct(Product p);
        Product getProduct(int id);
        List<Product> getSellerProd(int cid);
        void Delete(int id);
        bool check(string email);

    }
}