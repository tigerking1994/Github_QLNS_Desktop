using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlDanhMucChucVuModel : ModelBase
    {
        private string _maCv;
        [DisplayName("Mã chức vụ")]
        [DisplayDetailInfo("Mã chức vụ")]
        public string MaCv
        {
            get => _maCv;
            set => SetProperty(ref _maCv, value);
        }

        private string _tenCv;
        [DisplayName("Tên chức vụ")]
        [DisplayDetailInfo("Tên chức vụ ")]
        public string TenCv
        {
            get => _tenCv;
            set => SetProperty(ref _tenCv, value);
        }

        private decimal? _heSoCv;
        [DisplayName("Hệ số chức vụ")]
        [DisplayDetailInfo("Hệ số chức vụ")]
        [FormatAttribute("{0:N2}")]
        public decimal? HeSoCv
        {
            get => _heSoCv;
            set => SetProperty(ref _heSoCv, value);
        }

        private decimal? _thanhTienCv;
        //[DisplayName("Tiền")]
        //[DisplayDetailInfo("Tiền")]
        //[FormatAttribute("{0:N0}")]
        public decimal? ThanhTienCv
        {
            get => _thanhTienCv;
            set => SetProperty(ref _thanhTienCv, value);
        }

        public string ChucVuDisplay => string.Format("{0} - {1}", TenCv, HeSoCv);
    }
}
