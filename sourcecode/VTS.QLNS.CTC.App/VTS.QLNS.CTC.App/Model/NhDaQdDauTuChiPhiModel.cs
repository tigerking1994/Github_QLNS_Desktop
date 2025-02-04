using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaQdDauTuChiPhiModel : CurrencyDetailModelBase
    {
        public Guid? IIdQdDauTuId { get; set; }
        public Guid? IIdChiPhiId { get; set; }

        private Guid? _iIdParentId;
        public Guid? IIdParentId
        {
            get => _iIdParentId;
            set
            {
                SetProperty(ref _iIdParentId, value);
                OnPropertyChanged(nameof(IsHangCha));
            }
        }

        private string _sMaOrder;
        public string SMaOrder
        {
            get => _sMaOrder;
            set => SetProperty(ref _sMaOrder, value);
        }

        private string _sMaChiPhi;
        public string SMaChiPhi
        {
            get => _sMaChiPhi;
            set => SetProperty(ref _sMaChiPhi, value);
        }

        private string _sTenChiPhi;
        public string STenChiPhi
        {
            get => _sTenChiPhi;
            set => SetProperty(ref _sTenChiPhi, value);
        }

        public Guid? IIdQDDauTuNguonVonId { get; set; }

        // Another properties

        private ObservableCollection<NhDaQdDauTuHangMucModel> _qdDauTuHangMucs = new ObservableCollection<NhDaQdDauTuHangMucModel>();
        public ObservableCollection<NhDaQdDauTuHangMucModel> QdDauTuHangMucs
        {
            get => _qdDauTuHangMucs;
            set => SetProperty(ref _qdDauTuHangMucs, value);
        }

        public string STenNguonVon { get; set; }

        public ObservableCollection<NhDmChiPhiModel> ItemsLoaiNoiDungChi { get; set; }

        public virtual bool IsLoaiNoiDungChi => IIdParentId == null;

        public virtual bool IsNoiDungChi => IIdParentId != null;
    }
}
