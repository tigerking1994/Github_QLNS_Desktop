using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlDmPhuCapHeThongNq104Model : ModelBase
    {
        private string _maPhuCap;
        [DisplayName("Mã hiệu")]
        [DisplayDetailInfo("Mã hiệu")]
        public string MaPhuCap
        {
            get => _maPhuCap;
            set => SetProperty(ref _maPhuCap, value);
        }

        private string _tenPhuCap;
        [DisplayName("Tên hiệu")]
        [DisplayDetailInfo("Tên hiệu")]
        public string TenPhuCap
        {
            get => _tenPhuCap;
            set => SetProperty(ref _tenPhuCap, value);
        }

        private decimal _giaTri;
        public decimal GiaTri
        {
            get => _giaTri;
            set => SetProperty(ref _giaTri, value);
        }

        public string TenNganHang { get; set; }

        [NotMapped]
        private string _giaTriMoi;
        [DisplayName("Giá trị mặc định")]
        [DisplayDetailInfo("Giá trị mặc định")]
        [FormatAttribute("{0:N2}")]
        public string GiaTriMoi
        {
            get => _giaTriMoi;
            set => SetProperty(ref _giaTriMoi, value);
        }

        private string _tinhTncn;
        //[DisplayName("TINH_TNCN")]
        //[DisplayDetailInfo("TINH_TNCN")]
        //[ColumnType(ColumnType.Checkbox)]
        public string TinhTncn
        {
            get => _tinhTncn;
            set => SetProperty(ref _tinhTncn, value);
        }

        private string _maTtmNg;
        [DisplayDetailInfo("MA_TTM_NG")]
        public string MaTtmNg
        {
            get => _maTtmNg;
            set => SetProperty(ref _maTtmNg, value);
        }

        private bool _isFormula;
        [DisplayName("Tính theo công thức")]
        [ColumnType(ColumnType.Checkbox)]
        public bool IsFormula
        {
            get => _isFormula;
            set => SetProperty(ref _isFormula, value);
        }

        private bool? _chon;
        [DisplayName("Có sử dụng")]
        [ColumnType(ColumnType.Checkbox)]
        public bool? Chon
        {
            get => _chon;
            set => SetProperty(ref _chon, value);
        }

        private bool? _isReadonly;
        [DisplayName("Không cho phép sửa theo từng cán bộ")]
        [ColumnType(ColumnType.Checkbox)]
        public bool? IsReadonly
        {
            get => _isReadonly;
            set => SetProperty(ref _isReadonly, value);
        }

        private string _parent;
        [DisplayName("Phụ cấp cha")]
        //[ColumnTypeAttribute(ColumnType.Combobox,"")]
        [DisplayDetailInfo("Phụ cấp cha")]
        [TypeOfDialogAttribute(typeof(TlDmPhuCapHeThongNq104Model), typeof(TlDmPhuCapNq104), typeof(TlDmPhuCapHeThongNq104Service), typeof(ITlDmPhuCapHeThongService))]
        [MapperMethodAttribute("MapPhuCapChaToPhuCap")]
        [InitSelectedItemsMethodAttribute("SetSelectePhuCapChaOfPhuCap")]
        [ColumnTypeAttribute(ColumnType.ReferencePopup)]
        [IsAllowMultipleSelectAttribute(false)]
        public string Parent
        {
            get => _parent;
            set
            {
                SetProperty(ref _parent, value);
                OnPropertyChanged(nameof(IsHangCha));
            }
        }

        private bool _tinhBhxh;
        public bool TinhBhxh
        {
            get => _tinhBhxh;
            set
            {
                SetProperty(ref _tinhBhxh, value);
            }
        }

        private bool _dinhDang;
        //[DisplayName("Định dạng")]
        ////[ColumnTypeAttribute(ColumnType.Combobox, "LoadAllDinhDang")]
        //[HorizontalAttribute(HorizontalAlignment.Center)]
        //[ColumnType(ColumnType.Checkbox)]
        public bool DinhDang
        {
            get => _dinhDang;
            set
            {
                SetProperty(ref _dinhDang, value);
            }
        }

        private decimal? _huongPCSN;
        public decimal? HuongPCSN
        {
            get => _huongPCSN;
            set
            {
                SetProperty(ref _huongPCSN, value);
            }
        }

        private string _xauNoiMa;
        public string XauNoiMa
        {
            get => _xauNoiMa;
            set
            {
                SetProperty(ref _xauNoiMa, value);
            }
        }

        public bool IsHangCha
        {
            get => Parent == "";
        }

        public string DisplayCheckBox
        {
            get => _maPhuCap + " - " + _tenPhuCap;
        }

        public decimal SttExport { get; set; }
    }
}
