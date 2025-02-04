using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlDmCheDoBHXHModel : ModelBase
    {
        private string _sMaCheDo;
        [DisplayName("Mã chế độ")]
        [DisplayDetailInfo("Mã chế độ")]
        [Validate("Mã chế độ", Utility.Enum.DATA_TYPE.String, true)]
        public string SMaCheDo
        {
            get => _sMaCheDo;
            set => SetProperty(ref _sMaCheDo, value);
        }

        private string _sTenCheDo;
        [DisplayName("Tên chế độ")]
        [DisplayDetailInfo("Tên chế độ")]
        [Validate("Tên chế độ", Utility.Enum.DATA_TYPE.String, true)]
        public string STenCheDo
        {
            get => _sTenCheDo;
            set => SetProperty(ref _sTenCheDo, value);
        }

        private int? _iLoaiCheDo;
        [DisplayName("Loại chế độ")]
        [ColumnTypeAttribute(ColumnType.Combobox, "LoadLoaiPhuCap")]
        [HorizontalAttribute(HorizontalAlignment.Center)]
        public int? ILoaiCheDo
        {
            get => _iLoaiCheDo;
            set => SetProperty(ref _iLoaiCheDo, value);
        }

        private bool _isFormula;
        [DisplayName("Tính theo công thức")]
        [ColumnType(ColumnType.Checkbox)]
        public bool IsFormula
        {
            get => _isFormula;
            set => SetProperty(ref _isFormula, value);
        }

        private string _sMaCheDoCha;
        [DisplayName("Chế độ cha")]
        [ColumnTypeAttribute(ColumnType.Combobox, "LoadCheDoCha")]
        [HorizontalAttribute(HorizontalAlignment.Center)]
        public string SMaCheDoCha
        {
            get => _sMaCheDoCha;
            set
            {
                SetProperty(ref _sMaCheDoCha, value);
                OnPropertyChanged(nameof(IsHangCha));
            }
        }

        private string _sLoaiTruyLinh;
        [DisplayName("Loại truy lĩnh")]
        [ColumnTypeAttribute(ColumnType.Combobox, "LoadComboboxData")]
        [HorizontalAttribute(HorizontalAlignment.Center)]
        public string SLoaiTruyLinh
        {
            get => _sLoaiTruyLinh;
            set
            {
                SetProperty(ref _sLoaiTruyLinh, value);
            }
        }

        private decimal? _fGiaTri;
        [DisplayName("Giá trị mặc định")]
        [DisplayDetailInfo("Giá trị mặc định")]
        [FormatAttribute("{0:N4}")]
        public decimal? FGiaTri
        {
            get => _fGiaTri;
            set => SetProperty(ref _fGiaTri, value);
        }

        private string _sMoTa;
        [DisplayName("Mô tả")]
        [DisplayDetailInfo("Mô tả")]
        public string SMoTa
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }

        private string _sXauNoiMa;
        public string SXauNoiMa
        {
            get => _sXauNoiMa;
            set => SetProperty(ref _sXauNoiMa, value);
        }

        private string _sXauNoiMaMlnsBHXH;
        [DisplayName("MLNS BHXH (F6)")]
        [DisplayDetailInfo("MLNS BHXH")]
        [ColumnTypeAttribute(ColumnType.ReferencePopup)]
        public string SXauNoiMaMlnsBHXH
        {
            get => _sXauNoiMaMlnsBHXH;
            set => SetProperty(ref _sXauNoiMaMlnsBHXH, value);
        }

        private string _sMlnsBHXH;
        public string SMlnsBHXH
        {
            get => _sMlnsBHXH;
            set => SetProperty(ref _sMlnsBHXH, value);
        }

        private bool _bTinhNgayLe;
        [DisplayName("Tính ngày lễ")]
        [ColumnType(ColumnType.Checkbox)]
        public bool BTinhNgayLe
        {
            get => _bTinhNgayLe;
            set => SetProperty(ref _bTinhNgayLe, value);
        }

        private bool _bTinhCN;
        [DisplayName("Tính chủ nhật")]
        [ColumnType(ColumnType.Checkbox)]
        public bool BTinhCN
        {
            get => _bTinhCN;
            set => SetProperty(ref _bTinhCN, value);
        }

        private bool _bTinhT7;
        [DisplayName("Tính thứ 7")]
        [ColumnType(ColumnType.Checkbox)]
        public bool BTinhT7
        {
            get => _bTinhT7;
            set => SetProperty(ref _bTinhT7, value);
        }

        private bool _isDisplay;
        [DisplayName("Hiển thị theo đối tượng")]
        [ColumnType(ColumnType.Checkbox)]
        public bool IsDisplay
        {
            get => _isDisplay;
            set => SetProperty(ref _isDisplay, value);
        }

        public override bool IsHangCha => string.IsNullOrEmpty(SMaCheDoCha);

        [NotMapped]
        public string LoaiCheDoDisplay
        {
            get => ILoaiCheDo switch
            {
                1 => "Trợ cấp ốm đau",
                2 => "Trợ cấp thai sản",
                3 => "Trợ cấp tai nạn LĐ",
                4 => "Trợ cấp hưu trí, phục viên, thôi việc, tử tuất",
                5 => "Trợ cấp xuất ngũ",
                _ => ""
            };
        }

        [NotMapped]
        public string IsFormulaDisplay
        {
            get => IsFormula switch
            {
                true => "Theo công thức",
                false => "Không theo công thức"
            };
        }
    }
}
