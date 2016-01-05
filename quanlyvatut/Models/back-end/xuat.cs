using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace quanlyvatut.Models.back_end
{
    public class xuat
    {
        public int id { get; set; }
        [Display(Name = "Mã xuất")]
        public string maphieu { get; set; }
        [Display(Name = "Tên xuất")]
        public string name { get; set; }
        [DataType(DataType.Date), Display(Name = "Ngày xuất"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime day { get; set; }
        [Display(Name = "Người xuất")]
        public string creator { get; set; }

        public virtual ICollection<phieuxuat> phieuxuats { get; set; }
    }
}