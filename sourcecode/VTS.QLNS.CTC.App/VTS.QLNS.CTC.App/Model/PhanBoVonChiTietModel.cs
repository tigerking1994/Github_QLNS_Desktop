using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model
{
    public class PhanBoVonChiTietModel : DetailModelBase
    {
        private Guid _iID_DuAnID;
        public Guid iID_DuAnID
        {
            get => _iID_DuAnID;
            set => SetProperty(ref _iID_DuAnID, value);
        }

        public string sMaDuAn { get; set; }

        private string _sTenDuAn;
        public string sTenDuAn
        {
            get => _sTenDuAn;
            set => SetProperty(ref _sTenDuAn, value);
        }
        private string _sTrangThaiDuAn;
        public string sTrangThaiDuAn
        {
            get => _sTrangThaiDuAn;
            set => SetProperty(ref _sTrangThaiDuAn, value);
        }
        private string _sKhoiCong;
        public string sKhoiCong
        {
            get => _sKhoiCong;
            set => SetProperty(ref _sKhoiCong, value);
        }
        private string _sKetThuc;
        public string sKetThuc
        {
            get => _sKetThuc;
            set => SetProperty(ref _sKetThuc, value);
        }

        public string sThoiGianThucHien
        {
            get
            {
                return sKhoiCong + " - " + sKetThuc;
            }
        }

        private string _sMaKetNoi;
        public string sMaKetNoi
        {
            get => _sMaKetNoi;
            set => SetProperty(ref _sMaKetNoi, value);
        }

        public Guid? iID_LoaiCongTrinhID { get; set; }
        public Guid? iID_CapPheDuyetID { get; set; }

        private string _sTenLoaiCongTrinh;
        public string sTenLoaiCongTrinh
        {
            get => _sTenLoaiCongTrinh;
            set => SetProperty(ref _sTenLoaiCongTrinh, value);
        }
        private string _sTenCapPheDuyet;
        public string sTenCapPheDuyet
        {
            get => _sTenCapPheDuyet;
            set => SetProperty(ref _sTenCapPheDuyet, value);
        }
        private double _fGiaTriDauTu;
        public double fGiaTriDauTu
        {
            get => _fGiaTriDauTu;
            set => SetProperty(ref _fGiaTriDauTu, value);
        }
        private double _fChiTieuDauNam;
        public double fChiTieuDauNam
        {
            get => _fChiTieuDauNam;
            set => SetProperty(ref _fChiTieuDauNam, value);
        }

        private string _sLNS;
        public string sLNS
        {
            get => _sLNS;
            set => SetProperty(ref _sLNS, value);
        }

        private string _sL;
        public string sL
        {
            get => _sL;
            set => SetProperty(ref _sL, value);
        }
        private string _sK;
        public string sK
        {
            get => _sK;
            set => SetProperty(ref _sK, value);
        }
        private string _sM;
        public string sM
        {
            get => _sM;
            set => SetProperty(ref _sM, value);
        }
        private string _sTM;
        public string sTM
        {
            get => _sTM;
            set => SetProperty(ref _sTM, value);
        }
        private string _sTTM;
        public string sTTM
        {
            get => _sTTM;
            set => SetProperty(ref _sTTM, value);
        }
        private string _sNG;
        public string sNG
        {
            get => _sNG;
            set => SetProperty(ref _sNG, value);
        }
        private double _fVonDaBoTri;
        public double fVonDaBoTri
        {
            get => _fVonDaBoTri;
            set => SetProperty(ref _fVonDaBoTri, value);
        }
        private double _fVonConLai;
        public double fVonConLai
        {
            get => _fVonConLai;
            set => SetProperty(ref _fVonConLai, value);
        }
        private double? _fChiTieuNganSach;
        public double? fChiTieuNganSach
        {
            get => _fChiTieuNganSach;
            set
            {
                if (SetProperty(ref _fChiTieuNganSach, value))
                {
                    OnPropertyChanged(nameof(IsComplete));
                }
            }
        }

        private string _sLoaiDieuChinh;
        public string sLoaiDieuChinh
        {
            get => _sLoaiDieuChinh;
            set
            {
                if (SetProperty(ref _sLoaiDieuChinh, value))
                {
                    OnPropertyChanged(nameof(IsComplete));
                }
            }
        }

        private double _fSoDieuChinh;
        public double fSoDieuChinh
        {
            get => _fSoDieuChinh;
            set
            {
                if (SetProperty(ref _fSoDieuChinh, value))
                {
                    OnPropertyChanged(nameof(IsComplete));
                }
            }
        }

        public double? fChiTieuGoc { get; set; }

        private string _sGhiChu;
        public string sGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }

        private double? _fChiTieuNganSachChuaCap;
        public double? fChiTieuNganSachChuaCap
        {
            get => _fChiTieuNganSachChuaCap;
            set => SetProperty(ref _fChiTieuNganSachChuaCap, value);
        }

        private double? _fThuUngXDCB;
        public double? fThuUngXDCB
        {
            get => _fThuUngXDCB;
            set => SetProperty(ref _fThuUngXDCB, value);
        }
        private double? _fCapPhatTaiKhoBac;
        public double? FCapPhatTaiKhoBac
        {
            get => _fCapPhatTaiKhoBac;
            set => SetProperty(ref _fCapPhatTaiKhoBac, value);
        }

        private double? _fCapPhatBangLenhChi;
        public double? FCapPhatBangLenhChi
        {
            get => _fCapPhatBangLenhChi;
            set => SetProperty(ref _fCapPhatBangLenhChi, value);
        }

        private double? _fTonKhoanTaiDonVi;
        public double? FTonKhoanTaiDonVi
        {
            get => _fTonKhoanTaiDonVi;
            set => SetProperty(ref _fTonKhoanTaiDonVi, value);
        }

        private double? _fGiaTriThuHoiNamTruocKhoBac;
        public double? FGiaTriThuHoiNamTruocKhoBac
        {
            get => _fGiaTriThuHoiNamTruocKhoBac;
            set => SetProperty(ref _fGiaTriThuHoiNamTruocKhoBac, value);
        }
        
        private double? _fGiaTriThuHoiNamTruocLenhChi;
        public double? FGiaTriThuHoiNamTruocLenhChi
        {
            get => _fGiaTriThuHoiNamTruocLenhChi;
            set => SetProperty(ref _fGiaTriThuHoiNamTruocLenhChi, value);
        }
        
        private double? _fCapPhatTaiKhoBacDc;
        public double? FCapPhatTaiKhoBacDc
        {
            get => _fCapPhatTaiKhoBacDc;
            set => SetProperty(ref _fCapPhatTaiKhoBacDc, value);
        }

        private double? _fCapPhatBangLenhChiDc;
        public double? FCapPhatBangLenhChiDc
        {
            get => _fCapPhatBangLenhChiDc;
            set => SetProperty(ref _fCapPhatBangLenhChiDc, value);
        }
        private double? _fTonKhoanTaiDonViDc;
        public double? FTonKhoanTaiDonViDc
        {
            get => _fTonKhoanTaiDonViDc;
            set => SetProperty(ref _fTonKhoanTaiDonViDc, value);
        }
        private double? _fGiaTriThuHoiNamTruocKhoBacDc;
        public double? FGiaTriThuHoiNamTruocKhoBacDc
        {
            get => _fGiaTriThuHoiNamTruocKhoBacDc;
            set => SetProperty(ref _fGiaTriThuHoiNamTruocKhoBacDc, value);
        }

        private double? _fGiaTriThuHoiNamTruocLenhChiDc;
        public double? FGiaTriThuHoiNamTruocLenhChiDc
        {
            get => _fGiaTriThuHoiNamTruocLenhChiDc;
            set => SetProperty(ref _fGiaTriThuHoiNamTruocLenhChiDc, value);
        }

        private double? _fThanhToanDeXuat;
        public double? fThanhToanDeXuat
        {
            get => _fThanhToanDeXuat;
            set => SetProperty(ref _fThanhToanDeXuat, value);
        }

        public double fChiTieuBoXungTrongNam { get; set; }
        public double fNamTruocChuyenSang { get; set; }
        public bool IsComplete => _fChiTieuNganSach != 0 && (!string.IsNullOrEmpty(sM));
        public Guid? IIdParent { get; set; }
        public int? ILoaiDuAn { get; set; }
        public bool BActive { get; set; }
        public string STenDonViThucHienDuAn { get; set; }
        public string IIdMaDonViThucHienDuAn { get; set; }

        #region data combobox
        private ObservableCollection<ComboboxItem> _cbxLoaiDieuChinh;
        public ObservableCollection<ComboboxItem> CbxLoaiDieuChinh
        {
            get => _cbxLoaiDieuChinh;
            set => SetProperty(ref _cbxLoaiDieuChinh, value);
        }
        #endregion

        public Guid? IID_DuAn_HangMucID { get; set; }

    }
}
