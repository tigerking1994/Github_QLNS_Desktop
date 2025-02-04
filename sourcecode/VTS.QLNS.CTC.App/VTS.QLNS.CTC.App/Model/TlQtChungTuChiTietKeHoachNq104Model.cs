using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlQtChungTuChiTietKeHoachNq104Model : ModelBase
    {
        public Guid? IdChungTu { get; set; }
        public Guid MlnsId { get; set; }
        public Guid? MlnsIdParent { get; set; }
        public string XauNoiMa { get; set; }
        public string Lns { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string Tm { get; set; }
        public string Ttm { get; set; }
        public string Ng { get; set; }
        public string Tng { get; set; }
        public string Tng1 { get; set; }
        public string Tng2 { get; set; }
        public string Tng3 { get; set; }
        public string MoTa { get; set; }
        public string Chuong { get; set; }
        public int NamNganSach { get; set; }
        public int NguonNganSach { get; set; }
        public int NamLamViec { get; set; }
        public int? Thang { get; set; }
        public int? ITrangThai { get; set; }
        public int? IThangQuy { get; set; }
        public string IdDonVi { get; set; }
        public string TenDonVi { get; set; }
        public double? MucAn { get; set; }

        private string _ghiChu;
        public string GhiChu
        {
            get => _ghiChu;
            set => SetProperty(ref _ghiChu, value);
        }

        public DateTime DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }
        public decimal? TongCong { get; set; }
        public bool? BHangCha { get; set; }
        public string ChiTietToi { get; set; }
        public int? SoNgay { get; set; }
        public int? SoNguoi { get; set; }

        private decimal? _dieuChinh;
        public decimal? DieuChinh
        {
            get => _dieuChinh;
            set
            {
                SetProperty(ref _dieuChinh, value);
                OnPropertyChanged(nameof(ChenhLech));
            }
        }

        public decimal? TongNamTruoc { get; set; }
        public bool? IsParent { get; set; }

        private decimal? _chenhLech;
        public decimal? ChenhLech
        {
            get => _chenhLech;
            set => SetProperty(ref _chenhLech, value);
        }

        public string Ngach { get; set; }
        public string MaPhuCap { get; set; }
    }
}
