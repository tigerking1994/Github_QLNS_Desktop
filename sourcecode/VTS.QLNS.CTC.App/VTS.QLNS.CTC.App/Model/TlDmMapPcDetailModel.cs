using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlDmMapPcDetailModel : ModelBase
    {
        private string _oldValue;
        public string OldValue
        {
            get => _oldValue;
            set => SetProperty(ref _oldValue, value);
        }

        private Guid? _idPhuCap;
        public Guid? IdPhuCap
        {
            get => _idPhuCap;
            set => SetProperty(ref _idPhuCap, value);
        }

        private string _maPhuCap;
        public string MaPhuCap
        {
            get => _maPhuCap;
            set => SetProperty(ref _maPhuCap, value);
        }

        private string _tenPhuCap;
        public string TenPhuCap
        {
            get => _tenPhuCap;
            set => SetProperty(ref _tenPhuCap, value);
        }

        private decimal? _giaTri;
        public decimal? Giatri
        {
            get => _giaTri;
            set => SetProperty(ref _giaTri, value);
        }

        public bool BHangCha => IsHangCha;
    }
}
