using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class DmCapBacModel : ModelBase
    {
        private string _sType;
        [DisplayName("Loại chữ ký")]
        [ColumnType(ColumnType.Combobox)]
        public string SType 
        {
            get => _sType;
            set => SetProperty(ref _sType, value);
        }

        private string _iidMaDanhMuc;
        [DisplayName("Mã")]
        [DisplayDetailInfo("Mã")]
        public string IIDMaDanhMuc 
        {
            get => _iidMaDanhMuc;
            set => SetProperty(ref _iidMaDanhMuc, value);
        }

        
        private string _ten;
        //[DisplayName("Tên")]
        //[DisplayDetailInfo("Tên")]
        public string STen
        {
            get => _ten;
            set => SetProperty(ref _ten, value);
        }

        private string _value;
        [DisplayName("Giá trị")]
        [DisplayDetailInfo("Giá trị")]
        public string SGiaTri
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        private string _mota;
        [DisplayName("Mô tả")]
        [DisplayDetailInfo("Mô tả")]
        public string SMoTa
        {
            get => _mota;
            set => SetProperty(ref _mota, value);
        }

        public int? IThuTu { get; set; }

        public int? INamLamViec { get; set; }
        public int ITrangThai { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public string Tag { get; set; }
        public string Log { get; set; }
        public override bool IsEditable => !IsDeleted;
    }
}
