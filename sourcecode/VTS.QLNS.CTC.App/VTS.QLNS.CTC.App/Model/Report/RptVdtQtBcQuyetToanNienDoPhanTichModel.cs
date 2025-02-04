using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class RptVdtQtBcQuyetToanNienDoPhanTichModel
    {
        public bool BIsChuyenTiep { get; set; }
        public bool IsHangCha { get; set; }
        public string SSoThuTu { get; set; }
        public string STenDuAn { get; set; }
        public Guid? IIdLoaiCongTrinh { get; set; }
        public string STenLoaiCongTrinh { get; set; }
        /// <summary>
        /// col 1
        /// </summary>
        public double FDuToanCnsChuaGiaiNganTaiKbNamTruoc { get; set; }
        /// <summary>
        /// col 2
        /// </summary>
        public double FDuToanCnsChuaGiaiNganTaiDvNamTruoc { get; set; }
        /// <summary>
        /// col 3
        /// </summary>
        public double FDuToanCnsChuaGiaiNganTaiCucNamTruoc { get; set; }
        /// <summary>
        /// col 4
        /// </summary>
        public double FTongChuaThuHoi
        {
            get
            {
                return FTuChuaThuHoiTaiCuc + FTuChuaThuHoiTaiDonVi;
            }
        }
        /// <summary>
        /// col 5
        /// </summary>
        public double FTongDuToanDuocGiaoNamTruocChuyenSang
        {
            get
            {
                return FDuToanCnsChuaGiaiNganTaiKbNamTruoc + FDuToanCnsChuaGiaiNganTaiDvNamTruoc
                    + FDuToanCnsChuaGiaiNganTaiCucNamTruoc + FTongChuaThuHoi;
            }
        }
        /// <summary>
        /// col 6
        /// </summary>
        public double FChiTieuNamNayKb { get; set; }
        /// <summary>
        /// col 7
        /// </summary>
        public double FChiTieuNamNayLc { get; set; }
        /// <summary>
        /// col 8
        /// </summary>
        public double FTongDuToanDuocGiao
        {
            get
            {
                return FChiTieuNamNayKb + FChiTieuNamNayLc;
            }
        }
        /// <summary>
        /// col 9
        /// </summary>
        public double FTotalDuToanDuocGiao
        {
            get
            {
                return FTongDuToanDuocGiao + FTongDuToanDuocGiaoNamTruocChuyenSang;
            }
        }
        /// <summary>
        /// col 10
        /// </summary>
        public double FSoCapNamTrcCs { get; set; }
        /// <summary>
        /// col 11
        /// </summary>
        public double FSoCapNamNay { get; set; }
        /// <summary>
        /// col 12
        /// </summary>
        public double FTongSoDuocCap
        {
            get
            {
                return FSoCapNamTrcCs + FSoCapNamNay;
            }
        }
        /// <summary>
        /// col 13
        /// </summary>
        public double FDnQuyetToanNamTrc { get; set; }
        /// <summary>
        /// col 14
        /// </summary>
        public double FDnQuyetToanNamNay { get; set; }
        /// <summary>
        /// col 15
        /// </summary>
        public double FTongDeNghiQuyetToan
        {
            get
            {
                return FDnQuyetToanNamTrc + FDnQuyetToanNamNay;
            }
        }
        /// <summary>
        /// col 16
        /// </summary>
        public double FTongChuyenNamSau
        {
            get
            {
                return (FTuChuaThuHoiTaiCuc + FTuChuaThuHoiTaiDonVi) + (FDuToanCnsChuaGiaiNganTaiCuc + FDuToanCnsChuaGiaiNganTaiDv + FDuToanCnsChuaGiaiNganTaiKb);
            }
        }
        /// <summary>
        /// col 17
        /// </summary>
        public double FTongTamUngChuaThuHoi
        {
            get
            {
                return FTuChuaThuHoiTaiCuc + FTuChuaThuHoiTaiDonVi;
            }
        }
        /// <summary>
        /// col 18
        /// </summary>
        public double FTuChuaThuHoiTaiCuc { get; set; }

        /// <summary>
        /// col 19
        /// </summary>
        public double FTuChuaThuHoiTaiDonVi { get; set; }
        /// <summary>
        /// col 20
        /// </summary>
        public double FTongDuChuaGiaiNgan
        {
            get
            {
                return FDuToanCnsChuaGiaiNganTaiCuc + FDuToanCnsChuaGiaiNganTaiDv + FDuToanCnsChuaGiaiNganTaiKb;
            }
        }
        /// <summary>
        /// col 21
        /// </summary>
        public double FDuToanCnsChuaGiaiNganTaiCuc { get; set; }
        /// <summary>
        /// col 22
        /// </summary>
        public double FDuToanCnsChuaGiaiNganTaiDv { get; set; }
        /// <summary>
        /// col 23
        /// </summary>
        public double FDuToanCnsChuaGiaiNganTaiKb { get; set; }
        /// <summary>
        /// col 24
        /// </summary>
        public double FDuToanThuHoi { get; set; }
        public int LoaiParent { get; set; }
        public Guid? IIdLoaiCongTrinhParent { get; set; }
    }
}
