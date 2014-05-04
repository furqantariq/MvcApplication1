using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class CompanyController : Controller
    {
        //
        // GET: /Company/
        ISellerRepository sellerPool;

        public CompanyController(ISellerRepository iseller)
        {
            sellerPool = iseller;
        }

         public ActionResult Authenticate()
        {
            if (Session["User"] != null || Session["Company"] != null)
                return HttpNotFound();

            return View();
        }

        [HttpPost]
        public ActionResult Authenticate(Company seller)
        {
            if (!ModelState.IsValidField("Email") || !ModelState.IsValidField("Password")) return View();
            Company s = sellerPool.isSellerExist(seller);
            if (s != null)
            {
                Session["Company"] = s;
                return RedirectToAction("Index");
            }
            ViewBag.Error = "Wrong Email/Password";
            return View();
        }

        public ActionResult Create()
        {
            if (Session["User"] != null || Session["Company"] != null)
                return RedirectToAction("Home/Index");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Company seller)
        {
            if (ModelState.IsValid && sellerPool.check(seller.Email))
            {

                seller = sellerPool.Add(seller);
                Session["Company"] = seller;
                HttpPostedFileBase file = Request.Files[0];
                file.SaveAs(Server.MapPath(@"~\Images\Company\" + seller.Id + ".jpg"));
                return RedirectToAction("Index");
            }
            else{
                ViewBag.Error = "error";
                return View();
            }
        }

        public ActionResult Index()
        {
            if (Session["company"] == null) return HttpNotFound();
            Company seller = (Company)Session["Company"];
            return View(seller);
        }


        public ActionResult ViewOwnProd()
        {
            if (Session["company"] == null) return HttpNotFound();
            Company seller=(Company)Session["Company"];
            List<Product> pl = sellerPool.getSellerProd(seller.Id);
            return View(pl);
        }

        public ActionResult EditProduct(int id)
        {
            if (Session["company"] == null) return HttpNotFound();
            if (sellerPool.isAllowChanges(((Company)Session["Company"]).Id, id))
                return View(sellerPool.getProduct(id));
            else return HttpNotFound();
        }
       
        public ActionResult DeleteProduct(int id)
        {
            if (Session["company"] == null) return HttpNotFound();
            sellerPool.Delete(id);
            return RedirectToAction("ViewOwnProd");
        }


        [HttpPost]
        public ActionResult EditProduct(Product p)
        {
            if (ModelState.IsValid)
            {
                sellerPool.ChangeProduct(p);
                return RedirectToAction("ViewOwnProd");
            }
            return HttpNotFound();
        }

        public ActionResult AddProduct()
        {
            if (Session["company"] == null ) return HttpNotFound();
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product p)
        {
            if (ModelState.IsValid)
            {
                p.CompanyId = ((Company)Session["Company"]).Id;
                p = sellerPool.AddProduct(p);
                HttpPostedFileBase file = Request.Files[0];
                file.SaveAs(Server.MapPath(@"~\Images\products\" + p.Id + ".jpg"));
                return RedirectToAction("Index");
            }
            else
                return View();
        }
    }
}
