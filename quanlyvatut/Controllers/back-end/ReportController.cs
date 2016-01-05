using quanlyvatut.DAL;
using quanlyvatut.Models.back_end;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace quanlyvatut.Controllers.back_end
{
    public class ReportController : Controller
    {
        vattudbcontext db = new vattudbcontext();
        //
        // GET: /Report/
        public ActionResult baocaonhap()
        {

            ViewBag.Date = DateTime.Now.ToShortDateString();
            var a = DateTime.Now.Date;
            var query = from i in db.phieunhaps
                        where i.ngaytaophieu == a
                        select i;
            return View(query.ToList());

        }


        [Authorize]
        public ActionResult baocaoxuat()
        {

            ViewBag.Date = DateTime.Now.ToShortDateString();
            var a = DateTime.Now.Date;
            var query = from i in db.phieuxuats
                        where i.ngaytaophieu == a
                        select i;
            return View(query.ToList());

        }
        [Authorize]
        public ActionResult baocaochung()
        {
            ViewBag.countnhap = db.phieunhaps.Count();
            ViewBag.countxuat = db.phieuxuats.Count();
            var grandtotalnhap = db.phieunhaps.AsEnumerable().Sum(s => s.totalprice);
            ViewBag.totalnhap = grandtotalnhap;
            var grandtotalxuat = db.phieuxuats.AsEnumerable().Sum(s => s.totalprice);
            ViewBag.totalxuat = grandtotalxuat;
            var data = db.vattus.Where(q => q.soluong < 5).ToList();
            return View(data);
        }
        public ActionResult _indexvt()
        {
            var data = db.vattus.ToList();
            return PartialView("_indexvt", data);
        }
    }
}