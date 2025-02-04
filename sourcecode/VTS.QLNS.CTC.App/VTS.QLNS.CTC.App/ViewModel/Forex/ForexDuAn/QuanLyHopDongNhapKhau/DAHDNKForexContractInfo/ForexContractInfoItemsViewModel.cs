using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.View.Forex.ForexDuAn.QuanLyHopDongNhapKhau.DAHDNKForexContractInfo;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.QuanLyHopDongNhapKhau.DAHDNKForexContractInfo
{
    public class ForexContractInfoItemsViewModel : DetailViewModelBase<NhDaHopDongChiPhiModel, NhDaHopDongHangMucModel>
    {
        private readonly IMapper _mapper;
        private ObservableCollection<NhDaHopDongHangMucModel> _originItems;
        public override string Title => "HỢP ĐỒNG";
        public override string Name => "Thông tin Hợp đồng phụ lục - hạng mục";
        public override string Description => "Chi tiết thông tin Hợp đồng phụ lục - hạng mục";
        public override Type ContentType => typeof(ForexContractInfoItems);
        public bool HasChanged => !Items.ToJsonString().Equals(_originItems.ToJsonString());

        public Action<object> CurrencyExchangeAction { get; set; }

        public RelayCommand AddGoiThauHangMucCommand { get; }
        public RelayCommand DeleteGoiThauHangMucCommand { get; }

        public ForexContractInfoItemsViewModel(IMapper mapper)
        {
            _mapper = mapper;

            SaveCommand = new RelayCommand(obj => OnSave(), obj => HasChanged);
        }

        public override void Init()
        {
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            Items = new ObservableCollection<NhDaHopDongHangMucModel>();
            var data = _mapper.Map<IEnumerable<NhDaHopDongHangMucModel>>(Model.HopDongHangMucs.OrderBy(s => s.SMaOrder)).ToList();
            data.ForEach(x =>
            {
                x.PropertyChanged += HangMuc_PropertyChanged;
            });
            Items = _mapper.Map<ObservableCollection<NhDaHopDongHangMucModel>>(data);
            UpdateTreeItems();
            _originItems = Items.Clone();
        }

        public void HangMuc_BeginningEditHanlder(DataGridBeginningEditEventArgs e)
        {
            SelectedItem = (NhDaHopDongHangMucModel)e.Row.Item;
            if (e.Column.SortMemberPath.Equals(nameof(NhDaHopDongHangMucModel.FGiaTriUsd)) ||
                e.Column.SortMemberPath.Equals(nameof(NhDaHopDongHangMucModel.FGiaTriEur)) ||
                e.Column.SortMemberPath.Equals(nameof(NhDaHopDongHangMucModel.FGiaTriVnd)) ||
                e.Column.SortMemberPath.Equals(nameof(NhDaHopDongHangMucModel.FGiaTriNgoaiTeKhac)))
            {
                e.Cancel = !SelectedItem.CanEditValue;
            }
        }

        private void HangMuc_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NhDaHopDongHangMucModel objectSender = (NhDaHopDongHangMucModel)sender;
            if (e.PropertyName.Equals(nameof(NhDaHopDongHangMucModel.FGiaTriUsd)) ||
                e.PropertyName.Equals(nameof(NhDaHopDongHangMucModel.FGiaTriEur)) ||
                e.PropertyName.Equals(nameof(NhDaHopDongHangMucModel.FGiaTriVnd)) ||
                e.PropertyName.Equals(nameof(NhDaHopDongHangMucModel.FGiaTriNgoaiTeKhac)))
            {
                CalculateHangMuc();
            }
            if (!e.PropertyName.Equals(nameof(NhDaHopDongHangMucModel.IsHangCha)) &&
                !e.PropertyName.Equals(nameof(NhDaHopDongHangMucModel.CanEditValue)))
            {
                objectSender.IsModified = true;
            }
            OnPropertyChanged(nameof(HasChanged));
        }

        private void UpdateTreeItems()
        {
            if (!Items.IsEmpty())
            {
                Items.ForAll(s => s.CanEditValue = !Items.Any(y => y.IIdParentId == s.IIdGoiThauHangMucId));
                Items.ForAll(x =>
                {
                    // Là hàng cha nếu thỏa mãn một trong các điều kiện sau
                    // 1. Có parent id là null hoặc ko nhận phần tử nào là cha
                    // 2. Có phần tử con. CanEditValue = false
                    // 3. Có phần tử cùng cấp là hàng cha
                    if (x.IIdParentId.IsNullOrEmpty() || !Items.Any(y => y.IIdGoiThauHangMucId == x.IIdParentId)) x.IsHangCha = true;
                    if (!x.CanEditValue) x.IsHangCha = true;
                    else if (Items.Any(y => y.IIdParentId == x.IIdParentId && !y.CanEditValue)) x.IsHangCha = true;
                });
            }
        }

        private void CalculateHangMuc()
        {
            var parents = Items.Where(x => x.IIdParentId.IsNullOrEmpty() || !Items.Any(y => y.IIdGoiThauHangMucId == x.IIdParentId));
            foreach (var item in parents)
            {
                CalculateHangMuc(item);
            }
        }

        private void CalculateHangMuc(NhDaHopDongHangMucModel parentItem)
        {
            var childs = Items.Where(x => x.IIdParentId == parentItem.IIdGoiThauHangMucId && !x.IsDeleted);
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

        public override void OnSave()
        {
            SaveItems();
            DialogHost.Close("ForexContractInfoItems");
        }

        public override void OnClose(object obj)
        {
            if (HasChanged)
            {
                var result = MessageBoxHelper.Confirm("Đồng chí có muốn lưu thông tin hạng mục đã được nhập không?");
                if (result == MessageBoxResult.Yes)
                {
                    SaveItems();
                }
            }
            DialogHost.Close("ForexContractInfoItems");
        }

        private void SaveItems()
        {
            var data = _mapper.Map<IEnumerable<NhDaHopDongHangMucModel>>(Items).ToList();
            data.ForEach(x =>
            {
                x.PropertyChanged -= HangMuc_PropertyChanged;
            });
            SavedAction?.Invoke(data);
        }

        public override void OnCellEditEnding(object obj)
        {
            base.OnCellEditEnding(obj);
            CurrencyExchangeAction?.Invoke(obj);
        }
    }
}
