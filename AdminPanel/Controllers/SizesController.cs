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
    public class SizesController : Controller
    {
        private Cuff_And_Cufflinks_DBEntities db = new Cuff_And_Cufflinks_DBEntities();

        // GET: /Sizes/
        public ActionResult Index()
        {
            var table_size = db.Table_Size.Include(t => t.Table_Category);
            return View(table_size.ToList());
        }

        // GET: /Sizes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table_Size table_size = db.Table_Size.Find(id);
            if (table_size == null)
            {
                return HttpNotFound();
            }
            return View(table_size);
        }

        // GET: /Sizes/Create
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(db.Table_Category, "category_id", "category_name");
            return View();
        }

        // POST: /Sizes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="s_id,s_size,category_id")] Table_Size table_size)
        {
            if (ModelState.IsValid)
            {
                db.Table_Size.Add(table_size);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.category_id = new SelectList(db.Table_Category, "category_id", "category_name", table_size.category_id);
            return View(table_size);
        }

        // GET: /Sizes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table_Size table_size = db.Table_Size.Find(id);
            if (table_size == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_id = new SelectList(db.Table_Category, "category_id", "category_name", table_size.category_id);
            return View(table_size);
        }

        // POST: /Sizes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="s_id,s_size,category_id")] Table_Size table_size)
        {
            if (ModelState.IsValid)
            {
                db.Entry(table_size).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.category_id = new SelectList(db.Table_Category, "category_id", "category_name", table_size.category_id);
            return View(table_size);
        }

        // GET: /Sizes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table_Size table_size = db.Table_Size.Find(id);
            if (table_size == null)
            {
                return HttpNotFound();
            }
            return View(table_size);
        }

        // POST: /Sizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Table_Size table_size = db.Table_Size.Find(id);
            db.Table_Size.Remove(table_size);
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
