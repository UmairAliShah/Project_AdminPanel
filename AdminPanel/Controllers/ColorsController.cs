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
    public class ColorsController : Controller
    {
        private Cuff_And_Cufflinks_DBEntities db = new Cuff_And_Cufflinks_DBEntities();

        // GET: /Colors/
        public ActionResult Index()
        {
            var table_colors = db.Table_Colors.Include(t => t.Table_Category);
            return View(table_colors.ToList());
        }

        // GET: /Colors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table_Colors table_colors = db.Table_Colors.Find(id);
            if (table_colors == null)
            {
                return HttpNotFound();
            }
            return View(table_colors);
        }

        // GET: /Colors/Create
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(db.Table_Category, "category_id", "category_name");
            return View();
        }

        // POST: /Colors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="c_name,c_id,category_id")] Table_Colors table_colors)
        {
            if (ModelState.IsValid)
            {
                db.Table_Colors.Add(table_colors);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.category_id = new SelectList(db.Table_Category, "category_id", "category_name", table_colors.category_id);
            return View(table_colors);
        }

        // GET: /Colors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table_Colors table_colors = db.Table_Colors.Find(id);
            if (table_colors == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_id = new SelectList(db.Table_Category, "category_id", "category_name", table_colors.category_id);
            return View(table_colors);
        }

        // POST: /Colors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="c_name,c_id,category_id")] Table_Colors table_colors)
        {
            if (ModelState.IsValid)
            {
                db.Entry(table_colors).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.category_id = new SelectList(db.Table_Category, "category_id", "category_name", table_colors.category_id);
            return View(table_colors);
        }

        // GET: /Colors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table_Colors table_colors = db.Table_Colors.Find(id);
            if (table_colors == null)
            {
                return HttpNotFound();
            }
            return View(table_colors);
        }

        // POST: /Colors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Table_Colors table_colors = db.Table_Colors.Find(id);
            db.Table_Colors.Remove(table_colors);
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
