using Cuffs_And_Cufflinks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace project.Controllers
{
    public class HomeController : Controller
    {

        Cuff_And_Cufflinks_DBEntities db = new Cuff_And_Cufflinks_DBEntities();

        public ActionResult Index()
        {

            int id_Shirt = db.Table_Category.Where(a => a.category_name.ToLower() == "shirts").Select(i => i.category_id).Single();
            int id_Pent = db.Table_Category.Where(a => a.category_name.ToLower() == "pents").Select(i => i.category_id).Single();

            ViewBag.Category_Shirt = id_Shirt;
            ViewBag.Category_Pent = id_Pent;


            var products_list = db.Table_products.ToList();
            return View(products_list);
        }
       

        

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpPost]
        public ActionResult Contact(Contact_Us obj)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    WebMail.Send(

                     "umairali43230@gmail.com",
                      obj.Subject,
                      "<b>Message:  <br/></b>" + obj.Message + "<br/><br/>" + obj.Name + " Mail address is: " + obj.Email,
                      null,
                      null,
                      null,
                      true,
                      null,
                      null,
                      null,
                      null,
                      null,
                      obj.Email
                     );
                    return RedirectToAction("Index");

                }
            }
            catch (Exception)
            {

                ViewBag.Error = "Problems sending Email!";


            }
          


            

            return View();
        }
        public ActionResult FAQ()
        {

            return View();
        
        
        }
   
       
    }
}