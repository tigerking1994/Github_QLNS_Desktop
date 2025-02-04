using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Helper;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexPlan.PlanOverview
{
    public class PlanOverviewStageDialogViewModel : DialogAttachmentViewModelBase<NhKhTongTheModel>
    {
        #region Private
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INhDmNhiemVuChiService _nhDmNhiemVuChiService;
        private readonly INhKhTongTheNhiemVuChiService _nhKhTongTheNhiemVuChiService;
        private readonly INhKhTongTheService _nhKhTongTheService;
        private readonly INsDonViService _nsDonViService;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        #endregion

        public override string Name => IsViewGiaiDoan ? "Kế hoạch tổng thể theo giai đoạn" : "Kế hoạch tổng thể theo năm";
        public override string Title => IsViewGiaiDoan ? "Kế hoạch tổng thể theo giai đoạn" : "Kế hoạch tổng thể theo năm";
        public override Type ContentType => typeof(View.Forex.ForexPlan.PlanOverview.PlanOverviewStageDialog);
        public bool IsInsert => Model.Id == Guid.Empty;
        public override AttachmentEnum.Type ModuleType => AttachmentEnum.Type.NH_KH_TONGTHE;
        public double FTongGiaTriKhTtcp { get; set; }
        public double FTongGiaTriKhBqpGiaiDoan { get; set; }

        private NhKhTongTheNhiemVuChiModel _selectedKhTongTheNhiemVuChi;
        public NhKhTongTheNhiemVuChiModel SelectedKhTongTheNhiemVuChi
        {
            get => _selectedKhTongTheNhiemVuChi;
            set => SetProperty(ref _selectedKhTongTheNhiemVuChi, value);
        }

        private ObservableCollection<NhKhTongTheNhiemVuChiModel> _itemsKhTongTheNhiemVuChi;
        public ObservableCollection<NhKhTongTheNhiemVuChiModel> ItemsKhTongTheNhiemVuChi
        {
            get => _itemsKhTongTheNhiemVuChi;
            set => SetProperty(ref _itemsKhTongTheNhiemVuChi, value);
        }

        private ObservableCollection<ComboboxItem> _itemsDonVi;
        public ObservableCollection<ComboboxItem> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private ComboboxItem _selectedDonVi;
        public ComboboxItem SelectedDonVi
        {
            get => _selectedDonVi;
            set => SetProperty(ref _selectedDonVi, value);
        }

        private ObservableCollection<ComboboxItem> _dataNhDmNhiemVuChi;
        public ObservableCollection<ComboboxItem> DataNhDmNhiemVuChi
        {
            get => _dataNhDmNhiemVuChi;
            set => SetProperty(ref _dataNhDmNhiemVuChi, value);
        }

        //private ObservableCollection<NhKhTongTheModel> _itemsKHGiaiDoan;
        //public ObservableCollection<NhKhTongTheModel> ItemsKHGiaiDoan
        //{
        //    get => _itemsKHGiaiDoan;
        //    set => SetProperty(ref _itemsKHGiaiDoan, value);
        //}

        //private NhKhTongTheModel _selectedKHGiaiDoan;
        //public NhKhTongTheModel SelectedKHGiaiDoan
        //{
        //    get => _selectedKHGiaiDoan;
        //    set
        //    {
        //        if (SetProperty(ref _selectedKHGiaiDoan, value))
        //        {
        //            if (_selectedKHGiaiDoan != null)
        //            {
        //                LoadKhTongTheNhiemVuChi(_selectedKHGiaiDoan.Id);
        //                GetTongGiaTri();
        //            }
        //        }
        //    }
        //}

        private bool _isAddChild;
        public bool IsAddChild
        {
            get => _isAddChild;
            set => SetProperty(ref _isAddChild, value);
        }

        private bool _isViewGiaiDoan;
        public bool IsViewGiaiDoan
        {
            get => _isViewGiaiDoan;
            set => SetProperty(ref _isViewGiaiDoan, value);
        }

        public bool IsViewNam => !IsViewGiaiDoan;
        private string MoTaChiTietTTCPStr { get; set; }
        private bool IsFirstEditMoTaChiTietTTCP { get; set; }
        private string MoTaChiTietBQPStr { get; set; }
        private bool IsFirstEditMoTaChiTietBQP { get; set; }
        private bool IsChangeMoTaTTCP { get; set; }
        private bool IsSave { get; set; }
        private bool IsShowMessageError { get; set; }
        private NhKhTongTheModel ModelClone = new NhKhTongTheModel();

        #region RelayCommand
        public RelayCommand AddNhKhTongTheNhiemVuChiCommand { get; }
        public RelayCommand SaveNhKhTongTheNhiemVuChiCommand { get; }
        public RelayCommand DeleteNhKhTongTheNhiemVuChiCommand { get; }
        #endregion

        public PlanOverviewStageDialogViewModel(
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            INhKhTongTheService nhKhTongTheService,
            INhKhTongTheNhiemVuChiService nhKhTongTheNhiemVuChiService,
            INhDmNhiemVuChiService nhDmNhiemVuChiService,
            INsDonViService nsDonViService)
            : base(mapper, storageServiceFactory, attachService)
        {
            _logger = logger;
            _sessionService = sessionService;
            _nhKhTongTheService = nhKhTongTheService;
            _nhKhTongTheNhiemVuChiService = nhKhTongTheNhiemVuChiService;
            _nhDmNhiemVuChiService = nhDmNhiemVuChiService;
            _nsDonViService = nsDonViService;

            AddNhKhTongTheNhiemVuChiCommand = new RelayCommand(obj => OnAdd(obj), obj => ((bool)obj || SelectedKhTongTheNhiemVuChi != null) && !BIsReadOnly);
            SaveNhKhTongTheNhiemVuChiCommand = new RelayCommand(obj => OnSave(obj));
            DeleteNhKhTongTheNhiemVuChiCommand = new RelayCommand(obj => OnDelete(), obj => SelectedKhTongTheNhiemVuChi != null && !BIsReadOnly
                        && (ItemsKhTongTheNhiemVuChi.Any(n => !n.IsDeleted && SelectedKhTongTheNhiemVuChi.ParentNhiemVuChiId != null && n.IIdNhiemVuChiId == SelectedKhTongTheNhiemVuChi.ParentNhiemVuChiId)
                            || SelectedKhTongTheNhiemVuChi.ParentNhiemVuChiId == null));
        }

        public override void Init()
        {
            //LoadKeHoachGiaiDoan();
            ModelClone = Model.Clone();
            LoadDonVi();
            LoadData();
        }

        public override void OnClosing()
        {
            if (!ItemsDonVi.IsEmpty()) ItemsDonVi.Clear();
            //if (!ItemsKHGiaiDoan.IsEmpty()) ItemsKHGiaiDoan.Clear();
            if (!ItemsKhTongTheNhiemVuChi.IsEmpty()) ItemsKhTongTheNhiemVuChi.Clear();
            if (!IsSave)
            {
                Model.IGiaiDoanTu_TTCP = ModelClone.IGiaiDoanTu_TTCP;
                Model.IGiaiDoanDen_TTCP = ModelClone.IGiaiDoanDen_TTCP;
                Model.IGiaiDoanTu_BQP = ModelClone.IGiaiDoanTu_BQP;
                Model.IGiaiDoanDen_BQP = ModelClone.IGiaiDoanDen_BQP;
                Model.SMoTaChiTietKhttcp = ModelClone.SMoTaChiTietKhttcp;
                Model.SMoTaChiTietKhbqp = ModelClone.SMoTaChiTietKhbqp;
            }
        }

        public override void OnClose(object obj)
        {
            if (obj is Window window)
            {
                window.Close();
            }
        }

        public override void LoadData(params object[] args)
        {
            if (Model.Id.IsNullOrEmpty())
            {
                Description = "Thêm mới thông tin kế hoạch";
            }
            else
            {
                if (BIsReadOnly)
                {
                    Description = "Xem thông tin kế hoạch";
                }
                else if (IsDieuChinh)
                {
                    Description = "Điều chỉnh thông tin kế hoạch";
                }
                else
                {
                    Description = "Cập nhật thông tin kế hoạch";
                }
                //_selectedKHGiaiDoan = ItemsKHGiaiDoan.FirstOrDefault(s => s.Id == Model.IIdParentId);

                LoadKhTongTheNhiemVuChi(Model.Id);
                GetTongGiaTri();
            }
            IsChangeMoTaTTCP = false;
            IsSave = false;
            IsShowMessageError = false;
            if (!BIsReadOnly)
            {
                IsFirstEditMoTaChiTietTTCP = true;
                IsFirstEditMoTaChiTietBQP = true;
                if (!Model.Id.IsNullOrEmpty())
                {
                    if (Model.IGiaiDoanTu_TTCP != null && Model.IGiaiDoanDen_TTCP != null && Model.IGiaiDoanTu_TTCP.HasValue && Model.IGiaiDoanDen_TTCP.HasValue)
                    {
                        string moTaTTCPStr = string.Empty;
                        string suffixTTCP = Model.IGiaiDoanTu_TTCP.Value == Model.IGiaiDoanDen_TTCP.Value ? ("năm " + Model.IGiaiDoanTu_TTCP.Value) : ("giai đoạn " + Model.IGiaiDoanTu_TTCP.Value + " ~ " + Model.IGiaiDoanDen_TTCP.Value);
                        moTaTTCPStr = "Kế hoạch tổng thể Thủ tướng Chính phủ phê duyệt " + suffixTTCP;
                        IsFirstEditMoTaChiTietTTCP = Model.SMoTaChiTietKhttcp.IsEmpty("") || moTaTTCPStr.Equals(Model.SMoTaChiTietKhttcp);
                        MoTaChiTietTTCPStr = moTaTTCPStr;
                    }

                    if (Model.IGiaiDoanTu_BQP != null && Model.IGiaiDoanDen_BQP != null && Model.IGiaiDoanTu_BQP.HasValue && Model.IGiaiDoanDen_BQP.HasValue)
                    {
                        string moTaBQPStr = string.Empty;
                        string suffixBQP = Model.IGiaiDoanTu_BQP.Value == Model.IGiaiDoanDen_BQP.Value ? ("năm " + Model.IGiaiDoanTu_BQP.Value) : ("giai đoạn " + Model.IGiaiDoanTu_BQP.Value + " ~ " + Model.IGiaiDoanDen_BQP.Value);
                        moTaBQPStr = "Kế hoạch tổng thể Bộ quốc phòng phê duyệt " + suffixBQP;
                        IsFirstEditMoTaChiTietBQP = Model.SMoTaChiTietKhbqp.IsEmpty("") || moTaBQPStr.Equals(Model.SMoTaChiTietKhbqp);
                        MoTaChiTietBQPStr = moTaBQPStr;
                    }
                }
            }

            Model.PropertyChanged += Model_PropertyChanged;
            //OnPropertyChanged(nameof(SelectedKHGiaiDoan));
            OnPropertyChanged(nameof(Description));
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var selected = (NhKhTongTheModel)sender;
            switch (e.PropertyName)
            {
                //case nameof(NhKhTongTheModel.IGiaiDoanTu):
                //    selected.IGiaiDoanDen = selected.IGiaiDoanTu + 5;
                //    break;
                case nameof(NhKhTongTheModel.IGiaiDoanTu_TTCP):
                case nameof(NhKhTongTheModel.IGiaiDoanDen_TTCP):
                    if (e.PropertyName.Equals(nameof(NhKhTongTheModel.IGiaiDoanTu_TTCP)))
                    {
                        //selected.IGiaiDoanDen_TTCP = selected.IGiaiDoanTu_TTCP + 4;
                    }
                    if (IsFirstEditMoTaChiTietTTCP)
                    {
                        if (!selected.SMoTaChiTietKhttcp.IsEmpty("") && !selected.SMoTaChiTietKhttcp.Equals(MoTaChiTietTTCPStr))
                        {
                            if (!IsChangeMoTaTTCP)
                            {
                                IsFirstEditMoTaChiTietTTCP = false;
                            }
                            else
                            {
                                IsChangeMoTaTTCP = false;
                            }
                        }
                        else
                        {
                            if (selected.IGiaiDoanTu_TTCP != null && selected.IGiaiDoanDen_TTCP != null && selected.IGiaiDoanTu_TTCP.HasValue && selected.IGiaiDoanDen_TTCP.HasValue)
                            {
                                string suffixTTCP = selected.IGiaiDoanTu_TTCP.Value == selected.IGiaiDoanDen_TTCP.Value ? ("năm " + selected.IGiaiDoanTu_TTCP.Value) : ("giai đoạn " + selected.IGiaiDoanTu_TTCP.Value + " ~ " + selected.IGiaiDoanDen_TTCP.Value);
                                string moTaTTCPStr = "Kế hoạch tổng thể Thủ tướng Chính phủ phê duyệt " + suffixTTCP;
                                selected.SMoTaChiTietKhttcp = moTaTTCPStr;
                                MoTaChiTietTTCPStr = moTaTTCPStr;
                                IsChangeMoTaTTCP = true;
                            }
                        }
                    }
                    break;
                case nameof(NhKhTongTheModel.IGiaiDoanTu_BQP):
                case nameof(NhKhTongTheModel.IGiaiDoanDen_BQP):
                    if (IsFirstEditMoTaChiTietBQP)
                    {
                        if (!selected.SMoTaChiTietKhbqp.IsEmpty("") && !selected.SMoTaChiTietKhbqp.Equals(MoTaChiTietBQPStr))
                        {
                            IsFirstEditMoTaChiTietBQP = false;
                        }
                        else
                        {
                            if (selected.IGiaiDoanTu_BQP != null && selected.IGiaiDoanDen_BQP != null && selected.IGiaiDoanTu_BQP.HasValue && selected.IGiaiDoanDen_BQP.HasValue)
                            {
                                string suffixBQP = selected.IGiaiDoanTu_BQP.Value == selected.IGiaiDoanDen_BQP.Value ? ("năm " + selected.IGiaiDoanTu_BQP.Value) : ("giai đoạn " + selected.IGiaiDoanTu_BQP.Value + " ~ " + selected.IGiaiDoanDen_BQP.Value);
                                string moTaBQPStr = "Kế hoạch tổng thể Bộ quốc phòng phê duyệt " + suffixBQP;
                                selected.SMoTaChiTietKhbqp = moTaBQPStr;
                                MoTaChiTietBQPStr = moTaBQPStr;
                            }
                        }
                    }
                    break;
                case nameof(NhKhTongTheModel.SMoTaChiTietKhttcp):
                    if (selected.SMoTaChiTietKhttcp.IsEmpty(""))
                    {
                        IsFirstEditMoTaChiTietTTCP = true;
                        IsChangeMoTaTTCP = false;
                    }
                    break;
                case nameof(NhKhTongTheModel.SMoTaChiTietKhbqp):
                    if (selected.SMoTaChiTietKhbqp.IsEmpty(""))
                    {
                        IsFirstEditMoTaChiTietBQP = true;
                    }
                    break;
            }
        }

        private void LoadKhTongTheNhiemVuChi(Guid id)
        {
            List<NhKhTongTheNhiemVuChiQuery> listNhKhTongTheNhiemVuChi = _nhKhTongTheNhiemVuChiService.FindKHTongTheAndDmNhiemVuChi(id).ToList();
            if (listNhKhTongTheNhiemVuChi == null) return;
            ItemsKhTongTheNhiemVuChi = _mapper.Map<ObservableCollection<NhKhTongTheNhiemVuChiModel>>(listNhKhTongTheNhiemVuChi);
            foreach (var model in ItemsKhTongTheNhiemVuChi)
            {
                model.SMaNhiemVuChi = StringUtils.ConvertMaOrder(model.SMaOrder);
                model.PropertyChanged += NhiemVuChi_OnPropertyChanged;
            }
            UpdateTreeItems();
        }

        private bool ValidateData()
        {
            List<string> lstError = new List<string>();
            //if (Model.INamKeHoach == null)
            //{
            //    lstError.Add("Chưa điền năm kế hoạch !");
            //}
            //if (Model.INamKeHoach > 9999)
            //{
            //    lstError.Add("Năm kế hoạch chỉ có thể nhập tối đa 4 kí tự !");
            //}
            //if (Model.INamKeHoach < 1000)
            //{
            //    lstError.Add("Năm kế hoạch phải lớn hơn 1000 !");
            //}

            if (Model.IGiaiDoanTu_TTCP.HasValue && Model.IGiaiDoanDen_TTCP.HasValue)
            {
                if (Model.IGiaiDoanTu_TTCP.Value > Model.IGiaiDoanDen_TTCP.Value)
                {
                    lstError.Add(string.Format(Resources.MsgCheckGiaiDoan1));
                }
            }
            if (Model.IGiaiDoanTu_BQP.HasValue && Model.IGiaiDoanDen_BQP.HasValue)
            {
                if (Model.IGiaiDoanTu_BQP.Value > Model.IGiaiDoanDen_BQP.Value)
                {
                    lstError.Add(string.Format(Resources.MsgCheckGiaiDoan2));
                }
            }
            if (Model.IGiaiDoanTu_BQP.HasValue)
            {
                if ((Model.IGiaiDoanTu_TTCP.HasValue && Model.IGiaiDoanTu_BQP.Value < Model.IGiaiDoanTu_TTCP.Value)
                    || (Model.IGiaiDoanDen_TTCP.HasValue && Model.IGiaiDoanTu_BQP.Value > Model.IGiaiDoanDen_TTCP.Value))
                {
                    lstError.Add(string.Format(Resources.MsgCheckGiaiDoan3));
                }
            }
            if (Model.IGiaiDoanDen_BQP.HasValue)
            {
                if ((Model.IGiaiDoanDen_TTCP.HasValue && Model.IGiaiDoanDen_BQP.Value > Model.IGiaiDoanDen_TTCP.Value)
                    || (Model.IGiaiDoanTu_TTCP.HasValue && Model.IGiaiDoanDen_BQP.Value < Model.IGiaiDoanTu_TTCP.Value))
                {
                    lstError.Add(string.Format(Resources.MsgCheckGiaiDoan4));
                }
            }

            if (lstError.Count != 0)
            {
                System.Windows.Forms.MessageBox.Show(string.Join("\n", lstError), Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public override void OnSave(object obj)
        {
            //if (Model.ILoai == Loai_KHTT.GIAIDOAN)
            //{
            //    if (!ValidateViewModelHelper.Validate(Model)) return;
            //}
            //else
            //{
            //    NhKhTongTheValidateModel NhKhTongTheValidate = new NhKhTongTheValidateModel();
            //    NhKhTongTheValidate.SSoKeHoachBqp = Model.SSoKeHoachBqp;
            //    NhKhTongTheValidate.DNgayKeHoachBqp = Model.DNgayKeHoachBqp;
            //    NhKhTongTheValidate.SMoTaChiTietKhbqp = Model.SMoTaChiTietKhbqp;
            //    if (!ValidateViewModelHelper.Validate(NhKhTongTheValidate)) return;
            //    if (!ValidateData())
            //    {
            //        return;
            //    };
            //}

            if (!ValidateViewModelHelper.Validate(Model)) return;
            if (!ValidateData()) return;

            NhKhTongThe entity = _mapper.Map<NhKhTongThe>(Model);
            if (IsDieuChinh)
            {
                // Điều chỉnh
                entity = _mapper.Map<NhKhTongThe>(Model);
                entity.Id = Guid.NewGuid();
                entity.IIdGocId = Model.IIdGocId;
                entity.IIdParentAdjustId = Model.Id;
                entity.DNgayTao = DateTime.Now;
                entity.SNguoiTao = _sessionService.Current.Principal;
                entity.DNgaySua = DateTime.Now;
                entity.SNguoiSua = _sessionService.Current.Principal;
                entity.ILanDieuChinh = entity.ILanDieuChinh + 1;
                entity.BIsActive = true;
                entity.BIsGoc = false;
                //if (IsViewNam && _selectedKHGiaiDoan != null)
                //{
                //    entity.IIdParentId = _selectedKHGiaiDoan.Id;
                //}
                _mapper.Map(entity, Model);
                AdjustNhKhTongThe(entity);
                IsDieuChinh = false;
            }
            else if (IsAddChild && Model.ILoai == Loai_KHTT.GIAIDOAN)
            {
                // Tạo mới giai đoạn con
                var parentId = entity.Id;
                entity.Id = Guid.NewGuid();
                entity.IIdParentId = parentId;
                entity.IIdGocId = entity.Id;
                entity.DNgayTao = DateTime.Now;
                entity.SNguoiTao = _sessionService.Current.Principal;
                entity.BIsActive = true;
                entity.BIsGoc = true;
                entity.ILanDieuChinh = 0;
                AddNhKhTongThe(entity);
            }
            else
            {
                if (Model.Id.IsNullOrEmpty())
                {
                    // Thêm mới
                    entity.Id = Guid.NewGuid();
                    entity.IIdGocId = entity.Id;
                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionService.Current.Principal;
                    entity.BIsActive = true;
                    entity.BIsGoc = true;
                    entity.ILanDieuChinh = 0;
                    //if (IsViewNam && _selectedKHGiaiDoan != null)
                    //{
                    //    entity.IIdParentId = _selectedKHGiaiDoan.Id;
                    //}
                    _mapper.Map(entity, Model);
                    AddNhKhTongThe(entity);
                }
                else
                {
                    // Cập nhật
                    entity.DNgaySua = DateTime.Now;
                    entity.SNguoiSua = _sessionService.Current.Principal;
                    UpdateNhKhTongThe(entity);
                }
            }

            MessageBoxHelper.Info(Resources.MsgSaveDone);
            //System.Windows.Forms.MessageBox.Show(Resources.MsgSaveDone);

            SavedAction?.Invoke(_mapper.Map<NhKhTongTheModel>(entity));
            LoadData();
            IsSave = true;
            // Save xong không out ra màn index
            //var view = obj as Window;
            //if (view != null) view.Close();
            //LoadData();
        }

        private void AdjustNhKhTongThe(NhKhTongThe entity)
        {
            var lstInsert = ItemsKhTongTheNhiemVuChi.Where(x => !x.IsDeleted).ToList();
            foreach (var item in lstInsert)
            {
                item.Id = Guid.NewGuid();
                item.IIdKhTongTheId = entity.Id;
                item.IsAdded = true;
                item.IIdNhiemVuChiId = Guid.NewGuid();
            }
            List<NhKhTongTheNhiemVuChi> listNhKhTongTheNhiemVuChi = _mapper.Map<List<NhKhTongTheNhiemVuChi>>(lstInsert);
            _nhKhTongTheService.Adjust(entity, listNhKhTongTheNhiemVuChi);
        }

        private bool ValidateNhiemVuChi(int countNhiemVuChi)
        {
            List<string> lstError = new List<string>();
            if (countNhiemVuChi == 0)
            {
                lstError.Add(string.Format(Resources.MsgCheckThongTinChuongTrinh));
            }
            if (lstError.Count != 0)
            {
                System.Windows.Forms.MessageBox.Show(string.Join("\n", lstError), Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void AddNhKhTongThe(NhKhTongThe entity)
        {
            List<NhKhTongTheNhiemVuChi> listNhKhTongTheNhiemVuChi = _mapper.Map<List<NhKhTongTheNhiemVuChi>>(ItemsKhTongTheNhiemVuChi);
            if (!ValidateNhiemVuChi(listNhKhTongTheNhiemVuChi.Count))
            {
                return;
            };

            //if (IsAddChild)
            //{
            //    List<NhKhTongTheNhiemVuChi> listNhKhTongTheNhiemVuChiNew = new List<NhKhTongTheNhiemVuChi>();
            //    List<NhKhTongTheNhiemVuChi> listNhKhTongTheNhiemVuChiParentRoot = listNhKhTongTheNhiemVuChi.Where(x => x.ParentNhiemVuChiId == null).ToList();
            //    List<NhKhTongTheNhiemVuChi> listNhKhTongTheNhiemVuChiChild;
            //    NhKhTongTheNhiemVuChi nhKhTongTheNhiemVuChi;
            //    NhKhTongTheNhiemVuChi nhKhTongTheNhiemVuChiChild;
            //    foreach (NhKhTongTheNhiemVuChi nvcParentRoot in listNhKhTongTheNhiemVuChiParentRoot)
            //    {
            //        nhKhTongTheNhiemVuChi = nvcParentRoot.Clone();
            //        nhKhTongTheNhiemVuChi.IIdNhiemVuChiId = Guid.NewGuid();
            //        listNhKhTongTheNhiemVuChiNew.Add(nhKhTongTheNhiemVuChi);
            //        listNhKhTongTheNhiemVuChiChild = listNhKhTongTheNhiemVuChi.Where(x => x.ParentNhiemVuChiId.HasValue && x.ParentNhiemVuChiId.Value.Equals(nvcParentRoot.IIdNhiemVuChiId)).ToList();
            //        foreach(NhKhTongTheNhiemVuChi child in listNhKhTongTheNhiemVuChiChild)
            //        {
            //            nhKhTongTheNhiemVuChiChild = child.Clone();
            //            nhKhTongTheNhiemVuChiChild.IIdNhiemVuChiId = Guid.NewGuid();
            //            nhKhTongTheNhiemVuChiChild.ParentNhiemVuChiId = nhKhTongTheNhiemVuChi.IIdNhiemVuChiId;
            //            listNhKhTongTheNhiemVuChiNew.Add(nhKhTongTheNhiemVuChiChild);
            //            RecursiveNvc(ref listNhKhTongTheNhiemVuChiNew, listNhKhTongTheNhiemVuChi, child.IIdNhiemVuChiId);
            //        }
            //    }
            //    listNhKhTongTheNhiemVuChi = listNhKhTongTheNhiemVuChiNew;
            //}
            listNhKhTongTheNhiemVuChi.ForEach(s => s.IsAdded = true);
            _nhKhTongTheService.Add(entity, listNhKhTongTheNhiemVuChi);
        }

        private void RecursiveNvc(ref List<NhKhTongTheNhiemVuChi> listNhKhTongTheNhiemVuChiNew, List<NhKhTongTheNhiemVuChi> listNhKhTongTheNhiemVuChi, Guid idNhiemVuChiId)
        {
            NhKhTongTheNhiemVuChi nhKhTongTheNhiemVuChiChild;
            List<NhKhTongTheNhiemVuChi> listNhKhTongTheNhiemVuChiChild = listNhKhTongTheNhiemVuChi.Where(x => x.ParentNhiemVuChiId.HasValue && x.ParentNhiemVuChiId.Value.Equals(idNhiemVuChiId)).ToList();
            foreach (NhKhTongTheNhiemVuChi child in listNhKhTongTheNhiemVuChiChild)
            {
                nhKhTongTheNhiemVuChiChild = child.Clone();
                nhKhTongTheNhiemVuChiChild.IIdNhiemVuChiId = Guid.NewGuid();
                nhKhTongTheNhiemVuChiChild.ParentNhiemVuChiId = nhKhTongTheNhiemVuChiChild.IIdNhiemVuChiId;
                listNhKhTongTheNhiemVuChiNew.Add(nhKhTongTheNhiemVuChiChild);
                RecursiveNvc(ref listNhKhTongTheNhiemVuChiNew, listNhKhTongTheNhiemVuChi, child.IIdNhiemVuChiId);
            }
        }

        private void UpdateNhKhTongThe(NhKhTongThe entity)
        {
            List<NhKhTongTheNhiemVuChi> listNhKhTongTheNhiemVuChi = _mapper.Map<List<NhKhTongTheNhiemVuChi>>(ItemsKhTongTheNhiemVuChi);
            _nhKhTongTheService.Update(entity, listNhKhTongTheNhiemVuChi);
        }

        private void OnAdd(object obj)
        {
            if (_itemsKhTongTheNhiemVuChi == null) _itemsKhTongTheNhiemVuChi = new ObservableCollection<NhKhTongTheNhiemVuChiModel>();
            NhKhTongTheNhiemVuChiModel sourceItem = _selectedKhTongTheNhiemVuChi;
            NhKhTongTheNhiemVuChiModel targetItem = new NhKhTongTheNhiemVuChiModel();
            bool isParent = (bool)obj;
            int currentRow = -1;
            if (!_itemsKhTongTheNhiemVuChi.IsEmpty())
            {
                if (sourceItem != null)
                {
                    int indexCurrent = _itemsKhTongTheNhiemVuChi.IndexOf(sourceItem);
                    if (sourceItem.ParentNhiemVuChiId == null && isParent)
                    {
                        currentRow = _itemsKhTongTheNhiemVuChi.Count() - 1;
                    }
                    else
                    {
                        currentRow = indexCurrent + CountTreeChildItems(sourceItem);
                        if (sourceItem.ParentNhiemVuChiId != null && isParent)
                        {
                            currentRow += CountTreeChildItems(sourceItem, true);
                        }
                    }
                }
                else
                {
                    // Thêm vào cuối danh sách
                    currentRow = _itemsKhTongTheNhiemVuChi.Count() - 1;
                }
            }

            if (sourceItem != null)
            {
                if (isParent)
                {
                    targetItem.ParentNhiemVuChiId = sourceItem.ParentNhiemVuChiId;
                }
                else
                {
                    targetItem.ParentNhiemVuChiId = sourceItem.IIdNhiemVuChiId;
                    targetItem.IIdDonViThuHuongId = sourceItem.IIdDonViThuHuongId;
                    targetItem.SMaDonViThuHuong = sourceItem.SMaDonViThuHuong;


                }
            }

            targetItem.IIdNhiemVuChiId = Guid.NewGuid();
            targetItem.IsAdded = true;
            targetItem.IsModified = true;
            targetItem.PropertyChanged += NhiemVuChi_OnPropertyChanged;
            _itemsKhTongTheNhiemVuChi.Insert(currentRow + 1, targetItem);
            OrderItems(targetItem.ParentNhiemVuChiId);
            UpdateTreeItems();
            OnPropertyChanged(nameof(ItemsKhTongTheNhiemVuChi));
        }

        private void GetTongGiaTri()
        {
            var sumData = ItemsKhTongTheNhiemVuChi.Where(n => !n.IsDeleted && n.ParentNhiemVuChiId == null && n.FGiaTriKhTtcp.HasValue && n.FGiaTriKhBqp.HasValue);
            Model.FTongGiaTriKhttcp = sumData.Sum(n => n.FGiaTriKhTtcp != null ? n.FGiaTriKhTtcp.Value : 0);
            Model.FTongGiaTriKhbqp = sumData.Sum(n => n.FGiaTriKhBqp != null ? n.FGiaTriKhBqp.Value : 0);
            //Model.FTongGiaTriKhbqpVnd = sumData.Sum(n => n.FGiaTriKhBqpVnd != null ? n.FGiaTriKhBqpVnd.Value : 0);
        }

        private void NhiemVuChi_OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            NhKhTongTheNhiemVuChiModel nhiemVuChiSelected = (NhKhTongTheNhiemVuChiModel)sender;
            if (nhiemVuChiSelected == null) return;
            switch (args.PropertyName)
            {
                case nameof(NhKhTongTheNhiemVuChiModel.STenNhiemVuChi):
                    nhiemVuChiSelected.IsModified = true;
                    break;
                case nameof(NhKhTongTheNhiemVuChiModel.FGiaTriKhBqp):
                    //if (!IsShowMessageError && nhiemVuChiSelected.FGiaTriKhTtcp.HasValue && nhiemVuChiSelected.FGiaTriKhBqp.HasValue && nhiemVuChiSelected.FGiaTriKhBqp > nhiemVuChiSelected.FGiaTriKhTtcp)
                    //{
                    //    System.Windows.Forms.MessageBox.Show("Bạn có chắc chắn muốn nhập Giá trị của KHTT BQP (USD) lớn hơn Giá trị của KHTT TTCP (USD)?",
                    //        Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    IsShowMessageError = true;
                    //}
                    nhiemVuChiSelected.FGiaTriKhTtcp = nhiemVuChiSelected.FGiaTriKhBqp;
                    GetTongGiaTri();
                    CalculateNhiemVuChi();
                    IsShowMessageError = false;
                    nhiemVuChiSelected.IsModified = true;
                    break;
                //case nameof(NhKhTongTheNhiemVuChiModel.FGiaTriKhBqpVnd):
                    //if (nhiemVuChiSelected.FGiaTriKhTtcp.HasValue && nhiemVuChiSelected.FGiaTriKhBqpVnd.HasValue && nhiemVuChiSelected.FGiaTriKhBqpVnd > nhiemVuChiSelected.FGiaTriKhTtcp)
                    //{
                    //    System.Windows.Forms.MessageBox.Show("Bạn có chắc chắn muốn nhập Giá trị của KHTT BQP VNĐ lớn hơn Giá trị của KHTT TTCP?",
                    //        Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}
                    //GetTongGiaTri();
                    //CalculateNhiemVuChi();
                    //IsShowMessageError = false;
                    //nhiemVuChiSelected.IsModified = true;
                    //break;
                case nameof(NhKhTongTheNhiemVuChiModel.FGiaTriKhTtcp):
                    if (!IsShowMessageError && nhiemVuChiSelected.FGiaTriKhTtcp.HasValue && nhiemVuChiSelected.FGiaTriKhBqp.HasValue && nhiemVuChiSelected.FGiaTriKhBqp > nhiemVuChiSelected.FGiaTriKhTtcp)
                    {
                        System.Windows.Forms.MessageBox.Show("Dòng " + nhiemVuChiSelected.SMaNhiemVuChi + ": Giá trị của KHTT BQP (USD) lớn hơn Giá trị của KHTT TTCP (USD).", Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        IsShowMessageError = true;
                    }
                    GetTongGiaTri();
                    CalculateNhiemVuChi();
                    IsShowMessageError = false;
                    nhiemVuChiSelected.IsModified = true;
                    break;
                case nameof(NhKhTongTheNhiemVuChiModel.IIdDonViThuHuongId):
                    if (nhiemVuChiSelected.IIdDonViThuHuongId != null)
                    {
                        nhiemVuChiSelected.SMaDonViThuHuong = _itemsDonVi.FirstOrDefault(n => n.Id == nhiemVuChiSelected.IIdDonViThuHuongId).HiddenValue;
                        nhiemVuChiSelected.IsModified = true;
                    }
                    break;
            }
        }

        private void LoadDonVi()
        {
            var data = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            if (data == null) return;
            _itemsDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(data.Select(n => new ComboboxItem()
            {
                Id = n.Id,
                DisplayItem = n.IIDMaDonVi + " - " + n.TenDonVi,
                HiddenValue = n.IIDMaDonVi
            }));
            OnPropertyChanged(nameof(ItemsDonVi));
        }

        //private void LoadKeHoachGiaiDoan()
        //{
        //    var data = _nhKhTongTheService.FindAll(n => n.BIsActive && n.ILoai == 1).ToList();
        //    _itemsKHGiaiDoan = _mapper.Map<ObservableCollection<NhKhTongTheModel>>(data);
        //    _itemsKHGiaiDoan.ForAll(s =>
        //    {
        //        //s.TenKeHoach = "KHTT " + string.Format("{0} - {1}", s.IGiaiDoanTu, s.IGiaiDoanDen) + " - Số KH: " + s.SSoKeHoachTtcp;
        //        s.TenKeHoach = "KHTT " + string.Format("{0} - {1}", s.IGiaiDoanTu, s.IGiaiDoanDen) + " - Số KH: " + s.SSoKeHoachBqp;
        //    });
        //    OnPropertyChanged(nameof(ItemsKHGiaiDoan));
        //}

        private void OnDelete()
        {
            if (ItemsKhTongTheNhiemVuChi.Any(x => x.IsSelected) && ItemsKhTongTheNhiemVuChi.Where(x => x.IsSelected).Count() > 1)
            {
                DeleteTreeItems(ItemsKhTongTheNhiemVuChi.Where(x => x.IsSelected).ToList(), ItemsKhTongTheNhiemVuChi.Any(x => x.IsSelected) ? !ItemsKhTongTheNhiemVuChi.FirstOrDefault(x => x.IsSelected).IsDeleted : true);
                OrderItems();
                CalculateNhiemVuChi();
                GetTongGiaTri();
            }
            else if (SelectedKhTongTheNhiemVuChi != null)
            {
                if (SelectedKhTongTheNhiemVuChi.IsDeleted && ItemsKhTongTheNhiemVuChi.Any(n => !n.IsDeleted && n.IIdNhiemVuChiId == SelectedKhTongTheNhiemVuChi.IIdNhiemVuChiId))
                {
                    System.Windows.Forms.MessageBox.Show("Nhiệm vụ chi này đã được gán cho 1 đơn vị, không thể hoàn tác", Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DeleteTreeItems(new List<NhKhTongTheNhiemVuChiModel> { SelectedKhTongTheNhiemVuChi }, !SelectedKhTongTheNhiemVuChi.IsDeleted);
                OrderItems();
                CalculateNhiemVuChi();
                GetTongGiaTri();
            }
            OnPropertyChanged(nameof(ItemsKhTongTheNhiemVuChi));
        }

        private void DeleteTreeItems(List<NhKhTongTheNhiemVuChiModel> currentItems, bool status)
        {
            if (!currentItems.IsEmpty())
            {
                foreach (var currentItem in currentItems)
                {
                    var items = _itemsKhTongTheNhiemVuChi;
                    currentItem.IsDeleted = status;
                    var childs = items.Where(x => x.ParentNhiemVuChiId == currentItem.IIdNhiemVuChiId);
                    if (!childs.IsEmpty())
                    {
                        DeleteTreeItems(childs.ToList(), status);
                        //foreach (var item in childs)
                        //{
                        //    DeleteTreeItems(item, status);
                        //}
                    }
                }

            }
        }

        private int CountTreeChildItems(NhKhTongTheNhiemVuChiModel currentItem, bool isCountSiblings = false)
        {
            var items = _itemsKhTongTheNhiemVuChi;
            int count = 0;
            if (!isCountSiblings)
            {
                var childs = items.Where(x => x.ParentNhiemVuChiId != null && x.ParentNhiemVuChiId == currentItem.IIdNhiemVuChiId);
                if (!childs.IsEmpty())
                {
                    count += childs.Count();
                    foreach (var item in childs)
                    {
                        count += CountTreeChildItems(item);
                    }
                }
            }
            else
            {
                var itemSiblings = items.Where(x => x.ParentNhiemVuChiId != null
                                            && x.IIdNhiemVuChiId != currentItem.IIdNhiemVuChiId
                                            && items.IndexOf(currentItem) < items.IndexOf(x)
                                            && x.ParentNhiemVuChiId == currentItem.ParentNhiemVuChiId);
                if (!itemSiblings.IsEmpty())
                {
                    count += itemSiblings.Count();
                    foreach (var item in itemSiblings)
                    {
                        count += CountTreeChildItems(item);
                    }
                }
            }
            return count;
        }

        private void OrderItems(Guid? parentId = null)
        {
            var childs = _itemsKhTongTheNhiemVuChi.Where(x => (x.ParentNhiemVuChiId == parentId) && !x.IsDeleted);
            if (!childs.IsEmpty())
            {
                var parent = _itemsKhTongTheNhiemVuChi.FirstOrDefault(x => x.IIdNhiemVuChiId == parentId);
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
            if (!_itemsKhTongTheNhiemVuChi.IsEmpty())
            {
                _itemsKhTongTheNhiemVuChi.ForAll(s => s.IsEnabled = !_itemsKhTongTheNhiemVuChi.Any(y => y.ParentNhiemVuChiId == s.IIdNhiemVuChiId));
                _itemsKhTongTheNhiemVuChi.ForAll(x =>
                {
                    if (x.ParentNhiemVuChiId.IsNullOrEmpty() || !_itemsKhTongTheNhiemVuChi.Any(y => y.IIdNhiemVuChiId == x.ParentNhiemVuChiId)) x.IsHangCha = true;
                    if (!x.IsEnabled) x.IsHangCha = true;
                    else if (_itemsKhTongTheNhiemVuChi.Any(y => y.ParentNhiemVuChiId == x.ParentNhiemVuChiId && !y.IsEnabled)) x.IsHangCha = true;
                });
            };
        }

        public void NhiemVuChi_BeginningEditHanlder(DataGridBeginningEditEventArgs e)
        {
            SelectedKhTongTheNhiemVuChi = (NhKhTongTheNhiemVuChiModel)e.Row.Item;

            if (e.Column.SortMemberPath.Equals(nameof(NhKhTongTheNhiemVuChiModel.FGiaTriKhTtcp))
                || e.Column.SortMemberPath.Equals(nameof(NhKhTongTheNhiemVuChiModel.FGiaTriKhBqp)))
                //|| e.Column.SortMemberPath.Equals(nameof(NhKhTongTheNhiemVuChiModel.FGiaTriKhBqpVnd)))
            {
                if (SelectedKhTongTheNhiemVuChi != null && !SelectedKhTongTheNhiemVuChi.IsEnabled)
                {
                    e.Cancel = true;
                }
            }
        }

        private void CalculateNhiemVuChi()
        {
            var parents = ItemsKhTongTheNhiemVuChi.Where(x => x.ParentNhiemVuChiId.IsNullOrEmpty() || !ItemsKhTongTheNhiemVuChi.Any(y => y.IIdNhiemVuChiId == x.ParentNhiemVuChiId));
            foreach (var item in parents)
            {
                CalculateNhiemVuChi(item);
            }
        }

        private void CalculateNhiemVuChi(NhKhTongTheNhiemVuChiModel parentItem)
        {
            var childs = ItemsKhTongTheNhiemVuChi.Where(x => x.ParentNhiemVuChiId == parentItem.IIdNhiemVuChiId && !x.IsDeleted);
            if (!childs.IsEmpty())
            {
                foreach (var item in childs)
                {
                    CalculateNhiemVuChi(item);
                    parentItem.FGiaTriKhBqp = childs.Sum(x => x.FGiaTriKhBqp);
                    //parentItem.FGiaTriKhBqpVnd = childs.Sum(x => x.FGiaTriKhBqpVnd);
                    parentItem.FGiaTriKhTtcp = childs.Sum(x => x.FGiaTriKhTtcp);
                }
            }
        }
    }
}

