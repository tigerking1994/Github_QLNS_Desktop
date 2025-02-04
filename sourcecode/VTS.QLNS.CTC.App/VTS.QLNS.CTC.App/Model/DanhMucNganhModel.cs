using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class DanhMucNganhModel : ModelBase
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
        public string SGiaTri 
        {
            get => _value;
            set => SetProperty(ref _value, value); 
        }

        private string _tenDonViNoiBo;
        //[DisplayName("Thuộc đơn vị nội bộ")]
        [TypeOfDialogAttribute(typeof(DonViModel), typeof(DonVi), typeof(NsDonViService), typeof(INsDonViService))]
        [ColumnTypeAttribute(ColumnType.ReferencePopup)]
        //[DisplayDetailInfo("Thuộc đơn vị nội bộ")]
        public string TenDonViNoiBo
        {
            get => _tenDonViNoiBo;
            set => SetProperty(ref _tenDonViNoiBo, value);
        }

        private string _moTa;
        [DisplayName("Mô tả chi tiết")]
        [DisplayDetailInfo("Mô tả chi tiết")]
        public string SMoTa 
        {
            get => _moTa;
            set => SetProperty(ref _moTa, value);
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

        private bool? _nganSachNganh;
        [DisplayDetailInfo("Có ngân sách ngành")]
        [ColumnTypeAttribute(ColumnType.Checkbox)]
        public bool? NganSachNganh 
        {
            get => _nganSachNganh;
            set => SetProperty(ref _nganSachNganh, value);
        }

        public override bool IsEditable => !IsDeleted;

        public string DisplayMemberPath => IIDMaDanhMuc + " - " + STen;

        public DanhMucNganhModel()
        {
           
        }
    }
}
