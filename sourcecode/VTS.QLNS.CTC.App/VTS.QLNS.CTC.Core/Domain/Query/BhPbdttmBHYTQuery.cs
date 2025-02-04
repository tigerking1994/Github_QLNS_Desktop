using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhPbdttmBHYTQuery
    {
        public Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime DNgayChungTu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime DNgayQuyetDinh { get; set; }
        public int INamLamViec { get; set; }
        public string SDSLNS { get; set; }
        public string SDS_IDMaDonVi { get; set; }
        public int ILoaiDuToan { get; set; }
        public Double? FDuToan { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public bool BIsKhoa { get; set; }
        public string SMoTa { get; set; }
        public string SDS_DotNhan { get; set; }
    }
}
