using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class HTNhomNguoiDungModel
    {
        public Guid IIDNhomNguoidung { get; set; }
        public Guid IIDMaNguoiDung { get; set; }

        public virtual HTNhomModel HTNhomModel { get; set; }
        public virtual HTNguoiDungModel HTNguoiDungModel { get; set; }
    }
}
