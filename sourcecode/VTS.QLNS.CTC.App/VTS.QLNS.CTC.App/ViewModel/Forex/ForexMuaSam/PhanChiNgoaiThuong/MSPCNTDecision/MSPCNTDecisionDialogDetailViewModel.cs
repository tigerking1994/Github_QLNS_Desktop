using log4net;
using System;
using AutoMapper;
using System.Linq;
using System.Windows;
using VTS.QLNS.CTC.Utility;
using System.ComponentModel;
using VTS.QLNS.CTC.App.Model;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.PhanChiNgoaiThuong.MSPCNTDecision;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.PhanChiNgoaiThuong.MSPCNTDecision
{
    public class MSPCNTDecisionDialogDetailViewModel : DialogAttachmentViewModelBase<NhDaGoiThauModel>
    {
        private IMapper _mapper;
        private readonly INhDaGoiThauNguonVonService _nhDaGoiThauNguonVonService;
        private readonly INhDaGoiThauChiPhiService _nhDaGoiThauChiPhiService;
        private readonly INhDaGoiThauHangMucSerrvice _nhDaGoiThauHangMucSerrvice;
        private readonly INsNguonNganSachService _nsNguonNganSachService;

        public override string Name => "THÔNG TIN GÓI THẦU CHI TIẾT";
        public override string Title => "Quản lý thông tin gói thầu chi tiết";
        public override string Description => string.Format("Số quyết định chi tiết: {0} - Tên gói: : {1}", SoQuetDinhChiTiet, Model.STenGoiThau);
        public override Type ContentType => typeof(MSPCNTDecisionDialogDetail);

        private ObservableCollection<NguonNganSachModel> _itemsNguonVon;
        public ObservableCollection<NguonNganSachModel> ItemsNguonVon
        {
            get => _itemsNguonVon;
            set => SetProperty(ref _itemsNguonVon, value);
        }

        private ObservableCollection<NhDaGoiThauNguonVonModel> _itemsGoiThauNguonVon;
        public ObservableCollection<NhDaGoiThauNguonVonModel> ItemsGoiThauNguonVon
        {
            get => _itemsGoiThauNguonVon;
            set => SetProperty(ref _itemsGoiThauNguonVon, value);
        }

        private NhDaGoiThauNguonVonModel _selectedGoiThauNguonVon;
        public NhDaGoiThauNguonVonModel SelectedGoiThauNguonVon
        {
            get => _selectedGoiThauNguonVon;
            set => SetProperty(ref _selectedGoiThauNguonVon, value);
        }

        private NhDaGoiThauNguonVonModel _tongTienGoiThauNguonVon;
        public NhDaGoiThauNguonVonModel TongTienGoiThauNguonVon
        {
            get => _tongTienGoiThauNguonVon;
            set => SetProperty(ref _tongTienGoiThauNguonVon, value);
        }

        private ObservableCollection<NhDaGoiThauChiPhiModel> _itemsChiPhi;
        public ObservableCollection<NhDaGoiThauChiPhiModel> ItemsChiPhi
        {
            get => _itemsChiPhi;
            set => SetProperty(ref _itemsChiPhi, value);
        }

        private NhDaGoiThauChiPhiModel _selectedChiPhi;
        public NhDaGoiThauChiPhiModel SelectedChiPhi
        {
            get => _selectedChiPhi;
            set => SetProperty(ref _selectedChiPhi, value);
        }

        private NhDaGoiThauChiPhiModel _tongTienChiPhi;
        public NhDaGoiThauChiPhiModel TongTienChiPhi
        {
            get => _tongTienChiPhi;
            set => SetProperty(ref _tongTienChiPhi, value);
        }

        private string _soQuetDinhChiTiet;
        public string SoQuetDinhChiTiet
        {

            get => _soQuetDinhChiTiet;
            set => SetProperty(ref _soQuetDinhChiTiet, value);
        }

        public RelayCommand ShowHangMucDetailCommand { get; set; }
        public MSPCNTDecisionDialogDetailItemsViewModel MSPCNTDecisionDialogDetailItemsViewModel { get; }

        public MSPCNTDecisionDialogDetailViewModel
        (
            IMapper mapper,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            INhDaGoiThauNguonVonService nhDaGoiThauNguonVonService,
            INhDaGoiThauChiPhiService nhDaGoiThauChiPhiService,
            INhDaGoiThauHangMucSerrvice nhDaGoiThauHangMucSerrvice,
            INsNguonNganSachService nsNguonNganSachService,
            MSPCNTDecisionDialogDetailItemsViewModel mspcntdecisionDialogDetailItemsViewModel
        ) : base(mapper, storageServiceFactory, attachService)
        {
            _mapper = mapper;
            _nhDaGoiThauNguonVonService = nhDaGoiThauNguonVonService;
            _nhDaGoiThauChiPhiService = nhDaGoiThauChiPhiService;
            _nhDaGoiThauHangMucSerrvice = nhDaGoiThauHangMucSerrvice;
            _nsNguonNganSachService = nsNguonNganSachService;

            MSPCNTDecisionDialogDetailItemsViewModel = mspcntdecisionDialogDetailItemsViewModel;
            ShowHangMucDetailCommand = new RelayCommand(obj => OnShowHangMucDetail());
        }

        public override void Init()
        {
            LoadDefault();
            LoadNguonVon();
            LoadGoiThauNguonVon();
        }

        private void LoadDefault()
        {
            _tongTienGoiThauNguonVon = new NhDaGoiThauNguonVonModel();
            _tongTienChiPhi = new NhDaGoiThauChiPhiModel();
            _itemsChiPhi = new ObservableCollection<NhDaGoiThauChiPhiModel>();
        }

        private void LoadNguonVon()
        {
            var data = _nsNguonNganSachService.FindAll();
            _itemsNguonVon = _mapper.Map<ObservableCollection<NguonNganSachModel>>(data);
            OnPropertyChanged(nameof(ItemsNguonVon));
        }

        private void LoadGoiThauNguonVon()
        {
            _itemsGoiThauNguonVon = new ObservableCollection<NhDaGoiThauNguonVonModel>();
            if (!Model.Id.IsNullOrEmpty())
            {
                IEnumerable<NhDaGoiThauNguonVon> data = _nhDaGoiThauNguonVonService.GetListNguonVonByIdGoiThau(Model.Id);
                if (!data.IsEmpty())
                {
                    _itemsGoiThauNguonVon = _mapper.Map<ObservableCollection<NhDaGoiThauNguonVonModel>>(data);
                    _itemsGoiThauNguonVon.ForAll(item => item.PropertyChanged += GoiThauNguonVon_PropertyChanged);

                    // Tính tổng tiền nguồn vốn
                    _tongTienGoiThauNguonVon.FGiaTriUsd = data.Sum(s => s.FTienGoiThauUsd);
                    _tongTienGoiThauNguonVon.FGiaTriVnd = data.Sum(s => s.FTienGoiThauVnd);
                    _tongTienGoiThauNguonVon.FGiaTriEur = data.Sum(s => s.FTienGoiThauEur);
                    _tongTienGoiThauNguonVon.FGiaTriNgoaiTeKhac = data.Sum(s => s.FTienGoiThauNgoaiTeKhac);
                }
            }
            OnPropertyChanged(nameof(ItemsGoiThauNguonVon));
            OnPropertyChanged(nameof(TongTienGoiThauNguonVon));
        }

        private void GoiThauNguonVon_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NhDaGoiThauNguonVonModel obj = (NhDaGoiThauNguonVonModel)sender;
            if (e.PropertyName == nameof(NhDaGoiThauNguonVonModel.IsChecked))
            {
                if (obj.IsChecked)
                {
                    LoadGoiThauChiPhi(obj.Id);
                }
                else
                {
                    // Xoá những chi phí có nguồn vốn có IsChecked = false
                    List<NhDaGoiThauChiPhiModel> lstRemove = _itemsChiPhi.Where(s => s.IIdGoiThauNguonVonId == obj?.Id).ToList();
                    lstRemove.ForEach(s => _itemsChiPhi.Remove(s));
                    CaculateChiPhi();
                }
            }
        }

        private void LoadGoiThauChiPhi(Guid nguonVonId)
        {
            IEnumerable<NhDaGoiThauChiPhi> data = _nhDaGoiThauChiPhiService.FindAll(s => s.IIdGoiThauNguonVonId == nguonVonId)?.OrderBy(s => s.SMaOrder);
            if (!data.IsEmpty())
            {
                var dataMap = _mapper.Map<ObservableCollection<NhDaGoiThauChiPhiModel>>(data);
                dataMap.ForAll(s =>
                {
                    s.SMaChiPhi = StringUtils.ConvertMaOrder(s.SMaOrder);
                    s.GoiThauHangMucs = _mapper.Map<ObservableCollection<NhDaGoiThauHangMucModel>>(LoadNhDaGoiThauHangMucs(s));
                    _itemsChiPhi.Add(s);
                });

                // Tính tổng tiền chi phí
                CaculateChiPhi();

                _itemsChiPhi = new ObservableCollection<NhDaGoiThauChiPhiModel>(_itemsChiPhi.OrderBy(s => s.SMaChiPhi));
                UpdateTreeItemsChiPhi();
                OnPropertyChanged(nameof(ItemsChiPhi));
            }      
        }

        private List<NhDaGoiThauHangMucModel> LoadNhDaGoiThauHangMucs(NhDaGoiThauChiPhiModel chiPhiModel)
        {
            var result = new List<NhDaGoiThauHangMucModel>();
            if (chiPhiModel != null)
            {
                IEnumerable<NhDaGoiThauHangMuc> data = _nhDaGoiThauHangMucSerrvice.FindAll(s => s.IIdGoiThauChiPhiId == chiPhiModel.Id).OrderBy(s => s.SMaOrder);
                result = _mapper.Map<List<NhDaGoiThauHangMucModel>>(data);
            }
            return result;
        }

        private void OnShowHangMucDetail()
        {
            if (SelectedChiPhi == null) return;
            MSPCNTDecisionDialogDetailItemsViewModel.Model = SelectedChiPhi;
            MSPCNTDecisionDialogDetailItemsViewModel.Init();
            MSPCNTDecisionDialogDetailItemsViewModel.ShowDialogHost("DecisionDialogDetailItems");
        }

        private void CaculateChiPhi()
        {
            if (_itemsChiPhi.IsEmpty())
            {
                _tongTienChiPhi = new NhDaGoiThauChiPhiModel();
            }
            else
            {
                var sumData = ItemsChiPhi.Where(n => !n.IsDeleted && n.IIdParentId == null );
                //_tongTienChiPhi.FGiaTriUsd = _itemsChiPhi.Sum(s => s.FTienGoiThauUsd);
                //_tongTienChiPhi.FGiaTriVnd = _itemsChiPhi.Sum(s => s.FTienGoiThauVnd);
                //_tongTienChiPhi.FGiaTriEur = _itemsChiPhi.Sum(s => s.FTienGoiThauEur);
                //_tongTienChiPhi.FGiaTriNgoaiTeKhac = _itemsChiPhi.Sum(s => s.FTienGoiThauNgoaiTeKhac);

                _tongTienChiPhi.FGiaTriUsd = sumData.Sum(s => s.FTienGoiThauUsd);
                _tongTienChiPhi.FGiaTriVnd = sumData.Sum(s => s.FTienGoiThauVnd);
                _tongTienChiPhi.FGiaTriEur = sumData.Sum(s => s.FTienGoiThauEur);
                _tongTienChiPhi.FGiaTriNgoaiTeKhac = sumData.Sum(s => s.FTienGoiThauNgoaiTeKhac);
            }
            OnPropertyChanged(nameof(TongTienChiPhi));

        }
        private void UpdateTreeItemsChiPhi()
        {
            if (!ItemsChiPhi.IsEmpty())
            {
                ItemsChiPhi.ForAll(s => s.CanEditValue = !ItemsChiPhi.Any(y => y.IIdParentId == s.Id));
                ItemsChiPhi.ForAll(x =>
                {
                    // Là hàng cha nếu thỏa mãn một trong các điều kiện sau
                    // 1. Có parent id là null hoặc ko nhận phần tử nào là cha
                    // 2. Có phần tử con. CanEditValue = false
                    // 3. Có phần tử cùng cấp là hàng cha
                    if (x.IIdParentId.IsNullOrEmpty() || !ItemsChiPhi.Any(y => y.Id == x.IIdParentId)) x.IsHangCha = true;
                    if (!x.CanEditValue) x.IsHangCha = true;
                    else if (ItemsChiPhi.Any(y => y.IIdParentId == x.IIdParentId && !y.CanEditValue)) x.IsHangCha = true;
                });
            }
        }
    }
}
