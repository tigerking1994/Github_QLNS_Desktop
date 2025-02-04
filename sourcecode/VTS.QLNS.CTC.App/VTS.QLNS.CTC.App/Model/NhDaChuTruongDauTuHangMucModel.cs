using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Utility;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VTS.QLNS.CTC.App.Model
{
    public partial class NhDaChuTruongDauTuHangMucModel : ModelBase
    {
        public Guid? IIdChuTruongDauTuId { get; set; }
        public Guid? IIdDuAnHangMucId { get; set; }

        private Guid? _iIdParentId;
        public Guid? IIdParentId
        {
            get => _iIdParentId;
            set
            {
                SetProperty(ref _iIdParentId, value);
                OnPropertyChanged(nameof(IsHangCha));
            }
        }

        private string _sMaOrder;
        public string SMaOrder
        {
            get => _sMaOrder;
            set => SetProperty(ref _sMaOrder, value);
        }

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

        private Guid? _iIdLoaiCongTrinhId;
        public Guid? IIdLoaiCongTrinhId
        {
            get => _iIdLoaiCongTrinhId;
            set => SetProperty(ref _iIdLoaiCongTrinhId, value);
        }
    }
}
