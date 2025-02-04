using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VTS.QLNS.CTC.App.Model
{
    public partial class NhDaChuTruongDauTuNguonVonModel : CurrencyDetailModelBase
    {
        public Guid IIdChuTruongDauTuId { get; set; }
        public Guid? IIdDuAnNguonVonId { get; set; }

        private int? _iIdNguonVonId;
        public int? IIdNguonVonId
        {
            get => _iIdNguonVonId;
            set => SetProperty(ref _iIdNguonVonId, value);
        }
    }
}
