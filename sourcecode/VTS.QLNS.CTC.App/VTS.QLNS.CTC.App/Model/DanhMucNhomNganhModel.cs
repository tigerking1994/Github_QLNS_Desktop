
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class DanhMucNhomNganhModel : ModelBase
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

        public string SGiaTri { get; set; }


        private string _valueToString;
        [DisplayDetailInfo("Giá trị")]
        [DisplayName("Giá trị (F6)")]
        [TypeOfDialogAttribute(typeof(DanhMucNganhModel), typeof(DanhMuc), typeof(DanhMucNganhService), typeof(DanhMucNganhService))]
        [MapperMethodAttribute("ConvertNganhToNhomNganh")]
        [InitSelectedItemsMethodAttribute("setSelectedNganh")]
        [ColumnTypeAttribute(ColumnType.ReferencePopup)]
        [IsAllowMultipleSelectAttribute(true)]
        public string ValueToString
        {
            get => _valueToString;
            set => SetProperty(ref _valueToString, value);
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

        [DisplayDetailInfo("Năm làm việc")]
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
        [DisplayDetailInfo("Tag")]
        public string Tag { get; set; }
        [DisplayDetailInfo("Log")]
        public string Log { get; set; }

        private IEnumerable<DanhMucNganhModel> _values;
        public IEnumerable<DanhMucNganhModel> Values 
        {
            get => _values;
            set
            {
                SetProperty(ref _values, value);
                ValueToString = value == null ? string.Empty : String.Join(",", value.Select(d => String.IsNullOrEmpty(d.STen) ? "" : d.STen));
            }
        }

        public override string DetailInfoModalTitle => "Chi tiết danh mục " + STen;

        public override bool IsEditable => !IsDeleted;

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

        public DanhMucNhomNganhModel()
        {
            Values = new List<DanhMucNganhModel>();
        }
    }
}
