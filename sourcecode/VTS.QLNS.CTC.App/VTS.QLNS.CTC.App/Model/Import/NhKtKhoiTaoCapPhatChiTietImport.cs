using System;
using System.ComponentModel.DataAnnotations.Schema;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [Sheet(2, "1. Khởi tạo theo QTNĐ", 7, 0)]

    public class NhKtKhoiTaoCapPhatChiTietImport : BindableBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [ColumnAttribute("1", 0)]
        public string STT { get; set; }

        [ColumnAttribute("2", 1)]
        public string NoiDung { get; set; }

        [ColumnAttribute("3", 2)]
        public string SLoai { get; set; }

        public int ILoai
        {
            get
            {
                if (this.SLoai.ToLower().Trim().Equals(NhTongHopConstants.SLOAI_CHI_PHI.ToLower().Trim()))
                {
                    return NHConstants.ILOAI_CHI_PHI;
                }
                else if (this.SLoai.ToLower().Trim().Equals(NhTongHopConstants.SLOAI_CHUONG_TRINH.ToLower().Trim()))
                {
                    return NHConstants.ILOAI_CHUONG_TRINH;
                }
                else if (this.SLoai.ToLower().Trim().Equals(NhTongHopConstants.SLOAI_DU_AN.ToLower().Trim()))
                {
                    return NHConstants.ILOAI_DU_AN;
                }
                else if (this.SLoai.ToLower().Trim().Equals(NhTongHopConstants.SLOAI_HOP_DONG.ToLower().Trim()))
                {
                    return NHConstants.ILOAI_HOP_DONG;
                }
                else if (this.SLoai.ToLower().Trim().Equals(NhTongHopConstants.SLOAI_QD_KHAC.ToLower().Trim()))
                {
                    return NHConstants.ILOAI_QD_KHAC;
                }
                else
                {
                    return NHConstants.ZERO;
                }

            }
        }
        [ColumnAttribute("4", 3)]
        public string SMaNhiemVuChi { get; set; }
        [ColumnAttribute("5", 4)]
        public string SSoQuyetDinhDauTu { get; set; }
        [ColumnAttribute("6", 5)]
        public string SSoHopDong { get; set; }
        [ColumnAttribute("7", 6)]
        public string SSoQuyetDinhKhac { get; set; }
        
        [ColumnAttribute("8", 7)]
        public string SMaChiPhi { get; set; }

        private string _fGiaTriNoiDungUSD;

        [ColumnAttribute("9", 8)]
        public string FGiaTriNoiDungUSD
        {
            get => _fGiaTriNoiDungUSD;
            set => SetProperty(ref _fGiaTriNoiDungUSD, value);
        }
        private string _fGiaTriNoiDungVND;

        [ColumnAttribute("10", 9)]
        public string FGiaTriNoiDungVND
        {
            get => _fGiaTriNoiDungVND;
            set => SetProperty(ref _fGiaTriNoiDungVND, value);
        }

        private string _fQTKinhPhiDuyetCacNamTruocUSD;

        [ColumnAttribute("11", 10)]
        public string FQTKinhPhiDuyetCacNamTruocUSD
        {
            get => _fQTKinhPhiDuyetCacNamTruocUSD;
            set => SetProperty(ref _fQTKinhPhiDuyetCacNamTruocUSD, value);
        }
        private string _fQTKinhPhiDuyetCacNamTruocVND;
        [ColumnAttribute("12", 11)]
        public string FQTKinhPhiDuyetCacNamTruocVND
        {
            get => _fQTKinhPhiDuyetCacNamTruocVND;
            set => SetProperty(ref _fQTKinhPhiDuyetCacNamTruocVND, value);
        }
        private string _fDeNghiQTNamNayUSD;

        [ColumnAttribute("13", 12)]
        public string FDeNghiQTNamNayUSD
        {
            get => _fDeNghiQTNamNayUSD;
            set => SetProperty(ref _fDeNghiQTNamNayUSD, value);
        }
        private string _fDeNghiQTNamNayVND;
        [ColumnAttribute("14", 13)]
        public string FDeNghiQTNamNayVND
        {
            get => _fDeNghiQTNamNayVND;
            set => SetProperty(ref _fDeNghiQTNamNayVND, value);
        }
        private string _fDeNghiChuyenNamSauUSD;
        [ColumnAttribute("15", 14)]
        public string FDeNghiChuyenNamSauUSD
        {
            get => _fDeNghiChuyenNamSauUSD;
            set => SetProperty(ref _fDeNghiChuyenNamSauUSD, value);
        }
        private string _fDeNghiChuyenNamSauVND;
        [ColumnAttribute("16", 15)]
        public string FDeNghiChuyenNamSauVND
        {
            get => _fDeNghiChuyenNamSauVND;
            set => SetProperty(ref _fDeNghiChuyenNamSauVND, value);
        }
        private string _fLuyKeKinhPhiDuocCapUSD;
        [ColumnAttribute("17", 16)]
        public string FLuyKeKinhPhiDuocCapUSD
        {
            get => _fLuyKeKinhPhiDuocCapUSD;
            set => SetProperty(ref _fLuyKeKinhPhiDuocCapUSD, value);
        }
        private string _fLuyKeKinhPhiDuocCapVND;
        [ColumnAttribute("18", 17)]
        public string FLuyKeKinhPhiDuocCapVND
        {
            get => _fLuyKeKinhPhiDuocCapVND;
            set => SetProperty(ref _fLuyKeKinhPhiDuocCapVND, value);
        }
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }
        private string _fLuyKeKinhPhiDaGiaiNganTrongNamNayUSD;
        [ColumnAttribute("19", 18)]
        public string FLuyKeKinhPhiDaGiaiNganTrongNamNayUSD
        {
            get => _fLuyKeKinhPhiDaGiaiNganTrongNamNayUSD;
            set => SetProperty(ref _fLuyKeKinhPhiDaGiaiNganTrongNamNayUSD, value);
        }
        private string _fLuyKeKinhPhiDaGiaiNganTrongNamNayVND;
        [ColumnAttribute("20", 19)]
        public string FLuyKeKinhPhiDaGiaiNganTrongNamNayVND
        {
            get => _fLuyKeKinhPhiDaGiaiNganTrongNamNayVND;
            set => SetProperty(ref _fLuyKeKinhPhiDaGiaiNganTrongNamNayVND, value);
        }

        private string _fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUSD;
        [ColumnAttribute("21", 20)]
        public string FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUSD
        {
            get => _fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUSD;
            set => SetProperty(ref _fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUSD, value);
        }

        private string _fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVND;
        [ColumnAttribute("22", 21)]
        public string FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVND
        {
            get => _fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVND;
            set => SetProperty(ref _fLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVND, value);
        }
        private string _fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUSD;
        [ColumnAttribute("23", 22)]
        public string FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUSD
        {
            get => _fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUSD;
            set => SetProperty(ref _fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUSD, value);
        }
        private string _fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVND;
        [ColumnAttribute("24", 23)]
        public string FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVND
        {
            get => _fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVND;
            set => SetProperty(ref _fLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVND, value);
        }

        private string _fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUSD;
        [ColumnAttribute("25", 24)]
        public string FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUSD
        {
            get => _fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUSD;
            set => SetProperty(ref _fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUSD, value);
        }

        private string _fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVND;
        [ColumnAttribute("26", 25)]
        public string FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVND
        {
            get => _fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVND;
            set => SetProperty(ref _fLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVND, value);
        }


        private string _fKinhPhiThuaNopNsnnUSD;
        [ColumnAttribute("27", 26)]
        public string FKinhPhiThuaNopNsnnUSD
        {
            get => _fKinhPhiThuaNopNsnnUSD;
            set => SetProperty(ref _fKinhPhiThuaNopNsnnUSD, value);
        }

        private string _fKinhPhiThuaNopNsnnVND;
        [ColumnAttribute("28", 27)]
        public string FKinhPhiThuaNopNsnnVND
        {
            get => _fKinhPhiThuaNopNsnnVND;
            set => SetProperty(ref _fKinhPhiThuaNopNsnnVND, value);
        }

        private string _fConLaiChuaGiaiNganUSD;
        [ColumnAttribute("29", 28)]
        public string FConLaiChuaGiaiNganUSD
        {
            get => _fConLaiChuaGiaiNganUSD;
            set => SetProperty(ref _fConLaiChuaGiaiNganUSD, value);
        }

        private string _fConLaiChuaGiaiNganVND;
        [ColumnAttribute("30", 29)]
        public string FConLaiChuaGiaiNganVND
        {
            get => _fConLaiChuaGiaiNganVND;
            set => SetProperty(ref _fConLaiChuaGiaiNganVND, value);
        }

        public string STenVietTatChiPhi { get; set; }
        public string TenChiPhiDisplay
        {
            get
            {
                if (string.IsNullOrEmpty(SMaChiPhi) && string.IsNullOrEmpty(STenVietTatChiPhi))
                    return string.Empty;
                return $"{this.SMaChiPhi} - {this.STenVietTatChiPhi}";
            }
        }
        public Guid? IIdKhoiTaoCapPhatID { get; set; }
        public Guid? IIdDuAnID { get; set; }
        public Guid? IIdHopDongID { get; set; }
        public Guid? IIdKhttNhiemVuChiID { get; set; }
        public Guid? IIdQuyetDinhKhacID { get; set; }
        public Guid? IIdChiPhiID { get; set; }
        public Guid? IIdParentID { get; set; }

    }
}
