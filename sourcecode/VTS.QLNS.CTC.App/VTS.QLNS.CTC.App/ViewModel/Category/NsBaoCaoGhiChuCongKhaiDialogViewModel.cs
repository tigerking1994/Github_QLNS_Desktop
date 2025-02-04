using AutoMapper;
using MaterialDesignThemes.Wpf;
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
    public class NsBaoCaoGhiChuCongKhaiDialogViewModel : DetailViewModelBase<NsCauHinhBaoCao, NsCauHinhBaoCaoModel>
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
        public override Type ContentType => typeof(NsBaoCaoGhiChuCongKhaiDialog);

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

        private ObservableCollection<ComboboxItem> _reportType = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> ReportType
        {
            get => _reportType;
            set => SetProperty(ref _reportType, value);
        }

        public ComboboxItem SelectedReportType => ReportType.FirstOrDefault(x => x.IsEnabled);

        public string SMaBaoCao
        {
            get {
                return Convert.ToInt16(SelectedReportType.ValueItem) switch
                {
                    LOAI_BAOCAO_CONGKHAI.BIEUSO_01_QĐ_CKNS => TypeChuKy.RPT_NS_DUTOAN_QD_CONGKHAINGANSACH,
                    LOAI_BAOCAO_CONGKHAI.BIEUSO_02_CKNS => TypeChuKy.RPT_NS_DUTOAN_CONGKHAI_02CKNS,
                    LOAI_BAOCAO_CONGKHAI.BIEUSO_06_CKNS => TypeChuKy.RPT_NS_DUTOAN_CONGKHAI_06CKNS,
                    LOAI_BAOCAO_CONGKHAI_QUYETTOAN.BIEUSO_04a_CKNS => TypeChuKy.RPT_NS_QUYET_TOAN_CONGKHAI_THUCHI_04a,
                    LOAI_BAOCAO_CONGKHAI_QUYETTOAN.BIEUSO_04b_CKNS => TypeChuKy.RPT_NS_QUYET_TOAN_CONGKHAI_THUCHI_04b,
                    _ => string.Empty
                };
            }
        }
        public NsBaoCaoGhiChuCongKhaiDialogViewModel(
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
            base.Init();
            SelectedItemChange = null;
            LoadDataDefault();
        }


        private string GetDefaultGhiChu()
        {
            return string.Empty;
        }

        public void LoadTypeReport()
        {
            var typeReport = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Biểu 01/QĐ-CKNS, 02/CKNS-BC - Quyết toán", ValueItem = LOAI_BAOCAO_CONGKHAI_QUYETTOAN.BIEUSO_01_QĐ_CKNS.ToString()},
                new ComboboxItem {DisplayItem = "Biểu 03/CKNS: Công khai thực hiện dự toán thu - chi NSNN", ValueItem = LOAI_BAOCAO_CONGKHAI_QUYETTOAN.BIEUSO_03_CKNS.ToString()},
                new ComboboxItem {DisplayItem = "Biểu số 04a/CKNS: Quyết toán thu, chi NSNN - Chi tiết đơn vị", ValueItem = LOAI_BAOCAO_CONGKHAI_QUYETTOAN.BIEUSO_04a_CKNS.ToString()},
                new ComboboxItem {DisplayItem = "Biểu số 04b/CKNS: Quyết toán thu chi NSNN - Tổng hợp", ValueItem = LOAI_BAOCAO_CONGKHAI_QUYETTOAN.BIEUSO_04b_CKNS.ToString()},
            };

            ReportType = new ObservableCollection<ComboboxItem>(typeReport);
            ReportType.ForAll(item =>
            {
                item.PropertyChanged += (sender, args) =>
                {
                    TxtGhiChu = Items.FirstOrDefault(x => x.SMaBaoCao == SMaBaoCao && x.SMaGhiChu == GetMaGhiChu())?.SGhiChu ?? GetDefaultGhiChu();
                };
            });
        }


        private string GetMaGhiChu()
        {
            if (SelectedReportType != null)
            {
                var data = JsonConvert.SerializeObject(new
                {
                    LoaiBaoCao = SelectedReportType.ValueItem
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
            var item = Items.FirstOrDefault(x => x.SMaBaoCao == SMaBaoCao && x.SMaGhiChu == maGhiChu);
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
            predicate = predicate.And(x => x.SMaBaoCao == TypeChuKy.RPT_NS_DUTOAN_QD_CONGKHAINGANSACH
                || x.SMaBaoCao == TypeChuKy.RPT_NS_DUTOAN_CONGKHAI_02CKNS
                || x.SMaBaoCao == TypeChuKy.RPT_NS_DUTOAN_CONGKHAI_06CKNS
                || x.SMaBaoCao == TypeChuKy.RPT_NS_QUYET_TOAN_CONGKHAI_THUCHI_04a
                || x.SMaBaoCao == TypeChuKy.RPT_NS_QUYET_TOAN_CONGKHAI_THUCHI_04b);

            var data = _ghiChuService.FindByCondition(predicate).ToList();
            Items = _mapper.Map<ObservableCollection<NsCauHinhBaoCaoModel>>(data);
            TxtGhiChu = Items.FirstOrDefault(x => x.SMaBaoCao == SMaBaoCao && x.SMaGhiChu == GetMaGhiChu())?.SGhiChu ?? GetDefaultGhiChu();
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
