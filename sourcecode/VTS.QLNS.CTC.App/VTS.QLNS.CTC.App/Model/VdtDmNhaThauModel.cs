using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtDmNhaThauModel : ModelBase
    {
        private string _sMaNhaThau;
        [DisplayName("Mã nhà thầu")]
        [DisplayDetailInfo("Mã nhà thầu")]
        public string SMaNhaThau 
        {
            get => _sMaNhaThau;
            set => SetProperty(ref _sMaNhaThau, value);
        }

        private string _sTenNhaThau;
        [DisplayName("Tên nhà thầu")]
        [DisplayDetailInfo("Tên nhà thầu")]
        public string STenNhaThau 
        {
            get => _sTenNhaThau;
            set => SetProperty(ref _sTenNhaThau, value);
        }

        private string _sDiaChi;
        [DisplayName("Địa chỉ")]
        [DisplayDetailInfo("Địa chỉ")]
        public string SDiaChi 
        {
            get => _sDiaChi;
            set => SetProperty(ref _sDiaChi, value);
        }

        private string _sDaiDien;
        [DisplayName("Đại diện")]
        [DisplayDetailInfo("Đại diện")]
        public string SDaiDien 
        {
            get => _sDaiDien;
            set => SetProperty(ref _sDaiDien, value);
        }

        private string _sChucVu;
        [DisplayName("Chức vụ")]
        [DisplayDetailInfo("Chức vụ")]
        public string SChucVu 
        {
            get => _sChucVu;
            set => SetProperty(ref _sChucVu, value);
        }

        private string _sDienThoai;
        [DisplayName("Điện thoại")]
        [DisplayDetailInfo("Điện thoại")]
        public string SDienThoai 
        {
            get => _sDienThoai;
            set => SetProperty(ref _sDienThoai, value);
        }

        private string _sFax;
        [DisplayName("Fax")]
        [DisplayDetailInfo("Fax")]
        public string SFax 
        {
            get => _sFax;
            set => SetProperty(ref _sFax, value);
        }

        private string _sEmail;
        [DisplayName("Email")]
        [DisplayDetailInfo("Email")]
        [Validate("Email", regularExpression: RegexExpression.REGEX_EMAIL)]
        public string SEmail
        {
            get => _sEmail;
            set => SetProperty(ref _sEmail, value);
        }

        private string _sWebsite;
        [DisplayName("Website")]
        [DisplayDetailInfo("Website")]
        [Validate("Website")]
        public string SWebsite 
        {
            get => _sWebsite;
            set => SetProperty(ref _sWebsite, value);
        }

        private string _sSoTaiKhoan;
        [DisplayName("Số tài khoản")]
        [DisplayDetailInfo("Số tài khoản")]
        [Validate("Số tài khoản", regularExpression: RegexExpression.REGEX_ALPHANUMERIC)]
        public string SSoTaiKhoan
        {
            get => _sSoTaiKhoan;
            set => SetProperty(ref _sSoTaiKhoan, value);
        }

        private string _sSoTaiKhoan2;
        [DisplayName("Số tài khoản 2")]
        [DisplayDetailInfo("Số tài khoản 2")]
        [Validate("Số tài khoản 2", regularExpression: RegexExpression.REGEX_ALPHANUMERIC)]
        public string SSoTaiKhoan2 
        { 
            get => _sSoTaiKhoan2; 
            set => SetProperty(ref _sSoTaiKhoan2, value);
        }

        private string _sSoTaiKhoan3;
        [DisplayName("Số tài khoản 3")]
        [DisplayDetailInfo("Số tài khoản 3")]
        public string SSoTaiKhoan3
        {
            get => _sSoTaiKhoan3;
            set => SetProperty(ref _sSoTaiKhoan3, value);
        }

        private string _sNganHang;
        [DisplayName("Ngân hàng")]
        [DisplayDetailInfo("Ngân hàng")]
        public string SNganHang 
        {
            get => _sNganHang;
            set => SetProperty(ref _sNganHang, value);
        }

        private string _sMaSoThue;
        [DisplayName("Mã số thuế")]
        [DisplayDetailInfo("Mã số thuế")]
        [Validate("Mã số thuế", regularExpression: RegexExpression.REGEX_ALPHANUMERIC)]
        public string SMaSoThue 
        {
            get => _sMaSoThue;
            set => SetProperty(ref _sMaSoThue, value);
        }

        private string _sNguoiLienHe;
        [DisplayName("Người liên hệ")]
        [DisplayDetailInfo("Người liên hệ")]
        [Validate("SĐT liên hệ", regularExpression: RegexExpression.REGEX_PHONE_FAX)]
        public string SNguoiLienHe 
        {
            get => _sNguoiLienHe;
            set => SetProperty(ref _sNguoiLienHe, value);
        }

        private string _sDienThoaiLienHe;
        [DisplayName("Điện thoại liên hệ")]
        [DisplayDetailInfo("Điện thoại liên hệ")]
        public string SDienThoaiLienHe 
        {
            get => _sDienThoaiLienHe;
            set => SetProperty(ref _sDienThoaiLienHe, value);
        }

        private string _sMaNganHang;
        [DisplayName("Mã ngân hàng")]
        [DisplayDetailInfo("Mã ngân hàng")]
        public string SMaNganHang
        {
            get => _sMaNganHang;
            set => SetProperty(ref _sMaNganHang, value);
        }
        public string DisplayNhaThau => SMaNhaThau + " - " + STenNhaThau;
    }
}
