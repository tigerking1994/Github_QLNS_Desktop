using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaHopDongChiPhiModel : CurrencyDetailModelBase
    {
        public Guid? IIdHopDongId { get; set; }
        public Guid? IIdChiPhiId { get; set; }
        public Guid? IIdParentId { get; set; }
        public Guid? IIdCacQuyetDinhChiPhiID { get; set; }
        public Guid? IIdCacQuyetDinhId { get; set; }
        public Guid? IIdGoiThauChiPhiId { get; set; }
        public double? FTienHopDongEUR { get; set; }
        public double? FTienHopDongVND { get; set; }
        public double? FTienHopDongUSD { get; set; }
        public double? FTienHopDongNgoaiTeKhac { get; set; }
        public string STenChiPhi { get; set; }
        public string SMaOrder { get; set; }
        public Guid? IIdHopDongGoiThauNhaThauId { get; set; }
        public Guid? IIdHopDongNguonVonId { get; set; }

        // Another properties
        public string STenNguonVon { get; set; }

        private ObservableCollection<NhDaHopDongHangMucModel> _hopDongHangMucs = new ObservableCollection<NhDaHopDongHangMucModel>();
        public ObservableCollection<NhDaHopDongHangMucModel> HopDongHangMucs
        {
            get => _hopDongHangMucs;
            set => SetProperty(ref _hopDongHangMucs, value);
        }
        public ObservableCollection<NhDmChiPhiModel> ItemsLoaiNoiDungChi { get; set; }
        public NhDmChiPhiModel SelectedLoaiNoiDungChi { get; set; }

        public virtual bool IsLoaiNoiDungChi => IIdParentId == null;

        public virtual bool IsNoiDungChi => IIdParentId != null;
        public string SMaChiPhi { get; set; }

    }
}
