using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class PheDuyetQuyetToanModel : ModelBase
    {
        public int Stt { get; set; }
        public int SttExport { get; set; }

        private string _soQuyetDinh;
        public string SoQuyetDinh
        {
            get => _soQuyetDinh;
            set => SetProperty(ref _soQuyetDinh, value);
        }

        private DateTime? _ngayQuyetDinh;
        public DateTime? NgayQuyetDinh
        {
            get => _ngayQuyetDinh;
            set => SetProperty(ref _ngayQuyetDinh, value);
        }

        public string StrThoiGianBaoCao => NgayQuyetDinh.HasValue ? NgayQuyetDinh.Value.ToString("dd/MM/yyyy") : string.Empty;

        private Guid? _idDuAn;
        public Guid? IdDuAn
        {
            get => _idDuAn;
            set => SetProperty(ref _idDuAn, value);
        }

        private Guid _idDeNghiQuyetToan;
        public Guid IdDeNghiQuyetToan
        {
            get => _idDeNghiQuyetToan;
            set => SetProperty(ref _idDeNghiQuyetToan, value);
        }

        private string _tenDuAn;
        public string TenDuAn
        {
            get => _tenDuAn;
            set => SetProperty(ref _tenDuAn, value);
        }

        private string _maDonVi;
        public string MaDonVi
        {
            get => _maDonVi;
            set => SetProperty(ref _maDonVi, value);
        }

        private double _tongMucDauTuPheDuyet;
        public double TongMucDauTuPheDuyet
        {
            get => _tongMucDauTuPheDuyet;
            set => SetProperty(ref _tongMucDauTuPheDuyet, value);
        }

        private double _tienQuyetToanPheDuyet;
        public double TienQuyetToanPheDuyet
        {
            get => _tienQuyetToanPheDuyet;
            set => SetProperty(ref _tienQuyetToanPheDuyet, value);
        }

        private double _chiPhiThietHai;
        public double ChiPhiThietHai
        {
            get => _chiPhiThietHai;
            set => SetProperty(ref _chiPhiThietHai, value);
        }

        private double _chiPhiKhongTaoNenTaiSan;
        public double ChiPhiKhongTaoNenTaiSan
        {
            get => _chiPhiKhongTaoNenTaiSan;
            set => SetProperty(ref _chiPhiKhongTaoNenTaiSan, value);
        }

        private double _taiSanDaiHanThuocCDTQuanLy;
        public double TaiSanDaiHanThuocCDTQuanLy
        {
            get => _taiSanDaiHanThuocCDTQuanLy;
            set => SetProperty(ref _taiSanDaiHanThuocCDTQuanLy, value);
        }

        private double _taiSanDaiHanDonViKhacQuanLy;
        public double TaiSanDaiHanDonViKhacQuanLy
        {
            get => _taiSanDaiHanDonViKhacQuanLy;
            set => SetProperty(ref _taiSanDaiHanDonViKhacQuanLy, value);
        }

        private double _taiSanNganHanThuocCDTQuanLy;
        public double TaiSanNganHanThuocCDTQuanLy
        {
            get => _taiSanNganHanThuocCDTQuanLy;
            set => SetProperty(ref _taiSanNganHanThuocCDTQuanLy, value);
        }

        private double _taiSanNganHanDonViKhacQuanLy;
        public double TaiSanNganHanDonViKhacQuanLy
        {
            get => _taiSanNganHanDonViKhacQuanLy;
            set => SetProperty(ref _taiSanNganHanDonViKhacQuanLy, value);
        }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        private string _maDuAn;
        public string MaDuAn
        {
            get => _maDuAn;
            set => SetProperty(ref _maDuAn, value);
        }

        private string _userCreate;
        public string UserCreate
        {
            get => _userCreate;
            set => SetProperty(ref _userCreate, value);
        }

        private bool _bKhoa;
        public bool BKhoa
        {
            get => _bKhoa;
            set => SetProperty(ref _bKhoa, value);
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
    }
}
