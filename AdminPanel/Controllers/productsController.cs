using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminPanel.Models;
using System.IO;

namespace AdminPanel.Controllers
{
    [Authorize]
    public class productsController : Controller
    {
        private Cuff_And_Cufflinks_DBEntities db = new Cuff_And_Cufflinks_DBEntities();

        // GET: /products/
        public ActionResult Index()
        {
            var table_products = db.Table_products.Include(t => t.Table_Category).Include(t => t.Table_Colors).Include(t => t.Table_Size);
            return View(table_products.ToList());
        }

        // GET: /products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table_products table_products = db.Table_products.Find(id);
            if (table_products == null)
            {
                return HttpNotFound();
            }
            return View(table_products);
        }

        // GET: /products/Create
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(db.Table_Category, "category_id", "category_name");
            ViewBag.c_id = new SelectList(db.Table_Colors, "c_id", "c_name");
            ViewBag.s_id = new SelectList(db.Table_Size, "s_id", "s_size");
            return View();
        }

        // POST: /products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Table_products table_products)
        {
            if (ModelState.IsValid)
            {
                String fileName = Path.GetFileNameWithoutExtension(table_products.ImageFile.FileName);
                String extension = Path.GetExtension(table_products.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                table_products.p_img = "~/Images/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                table_products.ImageFile.SaveAs(fileName);

                db.Table_products.Add(table_products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.category_id = new SelectList(db.Table_Category, "category_id", "category_name", table_products.category_id);
            ViewBag.c_id = new SelectList(db.Table_Colors, "c_id", "c_name", table_products.c_id);
            ViewBag.s_id = new SelectList(db.Table_Size, "s_id", "s_size", table_products.s_id);
            return View(table_products);
        }

        // GET: /products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table_products table_products = db.Table_products.Find(id);
            if (table_products == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_id = new SelectList(db.Table_Category, "category_id", "category_name", table_products.category_id);
            ViewBag.c_id = new SelectList(db.Table_Colors, "c_id", "c_name", table_products.c_id);
            ViewBag.s_id = new SelectList(db.Table_Size, "s_id", "s_size", table_products.s_id);
            return View(table_products);
        }

        // POST: /products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Table_products table_products)
        {
            if (ModelState.IsValid)
            {

                if (table_products.ImageFile != null)
                {
                    String fileName = Path.GetFileNameWithoutExtension(table_products.ImageFile.FileName);
                    String extension = Path.GetExtension(table_products.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    table_products.p_img = "~/Images/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    table_products.ImageFile.SaveAs(fileName);


                }

                db.Entry(table_products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.category_id = new SelectList(db.Table_Category, "category_id", "category_name", table_products.category_id);
            ViewBag.c_id = new SelectList(db.Table_Colors, "c_id", "c_name", table_products.c_id);
            ViewBag.s_id = new SelectList(db.Table_Size, "s_id", "s_size", table_products.s_id);
            return View(table_products);
        }

        // GET: /products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table_products table_products = db.Table_products.Find(id);
            if (table_products == null)
            {
                return HttpNotFound();
            }
            return View(table_products);
        }

        // POST: /products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Table_products table_products = db.Table_products.Find(id);
            db.Table_products.Remove(table_products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
