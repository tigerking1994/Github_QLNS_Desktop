using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDmLoaiTaiSanModel : ModelBase
    {
        private string _sMaLoaiTaiSan;
        [DisplayName("Mã loại tài sản")]
        [DisplayDetailInfo("Mã loại tài sản")]
        [Validate("Mã loại tài sản", DATA_TYPE.String, true)]
        public string SMaLoaiTaiSan
        {
            get => _sMaLoaiTaiSan;
            set => SetProperty(ref _sMaLoaiTaiSan, value);
        }

        private string _sTenLoaiTaiSan;
        [DisplayName("Tên loại tài sản")]
        [DisplayDetailInfo("loại tài sản")]
        [Validate("Tên loại tài sản", DATA_TYPE.String, true)]
        public string STenLoaiTaiSan
        {
            get => _sTenLoaiTaiSan;
            set => SetProperty(ref _sTenLoaiTaiSan, value);
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
        public string SNguoiTao { get; set; }
        public DateTime? DNgayTao { get; set; }
    }
}
