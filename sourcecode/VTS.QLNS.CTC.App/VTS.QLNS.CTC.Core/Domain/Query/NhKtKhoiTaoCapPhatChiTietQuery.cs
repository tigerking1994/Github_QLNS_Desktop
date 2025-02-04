using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhKtKhoiTaoCapPhatChiTietQuery
    {
        public Guid? Id { get; set; }
        public Guid? IIdKhoiTaoCapPhatID { get; set; }
        public Guid? IIdDuAnID { get; set; }
        public Guid? IIdHopDongID { get; set; }
        public double? FQTKinhPhiDuyetCacNamTruocUSD { get; set; }
        public double? FQTKinhPhiDuyetCacNamTruocVND { get; set; }
        public double? FDeNghiQTNamNayUSD { get; set; }
        public double? FDeNghiQTNamNayVND { get; set; }
        public double? FLuyKeKinhPhiDuocCapUSD { get; set; }
        public double? FLuyKeKinhPhiDuocCapVND { get; set; }
        public double? FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd { get; set; }
        public double? FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd { get; set; }
        public double? FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd { get; set; }
        public double? FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd { get; set; }
        public double? FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd { get; set; }
        public double? FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd { get; set; }
        public double? FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd { get; set; }
        public double? FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd { get; set; }
        public double? FDeNghiChuyenNamSauUsd { get; set; }
        public double? FDeNghiChuyenNamSauVnd { get; set; }
        public double? FKinhPhiThuaNopNsnnUSD { get; set; }
        public double? FKinhPhiThuaNopNsnnVND { get; set; }
        public double? FConLaiChuaGiaiNganUSD { get; set; }
        public double? FConLaiChuaGiaiNganVND { get; set; }
        public double? FGiaTriNoiDungUSD { get; set; }
        public double? FGiaTriNoiDungVND { get; set; }
        public Guid? IIdParentID { get; set; }
        // Another properties
        public string SMaDuAn { get; set; }
        public string STenDuAn { get; set; }
        public string SMaHopDong { get; set; }
        public string STenHopDong { get; set; }
        public string STenChiPhiDetail { get; set; }
        public string SMaNhiemVuChi { get; set; }
        public string SSoQuyetDinhDauTu { get; set; }
        public string SSoHopDong { get; set; }
        public string SSoQuyetDinhKhac { get; set; }
        public string SMaChiPhi { get; set; }
        public int? ILoaiNoiDung { get; set; }
        public string SMaNoiDung { get; set; }
        public string STenNoiDung { get; set; }
        public bool BHangCha { get; set; }
        public string SLoai
        {
            get
            {
                if (this.ILoaiNoiDung == NHConstants.ILOAI_CHI_PHI)
                {
                    return NhTongHopConstants.SLOAI_CHI_PHI;
                }
                else if (this.ILoaiNoiDung == NHConstants.ILOAI_CHUONG_TRINH)
                {
                    return NhTongHopConstants.SLOAI_CHUONG_TRINH;
                }
                else if (this.ILoaiNoiDung == NHConstants.ILOAI_DU_AN)
                {
                    return NhTongHopConstants.SLOAI_DU_AN;
                }
                else if (this.ILoaiNoiDung == NHConstants.ILOAI_HOP_DONG)
                {
                    return NhTongHopConstants.SLOAI_HOP_DONG;
                }
                else if (this.ILoaiNoiDung == NHConstants.ILOAI_QD_KHAC)
                {
                    return NhTongHopConstants.SLOAI_QD_KHAC;
                }
                else
                {
                    return string.Empty;
                }

            }
        }

    }
}
