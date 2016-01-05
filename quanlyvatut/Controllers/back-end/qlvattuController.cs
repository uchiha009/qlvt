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
using PagedList;

namespace quanlyvatut.Controllers.back_end
{
    public class qlvattuController : Controller
    {
        private vattudbcontext db = new vattudbcontext();

        // GET: /qlvattu/
        [Authorize]
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var vattu = from s in db.vattus
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                vattu = vattu.Where(s => s.name.Contains(searchString)
                                       || s.mavattu.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    vattu = vattu.OrderBy(s => s.name);
                    break;
                case "Date":
                    vattu = vattu.OrderByDescending(s => s.ngaytao);
                    break;
                default:  // Name ascending 
                    vattu = vattu.OrderBy(s => s.id);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(vattu.ToPagedList(pageNumber, pageSize));
        }

        // GET: /qlvattu/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vattu vattu = db.vattus.Find(id);
            if (vattu == null)
            {
                return HttpNotFound();
            }
            return View(vattu);
        }

        // GET: /qlvattu/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /qlvattu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,mavattu,name,soluong,dongia,ngaytao")] vattu vattu)
        {
            if (ModelState.IsValid)
            {
                db.vattus.Add(vattu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vattu);
        }

        // GET: /qlvattu/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vattu vattu = db.vattus.Find(id);
            if (vattu == null)
            {
                return HttpNotFound();
            }
            return View(vattu);
        }

        // POST: /qlvattu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,mavattu,name,soluong,dongia,ngaytao")] vattu vattu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vattu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vattu);
        }

        // GET: /qlvattu/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vattu vattu = db.vattus.Find(id);
            if (vattu == null)
            {
                return HttpNotFound();
            }
            return View(vattu);
        }

        // POST: /qlvattu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            vattu vattu = db.vattus.Find(id);
            db.vattus.Remove(vattu);
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
