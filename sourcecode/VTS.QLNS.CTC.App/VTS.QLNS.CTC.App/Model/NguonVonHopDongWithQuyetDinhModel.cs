using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NguonVonHopDongWithQuyetDinhModel : ModelBase
    {
        public Guid? IIdHopDongId { get; set; }
        public Guid? IIdQuyetDinhNguonVonId { get; set; }
        public Guid? IIdCacQuyetDinhId { get; set; }
        public Guid? IIdHopDongCacQuyetDinhId { get; set; }
        public Guid? IIdGoiThauNguonVonId { get; set; }
        public int IIdNguonVonId { get; set; }
        public string STenNguonVon { get; set; }
        private double? _fGiaTriNgoaiTeKhacHopDong;
        public double? FGiaTriNgoaiTeKhacHopDong
        {
            get => _fGiaTriNgoaiTeKhacHopDong;
            set => SetProperty(ref _fGiaTriNgoaiTeKhacHopDong, value);
        }
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
        public double? FGiaTriNgoaiTeKhacDuyet { get; set; }
        public double? FGiaTriUSDDuyet { get; set; }
        public double? FGiaTriEURDuyet { get; set; }
        public double? FGiaTriVNDDuyet { get; set; }
        private double? _fGiaTriNgoaiTeKhacConLai;
        public double? FGiaTriNgoaiTeKhacConLai
        {
            get => _fGiaTriNgoaiTeKhacConLai;
            set => SetProperty(ref _fGiaTriNgoaiTeKhacConLai, value);
        }
        private double? _fGiaTriUSDConLai;
        public double? FGiaTriUSDConLai
        {
            get => _fGiaTriUSDConLai;
            set => SetProperty(ref _fGiaTriUSDConLai, value);
        }
        private double? _fGiaTriEURConLai;
        public double? FGiaTriEURConLai
        {
            get => _fGiaTriEURConLai;
            set => SetProperty(ref _fGiaTriEURConLai, value);
        }
        private double? _fGiaTriVNDConLai;
        public double? FGiaTriVNDConLai
        {
            get => _fGiaTriVNDConLai;
            set => SetProperty(ref _fGiaTriVNDConLai, value);
        }
    }
}
