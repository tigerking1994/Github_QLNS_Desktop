using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhDtcDcdToanChiQuery
    {
        [Column("iID_BH_DTC")]
        public Guid IID_BH_DTC { get; set; }
        [Column("sSoChungTu")]
        public string SSoChungTu { get; set; }
        [Column("dNgayChungTu")]
        public DateTime DNgayChungTu { get; set; }
        [Column("sSoQuyetDinh")]
        public string SSoQuyetDinh { get; set; }
        [Column("dNgayQuyetDinh")]
        public DateTime? DNgayQuyetDinh { get; set; }
        [Column("iNamLamViec")]
        public int? INamLamViec { get; set; }
        [Column("iID_DonVi")]
        public Guid? IIdDonViId { get; set; }
        [Column("iID_MaDonVi")]
        public string IID_MaDonVi { get; set; }
        [Column("sMoTa")]
        public string SMoTa { get; set; }
        [Column("sLNS")]
        public string SLNS { get; set; }
        [Column("iID_LoaiCap")]
        public Guid IID_LoaiCap { get; set; }
        [Column("fTienDuToanDuocGiao")]
        public double? FTienDuToanDuocGiao { get; set; }
        [Column("fTienThucHien06ThangDauNam")]
        public double? FTienThucHien06ThangDauNam { get; set; }
        [Column("fTienUocThucHien06ThangCuoiNam")]
        public double? FTienUocThucHien06ThangCuoiNam { get; set; }
        [Column("fTienUocThucHienCaNam")]
        public double? FTienUocThucHienCaNam { get; set; }
        [Column("fTienSoSanhTang")]
        public double? FTienSoSanhTang { get; set; }
        [Column("fTienSoSanhGiam")]
        public double? FTienSoSanhGiam { get; set; }
        [Column("sTongHop")]
        public string STongHop { get; set; }
        [Column("iID_TongHopID")]
        public Guid? IID_TongHopID { get; set; }
        [Column("iLoaiTongHop")]
        public int ILoaiTongHop { get; set; }
        [Column("bIsKhoa")]
        public bool BIsKhoa { get; set; }
        [Column("dNgaySua")]
        public DateTime? DNgaySua { get; set; }
        [Column("dNgayTao")]
        public DateTime? DNgayTao { get; set; }
        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }
        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }
        public string STenDonVi { get; set; }
        public string STenDanhMucLoaiChi { get; set; }
        public bool BDaTongHop { get; set; }
        public string SMaLoaiChi { get; set; }
    }
}
