using AutoMapper;
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
    public class BhBaoCaoGhiChuDialogViewModel : DetailViewModelBase<BhCauHinhBaoCao, BhCauHinhBaoCaoModel>
    {
        private readonly IBhBaoCaoGhiChuService _ghiChuService;
        private readonly IDanhMucService _danhMucService;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _provider;
        private ISessionService _sessionService;
        private readonly IDmChuKyService _dmChuKyService;
        public override string Name => "Danh mục báo cáo ghi chú";
        public override string Description => "Danh mục báo cáo ghi chú";
        public override Type ContentType => typeof(BhBaoCaoGhiChuDialog);
        private bool _isAgregate;
        public bool IsAgregate
        {
            get => _isAgregate;
            set
            {
                SetProperty(ref _isAgregate, value);
                LoadDataDefault();
            }

        }

        public bool IsShowAgencyDetail { get; set; }

        private List<DonVi> _itemsAgencies;
        public List<DonVi> ItemsAgencies
        {
            get => _itemsAgencies;
            set
            {
                SetProperty(ref _itemsAgencies, value);
            }
        }

        private DonVi _selectedAgency;
        public DonVi SelectedAgency
        {
            get => _selectedAgency;
            set
            {
                SetProperty(ref _selectedAgency, value);
            }
        }

        private List<ComboboxItem> _itemsReportType;
        public List<ComboboxItem> ItemsReportType
        {
            get => _itemsReportType;
            set
            {
                SetProperty(ref _itemsReportType, value);
            }
        }

        private string _txtGhiChu;
        public string TxtGhiChu
        {
            get => _txtGhiChu;
            set
            {
                if (SetProperty(ref _txtGhiChu, value))
                {
                    if (Items.Any(x => x.Selected))
                    {
                        Items.FirstOrDefault(x => x.Selected).SGhiChu = value;
                        OnPropertyChanged(nameof(Items));
                    }
                };
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

        private bool _isShowCanCu;
        public bool IsShowCanCu
        {
            get => _isShowCanCu;
            set
            {
                SetProperty(ref _isShowCanCu, value);
            }

        }

        private string _txtCanCu1;
        public string TxtCanCu1
        {
            get => _txtCanCu1;
            set
            {
                if (SetProperty(ref _txtCanCu1, value))
                {
                    if (Items.Any(x => x.Selected))
                    {
                        Items.FirstOrDefault(x => x.Selected).SCanCu1 = value;
                        OnPropertyChanged(nameof(Items));
                    }
                };
            }
        }

        private string _txtCanCu2;
        public string TxtCanCu2
        {
            get => _txtCanCu2;
            set
            {
                if (SetProperty(ref _txtCanCu2, value))
                {
                    if (Items.Any(x => x.Selected))
                    {
                        Items.FirstOrDefault(x => x.Selected).SCanCu2 = value;
                        OnPropertyChanged(nameof(Items));
                    }
                };
            }
        }

        public int NoteColWidth => IsShowCanCu ? (int)Utility.NoteColWidth.Normal : (int)Utility.NoteColWidth.Extend;

        public List<string> ListMaBaoCao { get; set; }
        public string SMaBaoCao { get; set; }
        public BhBaoCaoGhiChuDialogViewModel(
            IMapper mapper,
            IServiceProvider serviceProvider,
            ISessionService sessionService,
            IDmChuKyService dmChuKyService)
        {
            _mapper = mapper;
            _provider = serviceProvider;
            _ghiChuService = (IBhBaoCaoGhiChuService)_provider.GetService(typeof(IBhBaoCaoGhiChuService));
            _danhMucService = (IDanhMucService)_provider.GetService(typeof(IDanhMucService));
            _sessionService = sessionService;
            _dmChuKyService = dmChuKyService;
            SaveCommand = new RelayCommand(obj => OnSave(obj));
        }

        public override void Init()
        {
            SelectedItemChange = null;
            LoadDataDefault();
            base.Init();
        }

        private void LoadDataDefault()
        {
            TxtGhiChu = string.Empty;
            ListMaBaoCao ??= new List<string>();
            var iNamLamViec = _sessionService.Current.YearOfWork;
            var itemsBaoCao = _dmChuKyService.FindByCondition(x => ListMaBaoCao.Contains(x.IdType) && x.ITrangThai.Equals(StatusType.ACTIVE));

            var predicate = PredicateBuilder.True<BhCauHinhBaoCao>();
            predicate = predicate.And(x => x.INamLamViec.Equals(iNamLamViec));
            if (IsAgregate)
            {
                predicate = predicate.And(x => ListMaBaoCao.Contains(x.SMaBaoCao));
                predicate = predicate.And(x => x.ILoaiBaoCao.Equals((int)NoteTypeBhxh.AgencySummary));
            }
            else
            {
                predicate = predicate.And(x => x.SMaBaoCao.Equals(SMaBaoCao));
                predicate = predicate.And(x => x.ILoaiBaoCao.Equals((int)NoteTypeBhxh.AgencyDetail));

            }
            var data = _ghiChuService.FindByCondition(predicate);
            data ??= new List<BhCauHinhBaoCao>();
            var ItemsModel = _mapper.Map<List<BhCauHinhBaoCaoModel>>(data);
            if (!IsAgregate && !ItemsAgencies.IsEmpty() && ItemsAgencies.Any(x => !ItemsModel.Select(z => z.IIdMaDonVi).Contains(x.IIDMaDonVi)))
            {
                var itemsMap = _mapper.Map<List<BhCauHinhBaoCaoModel>>(ItemsAgencies.Where(x => !ItemsModel.Select(z => z.IIdMaDonVi).Contains(x.IIDMaDonVi)));
                itemsMap.Select(x =>
                {
                    x.Id = Guid.Empty;
                    x.SMaBaoCao = SMaBaoCao;
                    x.STenBaoCao = itemsBaoCao.FirstOrDefault(x => x.IdType.Equals(SMaBaoCao) && !string.IsNullOrEmpty(x.Ten))?.Ten ?? string.Empty;
                    x.ILoaiBaoCao = (int)NoteTypeBhxh.AgencyDetail;
                    x.INamLamViec = iNamLamViec;

                    return x;
                }).ToList();
                ItemsModel.Select(x => x.STenDonVi = ItemsAgencies.FirstOrDefault(z => z.IIDMaDonVi.Equals(x.IIdMaDonVi))?.TenDonVi ?? x.STenBaoCao).ToList();
                ItemsModel.AddRange(itemsMap);
            }
            else if (IsAgregate && ((!ItemsModel.IsEmpty() && ItemsModel.Any(x => !ListMaBaoCao.Contains(x.SMaBaoCao))) || ItemsModel.IsEmpty() || (!ItemsModel.IsEmpty() && ListMaBaoCao.Any(x => !ItemsModel.Select(y => y.SMaBaoCao).Contains(x)))))
            {
                var listAgregateAdd = ListMaBaoCao.Where(x => !ItemsModel.Select(y => y.SMaBaoCao).Contains(x)).Select(s => new BhCauHinhBaoCaoModel
                {
                    Id = Guid.Empty,
                    IIdMaDonVi = _sessionService.Current.IdDonVi,
                    SMaBaoCao = s,
                    STenBaoCao = itemsBaoCao.FirstOrDefault(x => x.IdType.Equals(s) && !string.IsNullOrEmpty(x.Ten))?.Ten ?? s,
                    INamLamViec = iNamLamViec,
                    STenDonVi = itemsBaoCao.FirstOrDefault(x => x.IdType.Equals(s) && !string.IsNullOrEmpty(x.Ten))?.Ten ?? s,
                    ILoaiBaoCao = (int)NoteTypeBhxh.AgencySummary
                }).ToList();
                ItemsModel.Select(x => x.STenDonVi = x.STenBaoCao).ToList();
                ItemsModel.AddRange(listAgregateAdd);
            }
            else if (IsAgregate)
            {
                ItemsModel.Select(x => x.STenDonVi = x.STenBaoCao).ToList();
            }
            else if (!ItemsAgencies.IsEmpty())
            {
                ItemsModel.Select(x => x.STenDonVi = ItemsAgencies.FirstOrDefault(z => z.IIDMaDonVi.Equals(x.IIdMaDonVi))?.TenDonVi ?? string.Empty).ToList();

            }
            Items = new ObservableCollection<BhCauHinhBaoCaoModel>(ItemsModel.Where(x => !string.IsNullOrEmpty(x.STenDonVi)));
            SelectedItemChange ??= Items.FirstOrDefault(x => x.SMaBaoCao.Equals(SMaBaoCao));
            Items.ForAll(x =>
            {
                x.PropertyChanged += OnChangeItem;
                x.IsModified = true;
                if (SelectedItemChange != null && SelectedItemChange.SMaBaoCao.Equals(x.SMaBaoCao) && SelectedItemChange.IIdMaDonVi.Equals(x.IIdMaDonVi))
                {
                    x.Selected = true;
                }
            });
        }

        public override void OnSave(object obj)
        {
            if (Items.Any(x => x.Selected))
            {
                var itemsSaveChange = _mapper.Map<List<BhCauHinhBaoCao>>(Items.Where(x => x.Selected));
                var dateChange = DateTime.Now;
                string principal = _sessionService.Current.Principal;
                itemsSaveChange.ForAll(x =>
                {
                    if (x.Id.IsNullOrEmpty())
                    {
                        x.DNgayTao = dateChange;
                        x.SNguoiTao = principal;
                    }
                    else
                    {
                        x.DNgaySua = dateChange;
                        x.SNguoiSua = principal;
                    }
                });
                _ghiChuService.AddOrUpdateRange(itemsSaveChange);
                LoadDataDefault();
            }
            MessageBoxHelper.Info(Resources.MsgSaveDone);
            SavedAction?.Invoke(null);
        }
        private void OnChangeItem(object sender, PropertyChangedEventArgs args)
        {
            var obj = (BhCauHinhBaoCaoModel)sender;
            Items.ForAll(x => x.PropertyChanged -= OnChangeItem);
            if (obj.Selected)
            {
                if (IsAgregate)
                    Items.Where(x => !x.SMaBaoCao.Equals(obj.SMaBaoCao)).Select(x => x.Selected = false).ToList();
                else
                    Items.Where(x => !x.IIdMaDonVi.Equals(obj.IIdMaDonVi)).Select(x => x.Selected = false).ToList();
                TxtGhiChu = obj?.SGhiChu ?? string.Empty;
                TxtCanCu1 = obj?.SCanCu1 ?? string.Empty;
                TxtCanCu2 = obj?.SCanCu2 ?? string.Empty;
                SelectedItemChange = obj;
                OnPropertyChanged(nameof(Items));
                OnPropertyChanged(nameof(TxtGhiChu));
            }
            Items.ForAll(x => x.PropertyChanged += OnChangeItem);
        }

        public override void OnClose(object obj)
        {
            base.OnClose(obj);
            Window window = obj as Window;
            window.Close();
        }
    }
}
