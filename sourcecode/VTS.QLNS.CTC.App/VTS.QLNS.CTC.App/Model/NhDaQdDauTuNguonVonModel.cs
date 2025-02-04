using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaQdDauTuNguonVonModel : CurrencyDetailModelBase
    {
        public Guid? IIdQdDauTuId { get; set; }

        private int? _iIdNguonVonId;
        public int? IIdNguonVonId
        {
            get => _iIdNguonVonId;
            set => SetProperty(ref _iIdNguonVonId, value);
        }

        public Guid? IIdChuTruongDauTuNguonVonId { get; set; }

        // Another properties

        private ObservableCollection<NhDaQdDauTuChiPhiModel> _qdDauTuChiPhis = new ObservableCollection<NhDaQdDauTuChiPhiModel>();
        public ObservableCollection<NhDaQdDauTuChiPhiModel> QdDauTuChiPhis
        {
            get => _qdDauTuChiPhis;
            set => SetProperty(ref _qdDauTuChiPhis, value);
        }

        public NhDaChuTruongDauTuNguonVon nhDaChuTruongDauTuNguonVon { get; set; } = new NhDaChuTruongDauTuNguonVon();
    }
}
