using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NHDAQDDauTuNguonVonQueryModel : BindableBase
    {
        public Guid QddtId { get; set; }
        public int NguonVonId { get; set; }

        private double? _fGiaTriNgoaiTeKhacQDDT;
        public double? FGiaTriNgoaiTeKhacQDDT
        {
            get => _fGiaTriNgoaiTeKhacQDDT;
            set => SetProperty(ref _fGiaTriNgoaiTeKhacQDDT, value);
        }

        private double? _fGiaTriUSDQDDT;
        public double? FGiaTriUSDQDDT 
        {
            get => _fGiaTriUSDQDDT;
            set => SetProperty(ref _fGiaTriUSDQDDT, value);
        }

        private double? _fGiaTriVNDQDDT;
        public double? FGiaTriVNDQDDT
        {
            get => _fGiaTriVNDQDDT;
            set => SetProperty(ref _fGiaTriVNDQDDT, value);
        }

        private double? _fGiaTriEurQDDT;
        public double? FGiaTriEurQDDT 
        {
            get => _fGiaTriEurQDDT;
            set => SetProperty(ref _fGiaTriEurQDDT, value);
        }

        private string _sTenNguonVon;
        public string STenNguonVon 
        {
            get => _sTenNguonVon;
            set => SetProperty(ref _sTenNguonVon, value);
        }

        public double? FGiaTriNgoaiTeKhacKH { get; set; }
        public double? FGiaTriUSDKH { get; set; }
        public double? FGiaTriVNDKH { get; set; }
        public double? FGiaTriEurKH { get; set; }
        public double? FGiaTriNgoaiTeKhacTT { get; set; }
        public double? FGiaTriUSDTT { get; set; }
        public double? FGiaTriVNDTT { get; set; }
        public double? FGiaTriEurTT { get; set; }

        public int STT { get; set; }
    }
}
