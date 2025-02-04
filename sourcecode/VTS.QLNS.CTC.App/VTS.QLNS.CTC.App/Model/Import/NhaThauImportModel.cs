using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [Sheet(0, "Thông tin nhà thầu", 23, 0)]
    public class NhaThauImportModel : BindableBase
    {
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private bool _isErrorMLNS;
        public bool IsErrorMLNS
        {
            get => _isErrorMLNS;
            set => SetProperty(ref _isErrorMLNS, value);
        }

        private bool _isWarning;
        public bool IsWarning
        {
            get => _isWarning;
            set => SetProperty(ref _isWarning, value);
        }

        private string _loai;
        [Column("Loại", 0, ValidateType.IsIntNumber, true)]
        [Validate("Loại", DATA_TYPE.String, true)]
        public string Loai
        {
            get => _loai;
            set => SetProperty(ref _loai, value);
        }

        private string _maNhaThau;
        [Column("Mã nhà thầu", 1, ValidateType.IsString, true)]
        [Validate("Mã nhà thầu", DATA_TYPE.String, true)]
        public string MaNhaThau
        {
            get => _maNhaThau;
            set => SetProperty(ref _maNhaThau, value);
        }

        private string _tenNhaThau;
        [Column("Tên nhà thầu", 2, ValidateType.IsString, true)]
        [Validate("Tên nhà thầu", DATA_TYPE.String, true)]
        public string TenNhaThau
        {
            get => _tenNhaThau;
            set => SetProperty(ref _tenNhaThau, value);
        }

        private string _diaChi;
        [Column("Địa chỉ", 3)]
        [Validate("Địa chỉ")]
        public string DiaChi
        {
            get => _diaChi;
            set => SetProperty(ref _diaChi, value);
        }

        private string _daiDien;
        [Column("Đại diện", 4)]
        [Validate("Đại diện")]
        public string DaiDien
        {
            get => _daiDien;
            set => SetProperty(ref _daiDien, value);
        }

        private string _chucVu;
        [Column("Chức vụ", 5)]
        [Validate("Chức vụ")]
        public string ChucVu
        {
            get => _chucVu;
            set => SetProperty(ref _chucVu, value);
        }

        private string _soDienThoai;
        [Column("Số điện thoại", 6)]
        [Validate("Số điện thoại", regularExpression: RegexExpression.REGEX_PHONE_FAX)]
        public string SoDienThoai
        {
            get => _soDienThoai;
            set => SetProperty(ref _soDienThoai, value);
        }

        private string _soFax;
        [Column("Số fax", 7)]
        [Validate("Số fax", regularExpression: RegexExpression.REGEX_PHONE_FAX)]
        public string SoFax
        {
            get => _soFax;
            set => SetProperty(ref _soFax, value);
        }

        private string _email;
        [Column("Email", 8)]
        [Validate("Email", regularExpression: RegexExpression.REGEX_EMAIL)]
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _website;
        [Column("Website", 9)]
        [Validate("Website")]
        public string Website
        {
            get => _website;
            set => SetProperty(ref _website, value);
        }

        private string _soTaiKhoan;
        [Column("Số tài khoản", 10)]
        [Validate("Số tài khoản", regularExpression: RegexExpression.REGEX_ALPHANUMERIC)]
        public string SoTaiKhoan
        {
            get => _soTaiKhoan;
            set => SetProperty(ref _soTaiKhoan, value);
        }

        private string _nganHang;
        [Column("Ngân hàng", 11)]
        [Validate("Ngân hàng")]
        public string NganHang
        {
            get => _nganHang;
            set => SetProperty(ref _nganHang, value);
        }

        private string _maNganHang;
        [Column("Mã ngân hàng", 12)]
        [Validate("Mã ngân hàng")]
        public string MaNganHang
        {
            get => _maNganHang;
            set => SetProperty(ref _maNganHang, value);
        }

        private string _maSoThue;
        [Column("Mã số thuế", 13)]
        [Validate("Mã số thuế", regularExpression: RegexExpression.REGEX_ALPHANUMERIC)]
        public string MaSoThue
        {
            get => _maSoThue;
            set => SetProperty(ref _maSoThue, value);
        }

        private string _nguoiLienHe;
        [Column("Người liên hệ", 14)]
        [Validate("Người liên hệ")]
        public string NguoiLienHe
        {
            get => _nguoiLienHe;
            set => SetProperty(ref _nguoiLienHe, value);
        }

        private string _sdtLienHe;
        [Column("SĐT liên hệ", 15)]
        [Validate("SĐT liên hệ", regularExpression: RegexExpression.REGEX_PHONE_FAX)]
        public string SdtLienHe
        {
            get => _sdtLienHe;
            set => SetProperty(ref _sdtLienHe, value);
        }

        private string _soCmnd;
        [Column("Số chứng minh nhân dân", 16)]
        [Validate("Số chứng minh nhân dân", regularExpression: RegexExpression.REGEX_ALPHANUMERIC)]
        public string SoCmnd
        {
            get => _soCmnd;
            set => SetProperty(ref _soCmnd, value);
        }

        private string _noiCapCmnd;
        [Column("Nơi cấp chứng minh nhân dân", 17)]
        [Validate("Nơi cấp chứng minh nhân dân")]
        public string NoiCapCmnd
        {
            get => _noiCapCmnd;
            set => SetProperty(ref _noiCapCmnd, value);
        }

        private string _ngayCapCmnd;
        [Column("Ngày cấp chứng minh nhân dân", 18, ValidateType.IsDateTime)]
        [Validate("Ngày cấp chứng minh nhân dân")]
        public string NgayCapCmnd
        {
            get => _ngayCapCmnd;
            set => SetProperty(ref _ngayCapCmnd, value);
        }
    }
}
