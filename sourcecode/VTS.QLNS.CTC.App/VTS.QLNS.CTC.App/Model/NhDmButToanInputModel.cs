using System.ComponentModel;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDmButToanInputModel : ModelBase
    {
        private string _sSuKien;
        [DisplayName("Sự kiện")]
        [DisplayDetailInfo("Sự kiện")]
        //[Validate("Sự kiện", DATA_TYPE.String, 10, true)]
        public string SSuKien
        {
            get => _sSuKien;
            set => SetProperty(ref _sSuKien, value);
        }

        private string _sMaButToanInput;
        [DisplayName("Mã bút toán input")]
        [DisplayDetailInfo("Mã bút toán input")]
        //[Validate("Mã bút toán input", DATA_TYPE.String, 250, true)]
        public string SMaButToanInput
        {
            get => _sMaButToanInput;
            set => SetProperty(ref _sMaButToanInput, value);
        }

        private string _sTenButToanInput;
        [DisplayName("Tên bút toán input")]
        [DisplayDetailInfo("Tên bút toán input")]
        //[Validate("Tên bút toán input", DATA_TYPE.String, 250, true)]
        public string STenButToanInput
        {
            get => _sTenButToanInput;
            set => SetProperty(ref _sTenButToanInput, value);
        }

        private string _sCongThucBangLoi;
        [DisplayName("Công thức bằng lời")]
        [DisplayDetailInfo("Công thức bằng lời")]
        //[Validate("Công thức bằng lời", DATA_TYPE.String, 10, true)]
        public string SCongThucBangLoi
        {
            get => _sCongThucBangLoi;
            set => SetProperty(ref _sCongThucBangLoi, value);
        }

        private string _sCongThucTheoBangTongHop;
        [DisplayName("Công thức theo tên")]
        [DisplayDetailInfo("Công thức theo tên")]
        //[Validate("Công thức theo tên", DATA_TYPE.String, 10, true)]
        public string SCongThucTheoBangTongHop
        {
            get => _sCongThucTheoBangTongHop;
            set => SetProperty(ref _sCongThucTheoBangTongHop, value);
        }

        public int? _iNamNganSach;
        public int? INamNganSach
        {
            get => _iNamNganSach;
            set => SetProperty(ref _iNamNganSach, value);
        }
    }
}
