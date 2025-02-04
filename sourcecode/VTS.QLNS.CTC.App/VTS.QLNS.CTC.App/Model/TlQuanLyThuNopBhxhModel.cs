using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlQuanLyThuNopBhxhModel : ModelBase
    {
        public string _sTen;
        [DisplayName("Tên quản lý thu BHXH")]
        public string STen
        {
            get => _sTen;
            set => SetProperty(ref _sTen, value);
        }

        //[DisplayName("Từ ngày")]
        private DateTime? _dTuNgay;
        public DateTime? DTuNgay
        {
            get => _dTuNgay;
            set => SetProperty(ref _dTuNgay, value);
        }

        public string TuNgayString { get; set; }

        //[DisplayName("Đến ngày")]
        private DateTime? _dDenNgay;
        public DateTime? DDenNgay
        {
            get => _dDenNgay;
            set => SetProperty(ref _dDenNgay, value);
        }

        public string DenNgayString { get; set; }

        public string SMaPban { get; set; }

        [DisplayName("Mã cán bộ")]
        public string SMaCbo { get; set; }

        public int? _iThang;
        [DisplayName("Tháng")]
        public int? IThang
        {
            get => _iThang;
            set => SetProperty(ref _iThang, value);
        }

        public int? _iNam;
        [DisplayName("Năm")]
        public int? INam
        {
            get => _iNam;
            set => SetProperty(ref _iNam, value);
        }

        public int? ISoTt { get; set; }

        [DisplayName("Mã cách tính lương")]
        public string SMaCachTl { get; set; }

        private string _sTenDonVi;
        [DisplayName("Tên đơn vị")]
        public string STenDonVi
        {
            get => _sTenDonVi;
            set => SetProperty(ref _sTenDonVi, value);
        }

        public string IIdMaDonVi { get; set; }

        private bool? _status;
        public bool? Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        public DateTime? DNgayTao { get; set; }
        public string NgayTaoBLString { get; set; }
        public string SNguoiTao { get; set; }
        public string SMoTa { get; set; }

        private bool? _bIsKhoa;
        public bool? BIsKhoa
        {
            get => _bIsKhoa;
            set => SetProperty(ref _bIsKhoa, value);
        }

        private string _trangThai;
        public string TrangThai
        {
            get => _trangThai;
            set
            {
                SetProperty(ref _trangThai, value);
            }
        }

        private string _suDung;
        public string SuDung
        {
            get => _suDung;
            set
            {
                SetProperty(ref _suDung, value);
            }
        }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        public bool? IsTongHop { get; set; }
        public string STongHop { get; set; }
    }
}
