using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace quanlyvatut.Models.front_end
{
    public class workPN
    {

        public string maphieu { get; set; }
        public string tenphieu { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ngaytao { get; set; }
        public string nguoitao { get; set; }
        public string mavattu { get; set; }
        public int soluong { get; set; }
        public decimal dongia { get; set; }
    }
}