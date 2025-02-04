using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexPlan.PlanOverview
{
    public class PlanOverviewStableDialogViewModel : DialogAttachmentViewModelBase<NhKhTongTheModel>
    {
        #region Private
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INhDmNhiemVuChiService _nhDmNhiemVuChiService;
        private readonly INhKhTongTheNhiemVuChiService _nhKhTongTheNhiemVuChiService;
        private readonly INhKhTongTheService _nhKhTongTheService;
        private readonly INsDonViService _nsDonViService;

        #endregion

        public override string Name => "Kế hoạch tổng thể theo năm";
        public override string Title => "Kế hoạch tổng thể theo năm";
        public override Type ContentType => typeof(View.Forex.ForexPlan.PlanOverview.PlanOverviewStableDialog);
        public bool IsInsert => Model.Id == Guid.Empty;
        public override AttachmentEnum.Type ModuleType => AttachmentEnum.Type.NH_KH_TONGTHE;
        private NhKhTongTheNhiemVuChiModel _selectedNhKhTongTheNhiemVuChi;
        public NhKhTongTheNhiemVuChiModel SelectedNhKhTongTheNhiemVuChi
        {
            get => _selectedNhKhTongTheNhiemVuChi;
            set => SetProperty(ref _selectedNhKhTongTheNhiemVuChi, value);
        }

        private ObservableCollection<NhKhTongTheNhiemVuChiModel> _nhKhTongTheNhiemVuChiItems;
        public ObservableCollection<NhKhTongTheNhiemVuChiModel> NhKhTongTheNhiemVuChiItems
        {
            get => _nhKhTongTheNhiemVuChiItems;
            set => SetProperty(ref _nhKhTongTheNhiemVuChiItems, value);
        }

        private IEnumerable<NhKhTongTheNhiemVuChiQuery> _dataKhttNhiemVuChiItems;
        public IEnumerable<NhKhTongTheNhiemVuChiQuery> DataKhttNhiemVuChiItems
        {
            get => _dataKhttNhiemVuChiItems;
            set => SetProperty(ref _dataKhttNhiemVuChiItems, value);
        }

        private ObservableCollection<ComboboxItem> _dataNhDmNhiemVuChi;
        public ObservableCollection<ComboboxItem> DataNhDmNhiemVuChi
        {
            get => _dataNhDmNhiemVuChi;
            set => SetProperty(ref _dataNhDmNhiemVuChi, value);
        }

        private ObservableCollection<ComboboxItem> _dataNamKeHoachGiaiDoan;

        public ObservableCollection<ComboboxItem> DataNamKeHoachGiaiDoan
        {
            get => _dataNamKeHoachGiaiDoan;
            set => SetProperty(ref _dataNamKeHoachGiaiDoan, value);
        }

        private ComboboxItem _selectedIIdParentId;
        public ComboboxItem SelectedIIdParentId
        {
            get => _selectedIIdParentId;
            set
            {
                SetProperty(ref _selectedIIdParentId, value);
                if (value != null)
                {
                    LoadNhDmNhiemVuChiOnChangeParent();
                    LoadKhTTNamCungGiaiDoan(value.Id);
                }
            }
        }

        private IEnumerable<NhKhTongTheNhiemVuChiQuery> _listKhttNvcNamKhacCungGiaiDoan;
        public IEnumerable<NhKhTongTheNhiemVuChiQuery> ListKhttNvcNamKhacCungGiaiDoan
        { 
            get => _listKhttNvcNamKhacCungGiaiDoan;
            set => SetProperty(ref _listKhttNvcNamKhacCungGiaiDoan, value);
        }
        private NhKhTongTheModel _nhKhTongTheModelParent;
        public NhKhTongTheModel NhKhTongTheModelParent
        {
            get => _nhKhTongTheModelParent;
            set => SetProperty(ref _nhKhTongTheModelParent, value);
        }

        private IEnumerable<NhKhTongTheNhiemVuChiQuery> _nhKhTongTheGiaiDoanNhiemVuChiItems;
        public IEnumerable<NhKhTongTheNhiemVuChiQuery> NhKhTongTheGiaiDoanNhiemVuChiItems
        {
            get => _nhKhTongTheGiaiDoanNhiemVuChiItems;
            set => SetProperty(ref _nhKhTongTheGiaiDoanNhiemVuChiItems, value);
        }

        #region RelayCommand
        public RelayCommand AddNhKhTongTheNhiemVuChiCommand { get; }
        public RelayCommand SaveNhKhTongTheNhiemVuChiCommand { get; }
        public RelayCommand DeleteNhKhTongTheNhiemVuChiCommand { get; }
        #endregion

        public PlanOverviewStableDialogViewModel(
            ISessionService sessionService,
            ILog logger,
            IMapper mapper,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            INhKhTongTheService nhKhTongTheService,
            INhKhTongTheNhiemVuChiService nhKhTongTheNhiemVuChiService,
            INhDmNhiemVuChiService nhDmNhiemVuChiService,
            INsDonViService nsDonViService,
            IVdtDmDonViThucHienDuAnService vdtDmDonViThucHienDuAnService)
            : base(mapper, storageServiceFactory, attachService)
        {
            _logger = logger;
            _sessionService = sessionService;
            _nhKhTongTheService = nhKhTongTheService;
            _nhKhTongTheNhiemVuChiService = nhKhTongTheNhiemVuChiService;
            _nhDmNhiemVuChiService = nhDmNhiemVuChiService;
            _nsDonViService = nsDonViService;

            AddNhKhTongTheNhiemVuChiCommand = new RelayCommand(obj => OnAdd(obj), obj => (bool)obj || SelectedNhKhTongTheNhiemVuChi != null);
            SaveNhKhTongTheNhiemVuChiCommand = new RelayCommand(obj => OnSave());
            DeleteNhKhTongTheNhiemVuChiCommand = new RelayCommand(obj => OnDelete(), obj => SelectedNhKhTongTheNhiemVuChi != null);
        }

        private void OnDelete()
        {
            if (SelectedNhKhTongTheNhiemVuChi != null)
            {
                if (SelectedNhKhTongTheNhiemVuChi.IsDeleted && NhKhTongTheNhiemVuChiItems.Any(n => !n.IsDeleted && n.IIdNhiemVuChiId == SelectedNhKhTongTheNhiemVuChi.IIdNhiemVuChiId))
                {
                    System.Windows.Forms.MessageBox.Show("Nhiệm vụ chi này đã được gán cho 1 đơn vị, không thể hoàn tác", Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                SelectedNhKhTongTheNhiemVuChi.IsDeleted = !SelectedNhKhTongTheNhiemVuChi.IsDeleted;
            }
            GetTongGiaTri();
            OnPropertyChanged(nameof(NhKhTongTheNhiemVuChiItems));
        }

        public override void Init()
        {
            LoadAttach();
            LoadNamKeHoach();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            if (Model.Id.IsNullOrEmpty())
            {
                DataNhDmNhiemVuChi = new ObservableCollection<ComboboxItem>();
                NhKhTongTheNhiemVuChiItems = new ObservableCollection<NhKhTongTheNhiemVuChiModel>();
                Description = "Thêm mới thông tin kế hoạch";
            }
            else
            {
                OnSetSelectedDataModel();
                LoadDataNhKhTongTheNhiemVuChiByIdNhKhTongThe(Model.Id);
                GetTongGiaTri();
                if (BIsReadOnly)
                {
                    Description = "Xem thông tin kế hoạch";
                }
                else
                {
                    Description = "Cập nhật thông tin kế hoạch";
                }
            }
            OnPropertyChanged(nameof(Description));
        }

        private void LoadDataNhKhTongTheNhiemVuChiByIdNhKhTongThe(Guid id)
        {
            List<NhKhTongTheNhiemVuChi> listNhKhTongTheNhiemVuChi = _nhKhTongTheNhiemVuChiService.FindAllByKhTongTheId(id).ToList();
            if (listNhKhTongTheNhiemVuChi == null) return;
            NhKhTongTheNhiemVuChiItems = _mapper.Map<ObservableCollection<NhKhTongTheNhiemVuChiModel>>(listNhKhTongTheNhiemVuChi.OrderBy(n => n.SMaOrder.Length).ThenBy(x => x.SMaOrder));
            foreach (var model in NhKhTongTheNhiemVuChiItems)
            {
                model.PropertyChanged += NhiemVuChi_OnPropertyChanged;
                NhKhTongTheNhiemVuChiQuery data = _nhKhTongTheNhiemVuChiService.FindOneByIdKhTongTheAndIdNhiemVuChi(SelectedIIdParentId.Id, model.IIdNhiemVuChiId.Value);
                if (data != null)
                {
                    model.TenDonVi = data.STenDonVi;
                    model.FGiaTriKhTtcpGiaiDoan = data.FGiaTriKhTTCP;
                    model.FGiaTriKhBqpGiaiDoan = data.FGiaTriKhBQP;
                }
            }
            LoadNhDmNhiemVuChi();
        }

        private void OnSetSelectedDataModel()
        {
            if (Model.IIdParentId.HasValue)
            {
                SelectedIIdParentId = DataNamKeHoachGiaiDoan.FirstOrDefault(n => n.Id == Model.IIdParentId);
                if (SelectedIIdParentId == null)
                {
                    var khtt = _nhKhTongTheService.Find(Model.IIdParentId.Value);
                    var data = _mapper.Map<NhKhTongTheModel>(khtt);
                    if (data == null) return;

                    SelectedIIdParentId = new ComboboxItem()
                    {
                        Id = data.Id,
                        ValueItem = string.Format("{0}:{1}", data.IGiaiDoanTu, data.IGiaiDoanDen),
                        DisplayItem = "KHTT " + data.SNam + " - Số KH: " + data.SSoKeHoachTtcp + " (đã bị điều chỉnh)"
                    };

                    DataNamKeHoachGiaiDoan.Add(SelectedIIdParentId);
                }
            }
        }

        public override void OnSave()
        {
            //if (!ValidateViewModelHelper.Validate(Model)) return;
            if (!ValidateData()) return;
            //if (!IsAcceptWarningData()) return;
            NhKhTongThe entity = _mapper.Map<NhKhTongThe>(Model);
            entity.ILoai = Loai_KHTT.NAM;
            if (IsDieuChinh)
            {
                if (IdDieuChinh.HasValue)
                {
                    NhKhTongThe oldEntity = _nhKhTongTheService.Find(IdDieuChinh.Value);
                    oldEntity.BIsActive = false;
                    _nhKhTongTheService.Update(oldEntity);
                    entity.IIdGocId = oldEntity.IIdGocId;
                    entity.IIdParentAdjustId = oldEntity.Id;
                    entity.Id = Guid.NewGuid();
                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionService.Current.Principal;
                    entity.DNgaySua = DateTime.Now;
                    entity.SNguoiSua = _sessionService.Current.Principal;
                    entity.IIdParentId = SelectedIIdParentId.Id;
                    entity.ILanDieuChinh = entity.ILanDieuChinh + 1;
                    entity.BIsActive = true;
                    entity.BIsGoc = false;
                    _nhKhTongTheService.Add(entity);
                    Model.Id = entity.Id;
                    SaveAttachment(entity.Id);
                    AdjustNhKhTongTheNhiemVuChi(entity.Id);
                    // sau khi save quay lai man cap nhat
                    IsDieuChinh = false;
                }
            }
            else
            {
                if (Model.Id == Guid.Empty)
                {
                    entity.Id = Guid.NewGuid();
                    entity.IIdGocId = entity.Id;
                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionService.Current.Principal;
                    entity.IIdParentId = SelectedIIdParentId.Id;
                    entity.BIsActive = true;
                    entity.BIsGoc = true;
                    entity.ILanDieuChinh = 0;
                    _nhKhTongTheService.Add(entity);
                    _mapper.Map(entity, Model);
                    SaveNhKhTongTheNhiemVuChi();
                    SaveAttachment(entity.Id);
                }
                else
                {
                    entity.DNgaySua = DateTime.Now;
                    entity.SNguoiSua = _sessionService.Current.Principal;
                    _nhKhTongTheService.Update(entity);
                    SaveNhKhTongTheNhiemVuChi();
                    SaveAttachment(entity.Id);
                }
            }

            System.Windows.Forms.MessageBox.Show(Resources.MsgSaveDone);
            SavedAction?.Invoke(_mapper.Map<NhKhTongTheModel>(entity));
            LoadData();
        }

        private void AdjustNhKhTongTheNhiemVuChi(Guid idNhKhTongThe)
        {
            var lstInsert = NhKhTongTheNhiemVuChiItems.Where(x => !x.IsDeleted && (x.Id == Guid.Empty)).ToList();
            foreach (var item in lstInsert)
            {
                item.IIdKhTongTheId = idNhKhTongThe;
            }
            List<NhKhTongTheNhiemVuChi>  listNhKhTongTheNhiemVuChi = _mapper.Map<List<NhKhTongTheNhiemVuChi>>(lstInsert);
            listNhKhTongTheNhiemVuChi.ForEach(x => x.Id = Guid.NewGuid());
            _nhKhTongTheNhiemVuChiService.AddRange(listNhKhTongTheNhiemVuChi);
        }

        private void SaveNhKhTongTheNhiemVuChi()
        {
            var lstInsert = NhKhTongTheNhiemVuChiItems.Where(x => !x.IsDeleted && x.Id.IsNullOrEmpty()).ToList();
            var lstUpdate = NhKhTongTheNhiemVuChiItems.Where(x => !x.IsDeleted && !x.Id.IsNullOrEmpty()).ToList();
            var lstDelete = NhKhTongTheNhiemVuChiItems.Where(x => x.IsDeleted && !x.Id.IsNullOrEmpty()).ToList();

            if (lstInsert != null && lstInsert.Count > 0)
            {
                OnAddNhKhTongTheNhiemVuChi(lstInsert);
            }
            if (lstUpdate != null && lstUpdate.Count > 0)
            {
                OnUpdateNhKhTongTheNhiemVuChi(lstUpdate);
            }
            if (lstDelete != null && lstDelete.Count > 0)
            {
                OnDeleteNhKhTongTheNhiemVuChi(lstDelete);
            }
        }


        private void OnDeleteNhKhTongTheNhiemVuChi(List<NhKhTongTheNhiemVuChiModel> lstDelete)
        {
            List<NhKhTongTheNhiemVuChi>  listNhKhTongTheNhiemVuChi = _mapper.Map<List<NhKhTongTheNhiemVuChi>>(lstDelete);
            _nhKhTongTheNhiemVuChiService.RemoveRange(listNhKhTongTheNhiemVuChi);
        }

        private void OnUpdateNhKhTongTheNhiemVuChi(List<NhKhTongTheNhiemVuChiModel> lstUpdate)
        {
            List<NhKhTongTheNhiemVuChi>  listNhKhTongTheNhiemVuChi = _mapper.Map<List<NhKhTongTheNhiemVuChi>>(lstUpdate);
            _nhKhTongTheNhiemVuChiService.UpdateRange(listNhKhTongTheNhiemVuChi);
        }
        private void OnAddNhKhTongTheNhiemVuChi(List<NhKhTongTheNhiemVuChiModel> lstInsert)
        {
            foreach (var item in lstInsert)
            {
                item.IIdKhTongTheId = Model.Id;
            }
            List<NhKhTongTheNhiemVuChi>  listNhKhTongTheNhiemVuChi = _mapper.Map<List<NhKhTongTheNhiemVuChi>>(lstInsert);
            listNhKhTongTheNhiemVuChi.ForEach(x => x.Id = Guid.NewGuid());
            _nhKhTongTheNhiemVuChiService.AddRange(listNhKhTongTheNhiemVuChi);
        }

        private void OnAdd(object obj)
        {
            if (SelectedIIdParentId == null)
            {
                System.Windows.Forms.MessageBox.Show("Chưa có Kế hoạch Tổng thể giai đoạn", Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_nhKhTongTheNhiemVuChiItems == null) _nhKhTongTheNhiemVuChiItems = new ObservableCollection<NhKhTongTheNhiemVuChiModel>();
            NhKhTongTheNhiemVuChiModel sourceItem = _selectedNhKhTongTheNhiemVuChi;
            NhKhTongTheNhiemVuChiModel targetItem = new NhKhTongTheNhiemVuChiModel();
            bool isParent = (bool)obj;
            int currentRow = -1;
            if (!_nhKhTongTheNhiemVuChiItems.IsEmpty())
            {
                if (sourceItem != null)
                {
                    currentRow = _nhKhTongTheNhiemVuChiItems.IndexOf(sourceItem) + CountTreeChildItems(sourceItem);
                }
                else
                {
                    // Thêm vào cuối danh sách
                    currentRow = _nhKhTongTheNhiemVuChiItems.Count() - 1;
                }
            }

            if (sourceItem != null)
            {
                targetItem.ParentNhiemVuChiId = isParent ? sourceItem.ParentNhiemVuChiId : sourceItem.IIdNhiemVuChiId;
            }

            targetItem.IIdNhiemVuChiId = Guid.NewGuid();
            targetItem.IsAdded = true;
            targetItem.IsModified = true;
            targetItem.PropertyChanged += NhiemVuChi_OnPropertyChanged;
            _nhKhTongTheNhiemVuChiItems.Insert(currentRow + 1, targetItem);
            OrderItems(targetItem.ParentNhiemVuChiId);
            UpdateTreeItems();
            OnPropertyChanged(nameof(NhKhTongTheNhiemVuChiItems));
        }

        private void GetTongGiaTri()
        {
            Model.FTongGiaTriKhbqp = NhKhTongTheNhiemVuChiItems.Where(n => !n.IsDeleted && n.FGiaTriKhBqp.HasValue).Sum(n => n.FGiaTriKhBqp.Value);
        }
        private void NhiemVuChi_OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var khttNhiemVuChiSelected = (NhKhTongTheNhiemVuChiModel)sender;
            if (khttNhiemVuChiSelected == null) return;
            switch (args.PropertyName)
            {
                case nameof(NhKhTongTheNhiemVuChiModel.FGiaTriKhBqp):
                    if (khttNhiemVuChiSelected.FGiaTriKhBqp > khttNhiemVuChiSelected.FGiaTriKhBqpGiaiDoan)
                    {
                        System.Windows.Forms.MessageBox.Show(Resources.MsgValidateValueNvcKhttBQP,
                            Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if (ListKhttNvcNamKhacCungGiaiDoan.Any())
                        {
                            double tongGiaTriKhbqpAllGiaiDoan = (khttNhiemVuChiSelected.FGiaTriKhBqp ?? 0) + (ListKhttNvcNamKhacCungGiaiDoan.Where(x => x.IIdNhiemVuChiId == khttNhiemVuChiSelected.IIdNhiemVuChiId)?.Sum(x => x.FGiaTriKhBQP) ?? 0);
                            var khttNvcGiaiDoan = NhKhTongTheGiaiDoanNhiemVuChiItems.FirstOrDefault(x => x.IIdNhiemVuChiId == khttNhiemVuChiSelected.IIdNhiemVuChiId);
                            if (khttNvcGiaiDoan != null && khttNvcGiaiDoan.FGiaTriKhBQP.HasValue && tongGiaTriKhbqpAllGiaiDoan > khttNvcGiaiDoan.FGiaTriKhBQP.Value)
                            {
                                System.Windows.Forms.MessageBox.Show(Resources.MsgValidateValueTotalNvcKhttBQP,
                                    Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    GetTongGiaTri();
                    break;

                case nameof(NhKhTongTheNhiemVuChiModel.IIdNhiemVuChiId):
                    var khttNhiemVuChiFromGiaiDoan = DataKhttNhiemVuChiItems.FirstOrDefault(x => x.IIdNhiemVuChiId == khttNhiemVuChiSelected.IIdNhiemVuChiId);
                    if (khttNhiemVuChiFromGiaiDoan == null)
                    {
                        khttNhiemVuChiFromGiaiDoan = _nhKhTongTheNhiemVuChiService.FindOneByIdKhTongTheAndIdNhiemVuChi(Model.IIdParentId.Value, khttNhiemVuChiSelected.IIdNhiemVuChiId.Value);
                    }
                    if (khttNhiemVuChiFromGiaiDoan != null)
                    {
                        khttNhiemVuChiSelected.IIdDonViThuHuongId = khttNhiemVuChiFromGiaiDoan.IIdDonViThuHuongId;
                        khttNhiemVuChiSelected.TenDonVi = khttNhiemVuChiFromGiaiDoan.STenDonVi;
                        khttNhiemVuChiSelected.FGiaTriKhBqpGiaiDoan = khttNhiemVuChiFromGiaiDoan.FGiaTriKhBQP;
                        khttNhiemVuChiSelected.FGiaTriKhTtcpGiaiDoan = khttNhiemVuChiFromGiaiDoan.FGiaTriKhTTCP;
                        khttNhiemVuChiSelected.FGiaTriKhTtcp = null;
                        khttNhiemVuChiSelected.SMaDonViThuHuong = khttNhiemVuChiFromGiaiDoan.SMaDonViThuHuong;
                    }
                    LoadNhDmNhiemVuChi();
                    break;
                case nameof(NhKhTongTheNhiemVuChiModel.IsDeleted):
                    LoadNhDmNhiemVuChi();
                    break;
            }
        }

        private bool ValidateData()
        {
            List<string> lstError = new List<string>();
            if (SelectedIIdParentId == null)
            {
                lstError.Add(string.Format(Resources.MsgCheckKHTongTheTheoGiaiDoan));
            } else
            {
                string[] namGiaiDoan = SelectedIIdParentId.ValueItem.Split(":");
                if (Model.INamKeHoach < Convert.ToInt32(namGiaiDoan[0]) || Model.INamKeHoach > Convert.ToInt32(namGiaiDoan[1]))
                {
                    lstError.Add(string.Format(Resources.MsgCheckNamKeHoachHopLe));
                }
            }
            if (!Model.INamKeHoach.HasValue)
            {
                lstError.Add(string.Format(Resources.MsgCheckNamKeHoach));
            }
            if (string.IsNullOrEmpty(Model.SSoKeHoachBqp))
            {
                lstError.Add(string.Format(Resources.MsgCheckSoKeHoachBQP));
            }
            if (Model.DNgayKeHoachBqp == null)
            {
                lstError.Add(string.Format(Resources.MsgCheckNgayBanHanhKH));
            }
            if (Model.DNgayKeHoachBqp != null && DateTime.Compare(DateTime.Now,Model.DNgayKeHoachBqp.Value) < 0)
            {
                lstError.Add(string.Format(Resources.MsgCheckNgayBanHanhKHLonHon));
            }
            bool isInvalidNhiemVuChi = NhKhTongTheNhiemVuChiItems.Where(x => !x.IsDeleted).Any(x => !x.IIdDonViThuHuongId.HasValue || !x.IIdNhiemVuChiId.HasValue || !x.FGiaTriKhBqp.HasValue || x.FGiaTriKhBqp <= 0);
            if (isInvalidNhiemVuChi)
            {
                lstError.Add(string.Format(Resources.MsgCheckNvChiHopLe));
            }
            if (Model.Id == Guid.Empty && !IsDieuChinh && Model.INamKeHoach.HasValue && SelectedIIdParentId != null)
            {
                if (_nhKhTongTheService.IsExistKhTongTheNam(SelectedIIdParentId.Id, Model.INamKeHoach.Value))
                {
                    lstError.Add(string.Format(Resources.MsgCheckNamKeHoachTonTai));
                }
            }
            if (lstError.Count != 0)
            {
                System.Windows.Forms.MessageBox.Show(string.Join("\n", lstError),
                    Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool IsAcceptWarningData()
        {
            // Warning TongGiatri khttGiaiDoan <= TongGIatri khttNam
            var khttGiaiDoan = _nhKhTongTheService.Find(_selectedIIdParentId.Id);
            if (khttGiaiDoan == null) return true;
            var lstKhttNam = _nhKhTongTheService.FindByParentId(_selectedIIdParentId.Id).ToList();

            double sum = 0;
            if (lstKhttNam == null || lstKhttNam.Count == 0)
            {
                sum = khttGiaiDoan.FTongGiaTriKhbqp;
            }
            else
            {
                sum = lstKhttNam.Where(n => n.Id != Model.Id).Sum(n => n.FTongGiaTriKhbqp);
            }

            if (Model.FTongGiaTriKhbqp + sum > khttGiaiDoan.FTongGiaTriKhbqp)
            {
                var resultConfirm = System.Windows.MessageBox.Show(Resources.SumKhttBqpConfirm, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultConfirm != MessageBoxResult.Yes) return false;
                else return true;
            }
            else
            {
                return true;
            }
        }

        private void LoadNhDmNhiemVuChiOnChangeParent()
        {
            DataKhttNhiemVuChiItems = _nhKhTongTheNhiemVuChiService.FindByIdKhTongThe(SelectedIIdParentId.Id);
            if (DataKhttNhiemVuChiItems == null) return;
            DataNhDmNhiemVuChi = _mapper.Map<ObservableCollection<ComboboxItem>>(DataKhttNhiemVuChiItems.Select(n => new ComboboxItem()
            {
                Id = n.IIdNhiemVuChiId,
                DisplayItem = n.STenNhiemVuChi,
                IsEnabled = true
            }));

            OnPropertyChanged(nameof(DataNhDmNhiemVuChi));
            if (NhKhTongTheNhiemVuChiItems != null && NhKhTongTheNhiemVuChiItems.Any())
            {
                NhKhTongTheNhiemVuChiItems.Clear();
            }
        }

        private void LoadKhTTNamCungGiaiDoan(Guid khttGiaiDoanId)
        {
            ListKhttNvcNamKhacCungGiaiDoan = _nhKhTongTheNhiemVuChiService.FindAllNvcByIdKhTongTheGiaiDoan(khttGiaiDoanId)?.Where(x => x.IIdKhTongTheId != Model.Id);
            if (ListKhttNvcNamKhacCungGiaiDoan == null)
            {
                ListKhttNvcNamKhacCungGiaiDoan = new List<NhKhTongTheNhiemVuChiQuery>();
            }
            NhKhTongTheModelParent = _mapper.Map<NhKhTongTheModel>(_nhKhTongTheService.Find(khttGiaiDoanId));
            NhKhTongTheGiaiDoanNhiemVuChiItems = _nhKhTongTheNhiemVuChiService.FindByIdKhTongThe(khttGiaiDoanId);
        }

        private void LoadNhDmNhiemVuChi()
        {
            if (DataNhDmNhiemVuChi == null || DataNhDmNhiemVuChi.Count == 0)
            {
                DataKhttNhiemVuChiItems = _nhKhTongTheNhiemVuChiService.FindByIdKhTongThe(SelectedIIdParentId.Id);
                if (DataKhttNhiemVuChiItems == null) return;
                DataNhDmNhiemVuChi = _mapper.Map<ObservableCollection<ComboboxItem>>(DataKhttNhiemVuChiItems.Select(n => new ComboboxItem()
                {
                    Id = n.IIdNhiemVuChiId,
                    DisplayItem = n.STenNhiemVuChi,
                    IsEnabled = true
                }));
            }

            foreach (var item in DataNhDmNhiemVuChi)
            {
                if (NhKhTongTheNhiemVuChiItems.Where(n => !n.IsDeleted).Select(x => x.IIdNhiemVuChiId).Contains(item.Id))
                {
                    item.IsEnabled = false;
                }
                else
                {
                    item.IsEnabled = true;
                }
            }
        }

        private void LoadNamKeHoach()
        {
            var khttList = _nhKhTongTheService.FindAll().Where(n => n.BIsActive && n.ILoai == 1);
            var dataKhttList = _mapper.Map<ObservableCollection<NhKhTongTheModel>>(khttList);
            if (dataKhttList == null) return;
            DataNamKeHoachGiaiDoan = new ObservableCollection<ComboboxItem>();
            foreach (var item in dataKhttList)
            {
                DataNamKeHoachGiaiDoan.Add(new ComboboxItem()
                {
                    Id = item.Id,
                    ValueItem = string.Format("{0}:{1}", item.IGiaiDoanTu, item.IGiaiDoanDen),
                    DisplayItem = "KHTT " + item.SNam + " - Số KH: " + item.SSoKeHoachTtcp
                });
            }
            OnPropertyChanged(nameof(DataNamKeHoachGiaiDoan));
        }

        private void DeleteTreeItems(NhKhTongTheNhiemVuChiModel currentItem, bool status)
        {
            if (currentItem != null)
            {
                var items = _nhKhTongTheNhiemVuChiItems;
                currentItem.IsDeleted = status;
                var childs = items.Where(x => x.ParentNhiemVuChiId == currentItem.IIdNhiemVuChiId);
                if (!childs.IsEmpty())
                {
                    foreach (var item in childs)
                    {
                        DeleteTreeItems(item, status);
                    }
                }
            }
        }

        private int CountTreeChildItems(NhKhTongTheNhiemVuChiModel currentItem)
        {
            var items = _nhKhTongTheNhiemVuChiItems;
            int count = 0;
            var childs = items.Where(x => x.ParentNhiemVuChiId == currentItem.IIdNhiemVuChiId);
            if (!childs.IsEmpty())
            {
                count += childs.Count();
                foreach (var item in childs)
                {
                    count += CountTreeChildItems(item);
                }
            }
            return count;
        }

        private void OrderItems(Guid? parentId = null)
        {
            var childs = _nhKhTongTheNhiemVuChiItems.Where(x => x.ParentNhiemVuChiId == parentId);
            if (!childs.IsEmpty())
            {
                var parent = _nhKhTongTheNhiemVuChiItems.FirstOrDefault(x => x.IIdNhiemVuChiId == parentId);
                int index = 1;
                foreach (var child in childs)
                {
                    if (parent != null)
                    {
                        child.SMaOrder = string.Format("{0}-{1}", parent.SMaOrder, index.ToString("D2"));
                    }
                    else
                    {
                        child.SMaOrder = index.ToString("D2");
                    }
                    child.SMaNhiemVuChi = StringUtils.ConvertMaOrder(child.SMaOrder);
                    OrderItems(child.IIdNhiemVuChiId);
                    index++;
                }
            }
        }

        private void UpdateTreeItems()
        {
            if (!_nhKhTongTheNhiemVuChiItems.IsEmpty())
            {
                _nhKhTongTheNhiemVuChiItems.ForAll(s => s.IsEnabled = !_nhKhTongTheNhiemVuChiItems.Any(y => y.ParentNhiemVuChiId == s.IIdNhiemVuChiId));
                _nhKhTongTheNhiemVuChiItems.ForAll(x =>
                {
                    if (x.ParentNhiemVuChiId.IsNullOrEmpty() || !_nhKhTongTheNhiemVuChiItems.Any(y => y.IIdNhiemVuChiId == x.ParentNhiemVuChiId)) x.IsHangCha = true;
                    if (!x.IsEnabled) x.IsHangCha = true;
                    else if (_nhKhTongTheNhiemVuChiItems.Any(y => y.ParentNhiemVuChiId == x.ParentNhiemVuChiId && !y.IsEnabled)) x.IsHangCha = true;
                });
            };
        }

        public void NhiemVuChi_BeginningEditHanlder(DataGridBeginningEditEventArgs e)
        {
            SelectedNhKhTongTheNhiemVuChi = (NhKhTongTheNhiemVuChiModel)e.Row.Item;

            if (e.Column.SortMemberPath.Equals(nameof(NhKhTongTheNhiemVuChiModel.FGiaTriKhTtcp)) || e.Column.SortMemberPath.Equals(nameof(NhKhTongTheNhiemVuChiModel.FGiaTriKhBqp)))
            {
                if (SelectedNhKhTongTheNhiemVuChi != null && !SelectedNhKhTongTheNhiemVuChi.IsEnabled)
                {
                    e.Cancel = true;
                }
            }
        }

        private void CalculateNhiemVuChi()
        {
            var parents = NhKhTongTheNhiemVuChiItems.Where(x => x.ParentNhiemVuChiId.IsNullOrEmpty() || !NhKhTongTheNhiemVuChiItems.Any(y => y.IIdNhiemVuChiId == x.ParentNhiemVuChiId));
            foreach (var item in parents)
            {
                CalculateNhiemVuChi(item);
            }
        }

        private void CalculateNhiemVuChi(NhKhTongTheNhiemVuChiModel parentItem)
        {
            var childs = NhKhTongTheNhiemVuChiItems.Where(x => x.ParentNhiemVuChiId == parentItem.IIdNhiemVuChiId && !x.IsDeleted);
            if (!childs.IsEmpty())
            {
                foreach (var item in childs)
                {
                    CalculateNhiemVuChi(item);
                    parentItem.FGiaTriKhBqp = childs.Sum(x => x.FGiaTriKhBqp);
                    parentItem.FGiaTriKhTtcp = childs.Sum(x => x.FGiaTriKhTtcp);
                }
            }
        }
    }
}
