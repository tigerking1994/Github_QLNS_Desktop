using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.App.Model.ConvertGenericData;
using System.Windows;
using System.Linq;

namespace VTS.QLNS.CTC.App.Model
{
    public class NSPhongBanModel : ModelBase, IDataErrorInfo
    {
        private string _iIDMaBQuanLy;
        [DisplayName("Mã phòng ban")]
        [DisplayDetailInfo("Mã phòng ban")]
        [ColumnIndexAttribute(0)]
        public string IIDMaBQuanLy
        {
            get => _iIDMaBQuanLy;
            set => SetProperty(ref _iIDMaBQuanLy, value);
        }

        public string _sTenBQuanLy;
        [DisplayName("Tên")]
        [DisplayDetailInfo("Tên")]
        public string STenBQuanLy
        {
            get => _sTenBQuanLy;
            set => SetProperty(ref _sTenBQuanLy, value);
        }

        public string _sKyHieu;
        [DisplayName("Ký hiệu")]
        [DisplayDetailInfo("Ký hiệu")]
        public string SKyHieu
        {
            get => _sKyHieu;
            set => SetProperty(ref _sKyHieu, value);
        }

        public string _sMoTa;
        [DisplayName("Mô tả")]
        [DisplayDetailInfo("Mô tả")]
        public string SMoTa
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }

        public int? _iNamLamViec;
        [FormatAttribute("####")]
        [DisplayDetailInfo("Năm làm việc")]
        [HorizontalAttribute(HorizontalAlignment.Center)]
        public int? INamLamViec
        {
            get => _iNamLamViec;
            set => SetProperty(ref _iNamLamViec, value);
        }

        public int ITrangThai { get; set; }

        private DateTime? _dNgayTao;
        [DisplayDetailInfo("Ngày tạo")]
        public DateTime? DNgayTao 
        { 
            get => _dNgayTao;
            set => SetProperty(ref _dNgayTao, value);
        }
        [DisplayDetailInfo("Người tạo")]
        public string SNguoiTao { get; set; }
        [DisplayDetailInfo("Ngày cập nhật")]
        public DateTime? DNgaySua { get; set; }
        [DisplayDetailInfo("Người cập nhật")]
        public string SNguoiSua { get; set; }
        
        public override string DetailInfoModalTitle => "Chi tiết phòng ban " + STenBQuanLy;

        public override bool IsEditable => !IsDeleted;

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                string result = null;
                switch (columnName)
                {
                    case "IdPhongBan":
                        if (String.IsNullOrEmpty(IIDMaBQuanLy))
                        {
                            return "Không được bỏ trống trường TT này";
                        }
                        var isNumeric = int.TryParse(IIDMaBQuanLy, out _);
                        if (!isNumeric)
                        {
                            return "Trường TT phải là số";
                        }
                        return null;
                }
                return result;
            }
        }
    }
}
