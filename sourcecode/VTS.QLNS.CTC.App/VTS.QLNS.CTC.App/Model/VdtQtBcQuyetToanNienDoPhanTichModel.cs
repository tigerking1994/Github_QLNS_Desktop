using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtQtBcQuyetToanNienDoPhanTichModel : ModelBase
    {
        public Guid IIdDuAnId { get; set; }
        public string STenDuAn { get; set; }
        public Guid? IIdLoaiCongTrinh { get; set; }
        public string STenLoaiCongTrinh { get; set; }

        private double _fDuToanCnsChuaGiaiNganTaiKbNamTruoc;
        /// <summary>
        /// col 1
        /// </summary>
        public double FDuToanCnsChuaGiaiNganTaiKbNamTruoc
        {
            get => _fDuToanCnsChuaGiaiNganTaiKbNamTruoc;
            set
            {
                SetProperty(ref _fDuToanCnsChuaGiaiNganTaiKbNamTruoc, value);
                FTongDuToanDuocGiao = (FDuToanCnsChuaGiaiNganTaiKbNamTruoc + FDuToanCnsChuaGiaiNganTaiDvNamTruoc + FDuToanCnsChuaGiaiNganTaiCucNamTruoc + FTongChuaThuHoi)
                    + (FChiTieuNamNayKb + FChiTieuNamNayLc);
                OnPropertyChanged(nameof(FTongDuToanDuocGiao));
            }
        }

        private double _fTongChuaThuHoi;
        /// <summary>
        /// col 4
        /// </summary>
        public double FTongChuaThuHoi
        {
            get => _fTongChuaThuHoi;
            set
            {
                SetProperty(ref _fTongChuaThuHoi, value);
                FTongDuToanDuocGiao = (FDuToanCnsChuaGiaiNganTaiKbNamTruoc + FDuToanCnsChuaGiaiNganTaiDvNamTruoc + FDuToanCnsChuaGiaiNganTaiCucNamTruoc + FTongChuaThuHoi)
                    + (FChiTieuNamNayKb + FChiTieuNamNayLc);
                OnPropertyChanged(nameof(FTongDuToanDuocGiao));
            }
        }

        private double _fDuToanCnsChuaGiaiNganTaiDvNamTruoc;
        /// <summary>
        /// col 2
        /// </summary>
        public double FDuToanCnsChuaGiaiNganTaiDvNamTruoc
        {
            get => _fDuToanCnsChuaGiaiNganTaiDvNamTruoc;
            set
            {
                SetProperty(ref _fDuToanCnsChuaGiaiNganTaiDvNamTruoc, value);
                FTongDuToanDuocGiao = (FDuToanCnsChuaGiaiNganTaiKbNamTruoc + FDuToanCnsChuaGiaiNganTaiDvNamTruoc + FDuToanCnsChuaGiaiNganTaiCucNamTruoc + FTongChuaThuHoi)
                    + (FChiTieuNamNayKb + FChiTieuNamNayLc);
                OnPropertyChanged(nameof(FTongDuToanDuocGiao));
            }
        }

        private double _fDuToanCnsChuaGiaiNganTaiCucNamTruoc;
        /// <summary>
        /// col 3
        /// </summary>
        public double FDuToanCnsChuaGiaiNganTaiCucNamTruoc
        {
            get => _fDuToanCnsChuaGiaiNganTaiCucNamTruoc;
            set
            {
                SetProperty(ref _fDuToanCnsChuaGiaiNganTaiCucNamTruoc, value);
                FTongDuToanDuocGiao = (FDuToanCnsChuaGiaiNganTaiKbNamTruoc + FDuToanCnsChuaGiaiNganTaiDvNamTruoc + FDuToanCnsChuaGiaiNganTaiCucNamTruoc + FTongChuaThuHoi)
                    + (FChiTieuNamNayKb + FChiTieuNamNayLc);
                OnPropertyChanged(nameof(FTongDuToanDuocGiao));
            }
        }

        private double _fDuToanCnsChuaGiaiNganTaiKb;
        /// <summary>
        /// col 23
        /// </summary>
        public double FDuToanCnsChuaGiaiNganTaiKb
        {
            get => _fDuToanCnsChuaGiaiNganTaiKb;
            set
            {
                SetProperty(ref _fDuToanCnsChuaGiaiNganTaiKb, value);
                FTongChuyenNamSau = (FTuChuaThuHoiTaiCuc + FTuChuaThuHoiTaiDonVi) + (FDuToanCnsChuaGiaiNganTaiCuc + FDuToanCnsChuaGiaiNganTaiDv + FDuToanCnsChuaGiaiNganTaiKb);
                OnPropertyChanged(nameof(FTongChuyenNamSau));
            }
        }

        private double _fDuToanCnsChuaGiaiNganTaiDv;
        /// <summary>
        /// col 22
        /// </summary>
        public double FDuToanCnsChuaGiaiNganTaiDv
        {
            get => _fDuToanCnsChuaGiaiNganTaiDv;
            set
            {
                SetProperty(ref _fDuToanCnsChuaGiaiNganTaiDv, value);
                FTongChuyenNamSau = (FTuChuaThuHoiTaiCuc + FTuChuaThuHoiTaiDonVi) + (FDuToanCnsChuaGiaiNganTaiCuc + FDuToanCnsChuaGiaiNganTaiDv + FDuToanCnsChuaGiaiNganTaiKb);
                OnPropertyChanged(nameof(FTongChuyenNamSau));
            }
        }

        private double _fDuToanCnsChuaGiaiNganTaiCuc;
        /// <summary>
        /// col 21
        /// </summary>
        public double FDuToanCnsChuaGiaiNganTaiCuc
        {
            get => _fDuToanCnsChuaGiaiNganTaiCuc;
            set
            {
                SetProperty(ref _fDuToanCnsChuaGiaiNganTaiCuc, value);
                FTongChuyenNamSau = (FTuChuaThuHoiTaiCuc + FTuChuaThuHoiTaiDonVi) + (FDuToanCnsChuaGiaiNganTaiCuc + FDuToanCnsChuaGiaiNganTaiDv + FDuToanCnsChuaGiaiNganTaiKb);
                OnPropertyChanged(nameof(FTongChuyenNamSau));
            }
        }

        private double _fTuChuaThuHoiTaiCuc;
        /// <summary>
        /// col 18
        /// </summary>
        public double FTuChuaThuHoiTaiCuc
        {
            get => _fTuChuaThuHoiTaiCuc;
            set
            {
                SetProperty(ref _fTuChuaThuHoiTaiCuc, value);
                FTongChuaThuHoi = FTuChuaThuHoiTaiCuc + FTuChuaThuHoiTaiDonVi;
                FTongChuyenNamSau = (FTuChuaThuHoiTaiCuc + FTuChuaThuHoiTaiDonVi) + (FDuToanCnsChuaGiaiNganTaiCuc + FDuToanCnsChuaGiaiNganTaiDv + FDuToanCnsChuaGiaiNganTaiKb);
                OnPropertyChanged(nameof(FTongChuyenNamSau));
                OnPropertyChanged(nameof(FTongChuaThuHoi));
            }
        }

        private double _fChiTieuNamNayKb;
        /// <summary>
        /// col 6
        /// </summary>
        public double FChiTieuNamNayKb
        {
            get => _fChiTieuNamNayKb;
            set
            {
                SetProperty(ref _fChiTieuNamNayKb, value);
                FTongDuToanDuocGiao = (FDuToanCnsChuaGiaiNganTaiKbNamTruoc + FDuToanCnsChuaGiaiNganTaiDvNamTruoc + FDuToanCnsChuaGiaiNganTaiCucNamTruoc + FTongChuaThuHoi)
                    + (FChiTieuNamNayKb + FChiTieuNamNayLc);
                OnPropertyChanged(nameof(FTongDuToanDuocGiao));
            }
        }

        private double _fChiTieuNamNayLc;
        /// <summary>
        /// col 7
        /// </summary>
        public double FChiTieuNamNayLc
        {
            get => _fChiTieuNamNayLc;
            set
            {
                SetProperty(ref _fChiTieuNamNayLc, value);
                FTongDuToanDuocGiao = (FDuToanCnsChuaGiaiNganTaiKbNamTruoc + FDuToanCnsChuaGiaiNganTaiDvNamTruoc + FDuToanCnsChuaGiaiNganTaiCucNamTruoc + FTongChuaThuHoi)
                    + (FChiTieuNamNayKb + FChiTieuNamNayLc);
                OnPropertyChanged(nameof(FTongDuToanDuocGiao));
            }
        }

        private double _fSoCapNamTrcCs;
        /// <summary>
        /// col 10
        /// </summary>
        public double FSoCapNamTrcCs
        {
            get => _fSoCapNamTrcCs;
            set
            {
                SetProperty(ref _fSoCapNamTrcCs, value);
                FSumSoDuocCap = FSoCapNamTrcCs + FSoCapNamNay;
                OnPropertyChanged(nameof(FSumSoDuocCap));
            }
        }

        private double _fSoCapNamNay;
        /// <summary>
        /// col 11
        /// </summary>
        public double FSoCapNamNay
        {
            get => _fSoCapNamNay;
            set
            {
                SetProperty(ref _fSoCapNamNay, value);
                FSumSoDuocCap = FSoCapNamTrcCs + FSoCapNamNay;
                OnPropertyChanged(nameof(FSumSoDuocCap));
            }
        }

        private double _fDnQuyetToanNamTrc;
        /// <summary>
        /// col 13
        /// </summary>
        public double FDnQuyetToanNamTrc
        {
            get => _fDnQuyetToanNamTrc;
            set
            {
                SetProperty(ref _fDnQuyetToanNamTrc, value);
                FSumSoQuyetToan = FDnQuyetToanNamTrc + FDnQuyetToanNamNay;
                OnPropertyChanged(nameof(FSumSoQuyetToan));
            }
        }

        private double _fDnQuyetToanNamNay;
        /// <summary>
        /// col 14
        /// </summary>
        public double FDnQuyetToanNamNay
        {
            get => _fDnQuyetToanNamNay;
            set
            {
                SetProperty(ref _fDnQuyetToanNamNay, value);
                FSumSoQuyetToan = FDnQuyetToanNamTrc + FDnQuyetToanNamNay;
                OnPropertyChanged(nameof(FSumSoQuyetToan));
            }
        }

        private double _fTuChuaThuHoiTaiDonVi;
        /// <summary>
        /// col 19
        /// </summary>
        public double FTuChuaThuHoiTaiDonVi
        {
            get => _fTuChuaThuHoiTaiDonVi;
            set
            {
                SetProperty(ref _fTuChuaThuHoiTaiDonVi, value);
                FTongChuaThuHoi = FTuChuaThuHoiTaiCuc + FTuChuaThuHoiTaiDonVi;
                FTongChuyenNamSau = (FTuChuaThuHoiTaiCuc + FTuChuaThuHoiTaiDonVi) + (FDuToanCnsChuaGiaiNganTaiCuc + FDuToanCnsChuaGiaiNganTaiDv + FDuToanCnsChuaGiaiNganTaiKb);
                OnPropertyChanged(nameof(FTongChuyenNamSau));
                OnPropertyChanged(nameof(FTongChuaThuHoi));
            }
        }

        private double _fDuToanThuHoi;
        /// <summary>
        /// col 24
        /// </summary>
        public double FDuToanThuHoi
        {
            get => _fDuToanThuHoi;
            set => SetProperty(ref _fDuToanThuHoi, value);
        }

        private double _fTongDuToanDuocGiao;
        /// <summary>
        /// col 9
        /// </summary>
        public double FTongDuToanDuocGiao
        {
            get => _fTongDuToanDuocGiao;
            set => SetProperty(ref _fTongDuToanDuocGiao, value);
        }

        private double _fSumSoDuocCap;
        /// <summary>
        /// col 12
        /// </summary>
        public double FSumSoDuocCap
        {
            get => _fSumSoDuocCap;
            set => SetProperty(ref _fSumSoDuocCap, value);
        }

        private double _fSumSoQuyetToan;
        /// <summary>
        /// col 15
        /// </summary>
        public double FSumSoQuyetToan
        {
            get => _fSumSoQuyetToan;
            set => SetProperty(ref _fSumSoQuyetToan, value);
        }

        private double _fTongChuyenNamSau;
        /// <summary>
        /// col 16
        /// </summary>
        public double FTongChuyenNamSau
        {
            get => _fTongChuyenNamSau;
            set => SetProperty(ref _fTongChuyenNamSau, value);
        }
    }
}
