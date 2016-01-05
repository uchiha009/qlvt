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
    public class qlphieunhapController : Controller
    {
        vattudbcontext db = new vattudbcontext();

        // GET: /qlphieunhap/
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

            var pn = from s in db.phieunhaps
                     select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                pn = pn.Where(s => s.nhap.maphieu.Contains(searchString)
                                       || s.vattu.name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    pn = pn.OrderBy(s => s.vattuID);
                    break;
                case "Date":
                    pn = pn.OrderByDescending(s => s.ngaytaophieu);
                    break;
                default:  // Name ascending 
                    pn = pn.OrderBy(s => s.id);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(pn.ToPagedList(pageNumber, pageSize));
        }

        // GET: /qlphieunhap/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            phieunhap phieunhap = db.phieunhaps.Find(id);
            if (phieunhap == null)
            {
                return HttpNotFound();
            }
            return View(phieunhap);
        }

        // GET: /qlphieunhap/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.nhapID = new SelectList(db.nhaps, "id", "maphieu");
            ViewBag.vattuID = new SelectList(db.vattus, "id", "name");
            return View();
        }

        // POST: /qlphieunhap/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nhapID,vattuID,soluong,dongia,ngaytaophieu")] phieunhap phieunhap)
        {
            if (ModelState.IsValid)
            {
                var query = from i in db.vattus
                            where i.id == phieunhap.vattuID
                            select i;
                foreach (vattu i in query)
                {
                    i.soluong = i.soluong + phieunhap.soluong;
                }
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return HttpNotFound();
                }
                db.phieunhaps.Add(phieunhap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.nhapID = new SelectList(db.nhaps, "id", "maphieu", phieunhap.nhapID);
            ViewBag.vattuID = new SelectList(db.vattus, "id", "name", phieunhap.vattuID);
            return View(phieunhap);
        }

        // GET: /qlphieunhap/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            phieunhap phieunhap = db.phieunhaps.Find(id);
            if (phieunhap == null)
            {
                return HttpNotFound();
            }
            ViewBag.nhapID = new SelectList(db.nhaps, "id", "maphieu", phieunhap.nhapID);
            ViewBag.vattuID = new SelectList(db.vattus, "id", "name", phieunhap.vattuID);
            return View(phieunhap);
        }

        // POST: /qlphieunhap/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nhapID,vattuID,soluong,dongia,ngaytaophieu")] phieunhap phieunhap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieunhap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.nhapID = new SelectList(db.nhaps, "id", "maphieu", phieunhap.nhapID);
            ViewBag.vattuID = new SelectList(db.vattus, "id", "name", phieunhap.vattuID);
            return View(phieunhap);
        }

        // GET: /qlphieunhap/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            phieunhap phieunhap = db.phieunhaps.Find(id);
            if (phieunhap == null)
            {
                return HttpNotFound();
            }
            return View(phieunhap);
        }

        // POST: /qlphieunhap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            phieunhap phieunhap = db.phieunhaps.Find(id);
            db.phieunhaps.Remove(phieunhap);
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
