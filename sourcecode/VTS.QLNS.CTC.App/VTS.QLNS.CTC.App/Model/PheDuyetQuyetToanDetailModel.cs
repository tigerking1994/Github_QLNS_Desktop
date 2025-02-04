using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class PheDuyetQuyetToanDetailModel : DetailModelBase
    {
        private int _loai;
        public int Loai
        {
            get => _loai;
            set => SetProperty(ref _loai, value);
        }

        private string _tenLoai;
        public string TenLoai
        {
            get => _tenLoai;
            set => SetProperty(ref _tenLoai, value);
        }

        private Guid? _idChiPhi;
        public Guid? IdChiPhi
        {
            get => _idChiPhi;
            set => SetProperty(ref _idChiPhi, value);
        }

        private int _idNguonVon;
        public int IdNguonVon
        {
            get => _idNguonVon;
            set => SetProperty(ref _idNguonVon, value);
        }

        private string _tenChiPhi;
        public string TenChiPhi
        {
            get => _tenChiPhi;
            set => SetProperty(ref _tenChiPhi, value);
        }

        private double _gtDuToan;
        public double GtDuToan
        {
            get => _gtDuToan;
            set
            {
                if(SetProperty(ref _gtDuToan, value))
                {
                    OnPropertyChanged(nameof(ChenhLech));
                }
            }
        }

        private double _gtQuyetToan;
        public double GtQuyetToan
        {
            get => _gtQuyetToan;
            set
            {
                if(SetProperty(ref _gtQuyetToan, value))
                {
                    OnPropertyChanged(nameof(ChenhLech));
                }
            }
        }

        public double ChenhLech
        {
            get => GtDuToan - GtQuyetToan;
        }
    }
}
