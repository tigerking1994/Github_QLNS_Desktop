using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaHopDongNguonVonModel : CurrencyDetailModelBase
    {
        public Guid? IIdHopDongId { get; set; }
        public Guid? IIdCacQuyetDinhNguonVonId { get; set; }
        public Guid? IIdGoiThauNguonVonId { get; set; }
        public Guid? IIdCacQuyetDinhId { get; set; }
        public int IIdNguonVonId { get; set; }
        public double? FTienHopDongEUR { get; set; }
        public double? FTienHopDongVND { get; set; }
        public double? FTienHopDongUSD { get; set; }
        public double? FTienHopDongNgoaiTeKhac { get; set; }
        public string SMaOrder { get; set; }

        // Another properties
        public string STenNguonVon { get; set; }

        private ObservableCollection<NhDaHopDongChiPhiModel> _hopDongChiPhis;
        public ObservableCollection<NhDaHopDongChiPhiModel> HopDongChiPhis
        {
            get => _hopDongChiPhis;
            set => SetProperty(ref _hopDongChiPhis, value);
        }
    }
}
