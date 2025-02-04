using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDmPhanCapPheDuyetModel : ModelBase
    {
        private string _sMa;
        [DisplayName("Mã phân cấp phê duyệt")]
        [DisplayDetailInfo("Mã phân cấp phê duyệt")]
        [Validate("Mã phân cấp phê duyệt", DATA_TYPE.String, 100, true)]
        public string SMa
        {
            get => _sMa;
            set => SetProperty(ref _sMa, value);
        }

        private string _STenVietTat;
        [DisplayName("Tên viết tắt")]
        [DisplayDetailInfo("Tên viết tắt")]
        [Validate("Tên viết tắt", DATA_TYPE.String, 300)]
        public string STenVietTat
        {
            get => _STenVietTat;
            set => SetProperty(ref _STenVietTat, value);
        }

        private string _STen;
        [DisplayName("Tên cấp phê duyệt")]
        [DisplayDetailInfo("Tên cấp phê duyệt")]
        [Validate("Tên phân cấp phê duyệt", DATA_TYPE.String, 300, true)]
        public string STen
        {
            get => _STen;
            set => SetProperty(ref _STen, value);
        }

        private string _sMoTa;
        [DisplayName("Mô tả")]
        [DisplayDetailInfo("Mô tả")]
        [Validate("Mô tả")]
        public string SMoTa
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }

        public int? IThuTu { get; set; }

        private bool? _bActive;
        public bool? BActive
        {
            get => _bActive;
            set => SetProperty(ref _bActive, value);
        }
    }
}
