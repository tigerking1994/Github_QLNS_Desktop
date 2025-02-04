using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhQttBHXHChiTietQuery
    {
        [Column("iID_QTT_BHXH_ChungTu_ChiTiet")]
        [Key]
        public Guid Id { get; set; }
        [Column("iID_QTT_BHXH_ChungTu")]
        public Guid? QttBHXHId { get; set; }
        [Column("iQSBQNam")]
        public int? IQSBQNam { get; set; }
        [Column("fLuongChinh")]
        public double? FLuongChinh { get; set; }
        [Column("fPhuCapChucVu")]
        public double? FPhuCapChucVu { get; set; }
        [Column("fPCTNNghe")]
        public double? FPCTNNghe { get; set; }
        [Column("fPCTNVuotKhung")]
        public double? FPCTNVuotKhung { get; set; }
        [Column("fNghiOm")]
        public double? FNghiOm { get; set; }
        [Column("fHSBL")]
        public double? FHSBL { get; set; }
        [Column("fTongQTLN")]
        public double? FTongQuyTienLuongNam { get; set; }
        [Column("fDuToan")]
        public double? FDuToan { get; set; }
        [Column("fDaQuyetToan")]
        public double? FDaQuyetToan { get; set; }
        [Column("fConLai")]
        public double? FConLai { get; set; }
        [Column("fThu_BHXH_NLD")]
        public double? FThuBHXHNLD { get; set; }
        [Column("fThu_BHXH_NSD")]
        public double? FThuBHXHNSD { get; set; }
        [Column("fTongSoPhaiThuBHXH")]
        public double? FTongSoPhaiThuBHXH { get; set; }
        [Column("fThu_BHYT_NLD")]
        public double? FThuBHYTNLD { get; set; }
        [Column("fThu_BHYT_NSD")]
        public double? FThuBHYTNSD { get; set; }
        [Column("fTongSoPhaiThuBHYT")]
        public double? FTongSoPhaiThuBHYT { get; set; }
        [Column("fThu_BHTN_NLD")]
        public double? FThuBHTNNLD { get; set; }
        [Column("fThu_BHTN_NSD")]
        public double? FThuBHTNNSD { get; set; }
        [Column("fTongSoPhaiThuBHTN")]
        public double? FTongSoPhaiThuBHTN { get; set; }
        [Column("fTongCong")]
        public double? FTongCong { get; set; }
        [Column("fSoPhaiThu")]
        public double? FSoPhaiThu { get; set; }
        [Column("sGhiChu")]
        public string SGhiChu { get; set; }
        [Column("iID_MLNS")]
        public Guid IIDMLNS { get; set; }
        [Column("iID_MLNS_Cha")]
        public Guid? IIDMLNSCha { get; set; }
        [Column("sXauNoiMa")]
        public string SXauNoiMa { get; set; }
        [Column("sLNS")]
        public string SLns { get; set; }
        [Column("sMoTa")]
        public string STenBhMLNS { get; set; }
        [Column("sM")]
        public string SM { get; set; }
        [Column("bHangCha")]
        public bool BHangCha { get; set; }
        [Column("iNamLamViec")]
        public int INamLamViec { get; set; }

        [Column("idDonVi")]
        public string IdDonVi { get; set; }
        [Column("sTenDonVI")]
        public string STenDonVI { get; set; }
        [Column("fBhxhNldDongDuToan")]
        public double? FBhxhNldDongDuToan { get; set; }
        [Column("fBhxhNsddDongDuToan")]
        public double? FBhxhNsddDongDuToan { get; set; }
        [Column("fBhxhNldDongHachToan")]
        public double? FBhxhNldDongHachToan { get; set; }
        [Column("fBhxhNsddDongHachToan")]
        public double? FBhxhNsddDongHachToan { get; set; }
        [Column("fBHXHTongCongDuToan")]
        public double? FBHXHTongCongDuToan { get; set; }
        [Column("fBHXHTongCongHachToan")]
        public double? FBHXHTongCongHachToan { get; set; }
        public double? FBHXHTongCong => FBHXHTongCongDuToan + FBHXHTongCongHachToan;
        [Column("fBhtnNldDongDuToan")]
        public double? FBhtnNldDongDuToan { get; set; }
        [Column("fBhtnNsddDongDuToan")]
        public double? FBhtnNsddDongDuToan { get; set; }
        [Column("fBhtnNldDongHachToan")]
        public double? FBhtnNldDongHachToan { get; set; }
        [Column("fBhtnNsddDongHachToan")]
        public double? FBhtnNsddDongHachToan { get; set; }
        [Column("fBHTNTongCongDuToan")]
        public double? FBHTNTongCongDuToan { get; set; }
        [Column("fBHTNTongCongHachToan")]
        public double? FBHTNTongCongHachToan { get; set; }
        public double? FBHTNTongCong => FBHTNTongCongDuToan + FBHTNTongCongHachToan;
        [Column("fBhytNldDongDuToan")]
        public double? FBhytNldDongDuToan { get; set; }
        [Column("fBhytNldDongHachToan")]
        public double? FBhytNldDongHachToan { get; set; }
        [Column("fBhytNsddDongDuToan")]
        public double? FBhytNsddDongDuToan { get; set; }
        [Column("fBhytNsddDongHachToan")]
        public double? FBhytNsddDongHachToan { get; set; }
        [Column("fBHYTTongCongDuToan")]
        public double? FBHYTTongCongDuToan { get; set; }
        [Column("fBHYTTongCongHachToan")]
        public double? FBHYTTongCongHachToan { get; set; }
        [Column("fTongNLD")]
        public double? FTongNLD { get; set; }
        [Column("fTongNSD")]
        public double? FTongNSD { get; set; }
        [Column("iID_MaDonVi")]
        public string IIDMaDonVi { get; set; }
        public double? FBhytTongCong => FBHYTTongCongDuToan + FBHYTTongCongHachToan;
        public int? STT { get; set; }
        [Column("SoTien")]
        public double? FSoTien { get; set; }
        [Column("NoiDung")]
        public string SNoiDung { get; set; }
        public string SNoiDungDisplay => SNoiDung?.Split(',')[2] ?? "";
        public string STTDisplay => SNoiDung?.Split(',')[1] ?? "";
        public string SMaCapBac { get; set; }
        public double? FGiaTriLuongChinh { get; set; }
        public double? FGiaTriPCCV { get; set; }
        public double? FGiaTriPCTN { get; set; }
        public double? FGiaTriPCTNVK { get; set; }
        public double? FGiaTriHSBL { get; set; }
        public double? FTyLeBHXHNSD { get; set; }
        public double? FTyLeBHXHNLD { get; set; }
        public double? FTyLeBHYTNSD { get; set; }
        public double? FTyLeBHYTNLD { get; set; }
        public double? FTyLeBHTNNSD { get; set; }
        public double? FTyLeBHTNNLD { get; set; }
        public int? ILoai { get; set; }
        [Column("iKhoi")]
        public string LoaiKhoi { get; set; }

        public bool HasDataToPrint => IQSBQNam.GetValueOrDefault() != 0 || FLuongChinh.GetValueOrDefault() != 0 || FPhuCapChucVu.GetValueOrDefault() != 0
            || FPCTNNghe.GetValueOrDefault() != 0 || FPCTNVuotKhung.GetValueOrDefault() != 0 || FNghiOm.GetValueOrDefault() != 0
            || FHSBL.GetValueOrDefault() != 0 || FDuToan.GetValueOrDefault() != 0 || FDaQuyetToan.GetValueOrDefault() != 0
            || FThuBHXHNLD.GetValueOrDefault() != 0 || FThuBHXHNSD.GetValueOrDefault() != 0 || FThuBHYTNLD.GetValueOrDefault() != 0
            || FThuBHYTNSD.GetValueOrDefault() != 0 || FThuBHTNNLD.GetValueOrDefault() != 0 || FThuBHTNNSD.GetValueOrDefault() != 0 || FSoPhaiThu.GetValueOrDefault() != 0
            || FBhxhNldDongDuToan.GetValueOrDefault() != 0 || FBhxhNsddDongDuToan.GetValueOrDefault() != 0 || FBhxhNldDongHachToan.GetValueOrDefault() != 0 || FBhxhNsddDongHachToan.GetValueOrDefault() != 0
            || FBHXHTongCongDuToan.GetValueOrDefault() != 0 || FBHXHTongCongHachToan.GetValueOrDefault() != 0 || FBhtnNldDongDuToan.GetValueOrDefault() != 0 || FBhtnNsddDongDuToan.GetValueOrDefault() != 0
            || FBhtnNldDongHachToan.GetValueOrDefault() != 0 || FBhtnNsddDongHachToan.GetValueOrDefault() != 0 || FBHTNTongCongDuToan.GetValueOrDefault() != 0 || FBHTNTongCongHachToan.GetValueOrDefault() != 0
            || FBhytNldDongDuToan.GetValueOrDefault() != 0 || FBhytNldDongHachToan.GetValueOrDefault() != 0 || FBhytNsddDongDuToan.GetValueOrDefault() != 0 || FBhytNsddDongHachToan.GetValueOrDefault() != 0
            || FBHYTTongCongDuToan.GetValueOrDefault() != 0 || FBHYTTongCongHachToan.GetValueOrDefault() != 0;
        
        public bool? HasData => !string.IsNullOrEmpty(STenDonVI) || FBhxhNldDongDuToan.GetValueOrDefault() != 0 || FBhxhNsddDongDuToan.GetValueOrDefault() != 0
                || FBhxhNldDongHachToan.GetValueOrDefault() != 0 || FBhxhNsddDongHachToan.GetValueOrDefault() != 0;
    }

    public class BhReportQttBHXHChiTietQuery
    {
        public Guid IIdChungTu { get; set; }
        public Guid IIdParent { get; set; }
        public string SNoiDung { get; set; }
        public string STT { get; set; }
        public int ILevel { get; set; }
        public int IThuTu { get; set; }
        public int ILoaiChi { get; set; }
        public int IKinhPhiKCB { get; set; } // (3) = (1) - (2)
        public double FDuToan { get; set; }
        public double FHachToan { get; set; }
        public double FTongSo => FDuToan + FHachToan;
        public double FSoTien { get; set; }
    }

}
