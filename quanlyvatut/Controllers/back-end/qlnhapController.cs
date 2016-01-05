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
    public class qlnhapController : Controller
    {
        private vattudbcontext db = new vattudbcontext();

        // GET: /qlnhap/
        [Authorize(Roles="Admin")]
        public ActionResult Index()
        {
            return View(db.nhaps.ToList());
        }

        // GET: /qlnhap/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nhap nhap = db.nhaps.Find(id);
            if (nhap == null)
            {
                return HttpNotFound();
            }
            return View(nhap);
        }

        // GET: /qlnhap/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /qlnhap/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,maphieu,name,day,creator")] nhap nhap)
        {
            if (ModelState.IsValid)
            {
                db.nhaps.Add(nhap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nhap);
        }

        // GET: /qlnhap/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nhap nhap = db.nhaps.Find(id);
            if (nhap == null)
            {
                return HttpNotFound();
            }
            return View(nhap);
        }

        // POST: /qlnhap/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,maphieu,name,day,creator")] nhap nhap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhap);
        }

        // GET: /qlnhap/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nhap nhap = db.nhaps.Find(id);
            if (nhap == null)
            {
                return HttpNotFound();
            }
            return View(nhap);
        }

        // POST: /qlnhap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            nhap nhap = db.nhaps.Find(id);
            db.nhaps.Remove(nhap);
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
