using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlQtChungTuNq104Model : ModelBase
    {
        public int IdChungTu { get; set; }
        public string SoChungTu { get; set; }
        public DateTime NgayTao { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string Lns { get; set; }
        public string MoTa { get; set; }
        public int? TrangThai { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModified { get; set; }
        public string GhiChu { get; set; }
        public int? ChungTuIndex { get; set; }
        public string STongHop { get; set; }

        private bool _bKhoa;
        public bool BKhoa
        {
            get => _bKhoa;
            set => SetProperty(ref _bKhoa, value);
        }

        private bool? _bDaTongHop;
        public bool? BDaTongHop
        {
            get => _bDaTongHop.GetValueOrDefault(false);
            set => SetProperty(ref _bDaTongHop, value);
        }

        private bool? _bNganSachNhanDuLieu;
        public bool? BNganSachNhanDuLieu
        {
            get => _bNganSachNhanDuLieu.GetValueOrDefault(false);
            set => SetProperty(ref _bNganSachNhanDuLieu, value);
        }

        public string BNganSachNhanDuLieuString =>
            BNganSachNhanDuLieu.GetValueOrDefault(false) ? "Ngân sách đã lấy dữ liệu" : "";

        public string BDaTongHopString => BDaTongHop.GetValueOrDefault(false) ? "Đã tổng hợp" : "";

        private bool _isSummaryVocher;
        public bool IsSummaryVocher
        {
            get => _isSummaryVocher;
            set => SetProperty(ref _isSummaryVocher, value);
        }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        private decimal _tongCong;
        public decimal TongCong
        {
            get => _tongCong;
            set => SetProperty(ref _tongCong, value);
        }

        private double _tongQuyetToan;
        public double TongQuyetToan
        {
            get => _tongQuyetToan;
            set => SetProperty(ref _tongQuyetToan, value);
        }

        public string IidChungTuDuToan { get; set; }
    }
}
