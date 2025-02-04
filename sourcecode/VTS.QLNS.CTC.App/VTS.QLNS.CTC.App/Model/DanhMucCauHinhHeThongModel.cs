using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;

namespace VTS.QLNS.CTC.App.Model
{
    public class DanhMucCauHinhHeThongModel : ModelBase
    {
        public string SType { get; set; }

        private string _iIDMaDanhMuc;
        [DisplayName("Mã danh mục")]
        [DisplayDetailInfo("Mã danh mục")]
        public string IIDMaDanhMuc 
        {
            get => _iIDMaDanhMuc;
            set => SetProperty(ref _iIDMaDanhMuc, value);
        }

        private string _ten;
        [DisplayName("Tên danh mục")]
        [DisplayDetailInfo("Tên danh mục")]
        public string STen 
        {
            get => _ten;
            set => SetProperty(ref _ten, value);
        }

        private string _value;
        [DisplayName("Giá trị")]
        [DisplayDetailInfo("Giá trị")]
        public string SGiaTri 
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        private string _mota;
        [DisplayName("Mô tả chi tiết")]
        [DisplayDetailInfo("Mô tả chi tiết")]
        public string SMoTa 
        {
            get => _mota;
            set => SetProperty(ref _mota, value);
        }

        private int? _iThuTu;
        [DisplayName("Sắp xếp")]
        [DisplayDetailInfo("Sắp xếp")]
        public int? IThuTu 
        {
            get => _iThuTu;
            set => SetProperty(ref _iThuTu, value);
        }

        public int? INamLamViec { get; set; }
        public int ITrangThai { get; set; }
        [DisplayDetailInfo("Ngày tạo")]
        public DateTime? DNgayTao { get; set; }
        [DisplayDetailInfo("Người tạo")]
        public string SNguoiTao { get; set; }
        [DisplayDetailInfo("Ngày cập nhật")]
        public DateTime? DNgaySua { get; set; }
        [DisplayDetailInfo("Người cập nhật")]
        public string SNguoiSua { get; set; }
        public string Tag { get; set; }
        public string Log { get; set; }
        public IEnumerable<DanhMucNganhModel> Values { get; set; }
        public override string DetailInfoModalTitle => "Chi tiết danh mục " + STen;
        [DisplayDetailInfo("Trạng thái")]
        public string TrangThaiDisplay
        {
            get => ITrangThai switch
            {
                0 => "không sử dụng",
                1 => "Đang sử dụng",
                2 => "ngành nghiệp vụ",
                _ => ""
            };
        }

        public override bool IsEditable => !IsDeleted;
    }
}
