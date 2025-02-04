using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhDtPhanBoChungTuChiTietQuery
    {
        [Column("iID_DTT_BHXH_ChungTu_ChiTiet")]
        public Guid Id { get; set; }
        [Column("iID_DTT_BHXH_ChungTu")]
        public Guid? IIdDtchungTu { get; set; }
        [Column("iID_MLNS")]
        public Guid? IIdMlns { get; set; }
        [Column("iID_MLNS_Cha")]
        public Guid? IIdMlnsCha { get; set; }
        [Column("sXauNoiMa")]
        public string SXauNoiMa { get; set; }
        [Column("sLNS")]
        public string SLns { get; set; }
        [Column("sL")]
        public string SL { get; set; }
        [Column("sK")]
        public string SK { get; set; }
        [Column("sM")]
        public string SM { get; set; }
        [Column("sTM")]
        public string STm { get; set; }
        [Column("sTTM")]
        public string STtm { get; set; }
        [Column("sNG")]
        public string SNg { get; set; }
        [Column("sTNG")]
        public string STng { get; set; }
        [Column("sTNG1")]
        public string STng1 { get; set; }
        [Column("sTNG2")]
        public string STng2 { get; set; }
        [Column("sTNG3")]
        public string STng3 { get; set; }
        [Column("sMoTa")]
        public string SMoTa { get; set; }
        [Column("iNamLamViec")]
        public int? INamLamViec { get; set; }
        [Column("bHangCha")]
        public bool BHangCha { get; set; }
        [Column("iPhanCap")]
        public int IPhanCap { get; set; }
        [Column("iID_MaDonVi")]
        public string IIdMaDonVi { get; set; }
        [Column("fBHXHNLD")]
        public double? FBHXHNLD { get; set; }
        [Column("fBHXHNSD")]
        public double? FBHXHNSD { get; set; }
        [Column("fThuBHXH")]
        public double? FThuBHXH { get; set; }
        [Column("fBHYTNLD")]
        public double? FBHYTNLD { get; set; }
        [Column("fBHYTNSD")]
        public double? FBHYTNSD { get; set; }
        [Column("fThuBHYT")]
        public double? FThuBHYT { get; set; }
        [Column("fBHTNNLD")]
        public double? FBHTNNLD { get; set; }
        [Column("fBHTNNSD")]
        public double? FBHTNNSD { get; set; }
        [Column("fThuBHTN")]
        public double? FThuBHTN { get; set; }
        [Column("fTongCong")]
        public double? FTongCong { get; set; }
        [Column("sGhiChu")]
        public string SGhiChu { get; set; }
        [Column("dNgayTao")]
        public DateTime? DNgayTao { get; set; }
        [Column("sNguoiTao")]
        public string SNguoiTao { get; set; }
        [Column("dNgaySua")]
        public DateTime? DNgaySua { get; set; }
        [Column("sNguoiSua")]
        public string SNguoiSua { get; set; }
        [Column("iID_CTDuToan_Nhan")]
        public Guid? IIDCTDuToanNhan { get; set; }
        [Column("iDuLieuNhan")]
        public int IDuLieuNhan { get; set; }
        [NotMapped]
        public string SSoQuyetDinh { get; set; }
        [NotMapped]
        public bool IsEditHienVat { get; set; }
        [NotMapped]
        public bool IsEditHangNhap { get; set; }
        [NotMapped]
        public bool IsEditHangMua { get; set; }
        [NotMapped]
        public bool IsEditDuPhong { get; set; }
        [NotMapped]
        public bool IsEditPhanCap { get; set; }

        [Column("iRowType")]
        public int IRowType { get; set; }
        [Column("sTenDonVi")]
        public string STenDonVi { get; set; }
        [Column("fBHXHNLDTruocDieuChinh")]
        public Double? FBHXHNLDTruocDieuChinh { get; set; }
        [Column("fBHXHNSDTruocDieuChinh")]
        public Double? FBHXHNSDTruocDieuChinh { get; set; }
        [Column("fBHYTNLDTruocDieuChinh")]
        public Double? FBHYTNLDTruocDieuChinh { get; set; }
        [Column("fBHYTNSDTruocDieuChinh")]
        public Double? FBHYTNSDTruocDieuChinh { get; set; }
        [Column("fBHTNNLDTruocDieuChinh")]
        public Double? FBHTNNLDTruocDieuChinh { get; set; }
        [Column("fBHTNNSDTruocDieuChinh")]
        public Double? fBHTNNSDTruocDieuChinh { get; set; }

        [Column("fBHXHNLDSauDieuChinh")]
        public Double? FBHXHNLDSauDieuChinh { get; set; }
        [Column("fBHXHNSDtSauDieuChinh")]
        public Double? FBHXHNSDtSauDieuChinh { get; set; }
        [Column("fBHYTNLDSauDieuChinh")]
        public Double? FBHYTNLDSauDieuChinh { get; set; }
        [Column("fBHYTNSDSauDieuChinh")]
        public Double? FBHYTNSDSauDieuChinh { get; set; }
        [Column("fBHTNNLDSauDieuChinh")]
        public Double? FBHTNNLDSauDieuChinh { get; set; }
        [Column("fBHTNNSDSauDieuChinh")]
        public Double? FBHTNNSDSauDieuChinh { get; set; }
        [Column("IsRemainRow")]
        public bool IsRemainRow { get; set; }
        public bool IsHangCha => BHangCha;
    }
}
