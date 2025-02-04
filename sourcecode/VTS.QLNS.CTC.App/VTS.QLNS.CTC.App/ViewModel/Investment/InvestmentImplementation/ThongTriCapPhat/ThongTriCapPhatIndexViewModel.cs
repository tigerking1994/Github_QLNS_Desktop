using AutoMapper;
using FlexCel.Report;
using FlexCel.XlsAdapter;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.Investment.InvestmentImplementation.ThongTriCapPhat;
using VTS.QLNS.CTC.App.View.Investment.InvestmentImplementation.ThongTriCapPhat.PrintDialog;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.ThongTriCapPhat.PrintDialog;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.ThongTriCapPhat
{
    public class ThongTriCapPhatIndexViewModel : GridViewModelBase<VdtThongTriModel>
    {
        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_THONG_TRI_CAP_PHAT_INDEX;
        //public override string GroupName => MenuItemContants.GROUP_ALLOCATION;
        public override string Name => "Thông tri";
        public override string Description => "Danh sách thông tin thông tri";
        public bool IsEdit => SelectedItem != null && SelectedItem.Id != Guid.Empty;
        public override Type ContentType => typeof(View.Investment.InvestmentImplementation.ThongTriCapPhat.ThongTriCapPhatIndex);

        #region Private
        private readonly IVdtDmLoaiCongTrinhService _loaicongtrinhService;
        private readonly ISessionService _sessionService;
        private readonly IVdtThongTriService _thongTriService;
        private readonly INsDonViService _nsDonViService;
        private ICollectionView _thongTriView;
        private readonly IExportService _exportService;
        private readonly IDanhMucService _danhMucService;
        private IMapper _mapper;
        private readonly ILog _logger;
        private Dictionary<Guid, VdtDmLoaiCongTrinh> _dicLoaiCongTrinh;
        #endregion

        #region declare RelayCommand
        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; }
        public RelayCommand PrintReportCommand { get; }
        public RelayCommand ExportCommand { get; }
        #endregion

        #region Componer
        private string _sMaThongTri;
        public string SMaThongTri
        {
            get => _sMaThongTri;
            set => SetProperty(ref _sMaThongTri, value);
        }

        private string _sMoTa;
        public string SMoTa
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }

        private string _iNamThongTri;
        public string iNamThongTri
        {
            get => _iNamThongTri;
            set
            {
                SetProperty(ref _iNamThongTri, value);
                OnSearch();
            }
        }

        private DateTime? _dNgayThongTriFrom;
        public DateTime? DNgayThongTriFrom
        {
            get => _dNgayThongTriFrom;
            set
            {
                SetProperty(ref _dNgayThongTriFrom, value);
                OnSearch();
            }
        }

        private DateTime? _dNgayThongTriTo;
        public DateTime? DNgayThongTriTo
        {
            get => _dNgayThongTriTo;
            set
            {
                SetProperty(ref _dNgayThongTriTo, value);
                OnSearch();
            }
        }

        private ObservableCollection<ComboboxItem> _drpDonViQuanLy;
        public ObservableCollection<ComboboxItem> DrpDonViQuanLy
        {
            get => _drpDonViQuanLy;
            set => SetProperty(ref _drpDonViQuanLy, value);
        }

        private ComboboxItem _drpDonViQuanLySelected;
        public ComboboxItem DrpDonViQuanLySelected
        {
            get => _drpDonViQuanLySelected;
            set
            {
                SetProperty(ref _drpDonViQuanLySelected, value);
                OnSearch();
            }
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiThongTri;
        public ObservableCollection<ComboboxItem> ItemsLoaiThongTri
        {
            get => _itemsLoaiThongTri;
            set => SetProperty(ref _itemsLoaiThongTri, value);
        }

        private ComboboxItem _selectedLoaiThongTri;
        public ComboboxItem SelectedLoaiThongTri
        {
            get => _selectedLoaiThongTri;
            set => SetProperty(ref _selectedLoaiThongTri, value);
        }
        #endregion

        public ThongTriCapPhatDialogViewModel ThongTriCapPhatDialogViewModel { get; set; }
        public ThongTriCapPhatDetailViewModel ThongTriCapPhatDetailViewModel { get; set; }
        public ThongTriCapPhatPrintDialogViewModel ThongTriCapPhatPrintDialogViewModel { get; set; }

        public ThongTriCapPhatIndexViewModel(
            ThongTriCapPhatDialogViewModel thongTriCapPhatDialogViewModel,
            ThongTriCapPhatDetailViewModel thongTriCapPhatDetailViewModel,
            ThongTriCapPhatPrintDialogViewModel thongTriCapPhatPrintDialogViewModel,
            IVdtThongTriService thongTriService,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IVdtDmLoaiCongTrinhService loaicongtrinhService,
            IExportService exportService,
            ILog logger,
            IDanhMucService danhMucService,
            IMapper mapper)
        {
            ThongTriCapPhatDialogViewModel = thongTriCapPhatDialogViewModel;
            ThongTriCapPhatDialogViewModel.ParentPage = this;
            ThongTriCapPhatDetailViewModel = thongTriCapPhatDetailViewModel;
            ThongTriCapPhatDetailViewModel.ParentPage = this;
            ThongTriCapPhatPrintDialogViewModel = thongTriCapPhatPrintDialogViewModel;
            ThongTriCapPhatPrintDialogViewModel.ParentPage = this;
            _thongTriService = thongTriService;
            _loaicongtrinhService = loaicongtrinhService;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _danhMucService = danhMucService;
            _exportService = exportService;
            _mapper = mapper;
            _logger = logger;
            SearchCommand = new RelayCommand(obj => OnSearch());
            ResetFilterCommand = new RelayCommand(obj => onResetFilter());
            ExportCommand = new RelayCommand(obj => OnExport());
            PrintReportCommand = new RelayCommand(obj => OnPrintReport());
        }

        #region RelayCommand Event
        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(10);
            GetDonViQuanLy();
            LoadLoaiCongTrinh();
            LoadLoaiThongTri();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            Guid iIdLoaiThongTri = _thongTriService.GetAllDmLoaiThongTri().FirstOrDefault(n => n.IKieuLoaiThongTri == (int)LoaiThongTri.THONG_TRI_THANH_TOAN).Id;
            List<VdtThongTriQuery> listChungTu = _thongTriService.GetVdtThongTriIndex(iIdLoaiThongTri, OPEN_FROM_PHEDUYETTHANHTOAN.THONGTRI).ToList();
            var lstItem = _mapper.Map<List<VdtThongTriModel>>(listChungTu);
            lstItem = lstItem.Select(n => { n.iRowIndex = lstItem.IndexOf(n) + 1; return n; }).ToList();
            Items = _mapper.Map<ObservableCollection<VdtThongTriModel>>(lstItem);
            _thongTriView = CollectionViewSource.GetDefaultView(Items);
            _thongTriView.Filter = VdtTtDeNghiThanhToanFilter;
            if (Items != null && Items.Count > 0)
            {
                SelectedItem = Items.FirstOrDefault();
            }
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        public void OnSearch()
        {
            _thongTriView.Refresh();
        }

        protected override void OnAdd()
        {
            ThongTriCapPhatDialogViewModel.Model = new VdtThongTriModel();
            ThongTriCapPhatDialogViewModel.Init();
            ThongTriCapPhatDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenThongTriCapPhatDetail(_mapper.Map<VdtThongTriModel>(obj), false);
            };
            ThongTriCapPhatDialogViewModel.isOpenedFromThongTriCapPhat = true;
            var view = new ThongTriCapPhatDialog
            {
                DataContext = ThongTriCapPhatDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        private void OnPrintReport()
        {
            if (Items == null || !Items.Any(n => n.IsChecked)) return;
            ThongTriCapPhatPrintDialogViewModel.VdtThongTriModels = Items.Where(n => n.IsChecked).ToList();
            ThongTriCapPhatPrintDialogViewModel.Init();
            var view = new ThongTriCapPhatPrintDialog
            {
                DataContext = ThongTriCapPhatPrintDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        protected override void OnUpdate()
        {
            if (!CheckCanSuaXoa())
            {
                MessageBoxHelper.Info(string.Format(Resources.MsgRoleUpdate, SelectedItem.sUserCreate));
                return;
            }
            ThongTriCapPhatDialogViewModel.Model = SelectedItem;
            ThongTriCapPhatDialogViewModel.Init();
            ThongTriCapPhatDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenThongTriCapPhatDetail(_mapper.Map<VdtThongTriModel>(obj), false);
            };
            var view = new ThongTriCapPhatDialog
            {
                DataContext = ThongTriCapPhatDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        protected override void OnDelete()
        {
            if (!CheckCanSuaXoa())
            {
                MessageBoxHelper.Error(string.Format(Resources.MsgRoleDelete, SelectedItem.sUserCreate));
                return;
            }
            base.OnDelete();
            StringBuilder messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(Resources.MsgConfirmDeleteThongTriCapPhat, SelectedItem.sMaThongTri);
            var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo, DeleteEventHandler);
            DialogHost.Show(messageBox.Content, "RootDialog");
        }

        private bool CheckCanSuaXoa()
        {
            var user = _sessionService.Current.Principal;

            if (user == SelectedItem.sUserCreate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void onResetFilter()
        {
            SMaThongTri = null;
            iNamThongTri = null;
            SMoTa = null;
            DNgayThongTriFrom = null;
            DNgayThongTriTo = null;
            DrpDonViQuanLySelected = null;
            SelectedLoaiThongTri = null;
            OnSearch();
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEdit));
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            var data = (VdtThongTriModel)obj;
            OnOpenThongTriCapPhatDetail(data, true);
        }

        private void OnExport()
        {
            if (Items == null || !Items.Any(n => n.IsChecked))
            {
                MessageBox.Show(Resources.VoucherExportEmpty);
                return;
            }
            ExporThongTriThanhToan(Items.Where(n => n.IsChecked).ToList(), ExportType.EXCEL);
        }

        private void ExporThongTriThanhToan(List<VdtThongTriModel> items, ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    foreach (VdtThongTriModel item in items)
                    {
                        string templateFileName = string.Empty;
                        string fileNamePrefix = string.Empty;

                        //var lstThongChiChiTiet = _thongTriService.FindByIdThongTri(item.Id.Value);
                        var lstThongChiChiTiet = _thongTriService.GetVdtThongTriChiTietByParentId(item.Id.Value);
                        if (lstThongChiChiTiet == null) return;
                        var items = _mapper.Map<List<VdtThongTriChiTietModel>>(lstThongChiChiTiet);
                        double tongTien = 0;
                        FormatNumber formatNumber = new FormatNumber(1, exportType);
                        Dictionary<string, object> data = new Dictionary<string, object>();

                        switch (item.ILoaiThongTri)
                        {
                            case (int)LoaiThongTriEnum.Type.CAP_THANH_TOAN:
                                templateFileName = Path.Combine(ExportPrefix.PATH_TL_TTTT, ExportFileName.RPT_VDT_THONGTRI_THANHTOAN);
                                fileNamePrefix = string.Format("{0}_{1}", ExportFileName.RPT_VDT_THONGTRI_THANHTOAN, item.sMaThongTri);
                                break;
                            case (int)LoaiThongTriEnum.Type.CAP_TAM_UNG:
                                templateFileName = Path.Combine(ExportPrefix.PATH_TL_TTTT, ExportFileName.RPT_VDT_THONGTRI_TAMUNG);
                                fileNamePrefix = string.Format("{0}_{1}", ExportFileName.RPT_VDT_THONGTRI_TAMUNG, item.sMaThongTri);
                                break;
                            case (int)LoaiThongTriEnum.Type.CAP_KINH_PHI:
                                templateFileName = Path.Combine(ExportPrefix.PATH_TL_TTTT, ExportFileName.RPT_VDT_THONGTRI_KINHPHI);
                                fileNamePrefix = string.Format("{0}_{1}", ExportFileName.RPT_VDT_THONGTRI_KINHPHI, item.sMaThongTri);
                                break;
                            case (int)LoaiThongTriEnum.Type.CAP_HOP_THUC:
                                templateFileName = Path.Combine(ExportPrefix.PATH_TL_TTTT, ExportFileName.RPT_VDT_THONGTRI_HOPTHUC);
                                fileNamePrefix = string.Format("{0}_{1}", ExportFileName.RPT_VDT_THONGTRI_HOPTHUC, item.sMaThongTri);
                                break;
                        }

                        if (item.ILoaiThongTri == (int)LoaiThongTriEnum.Type.CAP_THANH_TOAN)
                        {
                            data = new Dictionary<string, object>();
                            data.Add("FormatNumber", formatNumber);
                            data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionService.Current.YearOfWork).ToUpper());
                            data.Add("Cap2", GetHeader2Report());
                            data.Add("Nam", DateTime.Now.Year.ToString());
                            data.Add("DonVi", item.sTenDonVi);
                            data.Add("Ve", string.Format("Tháng {0} năm {1}", DateTime.Now.Month, DateTime.Now.Year));
                            data.Add("Mota", ""); 
                            data.Add("NoiDung", "");
                            //data.Add("Items", ConvertDataExport(_thongTriService.GetVdtThongTriChiTietByParentId(item.Id.Value), ref tongTien, item.ILoaiThongTri, false));
                            if (lstThongChiChiTiet != null && lstThongChiChiTiet.Count() > 0)
                            {
                                data.Add("Items", ConvertDataExport(lstThongChiChiTiet, ref tongTien, item.ILoaiThongTri));
                            }
                            else
                            {
                                data.Add("Items", ConvertDataExport(_thongTriService.GetVdtThongTriChiTiet((item.Id ?? Guid.Empty), item.iID_MaDonViID, item.ILoaiThongTri, item.iNamThongTri, item.dNgayThongTri.Value,
                                item.sMaNguonVon, item.dNgayLapGanNhat).ToList(), ref tongTien, item.ILoaiThongTri));
                            }
                            data.Add("TongChiTieu", tongTien);
                            data.Add("TienBangChu", StringUtils.NumberToText(tongTien, true));
                            data.Add("Ngay", string.Format("Ngày {0} tháng {1} năm {2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_TTTT, ExportFileName.RPT_VDT_THONGTRI_THUHOIUNG);
                            fileNamePrefix = string.Format("{0}_{1}", ExportFileName.RPT_VDT_THONGTRI_THUHOIUNG, item.sMaThongTri);
                        } else
                        {
                            data.Add("FormatNumber", formatNumber);
                            data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionService.Current.YearOfWork).ToUpper());
                            data.Add("Cap2", GetHeader2Report());
                            data.Add("Nam", DateTime.Now.Year.ToString());
                            data.Add("DonVi", item.sTenDonVi);
                            data.Add("Ve", string.Format("Tháng {0} năm {1}", DateTime.Now.Month, DateTime.Now.Year));
                            data.Add("Mota", "");
                            data.Add("NoiDung", "");
                            //data.Add("Items", ConvertDataExport(_thongTriService.GetVdtThongTriChiTietByParentId(item.Id.Value), ref tongTien, item.ILoaiThongTri, false));
                            if (lstThongChiChiTiet != null && lstThongChiChiTiet.Count() > 0)
                            {
                                data.Add("Items", ConvertDataExport(lstThongChiChiTiet, ref tongTien, item.ILoaiThongTri));
                            }
                            else
                            {
                                data.Add("Items", ConvertDataExport(_thongTriService.GetVdtThongTriChiTiet((item.Id ?? Guid.Empty), item.iID_MaDonViID, item.ILoaiThongTri, item.iNamThongTri, item.dNgayThongTri.Value,
                                item.sMaNguonVon, item.dNgayLapGanNhat).ToList(), ref tongTien, item.ILoaiThongTri));
                            }
                            data.Add("TongChiTieu", tongTien);
                            data.Add("TienBangChu", StringUtils.NumberToText(tongTien, true));
                            data.Add("Ngay", string.Format("Ngày {0} tháng {1} năm {2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));
                        }

                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<VdtThongTriChiTietModel>(templateFileName, data);
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        if (result != null)
                        {
                            _exportService.Open(result, exportType);
                        }
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
        #endregion

        #region Helper
        public string GetHeader2Report()
        {
            DonVi donViParent = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
            return donViParent != null ? donViParent.TenDonVi.ToUpper() : string.Empty;
        }

        private void GetDonViQuanLy()
        {
            var cbxLoaiDonViData = _nsDonViService.GetDanhSachDonViByNguoiDung(
                _sessionService.Current.Principal, _sessionService.Current.YearOfWork)
                .Select(n => new ComboboxItem() { ValueItem = n.IIDMaDonVi, DisplayItem = string.Format("{0}-{1}", n.IIDMaDonVi, n.TenDonVi) });
            _drpDonViQuanLy = new ObservableCollection<ComboboxItem>(cbxLoaiDonViData);
        }

        private void LoadLoaiThongTri()
        {
            List<ComboboxItem> lstData = new List<ComboboxItem>();
            lstData.Add(new ComboboxItem()
            {
                DisplayItem = LoaiThongTriEnum.Name.CAP_THANH_TOAN,
                ValueItem = "1"
            });
            lstData.Add(new ComboboxItem()
            {
                DisplayItem = LoaiThongTriEnum.Name.CAP_TAM_UNG,
                ValueItem = "2"
            });
            lstData.Add(new ComboboxItem()
            {
                DisplayItem = LoaiThongTriEnum.Name.CAP_KINH_PHI,
                ValueItem = "3"
            });
            lstData.Add(new ComboboxItem()
            {
                DisplayItem = LoaiThongTriEnum.Name.CAP_HOP_THUC,
                ValueItem = "4"
            });
           
            ItemsLoaiThongTri = new ObservableCollection<ComboboxItem>(lstData);
        }

        private bool VdtTtDeNghiThanhToanFilter(object obj)
        {
            if (!(obj is VdtThongTriModel temp)) return true;
            var bCondition = true;
            int iNamKeHoachParse = 0;
            if (!string.IsNullOrEmpty(SMaThongTri))
            {
                bCondition &= (!string.IsNullOrEmpty(temp.sMaThongTri) && temp.sMaThongTri.ToLower().Contains(SMaThongTri.ToLower()));
            }
            if (!string.IsNullOrEmpty(SMoTa))
            {
                bCondition &= (!string.IsNullOrEmpty(temp.sMoTa) && temp.sMoTa.ToLower().Contains(SMoTa.ToLower()));
            }
            if (SelectedLoaiThongTri != null && !string.IsNullOrEmpty(SelectedLoaiThongTri.ValueItem))
            {
                bCondition &= (temp.ILoaiThongTri == int.Parse(SelectedLoaiThongTri.ValueItem));
            }
            if (!string.IsNullOrEmpty(iNamThongTri) && int.TryParse(iNamThongTri, out iNamKeHoachParse))
            {
                bCondition &= (temp.iNamThongTri == iNamKeHoachParse);
            }
            if (DNgayThongTriFrom.HasValue)
            {
                bCondition &= (temp.dNgayThongTri.HasValue && temp.dNgayThongTri >= DNgayThongTriFrom);
            }
            if (DNgayThongTriTo.HasValue)
            {
                bCondition &= (temp.dNgayThongTri.HasValue && temp.dNgayThongTri <= DNgayThongTriTo);
            }
            if (DrpDonViQuanLySelected != null)
            {
                bCondition &= (temp.iID_MaDonViID == DrpDonViQuanLySelected.ValueItem);
            }
            return bCondition;
        }

        private void OnOpenThongTriCapPhatDetail(VdtThongTriModel SelectedItem, bool isDetail)
        {
            ThongTriCapPhatDetailViewModel.Model = SelectedItem;
            ThongTriCapPhatDetailViewModel.IsDetail = isDetail;
            ThongTriCapPhatDetailViewModel.Init();
            var view = new ThongTriCapPhatDetail { DataContext = ThongTriCapPhatDetailViewModel };
            //view.Owner = System.Windows.Application.Current.MainWindow;
            view.ShowDialog();
            LoadData();
        }

        private void DeleteEventHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            _thongTriService.DeleteThongTriThanhToan(_mapper.Map<VdtThongTri>(SelectedItem));
            LoadData();
        }

        private List<VdtThongTriChiTietModel> ConvertDataExport(IEnumerable<VdtThongTriChiTietQuery> datas, ref double fTongTien, int iLoaiThanhToan, bool bIsThanhToan = true)
        {
            fTongTien = 0;
            if (datas == null) return new List<VdtThongTriChiTietModel>();
            List<VdtThongTriChiTietModel> results = new List<VdtThongTriChiTietModel>();

            if (iLoaiThanhToan == (int)LoaiThongTriEnum.Type.CAP_THANH_TOAN)
            {
                List<string> lstKieuThongTriThanhToan = new List<string>() { KieuThongTri.TT_KPQP, KieuThongTri.TT_Cap_KPK, KieuThongTri.TT_Cap_KPNN };
                List<string> lstKieuThongTriThuHoi = new List<string>() { KieuThongTri.TT_ThuUng_KPQP, KieuThongTri.TT_ThuUng_KPNN, KieuThongTri.TT_ThuUng_KPK };
                if (bIsThanhToan)
                {
                    datas = datas.Where(n => lstKieuThongTriThanhToan.Contains(n.SMaKieuThongTri));
                }
                else
                {
                    datas = datas.Where(n => lstKieuThongTriThuHoi.Contains(n.SMaKieuThongTri));
                }
            }

            fTongTien = datas.Sum(n => n.FSoTien);
            int asciiIndex = 65;

            foreach (var item in datas.GroupBy(n => n.IIdLoaiCongTrinhId).OrderBy(n => n.Key).Select(n => n.Key))
            {
                var lstChild = datas.Where(n => n.IIdLoaiCongTrinhId == item);
                if (item.HasValue)
                {
                    string sTenLoaiCongTrinh = _dicLoaiCongTrinh.ContainsKey(item.Value) ? _dicLoaiCongTrinh[item.Value].STenLoaiCongTrinh : string.Empty;
                    results.Add(new VdtThongTriChiTietModel()
                    {
                        FSoTien = lstChild.Sum(n => n.FSoTien),
                        IsHangCha = true,
                        STenDuAn = string.Format("{0}. {1}", (char)asciiIndex, sTenLoaiCongTrinh)
                    });
                    asciiIndex++;
                }
                if (lstChild != null)
                    results.AddRange(_mapper.Map<List<VdtThongTriChiTietModel>>(lstChild));
            }
            return results;
        }

        private void LoadLoaiCongTrinh()
        {
            _dicLoaiCongTrinh = new Dictionary<Guid, VdtDmLoaiCongTrinh>();
            var lstLoaiCongTrinh = _loaicongtrinhService.FindAll();
            if (lstLoaiCongTrinh == null) return;
            _dicLoaiCongTrinh = lstLoaiCongTrinh.ToDictionary(n => n.IIdLoaiCongTrinh, n => n);
        }
        #endregion
    }
}
