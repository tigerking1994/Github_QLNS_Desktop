using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class QuyetDinhModel : ModelBase
    {
        public string SSoQuyetDinh { get; set; }
        public string SMoTa { get; set; }
        public double? FGiaTriUSDDuyet { get; set; }

        public double? FGiaTriVNDDuyet { get; set; }

        public double? FGiaTriEURDuyet { get; set; }
        public double? FGiaTriNgoaiTeKhacDuyet { get; set; }

        private double? _fGiaTriUSDHopDong;
        public double? FGiaTriUSDHopDong
        {
            get => _fGiaTriUSDHopDong;
            set => SetProperty(ref _fGiaTriUSDHopDong, value);
        }

        private double? _fGiaTriEURHopDong;
        public double? FGiaTriEURHopDong
        {
            get => _fGiaTriEURHopDong;
            set => SetProperty(ref _fGiaTriEURHopDong, value);
        }

        private double? _fGiaTriVNDHopDong;
        public double? FGiaTriVNDHopDong
        {
            get => _fGiaTriVNDHopDong;
            set => SetProperty(ref _fGiaTriVNDHopDong, value);
        }

        private double? _fGiaTriNgoaiTeKhacHopDong;
        public double? FGiaTriNgoaiTeKhacHopDong
        {
            get => _fGiaTriNgoaiTeKhacHopDong;
            set => SetProperty(ref _fGiaTriNgoaiTeKhacHopDong, value);
        }
    }
}
