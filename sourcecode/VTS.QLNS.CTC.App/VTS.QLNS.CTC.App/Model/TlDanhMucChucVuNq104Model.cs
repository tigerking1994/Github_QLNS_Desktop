using System.ComponentModel;
using VTS.QLNS.CTC.App.Extensions;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlDanhMucChucVuNq104Model : ModelBase
    {
        private string _ma;
        [DisplayName("Mã chức vụ")]
        [DisplayDetailInfo("Mã chức vụ")]
        public virtual string Ma
        {
            get => _ma;
            set => SetProperty(ref _ma, value);
        }

        private string _ten;
        [DisplayName("Tên chức vụ")]
        [DisplayDetailInfo("Tên chức vụ ")]
        public virtual string Ten
        {
            get => _ten;
            set => SetProperty(ref _ten, value);
        }

        private decimal? _tienLuong;
        [DisplayName("Tiền lương chức vụ (F6)")]
        [DisplayDetailInfo("Tiền lương chức vụ")]
        [FormatAttribute("{0:#,0}")]
        public virtual decimal? TienLuong
        {
            get => _tienLuong;
            set => SetProperty(ref _tienLuong, value);
        }

        private decimal? _tienNangLuong;
        [DisplayName("Tiền nâng lương chức vụ (F6)")]
        [DisplayDetailInfo("Tiền nâng lương chức vụ")]
        [FormatAttribute("{0:#,0}")]
        public virtual decimal? TienNangLuong
        {
            get => _tienNangLuong;
            set => SetProperty(ref _tienNangLuong, value);
        }
        public bool? Loai { get; set; }
        public string XauNoiMa => $"{MaCha}-{Ma}";
        public string MaCha { get; set; }

        public int? Nam { get; set; }

    }
}
