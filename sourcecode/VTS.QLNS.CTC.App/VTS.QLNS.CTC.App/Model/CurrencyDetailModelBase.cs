using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class CurrencyDetailModelBase : DetailModelBase
    {
        private double? _fGiaTriNgoaiTeKhac;
        public double? FGiaTriNgoaiTeKhac
        {
            get => _fGiaTriNgoaiTeKhac;
            set
            {
                if (SetProperty(ref _fGiaTriNgoaiTeKhac, value))
                {
                    OnPropertyChanged(nameof(HasValue));
                }
            }
        }

        private double? _fGiaTriUsd;
        public double? FGiaTriUsd
        {
            get => _fGiaTriUsd;
            set
            {
                if (SetProperty(ref _fGiaTriUsd, value))
                {
                    OnPropertyChanged(nameof(HasValue));
                }
            }
        }

        private double? _fGiaTriVnd;
        public double? FGiaTriVnd
        {
            get => _fGiaTriVnd;
            set
            {
                if (SetProperty(ref _fGiaTriVnd, value))
                {
                    OnPropertyChanged(nameof(HasValue));
                }
            }
        }

        private double? _fGiaTriEur;
        public double? FGiaTriEur
        {
            get => _fGiaTriEur;
            set
            {
                if (SetProperty(ref _fGiaTriEur, value))
                {
                    OnPropertyChanged(nameof(HasValue));
                }
            }
        }

        public bool HasValue
        {
            get
            {
                bool hasValueUsd = FGiaTriUsd.HasValue && FGiaTriUsd.Value != 0;
                bool hasValueEur = FGiaTriEur.HasValue && FGiaTriEur.Value != 0;
                bool hasValueVnd = FGiaTriVnd.HasValue && FGiaTriVnd.Value != 0;
                bool hasValueNgoaiTeKhac = FGiaTriNgoaiTeKhac.HasValue && FGiaTriNgoaiTeKhac.Value != 0;
                return hasValueUsd || hasValueEur || hasValueVnd || hasValueNgoaiTeKhac;
            }
        }

        private bool _canEditValue;
        public bool CanEditValue
        {
            get => _canEditValue;
            set => SetProperty(ref _canEditValue, value);
        }
    }
}
