using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.ThamDinhQuyetToan;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.ThamDinhQuyetToan
{
    public class ThamDinhQuyetToanSummaryViewModel : StandardDialogViewModelBase<BhThamDinhQuyetToanModel>
    {
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhDmThamDinhQuyetToanService _bhDmThamDinhQuyetToanService;
        private readonly IBhThamDinhQuyetToanService _bhThamDinhQuyetToanService;
        private readonly IBhThamDinhQuyetToanChiTietService _bhThamDinhQuyetToanChiTietService;
        private readonly ISysAuditLogService _log;

        private ICollectionView _dataLNSView;
        private ICollectionView _dataCSYTView;
        private ICollectionView _nsDonViModelsView;

        public override Type ContentType => typeof(ThamDinhQuyetToanSummary);
        public override string Name => Guid.Empty.Equals(Model.Id) ? "TỔNG HỢP BÁO CÁO" : "CẬP NHẬT BÁO CÁO";
        public override string Description => Guid.Empty.Equals(Model.Id) ? "Tạo mới báo cáo tổng hợp thẩm định quyết toán" : "Cập nhật báo cáo tổng hợp thẩm định quyết toán";
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "Chọn LNS ({0}/{1})";
        public bool IsSummary { get; set; }
        public bool IsEnabled => Guid.Empty.Equals(Model.Id);
        public List<BhThamDinhQuyetToanModel> ListChungTuSummary { get; set; }

        private bool _isSaveData;
        public bool IsSaveData
        {
            get => _isSaveData;
            set => SetProperty(ref _isSaveData, value);
        }
        public bool IsAggregate { get; set; } = false;
        public bool IsEdit => Model.Id == Guid.Empty && !IsSummary;

        public List<string> ListIdDonViHasCt { get; set; }
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

        private ObservableCollection<DonViModel> _donViModelItems;
        public ObservableCollection<DonViModel> DonViModelItems
        {
            get => _donViModelItems;
            set
            {
                SetProperty(ref _donViModelItems, value);
                OnPropertyChanged();
            }
        }
        public string SelectedCountNsDonVi
        {
            get
            {
                var totalCount = DonViModelItems != null ? DonViModelItems.Count() : 0;
                var totalSelected = DonViModelItems != null ? DonViModelItems.Count(item => item.Selected) : 0;
                return string.Format("ĐƠN VỊ ({0}/{1})", totalSelected, totalCount);
            }
        }
        #region list LNS

        private ObservableCollection<BhDmMucLucNganSachModel> _dataLNS;
        public ObservableCollection<BhDmMucLucNganSachModel> DataLNS
        {
            get => _dataLNS;
            set => SetProperty(ref _dataLNS, value);
        }

        public string SelectedCountLNS
        {
            get
            {
                int totalCount = DataLNS != null ? DataLNS.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = DataLNS != null ? DataLNS.Count(item => item.IsSelected) : 0;
                return string.Format(SELECTED_BUDGET_INDEX_COUNT_STR, totalSelected, totalCount);
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
                if (SetProperty(ref _searchLNS, value))
                {
                    _dataLNSView.Refresh();
                    OnPropertyChanged(nameof(SelectedCountLNS));
                }
            }
        }
        #endregion

        public ThamDinhQuyetToanSummaryViewModel(
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IBhDmThamDinhQuyetToanService bhDmThamDinhQuyetToanService,
            IBhThamDinhQuyetToanService bhThamDinhQuyetToanService,
            IBhThamDinhQuyetToanChiTietService bhThamDinhQuyetToanChiTietService,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            ISysAuditLogService log,
            IDanhMucService danhMucService,
            ILog logger) : base(sessionService, mapper, logger, nsDonViService, danhMucService)
        {
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _bhDmThamDinhQuyetToanService = bhDmThamDinhQuyetToanService;
            _bhThamDinhQuyetToanService = bhThamDinhQuyetToanService;
            _bhThamDinhQuyetToanChiTietService = bhThamDinhQuyetToanChiTietService;
            _log = log;
        }

        public override void Init()
        {
            try
            {
                if (Model == null) Model = new BhThamDinhQuyetToanModel();
                if (Model.Id == Guid.Empty)
                {
                    Model = new BhThamDinhQuyetToanModel();
                    LoadChungTuIndex();
                    Model.DNgayChungTu = DateTime.Now;
                    Model.SMoTa = "Chi tiết";
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        //private void LoadLNS()
        //{
        //    int yearOfWork = _sessionService.Current.YearOfWork;
        //    List<BhDmMucLucNganSach> listMLNS;
        //    listMLNS = _bhDmMucLucNganSachService.GetListMucLucForDanhMucLoaiChi(yearOfWork, LNSValue.LNS_9010001_9010002).ToList();

        //    DataLNS = _mapper.Map<ObservableCollection<BhDmMucLucNganSachModel>>(listMLNS);
        //    _dataLNSView = CollectionViewSource.GetDefaultView(DataLNS);
        //    _dataLNSView.Filter = ListLNSFilter;

        //    if (_dataLNS != null && _dataLNS.Count > 0)
        //    {
        //        foreach (var model in _dataLNS)
        //        {
        //            model.PropertyChanged += (sender, args) =>
        //            {
        //                if (args.PropertyName == nameof(BhDmMucLucNganSachModel.IsSelected))
        //                {
        //                    foreach (var item in _dataLNS)
        //                    {
        //                        if (item.IIDMLNSCha == model.IIDMLNS)
        //                        {
        //                            item.IsSelected = model.IsSelected;
        //                        }
        //                    }
        //                    OnPropertyChanged(nameof(SelectAllLNS));
        //                    OnPropertyChanged(nameof(SelectedCountLNS));
        //                }
        //            };
        //        }
        //    }
        //}

        private bool ListLNSFilter(object obj)
        {
            bool result = true;
            var item = (BhDmMucLucNganSachModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchLNS))
                result = item.LNSDisplay.ToLower().Contains(_searchLNS!.ToLower());
            item.IsFilter = result;
            return result;
        }

        public override void LoadData(params object[] args)
        {
            if (Model == null || Model.Id == Guid.Empty)
            {
                Model.DNgayChungTu = DateTime.Now;
                Model.INamLamViec = YearOfWork;
                LoadChungTuIndex();
            }
            else
            {
                DonViModel donViModel = DonViModelItems.Where(x => x.IIDMaDonVi == Model.IID_MaDonVi).FirstOrDefault();
                if (donViModel != null)
                {
                    donViModel.Selected = true;
                }
            }
        }

        private bool DonViFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(SearchNsDonVi))
            {
                return true;
            }
            var item = (DonViModel)obj;
            var condition = item.TenDonVi.ToLower().Contains(SearchNsDonVi.Trim().ToLower()) ||
                            item.IIDMaDonVi.ToLower().Contains(SearchNsDonVi.Trim().ToLower());
            return condition;
        }

        private List<string> CheckSummary()
        {
            var predicate = PredicateBuilder.True<BhThamDinhQuyetToan>();
            predicate = predicate.And(x => x.INamLamViec == YearOfWork);
            predicate = predicate.And(x => !string.IsNullOrEmpty(x.STongHop));

            List<BhThamDinhQuyetToan> chungTu = _bhThamDinhQuyetToanService.FindAll(predicate).ToList();
            List<string> listSoChungTuSummary = new List<string>();
            foreach (var item in chungTu)
            {
                listSoChungTuSummary.AddRange(item.STongHop.Split(",").ToList());
            }
            List<string> listResult = listSoChungTuSummary.Where(x => ListChungTuSummary.Select(n => n.SSoChungTu).ToList().Contains(x)).ToList();
            return listResult;
        }

        public override void OnSave()
        {
            try
            {
                if (Model.Id != Guid.Empty)
                {
                    BhThamDinhQuyetToan entity = _bhThamDinhQuyetToanService.Find(Model.Id);
                    entity.DNgayChungTu = (DateTime)Model.DNgayChungTu;
                    entity.SMoTa = Model.SMoTa;
                    _bhThamDinhQuyetToanService.Update(entity);

                    DialogHost.Close(SystemConstants.ROOT_DIALOG);
                    SavedAction?.Invoke(_mapper.Map<BhThamDinhQuyetToanModel>(entity));
                }
                else
                {
                    DonVi donVi0 = _donViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
                    string message = GetMessageValidate();
                    if (!string.IsNullOrEmpty(message))
                    {
                        System.Windows.Forms.MessageBox.Show(message, Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (donVi0 != null)
                    {
                        if (ListChungTuSummary == null || ListChungTuSummary.Count() == 0)
                        {
                            return;
                        }

                        List<string> listSummaryHistory = CheckSummary();
                        if (listSummaryHistory != null && listSummaryHistory.Count > 0)
                        {
                            MessageBoxHelper.Warning(string.Format(Resources.MsgSumaryWarning, string.Join(",", listSummaryHistory)));
                            return;
                        }

                        LoadChungTuIndex();
                        BhThamDinhQuyetToan entity = new BhThamDinhQuyetToan();
                        entity.SSoChungTu = Model.SSoChungTu;
                        entity.SMoTa = Model.SMoTa;
                        entity.DNgayChungTu = Model.DNgayChungTu;
                        entity.STongHop = string.Join(",", ListChungTuSummary.Select(n => n.SSoChungTu).ToList());
                        entity.BDaTongHop = true;
                        entity.IID_MaDonVi = _sessionService.Current.IdDonVi;
                        entity.DNgayTao = DateTime.Now;
                        entity.DNgaySua = null;
                        entity.INamLamViec = YearOfWork;
                        entity.SNguoiTao = _sessionService.Current.Principal;
                        _bhThamDinhQuyetToanService.Add(entity);

                        CreateDetailSummary(entity);

                        _bhThamDinhQuyetToanService.UpdateTotalChungTu(entity.Id.ToString(), _sessionService.Current.Principal);

                        DialogHost.CloseDialogCommand.Execute(null, null);
                        MessageBoxHelper.Info(Resources.MsgSumaryDone);
                        SavedAction?.Invoke(_mapper.Map<BhThamDinhQuyetToanModel>(entity));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CreateDetailSummary(BhThamDinhQuyetToan chungTu)
        {
            _bhThamDinhQuyetToanChiTietService.CreateVoudcherSummary(string.Join(",", ListChungTuSummary.Select(n => n.Id.ToString()).ToList())
                , _sessionService.Current.Principal, _sessionService.Current.YearOfWork, chungTu.Id.ToString());
        }

        private string GetMessageValidate()
        {
            List<string> messages = new List<string>();

            if (Model.DNgayChungTu == null)
            {
                messages.Add(Resources.AlertNgayChungTuEmpty);
            }

            //check đã tồn tại số quyết định
            var predicate_sqd = PredicateBuilder.True<BhThamDinhQuyetToan>();
            predicate_sqd = predicate_sqd.And(x => x.INamLamViec == Model.INamLamViec);

            var chungtu_sqd = _bhThamDinhQuyetToanService.FindAll(predicate_sqd).FirstOrDefault();
            if (chungtu_sqd != null)
            {
                messages.Add(Resources.MsgTrungSoQD);
            }

            //Check đã tồn chứng từ tổng hợp của đơn vị 0
            DonVi donVi0 = _donViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
            var predicate = PredicateBuilder.True<BhThamDinhQuyetToan>();
            predicate = predicate.And(x => x.IID_MaDonVi == donVi0.IIDMaDonVi);
            predicate = predicate.And(x => x.INamLamViec == YearOfWork);

            var chungtu = _bhThamDinhQuyetToanService.FindAll(predicate).FirstOrDefault();
            if (chungtu != null)
            {
                messages.Add(string.Format(Resources.AlertExistSettlementMonthVoucher, donVi0?.TenDonVi, _sessionService.Current.YearOfWork, ""));
            }

            return string.Join(Environment.NewLine, messages);
        }

        public static string GetValueSelected(ObservableCollection<BhDmMucLucNganSachModel> data)
        {
            if (data.Count > 0)
            {
                return string.Join(",", data.Where(n => n.IsSelected == true).Select(n => n.SLNS).Distinct().ToList());
            }
            return string.Empty;
        }

        public static void SetCheckboxSelected(ObservableCollection<BhDmMucLucNganSachModel> data, string value)
        {
            if (string.IsNullOrEmpty(value) || data == null || data.Count == 0)
                return;
            List<string> selectedValues = value.Split(",").Distinct().ToList();
            foreach (BhDmMucLucNganSachModel item in data)
            {
                item.IsSelected = selectedValues.Contains(item.SLNS);
            }
        }

        private void LoadChungTuIndex()
        {
            var listCT = _bhThamDinhQuyetToanService.FindAll(x => x.INamLamViec == _sessionService.Current.YearOfWork).OrderByDescending(x => x.SSoChungTu);
            if (!listCT.Any()) Model.SSoChungTu = "QT-001";
            else
            {
                try
                {
                    var soCT = listCT.Select(x => x.SSoChungTu).FirstOrDefault();
                    Model.SSoChungTu = "QT-" + (int.Parse(soCT.Substring(3, 3)) + 1).ToString("D3");
                }
                catch
                {
                    Model.SSoChungTu = "QT-" + (listCT.Count() + 1).ToString("D3");
                }
            }

        }
    }
}
