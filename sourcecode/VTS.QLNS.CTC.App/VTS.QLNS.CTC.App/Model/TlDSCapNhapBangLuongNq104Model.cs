using System;
using System.ComponentModel;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlDSCapNhapBangLuongNq104Model : ModelBase
    {

        [DisplayName("Tên danh sách cập nhập bảng lương")]
        public string TenDsCnbluong { get; set; }

        //[DisplayName("Từ ngày")]
        private DateTime? _tuNgay;
        public DateTime? TuNgay
        {
            get => _tuNgay;
            set => SetProperty(ref _tuNgay, value);
        }

        public string TuNgayString { get; set; }

        //[DisplayName("Đến ngày")]
        private DateTime? _denNgay;
        public DateTime? DenNgay
        {
            get => _denNgay;
            set => SetProperty(ref _denNgay, value);
        }

        public string DenNgayString { get; set; }

        public string MaPban { get; set; }

        [DisplayName("Mã cán bộ")]
        public string MaCbo { get; set; }

        [DisplayName("Tháng")]
        public int? Thang { get; set; }

        [DisplayName("Năm")]
        public int? Nam { get; set; }

        public int? SoTt { get; set; }

        [DisplayName("Mã cách tính lương")]
        public string MaCachTl { get; set; }

        [DisplayName("Tên đơn vị")]
        public string TenDonVi { get; set; }
        public string MaDonVi { get; set; }

        private bool? _status;
        public bool? Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        public DateTime? NgayTaoBL { get; set; }
        public string NgayTaoBLString { get; set; }
        public string NguoiTao { get; set; }
        public string Note { get; set; }

        private bool? _khoaBangLuong;
        public bool? KhoaBangLuong
        {
            get => _khoaBangLuong;
            set => SetProperty(ref _khoaBangLuong, value);
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
    }
}
