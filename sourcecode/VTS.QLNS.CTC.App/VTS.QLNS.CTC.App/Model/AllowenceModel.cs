using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class AllowenceModel : DetailModelBase
    {
        public string MaPhuCap { get; set; }
        public string TenPhuCap { get; set; }

        private decimal? _giaTri;
        public decimal? GiaTri
        {
            get => _giaTri;
            set => SetProperty(ref _giaTri, value);
        }

        public string Parent { get; set; }
        public bool? IsFormula { get; set; }
        public bool? Chon { get; set; }
        public string AllowenceDisplay => String.Format("{0} - {1}", MaPhuCap, TenPhuCap);
        public string ParentName { get; set; }

        private decimal? _huongPCSN;
        public decimal? HuongPCSN
        {
            get => _huongPCSN;
            set => SetProperty(ref _huongPCSN, value);
        }

        public bool? BHangCha => IsHangCha;

        public int? SelectedYear { get; set; }
        public int? SelectedMonth { get; set; }

        private DateTime? _dateStart;
        public DateTime? DateStart
        {
            get => _dateStart;
            set => SetProperty(ref _dateStart, value);
        }

        private DateTime? _dateEnd;
        public DateTime? DateEnd
        {
            get => _dateEnd;
            set => SetProperty(ref _dateEnd, value);
        }

        private int? _isoThangHuong;
        public int? ISoThangHuong
        {
            get => _isoThangHuong;
            set => SetProperty(ref _isoThangHuong, value);
        }

        private bool? _bGiaTri;
        public bool? BGiaTri
        {
            get => _bGiaTri;
            set => SetProperty(ref _bGiaTri, value);
        }

        private bool? _bHuongPcSn;
        public bool? BHuongPcSn
        {
            get => _bHuongPcSn;
            set => SetProperty(ref _bHuongPcSn, value);
        }

        private int? _iDinhDang;
        public int? IDinhDang
        {
            get => _iDinhDang;
            set => SetProperty(ref _iDinhDang, value);
        }

        public bool? IsRemainRow { get; set; }
        public bool? BSaoChep { get; set; }

        public decimal FGiaTriNhoNhat { get; set; }
        public decimal FGiaTriLonNhat { get; set; }
        public Guid? IIdPhuCapKemTheo { get; set; }
        public string IIdMaPhuCapKemTheo { get; set; }
        public decimal FGiaTriPhuCapKemTheo { get; set; }
    }
}
