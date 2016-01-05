using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace quanlyvatut.Models.back_end
{
    public class vattu
    {
        public int id { get; set; }
        [Display(Name = "Mã vật tư")]
        public string mavattu { get; set; }
        [Display(Name = "Tên vật tư")]
        public string name { get; set; }
        [Display(Name = "Số lượng")]
        public int soluong { get; set; }
        [Display(Name = "Đơn giá")]
        public decimal dongia { get; set; }
        [Display(Name = "Ngày khởi tạo")]
        [DataType(DataType.Date),DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ngaytao { get; set; }

        public virtual ICollection<phieunhap> phieunhaps { get; set; }
        public virtual ICollection<phieuxuat> phieuxuats { get; set; }
    }
}