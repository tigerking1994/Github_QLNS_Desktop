using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using System.ComponentModel;

using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.DeNghiThanhToanChiPhi
{
    public class DeNghiThanhToanChiPhiDetailViewModel : DetailViewModelBase<VdtTtDeNghiThanhToanChiPhiIndexModel, VdtTtDeNghiThanhToanChiPhiChiTietModel>
    {
        #region Private
        private readonly IVdtTtDeNghiThanhToanChiPhiService _service;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        #endregion

        private bool _bIsViewDetail;
        public bool BIsViewDetail
        {
            get => _bIsViewDetail;
            set => SetProperty(ref _bIsViewDetail, value);
        }

        public override string Name => "Đề nghị thanh toán theo chi phí chi tiết";
        public override string Description => "Cấp phát cấp thanh toán theo chi phí chi tiết";

        private double? _fGiaTriDeNghiThanhToan;
        public double? FGiaTriDeNghiThanhToan
        {
            get => _fGiaTriDeNghiThanhToan;
            set => SetProperty(ref _fGiaTriDeNghiThanhToan, value);
        }


        private double? _fGiaTriDeNghiThuHoi;
        public double? FGiaTriDeNghiThuHoi
        {
            get => _fGiaTriDeNghiThuHoi;
            set => SetProperty(ref _fGiaTriDeNghiThuHoi, value);
        }

        private double? _fTongGiaTriDeNghi;
        public double? FTongGiaTriDeNghi
        {
            get => _fTongGiaTriDeNghi;
            set => SetProperty(ref _fTongGiaTriDeNghi, value);
        }

        private double? _fTongGiaTriPheDuyet;
        public double? FTongGiaTriPheDuyet
        {
            get => _fTongGiaTriPheDuyet;
            set => SetProperty(ref _fTongGiaTriPheDuyet, value);
        }


        public RelayCommand SaveDataCommand { get; }

        public DeNghiThanhToanChiPhiDetailViewModel(
            IVdtTtDeNghiThanhToanChiPhiService service,
            IMapper mapper,
            ILog logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
            SaveDataCommand = new RelayCommand(obj => OnSaveData());
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(10);
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                //var datas = _service.GetDeNghiThanhToanChiPhiDetailById(Model.Id);
                //if (datas == null || datas.Count() == 0)
                //{
                //    Items = _mapper.Map<ObservableCollection<VdtTtDeNghiThanhToanChiPhiChiTietModel>>(_service.GetDeNghiThanhToanChiPhiDetail(Model.IIdPhanBoVonChiPhiId.Value));
                //    UpdateTreeItems();
                //}
                //else
                //{
                //    Items = _mapper.Map<ObservableCollection<VdtTtDeNghiThanhToanChiPhiChiTietModel>>(datas);
                //    UpdateTreeItemsThanhToan();
                //}
                Items = _mapper.Map<ObservableCollection<VdtTtDeNghiThanhToanChiPhiChiTietModel>>(_service.GetDeNghiThanhToanChiPhiDetailById(Model.Id, Model.IIdPhanBoVonChiPhiId.Value));
                OnPropertyChanged(nameof(Items));
                UpdateTreeItems();
                UpdateListHangMucCanEdit();
                CalculateData();
            }
            catch (Exception ex)
            {

            }
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            VdtTtDeNghiThanhToanChiPhiChiTietModel objectSender = (VdtTtDeNghiThanhToanChiPhiChiTietModel)sender;
            if (SelectedItem == null)
            {
                return;
            }
            if (args.PropertyName == nameof(VdtTtDeNghiThanhToanChiPhiChiTietModel.FGiaTriDeNghi))
            {
                UpdateListHangMucCanEdit();
                CalculateData();
            }

            objectSender.IsModified = true;
            OnPropertyChanged(nameof(Items));
        }

        #region RelayCommand
        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        protected override void OnDelete()
        {
            if (SelectedItem != null)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
            }
            OnPropertyChanged(nameof(Items));
        }

        public void OnSaveData()
        {
            if (!Validate()) return;
            var lstData = ConvertDetail();
            _service.AddDetail(Model.Id, lstData);
            MessageBoxHelper.Info(Resources.MsgSaveDone);
            LoadData();
        }
        #endregion

        #region Helper
        private bool Validate()
        {
            if (Items == null || !Items.Any(n => !n.IsDeleted && n.FGiaTriDeNghi != 0))
            {
                MessageBoxHelper.Error(Resources.MsgErrorRequire, "Thông tin chi tiết");
                return false;
            }
            return true;
        }

        private List<VdtTtDeNghiThanhToanChiPhiChiTiet> ConvertDetail()
        {
            List<VdtTtDeNghiThanhToanChiPhiChiTiet> result = new List<VdtTtDeNghiThanhToanChiPhiChiTiet>();
            foreach (var item in Items.Where(n => !n.IsDeleted && n.FGiaTriDeNghi != 0))
            {
                result.Add(new VdtTtDeNghiThanhToanChiPhiChiTiet()
                {
                    Id = Guid.NewGuid(),
                    FGiaTriDeNghi = item.FGiaTriDeNghi,
                    IIdDeNghiThanhToanChiPhiId = Model.Id,
                    IIdNoiDungChi = item.Id,
                    SGhiChu = item.SGhiChu
                });
            }
            return result;
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
                    if (Items.Any(y => (y.IIdParent == x.IIdParent) && y.IsHangCha)) x.IsHangCha = true;
                });
            }
        }

        private void UpdateListHangMucCanEdit()
        {
            foreach (var item in Items)
            {
                item.IsEditHangMuc = true;
                item.PropertyChanged += DetailModel_PropertyChanged;
            }
            //update hàng cha có con thì ko đc edit
            foreach (var item in Items.Where(x => x.IsHangCha && !x.IsDeleted))
            {
                List<VdtTtDeNghiThanhToanChiPhiChiTietModel> listChild = new List<VdtTtDeNghiThanhToanChiPhiChiTietModel>();
                listChild = FindListChildDelete(item.Id);
                if (listChild == null || listChild.Count == 0)
                {
                    item.IsEditHangMuc = true;
                }
                else
                {
                    item.IsEditHangMuc = false;
                }
            }
            OnPropertyChanged(nameof(Items));
        }

        public List<VdtTtDeNghiThanhToanChiPhiChiTietModel> FindListChildDelete(Guid? parentId)
        {
            if (parentId == null) return null;
            List<VdtTtDeNghiThanhToanChiPhiChiTietModel> inner = new List<VdtTtDeNghiThanhToanChiPhiChiTietModel>();
            foreach (var t in Items.Where(item => item.IIdParent == parentId))
            {
                inner.Add(t);
                inner = inner.Union(FindListChildDelete(t.Id)).ToList();
            }

            return inner;
        }

        private void CalculateData()
        {
            FTongGiaTriDeNghi = Items.Where(n => n.IsHangCha).Sum(n => n.FGiaTriDeNghi);
            FTongGiaTriPheDuyet = Items.Where(n => n.IsHangCha).Sum(n => n.FGiaTriPheDuyet);
            if (Items == null) return;
            foreach (var item in Items.Where(n => !n.IIdParent.HasValue))
            {
                CalculateParent(item);
            }
            OnPropertyChanged(nameof(Items));
        }

        private void CalculateParent(VdtTtDeNghiThanhToanChiPhiChiTietModel parentItem)
        {
            int childRow = 0;
            foreach (var item in Items.Where(n => n.IIdParent.HasValue && n.IIdParent == parentItem.Id))
            {
                CalculateParent(item);
                childRow++;
            }
            if (childRow != 0)
                parentItem.FGiaTriDeNghi = Items.Where(n => n.IIdParent.HasValue && n.IIdParent == parentItem.Id && !n.IsDeleted).Sum(n => n.FGiaTriDeNghi);
        }
        #endregion
    }
}
