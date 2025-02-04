using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.NhanDuToanChiTrenGiao
{
    public class NhanDuToanChiTrenGiaoDialogViewModel : DialogViewModelBase<BhDtctgBHXHModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly INdtctgBHXHService _ndtctgBHXHService;
        private readonly INdtctgBHXHChiTietService _ndtctgBHXHChiTietService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly ISysAuditLogService _log;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private SessionInfo _sessionInfo;

        private bool isActive;
        private bool _isEdit;
        public bool IsEdit
        {
            get => _isEdit;
            set { SetProperty(ref _isEdit, value); }
        }

        private bool _isSaveData;
        public bool IsSaveData
        {
            get => _isSaveData;
            set => SetProperty(ref _isSaveData, value);
        }

        private ICollectionView _lnsView;
        private ICollectionView _loaiChiView;

        private BhDtctgBHXHModel _bhDtctgBHXHModel;
        public BhDtctgBHXHModel BhDtctgBHXHModel
        {
            get => _bhDtctgBHXHModel;
            set
            {
                SetProperty(ref _bhDtctgBHXHModel, value);
                OnPropertyChanged();
            }
        }
        private ObservableCollection<BhDmMucLucNganSachModel> _listLns;
        public ObservableCollection<BhDmMucLucNganSachModel> ListLns
        {
            get => _listLns;
            set => SetProperty(ref _listLns, value);
        }
        private bool _selectAllLns;
        public bool SelectAllLns
        {
            get => ListLns.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllLns, value);
                foreach (var item in ListLns) item.IsChecked = _selectAllLns;
            }
        }

        public string LabelSelectedCountLns
        {
            get => $"LNS ({ListLns.Count(item => item.IsChecked)}/{ListLns.Count})";
        }

        private string _searchLns;

        public string SearchLns
        {
            get => _searchLns;
            set
            {
                if (SetProperty(ref _searchLns, value))
                {
                    _lnsView.Refresh();
                }
            }
        }
        #region Combox loai chi
        private ObservableCollection<CheckBoxItem> _lstLoaiChi;
        public ObservableCollection<CheckBoxItem> LstLoaiChi
        {
            get => _lstLoaiChi;
            set
            {
                SetProperty(ref _lstLoaiChi, value);
                OnPropertyChanged();
            }
        }

        private string _searchLoaiChi;
        public string SearchLoaiChi
        {
            get => _searchLoaiChi;
            set
            {
                if (SetProperty(ref _searchLoaiChi, value))
                {
                    _loaiChiView.Refresh();
                }
            }
        }

        private bool _selectAllLoaiChi;
        public bool SelectAllLoaiChi
        {
            get => LstLoaiChi.Where(x => x.IsFilter).All(x => x.IsChecked);
            set
            {
                SetProperty(ref _selectAllLoaiChi, value);
                foreach (CheckBoxItem item in LstLoaiChi.Where(x => x.IsFilter))
                {
                    item.IsChecked = _selectAllLoaiChi;
                }
            }
        }

        public string SelectedCountLoaiChi
        {
            get => $"Loai chi ({LstLoaiChi.Count(item => item.IsChecked)}/{LstLoaiChi.Count})";
        }
        #endregion

        private ObservableCollection<ComboboxItem> _typeDotPhanBo = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> TypeDotPhanBo
        {
            get => _typeDotPhanBo;
            set => SetProperty(ref _typeDotPhanBo, value);
        }

        private ComboboxItem _selectDotPhanBo;
        public ComboboxItem SelectDotPhanBo
        {
            get => _selectDotPhanBo;
            set
            {
                SetProperty(ref _selectDotPhanBo, value);
            }
        }

        private ComboboxItem _cbxExpenseTypeSelected;
        public ComboboxItem CbxExpenseTypeSelected
        {
            get => _cbxExpenseTypeSelected;
            set
            {
                SetProperty(ref _cbxExpenseTypeSelected, value);
                if (_cbxExpenseTypeSelected != null)
                {
                    LoadLNS();
                }
            }
        }
        public bool IsEnabled => BhDtctgBHXHModel.Id.IsNullOrEmpty();
        private ObservableCollection<ComboboxItem> _cbxExpenseType;
        public ObservableCollection<ComboboxItem> CbxExpenseType
        {
            get => _cbxExpenseType;
            set => SetProperty(ref _cbxExpenseType, value);
        }

        private List<string> lstLNSUnCheckHasData { get; set; }

        public NhanDuToanChiTrenGiaoDialogViewModel() { }
        public NhanDuToanChiTrenGiaoDialogViewModel(
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            INdtctgBHXHChiTietService ndtctgBHXHChiTietService,
            ISessionService sessionService,
            INsDonViService donViService,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
            IMapper mapper,
            INdtctgBHXHService ndtctgBHXHService,
            ILog log)
        {
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _ndtctgBHXHService = ndtctgBHXHService;
            _ndtctgBHXHChiTietService = ndtctgBHXHChiTietService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;
            _logger = log;
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            //LoadLNS();

            LoadDotPhanBo();
            LoadExpenseType();
            LoadLoaiChi();
            LoadData();
        }

        private void LoadExpenseType()
        {
            var listDanhMucChi = _bhDanhMucLoaiChiService.FindByNamLamViec(_sessionInfo.YearOfWork).ToList();
            var cbxExpense = listDanhMucChi?.Select(x => new ComboboxItem
            {
                DisplayItem = x.STenDanhMucLoaiChi,
                HiddenValue = x.SMaLoaiChi,
                ValueItem = x.SLNS,
                Id = x.Id
            }).ToList();
            CbxExpenseType = new ObservableCollection<ComboboxItem>(cbxExpense);
            if (CbxExpenseType.Count() > 0)
            {
                CbxExpenseTypeSelected = CbxExpenseType.ElementAt(0);
            }

            if (BhDtctgBHXHModel.Id != Guid.Empty && !string.IsNullOrEmpty(BhDtctgBHXHModel.IIdLoaiDanhMucChi.ToString()))
            {
                CbxExpenseTypeSelected = CbxExpenseType.Where(x => x.Id == BhDtctgBHXHModel.IIdLoaiDanhMucChi).FirstOrDefault();
            }
        }

        private void LoadLoaiChi()
        {
            try
            {
                LstLoaiChi = new ObservableCollection<CheckBoxItem>();
                var yearOfWork = _sessionInfo.YearOfWork;
                var lstDmLoaiChi = _bhDanhMucLoaiChiService.FindByNamLamViec(_sessionInfo.YearOfWork);
                if (lstDmLoaiChi != null && lstDmLoaiChi.Count() > 0)
                    LstLoaiChi = _mapper.Map<ObservableCollection<CheckBoxItem>>(lstDmLoaiChi);
                _loaiChiView = CollectionViewSource.GetDefaultView(LstLoaiChi);
                _loaiChiView.Filter = ListLoaiChiFilter;

                foreach (var model in LstLoaiChi)
                {
                    if (model.NameItem == LNSValue.LNS_9010001_9010002 || model.NameItem == LNSValue.LNS_9010003 || model.NameItem == LNSValue.LNS_901_9010001_9010002
                        || model.NameItem == LNSValue.LNS_9010004_9010005 || model.NameItem == LNSValue.LNS_9010006_9010007)
                    {
                        model.IsChecked = true;
                    }

                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                        {
                            OnPropertyChanged(nameof(SelectedCountLoaiChi));
                            OnPropertyChanged(nameof(SelectAllLoaiChi));
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool ListLoaiChiFilter(object obj)
        {
            bool result = true;
            var item = (CheckBoxItem)obj;
            if (!string.IsNullOrWhiteSpace(SearchLoaiChi))
                result = item.ValueItem.ToLower().Contains(_searchLoaiChi!.ToLower());
            item.IsFilter = result;
            return result;
        }

        public void LoadLNS()
        {
            int yearOfWork = _sessionService.Current.YearOfWork;
            string idDonVi = _sessionService.Current.IdDonVi;
            List<string> lstSLNLoaiChi = new List<string>();

            if (CbxExpenseTypeSelected != null)
            {
                var danhmuchi = _bhDanhMucLoaiChiService.FindByNamLamViec(_sessionInfo.YearOfWork).Where(x => x.Id == CbxExpenseTypeSelected.Id).FirstOrDefault();
                string sLNLoaiChi = danhmuchi?.SLNS;
                lstSLNLoaiChi = sLNLoaiChi.Split(",").ToList();

            }

            List<BhDmMucLucNganSach> lstBhmuclucngansach = new List<BhDmMucLucNganSach>();
            var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
            predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == yearOfWork);
            predicate_danhmuc = predicate_danhmuc.And(x => lstSLNLoaiChi.Contains(x.SLNS));

            lstBhmuclucngansach = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).ToList();

            List<BhDmMucLucNganSachModel> lstBhmuclucngansachmodel = new List<BhDmMucLucNganSachModel>();

            lstBhmuclucngansachmodel = lstBhmuclucngansach.Select(d => new BhDmMucLucNganSachModel
            {
                Id = d.Id,
                SXauNoiMa = d.SXauNoiMa,
                SLNS = d.SLNS,
                SL = d.SL,
                SK = d.SK,
                SMoTa = d.SLNS + "-" + d.SMoTa,
                IIDMLNS = d.IIDMLNS,
                IIDMLNSCha = d.IIDMLNSCha,
                BHangCha = d.BHangCha,
                INamLamViec = d.INamLamViec
            }).ToList();
            lstBhmuclucngansachmodel = lstBhmuclucngansachmodel.Where(x => x.SLNS.StartsWith("901")).OrderBy(x => x.SXauNoiMa).ToList();
            ListLns = new ObservableCollection<BhDmMucLucNganSachModel>(lstBhmuclucngansachmodel);

            // Filter
            _lnsView = CollectionViewSource.GetDefaultView(ListLns);
            _lnsView.Filter = ListLNSFilter;


            if (_listLns != null && _listLns.Count > 0)
            {

                foreach (var model in _listLns)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhDmMucLucNganSachModel.IsChecked))
                        {
                            foreach (var item in _listLns)
                            {
                                if (item.IIDMLNSCha == model.IIDMLNS)
                                {
                                    item.IsChecked = model.IsChecked;
                                }
                            }
                            OnPropertyChanged(nameof(LabelSelectedCountLns));
                            OnPropertyChanged(nameof(SelectAllLns));
                        }
                    };
                }
            }
        }

        private bool ListLNSFilter(object obj)
        {
            bool result = true;
            var item = (BhDmMucLucNganSachModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchLns))
                result = item.SMoTa.ToLower().Contains(_searchLns!.ToLower());
            item.IsFilter = result;
            return result;
        }

        private void AddListTreeChilDanhMuc(BhDmMucLucNganSachModel danhmuc, List<BhDmMucLucNganSachModel> lstDanhMuc)
        {
            if (lstDanhMuc.Any(n => n.IIDMLNSCha == danhmuc.IIDMLNS))
            {
                foreach (var item in lstDanhMuc.Where(n => n.IIDMLNSCha == danhmuc.IIDMLNS))
                {
                    AddListTreeChilDanhMuc(item, lstDanhMuc);
                }
            }
        }

        public void LoadDotPhanBo()
        {
            var typeReport = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Đầu năm", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Bổ sung", ValueItem = "2"},
            };

            TypeDotPhanBo = new ObservableCollection<ComboboxItem>(typeReport);
            SelectDotPhanBo = TypeDotPhanBo.ElementAt(0);
            if (BhDtctgBHXHModel.Id != Guid.Empty && !string.IsNullOrEmpty(BhDtctgBHXHModel.ILoaiDotNhanPhanBo.ToString()))
            {
                SelectDotPhanBo = TypeDotPhanBo.Single(item => item.ValueItem.Equals(BhDtctgBHXHModel.ILoaiDotNhanPhanBo.ToString()));
            }

        }

        public void LoadData()
        {
            // Trường hợp tạo mới
            if (BhDtctgBHXHModel.Id == Guid.Empty)
            {
                var soChungTuIndex = _ndtctgBHXHService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork);
                BhDtctgBHXHModel = new BhDtctgBHXHModel()
                {
                    DNgayQuyetDinh = DateTime.Now,
                    DNgayChungTu = DateTime.Now,
                    SSoChungTu = "DTC-" + soChungTuIndex.ToString("D3"),
                    INamLamViec = _sessionService.Current.YearOfWork
                };
            }
            else
            {
                //List<string> lstLNSModel = new List<string>();
                //lstLNSModel = BhDtctgBHXHModel.SLNS.Split(",").ToList();

                //ListLns.Where(x => lstLNSModel.Contains(x.SLNS)).Select(c => { c.IsChecked = true; return c; }).ToList();
                SetCheckboxLoaiChiSelected(_lstLoaiChi, BhDtctgBHXHModel.SMaLoaiChi);
                var model = _ndtctgBHXHService.FindById(BhDtctgBHXHModel.Id);
                BhDtctgBHXHModel.SMoTa = model.SMoTa;

                SelectDotPhanBo.ValueItem = BhDtctgBHXHModel.ILoaiDotNhanPhanBo.ToString(); ;

                OnPropertyChanged(nameof(LstLoaiChi));
                OnPropertyChanged(nameof(SelectedCountLoaiChi));
                OnPropertyChanged(nameof(SelectAllLoaiChi));
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        private void SetCheckboxLoaiChiSelected(ObservableCollection<CheckBoxItem> loaiChiIItems, string sMaLoaiChi)
        {
            if (string.IsNullOrEmpty(sMaLoaiChi) || LstLoaiChi == null || LstLoaiChi.Count == 0)
                return;
            List<string> selectedValues = sMaLoaiChi.Split(",").ToList();
            foreach (CheckBoxItem item in loaiChiIItems)
            {
                item.IsChecked = selectedValues.Contains(item.ValueItem);
            }
        }

        private string GetValueLoaiChisMaLoaiChiSelected(ObservableCollection<CheckBoxItem> loaiChiIItems)
        {
            if (loaiChiIItems.Any())
            {
                return string.Join(",", loaiChiIItems.Where(n => n.IsChecked == true).Select(n => n.ValueItem).Distinct().ToList());
            }

            return string.Empty;
        }

        private string GetValueLoaiChisLNSSelected(ObservableCollection<CheckBoxItem> loaiChiIItems)
        {
            if (loaiChiIItems.Any())
            {
                return string.Join(",", loaiChiIItems.Where(n => n.IsChecked == true).Select(n => n.NameItem).Distinct().ToList());
            }

            return string.Empty;
        }

        public bool IsValidate()
        {
            bool result = true;

            if (ListLns.Count > 0)
            {
                ListLns.ForAll(x =>
                {
                    x.IsChecked = true;
                });
            }

            var listLNSHasDataUnchecked = ListLns.Where(n => n.IsChecked).Select(n => n.SLNS).ToList();
            if (string.IsNullOrEmpty(BhDtctgBHXHModel.DNgayChungTu.ToString()))
            {
                MessageBoxHelper.Warning(string.Format(Resources.MsgCheckNullNgayNhanPhanBoBHXH));
                result = false;
            }
            else if (string.IsNullOrEmpty(BhDtctgBHXHModel.SSoQuyetDinh))
            {
                MessageBoxHelper.Warning(string.Format(Resources.AlertSoQuyetDinhEmpty));
                result = false;
            }
            else if (string.IsNullOrEmpty(BhDtctgBHXHModel.DNgayQuyetDinh.ToString()))
            {
                MessageBoxHelper.Warning(string.Format(Resources.ErrorNgayQuyetDinhEmpty));
                result = false;
            }

            //if (SelectDotPhanBo != null)
            //{
            //    MessageBoxHelper.Warning(string.Format(Resources.MsgDotNhanEmpty));
            //    result = false;
            //}
            return result;
        }

        public override void OnSave()
        {
            base.OnSave();

            try
            {

                if (!IsValidate())
                {
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
                if (bDeleteDetail)
                {
                    //Xóa chi tiết nhận dự toán chi trên giao
                    var lstDetailBefore = _ndtctgBHXHChiTietService.FindByCondition(BhDtctgBHXHModel.Id).ToList();
                    var lstDetailDelete = lstDetailBefore.Where(x => lstLNSUnCheckHasData.Contains(x.SLNS)).ToList();
                    _ndtctgBHXHChiTietService.RemoveRange(lstDetailDelete);
                }

                int yearOfWork = _sessionService.Current.YearOfWork;
                string idDonVi = _sessionService.Current.IdDonVi;
                BhDtctgBHXH model = new BhDtctgBHXH();
                DateTime dtNow = DateTime.Now;
                _mapper.Map(BhDtctgBHXHModel, model);
                model.ILoaiDotNhanPhanBo = Int32.Parse(SelectDotPhanBo.ValueItem);

                if (BhDtctgBHXHModel.Id == Guid.Empty)
                {
                    var donvi = _donViService.FindByIdDonVi(idDonVi, yearOfWork);
                    model.IID_DonVi = donvi.Id;
                    model.IID_MaDonVi = donvi.IIDMaDonVi;
                    model.DNgayTao = dtNow;
                    model.DNgaySua = null;
                    model.SNguoiTao = _sessionInfo.Principal;
                    model.BIsKhoa = false;
                    model.SMaLoaiChi = GetValueLoaiChisMaLoaiChiSelected(LstLoaiChi);
                    model.SLNS = GetValueLoaiChisLNSSelected(LstLoaiChi);

                    _ndtctgBHXHService.Add(model);
                }
                else
                {
                    var lstDetailAfter = _ndtctgBHXHChiTietService.FindByCondition(BhDtctgBHXHModel.Id).ToList();
                    model.FTongTien = lstDetailAfter?.Sum(x => x.FTongTien);
                    model.FTongTienTuChi = lstDetailAfter?.Sum(x => x.FTienTuChi);
                    model.FTongTienHienVat = lstDetailAfter?.Sum(x => x.FTienHienVat);
                    model.SMaLoaiChi = GetValueLoaiChisMaLoaiChiSelected(LstLoaiChi);
                    model.SLNS = GetValueLoaiChisLNSSelected(LstLoaiChi);
                    model.DNgaySua = dtNow;
                    model.SNguoiSua = _sessionInfo.Principal;

                    _ndtctgBHXHService.Update(model);
                }

                DialogHost.CloseDialogCommand.Execute(null, null);
                SavedAction?.Invoke(_mapper.Map<BhDtctgBHXHModel>(model));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private string GetMessageValidateCheckBox()
        {
            List<string> messages = new List<string>();
            string sLNS = string.Join(",", LstLoaiChi.Where(n => n.IsChecked == true).Select(n => n.NameItem).Distinct().ToList());
            List<string> listLNSHasDataUnchecked = new List<string>();
            if (sLNS.Length > 1)
            {
                listLNSHasDataUnchecked.AddRange(sLNS.Split(','));
            }

            var lstDetail = _ndtctgBHXHChiTietService.FindByCondition(BhDtctgBHXHModel.Id).ToList();

            lstLNSUnCheckHasData = lstDetail.Where(x => !listLNSHasDataUnchecked.Contains(x.SLNS)).Select(x => x.SLNS).Distinct().ToList();

            string lnsText = string.Join(StringUtils.COMMA_SPLIT, lstLNSUnCheckHasData);

            if (!string.IsNullOrEmpty(lnsText))
            {
                messages.Add(string.Format(Resources.DivisionHasDataLNS, lnsText));
            }

            return string.Join(Environment.NewLine, messages);
        }
    }
}
