using System;
using AutoMapper;
using VTS.QLNS.CTC.App.Model;
using MaterialDesignThemes.Wpf;
using VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.PhanChiNgoaiThuong.MSPCNTDecision;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.Utility;
using System.Linq;
using System.Collections.Generic;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.PhanChiNgoaiThuong.MSPCNTDecision
{
    public class MSPCNTDecisionDialogDetailItemsViewModel : DetailViewModelBase<NhDaGoiThauChiPhiModel, NhDaGoiThauHangMucModel>
    {
        private readonly IMapper _mapper;
        private ObservableCollection<NhDaGoiThauHangMucModel> _originItems;

        public override string Name => "Chi tiết hạng mục";
        public override string Title => "Chi tiết hạng mục";
        public override string Description => "Chi tiết hạng mục";
        public override Type ContentType => typeof(MSPCNTDecisionDialogDetailItems);
        private ObservableCollection<NhDaGoiThauHangMucModel> _itemsHangMuc;
        public bool HasChanged => !ObjectCopier.ToJsonString(Items).Equals(ObjectCopier.ToJsonString(_originItems));

        public ObservableCollection<NhDaGoiThauHangMucModel> ItemsHangMuc
        {
            get => _itemsHangMuc;
            set => SetProperty(ref _itemsHangMuc, value);
        }
        public MSPCNTDecisionDialogDetailItemsViewModel(IMapper mapper)
        {
            _mapper = mapper;
        }

        public override void Init()
        {
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            Items = Model.GoiThauHangMucs;
            var data = _mapper.Map<IEnumerable<NhDaGoiThauHangMucModel>>(Model.GoiThauHangMucs).OrderBy(s => s.SMaOrder).ToList();
            data.ForEach(x =>
            {
                x.PropertyChanged += HangMuc_PropertyChanged;
            });
            CalculateHangMuc();

            Items = _mapper.Map<ObservableCollection<NhDaGoiThauHangMucModel>>(data);
            UpdateTreeItems();
            _originItems = ObjectCopier.Clone(Items);
        }
        private void HangMuc_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NhDaGoiThauHangMucModel objectSender = (NhDaGoiThauHangMucModel)sender;
            if (e.PropertyName.Equals(nameof(NhDaGoiThauHangMucModel.FGiaTriUsd)) ||
                e.PropertyName.Equals(nameof(NhDaGoiThauHangMucModel.FGiaTriEur)) ||
                e.PropertyName.Equals(nameof(NhDaGoiThauHangMucModel.FGiaTriVnd)) ||
                e.PropertyName.Equals(nameof(NhDaGoiThauHangMucModel.FGiaTriNgoaiTeKhac)))
            {
                CalculateHangMuc();
            }
            if (!e.PropertyName.Equals(nameof(NhDaGoiThauHangMucModel.IsHangCha)) &&
                !e.PropertyName.Equals(nameof(NhDaGoiThauHangMucModel.CanEditValue)))
            {
                objectSender.IsModified = true;
            }
            OnPropertyChanged(nameof(HasChanged));
        }
        private void CalculateHangMuc()
        {
            var parents = Items.Where(x => x.IIdParentId.IsNullOrEmpty() || !Items.Any(y => y.Id == x.IIdParentId));
            foreach (var item in parents)
            {
                CalculateHangMuc(item);
            }
        }

        private void CalculateHangMuc(NhDaGoiThauHangMucModel parentItem)
        {
            var childs = Items.Where(x => x.IIdParentId == parentItem.Id && !x.IsDeleted);
            if (!childs.IsEmpty())
            {
                foreach (var item in childs)
                {
                    CalculateHangMuc(item);
                }
                parentItem.FGiaTriUsd = childs.Sum(x => x.FGiaTriUsd);
                parentItem.FGiaTriEur = childs.Sum(x => x.FGiaTriEur);
                parentItem.FGiaTriVnd = childs.Sum(x => x.FGiaTriVnd);
                parentItem.FGiaTriNgoaiTeKhac = childs.Sum(x => x.FGiaTriNgoaiTeKhac);
            }
        }
        private void UpdateTreeItems()
        {
            if (!Items.IsEmpty())
            {
                Items.ForAll(s => s.CanEditValue = !Items.Any(y => y.IIdParentId == s.Id));
                Items.ForAll(x =>
                {
                    // Là hàng cha nếu thỏa mãn một trong các điều kiện sau
                    // 1. Có parent id là null hoặc ko nhận phần tử nào là cha
                    // 2. Có phần tử con. CanEditValue = false
                    // 3. Có phần tử cùng cấp là hàng cha
                    if (x.IIdParentId.IsNullOrEmpty() || !Items.Any(y => y.Id == x.IIdParentId)) x.IsHangCha = true;
                    if (!x.CanEditValue) x.IsHangCha = true;
                    else if (Items.Any(y => y.IIdParentId == x.IIdParentId && !y.CanEditValue)) x.IsHangCha = true;
                });
            }
        }
        public override void OnClose(object obj)
        {
            DialogHost.Close("DecisionDialogDetailItems");
        }
    }
}
