using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NsMucLucNganSachTotalModel : BindableBase
    {
        private double _fTongTuChi;
        public double FTongTuChi
        {
            get => _fTongTuChi;
            set
            {
                SetProperty(ref _fTongTuChi, value);
                //OnPropertyChanged(nameof(FTongCapBangTien));
            }
        }
    }
}
