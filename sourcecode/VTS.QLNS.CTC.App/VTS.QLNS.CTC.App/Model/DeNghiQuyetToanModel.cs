using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class DeNghiQuyetToanModel : DetailModelBase
    {
        public int Stt { get; set; }

        private string _maDuAn;
        public string MaDuAn
        {
            get => _maDuAn;
            set => SetProperty(ref _maDuAn, value);
        }

        private string _tenDuAn;
        public string TenDuAn
        {
            get => _tenDuAn;
            set => SetProperty(ref _tenDuAn, value);
        }

        private string _tenChuDauTu;
        public string TenChuDauTu
        {
            get => _tenChuDauTu;
            set => SetProperty(ref _tenChuDauTu, value);
        }

        private string _soBaoCao;
        public string SoBaoCao
        {
            get => _soBaoCao;
            set => SetProperty(ref _soBaoCao, value);
        }

        private string _nguoiTao;
        public string NguoiTao
        {
            get => _nguoiTao;
            set => SetProperty(ref _nguoiTao, value);
        }

        private DateTime? _thoiGianKhoiCong;
        public DateTime? ThoiGianKhoiCong
        {
            get => _thoiGianKhoiCong;
            set => SetProperty(ref _thoiGianKhoiCong, value);
        }

        private DateTime? _thoiGianHoanThanh;
        public DateTime? ThoiGianHoanThanh
        {
            get => _thoiGianHoanThanh;
            set => SetProperty(ref _thoiGianHoanThanh, value);
        }

        private double _giaTriDeNghiQuyetToan;
        public double GiaTriDeNghiQuyetToan
        {
            get => _giaTriDeNghiQuyetToan;
            set => SetProperty(ref _giaTriDeNghiQuyetToan, value);
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

        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
        }

        private DateTime? _thoiGianBaoCao;
        public DateTime? ThoiGianBaoCao
        {
            get => _thoiGianBaoCao;
            set => SetProperty(ref _thoiGianBaoCao, value);
        }

        public string StrThoiGianBaoCao => ThoiGianBaoCao.HasValue ? ThoiGianBaoCao.Value.ToString("dd/MM/yyyy") : string.Empty;
        public string StrThoiGianKhoiCong => ThoiGianKhoiCong.HasValue ? ThoiGianKhoiCong.Value.ToString("dd/MM/yyyy") : string.Empty;
        public string StrThoiGianHoanThanh => ThoiGianHoanThanh.HasValue ? ThoiGianHoanThanh.Value.ToString("dd/MM/yyyy") : string.Empty;
        public int SttExport { get; set; }
        public Guid? ParentId { get; set; }
        public bool? BTongHop { get; set; }

        private string _idLoaiQuyetToan;
        public string IdLoaiQuyetToan
        {
            get => _idLoaiQuyetToan;
            set => SetProperty(ref _idLoaiQuyetToan, value);
        }

        private string _sRowIndex;
        public string SRowIndex 
        {
            get => _sRowIndex;
            set => SetProperty(ref _sRowIndex, value);
        }
        private bool _isShowChildren;
        public bool IsShowChildren
        {
            get => _isShowChildren;
            set => SetProperty(ref _isShowChildren, value);
        }

        public HashSet<Guid> AncestorIds { get; internal set; }
        public bool HasChildren { get; internal set; }
    }
}
