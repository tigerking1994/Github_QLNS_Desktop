using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class HTNhomNguoiDungQuyenModel
    {
        public string AuthorityName { get; set; }
        public Guid GroupId { get; set; }

        public virtual HTQuyenModel HTQuyenModel { get; set; }
        public virtual HTNhomModel HTNhomModel { get; set; }
    }
}
