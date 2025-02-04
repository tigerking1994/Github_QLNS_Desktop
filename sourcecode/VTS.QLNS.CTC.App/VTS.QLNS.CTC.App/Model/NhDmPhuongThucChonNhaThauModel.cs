using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDmPhuongThucChonNhaThauModel : ModelBase
    {
        private string _sMaPhuongThucChonNhaThau;
        [DisplayName("Mã phương thức chọn nhà thầu")]
        [DisplayDetailInfo("Mã phương thức chọn nhà thầu")]
        [Validate("Mã phương thức chọn nhà thầu", DATA_TYPE.String, true)]
        public string SMaPhuongThucChonNhaThau
        {
            get => _sMaPhuongThucChonNhaThau;
            set => SetProperty(ref _sMaPhuongThucChonNhaThau, value);
        }

        private string _STenVietTat;
        [DisplayName("Tên viết tắt")]
        [DisplayDetailInfo("Tên viết tắt")]
        public string STenVietTat
        {
            get => _STenVietTat;
            set => SetProperty(ref _STenVietTat, value);
        }

        private string _sTenPhuongThucChonNhaThau;
        [DisplayName("Tên phương thức chọn nhà thầu")]
        [DisplayDetailInfo("Tên viết tắt")]
        [Validate("Tên phương thức chọn nhà thầu", DATA_TYPE.String, true)]
        public string STenPhuongThucChonNhaThau
        {
            get => _sTenPhuongThucChonNhaThau;
            set => SetProperty(ref _sTenPhuongThucChonNhaThau, value);
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
    }
}
