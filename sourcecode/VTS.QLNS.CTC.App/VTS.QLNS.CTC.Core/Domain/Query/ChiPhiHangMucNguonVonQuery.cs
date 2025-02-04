using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ChiPhiHangMucNguonVonQuery
    {
        public Guid? Id { get; set; }
        public int iLoai { get; set; }
        public Guid? iId_ChiPhi { get; set; }
        public Guid? iId_HangMuc { get; set; }
        public Guid? iId_NguonVon { get; set; }
        public Guid? iId_ParentId { get; set; }
        public string sNoiDung { get; set; }
        public double? fThanhTien { get; set; }
    }
}
