using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Category
{
    public class NsBaoCaoGhiChuDialogViewModel : DetailViewModelBase<NsCauHinhBaoCao, NsCauHinhBaoCaoModel>
    {
        private readonly INsBaoCaoGhiChuService _ghiChuService;
        private readonly IDanhMucService _danhMucService;
        private readonly INsPhongBanService _iNsPhongBanService;
        private readonly ISktChungTuService _sktChungTuService;
        private readonly ISktChungTuChiTietService _sktChungTuChiTietService;

        private readonly IMapper _mapper;
        private readonly IServiceProvider _provider;
        private ISessionService _sessionService;
        private readonly IDmChuKyService _dmChuKyService;
        public override string Name => "Danh mục báo cáo ghi chú";
        public override string Description => "Danh mục báo cáo ghi chú";
        public override Type ContentType => typeof(NsBaoCaoGhiChuDialog);
        public bool IsBaoCaoDuToan { get; set; }
        public bool IsPlanReport { get; set; }
        public bool IsShowBQuanLy { get; set; }
        public bool IsBaoCaoDuToanTongHop { get; set; }
        public bool IsBaoCaoSoKiemTra { get; set; }
        public bool IsBaoCaoSoNhuCauTongHop { get; set; }
        public bool IsBaoCaoSoNhuCauTongHop_Nganh => VoucherTypeSelected != null && int.Parse(VoucherTypeSelected.ValueItem).Equals(int.Parse(VoucherType.NSSD_Key)) && !SMaBaoCao.Equals(TypeChuKy.RPT_NS_PLAN_DUTOAN_NGANSACH_CHITIET_DONVI_SD) && !SMaBaoCao.Equals(TypeChuKy.RPT_NS_PLAN_DUTOAN_NGANSACH_CHITIET_DONVI_DT);
        public bool IsVisibilityRadioButtonNSBD => _paperPrintTypeSelected != null && _paperPrintTypeSelected.ValueItem == "1"
            && VoucherTypeSelected != null && int.Parse(VoucherTypeSelected.ValueItem).Equals(int.Parse(VoucherType.NSBD_Key));
        public bool IsBaoCaoDinhMuc => SMaBaoCao.Equals(TypeChuKy.RPT_NS_PLAN_DUTOAN_NGANSACH_CHITIET_DONVI_SD) || SMaBaoCao.Equals(TypeChuKy.RPT_NS_PLAN_DUTOAN_NGANSACH_CHITIET_DONVI_DT);

        private LoaiNSBD _loaiNSBD;
        public LoaiNSBD LoaiNSBD
        {
            get => _loaiNSBD;
            set
            {
                SetProperty(ref _loaiNSBD, value);
                TxtGhiChu = Items.FirstOrDefault(x => x.SMaGhiChu == GetMaGhiChu())?.SGhiChu ?? GetDefaultGhiChu();
            }
        }

        private string _txtGhiChu;
        public string TxtGhiChu
        {
            get => _txtGhiChu;
            set
            {
                SetProperty(ref _txtGhiChu, value);
                AddGhiChu();
            }
        }

        private BhCauHinhBaoCaoModel _selectedItemChange;
        private BhCauHinhBaoCaoModel SelectedItemChange
        {
            get => _selectedItemChange;
            set
            {
                SetProperty(ref _selectedItemChange, value);
            }
        }

        private ObservableCollection<ComboboxItem> _paperPrintTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> PaperPrintTypes
        {
            get => _paperPrintTypes;
            set => SetProperty(ref _paperPrintTypes, value);
        }

        private ComboboxItem _paperPrintTypeSelected;

        public ComboboxItem PaperPrintTypeSelected
        {
            get => _paperPrintTypeSelected;
            set
            {
                if (SetProperty(ref _paperPrintTypeSelected, value))
                {
                    TxtGhiChu = Items.FirstOrDefault(x => x.SMaGhiChu == GetMaGhiChu())?.SGhiChu ?? GetDefaultGhiChu();
                    OnPropertyChanged(nameof(IsVisibilityRadioButtonNSBD));
                    OnPropertyChanged(nameof(IsBaoCaoSoNhuCauTongHop_Nganh));
                }
            }
        }

        private bool _isInTheoTongHop;
        public bool IsInTheoTongHop
        {
            get => _isInTheoTongHop;
            set
            {
                if (SetProperty(ref _isInTheoTongHop, value))
                {
                    TxtGhiChu = Items.FirstOrDefault(x => x.SMaGhiChu == GetMaGhiChu())?.SGhiChu ?? GetDefaultGhiChu();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _khoiItems = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> KhoiItems
        {
            get => _khoiItems;
            set => SetProperty(ref _khoiItems, value);
        }

        private ComboboxItem _khoiSelected;

        public ComboboxItem KhoiSelected
        {
            get => _khoiSelected;
            set
            {
                if (SetProperty(ref _khoiSelected, value))
                {
                    TxtGhiChu = Items.FirstOrDefault(x => x.SMaGhiChu == GetMaGhiChu())?.SGhiChu ?? GetDefaultGhiChu();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _bQuanLyItems = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> BQuanLyItems
        {
            get => _bQuanLyItems;
            set => SetProperty(ref _bQuanLyItems, value);
        }

        private ComboboxItem _bQuanLySelected;

        public ComboboxItem BQuanLySelected
        {
            get => _bQuanLySelected;
            set
            {
                if (SetProperty(ref _bQuanLySelected, value))
                {
                    TxtGhiChu = Items.FirstOrDefault(x => x.SMaGhiChu == GetMaGhiChu())?.SGhiChu ?? GetDefaultGhiChu();
                }
            }
        }


        private ObservableCollection<ComboboxItem> _voucherTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> VoucherTypes
        {
            get => _voucherTypes;
            set => SetProperty(ref _voucherTypes, value);
        }

        private ComboboxItem _voucherTypeSelected;

        public ComboboxItem VoucherTypeSelected
        {
            get => _voucherTypeSelected;
            set
            {
                if (SetProperty(ref _voucherTypeSelected, value))
                {
                    TxtGhiChu = Items.FirstOrDefault(x => x.SMaGhiChu == GetMaGhiChu())?.SGhiChu ?? GetDefaultGhiChu();
                    OnPropertyChanged(nameof(IsVisibilityRadioButtonNSBD));
                    OnPropertyChanged(nameof(IsBaoCaoSoNhuCauTongHop_Nganh));
                }
            }
        }

        private ObservableCollection<ComboboxItem> _budgetTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> BudgetTypes
        {
            get => _budgetTypes;
            set => SetProperty(ref _budgetTypes, value);
        }

        private ComboboxItem _budgetTypeSelected;

        public ComboboxItem BudgetTypeSelected
        {
            get => _budgetTypeSelected;
            set
            {
                if (SetProperty(ref _budgetTypeSelected, value))
                {
                    TxtGhiChu = Items.FirstOrDefault(x => x.SMaGhiChu == GetMaGhiChu())?.SGhiChu ?? GetDefaultGhiChu();

                }
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
                if (SetProperty(ref _budgetSourceTypeSelected, value))
                {
                    TxtGhiChu = Items.FirstOrDefault(x => x.SMaGhiChu == GetMaGhiChu())?.SGhiChu ?? GetDefaultGhiChu();
                }
            }
        }

        public string SMaBaoCao { get; set; }
        public NsBaoCaoGhiChuDialogViewModel(
            IMapper mapper,
            IServiceProvider serviceProvider,
            ISessionService sessionService,
            IDmChuKyService dmChuKyService)
        {
            _mapper = mapper;
            _provider = serviceProvider;
            _ghiChuService = (INsBaoCaoGhiChuService)_provider.GetService(typeof(INsBaoCaoGhiChuService));
            _danhMucService = (IDanhMucService)_provider.GetService(typeof(IDanhMucService));
            _iNsPhongBanService = (INsPhongBanService)_provider.GetService(typeof(INsPhongBanService));
            _sktChungTuService = (ISktChungTuService)_provider.GetService(typeof(ISktChungTuService));
            _sktChungTuChiTietService = (ISktChungTuChiTietService)_provider.GetService(typeof(ISktChungTuChiTietService));

            _sessionService = sessionService;
            _dmChuKyService = dmChuKyService;
            SaveCommand = new RelayCommand(obj => OnSave(obj));
        }

        public override void Init()
        {
            IsBaoCaoDuToan = SMaBaoCao.Equals(TypeChuKy.RPT_NS_DUTOAN_DIEUCHINH) || SMaBaoCao.Equals(TypeChuKy.RPT_NS_DUTOAN_DIEUCHINH_DONVI) || SMaBaoCao.Equals(TypeChuKy.RPT_NS_DUTOAN_DIEUCHINH_TONGHOP2);
            IsShowBQuanLy = SMaBaoCao.Equals(TypeChuKy.RPT_NS_DUTOAN_DIEUCHINH) || SMaBaoCao.Equals(TypeChuKy.RPT_NS_DUTOAN_DIEUCHINH_DONVI);
            base.Init();
            SelectedItemChange = null;
            LoadBudgetSourceTypes();
            LoadBudgetTypes();
            LoadPaperPrintTypes();
            LoadVoucherTypes();
            LoadKhois();
            LoadBQuanLys();
            LoadDataDefault();
        }

        public void LoadBQuanLys()
        {
            _bQuanLyItems = new ObservableCollection<ComboboxItem>();
            var predicate = PredicateBuilder.True<DmBQuanLy>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);

            List<DmBQuanLy> data = _iNsPhongBanService.FindByCondition(predicate).ToList();
            _bQuanLyItems = _mapper.Map<ObservableCollection<ComboboxItem>>(data);
            _bQuanLyItems.Insert(0, new ComboboxItem { DisplayItem = "Tất cả", ValueItem = "0" });
            //_bQuanLySelected = _bQuanLyItems.FirstOrDefault();
        }

        private void LoadVoucherTypes()
        {
            var voucherTypes = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = VoucherType.NSSD_Value, ValueItem = VoucherType.NSSD_Key},
                new ComboboxItem {DisplayItem = VoucherType.NSBD_Value, ValueItem = VoucherType.NSBD_Key},
            };

            VoucherTypes = new ObservableCollection<ComboboxItem>(voucherTypes);
            //VoucherTypeSelected = VoucherTypes.ElementAt(0);
        }

        public void LoadPaperPrintTypes()
        {
            var paperPrintTypes = new List<ComboboxItem>();
            if (SMaBaoCao.Equals(TypeChuKy.RPT_NS_PHUONGAN_PHANBO_SOKIEMTRA_02A) || SMaBaoCao.Equals(TypeChuKy.RPT_NS_PHUONGAN_PHANBO_SOKIEMTRA_02B))
            {
                paperPrintTypes = new List<ComboboxItem>
                {
                    new ComboboxItem {DisplayItem = "Tổng hợp đơn vị dọc - mục lục ngang", ValueItem = "1"},
                    new ComboboxItem {DisplayItem = "Tổng hợp - đơn vị", ValueItem = "2"}
                };
            }
            else if (SMaBaoCao.Equals(TypeChuKy.RPT_NS_PLAN_DUTOAN_NGANSACH_CHITIET_DONVI_SD) || SMaBaoCao.Equals(TypeChuKy.RPT_NS_PLAN_DUTOAN_NGANSACH_CHITIET_DONVI_DT))
            {
                paperPrintTypes = new List<ComboboxItem>
                {
                    //new ComboboxItem {DisplayItem = Utility.LoaiBaoCao.DU_TOAN_NS_CHITIET_DONVI, ValueItem = Utility.LoaiBaoCao.DU_TOAN_NS_CHITIET_DONVI},
                    new ComboboxItem {DisplayItem = Utility.LoaiBaoCao.DU_TOAN_NS_CHI_TONGHOP_DONVI, ValueItem = Utility.LoaiBaoCao.DU_TOAN_NS_CHI_TONGHOP_DONVI},
                    new ComboboxItem {DisplayItem = Utility.LoaiBaoCao.DU_TOAN_NS_CHI_MUCLUC_DONVI, ValueItem = Utility.LoaiBaoCao.DU_TOAN_NS_CHI_MUCLUC_DONVI}
                };
            }
            else
            {
                paperPrintTypes = new List<ComboboxItem>
                {
                    new ComboboxItem {DisplayItem = "Tổng hợp đơn vị", ValueItem = "1"},
                    new ComboboxItem {DisplayItem = "Tổng hợp chi tiết đơn vị", ValueItem = "2"},
                    new ComboboxItem {DisplayItem = "Tổng hợp đơn vị ngang - mục lục dọc", ValueItem = "3"}
                };
            }
            PaperPrintTypes = new ObservableCollection<ComboboxItem>(paperPrintTypes);
            //_paperPrintTypeSelected = paperPrintTypes.ElementAt(0);
        }



        private string GetDefaultGhiChu()
        {
            if (!IsInTheoTongHop)
            {
                return string.Empty;
            }
            var yearOfWork = _sessionService.Current.YearOfWork;
            int yearOfBudget = _sessionService.Current.YearOfBudget;
            int budgetSource = _sessionService.Current.Budget;
            var iLoai = DemandCheckType.DEMAND;
            var loaiChungTu = VoucherTypeSelected != null ? Int32.Parse(VoucherTypeSelected.ValueItem) : 1;
            var listChungTu = _sktChungTuService
                .FindChungTuIndexByCondition(iLoai, yearOfWork, yearOfBudget, budgetSource, loaiChungTu, null,
                    _sessionService.Current.Principal, "sp_skt_nhap_so_nhu_cau").ToList();
            if (BudgetSourceTypeSelected is null)
                return string.Empty;
            var chungTu = listChungTu.Where(x => x.IIdMaDonVi == _sessionService.Current.IdDonVi && (int.Parse(BudgetSourceTypeSelected.ValueItem) == 0 || x.ILoaiNguonNganSach.Value == int.Parse(BudgetSourceTypeSelected.ValueItem))).Select(x => x.Id);
            var chitiets = _sktChungTuChiTietService.FindByCondition(x => chungTu.Contains(x.IIdCtsoKiemTra)).Where(x => !string.IsNullOrEmpty(x.SGhiChu)).Select(x => x.SGhiChu).Distinct().OrderBy(x => x).ToList();
            return string.Join(Environment.NewLine, chitiets);
        }

        private void LoadBudgetSourceTypes()
        {
            BudgetSourceTypes = new ObservableCollection<ComboboxItem> {
                new ComboboxItem() { DisplayItem = "Tất cả", ValueItem = TypeLoaiNNS.TAT_CA.ToString() },
                new ComboboxItem() { DisplayItem = "Ngân sách dự toán", ValueItem = TypeLoaiNNS.DU_TOAN.ToString() },
                new ComboboxItem() { DisplayItem = "Ngân sách bệnh viện tự chủ", ValueItem = TypeLoaiNNS.BENH_VIEN.ToString() },
                new ComboboxItem() { DisplayItem = "Ngân sách doanh nghiệp", ValueItem = TypeLoaiNNS.DOANH_NGHIEP.ToString() }
            };

            //BudgetSourceTypeSelected = BudgetSourceTypes.ElementAt(0);
        }

        private void LoadBudgetTypes()
        {
            BudgetTypes = new ObservableCollection<ComboboxItem> {
                new ComboboxItem() { DisplayItem = "Ngân sách quốc phòng", ValueItem = TypeLoaiNS.NS_QUOC_PHONG.ToString() },
                new ComboboxItem() { DisplayItem = "Ngân sách nhà nước chi hoạt động sự nghiệp và các hoạt động khác", ValueItem = TypeLoaiNS.NS_NHA_NUOC.ToString() },
            };

            //BudgetTypeSelected = BudgetTypes.ElementAt(0);
        }


        public void LoadKhois()
        {
            var khoiItems = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Tất cả", ValueItem = TypeKhoi.TAT_CA.ToString()},
                new ComboboxItem {DisplayItem = "Doanh nghiệp", ValueItem = TypeKhoi.DOANH_NGHIEP.ToString()},
                new ComboboxItem {DisplayItem = "Dự toán", ValueItem = TypeKhoi.DU_TOAN.ToString()},
                new ComboboxItem {DisplayItem = "Bệnh viện tự chủ", ValueItem = TypeKhoi.BENH_VIEN.ToString()},
            };
            KhoiItems = new ObservableCollection<ComboboxItem>(khoiItems);
            //KhoiSelected = KhoiItems.ElementAt(0);
        }


        private string GetMaGhiChu()
        {
            if (IsPlanReport)
            {
                if (PaperPrintTypeSelected != null && BudgetSourceTypeSelected != null && VoucherTypeSelected != null)
                {
                    var data = JsonConvert.SerializeObject(new
                    {
                        LoaiBaoCao = PaperPrintTypeSelected.DisplayItem,
                        LoaiNguonNganSach = BudgetSourceTypeSelected.DisplayItem,
                        LoaiChungTu = VoucherTypeSelected.DisplayItem
                    });
                    return CompressExtension.CompressToBase64(data);
                }
                else
                    return string.Empty;
            }
            else if (IsBaoCaoDuToan)
            {
                if (BQuanLySelected != null && (SMaBaoCao.Equals(TypeChuKy.RPT_NS_DUTOAN_DIEUCHINH) || SMaBaoCao.Equals(TypeChuKy.RPT_NS_DUTOAN_DIEUCHINH_DONVI)))
                {
                    var data = JsonConvert.SerializeObject(new
                    {
                        BQuanLy = BQuanLySelected.DisplayItem
                    });
                    return CompressExtension.CompressToBase64(data);
                }
                else if (BQuanLySelected == null && SMaBaoCao.Equals(TypeChuKy.RPT_NS_DUTOAN_DIEUCHINH_TONGHOP2))
                {
                    var data = JsonConvert.SerializeObject(new
                    {
                        BQuanLy = "rptNS_DuToan_DieuChinh_TONGHOP2"
                    });
                    return CompressExtension.CompressToBase64(data);
                }
                else
                    return string.Empty;
            }
            else
            {
                if (IsBaoCaoSoNhuCauTongHop)
                {
                    if (PaperPrintTypeSelected != null
                    && KhoiSelected != null
                    && BQuanLySelected != null
                    && VoucherTypeSelected != null
                    && BudgetSourceTypeSelected != null
                    && BudgetTypeSelected != null)
                    {
                        if (IsVisibilityRadioButtonNSBD)
                        {
                            var data = JsonConvert.SerializeObject(new
                            {
                                LoaiBaoCao = PaperPrintTypeSelected.DisplayItem,
                                InTheoChungTuTongHop = IsInTheoTongHop,
                                BQuanLy = BQuanLySelected.DisplayItem,
                                LoaiChungTu = VoucherTypeSelected.DisplayItem,
                                LoaiNguonNganSach = BudgetSourceTypeSelected.DisplayItem,
                                LoaiNganSach = BudgetTypeSelected.DisplayItem,
                                DacThu = LoaiNSBD == LoaiNSBD.DAC_THU,
                                MHHV = LoaiNSBD == LoaiNSBD.MHHV
                            });
                            return CompressExtension.CompressToBase64(data);
                        }
                        else
                        {
                            var data = JsonConvert.SerializeObject(new
                            {
                                LoaiBaoCao = PaperPrintTypeSelected.DisplayItem,
                                InTheoChungTuTongHop = IsInTheoTongHop,
                                Khoi = KhoiSelected.DisplayItem,
                                BQuanLy = BQuanLySelected.DisplayItem,
                                LoaiChungTu = VoucherTypeSelected.DisplayItem,
                                LoaiNguonNganSach = BudgetSourceTypeSelected.DisplayItem,
                                LoaiNganSach = BudgetTypeSelected.DisplayItem
                            });
                            return CompressExtension.CompressToBase64(data);
                        }
                    }
                    else
                        return string.Empty;
                }
                else
                {
                    if (VoucherTypeSelected != null
                        && BudgetSourceTypeSelected != null
                        && BudgetTypeSelected != null)
                    {
                        if (IsVisibilityRadioButtonNSBD)
                        {
                            var data = JsonConvert.SerializeObject(new
                            {
                                LoaiChungTu = VoucherTypeSelected.DisplayItem,
                                LoaiNguonNganSach = BudgetSourceTypeSelected.DisplayItem,
                                LoaiNganSach = BudgetTypeSelected.DisplayItem,
                                DacThu = LoaiNSBD == LoaiNSBD.DAC_THU,
                                MHHV = LoaiNSBD == LoaiNSBD.MHHV
                            });
                            return CompressExtension.CompressToBase64(data);
                        }
                        else if (IsBaoCaoSoKiemTra)
                        {
                            var data = JsonConvert.SerializeObject(new
                            {
                                LoaiBaoCao = PaperPrintTypeSelected.DisplayItem,
                                LoaiChungTu = VoucherTypeSelected.DisplayItem,
                                LoaiNguonNganSach = BudgetSourceTypeSelected.DisplayItem,
                                LoaiNganSach = BudgetTypeSelected.DisplayItem,
                                Khoi = KhoiSelected.DisplayItem,
                            });
                            return CompressExtension.CompressToBase64(data);
                        }
                        else
                        {
                            var data = JsonConvert.SerializeObject(new
                            {
                                LoaiChungTu = VoucherTypeSelected.DisplayItem,
                                LoaiNguonNganSach = BudgetSourceTypeSelected.DisplayItem,
                                LoaiNganSach = BudgetTypeSelected.DisplayItem,
                            });
                            return CompressExtension.CompressToBase64(data);
                        }
                    }
                    else
                        return string.Empty;
                }
            }
        }


        private void AddGhiChu()
        {
            var maGhiChu = GetMaGhiChu();
            if (string.IsNullOrEmpty(maGhiChu))
                return;
            var item = Items.FirstOrDefault(x => x.SMaGhiChu == maGhiChu);
            if (item is null)
            {
                if (!string.IsNullOrEmpty(TxtGhiChu))
                {
                    Items.Add(new NsCauHinhBaoCaoModel()
                    {
                        SMaGhiChu = maGhiChu,
                        INamLamViec = _sessionService.Current.YearOfWork,
                        SGhiChu = TxtGhiChu,
                        SMaBaoCao = SMaBaoCao,
                        IsModified = true
                    });
                }
            }
            else
            {
                item.SGhiChu = TxtGhiChu;
                item.INamLamViec = _sessionService.Current.YearOfWork;
                item.SMaBaoCao = SMaBaoCao;
                item.IsModified = true;
            }
        }

        private void LoadDataDefault()
        {
            var iNamLamViec = _sessionService.Current.YearOfWork;
            var predicate = PredicateBuilder.True<NsCauHinhBaoCao>();
            predicate = predicate.And(x => x.INamLamViec.Equals(iNamLamViec));
            predicate = predicate.And(x => x.SMaBaoCao == SMaBaoCao);

            var data = _ghiChuService.FindByCondition(predicate).ToList();
            Items = _mapper.Map<ObservableCollection<NsCauHinhBaoCaoModel>>(data);
            TxtGhiChu = Items.FirstOrDefault(x => x.SMaGhiChu == GetMaGhiChu())?.SGhiChu ?? GetDefaultGhiChu();
        }

        public override void OnSave(object obj)
        {
            var itemsSaveChange = _mapper.Map<List<NsCauHinhBaoCao>>(Items.Where(x => !string.IsNullOrEmpty(x.SGhiChu?.Trim())));
            var itemsDelete = _mapper.Map<List<NsCauHinhBaoCao>>(Items.Where(x => x.Id != Guid.Empty && string.IsNullOrEmpty(x.SGhiChu?.Trim())));
            itemsSaveChange.ForAll(x =>
            {
                x.SGhiChu = x.SGhiChu.Trim();
                if (x.Id.IsNullOrEmpty())
                {
                    x.DNgayTao = DateTime.Now;
                    x.SNguoiTao = _sessionService.Current.Principal;
                }
                else
                {
                    x.DNgaySua = DateTime.Now;
                    x.SNguoiSua = _sessionService.Current.Principal;
                }
            });
            _ghiChuService.AddOrUpdateRange(itemsSaveChange);
            _ghiChuService.RemoveRange(itemsDelete);
            LoadDataDefault();
            MessageBoxHelper.Info(Resources.MsgSaveDone);
            SavedAction?.Invoke(null);
        }

        public override void OnClose(object obj)
        {
            base.OnClose(obj);
            Window window = obj as Window;
            window.Close();
        }
    }
}
