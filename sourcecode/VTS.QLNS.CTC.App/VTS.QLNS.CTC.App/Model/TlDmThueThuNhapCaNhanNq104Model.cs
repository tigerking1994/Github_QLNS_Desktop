using System.ComponentModel;
using System.Windows;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlDmThueThuNhapCaNhanNq104Model : ModelBase
    {
        private string _iIsThueThang;
        [DisplayName("Thuế tháng/năm")]
        [ColumnTypeAttribute(ColumnType.Combobox, "LoadLoaiPhuCap")]
        [HorizontalAttribute(HorizontalAlignment.Center)]
        public string IIsThueThang
        {
            get => _iIsThueThang;
            set => SetProperty(ref _iIsThueThang, value);
        }

        private bool _bIsThueThang;
        public bool BIsThueThang
        {
            get => _bIsThueThang;
            set => SetProperty(ref _bIsThueThang, value);
        }

        private string _loaiThue;
        [DisplayName("Loại thuế")]
        [DisplayDetailInfo("Loại thuế")]
        public string LoaiThue
        {
            get => _loaiThue;
            set => SetProperty(ref _loaiThue, value);
        }

        private string _tenThue;
        [DisplayName("Tên thuế")]
        [DisplayDetailInfo("Tên thuế")]
        public string TenThue
        {
            get => _tenThue;
            set => SetProperty(ref _tenThue, value);
        }

        private decimal? _thuNhapTu;
        [DisplayName("Thu nhập từ")]
        [DisplayDetailInfo("Thu nhập từ")]
        [FormatAttribute("{0:N0}")]
        public decimal? ThuNhapTu
        {
            get => _thuNhapTu;
            set => SetProperty(ref _thuNhapTu, value);
        }

        private decimal? _thuNhapDen;
        [DisplayName("Thu nhập đến")]
        [DisplayDetailInfo("Thu nhập đến")]
        [FormatAttribute("{0:N0}")]
        public decimal? ThuNhapDen
        {
            get => _thuNhapDen;
            set => SetProperty(ref _thuNhapDen, value);
        }

        private decimal? _thueXuat;
        [DisplayName("Thuế xuất(%)")]
        [DisplayDetailInfo("Thuế xuất(%)")]
        [FormatAttribute("{0:N2}")]
        public decimal? ThueXuat
        {
            get => _thueXuat;
            set => SetProperty(ref _thueXuat, value);
        }
    }
}
