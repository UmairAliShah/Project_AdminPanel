using Cuffs_And_Cufflinks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace project.Controllers
{
    public class productsController : Controller
    {
        Cuff_And_Cufflinks_DBEntities db = new Cuff_And_Cufflinks_DBEntities();

     
        

        // GET: products
        public ActionResult products(int? id)
        {
            if (id == 1)
            {
                int id_Shirt = db.Table_Category.Where(a => a.category_name.ToLower() == "shirts").Select(i => i.category_id).Single();
                var obj = db.Table_products.Where(m  => m.category_id == id_Shirt).ToList();
                ViewBag.Message = "Shirts";
                return View(obj);
                
            }
            else if (id == 2)
            {
                int id_Pent = db.Table_Category.Where(a => a.category_name.ToLower() == "pents").Select(i => i.category_id).Single();
                var obj = db.Table_products.Where(m => m.category_id == id_Pent).ToList();

                ViewBag.Message = "Pents";
                return View(obj);
            
            }
            return View(new List<Cuffs_And_Cufflinks.Models.Table_products>());
          
        }

        public ActionResult products_detail(int? id, int? val)
        {
            
            Table_products table_products = db.Table_products.Where(m => m.p_id == id).Single();
           
            ViewBag.c_id = new SelectList(db.Table_Colors.Where(m => m.category_id == val), "c_id", "c_name", table_products.c_id);
            ViewBag.s_id = new SelectList(db.Table_Size.Where(m => m.category_id == val), "s_id", "s_size", table_products.s_id);
            return View(table_products);
        
        
        }

       
    }
}