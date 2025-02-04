using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlCanBoCheDoBHXHChiTietModel : ModelBase
    {
        private string _sMaCanBo;
        public string SMaCanBo
        {
            get => _sMaCanBo;
            set => SetProperty(ref _sMaCanBo, value);
        }

        private string _sMaCheDo;
        public string SMaCheDo
        {
            get => _sMaCheDo;
            set => SetProperty(ref _sMaCheDo, value);
        }

        private string _sTenCheDo;
        public string STenCheDo
        {
            get => _sTenCheDo;
            set => SetProperty(ref _sTenCheDo, value);
        }

        private DateTime? _dTuNgay;
        public DateTime? DTuNgay
        {
            get => _dTuNgay;
            set
            {
                SetProperty(ref _dTuNgay, value);
            }
        }

        private DateTime? _dDenNgay;
        public DateTime? DDenNgay
        {
            get => _dDenNgay;
            set
            {
                SetProperty(ref _dDenNgay, value);
            }
        }

        private double? _fSoNgayHuongBHXH;
        public double? FSoNgayHuongBHXH
        {
            get => _fSoNgayHuongBHXH.GetValueOrDefault();
            set
            {
                SetProperty(ref _fSoNgayHuongBHXH, value);
            }
        }

        private int? _iThang;
        public int? IThang
        {
            get => _iThang;
            set
            {
                SetProperty(ref _iThang, value);
            }
        }

        private int? _iNam;
        public int? INam
        {
            get => _iNam;
            set
            {
                SetProperty(ref _iNam, value);
            }
        }
    }
}
