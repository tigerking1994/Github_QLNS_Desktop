using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.GiaoDuToanChiPhi
{
    public class GiaoDuToanChiPhiDetailViewModel : DetailViewModelBase<VdtKhvPhanBoVonChiPhiModel, VdtKhvPhanBoVonChiPhiChiTietModel>
    {
        private readonly IVdtKhvKeHoachVonUngDxService _vonUngService;
        private readonly IDmDTChiService _dmDtChiService;
        private readonly IVdtKhvPhanBoVonChiPhiChiTietService _khvChiPhiChiTietService;
        private readonly IVdtKhvPhanBoVonChiPhiService _khvChiPhiService;
        private readonly ISessionService _sessionService;
        private IMapper _mapper;

        public override string Title => "Giao dự toán chi phí";
        public override string Name => "Giao dự toán chi phí chi tiết";

        private bool _bIsDetail;
        public bool BIsDetail
        {
            get => _bIsDetail;
            set => SetProperty(ref _bIsDetail, value);
        }

        private double _fTongMucDauTu;
        public double fTongMucDauTu
        {
            get => _fTongMucDauTu;
            set => SetProperty(ref _fTongMucDauTu, value);
        }

        private double _fTongGiaTri;
        public double fTongGiaTri
        {
            get => _fTongGiaTri;
            set
            {
                SetProperty(ref _fTongGiaTri, value);
            }
        }

        private double _fTongSoTien;
        public double FTongSoTien
        {
            get => _fTongSoTien;
            set
            {
                SetProperty(ref _fTongSoTien, value);
            }
        }

        private bool _bIsTongHop;
        public bool BIsTongHop
        {
            get => _bIsTongHop;
            set => SetProperty(ref _bIsTongHop, value);
        }

        public RelayCommand SaveDataCommand { get; }

        public GiaoDuToanChiPhiDetailViewModel(
            IVdtKhvKeHoachVonUngDxService vonUngService,
            IVdtKhvPhanBoVonChiPhiChiTietService khvChiPhiChiTietService,
            IVdtKhvPhanBoVonChiPhiService khvChiPhiService,
            IDmDTChiService dmDtChiService,
            ISessionService sessionService,
            IMapper mapper)
        {
            _vonUngService = vonUngService;
            _dmDtChiService = dmDtChiService;
            _khvChiPhiChiTietService = khvChiPhiChiTietService;
            _khvChiPhiService = khvChiPhiService;
            _sessionService = sessionService;
            _mapper = mapper;
            SaveDataCommand = new RelayCommand(obj => OnSaveData());
            AddCommand = new RelayCommand(obj => OnAdd(obj), obj => (bool)obj || SelectedItem != null);
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(10);
            LoadData();
        }

        #region RelayCommand
        public override void LoadData(params object[] args)
        {
            Items = new ObservableCollection<VdtKhvPhanBoVonChiPhiChiTietModel>();
            var lstChiPhi = _khvChiPhiChiTietService.FindByIdChiPhi(Model.Id).OrderBy(n => n.SMaOrder);
            if (!lstChiPhi.IsEmpty())
            {
                Items = _mapper.Map<ObservableCollection<VdtKhvPhanBoVonChiPhiChiTietModel>>(lstChiPhi);
            }
            else
            {
                var data = _dmDtChiService.GetAllDuToanChi().ToList();
                var currentRow = -1;
                foreach (var item in data)
                {
                    var chiPhiChiTiet = new VdtKhvPhanBoVonChiPhiChiTietModel();
                    chiPhiChiTiet.Id = Guid.NewGuid();
                    chiPhiChiTiet.IsAdded = true;
                    chiPhiChiTiet.IIdPhanBoVonChiPhiId = Model.Id;
                    chiPhiChiTiet.IIdDanhMucDtChi = item.IIdDuToanChi;
                    chiPhiChiTiet.SNoiDung = item.STenDuToanChi;
                    Items.Insert(currentRow + 1, chiPhiChiTiet);
                    OrderItems(chiPhiChiTiet.IIdParent);
                    
                }
            }
            foreach(var item in Items)
            {
                item.PropertyChanged += DetailModel_PropertyChanged;
            }
            UpdateTreeItems();
            OnPropertyChanged(nameof(Items));
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        protected override void OnAdd(object obj)
        {
            VdtKhvPhanBoVonChiPhiChiTietModel sourceItem = SelectedItem;
            VdtKhvPhanBoVonChiPhiChiTietModel targetItem = new VdtKhvPhanBoVonChiPhiChiTietModel();
            bool isParent = (bool)obj;
            int currentRow = -1;
            if (!Items.IsEmpty())
            {
                if (sourceItem != null)
                {
                    currentRow = Items.IndexOf(sourceItem) + CountTreeChildItems(sourceItem);
                }
                else
                {
                    // Thêm vào cuối danh sách
                    currentRow = Items.Count() - 1;
                }
            }

            if (sourceItem != null)
            {
                targetItem.IIdParent = isParent ? sourceItem.IIdParent : sourceItem.Id;
            }
            targetItem.Id = Guid.NewGuid();
            targetItem.IIdPhanBoVonChiPhiId = Model.Id;
            targetItem.IsAdded = true;
            targetItem.IsModified = true;
            targetItem.PropertyChanged += DetailModel_PropertyChanged;
            Items.Insert(currentRow + 1, targetItem);
            OrderItems(targetItem.IIdParent);
            UpdateTreeItems();
            OnPropertyChanged(nameof(Items));
        }

        protected override void OnDelete()
        {
            if (SelectedItem != null)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
            }
            OnPropertyChanged(nameof(fTongMucDauTu));
            OnPropertyChanged(nameof(fTongGiaTri));
            OnPropertyChanged(nameof(Items));
        }

        public void OnSaveData()
        {
            foreach(var item in Items)
            {
                if (!item.IsDeleted && item.IsAdded)
                    _khvChiPhiChiTietService.Add(_mapper.Map<VdtKhvPhanBoVonChiPhiChiTiet>(item));
                else if (!item.IsDeleted) {
                    _khvChiPhiChiTietService.Update(_mapper.Map<VdtKhvPhanBoVonChiPhiChiTiet>(item));
                } else if (item.IsDeleted) {
                    _khvChiPhiChiTietService.Delete(_mapper.Map<VdtKhvPhanBoVonChiPhiChiTiet>(item));
                }
                Model.FGiaTriDuocDuyet = Items.Where(n => !n.IsDeleted).Sum(n => n.FGiaTriPheDuyet);
                _khvChiPhiService.Update(_mapper.Map<VdtKhvPhanBoVonChiPhi>(Model));
            }
            MessageBox.Show(Resources.MsgSaveDone);
            LoadData();
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case nameof(VdtKhvPhanBoVonChiPhiChiTietModel.FGiaTriPheDuyet):
                case nameof(VdtKhvPhanBoVonChiPhiChiTietModel.IsDeleted):
                    {
                        CalculateData();
                        CaculateTongSoTien();
                        break;
                    } 
            }
        }

        private void CaculateTongSoTien()
        {
            FTongSoTien = Items.Where(n => !n.IsDeleted).Sum(n => n.FGiaTriPheDuyet ?? 0);
            OnPropertyChanged(nameof(FTongSoTien));
        }

        private int CountTreeChildItems(VdtKhvPhanBoVonChiPhiChiTietModel currentItem)
        {
            var items = Items;
            int count = 0;
            var childs = items.Where(x => x.IIdParent == currentItem.Id);
            if (!childs.IsEmpty())
            {
                count += childs.Count();
                foreach (var item in childs)
                {
                    count += CountTreeChildItems(item);
                }
            }
            return count;
        }

        private void OrderItems(Guid? parentId = null)
        {
            var childs = Items.Where(x => x.IIdParent == parentId);
            if (!childs.IsEmpty())
            {
                var parent = Items.FirstOrDefault(x => x.Id == parentId);
                int index = 1;
                foreach (var child in childs)
                {
                    if (parent != null)
                    {
                        child.SMaOrder = string.Format("{0}-{1}", parent.SMaOrder, index.ToString("D2"));
                    }
                    else
                    {
                        child.SMaOrder = index.ToString("D2");
                    }
                    child.SMaChiPhi = StringUtils.ConvertMaOrder(child.SMaOrder);
                    OrderItems(child.Id);
                    index++;
                }
            }
        }

        private void UpdateTreeItems()
        {
            if (!Items.IsEmpty())
            {
                Items.ForAll(s => s.IsHangCha = Items.Any(y => y.IIdParent == s.Id));
                Items.ForAll(x =>
                {
                    // Là hàng cha nếu thỏa mãn một trong các điều kiện sau
                    // 1. Có parent id là null hoặc ko nhận phần tử nào là cha
                    // 2. Có phần tử cùng cấp là hàng cha
                    if (x.IIdParent.IsNullOrEmpty() || !Items.Any(y => y.Id == x.IIdParent)) x.IsHangCha = true;
                    if (Items.Any(y => y.IIdParent == x.IIdParent && y.IsHangCha)) x.IsHangCha = true;
                });
            }
        }

        private void CalculateData()
        {
            if (Items == null) return;
            foreach (var item in Items.Where(n => !n.IIdParent.HasValue))
            {
                CalculateParent(item);
            }
            OnPropertyChanged(nameof(Items));
        }

        private void CalculateParent(VdtKhvPhanBoVonChiPhiChiTietModel parentItem)
        {
            int childRow = 0;
            foreach (var item in Items.Where(n => n.IIdParent.HasValue && n.IIdParent == parentItem.Id))
            {
                CalculateParent(item);
                childRow++;
            }
            if (childRow != 0)
                parentItem.FGiaTriPheDuyet = Items.Where(n => n.IIdParent.HasValue && n.IIdParent == parentItem.Id && !n.IsDeleted).Sum(n => n.FGiaTriPheDuyet);
        }
        #endregion
    }
}
