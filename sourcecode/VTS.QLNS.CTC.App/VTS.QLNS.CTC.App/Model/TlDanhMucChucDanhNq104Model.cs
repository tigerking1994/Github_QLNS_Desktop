using System.ComponentModel;
using VTS.QLNS.CTC.App.Extensions;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlDanhMucChucDanhNq104Model : TlDanhMucChucVuNq104Model
    {
        private string _ma;
        [DisplayName("Mã chức danh")]
        [DisplayDetailInfo("Mã chức danh")]
        public override string Ma
        {
            get => _ma;
            set => SetProperty(ref _ma, value);
        }

        private string _ten;
        [DisplayName("Tên chức danh")]
        [DisplayDetailInfo("Tên chức danh ")]
        public override string Ten
        {
            get => _ten;
            set => SetProperty(ref _ten, value);
        }

        private decimal? _tienLuong;
        [DisplayName("Tiền lương chức danh (F6)")]
        [DisplayDetailInfo("Tiền lương chức danh")]
        [FormatAttribute("{0:#,0}")]
        public override decimal? TienLuong
        {
            get => _tienLuong;
            set => SetProperty(ref _tienLuong, value);
        }
        private decimal? _tienNangLuong;
        [DisplayName("Tiền nâng lương chức danh (F6)")]
        [DisplayDetailInfo("Tiền nâng lương chức danh")]
        [FormatAttribute("{0:#,0}")]
        public override decimal? TienNangLuong
        {
            get => _tienNangLuong;
            set => SetProperty(ref _tienNangLuong, value);
        }
    }
}
