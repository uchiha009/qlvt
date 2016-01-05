using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using quanlyvatut.Models.back_end;
using quanlyvatut.DAL;

namespace quanlyvatut.Controllers.back_end
{
    public class qlxuatController : Controller
    {
        private vattudbcontext db = new vattudbcontext();

        // GET: /qlxuat/
        [Authorize]
        public ActionResult Index()
        {
            return View(db.xuats.ToList());
        }

        // GET: /qlxuat/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            xuat xuat = db.xuats.Find(id);
            if (xuat == null)
            {
                return HttpNotFound();
            }
            return View(xuat);
        }

        // GET: /qlxuat/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /qlxuat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,maphieu,name,day,creator")] xuat xuat)
        {
            if (ModelState.IsValid)
            {
                db.xuats.Add(xuat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(xuat);
        }

        // GET: /qlxuat/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            xuat xuat = db.xuats.Find(id);
            if (xuat == null)
            {
                return HttpNotFound();
            }
            return View(xuat);
        }

        // POST: /qlxuat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,maphieu,name,day,creator")] xuat xuat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(xuat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(xuat);
        }

        // GET: /qlxuat/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            xuat xuat = db.xuats.Find(id);
            if (xuat == null)
            {
                return HttpNotFound();
            }
            return View(xuat);
        }

        // POST: /qlxuat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            xuat xuat = db.xuats.Find(id);
            db.xuats.Remove(xuat);
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
