using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhKhcCheDoBhXhQuery
    {
        public Guid? IID_BH_KHC_CheDoBHXH { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public int? INamLamViec { get; set; }
        public Guid? IIdDonViId { get; set; }
        public string IID_MaDonVi { get; set; }
        public string sMoTa { get; set; }
        public int? iTongSoDaThucHienNamTruoc { get; set; }
        public int? iTongSoUocThucHienNamTruoc { get; set; }
        public int? iTongSoKeHoachThucHienNamNay { get; set; }
        public int? iTongSoSQ { get; set; }
        public int? iTongSoQNCN { get; set; }
        public int? iTongSoCNVQP { get; set; }
        public int? iTongSoLDHD { get; set; }
        public int? iTongSoHSQBS { get; set; }
        public double? fTongTienDaThucHienNamTruoc { get; set; }
        public double? fTongTienUocThucHienNamTruoc { get; set; }
        public double? fTongTienKeHoachThucHienNamNay { get; set; }
        public double? fTongTienSQ { get; set; }
        public double? fTongTienQNCN { get; set; }
        public double? fTongTienCNVQP { get; set; }
        public double? fTongTienLDHD { get; set; }
        public double? fTongTienHSQBS { get; set; }
        public string sTongHop { get; set; }
        public Guid? iID_TongHopID { get; set; }
        public int iLoaiTongHop { get; set; }
        public bool? bIsKhoa { get; set; }
        public bool BDaTongHop { get; set; }
        public DateTime? dNgaySua { get; set; }
        public DateTime? dNgayTao { get; set; }
        public string sNguoiSua { get; set; }
        public string sNguoiTao { get; set; }

        // Another properties
        public double? isTongTien { get; set; }
        public string sTenDonVi { get; set; }
    }
}
