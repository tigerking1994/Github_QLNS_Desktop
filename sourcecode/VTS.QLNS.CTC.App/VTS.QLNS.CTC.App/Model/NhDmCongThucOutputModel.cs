using System.ComponentModel;
using VTS.QLNS.CTC.App.Extensions;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDmCongThucOutputModel : ModelBase
    {
        private string _sMenuSuDung;
        [DisplayName("Menu sử dụng")]
        [DisplayDetailInfo("Menu sử dụng")]
        //[Validate("Sự kiện", DATA_TYPE.String, 10, true)]
        public string SMenuSuDung
        {
            get => _sMenuSuDung;
            set => SetProperty(ref _sMenuSuDung, value);
        }

        private string _sMaOutput;
        [DisplayName("Mã output")]
        [DisplayDetailInfo("Mã output")]
        //[Validate("Sự kiện", DATA_TYPE.String, 10, true)]
        public string SMaOutput
        {
            get => _sMaOutput;
            set => SetProperty(ref _sMaOutput, value);
        }

        private string _sTenOutput;
        [DisplayName("Tên output")]
        [DisplayDetailInfo("Tên output")]
        //[Validate("Sự kiện", DATA_TYPE.String, 10, true)]
        public string STenOutput
        {
            get => _sTenOutput;
            set => SetProperty(ref _sTenOutput, value);
        }

        private string _sCongThucBangLoi;
        [DisplayName("Công thức bằng lời")]
        [DisplayDetailInfo("Công thức bằng lời")]
        //[Validate("Sự kiện", DATA_TYPE.String, 10, true)]
        public string SCongThucBangLoi
        {
            get => _sCongThucBangLoi;
            set => SetProperty(ref _sCongThucBangLoi, value);
        }

        private string _sCongThucTheoBangTongHop;
        [DisplayName("Công thức theo bảng tổng hợp")]
        [DisplayDetailInfo("Công thức theo bảng tổng hợp")]
        //[Validate("Sự kiện", DATA_TYPE.String, 10, true)]
        public string SCongThucTheoBangTongHop
        {
            get => _sCongThucTheoBangTongHop;
            set => SetProperty(ref _sCongThucTheoBangTongHop, value);
        }
    }
}
