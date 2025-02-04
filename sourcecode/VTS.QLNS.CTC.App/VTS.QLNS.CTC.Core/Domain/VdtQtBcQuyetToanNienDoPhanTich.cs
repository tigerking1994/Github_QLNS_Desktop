using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class VdtQtBcQuyetToanNienDoPhanTich : EntityBase
    {
        public override Guid Id { get; set;  }
        public Guid? IIdBcQuyetToanNienDo { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public Guid? IIdLoaiCongTrinh { get; set; }
        public double? FDuToanCnsChuaGiaiNganTaiKb { get; set; }
        public double? FDuToanCnsChuaGiaiNganTaiDv { get; set; }
        public double? FDuToanCnsChuaGiaiNganTaiCuc { get; set; }
        public double? FTuChuaThuHoiTaiCuc { get; set; }
        public double? FChiTieuNamNayKb { get; set; }
        public double? FChiTieuNamNayLc { get; set; }
        public double? FSoCapNamTrcCs { get; set; }
        public double? FSoCapNamNay { get; set; }
        public double? FDnQuyetToanNamTrc { get; set; }
        public double? FDnQuyetToanNamNay { get; set; }
        public double? FTuChuaThuHoiTaiDonVi { get; set; }
        public double? FDuToanThuHoi { get; set; }
    }
}
