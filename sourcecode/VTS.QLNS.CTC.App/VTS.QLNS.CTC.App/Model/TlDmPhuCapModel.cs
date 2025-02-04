using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlDmPhuCapModel : ModelBase
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
        [DisplayName("Giá trị mặc định")]
        [DisplayDetailInfo("Giá trị mặc định")]
        [FormatAttribute("{0:N4}")]
        public decimal GiaTri
        {
            get => _giaTri;
            set => SetProperty(ref _giaTri, value);
        }

        private int? _ithangToiDa;
        [DisplayName("Số tháng hưởng tối đa")]
        [DisplayDetailInfo("Số tháng hưởng tối đa")]
        [FormatAttribute("{0:N0}")]
        public int? IthangToiDa
        {
            get => _ithangToiDa;
            set => SetProperty(ref _ithangToiDa, value);
        }

        private string _tinhTncn;
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
        [DisplayDetailInfo("Phụ cấp cha")]
        [TypeOfDialogAttribute(typeof(TlDmPhuCapModel), typeof(TlDmPhuCap), typeof(TlDmPhuCapService), typeof(ITlDmPhuCapService))]
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
        //[DisplayName("TINH_BHXH")]
        //[DisplayDetailInfo("TINH_BHXH")]
        //[ColumnType(ColumnType.Checkbox)]
        public bool TinhBhxh
        {
            get => _tinhBhxh;
            set => SetProperty(ref _tinhBhxh, value);
        }

        private bool _dinhDang;
        public bool DinhDang
        {
            get => _dinhDang;
            set => SetProperty(ref _dinhDang, value);
        }

        private decimal? _huongPCSN;
        public decimal? HuongPCSN
        {
            get => _huongPCSN;
            set => SetProperty(ref _huongPCSN, value);
        }

        private string _xauNoiMa;
        public string XauNoiMa
        {
            get => _xauNoiMa;
            set => SetProperty(ref _xauNoiMa, value);
        }

        private int? _iLoai;
        [DisplayName("Loại")]
        [ColumnTypeAttribute(ColumnType.Combobox, "LoadLoaiPhuCap")]
        [HorizontalAttribute(HorizontalAlignment.Center)]
        public int? ILoai
        {
            get => _iLoai;
            set => SetProperty(ref _iLoai, value);
        }

        private int? _iDinhDang;
        [DisplayName("Định dạng")]
        [ColumnTypeAttribute(ColumnType.Combobox, "LoadĐinhDangPhuCap")]
        [HorizontalAttribute(HorizontalAlignment.Center)]
        public int? IDinhDang
        {
            get => _iDinhDang;
            set => SetProperty(ref _iDinhDang, value);
        }

        private bool? _bGiaTri;
        [DisplayName("Được sửa giá trị")]
        [ColumnType(ColumnType.Checkbox)]
        public bool? BGiaTri
        {
            get => _bGiaTri;
            set => SetProperty(ref _bGiaTri, value);
        }

        private bool? _bHuongPcSn;
        [DisplayName("Được sửa số ngày hưởng")]
        [ColumnType(ColumnType.Checkbox)]
        public bool? BHuongPcSn
        {
            get => _bHuongPcSn;
            set => SetProperty(ref _bHuongPcSn, value);
        }

        private bool? _bSaoChep;
        [DisplayName("Kiểm tra sao chép sang tháng")]
        [ColumnType(ColumnType.Checkbox)]
        public bool? BSaoChep
        {
            get => _bSaoChep;
            set => SetProperty(ref _bSaoChep, value);
        }

        public string TenPhuCapCha { get; set; }
        public override bool IsHangCha => string.IsNullOrEmpty(Parent);
        public string DisplayCheckBox => _maPhuCap + " - " + _tenPhuCap;
        public decimal SttExport { get; set; }
        public string Width { get; set; }
        public string SoNgay { get; set; }
    }
}
