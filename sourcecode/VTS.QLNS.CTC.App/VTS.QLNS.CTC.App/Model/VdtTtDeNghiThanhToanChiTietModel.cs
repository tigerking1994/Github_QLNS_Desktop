using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtTtDeNghiThanhToanChiTietModel : DetailModelBase
    {
        private Guid _iID_DuAnID;
        public Guid iID_DuAnID
        {
            get => _iID_DuAnID;
            set => SetProperty(ref _iID_DuAnID, value);
        }

        private Guid? _iID_HopDongID;
        public Guid? iID_HopDongID
        {
            get => _iID_HopDongID;
            set => SetProperty(ref _iID_HopDongID, value);
        }

        private Guid? _iID_NhaThauID;
        public Guid? iID_NhaThauID
        {
            get => _iID_NhaThauID;
            set => SetProperty(ref _iID_NhaThauID, value);
        }

        private double _fGiaTriThanhToanTN;
        public double fGiaTriThanhToanTN
        {
            get => _fGiaTriThanhToanTN;
            set => SetProperty(ref _fGiaTriThanhToanTN, value);
        }

        private double _fGiaTriThanhToanNN;
        public double fGiaTriThanhToanNN
        {
            get => _fGiaTriThanhToanNN;
            set => SetProperty(ref _fGiaTriThanhToanNN, value);
        }

        private double _fGiaTriThuHoiNamTruocTN;
        public double fGiaTriThuHoiNamTruocTN
        {
            get => _fGiaTriThuHoiNamTruocTN;
            set => SetProperty(ref _fGiaTriThuHoiNamTruocTN, value);
        }

        private double _fGiaTriThuHoiNamTruocNN;
        public double fGiaTriThuHoiNamTruocNN
        {
            get => _fGiaTriThuHoiNamTruocNN;
            set => SetProperty(ref _fGiaTriThuHoiNamTruocNN, value);
        }

        private double _fGiaTriThuHoiNamNayTN;
        public double fGiaTriThuHoiNamNayTN
        {
            get => _fGiaTriThuHoiNamNayTN;
            set => SetProperty(ref _fGiaTriThuHoiNamNayTN, value);
        }

        private double _fGiaTriThuHoiNamNayNN;
        public double fGiaTriThuHoiNamNayNN
        {
            get => _fGiaTriThuHoiNamNayNN;
            set => SetProperty(ref _fGiaTriThuHoiNamNayNN, value);
        }

        private string _sXauNoiMa;
        public string sXauNoiMa
        {
            get => _sXauNoiMa;
            set => SetProperty(ref _sXauNoiMa, value);
        }

        private string _sTenDuAn;
        public string sTenDuAn
        {
            get => _sTenDuAn;
            set => SetProperty(ref _sTenDuAn, value);
        }

        private double? _fLuyKeThanhToanKLHT;
        public double? fLuyKeThanhToanKLHT
        {
            get => _fLuyKeThanhToanKLHT;
            set => SetProperty(ref _fLuyKeThanhToanKLHT, value);
        }

        private double? _fChiTieuNganSachNam;
        public double? fChiTieuNganSachNam
        {
            get => _fChiTieuNganSachNam;
            set => SetProperty(ref _fChiTieuNganSachNam, value);
        }

        private double? _fGiaTriDaThanhToanTrongNam;
        public double? fGiaTriDaThanhToanTrongNam
        {
            get => _fGiaTriDaThanhToanTrongNam;
            set => SetProperty(ref _fGiaTriDaThanhToanTrongNam, value);
        }

        private double _fSoThucThanhToanDotNay;
        public double fSoThucThanhToanDotNay
        {
            get => _fSoThucThanhToanDotNay;
            set => SetProperty(ref _fSoThucThanhToanDotNay, value);
        }

        private string _sThongTinNhaThau;
        public string sThongTinNhaThau
        {
            get => _sThongTinNhaThau;
            set => SetProperty(ref _sThongTinNhaThau, value);
        }

        private string _sThongTinHopDong;
        public string sThongTinHopDong
        {
            get => _sThongTinHopDong;
            set => SetProperty(ref _sThongTinHopDong, value);
        }

        private string _sGhiChu;
        public string sGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }

        private Guid? _iId_GoiThau;
        public Guid? iId_GoiThau
        {
            get => _iId_GoiThau;
            set => SetProperty(ref _iId_GoiThau, value);
        }

        private Guid? _iID_MucID;
        public Guid? iID_MucID
        {
            get => _iID_MucID;
            set => SetProperty(ref _iID_MucID, value);
        }

        private Guid? _iID_TieuMucID;
        public Guid? iID_TieuMucID
        {
            get => _iID_TieuMucID;
            set => SetProperty(ref _iID_TieuMucID, value);
        }

        private Guid? _iID_TietMucID;
        public Guid? iID_TietMucID
        {
            get => _iID_TietMucID;
            set => SetProperty(ref _iID_TietMucID, value);
        }

        private Guid? _iID_NganhID;
        public Guid? iID_NganhID
        {
            get => _iID_NganhID;
            set => SetProperty(ref _iID_NganhID, value);
        }

        public string M { get; set; }
        public string TM { get; set; }
        public string TTM { get; set; }
        public string NG { get; set; }

        private ObservableCollection<ComboboxItem> _cbxHopDong;
        public ObservableCollection<ComboboxItem> CbxHopDong 
        { 
            get => _cbxHopDong; 
            set => SetProperty(ref _cbxHopDong, value); 
        }
    }
}
