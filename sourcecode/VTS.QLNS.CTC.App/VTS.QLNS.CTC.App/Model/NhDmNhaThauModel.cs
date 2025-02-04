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
    public class NhDmNhaThauModel : ModelBase
    {
        public Guid? IIdGocId { get; set; }
        private int? _iLoai;
        [DisplayName("Loại")]
        [DisplayDetailInfo("Loại")]
        [ColumnType(ColumnType.Combobox)]
        public int? ILoai
        {
            get => _iLoai;
            set => SetProperty(ref _iLoai, value);
        }

        private string _sMaNhaThau;
        [DisplayName("Mã nhà thầu")]
        [DisplayDetailInfo("Mã nhà thầu")]
        [Validate("Mã nhà thầu", DATA_TYPE.String, true)]
        public string SMaNhaThau
        {
            get => _sMaNhaThau;
            set => SetProperty(ref _sMaNhaThau, value);
        }

        private string _sTenNhaThau;
        [DisplayName("Tên nhà thầu")]
        [DisplayDetailInfo("Tên nhà thầu")]
        [Validate("Tên nhà thầu", DATA_TYPE.String, true)]
        public string STenNhaThau
        {
            get => _sTenNhaThau;
            set => SetProperty(ref _sTenNhaThau, value);
        }

        private string _sDiaChi;
        [DisplayName("Địa chỉ")]
        [DisplayDetailInfo("Địa chỉ")]
        [Validate("Địa chỉ")]
        public string SDiaChi
        {
            get => _sDiaChi;
            set => SetProperty(ref _sDiaChi, value);
        }

        private string _sDaiDien;
        [DisplayName("Đại diện")]
        [DisplayDetailInfo("Đại diện")]
        [Validate("Đại diện")]
        public string SDaiDien
        {
            get => _sDaiDien;
            set => SetProperty(ref _sDaiDien, value);
        }

        private string _sChucVu;
        [DisplayName("Chức vụ")]
        [DisplayDetailInfo("Chức vụ")]
        [Validate("Chức vụ")]
        public string SChucVu
        {
            get => _sChucVu;
            set => SetProperty(ref _sChucVu, value);
        }

        private string _sDienThoai;
        [DisplayName("Số điện thoại")]
        [DisplayDetailInfo("Số điện thoại")]
        [Validate("Số điện thoại", regularExpression: RegexExpression.REGEX_PHONE_FAX)]
        public string SDienThoai
        {
            get => _sDienThoai;
            set => SetProperty(ref _sDienThoai, value);
        }

        private string _sFax;
        [DisplayName("Số Fax")]
        [DisplayDetailInfo("Số Fax")]
        [Validate("Số Fax", regularExpression: RegexExpression.REGEX_PHONE_FAX)]
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

        private string _sNganHang;
        [DisplayName("Ngân hàng")]
        [DisplayDetailInfo("Ngân hàng")]
        [Validate("Ngân hàng")]
        public string SNganHang
        {
            get => _sNganHang;
            set => SetProperty(ref _sNganHang, value);
        }

        private string _sMaNganHang;
        [DisplayName("Mã ngân hàng")]
        [DisplayDetailInfo("Mã ngân hàng")]
        [Validate("Mã ngân hàng")]
        public string SMaNganHang
        {
            get => _sMaNganHang;
            set => SetProperty(ref _sMaNganHang, value);
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
        [Validate("Người liên hệ")]
        public string SNguoiLienHe
        {
            get => _sNguoiLienHe;
            set => SetProperty(ref _sNguoiLienHe, value);
        }

        private string _sDienThoaiLienHe;
        [DisplayName("SĐT liên hệ")]
        [DisplayDetailInfo("SĐT liên hệ")]
        [Validate("SĐT liên hệ", regularExpression: RegexExpression.REGEX_PHONE_FAX)]
        public string SDienThoaiLienHe
        {
            get => _sDienThoaiLienHe;
            set => SetProperty(ref _sDienThoaiLienHe, value);
        }

        private string _sSoCmnd;
        [DisplayName("Số CMND")]
        [DisplayDetailInfo("Số CMND")]
        [Validate("Số CMND", regularExpression: RegexExpression.REGEX_ALPHANUMERIC)]
        public string SSoCmnd
        {
            get => _sSoCmnd;
            set => SetProperty(ref _sSoCmnd, value);
        }

        private string _sNoiCapCmnd;
        [DisplayName("Nơi cấp CMND")]
        [DisplayDetailInfo("Nơi cấp CMND")]
        [Validate("Nơi cấp CMND")]
        public string SNoiCapCmnd
        {
            get => _sNoiCapCmnd;
            set => SetProperty(ref _sNoiCapCmnd, value);
        }

        private DateTime? _dNgayCapCmnd;
        [DisplayName("Ngày cấp CMND")]
        [DisplayDetailInfo("Ngày cấp CMND")]
        [ColumnType(ColumnType.DateTime)]
        [Validate("Ngày cấp CMND", DATA_TYPE.Date)]
        public DateTime? DNgayCapCmnd
        {
            get => _dNgayCapCmnd;
            set => SetProperty(ref _dNgayCapCmnd, value);
        }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }

        public string DisplayNhaThau => SMaNhaThau + " - " + STenNhaThau;

        public string ILoaiString => ILoai != null && ILoai.HasValue ? LoaiNhaThau.Get(ILoai.Value) : string.Empty;
    }
}
