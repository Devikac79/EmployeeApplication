using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeApplication.DAL;
using EmployeeApplication.Models;

namespace EmployeeApplication.Controllers
{
    public class ProductController : Controller
    {
        Product_DAL Product_DAL=new Product_DAL();
        // GET: Product
        public ActionResult Index()
        {
            
            var productList=Product_DAL.GetAllProducts();

            if (productList.Count == 0)
            {
                TempData["InfoMessage"] = "Currently products not available in the database";
            }

            return View(productList);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            try
            {

                var product = Product_DAL.GetProductsByID(id).FirstOrDefault();
                if (product == null)
                {
                    TempData["InfoMessage"] = "Product not available " + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                // TODO: Add insert logic here
                bool IsInserted = false;
                if(ModelState.IsValid)
                {
                    IsInserted = Product_DAL.InsertProduct(product);
                if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Product details saved successfully";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to save the product Details";

                    }
                }

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {

                TempData["ErrorMessage"]=ex.Message;
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var products=Product_DAL.GetProductsByID(id).FirstOrDefault();

            if (products == null)
            {
                TempData["InfoMessage"] = "Product not available " + id.ToString();
                return RedirectToAction("Index");
            }
            return View(products);
        }

        // POST: Product/Edit/5
        [HttpPost,ActionName("Edit")]
        public ActionResult Update(Product product)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid) 
                { 
                    bool IsUpdated=Product_DAL.UpdateProduct(product);
                    if(IsUpdated)
                    {
                        TempData["SuccessMessage"] = "Product details saved successfully...";

                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Product is already available/unable to update the product details";

                    }
                }


                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var product = Product_DAL.GetProductsByID(id).FirstOrDefault();
                if (product == null)
                {
                    TempData["InfoMessage"] = "Product not available with id" + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // POST: Product/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConformation(int id)
        {
            try
            {
                // TODO: Add delete logic here
                string result = Product_DAL.DeleteProduct(id);
                if(result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = "Product deleted successfully";
                }
                else
                {
                    TempData["Error Message"] = "Product not deleted.";

                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }
    }
}
