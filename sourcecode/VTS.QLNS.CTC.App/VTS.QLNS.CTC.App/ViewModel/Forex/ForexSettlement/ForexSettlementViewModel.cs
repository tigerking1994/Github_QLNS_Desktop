using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.DeNghiQTDAHT;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.QuyetToanNienDo;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.ForexAsset;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.ChuyenDuLieuQuyetToan;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.PheDuyetQuyetToanDAHT;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.ThongTriQuyetToan;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.BaoCaoQuyetToan;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement
{
    public class ForexSettlementViewModel : ViewModelBase
    {
        public override string Name => "QUYẾT TOÁN";
        public override Type ContentType => typeof(View.Forex.ForexSettlement.SettlementIndex);
        public override PackIconKind IconKind => PackIconKind.BagChecked;

        public QuyetToanNienDoIndexViewModel QuyetToanNienDoIndexViewModel { get; set; }
        public DeNghiQTDAHTIndexViewModel DeNghiQTDAHTIndexViewModel { get; set; }
        public PheDuyetQuyetToanDAHTIndexViewModel PheDuyetQuyetToanDAHTIndexViewModel { get; set; }
        public BaoCaoKetLuanQuyetToanIndexViewModel BaoCaoKetLuanQuyetToanIndexViewModel { get; set; }
        public AssetIndexViewModel AssetIndexViewModel { get; set; }
        public ChuyenDuLieuQuyetToanIndexViewModel ChuyenDulieuQuyetToanIndexViewModel { get; set; }
		public ThongTriQuyetToanIndexViewModel ThongTriQuyetToanIndexViewModel { get; set; }
        public BaoCaoTaiSanIndexViewModel BaoCaoTaiSanIndexViewModel { get; set; }

        public ForexSettlementViewModel(
            QuyetToanNienDoIndexViewModel quyetToanNienDoIndexViewModel,
            DeNghiQTDAHTIndexViewModel deNghiQTDAHTIndexViewModel,
            AssetIndexViewModel assetIndexViewModel,
            BaoCaoTaiSanIndexViewModel baoCaoTaiSanIndexViewModel,
            ChuyenDuLieuQuyetToanIndexViewModel chuyenDulieuQuyetToanIndexViewModel,
            PheDuyetQuyetToanDAHTIndexViewModel pheDuyetQuyetToanDAHTIndexViewModel,
			ThongTriQuyetToanIndexViewModel thongTriQuyetToanIndexViewModel,
            BaoCaoKetLuanQuyetToanIndexViewModel baoCaoKetLuanQuyetToanIndexViewModel)
        {
            QuyetToanNienDoIndexViewModel = quyetToanNienDoIndexViewModel;
            DeNghiQTDAHTIndexViewModel = deNghiQTDAHTIndexViewModel;
            AssetIndexViewModel = assetIndexViewModel;
            ChuyenDulieuQuyetToanIndexViewModel = chuyenDulieuQuyetToanIndexViewModel;
            PheDuyetQuyetToanDAHTIndexViewModel = pheDuyetQuyetToanDAHTIndexViewModel;
			ThongTriQuyetToanIndexViewModel = thongTriQuyetToanIndexViewModel;
            BaoCaoTaiSanIndexViewModel = baoCaoTaiSanIndexViewModel;
            BaoCaoKetLuanQuyetToanIndexViewModel = baoCaoKetLuanQuyetToanIndexViewModel;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness();
            Documentation = new ObservableCollection<ViewModelBase>()
            {
                QuyetToanNienDoIndexViewModel,
                DeNghiQTDAHTIndexViewModel,
                AssetIndexViewModel,
                ChuyenDulieuQuyetToanIndexViewModel,
                PheDuyetQuyetToanDAHTIndexViewModel,
				ThongTriQuyetToanIndexViewModel,
                BaoCaoTaiSanIndexViewModel,
                BaoCaoKetLuanQuyetToanIndexViewModel
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
