using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cuffs_And_Cufflinks.Models;
using System.Web.Helpers;
using System.Text;
namespace Cuffs_And_Cufflinks.Controllers
{

    public class Shopping_CartController : Controller
    {

        Cuff_And_Cufflinks_DBEntities db = new Cuff_And_Cufflinks_DBEntities();
        public ActionResult Index()
        {
            return View();
        }
      

        private int isExisting(int id, string size, string color)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            try
            {
                for (int i = 0; i < cart.Count; i++)
                {
                    if (cart[i].Products.p_id == id && cart[i].Size == size && cart[i].Color == color)
                        return i;
                }
                return -1;
            
            }
            catch(Exception){

                return -1;
            }
           
        }
        private int isExisting(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            try
            {
                for (int i = 0; i < cart.Count; i++)
                {
                    if (cart[i].Products.p_id == id)
                        return i;
                }
                return -1;

            }
            catch (Exception)
            {

                return -1;
            }

        }

        public ActionResult Delete(int? id)
        {
            int index = isExisting((int)id);
            List<Item> cart = (List<Item>)Session["cart"];
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("ViewCart");
        }

      
        public ActionResult OrderNow(Table_products model)
        {

            string size = db.Table_Size.Where(s => s.s_id == model.s_id).Select(a => a.s_size).Single();
            string color = db.Table_Colors.Where(s => s.c_id == model.c_id).Select(a => a.c_name).Single(); 
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item(model, 1, size, color));
                Session["cart"] = cart;
            }
            else
            {

                List<Item> cart = (List<Item>)Session["cart"];

                int index = -1;
               
                    index = isExisting((int)model.p_id, size, color);
             
                if (index == -1)
                {
                    cart.Add(new Item(model , 1, size, color));

                }
                else
                {

                    cart[index].Quantity++;
                }
                Session["cart"] = cart;

            }
            ViewBag.category_id = new SelectList(db.Table_Category, "category_id", "category_name", model.category_id);
            ViewBag.c_id = new SelectList(db.Table_Colors, "c_id", "c_name", model.c_id);
            ViewBag.s_id = new SelectList(db.Table_Size, "s_id", "s_size", model.s_id);
            ViewBag.add = "add";
            return RedirectToAction("ViewCart");
        }

        public ActionResult EmptyCart()
        {
            List<Item> cart = (List<Item>)Session["cart"];

            cart = null;
            ViewBag.message = "empty";
            Session["cart"] = cart;

            return View("add_to_cart");
           
        
        }

        public ActionResult ViewCart()
        {
            List<Item> cart = (List<Item>)Session["cart"];
            if (cart == null)
            {
                ViewBag.message = "empty";
                cart = null; 
            }
            else if (cart.Count == 0)
            {
                ViewBag.message = "empty";
                cart = null;
            }
            else
            {
                ViewBag.message = "non-empty";
            }
            Session["cart"] = cart;
            return View("add_to_cart");
        }
        
        [Authorize]
        public ActionResult checkout()
        {
           
            return View(); 
        
        }

        [HttpPost]
        public ActionResult checkout(Order_Model obj)
        {

            List<Item> cart = (List<Item>)Session["cart"];
            decimal totalSum = 0;
            StringBuilder body = new StringBuilder();
            try { 
            
               if(cart.Count() != 0)
               {
               

                body.AppendLine("Order:");
                body.AppendLine("<table  font-family= arial, sans-serif, border-collapse = collapse, width= 100% >");
                body.AppendLine("<tr>");
                body.AppendLine("<td><b>Article</b></td>");
                body.AppendLine("<td><b>Title</b></td>");
                body.AppendLine("<td><b>Size</b></td>");
                body.AppendLine("<td><b>Color</b></td>");
                body.AppendLine("<td><b>Quantity</b></td>");
                body.AppendLine("<td><b>Price</b></td>");
                body.AppendLine("</tr>");


                foreach (Item items in (List<Item>)Session["cart"])
                {

                    totalSum = totalSum + Convert.ToDecimal(items.Products.p_price * items.Quantity);
                    body.AppendLine("<tr>");
                    body.AppendLine("<td>");
                    body.AppendLine(items.Products.p_article_no);
                    body.AppendLine("</td>");
                    body.AppendLine("<td>");
                    body.AppendLine(items.Products.p_title);
                    body.AppendLine("</td>");
                    body.AppendLine("<td>");
                    body.AppendLine("size");
                    body.AppendLine("</td>");
                    body.AppendLine("<td>");
                    body.AppendLine("color");
                    body.AppendLine("</td>");
                    body.AppendLine("<td>");
                    body.AppendLine("quantity");
                    body.AppendLine("</td>");
                    body.AppendLine("<td>");
                    body.AppendLine(items.Products.p_price.ToString());
                    body.AppendLine("</td>");

                }
                body.AppendLine("</table>");


            }
            
            
            }
            catch(Exception ){
            
            
            
            
            
            }
            
        
           

            try
            {
                if (ModelState.IsValid)
                {

                    WebMail.Send(

                     "umairali43230@gmail.com",
                      "New Order",
                      body + "<br/><br/>" + "<b>" + "Total Amount: "  + "</b>" + totalSum + "<br/><br/>" + "<b>" + "Information of Buyer:" + "</b>" + "<br/>" + "Name: " + obj.First_Name + " " + obj.Last_Name + "<br/>" +
                      "Email: " + obj.Email + "<br/>" + "Phone: " + obj.Phone + "<br/>" + "City: " + obj.City + "<br/>" +
                      "Address: " + obj.Address + "<br/><br/>",
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
                   
                    return RedirectToAction("Index", "Home");

                }
            }
            catch (Exception)
            {

                ViewBag.Error = "Problems sending Email!";


            }





            return View();




           
        
        
        }
	}
}