using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhQttBHXHChiTietGiaiThichQuery
    {
        [Column("iID_QT_CTCT_GiaiThich")]
        [Key]
        public Guid Id { get; set; }
        [Column("iID_QTT_BHXH_ChungTu")]
        public Guid? QttBHXHId { get; set; }
        [Column("iID_MLNS")]
        public Guid IIDMLNS { get; set; }
        [Column("iID_MLNS_Cha")]
        public Guid? IIDMLNSCha { get; set; }
        [Column("sLNS")]
        public string SLNS { get; set; }
        [Column("sMoTa")]
        public string STenBhMLNS { get; set; }
        [Column("bHangCha")]
        public bool BHangCha { get; set; }
        [Column("fTruyThu_BHXH_NLD")]
        public double? FTruyThuBHXHNLD { get; set; }
        [Column("fTruyThu_BHXH_NSD")]
        public double? FTruyThuBHXHNSD { get; set; }
        [Column("fTruyThu_BHXH_TongCong")]
        public double? FTruyThuBHXHTongCong { get; set; }

        [Column("fTruyThu_BHYT_NLD")]
        public double? FTruyThuBHYTNLD { get; set; }
        [Column("fTruyThu_BHYT_NSD")]
        public double? FTruyThuBHYTNSD { get; set; }
        [Column("fTruyThu_BHYT_TongCong")]
        public double? FTruyThuBHYTTongCong { get; set; }

        [Column("fTruyThu_BHTN_NLD")]
        public double? FTruyThuBHTNNLD { get; set; }
        [Column("fTruyThu_BHTN_NSD")]
        public double? FTruyThuBHTNNSD { get; set; }
        [Column("fTruyThu_BHTN_TongCong")]
        public double? FTruyThuBHTNTongCong { get; set; }
        [Column("fTongTruyThu_BHXH")]
        public double? FTongTruyThuBHXH { get; set; }
        [Column("sXauNoiMa")]
        public string SXauNoiMa { get; set; }
        [Column("sL")]
        public string SL { get; set; }
        [Column("sK")]
        public string SK { get; set; }
        [Column("sM")]
        public string SM { get; set; }
        [Column("sTM")]
        public string STM { get; set; }

        [Column("fTruyThu_BHXH_NLD_DT")]
        public double? FTruyThuBhxhNldDT { get; set; }
        [Column("fTruyThu_BHXH_NSD_DT")]
        public double? FTruyThuBhxhNsdDT { get; set; }
        [Column("fTruyThu_BHXH_TongCong_DT")]
        public double? FTruyThuBhxhTongCongDT { get; set; }
        [Column("fTruyThu_BHYT_NLD_DT")]
        public double? FTruyThuBhytNldDT { get; set; }
        [Column("fTruyThu_BHYT_NSD_DT")]
        public double? FTruyThuBhytNsdDT { get; set; }
        [Column("fTruyThu_BHYT_TongCong_DT")]
        public double? FTruyThuBhytTongCongDT { get; set; }
        [Column("fTruyThu_BHTN_NLD_DT")]
        public double? FTruyThuBhtnNldDT { get; set; }
        [Column("fTruyThu_BHTN_NSD_DT")]
        public double? FTruyThuBhtnNsdDT { get; set; }
        [Column("fTruyThu_BHTN_TongCong_DT")]
        public double? FTruyThuBhtnTongCongDT { get; set; }

        [Column("fTruyThu_BHXH_NLD_HT")]
        public double? FTruyThuBhxhNldHT { get; set; }
        [Column("fTruyThu_BHXH_NSD_HT")]
        public double? FTruyThuBhxhNsdHT { get; set; }
        [Column("fTruyThu_BHXH_TongCong_HT")]
        public double? FTruyThuBhxhTongCongHT { get; set; }
        [Column("fTruyThu_BHYT_NLD_HT")]
        public double? FTruyThuBhytNldHT { get; set; }
        [Column("fTruyThu_BHYT_NSD_HT")]
        public double? FTruyThuBhytNsdHT { get; set; }
        [Column("fTruyThu_BHYT_TongCong_HT")]
        public double? FTruyThuBhytTongCongHT { get; set; }
        [Column("fTruyThu_BHTN_NLD_HT")]
        public double? FTruyThuBhtnNldHT { get; set; }
        [Column("fTruyThu_BHTN_NSD_HT")]
        public double? FTruyThuBhtnNsdHT { get; set; }
        [Column("fTruyThu_BHTN_TongCong_HT")]
        public double? FTruyThuBhtnTongCongHT { get; set; }

        [Column("fTong_TruyThu_BHXH")]
        public double? FTongCongTruyThuBHXH { get; set; }
        [Column("fTong_TruyThu_BHYT")]
        public double? FTongCongTruyThuBHYT { get; set; }
        [Column("fTong_TruyThu_BHTN")]
        public double? FTongCongTruyThuBHTN { get; set; }
        [Column("fTong_TruyThu")]
        public double? FTongTruyThu { get; set; }

        [Column("fSoPhaiThuNop")]
        public double? FSoPhaiThuNop { get; set; }
        [Column("fSoDaNopTrongNam")]
        public double? FSoDaNopTrongNam { get; set; }
        [Column("fSoDaNopSau31_12")]
        public double? FSoDaNopSau3112 { get; set; }
        [Column("fTongSoDaNop")]
        public double? FTongSoDaNop { get; set; }
        [Column("fSoConPhaiNop")]
        public double? FSoConPhaiNop { get; set; }
        [Column("iQuanSo")]
        public int? IQuanSo { get; set; }
        [Column("fQuyTienLuongCanCu")]
        public double? FQuyTienLuongCanCu { get; set; }
        [Column("fSoTienGiamDong")]
        public double? FSoTienGiamDong { get; set; }
        [Column("dTuNgay")]
        public DateTime? DTuNgay { get; set; }
        [Column("dDenNgay")]
        public DateTime? DDenNgay { get; set; }
        [Column("iLoai")]
        public int ILoaiGiaiThich { get; set; }
        [Column("iSTT")]
        public int ISTT { get; set; }
        [Column("sNoiDung")]
        public string SNoiDung { get; set; }

        [Column("fPhaiNop_TrongQuyNam")]
        public double? FPhaiNopTrongQuyNam { get; set; }
        [Column("fTruyThu_QuyNamTruoc")]
        public double? FTruyThuQuyNamTruoc { get; set; }
        [Column("fPhaiNop_QuyNamTruoc")]
        public double? FPhaiNopQuyNamTruoc { get; set; }
        [Column("fDaNop_TrongQuyNam")]
        public double? FDaNopTrongQuyNam { get; set; }
        [Column("fConPhaiNopTiep")]
        public double? FConPhaiNopTiep { get; set; }
        [Column("sKienNghi")]
        public string SKienNghi { get; set; }
        [Column("sLoaiThu")]
        public string SLoaiThu { get; set; }
        public double? FLuongChinh { get; set; }
        public double? FPCChucVu { get; set; }
        public double? FPCTNNghe { get; set; }
        public double? FPCTNVuotKhung { get; set; }
        public double? FNghiOm { get; set; }
        public double? FHSBL { get; set; }
        public double? FTongQuyLuong { get; set; }
        public bool IsModified { get; set; }
        public string SMaDonVi { get; set; }
        public string STenDonVi { get; set; }

        public bool HasDataToPrint => FLuongChinh.GetValueOrDefault() != 0 || FPCChucVu.GetValueOrDefault() != 0 || FPCTNNghe.GetValueOrDefault() != 0
            || FPCTNVuotKhung.GetValueOrDefault() != 0 || FNghiOm.GetValueOrDefault() != 0 || FHSBL.GetValueOrDefault() != 0
            || FTruyThuBHXHNLD.GetValueOrDefault() != 0 || FTruyThuBHXHNSD.GetValueOrDefault() != 0
            || FTruyThuBHYTNLD.GetValueOrDefault() != 0 || FTruyThuBHYTNSD.GetValueOrDefault() != 0
            || FTruyThuBHTNNLD.GetValueOrDefault() != 0 || FTruyThuBHTNNSD.GetValueOrDefault() != 0;
    }
}
