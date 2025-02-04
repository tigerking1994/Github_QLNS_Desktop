using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class TnQtChungTuHD4554Model : ModelBase
    {
        public Guid Id { get; set; }
        private int? _iThangQuy;
        public int? IThangQuy
        {
            get => _iThangQuy;
            set => SetProperty(ref _iThangQuy, value);
        }

        public int? _iThangQuyLoai;
        public int? IThangQuyLoai
        {
            get => _iThangQuyLoai;
            set => SetProperty(ref _iThangQuyLoai, value);
        }
        private string _sThangQuyMoTa;
        public string SThangQuyMoTa
        {
            get => _sThangQuyMoTa;
            set => SetProperty(ref _sThangQuyMoTa, value);
        }
        public bool? BDaTongHop { get; set; }
        public string STongHop { get; set; }
        private double? _fTongSoTien;
        public double? FTongSoTien
        {
            get => _fTongSoTien;
            set => SetProperty(ref _fTongSoTien, value);
        }

        private double? _fTongSoTienDeNghi;
        public double? FTongSoTienDeNghi
        {
            get => _fTongSoTienDeNghi;
            set => SetProperty(ref _fTongSoTienDeNghi, value);
        }

        public string IIdMaDonVi { get; set; }
        public int? INamLamViec { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        private string _sMoTa;
        public string SMoTa
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }
        public bool? BSent { get; set; }
        public int? ISoChungTuIndex { get; set; }
        public string SDSLNS { get; set; }
        public int? INguonNganSach { get; set; }
        public int? INamNganSach { get; set; }
        private bool? _bKhoa;
        public bool? BKhoa
        {
            get => _bKhoa;
            set => SetProperty(ref _bKhoa, value);
        }
        public string STenDonVi { get;set; }
        private bool _isExpand;
        public bool IsExpand
        {
            get => _isExpand;
            set => SetProperty(ref _isExpand, value);
        }
        public bool IsChildSummary { get; set; }
        public string SoChungTuParent { get; set; }
        private bool _isCollapse;
        public bool IsCollapse
        {
            get => _isCollapse;
            set => SetProperty(ref _isCollapse, value);
        }
    }
}
