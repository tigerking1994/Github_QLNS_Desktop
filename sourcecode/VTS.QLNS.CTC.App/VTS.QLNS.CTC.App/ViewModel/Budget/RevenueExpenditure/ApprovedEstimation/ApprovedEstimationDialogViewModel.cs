using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Converters;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.Plan;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.Utility.Enum.RevenueExpenditureType;
using VTS.QLNS.CTC.App.Helper;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.Plan
{
    public class ApprovedEstimationDialogViewModel : DialogViewModelBase<TnDtChungTuModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ITnDtChungTuService _tnDtChungTuService;
        private readonly ITnDtChungTuChiTietService _tnDtChungTuChiTietService;
        private readonly INsMucLucNganSachService _nsMucLucNganSachService;
        private readonly ILog _logger;
        private ICollectionView _dataLNSView;

        public override Type ContentType => typeof(ApprovedEstimationDialog);
        public override string Name => Model.Id == Guid.Empty ? "THÊM CHỨNG TỪ" : "CẬP NHẬT CHỨNG TỪ";
        public override string Description => Model.Id == Guid.Empty ? "Tạo mới chứng từ phân bổ" : "Cập nhật chứng từ phân bổ";

        private ObservableCollection<NsMuclucNgansachModel> _dataLNS;
        public ObservableCollection<NsMuclucNgansachModel> DataLNS
        {
            get => _dataLNS;
            set => SetProperty(ref _dataLNS, value);
        }

        public string SelectedCountLNS
        {
            get
            {
                int totalItems = DataLNS != null ? DataLNS.Count() : 0;
                int totalSelected = DataLNS != null ? DataLNS.Count(item => item.IsSelected) : 0;
                return string.Format("CHỌN LNS ({0}/{1})", totalSelected, totalItems);
            }
        }

        private bool _selectAllLNS;
        public bool SelectAllLNS
        {
            get => (DataLNS == null || !DataLNS.Any()) ? false : DataLNS.All(item => item.IsSelected);
            set
            {
                SetProperty(ref _selectAllLNS, value);
                if (DataLNS != null)
                {
                    DataLNS.Select(c => { c.IsSelected = _selectAllLNS; return c; }).ToList();
                }
            }
        }

        private string _searchLNS;
        public string SearchLNS
        {
            get => _searchLNS;
            set
            {
                SetProperty(ref _searchLNS, value);
                _dataLNSView.Refresh();
            }
        }

        private List<string> ListLNSHasDataUnchecked { get; set; }

        public ApprovedEstimationDialogViewModel(IMapper mapper,
            ISessionService sessionService,
            ITnDtChungTuService tnDtChungTuService,
            ITnDtChungTuChiTietService tnDtChungTuChiTietService,
            INsMucLucNganSachService nsMucLucNganSachService,
            ILog logger)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _tnDtChungTuService = tnDtChungTuService;
            _tnDtChungTuChiTietService = tnDtChungTuChiTietService;
            _nsMucLucNganSachService = nsMucLucNganSachService;
            _logger = logger;
        }

        public override void Init()
        {
            LoadDataLNS();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                base.LoadData(args);

                if (Model != null && Model.Id != Guid.Empty)
                {
                    BudgetCatalogSelectedToStringConvert.SetCheckboxSelected(_dataLNS, Model.Lns);
                }
                else
                {
                    _selectAllLNS = false;

                    var predicate = this.CreatePredicate();
                    int soChungTuIndex = _tnDtChungTuService.FindNextSoChungTuIndex(predicate);
                    Model = new TnDtChungTuModel()
                    {
                        SoChungTu = "DT-" + soChungTuIndex.ToString("D3"),
                        SoChungTuIndex = soChungTuIndex,
                        NgayChungTu = DateTime.Now,
                        NgayQuyetDinh = DateTime.Now,
                        SoQuyetDinh = string.Empty,
                        MoTaChiTiet = string.Empty
                    };

                    OnPropertyChanged(nameof(SelectAllLNS));
                    OnPropertyChanged(nameof(SelectedCountLNS));
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            } 
        }

        private Expression<Func<TnDtChungTu, bool>> CreatePredicate()
        {
            var predicate = PredicateBuilder.True<TnDtChungTu>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.NamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.ILoai == RevenueAndExpenditureType.ApprovedEstimation);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            return predicate;
        }

        private void LoadDataLNS()
        {
            try
            {
                int yearOfWork = _sessionService.Current.YearOfWork;
                string idDonVi = _sessionService.Current.IdDonVi;
                List<NsMucLucNganSach> listNsMucLucNganSach = new List<NsMucLucNganSach>();

                if (_sessionService.Current.Budget.Equals(NSQP))
                {
                    listNsMucLucNganSach = _nsMucLucNganSachService.FindByMLNS(yearOfWork, MLNS_QP).ToList();
                }
                else if (_sessionService.Current.Budget.Equals(NSNN))
                {
                    listNsMucLucNganSach = _nsMucLucNganSachService.FindByMLNS(yearOfWork, MLNS_NN).ToList();
                }

                DataLNS = new ObservableCollection<NsMuclucNgansachModel>();
                DataLNS = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listNsMucLucNganSach);
                if (!Guid.Empty.Equals(Model.Id))
                {
                    List<string> listLnsHasData = _tnDtChungTuService.GetLnsHasData(new List<Guid> { Model.Id }).ToList();
                    DataLNS.Where(x => listLnsHasData.Contains(x.Lns)).ToList().ForEach(x => x.IsHitTestVisible = false);
                }
                _dataLNSView = CollectionViewSource.GetDefaultView(DataLNS);
                _dataLNSView.Filter = ListLNSFilter;

                if (_dataLNS != null && _dataLNS.Count > 0)
                {
                    foreach (var model in _dataLNS)
                    {
                        model.PropertyChanged += (sender, args) =>
                        {
                            if (args.PropertyName == nameof(NsMuclucNgansachModel.IsSelected))
                            {
                                foreach (var item in _dataLNS)
                                {
                                    if (item.MlnsIdParent == model.MlnsId)
                                    {
                                        item.IsSelected = model.IsSelected;
                                    }
                                }
                                OnPropertyChanged(nameof(SelectAllLNS));
                                OnPropertyChanged(nameof(SelectedCountLNS));
                            }
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool ListLNSFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchLNS))
            {
                return true;
            }
            return obj is NsMuclucNgansachModel item && item.Lns.ToLower().Contains(_searchLNS!.ToLower());
        }

        public override void OnSave()
        {
            try
            {
                if (Model == null) Model = new TnDtChungTuModel();
                Model.Lns = BudgetCatalogSelectedToStringConvert.GetValueSelected(DataLNS);
                Model.NamLamViec = _sessionService.Current.YearOfWork;
                Model.NamNganSach = _sessionService.Current.YearOfBudget;
                Model.NguonNganSach = _sessionService.Current.Budget;
                Model.IdDonVi = _sessionService.Current.IdDonVi;
                Model.IdDonViTao = _sessionService.Current.IdDonVi;
                Model.ILoai = RevenueAndExpenditureType.ApprovedEstimation;

                string message = GetMessageValidate();
                if (!string.IsNullOrEmpty(message))
                {
                    System.Windows.MessageBox.Show(message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }


                bool bDeleteDetail = false;
                string messageCheckBox = GetMessageValidateCheckBox();
                if (!string.IsNullOrEmpty(messageCheckBox))
                {
                    MessageBoxResult messageValidate = MessageBoxHelper.Confirm(messageCheckBox);
                    if (messageValidate.Equals(MessageBoxResult.Yes))
                    {
                        bDeleteDetail = true;
                    }
                    else
                    {
                        return;
                    }
                }
                if (bDeleteDetail && !Model.Id.IsNullOrEmpty())
                {
                    var itemDetails = _tnDtChungTuChiTietService.FindByChungtuId(Model.Id);
                    var itemRemoves = itemDetails.Where(x => ListLNSHasDataUnchecked.Contains(x.Lns)).ToList();
                    if (!itemRemoves.IsEmpty()) _tnDtChungTuChiTietService.RemoveRange(itemRemoves);
                    var itemRemain = itemDetails.Except(itemRemoves);
                    Model.TuChiSum = itemRemain.Sum(x => x.TuChi);
                    UpdatePhanBo();

                }

                TnDtChungTu entity;
                if (Model.Id == Guid.Empty)
                {
                    // Add
                    entity = new TnDtChungTu();
                    _mapper.Map(Model, entity);

                    entity.DateCreated = DateTime.Now;
                    entity.UserCreator = _sessionService.Current.Principal;
                    _tnDtChungTuService.Add(entity);
                }
                else
                {
                    // Update
                    entity = _tnDtChungTuService.FindById(Model.Id);
                    _mapper.Map(Model, entity);

                    entity.DateModified = DateTime.Now;
                    entity.UserModifier = _sessionService.Current.Principal;
                    _tnDtChungTuService.Update(entity);
                }

                DialogHost.CloseDialogCommand.Execute(null, null);

                // Show detail page when saved
                SavedAction?.Invoke(_mapper.Map<TnDtChungTuModel>(entity));
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void UpdatePhanBo()
        {
            TnDtChungTu chungTu = _tnDtChungTuService.FindByIdDotNhan(Model.Id.ToString());
            if (chungTu != null)
            {
                var itemDetails = _tnDtChungTuChiTietService.FindByChungtuId(chungTu.Id);
                var itemRemovePhanBos = itemDetails.Where(x => ListLNSHasDataUnchecked.Contains(x.Lns)).ToList();
                if (!itemRemovePhanBos.IsEmpty())
                {
                    var itemRemainPhanBos = itemDetails.Except(itemRemovePhanBos);
                    _tnDtChungTuChiTietService.RemoveRange(itemRemovePhanBos);
                    chungTu.TuChiSum = itemRemainPhanBos.Sum(x => x.TuChi);
                    _tnDtChungTuService.Update(chungTu);
                }
            }
        }

        private string GetMessageValidateCheckBox()
        {
            List<string> messages = new List<string>();

            ListLNSHasDataUnchecked = DataLNS.Where(n => !n.IsHitTestVisible && !n.IsSelected).Select(n => n.Lns).ToList();
            string lnsText = string.Join(StringUtils.COMMA_SPLIT, ListLNSHasDataUnchecked);

            if (!string.IsNullOrEmpty(lnsText))
            {
                messages.Add(string.Format(Resources.DivisionHasDataLNS, lnsText));
            }

            return string.Join(Environment.NewLine, messages);
        }

        private string GetMessageValidate()
        {
            IList<string> messages = new List<string>();

            if (!Model.NgayChungTu.HasValue)
            {
                messages.Add("Hãy nhập ngày chứng từ.");
            }

            if (string.IsNullOrEmpty(Model.SoQuyetDinh))
            {
                messages.Add("Hãy nhập số quyết định.");
            }

            if (!Model.NgayQuyetDinh.HasValue)
            {
                messages.Add("Hãy nhập ngày quyết định.");
            }

            if (DataLNS.All(x => !x.IsSelected))
            {
                messages.Add("Hãy chọn LNS");
            }

            return string.Join(Environment.NewLine, messages);
        }
    }
}
