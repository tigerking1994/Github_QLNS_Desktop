using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtDaChuTruongDTNguonVonModel : ModelBase
    {
        public Guid IdChuTruongNguonVon { get; set; }
        public Guid IIdChuTruongDauTuId { get; set; }
        public string TenNguonVon { get; set; }

        private int? _iIdNguonVonId;
        public int? IIdNguonVonId
        {
            get => _iIdNguonVonId;
            set => SetProperty(ref _iIdNguonVonId, value);
        }

        private double _fTienPheDuyet;
        public double FTienPheDuyet
        {
            get => _fTienPheDuyet;
            set => SetProperty(ref _fTienPheDuyet, value);
        }
        public Guid DuAnId { get; set; }

        private double _fGiaTriDieuChinh;
        public double FGiaTriDieuChinh
        {
            get => _fGiaTriDieuChinh;
            set => SetProperty(ref _fGiaTriDieuChinh, value);
        }

        private double _giaTriTruocDieuChinh;
        public double GiaTriTruocDieuChinh
        {
            get => _giaTriTruocDieuChinh;
            set => SetProperty(ref _giaTriTruocDieuChinh, value);
        }
    }
}
