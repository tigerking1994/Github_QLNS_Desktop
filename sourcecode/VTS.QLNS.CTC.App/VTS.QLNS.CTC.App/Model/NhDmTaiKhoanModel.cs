using System.ComponentModel;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDmTaiKhoanModel : ModelBase
    {
        private string _sMaTaiKhoan;
        [DisplayName("Mã tài khoản")]
        [DisplayDetailInfo("Mã tài khoản")]
        [Validate("Mã tài khoản", DATA_TYPE.String, 10, true)]
        public string SMaTaiKhoan
        {
            get => _sMaTaiKhoan;
            set => SetProperty(ref _sMaTaiKhoan, value);
        }
        private string _sTenTaiKhoan;
        [DisplayName("Tên nội dung chi")]
        [DisplayDetailInfo("Tên nội dung chi")]
        //[Validate("Tên nội dung chi", DATA_TYPE.String, 50, true)]
        public string STenTaiKhoan
        {
            get => _sTenTaiKhoan;
            set => SetProperty(ref _sTenTaiKhoan, value);
        }

        private string _sNhomTaiKhoan;
        [DisplayName("Nhóm tài khoản")]
        [DisplayDetailInfo("Nhóm tài khoản")]
        [Validate("Mã tài khoản", DATA_TYPE.String, 10, true)]
        public string SNhomTaiKhoan
        {
            get => _sNhomTaiKhoan;
            set => SetProperty(ref _sNhomTaiKhoan, value);
        }
    }
}
