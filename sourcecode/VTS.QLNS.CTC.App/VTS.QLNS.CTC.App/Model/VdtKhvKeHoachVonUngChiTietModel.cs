using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtKhvKeHoachVonUngChiTietModel : DetailModelBase
    {
        public Guid? iID_DuAnID { get; set; }
        public string sTrangThaiDuAnDangKy { get; set; }

        private double? _fCapPhatTaiKhoBac;
        public double? fCapPhatTaiKhoBac
        { 
            get => _fCapPhatTaiKhoBac; 
            set => SetProperty(ref _fCapPhatTaiKhoBac, value); 
        }

        private double? _fCapPhatBangLenhChi;
        public double? fCapPhatBangLenhChi
        {
            get => _fCapPhatBangLenhChi;
            set => SetProperty(ref _fCapPhatBangLenhChi, value);
        }

        private double? _fTonKhoanTaiDonVi;
        public double? fTonKhoanTaiDonVi
        {
            get => _fTonKhoanTaiDonVi;
            set => SetProperty(ref _fTonKhoanTaiDonVi, value);
        }

        public double fGiaTriDeNghi { get; set; }

        public Guid? iID_MucID { get; set; }
        public Guid? iID_TieuMucID { get; set; }
        public Guid? iID_TietMucID { get; set; }
        public Guid? iID_NganhID { get; set; }

        public Guid? iID_DonViTienTeID { get; set; }
        public Guid? iID_TienTeID { get; set; }
        public double? fTiGiaDonVi { get; set; }
        public double? fTiGia { get; set; }

        private string _sGhiChu;
        public string sGhiChu 
        { 
            get => _sGhiChu; 
            set => SetProperty(ref _sGhiChu, value); 
        }

        public string sMaDuAn { get; set; }
        public string sTenDuAn { get; set; }
        public double? fTongMucDauTuPheDuyet { get; set; }

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

        private double _fCapPhatTaiKhoBacTruocDieuChinh;
        public double FCapPhatTaiKhoBacTruocDieuChinh
        {
            get => _fCapPhatTaiKhoBacTruocDieuChinh;
            set => SetProperty(ref _fCapPhatTaiKhoBacTruocDieuChinh, value);
        }

        private double _fCapPhatBangLenhChiTruocDieuChinh;
        public double FCapPhatBangLenhChiTruocDieuChinh
        {
            get => _fCapPhatBangLenhChiTruocDieuChinh;
            set => SetProperty(ref _fCapPhatBangLenhChiTruocDieuChinh, value);
        }

        private double _fTonKhoanTaiDonViTruocDieuChinh;
        public double FTonKhoanTaiDonViTruocDieuChinh
        {
            get => _fTonKhoanTaiDonViTruocDieuChinh;
            set => SetProperty(ref _fTonKhoanTaiDonViTruocDieuChinh, value);
        }

        public Guid? ID_DuAn_HangMuc { get; set; }
        public string sTenHangMuc { get; set; }
    }
}
