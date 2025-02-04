using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public partial class BhKhcCheDoBhXhChiTietQuery : EntityBase
    {
        public Guid? IID_KHC_CheDoBHXH { get; set; }
        public Guid? IID_MucLucNganSach { get; set; }
        public string sLoaiTroCap { get; set; }
        public int? iSoDaThucHienNamTruoc { get; set; }
        public int? iSoUocThucHienNamTruoc { get; set; }
        public int? iSoKeHoachThucHienNamNay { get; set; }
        public int? iSoSQ { get; set; }
        public int? iSoQNCN { get; set; }
        public int? iSoCNVQP { get; set; }
        public int? iSoLDHD { get; set; }
        public int? iSoHSQBS { get; set; }
        public double? fTienDaThucHienNamTruoc { get; set; }
        public double? fTienUocThucHienNamTruoc { get; set; }
        public double? fTienKeHoachThucHienNamNay { get; set; }
        public double? fTienSQ { get; set; }
        public double? fTienQNCN { get; set; }
        public double? fTienCNVQP { get; set; }
        public double? fTienLDHD { get; set; }
        public double? fTienHSQBS { get; set; }
        public string sGhiChu { get; set; }
        public DateTime? dNgaySua { get; set; }
        public DateTime? dNgayTao { get; set; }
        public string sNguoiSua { get; set; }
        public string sNguoiTao { get; set; }
        public int INamLamViec { get; set; }
        public string SXauNoiMa { get; set; }
        public string IIdMaDonVi { get; set; }
        public bool BHangCha { get;set; }
        [Column("iID_MLNS_Cha")]
        public Guid IID_MLNS_Cha { get; set; }
        [Column("iID_MLNS")]
        public Guid IID_MLNS { get; set; }
        public bool IsHadData => fTienKeHoachThucHienNamNay.GetValueOrDefault(0)!=0;
        public string SMoTa { get;set; }
        public string SDuToanChiTietToi { get;set; }
    }
}
