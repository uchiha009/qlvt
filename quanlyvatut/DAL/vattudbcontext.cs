using quanlyvatut.Models.back_end;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace quanlyvatut.DAL
{
    public class vattudbcontext : DbContext
    {
        public virtual DbSet<nhap> nhaps { get; set; }
        public virtual DbSet<phieunhap> phieunhaps { get; set; }
        public virtual DbSet<xuat> xuats { get; set; }
        public virtual DbSet<phieuxuat> phieuxuats { get; set; }
        public virtual DbSet<vattu> vattus { get; set; }
    }
}