using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Forex.ForexAllocation.ForexDeNghiThanhToan;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.QuyetToanNienDo
{
    public class QuyetToanNienDoTongHopViewModel : DialogCurrencyAttachmentViewModelBase<NhQtQuyetToanNienDoModel>
    {
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly INhQtQuyetToanNienDoService _service;
        private SessionInfo _sessionInfo;

        public override string Name => "Đề nghị quyết toán niên độ";
        public override string Title => "Đề nghị quyết toán niên độ";
        public override string Description => "Tổng hợp đề nghị quyết toán niên độ";

        public List<NhQtQuyetToanNienDoModel> ListQuyetToanTongHop { get; set; } = new List<NhQtQuyetToanNienDoModel>();

        private ObservableCollection<NguonNganSachModel> _itemsNguonVon;
        public ObservableCollection<NguonNganSachModel> ItemsNguonVon
        {
            get => _itemsNguonVon;
            set => SetProperty(ref _itemsNguonVon, value);
        }

        private ObservableCollection<NhDmLoaiThanhToanModel> _itemsLoaiThanhToan;
        public ObservableCollection<NhDmLoaiThanhToanModel> ItemsLoaiThanhToan
        {
            get => _itemsLoaiThanhToan;
            set => SetProperty(ref _itemsLoaiThanhToan, value);
        }

        public QuyetToanNienDoTongHopViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            INsNguonNganSachService nsNguonNganSachService,
            INhQtQuyetToanNienDoService service,
            INhDmTiGiaService nhDmTiGiaService,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService
            )
            : base(mapper, nhDmTiGiaService, nhDmTiGiaChiTietService, storageServiceFactory, attachService)
        {
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _service = service;
        }

        public override void Init()
        {
            LoadDefault();
            LoadNguonVon();
            LoadLoaiThanhToan();
            LoadData(); 
        }

        private void LoadDefault()
        {
            _sessionInfo = _sessionService.Current;
        }

        private void LoadData()
        {
            Model = new NhQtQuyetToanNienDoModel();
        }

        // Load dropdown nguồn vốn
        private void LoadNguonVon()
        {
            var data = _nsNguonNganSachService.FindAll();
            _itemsNguonVon = _mapper.Map<ObservableCollection<NguonNganSachModel>>(data);
            OnPropertyChanged(nameof(ItemsNguonVon));
        }

        // Load dropdown loại thanh toán
        private void LoadLoaiThanhToan()
        {
            _itemsLoaiThanhToan = new ObservableCollection<NhDmLoaiThanhToanModel>();
            _itemsLoaiThanhToan.Add(new NhDmLoaiThanhToanModel() { Id = 1, STen = "Cấp kinh phí" });
            _itemsLoaiThanhToan.Add(new NhDmLoaiThanhToanModel() { Id = 2, STen = "Tạm ứng" });
            _itemsLoaiThanhToan.Add(new NhDmLoaiThanhToanModel() { Id = 3, STen = "Thanh toán" });
            OnPropertyChanged(nameof(ItemsLoaiThanhToan));
        }

        public override void OnSave(object obj)
        {
            if (!Validate())
            {
                return;
            }

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                // Thêm mới bản ghi tổng hợp cha
                NhQtQuyetToanNienDo entity = new NhQtQuyetToanNienDo();
                entity = _mapper.Map<NhQtQuyetToanNienDo>(Model);
                entity.BIsActive = true;
                entity.BIsGoc = true;
                entity.BIsKhoa = false;
                entity.BIsXoa = false;
                entity.DNgayTao = DateTime.Now;
                entity.SNguoiTao = _sessionInfo.Principal;
                _service.Add(entity);

                // Update các bản ghi tổng hợp con
                foreach (var item in ListQuyetToanTongHop)
                {
                    var thChild = _service.FindById(item.Id);
                    thChild.DNgaySua = DateTime.Now;
                    thChild.SNguoiSua = _sessionInfo.Principal;
                    thChild.IID_TongHopID = entity.Id;
                    _service.Update(thChild);
                }

                e.Result = entity;
            }, (s, e) => {
                if (e.Error == null)
                {
                    // Reload data
                    Model = _mapper.Map<NhQtQuyetToanNienDoModel>(e.Result);

                    // Invoke message
                    MessageBoxHelper.Info(Resources.MsgSaveDone);

                    var view = obj as ForexDeNghiThanhToanTongHopDialog;
                    DialogHost.Close(view);
                    SavedAction?.Invoke(Model);
                    //DialogHost.CloseDialogCommand.Execute(null, null);
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        private bool Validate()
        {
            List<string> lstError = new List<string>();
            if (string.IsNullOrEmpty(Model.SSoDeNghi?.Trim()))
            {
                lstError.Add(Resources.MsgCheckSoDeNghi);
            }
            if (!Model.DNgayDeNghi.HasValue)
            {
                lstError.Add(Resources.MsgCheckNgayDeNghi);
            }
            if (lstError.Count != 0)
            {
                MessageBoxHelper.Warning(string.Join("\n", lstError));
                return false;
            }
            return true;
        }
    }
}
