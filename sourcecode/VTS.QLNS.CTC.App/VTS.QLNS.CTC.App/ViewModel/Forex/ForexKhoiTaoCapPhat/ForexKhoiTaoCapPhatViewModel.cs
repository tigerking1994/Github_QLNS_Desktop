using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexKhoiTaoCapPhat.ForexDanhSachKhoiTao;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexKhoiTaoCapPhat.KhoiTaoTheoQuyetDinh;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexKhoiTaoCapPhat
{
    public class ForexKhoiTaoCapPhatViewModel : ViewModelBase
    {
        public override string Name => "KHỞI TẠO";
        public override Type ContentType => typeof(View.Forex.ForexKhoiTaoCapPhat.ForexKhoiTaoCapPhat);
        public override PackIconKind IconKind => PackIconKind.BagChecked;
        public ForexDanhSachKhoiTaoIndexViewModel ForexDanhSachKhoiTaoIndexViewModel { get; }
        public ForexKhoiTaoTheoQuyetDinhIndexViewModel ForexKhoiTaoTheoQuyetDinhIndexViewModel { get; }

        public ForexKhoiTaoCapPhatViewModel(ForexDanhSachKhoiTaoIndexViewModel forexDanhSachKhoiTaoIndexViewModel, ForexKhoiTaoTheoQuyetDinhIndexViewModel forexKhoiTaoTheoQuyetDinhIndexViewModel)
        {
            ForexDanhSachKhoiTaoIndexViewModel = forexDanhSachKhoiTaoIndexViewModel;
            ForexKhoiTaoTheoQuyetDinhIndexViewModel = forexKhoiTaoTheoQuyetDinhIndexViewModel;
            ForexDanhSachKhoiTaoIndexViewModel.ParentPage = this;
            ForexKhoiTaoTheoQuyetDinhIndexViewModel.ParentPage = this;


        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness();
            Documentation = new ObservableCollection<ViewModelBase>()
            {
                ForexDanhSachKhoiTaoIndexViewModel,
                ForexKhoiTaoTheoQuyetDinhIndexViewModel
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
