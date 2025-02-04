using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Command;
using System.Data;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Core.Domain.Query.Shared;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.Core.Domain.Query;
using System.IO;
using ControlzEx.Standard;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagementPlan.SalaryYearPlan
{
    public class SalaryYearPlanIndexViewModel : GridViewModelBase<TlDsBangLuongKeHoachModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        private ICollectionView _dtDanhSachBangLuongView;
        private readonly ITlDmDonViService _tlDmDonViService;
        private readonly INsDonViService _nsDonViService;
        private readonly ITlBangLuongKeHoachService _tlBangLuongKeHoachService;
        private readonly ITlDsBangLuongKeHoachService _tlDsBangLuongKeHoachService;
        private readonly ITlQtChungTuChiTietKeHoachService _tlQtChungTuChiTietKeHoachService;
        private readonly IExportService _exportService;

        public override string FuncCode => NSFunctionCode.SALARY_QUAN_LY_LUONG_KE_HOACH_BANG_LUONG_NAM_KH_INDEX;
        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Bảng lương năm kế hoạch";
        public override Type ContentType => typeof(View.Salary.SalaryManagementPlan.SalaryYearPlan.SalaryYearPlanIndex);
        public override PackIconKind IconKind => PackIconKind.ClipboardListOutline;
        public override string Title => "Bảng lương năm kế hoạch";
        public override string Description => "Bảng lương năm kế hoạch của đơn vị";

        private List<ComboboxItem> _years;
        public List<ComboboxItem> Years
        {
            get => _years;
            set => SetProperty(ref _years, value);
        }

        private ComboboxItem _yearSelected;
        public ComboboxItem YearSelected
        {
            get => _yearSelected;
            set
            {
                if (SetProperty(ref _yearSelected, value) && _dtDanhSachBangLuongView != null)
                {
                    _dtDanhSachBangLuongView.Refresh();
                }
            }
        }

        private ObservableCollection<TlDmDonViModel> _donViItems;
        public ObservableCollection<TlDmDonViModel> DonViItems
        {
            get => _donViItems;
            set => SetProperty(ref _donViItems, value);
        }

        private TlDmDonViModel _selectedDonViItems;
        public TlDmDonViModel SelectedDonViItems
        {
            get => _selectedDonViItems;
            set
            {
                if (SetProperty(ref _selectedDonViItems, value) && _dtDanhSachBangLuongView != null)
                {
                    _dtDanhSachBangLuongView.Refresh();
                }
            }
        }
        public bool? IsAllItemsSelected
        {
            get
            {
                if (!Items.IsEmpty())
                {
                    var selected = Items.Select(item => item.IsChecked).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    if (SelectedDonViItems.MaDonVi == null)
                    {
                        SelectAll(value.Value, Items.Where(x => x.Nam == int.Parse(YearSelected.ValueItem)));
                    }
                    else
                    {
                        SelectAll(value.Value, Items.Where(x => x.MaDonVi.Equals(SelectedDonViItems.MaDonVi) && x.Nam == int.Parse(YearSelected.ValueItem)));
                    }
                    OnPropertyChanged();
                }
            }
        }

        private void SelectAll(bool select, IEnumerable<TlDsBangLuongKeHoachModel> models)
        {
            foreach (var model in models)
            {
                model.IsChecked = select;
            }
        }

        public SalaryYearPlanDialogViewModel SalaryYearPlanDialogViewModel { get; }
        public SalaryYearPlanDetailViewModel SalaryYearPlanDetailViewModel { get; }
        public RelayCommand ExportBangLuongNamKHCommand { get; }

        public SalaryYearPlanIndexViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmDonViService tlDmDonViService,
            INsDonViService nsDonViService,
            ITlBangLuongKeHoachService tlBangLuongKeHoachService,
            ITlDsBangLuongKeHoachService tlDsBangLuongKeHoachService,
            ITlQtChungTuChiTietKeHoachService tlQtChungTuChiTietKeHoachService,
            IExportService exportService,
            SalaryYearPlanDialogViewModel salaryYearPlanDialogViewModel,
            SalaryYearPlanDetailViewModel salaryYearPlanDetailViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;
            _exportService = exportService;
            _tlDmDonViService = tlDmDonViService;
            _nsDonViService = nsDonViService;
            _tlBangLuongKeHoachService = tlBangLuongKeHoachService;
            _tlDsBangLuongKeHoachService = tlDsBangLuongKeHoachService;
            _tlQtChungTuChiTietKeHoachService = tlQtChungTuChiTietKeHoachService;

            SalaryYearPlanDialogViewModel = salaryYearPlanDialogViewModel;
            SalaryYearPlanDetailViewModel = salaryYearPlanDetailViewModel;
            ExportBangLuongNamKHCommand = new RelayCommand(o => OnExportBangLuongNamKH(ExportType.EXCEL));
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadYears();
            LoadDonViData();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                List<TlDsBangLuongKeHoach> data = new List<TlDsBangLuongKeHoach>();

                var _listDonVi = _nsDonViService.FindByCondition(n => n.NamLamViec == _sessionService.Current.YearOfWork && n.ITrangThai == 1).ToList();
                if (_listDonVi.Any(n => _sessionService.Current.IdsDonViQuanLy.Contains(n.IIDMaDonVi) && n.Loai == "0") || _sessionService.Current.Principal.Equals("admin"))
                {
                    data = _tlDsBangLuongKeHoachService.FindAll().ToList();
                }
                else
                {
                    data = _tlDsBangLuongKeHoachService.FindAll().Where(n => _sessionService.Current.IdsPhanHoQuanLy.Contains(n.MaDonVi)).ToList();
                }

                Items = _mapper.Map<ObservableCollection<TlDsBangLuongKeHoachModel>>(data);
                foreach (var item in Items)
                {
                    var donVi = _tlDmDonViService.FindByMaDonVi(item.MaDonVi);
                    if (donVi != null)
                    {
                        item.TenDonVi = donVi.TenDonVi;
                    }
                }
                _dtDanhSachBangLuongView = CollectionViewSource.GetDefaultView(Items);
                _dtDanhSachBangLuongView.Filter = BangLuongFilter;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadYears()
        {
            _years = new List<ComboboxItem>();
            for (int i = 1970; i <= 2050; i++)
            {
                ComboboxItem year = new ComboboxItem(i.ToString(), i.ToString());
                _years.Add(year);
            }
            var nam = _sessionService.Current.YearOfWork;
            OnPropertyChanged(nameof(Years));
            YearSelected = _years.FirstOrDefault(x => x.ValueItem == nam.ToString());
        }

        private void LoadDonViData()
        {
            var data = _tlDmDonViService.FindByCondition(x => x.ITrangThai.HasValue && (bool)x.ITrangThai);

            var lstDonVi = new List<TlDmDonViModel>();

            TlDmDonViModel tlDmDonViModel = new TlDmDonViModel();
            tlDmDonViModel.TenDonVi = "-- Tất cả --";
            tlDmDonViModel.Id = Guid.Empty;

            lstDonVi.Add(tlDmDonViModel);
            lstDonVi.AddRange(_mapper.Map<ObservableCollection<TlDmDonViModel>>(data).ToList());

            SelectedDonViItems = tlDmDonViModel;

            DonViItems = new ObservableCollection<TlDmDonViModel>(lstDonVi);
        }

        private bool BangLuongFilter(object obj)
        {
            var result = true;
            var item = (TlDsBangLuongKeHoachModel)obj;

            if (YearSelected != null)
            {
                result &= item.Nam == int.Parse(YearSelected.ValueItem);
            }
            if (SelectedDonViItems != null && !SelectedDonViItems.Id.Equals(Guid.Empty))
            {
                result &= item.MaDonVi == SelectedDonViItems.MaDonVi;
            }

            return result;
        }

        protected override void OnAdd()
        {
            base.OnAdd();
            TlDsBangLuongKeHoachModel tlDsBangLuongKeHoachModel = new TlDsBangLuongKeHoachModel();
            tlDsBangLuongKeHoachModel.Nam = int.Parse(YearSelected.ValueItem);
            tlDsBangLuongKeHoachModel.MaDonVi = SelectedDonViItems.MaDonVi;
            SalaryYearPlanDialogViewModel.Model = tlDsBangLuongKeHoachModel;
            SalaryYearPlanDialogViewModel.SavedAction = obj => {
                this.OnRefresh();
            };
            SalaryYearPlanDialogViewModel.Init();
            SalaryYearPlanDialogViewModel.ShowDialogHost();
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        protected override void OnDelete()
        {
            base.OnDelete();
            BackgroundWorkerHelper.Run((s, e) =>
            {
                DialogResult dialogResult = MessageBox.Show(Resources.MsgConfirmDeleteBangLuong, Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    IsLoading = true;
                    _tlBangLuongKeHoachService.DeleteByParentId(SelectedItem.Id);
                    _tlDsBangLuongKeHoachService.Delete(SelectedItem.Id);
                    _tlQtChungTuChiTietKeHoachService.DeleteByNamAndMaDonVi(SelectedItem.MaDonVi, SelectedItem.Nam);
                    MessageBoxHelper.Info("Xóa dữ liệu thành công");
                    OnRefresh();
                }
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    OnRefresh();
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OnOpenChiTietBangLuong((TlDsBangLuongKeHoachModel)obj);
        }

        private void OnOpenChiTietBangLuong(TlDsBangLuongKeHoachModel obj)
        {
            try
            {
                if (obj == null)
                    return;
                SalaryYearPlanDetailViewModel.Model = obj;
                SalaryYearPlanDetailViewModel.Init();
                SalaryYearPlanDetailViewModel.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnExportBangLuongNamKH(ExportType exportType)
        {
            if (!Items.Any(x => x.IsChecked && x.Nam == int.Parse(YearSelected?.ValueItem ?? _sessionService.Current.YearOfWork.ToString())))
            {
                MessageBoxHelper.Info("Vui lòng chọn bảng lương!");
                return;
            }
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    List<string> lstMaCanBo = new List<string>()
                    {
                        BhxhMLNS.LUONG_CHINH, BhxhMLNS.PHU_CAP_CHUC_VU, BhxhMLNS.PHU_CAP_TNN, BhxhMLNS.PHU_CAP_TNVK, BhxhMLNS.PhuCapHSBL
                    };

                    string sChungTuIds = string.Join(StringUtils.COMMA, Items.Where(x => x.IsChecked).Select(s => s.Id));
                    var dataExport = _tlBangLuongKeHoachService.ExportQuyLuongCanCu(_sessionInfo.YearOfWork, lstMaCanBo, sChungTuIds);
                    IsLoading = true;
                    CalculateDataExport(dataExport);
                    string templateFileName = "", fileNamePrefix = "";
                    templateFileName = GetTemplate();
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(1, exportType);
                    CurrencyToText currencyToText = new CurrencyToText();
                    data.Add("Items", dataExport);
                    data.Add("DONVITINH", 1);
                    data.Add("FormatNumber", formatNumber);
                    string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"));
                    var xlsFile = _exportService.Export<TlBangLuongKeHoachExportQuery>(templateFileName, data);
                    e.Result = new ExportResult(filename, filename, null, xlsFile);

                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CalculateDataExport(IEnumerable<TlBangLuongKeHoachExportQuery> listData)
        {
            // calculate parent tier 0,1
            var dataTier1 = listData.Where(x => x.ILevel == 1);
            var dataTier0 = listData.Where(x => x.ILevel == 0);
            dataTier1.ForAll(f =>
            {
                f.LHT_TT = listData.Where(x => x.ILevel == 2 && x.IIdParent.Equals(f.Id)).Sum(s => s.LHT_TT);
                f.PCCV_TT = listData.Where(x => x.ILevel == 2 && x.IIdParent.Equals(f.Id)).Sum(s => s.PCCV_TT);
                f.PCTN_TT = listData.Where(x => x.ILevel == 2 && x.IIdParent.Equals(f.Id)).Sum(s => s.PCTN_TT);
                f.PCTNVK_TT = listData.Where(x => x.ILevel == 2 && x.IIdParent.Equals(f.Id)).Sum(s => s.PCTNVK_TT);
                f.HSBL_TT = listData.Where(x => x.ILevel == 2 && x.IIdParent.Equals(f.Id)).Sum(s => s.HSBL_TT);
                f.QSBQ = listData.Where(x => x.ILevel == 2 && x.IIdParent.Equals(f.Id)).Sum(s => s.QSBQ);
            });

            dataTier0.ForAll(f =>
            {
                f.LHT_TT = dataTier1.Where(x => x.IIdParent.Equals(f.Id)).Sum(s => s.LHT_TT);
                f.PCCV_TT = dataTier1.Where(x => x.IIdParent.Equals(f.Id)).Sum(s => s.PCCV_TT);
                f.PCTN_TT = dataTier1.Where(x => x.IIdParent.Equals(f.Id)).Sum(s => s.PCTN_TT);
                f.PCTNVK_TT = dataTier1.Where(x => x.IIdParent.Equals(f.Id)).Sum(s => s.PCTNVK_TT);
                f.HSBL_TT = dataTier1.Where(x => x.IIdParent.Equals(f.Id)).Sum(s => s.HSBL_TT);
                f.QSBQ = dataTier1.Where(x => x.IIdParent.Equals(f.Id)).Sum(s => s.QSBQ);
            });
        }

        private string GetTemplate()
        {
            return Path.Combine(ExportPrefix.PATH_TL_LUONG, Path.GetFileNameWithoutExtension(ExportFileName.RPT_TL_QUY_LUONG_CAN_CU) + FileExtensionFormats.Xlsx);

        }
    }
}
