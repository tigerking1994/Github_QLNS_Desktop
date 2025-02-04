using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class SumQttBHXHChiTietGiaiThichGiamDongModel : BindableBase
    {
        private int? _iTongQuanSo;
        public int? ITongQuanSo
        {
            get => _iTongQuanSo.GetValueOrDefault();
            set => SetProperty(ref _iTongQuanSo, value);
        }

        private double? _fTongQuyTienLuongCanCu;
        public double? FTongQuyTienLuongCanCu
        {
            get => _fTongQuyTienLuongCanCu.GetValueOrDefault();
            set => SetProperty(ref _fTongQuyTienLuongCanCu, value);
        }

        private double? _fTongSoTienGiamDong;
        public double? FTongSoTienGiamDong
        {
            get => _fTongSoTienGiamDong.GetValueOrDefault();
            set => SetProperty(ref _fTongSoTienGiamDong, value);
        }
    }
}
