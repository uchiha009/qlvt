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
    public class qlphieuxuatController : Controller
    {
        private vattudbcontext db = new vattudbcontext();

        // GET: /qlphieuxuat/
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

            var px = from s in db.phieuxuats
                     select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                px = px.Where(s => s.xuat.maphieu.Contains(searchString)
                                       || s.vattu.name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    px = px.OrderBy(s => s.vattuID);
                    break;
                case "Date":
                    px = px.OrderByDescending(s => s.ngaytaophieu);
                    break;
                default:  // Name ascending 
                    px = px.OrderBy(s => s.id);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(px.ToPagedList(pageNumber, pageSize));
        }

        // GET: /qlphieuxuat/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            phieuxuat phieuxuat = db.phieuxuats.Find(id);
            if (phieuxuat == null)
            {
                return HttpNotFound();
            }
            return View(phieuxuat);
        }

        // GET: /qlphieuxuat/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.vattuID = new SelectList(db.vattus, "id", "mavattu");
            ViewBag.xuatID = new SelectList(db.xuats, "id", "maphieu");
            return View();
        }

        // POST: /qlphieuxuat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,xuatID,vattuID,soluong,dongia,ngaytaophieu")] phieuxuat phieuxuat)
        {

            if (ModelState.IsValid)
            {
                var set = db.vattus.FirstOrDefault(s => s.id == phieuxuat.vattuID);
                if (set.soluong >= phieuxuat.soluong)
                {
                    var query = from i in db.vattus
                                where i.id == phieuxuat.vattuID
                                select i;

                    foreach (vattu i in query)
                    {
                        i.soluong = i.soluong - phieuxuat.soluong;
                    }
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {
                        return HttpNotFound();
                    }
                    db.phieuxuats.Add(phieuxuat);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }




                ViewBag.vattuID = new SelectList(db.vattus, "id", "mavattu", phieuxuat.vattuID);
                ViewBag.xuatID = new SelectList(db.xuats, "id", "maphieu", phieuxuat.xuatID);
                return View(phieuxuat);
            }

            return JavaScript("<script language='text/javascript'>alert('Số lượng yêu cầu ko phù hợp');</script>");
        }

        // GET: /qlphieuxuat/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            phieuxuat phieuxuat = db.phieuxuats.Find(id);
            if (phieuxuat == null)
            {
                return HttpNotFound();
            }
            ViewBag.vattuID = new SelectList(db.vattus, "id", "mavattu", phieuxuat.vattuID);
            ViewBag.xuatID = new SelectList(db.xuats, "id", "maphieu", phieuxuat.xuatID);
            return View(phieuxuat);
        }

        // POST: /qlphieuxuat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,xuatID,vattuID,soluong,dongia,ngaytaophieu")] phieuxuat phieuxuat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuxuat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.vattuID = new SelectList(db.vattus, "id", "mavattu", phieuxuat.vattuID);
            ViewBag.xuatID = new SelectList(db.xuats, "id", "maphieu", phieuxuat.xuatID);
            return View(phieuxuat);
        }

        // GET: /qlphieuxuat/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            phieuxuat phieuxuat = db.phieuxuats.Find(id);
            if (phieuxuat == null)
            {
                return HttpNotFound();
            }
            return View(phieuxuat);
        }

        // POST: /qlphieuxuat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            phieuxuat phieuxuat = db.phieuxuats.Find(id);
            db.phieuxuats.Remove(phieuxuat);
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
