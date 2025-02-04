using AutoMapper;
using ICSharpCode.AvalonEdit.Document;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using System.Text.RegularExpressions;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagement.NewCalculateSalary
{
    public class NewCalculateSalaryDialogViewModel : DialogViewModelBase<TlCachTinhLuongNq104Model>
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly ITlDmPhuCapNq104Service _phuCapService;
        private readonly ITlDmCachTinhLuongChuanNq104Service _tlDmCachTinhLuongChuanService;
        private readonly ITlDmCachTinhLuongTruyLinhNq104Service _tlDmCachTinhLuongTruyLinhService;
        private readonly ITlDmThemCachTinhLuongService _themCachTinhLuongService;
        private readonly ITlDmCachTinhLuongBaoHiemNq104Service _tlDmCachTinhLuongBaoHiemService;
        private ICollectionView _phuCapItemsView;

        public override string FuncCode => NSFunctionCode.NEW_SALARY_MANAGEMENT_CALCULATE_SALARY_DIALOG;

        public override Type ContentType => typeof(View.NewSalary.NewSalaryManagement.NewCalculateSalary.NewCalculateSalaryDialog);
        public override string Title => Guid.Empty.Equals(Model.Id) ? "THÊM MỚI CHỈ TIÊU TÍNH LƯƠNG" : "CẬP NHẬT CHỈ TIÊU TÍNH LƯƠNG";
        public override string Description => Guid.Empty.Equals(Model.Id) ? "Thêm mới chỉ tiêu tính lương" : "Cập nhật chỉ tiêu tính lương";
        public bool IsEnabled => ViewState == FormViewState.ADD;
        public string BeforeChanged { get; set; }
        public bool IsSaveData { get; set; }
        public bool IsReadOnly => ViewState == FormViewState.DETAIL;

        private FormViewState _viewState;
        public FormViewState ViewState
        {
            get => _viewState;
            set
            {
                SetProperty(ref _viewState, value);
                OnPropertyChanged(nameof(IsReadOnly));
            }
        }

        private string _searchPhuCap;
        public string SearchPhuCap
        {
            get => _searchPhuCap;
            set
            {
                if (SetProperty(ref _searchPhuCap, value) && _phuCapItemsView != null)
                {
                    _phuCapItemsView.Refresh();
                }
            }
        }

        private ObservableCollection<AllowenceNq104Model> _phuCapItems;
        public ObservableCollection<AllowenceNq104Model> PhuCapItems
        {
            get => _phuCapItems;
            set => SetProperty(ref _phuCapItems, value);
        }

        private AllowenceNq104Model _selectedPhuCap;
        public AllowenceNq104Model SelectedPhuCap
        {
            get => _selectedPhuCap;
            set
            {
                SetProperty(ref _selectedPhuCap, value);
                OnPropertyChanged(nameof(MaCot));
                OnPropertyChanged(nameof(TenCot));
            }
        }

        private ObservableCollection<TlDmThemCachTinhLuongModel> _cachTinhLuongItems;
        public ObservableCollection<TlDmThemCachTinhLuongModel> CachTinhLuongItems
        {
            get => _cachTinhLuongItems;
            set => SetProperty(ref _cachTinhLuongItems, value);
        }

        private TlDmThemCachTinhLuongModel _selectedCachTinhLuongItem;
        public TlDmThemCachTinhLuongModel SelectedCachTinhLuong
        {
            get => _selectedCachTinhLuongItem;
            set
            {
                SetProperty(ref _selectedCachTinhLuongItem, value);
            }
        }

        private IEnumerable<string> _suggestionWords;
        public IEnumerable<string> SuggestionWords
        {
            get => _suggestionWords;
            set => SetProperty(ref _suggestionWords, value);
        }

        public string MaCot => SelectedPhuCap != null ? SelectedPhuCap.MaPhuCap : string.Empty;
        public string TenCot => SelectedPhuCap != null ? SelectedPhuCap.TenPhuCap : string.Empty;
        public TextDocument CongThucDocument { get; set; }

        public NewCalculateSalaryDialogViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmPhuCapNq104Service tlDmPhuCapService,
            ITlDmCachTinhLuongChuanNq104Service tlDmCachTinhLuongChuanService,
            ITlDmCachTinhLuongTruyLinhNq104Service tlDmCachTinhLuongTruyLinhService,
            ITlDmThemCachTinhLuongService tlDmThemCachTinhLuongService,
            ITlDmCachTinhLuongBaoHiemNq104Service tlDmCachTinhLuongBaoHiemService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;

            _phuCapService = tlDmPhuCapService;
            _tlDmCachTinhLuongChuanService = tlDmCachTinhLuongChuanService;
            _tlDmCachTinhLuongTruyLinhService = tlDmCachTinhLuongTruyLinhService;
            _themCachTinhLuongService = tlDmThemCachTinhLuongService;
            _tlDmCachTinhLuongBaoHiemService = tlDmCachTinhLuongBaoHiemService;
        }

        public override void Init()
        {
            try
            {
                MarginRequirement = new Thickness(10);
                IsSaveData = true;
                CongThucDocument = new TextDocument();
                LoadPhuCap();
                LoadThemCachTinhLuong();
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadPhuCap()
        {
            try
            {
                //var data = _phuCapService.FindAll();
                var dataQuery = _phuCapService.FindAllPhuCapVaCheDoBHXH();
                var data = _mapper.Map<IEnumerable<TlDmPhuCapNq104>>(dataQuery);

                var dataSuggest = new ObservableCollection<AllowenceNq104Model>();
                if (data != null)
                    dataSuggest = new ObservableCollection<AllowenceNq104Model>(_mapper.Map<ObservableCollection<AllowenceNq104Model>>(data.Clone()));
                if (data != null && data.Any(n => (n.IsFormula ?? false)))
                    data = data.Where(n => (n.IsFormula ?? false)).OrderBy(x => x.MaPhuCap);
                PhuCapItems = _mapper.Map<ObservableCollection<AllowenceNq104Model>>(data);
                _phuCapItemsView = CollectionViewSource.GetDefaultView(PhuCapItems);
                _phuCapItemsView.Filter = ListPhuCapFilter;

                // Init SuggestionWords
                SuggestionWords = dataSuggest.Select(x => x.MaPhuCap);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadThemCachTinhLuong()
        {
            try
            {
                IEnumerable<TlDmThemCachTinhLuong> dataCachTinhLuong = _themCachTinhLuongService.FindAll();
                CachTinhLuongItems = _mapper.Map<ObservableCollection<TlDmThemCachTinhLuongModel>>(dataCachTinhLuong);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            CongThucDocument = new TextDocument();
            if (Model != null && !Model.Id.IsNullOrEmpty())
            {
                // If edit then set CongThucDocument value
                CongThucDocument.Text = Model.CongThuc;

                if (PhuCapItems != null && PhuCapItems.Count > 0)
                {
                    SelectedPhuCap = PhuCapItems.FirstOrDefault(x => x.MaPhuCap.Equals(Model.MaCot));
                }

                if (CachTinhLuongItems != null && CachTinhLuongItems.Count > 0)
                {
                    SelectedCachTinhLuong = CachTinhLuongItems.FirstOrDefault(x => x.MaThemCachTl.Equals(Model.MaCachTl));
                }
            }
        }

        public override void OnSave(object obj)
        {
            // Validate
            string message = GetMessageValidate();
            if (!string.IsNullOrEmpty(message))
            {
                System.Windows.Forms.MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    // Convert model
                    Model.MaCot = MaCot;
                    Model.TenCot = TenCot;
                    Model.CongThuc = CongThucDocument.Text;
                    Model.MaCachTl = SelectedCachTinhLuong.MaThemCachTl;

                    // Lương chuẩn
                    if (CachTinhLuong.CACH0.Equals(SelectedCachTinhLuong.MaThemCachTl))
                    {
                        TlDmCachTinhLuongChuanNq104 tlDmCachTinhLuongChuan;
                        if (Model.Id.IsNullOrEmpty())
                        {
                            tlDmCachTinhLuongChuan = new TlDmCachTinhLuongChuanNq104();
                            _mapper.Map(Model, tlDmCachTinhLuongChuan);
                            _tlDmCachTinhLuongChuanService.Add(tlDmCachTinhLuongChuan);
                        }
                        else
                        {
                            tlDmCachTinhLuongChuan = _tlDmCachTinhLuongChuanService.Find(Model.Id);
                            _mapper.Map(Model, tlDmCachTinhLuongChuan);
                            _tlDmCachTinhLuongChuanService.Update(tlDmCachTinhLuongChuan);
                        }
                    }

                    // Truy lĩnh
                    if (CachTinhLuong.CACH5.Equals(SelectedCachTinhLuong.MaThemCachTl))
                    {
                        TlDmCachTinhLuongTruyLinhNq104 tlDmCachTinhLuongTruyLinh;
                        if (Model.Id.IsNullOrEmpty())
                        {
                            tlDmCachTinhLuongTruyLinh = new TlDmCachTinhLuongTruyLinhNq104();
                            _mapper.Map(Model, tlDmCachTinhLuongTruyLinh);
                            _tlDmCachTinhLuongTruyLinhService.Add(tlDmCachTinhLuongTruyLinh);
                        }
                        else
                        {
                            tlDmCachTinhLuongTruyLinh = _tlDmCachTinhLuongTruyLinhService.Find(Model.Id);
                            _mapper.Map(Model, tlDmCachTinhLuongTruyLinh);
                            _tlDmCachTinhLuongTruyLinhService.Update(tlDmCachTinhLuongTruyLinh);
                        }
                    }

                    // Bảo hiểm
                    if (CachTinhLuong.CACH2.Equals(SelectedCachTinhLuong.MaThemCachTl))
                    {
                        TlDmCachTinhLuongBaoHiemNq104 tlDmCachTinhLuongBaoHiem;
                        if (Model.Id.IsNullOrEmpty())
                        {
                            tlDmCachTinhLuongBaoHiem = new TlDmCachTinhLuongBaoHiemNq104();
                            _mapper.Map(Model, tlDmCachTinhLuongBaoHiem);
                            _tlDmCachTinhLuongBaoHiemService.Add(tlDmCachTinhLuongBaoHiem);
                        }
                        else
                        {
                            tlDmCachTinhLuongBaoHiem = _tlDmCachTinhLuongBaoHiemService.Find(Model.Id);
                            _mapper.Map(Model, tlDmCachTinhLuongBaoHiem);
                            _tlDmCachTinhLuongBaoHiemService.Update(tlDmCachTinhLuongBaoHiem);
                        }
                    }
                    MessageBoxHelper.Info(Resources.MsgSaveDone);
                    SavedAction?.Invoke(null);
                    if (obj is Window window)
                    {
                        window.Close();
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message, ex);
                }
            }
        }

        private bool ListPhuCapFilter(object obj)
        {
            if (string.IsNullOrEmpty(_searchPhuCap)) return true;
            return obj is AllowenceNq104Model item && (item.TenPhuCap.ToLower().Contains(SearchPhuCap.Trim().ToLower()) ||
                item.MaPhuCap.ToLower().Contains(SearchPhuCap.Trim().ToLower()));
        }

        private string GetMessageValidate()
        {
            List<string> messages = new List<string>();
            string congThucLuong = CongThucDocument.Text;

            if (SelectedCachTinhLuong == null)
            {
                messages.Add(string.Format(Resources.PayrollNull));
            }

            if (SelectedPhuCap == null || Guid.Empty.Equals(SelectedPhuCap.Id))
            {
                messages.Add(string.Format(Resources.TargetNull));
            }

            if (string.IsNullOrEmpty(congThucLuong))
            {
                messages.Add(string.Format(Resources.FormulaNull));
            }
            else
            {
                congThucLuong = congThucLuong.ToUpper();
                CongThucDocument.Text = congThucLuong;
            }

            if (ViewState == FormViewState.ADD)
            {
                if (SelectedCachTinhLuong != null && CachTinhLuong.CACH0.Equals(SelectedCachTinhLuong.MaThemCachTl) && !Guid.Empty.Equals(SelectedPhuCap.Id))
                {
                    var model = _tlDmCachTinhLuongChuanService.FindByMaCot(SelectedPhuCap.MaPhuCap);
                    if (model != null)
                    {
                        messages.Add(string.Format(Resources.TargetExists));
                    }
                }
                else if (SelectedCachTinhLuong != null && CachTinhLuong.CACH5.Equals(SelectedCachTinhLuong.MaThemCachTl) && !Guid.Empty.Equals(SelectedPhuCap.Id))
                {
                    var model = _tlDmCachTinhLuongTruyLinhService.FindByMaCot(SelectedPhuCap.MaPhuCap);
                    if (model != null)
                    {
                        messages.Add(string.Format(Resources.TargetExists));
                    }
                }
                else if (SelectedCachTinhLuong != null && CachTinhLuong.CACH2.Equals(SelectedCachTinhLuong.MaThemCachTl) && !Guid.Empty.Equals(SelectedPhuCap.Id))
                {
                    var model = _tlDmCachTinhLuongBaoHiemService.FindByMaCot(SelectedPhuCap.MaPhuCap);
                    if (model != null)
                    {
                        messages.Add("Chỉ tiêu đã tồn tại!");
                    }
                }
            }

            if (ViewState == FormViewState.UPDATE)
            {
                if (!Guid.Empty.Equals(SelectedPhuCap.Id) && !SelectedPhuCap.MaPhuCap.Equals(BeforeChanged) && CachTinhLuong.CACH0.Equals(SelectedCachTinhLuong.MaThemCachTl))
                {
                    var model = _tlDmCachTinhLuongChuanService.FindByMaCot(SelectedPhuCap.MaPhuCap);
                    if (model != null)
                    {
                        messages.Add(string.Format(Resources.TargetExists));
                    }
                }
                else if (!Guid.Empty.Equals(SelectedPhuCap.Id) && !SelectedPhuCap.MaPhuCap.Equals(BeforeChanged) && CachTinhLuong.CACH5.Equals(SelectedCachTinhLuong.MaThemCachTl))
                {
                    var model = _tlDmCachTinhLuongTruyLinhService.FindByMaCot(SelectedPhuCap.MaPhuCap);
                    if (model != null)
                    {
                        messages.Add(string.Format(Resources.TargetExists));
                    }
                }
                else if (!Guid.Empty.Equals(SelectedPhuCap.Id) && !SelectedPhuCap.MaPhuCap.Equals(BeforeChanged) && CachTinhLuong.CACH2.Equals(SelectedCachTinhLuong.MaThemCachTl))
                {
                    var model = _tlDmCachTinhLuongBaoHiemService.FindByMaCot(SelectedPhuCap.MaPhuCap);
                    if (model != null)
                    {
                        messages.Add("Chỉ tiêu đã tồn tại!");
                    }
                }
            }

            if (!string.IsNullOrEmpty(congThucLuong) && !string.IsNullOrEmpty(SelectedPhuCap.MaPhuCap))
            {
                List<string> phucap = congThucLuong.Split(StringUtils.SPLITCHARS, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (phucap.Contains(SelectedPhuCap.MaPhuCap))
                {
                    messages.Add(string.Format(Resources.AlertNotContainsMaChiTieu));
                }
            }

            if (!string.IsNullOrEmpty(congThucLuong))
            {
                List<string> phucap = congThucLuong.Split(StringUtils.SPLITCHARS, StringSplitOptions.RemoveEmptyEntries).ToList();
                Random random = new Random();
                var value = new Dictionary<string, object>();
                int index = -1;
                foreach (var item in phucap)
                {
                    double number;
                    if (!double.TryParse(item, out number))
                    {
                        index = SuggestionWords.ToList().FindIndex(x => x.Equals(item));
                        if (index == -1)
                        {
                            messages.Add(string.Format(Resources.TargetNotExists) + item);
                            return string.Join(Environment.NewLine, messages);
                        }
                        if (!value.ContainsKey(item))
                        {
                            value.Add(item, random.Next(0, 20));
                        }
                    }
                }
                try
                {
                    EvalExtensions.Execute(congThucLuong, value);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message, ex);
                    messages.Add(string.Format(Resources.FormulaError));
                }
            }
            return string.Join(Environment.NewLine, messages);
        }
    }
}