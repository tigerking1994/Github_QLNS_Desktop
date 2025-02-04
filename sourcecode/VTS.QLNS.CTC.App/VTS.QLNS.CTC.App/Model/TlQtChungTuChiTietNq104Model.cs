using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlQtChungTuChiTietNq104Model : ModelBase
    {
        public Guid IdChungTu { get; set; }
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

        private decimal? _tongCong;
        public decimal? TongCong
        {
            get => _tongCong;
            set => SetProperty(ref _tongCong, value);
        }

        private bool? _bHangCha;
        public bool? BHangCha
        {
            get => _bHangCha;
            set => SetProperty(ref _bHangCha, value);
        }

        public string ChiTietToi { get; set; }
        public bool? IsParent { get; set; }

        private int? _iSoNgay;
        public int? SoNgay 
        { 
            get => _iSoNgay; 
            set => SetProperty(ref _iSoNgay, value); 
        }

        private int? _iSoNguoi;
        public int? SoNguoi 
        { 
            get => _iSoNguoi; 
            set => SetProperty(ref _iSoNguoi, value); 
        }

        private decimal? _dieuChinh;
        public decimal? DieuChinh
        {
            get => _dieuChinh;
            set => SetProperty(ref _dieuChinh, value);
        }
        public string MaCachTl { get; set; }

        private decimal? _dDuToan;
        public decimal? DDuToan
        {
            get => _dDuToan;
            set => SetProperty(ref _dDuToan, value);
        }

        public decimal? TongLuyKe { get; set; }
        public string MaCb { get; set; }

        //other properties
        public string MaCbCha { get; set; }
        public string LoaiDoiTuong { get; set; }
    }
}
