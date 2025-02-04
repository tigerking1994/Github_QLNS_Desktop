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
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Helper;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagementPlan.MilitaryAdjustment
{
    public class MilitaryAdjustmentViewModel : GridViewModelBase<TlDieuChinhQsKeHoachModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ITlDmDonViService _tlDmDonViService;
        private readonly ITlDieuChinhQsKeHoachService _tlDieuChinhQsKeHoachService;
        private ICollectionView _dtDieuChinhQsKeHoach;
        private readonly ITlDmCanBoService _cadresService;
        private readonly ITlDmCapBacKeHoachService _tlDmCapBacKeHoachService;
        private readonly ITlDmPhuCapService _tlDmPhuCapService;
        private readonly ITlPhuCapDieuChinhService _tlPhuCapDieuChinhService;
        private readonly ITlQsKeHoachChiTietService _tlQsKeHoachChiTietService;

        public override string FuncCode => NSFunctionCode.SALARY_QUAN_LY_LUONG_KE_HOACH_DIEU_CHINH_QUAN_SO_INDEX;
        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Title => "Điều chỉnh quân số";
        public override string Name => "Điều chỉnh quân số năm kế hoạch";
        public override string Description => "Tổng số bản ghi: " + TlDieuChinhQsKeHoachItems.Count();
        public override PackIconKind IconKind => PackIconKind.ShieldCross;
        public override Type ContentType => typeof(View.Salary.SalaryManagementPlan.MilitaryAdjiustment.MilitaryAdjustmentIndex);

        private List<ComboboxItem> _year;
        public List<ComboboxItem> Year
        {
            get => _year;
            set => SetProperty(ref _year, value);
        }

        private ComboboxItem _yearSelectedItems;
        public ComboboxItem YearSelectedItems
        {
            get => _yearSelectedItems;
            set
            {
                if (SetProperty(ref _yearSelectedItems, value) && _dtDieuChinhQsKeHoach != null)
                {
                    _dtDieuChinhQsKeHoach.Refresh();
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
                if (SetProperty(ref _selectedDonViItems, value) && _dtDieuChinhQsKeHoach != null)
                {
                    _dtDieuChinhQsKeHoach.Refresh();
                }
            }
        }

        private List<ComboboxItem> _months;
        public List<ComboboxItem> Months
        {
            get => _months;
            set => SetProperty(ref _months, value);
        }

        private ComboboxItem _monthSelected;
        public ComboboxItem MonthSelected
        {
            get => _monthSelected;
            set => SetProperty(ref _monthSelected, value);
        }

        private ObservableCollection<TlDieuChinhQsKeHoachModel> _tlDieuChinhQsKeHoachItems;
        public ObservableCollection<TlDieuChinhQsKeHoachModel> TlDieuChinhQsKeHoachItems
        {
            get => _tlDieuChinhQsKeHoachItems;
            set
            {
                SetProperty(ref _tlDieuChinhQsKeHoachItems, value);
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        private TlDieuChinhQsKeHoachModel _seletedDieuChinhQsKeHoach;
        public TlDieuChinhQsKeHoachModel SeletedDieuChinhQsKeHoach
        {
            get => _seletedDieuChinhQsKeHoach;
            set
            {
                SetProperty(ref _seletedDieuChinhQsKeHoach, value);
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        private int currentRow = -1;
        public bool IsEnabled => SeletedDieuChinhQsKeHoach != null;
        public string ComboboxDisplayMemberPathDonVi => nameof(SelectedDonViItems.TenDonVi);
        public bool IsSaveData => TlDieuChinhQsKeHoachItems.Any(x => x.IsModified || x.IsDeleted);

        public MilitaryAdjustmentViewModel(
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            ITlDmDonViService tlDmDonViService,
            ITlDieuChinhQsKeHoachService tlDieuChinhQsKeHoachService,
            ITlDmCanBoService cadresService,
            ITlDmCapBacKeHoachService tlDmCapBacKeHoachService,
            ITlDmPhuCapService tlDmPhuCapService,
            ITlPhuCapDieuChinhService tlPhuCapDieuChinhService,
            ITlQsKeHoachChiTietService tlQsKeHoachChiTietService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;
            _tlDmDonViService = tlDmDonViService;
            _tlDieuChinhQsKeHoachService = tlDieuChinhQsKeHoachService;
            _cadresService = cadresService;
            _tlDmCapBacKeHoachService = tlDmCapBacKeHoachService;
            _tlDmPhuCapService = tlDmPhuCapService;
            _tlPhuCapDieuChinhService = tlPhuCapDieuChinhService;
            _tlQsKeHoachChiTietService = tlQsKeHoachChiTietService;
        }

        public override void Init()
        {
            base.Init();
            LoadDonVi();
            LoadYear();
            LoadMonths();
            LoadData();
        }

        public void LoadYear()
        {
            _year = new List<ComboboxItem>();
            for (int i = DateTime.Now.Year - 29; i <= DateTime.Now.Year + 29; i++)
            {
                ComboboxItem year = new ComboboxItem(i.ToString(), i.ToString());
                _year.Add(year);
            }
            var nam = _sessionService.Current.YearOfWork;
            OnPropertyChanged(nameof(Year));
            YearSelectedItems = _year.FirstOrDefault(x => x.ValueItem == nam.ToString());
        }

        public void LoadDonVi()
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

        public void LoadMonths()
        {
            _months = new List<ComboboxItem>();
            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem("Tháng " + i, i.ToString());
                _months.Add(month);
            }
            OnPropertyChanged(nameof(Months));
        }

        public void LoadData()
        {
            try
            {
                var data = _tlDieuChinhQsKeHoachService.FindAll().OrderBy(x => x.Thang).ToList();
                TlDieuChinhQsKeHoachItems = _mapper.Map<ObservableCollection<TlDieuChinhQsKeHoachModel>>(data);
                _dtDieuChinhQsKeHoach = CollectionViewSource.GetDefaultView(TlDieuChinhQsKeHoachItems);
                _dtDieuChinhQsKeHoach.Filter = DieuChinhQsKeHoachfilter;
                if (TlDieuChinhQsKeHoachItems.Count > 0)
                {
                    foreach (var item in TlDieuChinhQsKeHoachItems)
                    {
                        item.PropertyChanged += Detail_PropertyChanged;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void Detail_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            TlDieuChinhQsKeHoachModel tlDieuChinhQsKeHoachModel = (TlDieuChinhQsKeHoachModel)sender;
            var phuCap = _tlDmPhuCapService.FindAll().FirstOrDefault(x => x.MaPhuCap == "LCS");
            var phuCapKeHoach = _tlPhuCapDieuChinhService.FindAll().Where(x => x.IdPhuCap == phuCap.Id
                && ((DateTime)x.ApDungTu).Year <= tlDieuChinhQsKeHoachModel.Nam).OrderByDescending(x => x.ApDungTu).ToList();
            if (phuCapKeHoach.Count() != 0)
            {
                if (args.PropertyName == nameof(tlDieuChinhQsKeHoachModel.TangTuyenSinh))
                {
                    if (((DateTime)phuCapKeHoach.FirstOrDefault().ApDungTu).Year < tlDieuChinhQsKeHoachModel.Nam)
                    {
                        tlDieuChinhQsKeHoachModel.LuongTuyenSinh = (tlDieuChinhQsKeHoachModel.TangTuyenSinh ?? 0) * 0.4 * (double)(phuCapKeHoach[0].GiaTriMoi ?? 0);
                    }
                    else
                    {
                        var phuCapKeHoachThang = phuCapKeHoach.Where(x => ((DateTime)x.ApDungTu).Month <= tlDieuChinhQsKeHoachModel.Thang).ToList();
                        if (phuCapKeHoachThang.Count() == 0)
                        {
                            if (tlDieuChinhQsKeHoachModel.TangTuyenSinh != null)
                            {
                                tlDieuChinhQsKeHoachModel.LuongTuyenSinh = (tlDieuChinhQsKeHoachModel.TangTuyenSinh ?? 0) * 0.4 * (double)(phuCap.GiaTri ?? 0);
                            }
                        }
                        else
                        {
                            if (tlDieuChinhQsKeHoachModel.TangTuyenSinh != null)
                            {
                                tlDieuChinhQsKeHoachModel.LuongTuyenSinh = (tlDieuChinhQsKeHoachModel.TangTuyenSinh ?? 0) * 0.4 * (double)(phuCapKeHoachThang[0].GiaTriMoi);
                            }
                        }
                    }
                }
            }
            else if (args.PropertyName == nameof(tlDieuChinhQsKeHoachModel.TangTuyenSinh))
            {
                if (tlDieuChinhQsKeHoachModel.TangTuyenSinh != null)
                {
                    tlDieuChinhQsKeHoachModel.LuongTuyenSinh = (tlDieuChinhQsKeHoachModel.TangTuyenSinh ?? 0) * 0.4 * (double)(phuCap.GiaTri ?? 0);
                }
            }    
            tlDieuChinhQsKeHoachModel.IsModified = true;
            OnPropertyChanged(nameof(SeletedDieuChinhQsKeHoach));
            OnPropertyChanged(nameof(TlDieuChinhQsKeHoachItems));
        }

        private bool DieuChinhQsKeHoachfilter(object obj)
        {
            bool result = true;
            var item = (TlDieuChinhQsKeHoachModel)obj;
            if (YearSelectedItems != null)
            {
                result = result && item.Nam == int.Parse(YearSelectedItems.ValueItem);
            }
            if (SelectedDonViItems != null && !SelectedDonViItems.Id.Equals(Guid.Empty))
            {
                result = result && item.MaDonVi == SelectedDonViItems.MaDonVi;
            }
            return result;
        }

        protected override void OnAdd()
        {
            if (TlDieuChinhQsKeHoachItems.Count == 0 || SeletedDieuChinhQsKeHoach == null)
            {
                TlDieuChinhQsKeHoachModel tlDieuChinhQsKeHoachModel = new TlDieuChinhQsKeHoachModel();
                tlDieuChinhQsKeHoachModel.TenDonVi = SelectedDonViItems.TenDonVi;
                tlDieuChinhQsKeHoachModel.MaDonVi = SelectedDonViItems.MaDonVi;
                tlDieuChinhQsKeHoachModel.Nam = int.Parse(YearSelectedItems.ValueItem);
                tlDieuChinhQsKeHoachModel.PropertyChanged += Detail_PropertyChanged;
                TlDieuChinhQsKeHoachItems.Add(tlDieuChinhQsKeHoachModel);
            }
            else
            {
                TlDieuChinhQsKeHoachModel item = SeletedDieuChinhQsKeHoach;
                TlDieuChinhQsKeHoachModel target = ObjectCopier.Clone(item);
                currentRow = TlDieuChinhQsKeHoachItems.IndexOf(SeletedDieuChinhQsKeHoach);

                target.Id = Guid.Empty;
                target.IsModified = true;

                target.PropertyChanged += Detail_PropertyChanged;
                TlDieuChinhQsKeHoachItems.Insert(currentRow + 1, target);
            }
            OnPropertyChanged(nameof(TlDieuChinhQsKeHoachItems));
        }

        public override void OnSave()
        {
            List<TlDieuChinhQsKeHoachModel> listAdd = TlDieuChinhQsKeHoachItems.Where(x => x.IsModified && !x.IsDeleted && (x.Id == Guid.Empty || x.Id == null)).ToList();
            List<TlDieuChinhQsKeHoachModel> listEdit = TlDieuChinhQsKeHoachItems.Where(x => x.IsModified && !x.IsDeleted && x.Id != Guid.Empty && x.Id != null).ToList();
            List<TlDieuChinhQsKeHoachModel> listDelete = TlDieuChinhQsKeHoachItems.Where(x => x.IsDeleted && x.Id != Guid.Empty && x.Id != null).ToList();

            if (listAdd != null && listAdd.Count > 0)
            {
                var lstTlDieuChinhQsKeHoach = _mapper.Map<List<TlDieuChinhQsKeHoach>>(listAdd);
                string message = "";
                foreach (var item in listAdd)
                {
                    message = GetValidate(item);
                }
                if (!string.IsNullOrEmpty(message))
                {
                    System.Windows.Forms.MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _tlDieuChinhQsKeHoachService.AddRange(lstTlDieuChinhQsKeHoach);
                }
            }
            if (listEdit != null && listEdit.Count > 0)
            {
                listEdit.Select(x =>
                {
                    x.DNgaySua = DateTime.Now;
                    x.SNguoiSua = _sessionService.Current.Principal;
                    return x;
                }).ToList();
                var lstTlDieuChinhQsKeHoach = _mapper.Map<List<TlDieuChinhQsKeHoach>>(listEdit);
                _tlDieuChinhQsKeHoachService.UpdateRange(lstTlDieuChinhQsKeHoach);
                //string message = "";
                /*foreach (var item in listEdit)
                {
                    //message = GetValidate(item);
                }
                if (!string.IsNullOrEmpty(message))
                {
                    System.Windows.Forms.MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lstTlDieuChinhQsKeHoach = _mapper.Map<List<TlDieuChinhQsKeHoach>>(listEdit);
                    _tlDieuChinhQsKeHoachService.UpdateRange(lstTlDieuChinhQsKeHoach);
                }*/

            }
            if (listDelete != null && listDelete.Count > 0)
            {
                var lstTlDieuChinhQsKeHoach = _mapper.Map<List<TlDieuChinhQsKeHoach>>(listDelete);
                foreach (var item in lstTlDieuChinhQsKeHoach)
                {
                    var tlQsKhChiTiet = _tlQsKeHoachChiTietService.FindByCondition(item.MaDonVi, (int)item.Thang, (int)item.Nam);
                    if (tlQsKhChiTiet != null)
                    {
                        _tlQsKeHoachChiTietService.Delete(tlQsKhChiTiet.Id);
                    }
                    _tlDieuChinhQsKeHoachService.Delete(item.Id);
                }
            }
            OnRefresh();
        }

        protected override void OnDelete()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                if (TlDieuChinhQsKeHoachItems != null && TlDieuChinhQsKeHoachItems.Count > 0 && SeletedDieuChinhQsKeHoach != null)
                {
                    DialogResult dialogResult = MessageBox.Show(Resources.ConfirmDeleteUsers, Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        _tlDieuChinhQsKeHoachService.Delete(SeletedDieuChinhQsKeHoach.Id);
                        MessageBoxHelper.Info("Xóa dữ liệu thành công");
                    }
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

        protected override void OnRefresh()
        {
            LoadData();
        }

        private string GetValidate(TlDieuChinhQsKeHoachModel item)
        {
            var predicate = PredicateBuilder.True<TlDieuChinhQsKeHoach>();
            predicate = predicate.And(x => x.Thang == item.Thang);
            predicate = predicate.And(x => x.Nam == item.Nam);
            predicate = predicate.And(x => x.MaDonVi == item.MaDonVi);
            List<string> messages = new List<string>();
            if (_tlDieuChinhQsKeHoachService.FindByKeHoach(predicate) != null)
            {
                messages.Add(string.Format(Resources.MsgDieuChinhQsKeHoach, item.Thang, item.Nam, item.TenDonVi));
                goto End;
            }
        End:
            return string.Join(Environment.NewLine, messages);
        }
    }
}
