using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace quanlyvatut.Models.back_end
{
    public class nhap
    {
        public int id { get; set; }
        [Display(Name="Mã nhập")]
        public string maphieu { get; set; }
        [Display(Name = "Tên nhập")]
        public string name { get; set; }
        [DataType(DataType.Date), Display(Name = "Ngày nhập"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime day { get; set; }
        [Display(Name = "Người nhập")]
        public string creator { get; set; }

        public virtual ICollection<phieunhap> phieunhaps { get; set; }
    }
   
}