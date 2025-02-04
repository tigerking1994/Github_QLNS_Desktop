using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class NhQtPheDuyetQuyetToanDAHT : EntityBase
    {
        public string SSoPheDuyet { get; set; }
        public DateTime? DNgayPheDuyet { get; set; }
        public Guid? IIdDonViId { get; set; }
        public string IIdMaDonVi { get; set; }
        public Guid? IIdTiGiaId { get; set; }
        public int? INamBaoCaoTu { get; set; }
        public int? INamBaoCaoDen { get; set; }
        public string SMoTa { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public string SNguoiXoa { get; set; }
        public bool BIsXoa { get; set; }

    }
}
