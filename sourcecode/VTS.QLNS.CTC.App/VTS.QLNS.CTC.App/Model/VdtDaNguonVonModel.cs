using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtDaNguonVonModel : ModelBase
    {
        //public Guid Id { get; set; }
        public Guid IIdDuAn { get; set; }
        private double? _fThanhTien;
        public double? FThanhTien
        {
            get => _fThanhTien;
            set => SetProperty(ref _fThanhTien, value);
        }
        private int? _iIdNguonVonId;
        public int? IIdNguonVonId
        {
            get => _iIdNguonVonId;
            set => SetProperty(ref _iIdNguonVonId, value);
        }
        public string TenNguonVon { get; set; }
    }
}
