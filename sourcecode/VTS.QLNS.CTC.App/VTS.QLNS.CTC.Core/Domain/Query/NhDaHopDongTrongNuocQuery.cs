using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhDaHopDongTrongNuocQuery
    {
        public  Guid Id { get; set; }
        public string SSoHopDong { get; set; }
        public DateTime? DNgayHopDong { get; set; }
        public string STenHopDong { get; set; }
        public DateTime? DKhoiCongDuKien { get; set; }
        public DateTime? DKetThucDuKien { get; set; }
        public Guid? IIdLoaiHopDongId { get; set; }
        public Guid? IIdCacQuyetDinhId { get; set; }
        public Guid? IIdKhTongTheNhiemVuChiId { get; set; }
        public Guid? IIdNhaThauThucHienId { get; set; }
        public Guid? IIdParentAdjustId { get; set; }
        public Guid? IIdParentId { get; set; }
        public Guid? IIdGoiThauId { get; set; }
        public Guid? IIdTiGiaId { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public double? FGiaTriHopDongNgoaiTeKhac { get; set; }
        public double? FGiaTriHopDongUSD { get; set; }
        public double? FGiaTriHopDongVND { get; set; }
        public double? FGiaTriHopDongEUR { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public string SMaNgoaiTeKhac { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public string SNguoiXoa { get; set; }
        public bool? BIsActive { get; set; }
        public bool? BIsGoc { get; set; }
        public bool? BIsKhoa { get; set; }
        public int ILanDieuChinh { get; set; }
        public string DieuChinhTu { get; set; }
        public int TotalFiles { get; set; }
        public string STenDuAn { get; set; }
        public string STenDonVi { get; set; }
        public string SSoQuyeDinh { get; set; }
        public Guid? IIdDonViId { get; set; }
        public double? FTiGiaNhap { get; set; }
    }
}
