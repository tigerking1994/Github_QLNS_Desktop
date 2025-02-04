using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class VdtKhvPhanBoVonChiPhiChiTiet : EntityBase
    {
        public override Guid Id { get; set; }
        public string SMaOrder { get; set; }
        public string SMaChiPhi { get; set; }
        public string SNoiDung { get; set; }
        public Guid? IIdPhanBoVonChiPhiId { get; set; }
        public string STrangThaiDuAnDangKy { get; set; }
        public string SGhiChu { get; set; }
        public double? FGiaTriPheDuyet { get; set; }
        public Guid? IIdLoaiCongTrinh { get; set; }
        public double? FGiaTriPheDuyetDC { get; set; }
        public Guid? IIdParent { get; set; }
        public Guid? IIdDanhMucDtChi { get; set; }
        public int? ILoaiDuAn { get; set; }
    }
}
