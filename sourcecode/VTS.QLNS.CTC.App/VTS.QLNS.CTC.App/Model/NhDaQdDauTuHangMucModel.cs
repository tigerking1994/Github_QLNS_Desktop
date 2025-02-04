using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaQdDauTuHangMucModel : CurrencyDetailModelBase
    {
        public Guid? IIdGocId { get; set; }
        public Guid? IIdQdDauTuChiPhiId { get; set; }
        public Guid? IIdParentId { get; set; }

        private string _sMaHangMuc;
        public string SMaHangMuc
        {
            get => _sMaHangMuc;
            set => SetProperty(ref _sMaHangMuc, value);
        }

        private string _sTenHangMuc;
        public string STenHangMuc
        {
            get => _sTenHangMuc;
            set => SetProperty(ref _sTenHangMuc, value);
        }

        private string _sMaOrder;
        public string SMaOrder
        {
            get => _sMaOrder;
            set => SetProperty(ref _sMaOrder, value);
        }

        private Guid? _iIdLoaiCongTrinhId;
        public Guid? IIdLoaiCongTrinhId
        {
            get => _iIdLoaiCongTrinhId;
            set => SetProperty(ref _iIdLoaiCongTrinhId, value);
        }

        // Another properties
        public Guid? IIdChuTruongDauTuHangMucId { get; set; }
        public string STenNguonVon { get; set; }
        public string STenChiPhi { get; set; }
        public bool IsSuggestion { get; set; }
    }
}
