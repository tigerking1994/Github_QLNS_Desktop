using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlDmTietTieuMucNganhModel : ModelBase
    {
        private string _maTtmNg;
        [DisplayName("Mã tiết tiểu mục ngành")]
        [DisplayDetailInfo("Mã tiết tiểu mục ngành")]
        public string MaTtmNg
        {
            get => _maTtmNg;
            set => SetProperty(ref _maTtmNg, value);
        }

        private string _tenTtmNg;
        [DisplayName("Tên tiết tiểu mục ngành")]
        [DisplayDetailInfo("Tên tiết tiểu mục ngành")]
        public string TenTtmNg
        {
            get => _tenTtmNg;
            set => SetProperty(ref _tenTtmNg, value);
        }
    }
}
