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
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Category
{
    public class NsDacThuNganhGhiChuDialogViewModel : DetailViewModelBase<NsCauHinhBaoCao, NsCauHinhBaoCaoModel>
    {
        private readonly INsBaoCaoGhiChuService _ghiChuService;
        private readonly IDanhMucService _danhMucService;
        private readonly INsPhongBanService _iNsPhongBanService;
        private readonly ISktChungTuService _sktChungTuService;
        private readonly ISktChungTuChiTietService _sktChungTuChiTietService;
        private string _chiTietToi;

        private readonly IMapper _mapper;
        private readonly IServiceProvider _provider;
        private ISessionService _sessionService;
        private readonly IDmChuKyService _dmChuKyService;
        public override string Name => "Danh mục báo cáo ghi chú";
        public override string Description => "Danh mục báo cáo ghi chú";
        public override Type ContentType => typeof(NsDacThuNganhGhiChuDialog);

        public bool IsBaoCaoSoNhuCauTongHop { get; set; }
        public bool IsBaoCaoSoNhuCauTongHop_Nganh => VoucherTypeSelected != null && int.Parse(VoucherTypeSelected.ValueItem).Equals(int.Parse(VoucherType.NSSD_Key));
        public bool IsVisibilityRadioButtonNSBD => _paperPrintTypeSelected != null && _paperPrintTypeSelected.ValueItem == "1"
            && VoucherTypeSelected != null && int.Parse(VoucherTypeSelected.ValueItem).Equals(int.Parse(VoucherType.NSBD_Key));

        private ObservableCollection<ComboboxItem> _dataInToiMuc;
        public ObservableCollection<ComboboxItem> DataInToiMuc
        {
            get => _dataInToiMuc;
            set => SetProperty(ref _dataInToiMuc, value);
        }
        private ComboboxItem _selectedInToiMuc;
        public ComboboxItem SelectedInToiMuc
        {
            get => _selectedInToiMuc;
            set => SetProperty(ref _selectedInToiMuc, value);
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

        private ObservableCollection<CheckBoxItem> _loaiBaoCao;
        public ObservableCollection<CheckBoxItem> LoaiBaoCao
        {
            get => _loaiBaoCao;
            set => SetProperty(ref _loaiBaoCao, value);
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



        private List<ComboboxItem> _bQuanLyItems = new List<ComboboxItem>();

        public List<ComboboxItem> BQuanLyItems
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
        public NsDacThuNganhGhiChuDialogViewModel(
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

        private void LoadTypeChuKy()
        {
            string loaiBaoCao = LoaiBaoCao.FirstOrDefault(n => n.IsChecked)?.DisplayItem;
            if (loaiBaoCao is object)
            {
                if (loaiBaoCao == Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_DUTOAN_PHANBO_NGANSACH_DACTHU_NGANG)
                {
                    SMaBaoCao = TypeChuKy.RPT_NS_DUTOAN_DAUNAM_CHITHUONGXUYEN_DACTHU_NGANG;
                } else if (loaiBaoCao == Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_DUTOAN_PHANBO_NGANSACH_DACTHU_DOC)
                {
                    SMaBaoCao = TypeChuKy.RPT_NS_DUTOAN_DAUNAM_CHITHUONGXUYEN_DACTHU_DOC;
                }
                else if (loaiBaoCao == Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_DUTOAN_CHIMUAHANGTAPTRUNG_CAPHIENVAT)
                {
                    SMaBaoCao = TypeChuKy.RPT_NS_DUTOAN_DAUNAM_CHITHUONGXUYEN_MHHV;
                } else if (loaiBaoCao == Utility.LoaiBaoCao.TONG_HOP_DU_TOAN_NGAN_SACH_DAC_THU_NGANH)
                {
                    SMaBaoCao = TypeChuKy.RPT_NS_TONG_HOP_DU_TOAN_NGAN_SACH_DAC_THU_NGANH;
                }
                else
                {
                    SMaBaoCao = TypeChuKy.RPT_NS_DUTOAN_DAUNAM_CHITHUONGXUYEN;
                }
            }
        }

        public override void Init()
        {
            base.Init();
            LoadPaperPrintTypes();
            LoadVoucherTypes();
            LoadChiTietToi();
            LoadDataDefault();
            foreach (var item in LoaiBaoCao)
            {
                item.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                    {
                        LoadTypeChuKy();
                        LoadDataDefault();
                        TxtGhiChu = Items.FirstOrDefault(x => x.SMaGhiChu == GetMaGhiChu())?.SGhiChu ?? GetDefaultGhiChu();
                    }
                };
            }
        }

        private void LoadChiTietToi()
        {
            var danhMucCauHinh = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToList();
            if (danhMucCauHinh.Count > 0)
            {
                var danhMucMLNS = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
                _chiTietToi = danhMucMLNS == null ? string.Empty : danhMucMLNS.SGiaTri;
                DataInToiMuc = new ObservableCollection<ComboboxItem>(DynamicMLNS.CreateMLNSReportSetting(_chiTietToi));
                _selectedInToiMuc = DataInToiMuc != null ? DataInToiMuc[0] : null;
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
            //VoucherTypeSelected = VoucherTypes.ElementAt(0);
        }

        public void LoadPaperPrintTypes()
        {
            var paperPrintTypes = new List<ComboboxItem>();
            paperPrintTypes = new List<ComboboxItem>
                {
                    new ComboboxItem {DisplayItem = "Tổng hợp đơn vị", ValueItem = "1"},
                    new ComboboxItem {DisplayItem = "Tổng hợp chi tiết đơn vị", ValueItem = "2"},
                    new ComboboxItem {DisplayItem = "Tổng hợp đơn vị ngang - mục lục dọc", ValueItem = "3"}
                };

            PaperPrintTypes = new ObservableCollection<ComboboxItem>(paperPrintTypes);
            //_paperPrintTypeSelected = paperPrintTypes.ElementAt(0);
        }



        private string GetDefaultGhiChu()
        {
            return string.Empty;
        }

        private string GetMaGhiChu()
        {
            if (LoaiBaoCao != null
                 && BQuanLySelected != null)
            {
                var data = JsonConvert.SerializeObject(new
                {
                    LoaiBaoCao = LoaiBaoCao.FirstOrDefault(x => x.IsChecked).DisplayItem,
                    InTheoChungTuTongHop = IsInTheoTongHop,
                    BQuanLy = BQuanLySelected.DisplayItem,
                });
                return CompressExtension.CompressToBase64(data);
            }
            else
                return string.Empty;
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
