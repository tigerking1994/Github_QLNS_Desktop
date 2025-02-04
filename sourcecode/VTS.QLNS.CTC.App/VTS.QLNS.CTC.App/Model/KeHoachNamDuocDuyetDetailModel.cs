using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class KeHoachNamDuocDuyetDetailModel : DetailModelBase
    {
        public Guid iID_DuAnID { get; set; }
        public string sTenDuAn { get; set; }

        private string _lns;
        public string LNS 
        { 
            get => _lns; 
            set => SetProperty(ref _lns, value); 
        }

        private string _l;
        public string L 
        { 
            get => _l; 
            set => SetProperty(ref _l, value); 
        }

        private string _k;
        public string K 
        { 
            get => _k; 
            set => SetProperty(ref _k, value); 
        }

        private string _m;
        public string M 
        { 
            get => _m; 
            set => SetProperty(ref _m, value); 
        }

        private string _tm;
        public string TM 
        { 
            get => _tm; 
            set => SetProperty(ref _tm, value); 
        }

        private string _ttm;
        public string TTM 
        { 
            get => _ttm; 
            set => SetProperty(ref _ttm, value); 
        }

        private string _ng;
        public string NG 
        { 
            get => _ng; 
            set => SetProperty(ref _ng, value); 
        }

        private string _sGhiChu;
        public string sGhiChu 
        { 
            get => _sGhiChu; 
            set => SetProperty(ref _sGhiChu, value); 
        }

        private double? _fCapPhatTaiKhoBac;
        public double? fCapPhatTaiKhoBac 
        { 
            get => _fCapPhatTaiKhoBac; 
            set => SetProperty(ref _fCapPhatTaiKhoBac, value); 
        }

        private double? _fCapPhatBangLenhChi;
        public double? fCapPhatBangLenhChi 
        { 
            get => _fCapPhatBangLenhChi; 
            set => SetProperty(ref _fCapPhatBangLenhChi, value); 
        }

        public Guid? iID_LoaiCongTrinh { get; set; }
        public string sTenLoaiCongTrinh { get; set; }
    }
}
