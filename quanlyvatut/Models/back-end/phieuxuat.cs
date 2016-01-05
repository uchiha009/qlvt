using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace quanlyvatut.Models.back_end
{
    public class phieuxuat
    {
        public int id { get; set; }
        [Display(Name = "Mã xuất")]
        public int xuatID { get; set; }
        [Display(Name = "Mã vật tư")]
        public int vattuID { get; set; }
        [Display(Name = "Số lượng")]
        public int soluong { get; set; }
        [Display(Name = "Đơn giá")]
        public decimal dongia { get; set; }
        public decimal totalprice
        {
            get { return soluong * dongia; }

        }
        [DataType(DataType.Date), Display(Name = "Ngày tạo phiếu"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ngaytaophieu { get; set; }

        public virtual xuat xuat { get; set; }
        public virtual vattu vattu { get; set; }
    }
}