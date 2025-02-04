using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NsSktNganhThamDinhChiTietSktModel : DetailModelBase
    {
        public Guid IIdCtnganhThamDinh { get; set; }
        public string IIdMaDonVi { get; set; }
        public string STenDonVi { get; set; }
        public Guid IIdMucLuc { get; set; }
        public string SM { get; set; }
        public string SMoTa { get; set; }
        private double? _fTuChi;
        public double? FTuChi
        {
            get => _fTuChi;
            set => SetProperty(ref _fTuChi, value);
        }
        private string _sGhiChu;
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }
        public int INamLamViec { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public int INamNganSach { get; set; }
        public int IIdMaNguonNganSach { get; set; }
        public double? FSuDungTonKho { get; set; }
        public double? FChiDacThuNganhPhanCap { get; set; }
        public Guid? IIdMucLucParent { get; set; }
        public string SKyHieu { get; set;}
        public string SStt { get; set; }
    }
}
