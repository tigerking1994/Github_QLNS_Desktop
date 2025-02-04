using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using AutoMapper;
using MaterialDesignThemes.Wpf;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Distribution;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Distribution
{
    public class DistributionDialogViewModel : ViewModelBase
    {
        private ISktChungTuService _sktChungTuService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;

        public override Type ContentType => typeof(DistributionDialog);
        public List<NsSktChungTuModel> SktChungTuModels { get; set; }

        private ICollectionView _nsDonViModelsView;
        
        private Guid _id;
        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
        
        private string _searchNsDonVi;
        public string SearchNsDonVi
        {
            get => _searchNsDonVi;
            set
            {
                if (SetProperty(ref _searchNsDonVi, value))
                {
                    _nsDonViModelsView.Refresh();
                }
            }
        }

        public string SelectedCountNsDonVi
        {
            get
            {
                var totalCount = NsDonViModelItems != null ? NsDonViModelItems.Count() : 0;
                var totalSelected = NsDonViModelItems != null ? NsDonViModelItems.Count(item => item.Selected) : 0;
                return string.Format("ĐƠN VỊ ({0}/{1})", totalSelected, totalCount);
            }
        }
        
        private NsSktChungTuModel _nsSktChungTuModel;
        public NsSktChungTuModel NsSktChungTuModel
        {
            get => _nsSktChungTuModel;
            set
            {
                SetProperty(ref _nsSktChungTuModel, value);
                OnPropertyChanged();
            }
        }
        
        private ObservableCollection<DonViModel> _nsDonViModelItems;
        public ObservableCollection<DonViModel> NsDonViModelItems
        {
            get => _nsDonViModelItems;
            set
            {
                SetProperty(ref _nsDonViModelItems, value);
                OnPropertyChanged();
            }
        }
        
        public NsSktChungTu NsSktChungTu { get; set; }

        public RelayCommand BtnCloseCommand { get; }
        public RelayCommand SaveCommand { get; }
        
        public DistributionDialogViewModel(INsDonViService nsDonViService, 
            ISktChungTuService sktChungTuService,
            IMapper mapper, 
            ISessionService sessionService)
        {
            _sessionService = sessionService;
            _sktChungTuService = sktChungTuService;
            _nsDonViService = nsDonViService;
            _mapper = mapper;
            BtnCloseCommand = new RelayCommand(obj =>
            {
                foreach (Window item in Application.Current.Windows)
                    if (item.DataContext == this)
                        item.Close();
            });
            SaveCommand = new RelayCommand(OnSave);
        }

        private void OnSave(object obj)
        {
            var donViSelected = NsDonViModelItems.FirstOrDefault(n => n.Selected);
            NsSktChungTuModel.IIdMaDonVi = donViSelected?.IIDMaDonVi;
            NsSktChungTuModel.STenDonVi = donViSelected?.TenDonVi;
            if (Id == Guid.Empty)
            {
                var temp = new NsSktChungTu();
                NsSktChungTuModel.DNgayTao = DateTime.Now;
                temp = _mapper.Map<NsSktChungTu>(NsSktChungTuModel);
                temp.ILoai = DemandCheckType.DEMAND;
                _sktChungTuService.Add(temp);
            }
            else
            {
                _mapper.Map(NsSktChungTuModel, NsSktChungTu);
                _sktChungTuService.Update(NsSktChungTu);
            }
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private DonVi GetNsDonViOfCurrentUser()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var currentIdDonVi = _sessionInfo.IdDonVi;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.IIDMaDonVi == currentIdDonVi);
            var nsDonViOfCurrentUser = _nsDonViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }
        private void LoadNsDonVis()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var nsDonViOfCurrentUser = GetNsDonViOfCurrentUser();
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.IdParent == nsDonViOfCurrentUser.Id);
            var idDonVis = SktChungTuModels.Select(x => x.IIdMaDonVi).ToList();
            if (Id == Guid.Empty)
                predicate = predicate.And(x => idDonVis.All(y => y!= x.IIDMaDonVi));
            var listUnit = _nsDonViService.FindByCondition(predicate).ToList();
            NsDonViModelItems = _mapper.Map<ObservableCollection<DonViModel>>(listUnit);
            if (!string.IsNullOrEmpty(NsSktChungTuModel.IIdMaDonVi))
            {
                NsDonViModelItems.Where(x => x.IIDMaDonVi == NsSktChungTuModel.IIdMaDonVi).Select(x =>
                {
                    x.Selected = true;
                    return x;
                }).ToList();
            }
            _nsDonViModelsView = CollectionViewSource.GetDefaultView(NsDonViModelItems);
            _nsDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.Loai),
                ListSortDirection.Ascending)); 
            _nsDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.TenDonVi),
                ListSortDirection.Ascending)); 
            _nsDonViModelsView.Filter = NsDonViFilter;
            foreach (var model in NsDonViModelItems)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(DonViModel.Selected))
                    {
                        OnPropertyChanged(nameof(SelectedCountNsDonVi));
                    }
                };
            }
        }

        private bool NsDonViFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(SearchNsDonVi))
            {
                return true;
            }
            var item = (DonViModel) obj;
            var condition = item.TenDonVi.ToLower().Contains(SearchNsDonVi.Trim().ToLower()) ||
                            item.IIDMaDonVi.ToLower().Contains(SearchNsDonVi.Trim().ToLower());
            return condition;
        }

        private void SetDialogData()
        {
                NsSktChungTuModel = new NsSktChungTuModel();
            //trường hợp tạo mới
            if (Id == Guid.Empty)
            {
                NsSktChungTuModel.DNgayQuyetDinh = DateTime.Now;
                NsSktChungTuModel.DNgayChungTu = DateTime.Now;
                NsSktChungTuModel.DNgayTao = DateTime.Now;
                var soChungTuIndex = _sktChungTuService.GetSoChungTuIndexByCondition(DemandCheckType.DEMAND.ToString(),
                    _sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget);
                NsSktChungTuModel.SSoChungTu = "SNC-" + soChungTuIndex.ToString("D3");
                NsSktChungTuModel.SMoTa = "Chi tiết chứng từ";
                NsSktChungTuModel.SNguoiTao = _sessionInfo.Principal;
                NsSktChungTuModel.INamLamViec = _sessionInfo.YearOfWork;
                NsSktChungTuModel.INamNganSach = _sessionInfo.YearOfBudget;
                NsSktChungTuModel.IIdMaNguonNganSach = _sessionInfo.Budget;
            }
            else
            {
                NsSktChungTu = _sktChungTuService.FindById(Id);
                _mapper.Map(NsSktChungTu,NsSktChungTuModel);
                NsSktChungTuModel.DNgaySua = DateTime.Now;
                NsSktChungTuModel.SNguoiTao = _sessionInfo.Principal;
                NsSktChungTuModel.INamLamViec = _sessionInfo.YearOfWork;
                NsSktChungTuModel.INamNganSach = _sessionInfo.YearOfBudget;
                NsSktChungTuModel.IIdMaNguonNganSach = _sessionInfo.Budget;
            }
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            SetDialogData();
            LoadNsDonVis();
        }
    }
}