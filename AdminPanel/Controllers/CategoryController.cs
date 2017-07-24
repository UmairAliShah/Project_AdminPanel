using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminPanel.Models;

namespace AdminPanel.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private Cuff_And_Cufflinks_DBEntities db = new Cuff_And_Cufflinks_DBEntities();

        // GET: /Category/
        public ActionResult Index()
        {
            return View(db.Table_Category.ToList());
        }

        // GET: /Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table_Category table_category = db.Table_Category.Find(id);
            if (table_category == null)
            {
                return HttpNotFound();
            }
            return View(table_category);
        }

        // GET: /Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="category_id,category_name")] Table_Category table_category)
        {
            if (ModelState.IsValid)
            {
                db.Table_Category.Add(table_category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(table_category);
        }

        // GET: /Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table_Category table_category = db.Table_Category.Find(id);
            if (table_category == null)
            {
                return HttpNotFound();
            }
            return View(table_category);
        }

        // POST: /Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="category_id,category_name")] Table_Category table_category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(table_category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(table_category);
        }

        // GET: /Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table_Category table_category = db.Table_Category.Find(id);
            if (table_category == null)
            {
                return HttpNotFound();
            }
            return View(table_category);
        }

        // POST: /Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Table_Category table_category = db.Table_Category.Find(id);
            db.Table_Category.Remove(table_category);
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
