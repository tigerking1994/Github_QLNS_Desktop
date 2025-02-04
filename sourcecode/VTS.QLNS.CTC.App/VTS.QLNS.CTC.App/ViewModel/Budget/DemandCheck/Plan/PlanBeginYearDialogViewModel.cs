using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Converters;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Plan
{
    public class PlanBeginYearDialogViewModel : DialogViewModelBase<PlanBeginYearModel>
    {
        private readonly ISktSoLieuChungTuService _sktChungTuService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly ISktChungTuChiTietService _sktChungTuChiTietService;
        private readonly ISysAuditLogService _log;
        private readonly IMapper _mapper;
        private readonly INsNguoiDungLnsService _nsNguoiDungLNSService;
        private ICollectionView _nsDonViModelsView;
        private SessionInfo _sessionInfo;
        private ICollectionView _listLNSView;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;
        private readonly INsMucLucNganSachService _nSMucLucNganSachService;
        private readonly INsDtChungTuChiTietService _nsDtChungTuChiTietService;
        private readonly ISktSoLieuService _sktSoLieuService;
        private readonly ISktSoLieuChiTietCanCuDataService _sktSoLieuChiTietCanCuDataService;
        private readonly ISoLieuChiTietPhanCapService _soLieuChiTietPhanCapService;

        public override string Name => (Model != null && Model.Id != Guid.Empty) ? "Sửa lập dự toán đầu năm" : "Thêm lập dự toán đầu năm";
        public override Type ContentType => typeof(View.Budget.DemandCheck.Plan.PlanBeginYearDialog);
        public override PackIconKind IconKind => PackIconKind.FileDocumentBoxTickOutline;
        public override string Title => (Model != null && Model.Id != Guid.Empty) ? "SỬA CHỨNG TỪ LẬP DỰ TOÁN NGÂN SÁCH NĂM" : "THÊM CHỨNG TỪ LẬP DỰ TOÁN NGÂN SÁCH NĂM";
        public override string Description => (Model != null && Model.Id != Guid.Empty) ? "SỬA CHỨNG TỪ LẬP DỰ TOÁN NGÂN SÁCH NĂM" : "THÊM CHỨNG TỪ LẬP DỰ TOÁN NGÂN SÁCH NĂM";
        public bool IsEnableView { get; set; }
        public string IsEnableValue => IsEnableView ? "True" : "False";

        //sửa lỗi khi chưa lưu, đóng pop up tự động cập nhập giá trị vào danh sách
        public DateTime? DNgayChungTu { get; set; }
        public string SMoTa { get; set; }

        //public List<string> ListIdDonViHasCt { get; set; }
        public List<NsSktChungTuModel> ListIdsSktChungTuSummary { get; set; }
        public bool IsSummary { get; set; }

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

        private ObservableCollection<ComboboxItem> _budgetSourceTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> BudgetSourceTypes
        {
            get => _budgetSourceTypes;
            set => SetProperty(ref _budgetSourceTypes, value);
        }

        private ComboboxItem _budgetSourceTypeSelected;

        public ComboboxItem BudgetSourceTypeSelected
        {
            get => _budgetSourceTypeSelected;
            set
            {
                SetProperty(ref _budgetSourceTypeSelected, value);
                LoadNsDonVis();
                OnPropertyChanged(nameof(SelectedCountNsDonVi));
            }
        }

        private ObservableCollection<ComboboxItem> _voucherTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> VoucherTypes
        {
            get => _voucherTypes;
            set
            {
                SetProperty(ref _voucherTypes, value);
            }
        }

        private ComboboxItem _voucherTypeSelected;

        public ComboboxItem VoucherTypeSelected
        {
            get => _voucherTypeSelected;
            set
            {
                SetProperty(ref _voucherTypeSelected, value);
                LoadNsDonVis();
                LoadLNS();
                OnPropertyChanged(nameof(SelectedCountNsDonVi));
            }
        }

        public bool Flag { get; set; } = false;

        private bool _selectAllLNS;
        public bool SelectAllLNS
        {
            get => ListLNS != null && ListLNS.Any() && ListLNS.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllLNS, value);
                if (ListLNS != null)
                {
                    Flag = true;
                    ListLNS.Select(c => { c.IsChecked = _selectAllLNS; return c; }).ToList();
                    Flag = false;
                }
            }
        }

        public string SelectedCountLNS
        {
            get
            {
                int totalCount = ListLNS != null ? ListLNS.Count : 0;
                int totalSelected = ListLNS != null ? ListLNS.Count(item => item.IsChecked) : 0;
                return string.Format("CHỌN LNS ({0}/{1})", totalSelected, totalCount);
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
                    _listLNSView.Refresh();
                }
            }
        }

        private ObservableCollection<CheckBoxTreeItem> _listLNS;
        public ObservableCollection<CheckBoxTreeItem> ListLNS
        {
            get => _listLNS;
            set => SetProperty(ref _listLNS, value);
        }

        public bool IsEdit => Model.Id == Guid.Empty && !IsSummary;

        public bool IsEditMlns;

        public PlanBeginYearDialogViewModel(INsDonViService nsDonViService,
            ISktSoLieuChungTuService sktChungTuService,
            ISktChungTuChiTietService sktChungTuChiTietService,
            INsMucLucNganSachService nSMucLucNganSachService,
            IMapper mapper,
            ISessionService sessionService,
            INsNguoiDungDonViService nsNguoiDungDonViService,
            INsNguoiDungLnsService nsNguoiDungLNSService,
            INsDtChungTuChiTietService nsDtChungTuChiTietService,
            ISktSoLieuService sktSoLieuService,
            ISktSoLieuChiTietCanCuDataService sktSoLieuChiTietCanCuDataService,
            ISoLieuChiTietPhanCapService soLieuChiTietPhanCapService,
        ISysAuditLogService log)
        {
            _sessionService = sessionService;
            _sktChungTuService = sktChungTuService;
            _sktChungTuChiTietService = sktChungTuChiTietService;
            _nsDonViService = nsDonViService;
            _nSMucLucNganSachService = nSMucLucNganSachService;
            _nsNguoiDungLNSService = nsNguoiDungLNSService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _nsDtChungTuChiTietService = nsDtChungTuChiTietService;
            _sktSoLieuService = sktSoLieuService;
            _sktSoLieuChiTietCanCuDataService = sktSoLieuChiTietCanCuDataService;
            _soLieuChiTietPhanCapService = soLieuChiTietPhanCapService;
            _log = log;
            _mapper = mapper;
        }

        public override void OnSave()
        {
            base.OnSave();

            DateTime dtNow = DateTime.Now;
            var donViSelected = NsDonViModelItems.FirstOrDefault(n => n.Selected);
            if (donViSelected == null && IsEnableView)
            {
                MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                return;
            }

            string selectedLNS = CheckboxSelectedToStringConvert.GetValueSelected(ListLNS);
            if (string.IsNullOrEmpty(selectedLNS))
            {
                MessageBoxHelper.Warning(Resources.MsgCheckLNS);
                return;
            }

            if (DNgayChungTu == null)
            {
                MessageBoxHelper.Warning(string.Format(Resources.MsgInputRequire, "Ngày chứng từ"));
                return;
            }

            //Thực hiện yêu càu mới, cho phép  thay đổi mục lục ngân sách khi sửa chứng từ
            List<string> lstMlnsDelete = new List<string>();
            string lstMlnsOrg = Model.DsLNS;
            if (Model.Id != Guid.Empty)
            {
                //Lấy danh sách mục lục ngân sách ban đầu

                string[] arrMlnsOrg = lstMlnsOrg.Split(',');
                string[] arrMlnsSelect = selectedLNS.Split(',');
                for (int i = 0; i < arrMlnsOrg.Length; i++)
                {
                    if (Array.IndexOf(arrMlnsSelect, arrMlnsOrg[i]) == -1)
                    {
                        lstMlnsDelete.Add(arrMlnsOrg[i]);
                    }
                }
                if (lstMlnsDelete.Count() > 0)
                {

                    var predicate1 = PredicateBuilder.True<NsDtdauNamChungTuChiTiet>();
                    predicate1 = predicate1.And(x => x.IIdCtdtdauNam == Model.Id);
                    var listChungTuChiTiet = _sktSoLieuService.FindByCondition(predicate1);

                    var predicate2 = PredicateBuilder.True<NsDtdauNamChungTuChiTietCanCu>();
                    predicate2 = predicate2.And(x => x.IID_CTDTDauNam == Model.Id);
                    var listChungTuCanCu = _sktSoLieuChiTietCanCuDataService.FindByCondition(predicate2);

                    listChungTuChiTiet = listChungTuChiTiet.Where(x => lstMlnsDelete.Contains(x.SLns)).ToList();
                    listChungTuCanCu = listChungTuCanCu.Where(x => lstMlnsDelete.Contains(x.SLns)).ToList();

                    if (listChungTuChiTiet.Count() > 0 || listChungTuCanCu.Count() > 0)
                    {
                        List<string> lstMlnsData = new List<string>();
                        foreach (var item in listChungTuChiTiet)
                        {
                            if (!lstMlnsData.Contains(item.SLns))
                            {
                                lstMlnsData.Add(item.SLns);
                            }

                        }

                        foreach (var item in listChungTuCanCu)
                        {
                            if (!lstMlnsData.Contains(item.SLns))
                            {
                                lstMlnsData.Add(item.SLns);
                            }

                        }

                        MessageBoxResult dialogConfirm = MessageBox.Show(string.Format("Mục lục ngân sách " + string.Join(", ", lstMlnsData) + " đã có số liệu chi tiết, nếu bỏ chọn dữ liệu chi tiết theo mục lục ngân sách sẽ bị xóa. Bạn có chắc chắn muốn xóa?"), Name, System.Windows.MessageBoxButton.YesNo);

                        if (dialogConfirm == MessageBoxResult.No)
                        {
                            selectedLNS = lstMlnsOrg;
                            lstMlnsDelete = new List<string>();
                            VoucherTypeSelected = VoucherTypes.Where(n => n.ValueItem == Model.LoaiNganSach).FirstOrDefault();
                            CheckboxSelectedToStringConvert.SetCheckboxSelected(ListLNS, Model.DsLNS);
                            OnPropertyChanged(nameof(SelectAllLNS));
                            return;
                        }
                    }
                }

            }

            OnSaveData(selectedLNS, lstMlnsDelete);

        }

        public void OnSaveData(string selectedLNS, List<string> lstMlnsDelete)
        {
            bool IsSave = true;
            DateTime dtNow = DateTime.Now;
            var donViSelected = NsDonViModelItems.FirstOrDefault(n => n.Selected);
            if (IsEnableView)
            {
                Model.Id_DonVi = donViSelected?.IIDMaDonVi;
                Model.TenDonVi = donViSelected?.TenDonVi;
            }
            else
            {
                Model.Id_DonVi = _sessionInfo.IdDonVi;
                Model.TenDonVi = _sessionInfo.TenDonVi;
            }

            Model.LoaiNganSach = VoucherTypeSelected.ValueItem;
            Model.DNgayChungTu = DNgayChungTu;
            Model.SMoTa = SMoTa == null ? "" : SMoTa.Trim();
            NsDtdauNamChungTu chungTu = new NsDtdauNamChungTu();
            if (Model.Id == Guid.Empty)
            {
                chungTu = new NsDtdauNamChungTu();
                var loaiNguonNganSach = Int32.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                int soChungTuIndex = _sktChungTuService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork,
                _sessionService.Current.Budget, _sessionService.Current.YearOfBudget, int.Parse(VoucherTypeSelected.ValueItem));
                _mapper.Map(Model, chungTu);
                chungTu.ILoaiChungTu = int.Parse(VoucherTypeSelected.ValueItem);
                chungTu.INamLamViec = _sessionService.Current.YearOfWork;
                chungTu.IIdMaNguonNganSach = _sessionService.Current.Budget;
                chungTu.INamNganSach = _sessionService.Current.YearOfBudget;
                chungTu.DNgayTao = DateTime.Now;
                chungTu.ILoaiNguonNganSach = loaiNguonNganSach;
                chungTu.SNguoiTao = _sessionService.Current.Principal;
                chungTu.ISoChungTuIndex = soChungTuIndex;
                chungTu.SDslns = selectedLNS;
                _sktChungTuService.Add(chungTu);
                _log.WriteLog(Resources.ApplicationName, Description, (int)TypeExecute.Insert, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
            }
            else
            {
                chungTu = _sktChungTuService.Find(Model.Id);
                var loaiNguonNganSach = Int32.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                _mapper.Map(Model, chungTu);
                chungTu.DNgaySua = DateTime.Now;
                chungTu.ILoaiNguonNganSach = loaiNguonNganSach;
                chungTu.SNguoiSua = _sessionInfo.Principal;
                chungTu.SDslns = selectedLNS;
                _sktChungTuService.Update(chungTu);
                IsSave = false;

                //Xóa các chứng từ chi tiết khi thay đổi mlns
                if (lstMlnsDelete.Count() > 0)
                {
                    //xóa chứng từ phân cấp
                    string[] arrMlnsDelete = lstMlnsDelete.ToArray();
                    var lstPhancap = _soLieuChiTietPhanCapService.FindAll().Where(x => x.INamLamViec == _sessionService.Current.YearOfWork && x.IIdCtdtDauNam == Model.Id).ToList();
                    if (lstPhancap.Count() > 0)
                    {
                        foreach (var item in lstPhancap)
                        {
                            string sXauNoiMa = item.sXauNoiMaGoc;
                            if (!string.IsNullOrEmpty(sXauNoiMa))
                            {
                                string[] arrXauNoiMa = sXauNoiMa.Split("-");
                                if (Array.IndexOf(arrMlnsDelete, arrXauNoiMa[0]) != -1)
                                {
                                    _soLieuChiTietPhanCapService.Delete(item);
                                }
                            }
                        }
                    }

                    //xóa chứng từ chi tiết và căn cứ
                    _sktChungTuService.DeleteCtdnctByDeleteMlns(Model.Id, string.Join(",", lstMlnsDelete), _sessionService.Current.YearOfWork);
                }
                _log.WriteLog(Resources.ApplicationName, Description, (int)TypeExecute.Update, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
            }
            DonVi donViValue = GetDonVi(chungTu.IIdMaDonVi);
            var obj = _mapper.Map<PlanBeginYearModel>(chungTu);
            obj.TenDonVi = donViValue.TenDonVi;
            obj.Loai = donViValue.Loai;
            OnPropertyChanged(nameof(DNgayChungTu));
            OnPropertyChanged(nameof(SMoTa));

            if (IsSave)
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            else
            {
                DialogHost.Close(null);
            }
            SavedAction?.Invoke(obj);
        }



        private DonVi GetDonVi(string maDonVi)
        {
            DonVi data = _nsDonViService.FindByNamLamViec(_sessionInfo.YearOfWork).Where(n => n.IIDMaDonVi == maDonVi).FirstOrDefault();
            return data;
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

        private List<string> GetListIdDonViHasCt()
        {
            if (VoucherTypeSelected == null)
            {
                return new List<string>();
            }
            var predicate = PredicateBuilder.True<NsDtdauNamChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ILoaiChungTu == int.Parse(VoucherTypeSelected.ValueItem));
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);

            List<NsDtdauNamChungTu> chungTu = _sktChungTuService.FindByCondition(predicate).ToList();
            if (chungTu != null && chungTu.Count > 0)
            {
                return chungTu.Select(n => n.IIdMaDonVi).ToList();
            }
            return new List<string>();
        }

        private List<NguoiDungDonVi> GetListNguoiDungDonVi()
        {
            var predicate = PredicateBuilder.True<NguoiDungDonVi>();
            predicate = predicate.And(x => x.IIDMaNguoiDung.Equals(_sessionService.Current.Principal));
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            List<NguoiDungDonVi> nsDungDonVis = _nsNguoiDungDonViService.FindAll(predicate).ToList();
            return nsDungDonVis;
        }

        private void LoadBudgetSourceTypes()
        {
            BudgetSourceTypes = new ObservableCollection<ComboboxItem> {
                new ComboboxItem() { DisplayItem = "Ngân sách dự toán", ValueItem = TypeLoaiNNS.DU_TOAN.ToString() },
                new ComboboxItem() { DisplayItem = "Ngân sách bệnh viện tự chủ", ValueItem = TypeLoaiNNS.BENH_VIEN.ToString() },
                new ComboboxItem() { DisplayItem = "Ngân sách doanh nghiệp", ValueItem = TypeLoaiNNS.DOANH_NGHIEP.ToString() }
            };

            if (Model.Id != Guid.Empty)
            {
                if (Model.ILoaiNguonNganSach.HasValue)
                {
                    BudgetSourceTypeSelected = BudgetSourceTypes.ElementAt(Model.ILoaiNguonNganSach.Value - 1);
                }
                else
                {
                    BudgetSourceTypeSelected = null;
                }
            }
            else
            {
                if (IsSummary)
                {
                    BudgetSourceTypeSelected = BudgetSourceTypes.ElementAt(ListIdsSktChungTuSummary.First().ILoaiNguonNganSach.Value - 1);
                }
                else
                {
                    BudgetSourceTypeSelected = BudgetSourceTypes.ElementAt(0);
                }
            }

        }

        private void LoadNsDonVis()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();

            List<DonVi> listUnit = _nsDonViService.FindByNamLamViec(yearOfWork).ToList();
            if (VoucherTypeSelected != null && VoucherTypeSelected.ValueItem == VoucherType.NSBD_Key) listUnit = listUnit.Where(x => x.BCoNSNganh).ToList();

            List<NguoiDungDonVi> listNguoiDungDonVi = GetListNguoiDungDonVi();
            if (listNguoiDungDonVi != null && listNguoiDungDonVi.Count > 0)
            {
                listUnit = listUnit.Where(n => listNguoiDungDonVi.Select(n => n.IIdMaDonVi).Contains(n.IIDMaDonVi)).ToList();
            }
            else
            {
                listUnit = new List<DonVi>();
            }

            NsDonViModelItems = _mapper.Map<ObservableCollection<DonViModel>>(listUnit);
            if (!string.IsNullOrEmpty(Model.Id_DonVi))
            {
                NsDonViModelItems.Where(x => x.IIDMaDonVi == Model.Id_DonVi).Select(x =>
                {
                    x.Selected = true;
                    return x;
                }).ToList();
            }
            _nsDonViModelsView = CollectionViewSource.GetDefaultView(NsDonViModelItems);
            _nsDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.Loai),
                ListSortDirection.Ascending));
            _nsDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.IIDMaDonVi),
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
            var item = (DonViModel)obj;
            var condition = item.TenDonVi.ToLower().Contains(SearchNsDonVi.Trim().ToLower()) ||
                            item.IIDMaDonVi.ToLower().Contains(SearchNsDonVi.Trim().ToLower());
            return condition;
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            // Reset defaule value
            SearchNsDonVi = string.Empty;
            // Trường hợp tạo mới
            if (Model.Id == Guid.Empty)
            {
                //Model = new PlanBeginYearModel()
                //{
                //    DNgayChungTu = DateTime.Now
                //};
                DNgayChungTu = DateTime.Now;
                SMoTa = "";
                GetSoChungTu();
            }
            else
            {
                DonViModel donViSelected = NsDonViModelItems.Where(n => n.IIDMaDonVi == Model.Id_DonVi).FirstOrDefault();
                if (donViSelected != null)
                {
                    donViSelected.Selected = true;
                }
                DNgayChungTu = Model.DNgayChungTu;
                SMoTa = Model.SMoTa;
                OnPropertyChanged(nameof(NsDonViModelItems));
            }
        }

        private void GetSoChungTu()
        {
            if (VoucherTypeSelected == null || Model.Id != Guid.Empty)
            {
                return;
            }
            int soChungTuIndex = _sktChungTuService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork,
                    _sessionService.Current.Budget, _sessionService.Current.YearOfBudget, int.Parse(VoucherTypeSelected.ValueItem));
            if (Model != null)
            {
                Model.SSoChungTu = "DTDN-" + soChungTuIndex.ToString("D3");
                OnPropertyChanged(nameof(Model.SSoChungTu));
            }
        }

        private void LoadVoucherTypes()
        {
            var voucherTypes = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = VoucherType.NSSD_Value, ValueItem = VoucherType.NSSD_Key},
                new ComboboxItem {DisplayItem = VoucherType.NSBD_Value, ValueItem = VoucherType.NSBD_Key},
            };
            VoucherTypes = new ObservableCollection<ComboboxItem>(voucherTypes);
        }

        private List<NsNguoiDungLns> GetListLNSByUser()
        {
            var predicate = PredicateBuilder.True<NsNguoiDungLns>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.SMaNguoiDung == _sessionService.Current.Principal);
            List<NsNguoiDungLns> listNguoiDungDonVi = _nsNguoiDungLNSService.FindAll(predicate).ToList();
            return listNguoiDungDonVi;
        }

        private bool ListLNSFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchLNS))
            {
                return true;
            }
            return obj is CheckBoxItem item && item.ValueItem.ToLower().Contains(_searchLNS.Trim()!.ToLower());
        }

        public void FindChildCheckbox(CheckBoxTreeItem parent)
        {
            ListLNS.Where(n => n.ParentId == parent.Id).Select(n => { n.IsChecked = parent.IsChecked; return n; }).ToList();
            if (ListLNS.Where(n => n.ParentId == parent.Id && n.IsChecked == !parent.IsChecked).ToList().Count == 0)
            {
                return;
            }
            else
            {
                foreach (CheckBoxTreeItem item in ListLNS.Where(n => n.ParentId == parent.Id))
                {
                    FindChildCheckbox(item);
                }
            }
        }

        public void LoadLNS()
        {
            int yearOfWork = _sessionService.Current.YearOfWork;
            string idDonVi = _sessionService.Current.IdDonVi;

            var predicate = _nSMucLucNganSachService.createPredicateAllNull();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            IEnumerable<NsMucLucNganSach> listLNS = _nSMucLucNganSachService.FindByCondition(predicate).OrderBy(n => n.XauNoiMa);
            List<NsNguoiDungLns> listLNSNguoiDung = GetListLNSByUser();
            List<string> listParentLNS = StringUtils.GetListXauNoiMaParent(listLNSNguoiDung.Select(n => n.SLns).ToList());
            listLNS = listLNS.Where(n => listParentLNS.Contains(n.Lns));

            if (VoucherTypeSelected != null && VoucherTypeSelected.ValueItem == VoucherType.NSSD_Key)
            {
                listLNS = listLNS.Where(n => !n.Lns.StartsWith("104"));
            }
            else if (VoucherTypeSelected != null && VoucherTypeSelected.ValueItem == VoucherType.NSBD_Key)
            {
                listLNS = listLNS.Where(n => n.Lns.StartsWith("104") || n.Lns == "1");
            }

            ListLNS = new ObservableCollection<CheckBoxTreeItem>();
            ListLNS = _mapper.Map<ObservableCollection<CheckBoxTreeItem>>(listLNS);
            // Filter
            _listLNSView = CollectionViewSource.GetDefaultView(ListLNS);
            _listLNSView.Filter = ListLNSFilter;
            foreach (CheckBoxTreeItem model in ListLNS)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(CheckBoxItem.IsChecked) && !Flag)
                    {
                        Flag = true;
                        SetCheckChildren(ListLNS, model);
                        SetCheckParent(ListLNS, model);
                        Flag = false;
                        //FindChildCheckbox(model);
                        OnPropertyChanged(nameof(SelectedCountLNS));
                        OnPropertyChanged(nameof(SelectAllLNS));
                    }
                };
            }
        }

        private void SetCheckChildren(ObservableCollection<CheckBoxTreeItem> items, CheckBoxTreeItem item)
        {
            foreach (var e in items)
            {
                if (e.ParentId == item.Id)
                {
                    e.IsChecked = item.IsChecked;
                    SetCheckChildren(items, e);
                }
            }
        }

        private void SetCheckParent(ObservableCollection<CheckBoxTreeItem> items, CheckBoxTreeItem item)
        {
            foreach (var e in items)
            {
                if (e.Id == item.ParentId)
                {
                    e.IsChecked = items.Where(x => x.ParentId == item.ParentId).All(x => x.IsChecked);
                    SetCheckParent(items, e);
                }
            }
        }

        public override void Init()
        {
            _selectAllLNS = false;
            base.Init();
            _sessionInfo = _sessionService.Current;
            //Sửa lại theo yêu câu danh sách Mục lục ngân sách luôn luôn hiển thị
            IsEnableView = true;
            IsEditMlns = true;
            LoadVoucherTypes();
            LoadNsDonVis();
            LoadLNS();
            if (Model != null && Model.Id != Guid.Empty)
            {
                VoucherTypeSelected = VoucherTypes.Where(n => n.ValueItem == Model.LoaiNganSach).FirstOrDefault();
                CheckboxSelectedToStringConvert.SetCheckboxSelected(ListLNS, Model.DsLNS);
            }
            else
            {
                VoucherTypeSelected = VoucherTypes.FirstOrDefault();
            }
            LoadData();
            if (Model.Id_DonVi == _sessionInfo.IdDonVi && !string.IsNullOrEmpty(Model.DSSoChungTuTongHop))
            {
                IsEnableView = false;
            }
            LoadBudgetSourceTypes();
            OnPropertyChanged(nameof(IsEnableView));
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsEditMlns));
            OnPropertyChanged(nameof(SelectAllLNS));
        }

        private void CreateDemandVoucherDetail(NsSktChungTuModel nsSktChungTuModel)
        {
            DemandVoucherDetailCriteria creation = new DemandVoucherDetailCriteria()
            {
                ListIdChungTuTongHop = string.Join(",", ListIdsSktChungTuSummary.Select(x => x.Id.ToString()).ToList()),
                IdChungTu = nsSktChungTuModel.Id.ToString(),
                IdDonVi = nsSktChungTuModel.IIdMaDonVi,
                TenDonVi = nsSktChungTuModel.STenDonVi,
                LoaiChungTu = nsSktChungTuModel.ILoaiChungTu.GetValueOrDefault(-1),
                NamLamViec = nsSktChungTuModel.INamLamViec,
                NamNganSach = nsSktChungTuModel.INamNganSach,
                NguonNganSach = nsSktChungTuModel.IIdMaNguonNganSach
            };
            _sktChungTuChiTietService.AddAggregate(creation);
        }
    }
}