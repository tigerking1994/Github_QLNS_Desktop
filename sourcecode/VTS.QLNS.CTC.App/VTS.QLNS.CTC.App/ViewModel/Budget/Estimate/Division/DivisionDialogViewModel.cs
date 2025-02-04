using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Converters;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate
{
    public class DivisionDialogViewModel : DialogViewModelBase<DtChungTuModel>
    {
        private readonly INsMucLucNganSachService _nsMucLucNganSachService;
        private readonly ISessionService _sessionService;
        private readonly INsDtChungTuService _estimationService;
        private readonly INsDtChungTuChiTietService _chungTuChiTietService;
        private readonly INsDonViService _nSDonViService;
        private readonly IMapper _mapper;
        private ICollectionView _dataLNSView;

        public override Type ContentType => typeof(View.Budget.Estimate.DivisionDialog);
        public override string Name => Guid.Empty.Equals(Model.Id) ? "THÊM MỚI CHỨNG TỪ" : "CẬP NHẬT CHỨNG TỪ";
        public override string Description => Guid.Empty.Equals(Model.Id) ? "Tạo mới chứng từ nhận phân bổ dự toán" : "Cập nhật chứng từ nhận phân bổ dự toán";

        public bool IsEnabled => Guid.Empty.Equals(Model.Id);

        private bool _isSaveData;
        public bool IsSaveData
        {
            get => _isSaveData;
            set => SetProperty(ref _isSaveData, value);
        }

        private ObservableCollection<NsMuclucNgansachModel> _dataLNS;
        public ObservableCollection<NsMuclucNgansachModel> DataLNS
        {
            get => _dataLNS;
            set => SetProperty(ref _dataLNS, value);
        }

        public string SelectedCountLNS
        {
            get
            {
                int totalCount = DataLNS != null ? DataLNS.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = DataLNS != null ? DataLNS.Count(item => item.IsSelected) : 0;
                return string.Format("CHỌN LNS ({0}/{1})", totalSelected, totalCount);
            }
        }

        public bool Flag { get; set; } = false;

        private bool _selectAllLNS;
        public bool SelectAllLNS
        {
            get => (DataLNS == null || !DataLNS.Any()) ? false : DataLNS.All(item => item.IsSelected);
            set
            {
                SetProperty(ref _selectAllLNS, value);
                if (DataLNS != null)
                {
                    Flag = true;
                    DataLNS.Select(c => { c.IsSelected = _selectAllLNS; return c; }).ToList();
                    Flag = false;
                }
            }
        }

        private string _searchLNS;
        public string SearchLNS
        {
            get => _searchLNS;
            set
            {
                if (SetProperty(ref _searchLNS, value))
                {
                    _dataLNSView.Refresh();
                    OnPropertyChanged(nameof(SelectedCountLNS));
                }
            }
        }

        //Sửa lỗi tự động cập nhập xuống màn danh sách khi chưa thực hiện lưu(mới đóng popup)
        public DateTime? DNgayChungTu { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }

        private ComboboxItem _cbxVoucherTypeSelected;
        public ComboboxItem CbxVoucherTypeSelected
        {
            get => _cbxVoucherTypeSelected;
            set
            {
                SetProperty(ref _cbxVoucherTypeSelected, value);
                if (_cbxVoucherTypeSelected != null)
                {
                    if (Model != null && Guid.Empty.Equals(Model.Id))
                    {
                        LoadChungTuIndex();
                    }
                    LoadLNS();
                    OnPropertyChanged(nameof(SelectedCountLNS));
                }
            }
        }

        private ObservableCollection<ComboboxItem> _cbxVoucherType;
        public ObservableCollection<ComboboxItem> CbxVoucherType
        {
            get => _cbxVoucherType;
            set => SetProperty(ref _cbxVoucherType, value);
        }

        private ComboboxItem _cbxBudgetTypeSelected;
        public ComboboxItem CbxBudgetTypeSelected
        {
            get => _cbxBudgetTypeSelected;
            set
            {
                SetProperty(ref _cbxBudgetTypeSelected, value);
            }
        }

        private ObservableCollection<ComboboxItem> _cbxBudgetType;
        public ObservableCollection<ComboboxItem> CbxBudgetType
        {
            get => _cbxBudgetType;
            set => SetProperty(ref _cbxBudgetType, value);
        }

        public DivisionDialogViewModel(
            INsMucLucNganSachService nsMucLucNganSachService,
            IMapper mapper,
            INsDtChungTuService estimationService,
            ISessionService sessionService,
            INsDonViService nSDonViService,
            INsDtChungTuChiTietService chungTuChiTietService)
        {
            _nsMucLucNganSachService = nsMucLucNganSachService;
            _mapper = mapper;
            _sessionService = sessionService;
            _estimationService = estimationService;
            _nSDonViService = nSDonViService;
            _chungTuChiTietService = chungTuChiTietService;
        }

        public override void Init()
        {
            IsSaveData = true;
            _searchLNS = string.Empty;
            LoadVoucherType();
            LoadBudgetType();
            LoadLNS();
            LoadData();
        }

        private void CheckExitsDuToanDauNam()
        {
            IsSaveData = true;
            var budgetType = (BudgetType)(int.Parse(_cbxBudgetTypeSelected.ValueItem));
            if (!BudgetType.YEAR.Equals(budgetType) || !Guid.Empty.Equals(Model.Id)) return;

            int loaiChungTu = int.Parse(VoucherType.NSSD_Key);
            if (_cbxVoucherTypeSelected != null && VoucherType.NSBD_Key.Equals(_cbxVoucherTypeSelected.ValueItem))
            {
                loaiChungTu = int.Parse(VoucherType.NSBD_Key);
            }

            var predicate = PredicateBuilder.True<NsDtChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.ILoai == SoChungTuType.ReceiveEstimate);
            predicate = predicate.And(x => x.ILoaiChungTu == loaiChungTu);
            predicate = predicate.And(x => x.ILoaiDuToan == (int)BudgetType.YEAR);

            IEnumerable<NsDtChungTu> result = _estimationService.FindByCondition(predicate).ToList();
            if (result.Any())
            {
                var listSoChungTu = string.Join(",", result.Select(x => x.SSoChungTu));
                string message = $"Đã tồn tại dự án đầu năm: {listSoChungTu}";
                IsSaveData = false;
                System.Windows.Forms.MessageBox.Show(message, Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadBudgetType()
        {
            var cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = VoucherType.BudgetTypeName[BudgetType.YEAR], ValueItem = ((int)BudgetType.YEAR).ToString()},
                new ComboboxItem {DisplayItem = VoucherType.BudgetTypeName[BudgetType.LAST_YEAR], ValueItem = ((int)BudgetType.LAST_YEAR).ToString()},
                new ComboboxItem {DisplayItem = VoucherType.BudgetTypeName[BudgetType.ADDITIONAL], ValueItem = ((int)BudgetType.ADDITIONAL).ToString()},
                new ComboboxItem {DisplayItem = VoucherType.BudgetTypeName[BudgetType.ADDITIONAL_TRANSFER_LAST_YEAR], ValueItem = ((int)BudgetType.ADDITIONAL_TRANSFER_LAST_YEAR).ToString()}
            };

            CbxBudgetType = new ObservableCollection<ComboboxItem>(cbxVoucher);
            if (Model != null && Model.Id != Guid.Empty && Model.ILoaiDuToan.HasValue)
            {
                _cbxBudgetTypeSelected = CbxBudgetType.Single(item => item.ValueItem.Equals(Model.ILoaiDuToan.ToString()));
            }
            else if (_sessionService.Current.YearOfBudget != NAM_NGAN_SACH.NAM_NAY)
            {
                _cbxBudgetTypeSelected = CbxBudgetType.SingleOrDefault(item => item.ValueItem.Equals(((int)BudgetType.LAST_YEAR).ToString()));
            }
            else
            {
                _cbxBudgetTypeSelected = CbxBudgetType.First();
            }
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);

            if (Model != null && Model.Id != Guid.Empty)
            {
                BudgetCatalogSelectedToStringConvert.SetCheckboxSelected(_dataLNS, Model.SDslns);
                DNgayChungTu = Model.DNgayChungTu;
                DNgayQuyetDinh = Model.DNgayQuyetDinh;
            }
            else
            {
                Model = new DtChungTuModel()
                {
                    DNgayChungTu = DateTime.Now,
                    DNgayQuyetDinh = DateTime.Now,
                    SSoQuyetDinh = string.Empty,
                    // SMoTa = "- Mô tả chi tiết nội dung chứng từ"
                };
                DNgayChungTu = DateTime.Now;
                DNgayQuyetDinh = DateTime.Now;
                LoadChungTuIndex();
            }
        }

        private void LoadChungTuIndex()
        {
            var predicate = CreatePredicate();
            int soChungTuIndex = _estimationService.FindNextSoChungTuIndex(predicate);
            Model.SSoChungTu = "DT-" + soChungTuIndex.ToString("D3");
            Model.ISoChungTuIndex = soChungTuIndex;
            OnPropertyChanged(nameof(Division));
        }

        private void LoadLNS()
        {
            int yearOfWork = _sessionService.Current.YearOfWork;
            //string idDonVi = _sessionService.AuthenticationToken.IdDonVi;
            int loaiChungTu = int.Parse(VoucherType.NSSD_Key);

            if (_cbxVoucherTypeSelected != null && VoucherType.NSBD_Key.Equals(_cbxVoucherTypeSelected.ValueItem))
            {
                loaiChungTu = int.Parse(VoucherType.NSBD_Key);
            }
            var listNsMucLucNganSach = _nsMucLucNganSachService.FindByMLNS(yearOfWork, NSEntityStatus.ACTIVED, loaiChungTu).ToList();
            DataLNS = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listNsMucLucNganSach);

            if (!Guid.Empty.Equals(Model.Id))
            {
                List<string> listLnsHasData = _chungTuChiTietService.GetLnsHasData(new List<Guid> { Model.Id }).ToList();
                DataLNS.Where(x => listLnsHasData.Contains(x.Lns)).ToList().ForEach(x => x.IsHitTestVisible = false);
            }

            _dataLNSView = CollectionViewSource.GetDefaultView(DataLNS);
            _dataLNSView.Filter = ListLNSFilter;

            if (_dataLNS != null && _dataLNS.Count > 0)
            {
                foreach (var model in _dataLNS)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(NsMuclucNgansachModel.IsSelected) && !Flag)
                        {
                            Flag = true;
                            SetCheckChildren(_dataLNS, model);
                            SetCheckParent(_dataLNS, model);
                            //ListLNS.Where(x => x.MlnsIdParent == model.MlnsId).Select(x => x.IsSelected = model.IsSelected).ToList();
                            Flag = false;
                            //foreach (var item in _dataLNS)
                            //{
                            //    if (item.MlnsIdParent == model.MlnsId)
                            //    {
                            //        item.IsSelected = model.IsSelected;
                            //    }
                            //}
                            OnPropertyChanged(nameof(SelectAllLNS));
                            OnPropertyChanged(nameof(SelectedCountLNS));
                        }
                    };
                }
            }
        }

        private void SetCheckChildren(ObservableCollection<NsMuclucNgansachModel> items, NsMuclucNgansachModel item)
        {
            foreach (var e in items)
            {
                if (e.MlnsIdParent == item.MlnsId)
                {
                    e.IsSelected = item.IsSelected;
                    SetCheckChildren(items, e);
                }
            }
        }

        private void SetCheckParent(ObservableCollection<NsMuclucNgansachModel> items, NsMuclucNgansachModel item)
        {
            foreach (var e in items)
            {
                if (e.MlnsId == item.MlnsIdParent)
                {
                    e.IsSelected = items.Where(x => x.MlnsIdParent == item.MlnsIdParent).All(x => x.IsSelected);
                    SetCheckParent(items, e);
                }
            }
        }

        private void LoadVoucherType()
        {
            var cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = VoucherType.NSSD_Value, ValueItem = VoucherType.NSSD_Key},
            };

            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(item => item.BCoNSNganh);
            predicate = predicate.And(item => item.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(item => item.ITrangThai == NSEntityStatus.ACTIVED);
            predicate = predicate.And(item => !item.Loai.Equals(LoaiDonVi.TOAN_QUAN));
            var listDonVi = _nSDonViService.FindByCondition(predicate);
            if (listDonVi != null && listDonVi.Count() > 0)
            {
                cbxVoucher.Add(new ComboboxItem { DisplayItem = VoucherType.NSBD_Value, ValueItem = VoucherType.NSBD_Key });
            }

            CbxVoucherType = new ObservableCollection<ComboboxItem>(cbxVoucher);
            if (Model != null && Model.Id != Guid.Empty && Model.ILoaiChungTu.HasValue)
            {
                _cbxVoucherTypeSelected = CbxVoucherType.Where(item => item.ValueItem.Equals(Model.ILoaiChungTu.ToString()))
                    .Select(item => item).DefaultIfEmpty(CbxVoucherType.ElementAt(0)).FirstOrDefault();
            }
            else
            {
                _cbxVoucherTypeSelected = CbxVoucherType.ElementAt(0);
            }
        }

        private bool ListLNSFilter(object obj)
        {
            bool result = true;
            var item = (NsMuclucNgansachModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchLNS))
                result = item.LNSDisplay.ToLower().Contains(_searchLNS!.ToLower());
            item.IsFilter = result;
            return result;
        }

        public override void OnSave()
        {
            string message = GetMessageValidate();
            if (!string.IsNullOrEmpty(message))
            {
                System.Windows.Forms.MessageBox.Show(message, Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool bDeleteDetail = false;
            string messageCheckBox = GetMessageValidateCheckBox();
            if (!string.IsNullOrEmpty(messageCheckBox))
            {
                MessageBoxResult messageValidate = MessageBoxHelper.Confirm(messageCheckBox);
                if (messageValidate.Equals(MessageBoxResult.Yes))
                {
                    bDeleteDetail = true;
                }
                else
                {
                    return;
                }
            }

            IsSaveData = false;

            if (bDeleteDetail)
            {
                List<NsDtChungTuChiTiet> listChungTuChiTiet = _chungTuChiTietService.FindByIdChungTu(Model.Id.ToString()).ToList();
                var listLNSHasDataUnchecked = _dataLNS.Where(n => !n.IsHitTestVisible && !n.IsSelected).Select(n => n.Lns).ToList();
                listChungTuChiTiet = listChungTuChiTiet.Where(x => listLNSHasDataUnchecked.Contains(x.SLns)).ToList();
                _chungTuChiTietService.RemoveRange(listChungTuChiTiet);

                List<NsDtChungTuChiTiet> listChungTuPhanBoChiTiet = _chungTuChiTietService.FindAll(n => n.IIdCtduToanNhan.Equals(Model.Id)).ToList();
                listChungTuPhanBoChiTiet = listChungTuPhanBoChiTiet.Where(x => listLNSHasDataUnchecked.Contains(x.SLns)).ToList();
                _chungTuChiTietService.RemoveRange(listChungTuPhanBoChiTiet);

                List<NsDtChungTu> listChungTuPhanBo = _estimationService.FindByCondition(n => n.IIdDotNhan != null && n.IIdDotNhan.Equals(Model.Id.ToString())).ToList();
                foreach (var item in listChungTuPhanBo)
                {
                    UpdateChungTuPhanBo(item.Id);
                }
            }

            if (Model == null) Model = new DtChungTuModel();
            Model.SDslns = BudgetCatalogSelectedToStringConvert.GetValueSelected(DataLNS);
            Model.INamLamViec = _sessionService.Current.YearOfWork;
            Model.IIdMaNguonNganSach = _sessionService.Current.Budget;
            Model.INamNganSach = _sessionService.Current.YearOfBudget;
            Model.SDsidMaDonVi = _sessionService.Current.IdDonVi;
            Model.ILoai = SoChungTuType.ReceiveEstimate;
            Model.ILoaiChungTu = int.Parse(_cbxVoucherTypeSelected.ValueItem);
            Model.ILoaiDuToan = int.Parse(_cbxBudgetTypeSelected.ValueItem);
            Model.DNgayQuyetDinh = DNgayQuyetDinh;
            Model.DNgayChungTu = DNgayChungTu;

            NsDtChungTu entity;
            if (Model.Id == Guid.Empty)
            {
                // Add
                LoadChungTuIndex();
                entity = new NsDtChungTu();
                _mapper.Map(Model, entity);

                entity.DNgayTao = DateTime.Now;
                entity.SNguoiTao = _sessionService.Current.Principal;
                _estimationService.Add(entity);
            }
            else
            {
                // Update
                entity = _estimationService.FindById(Model.Id);
                var chungTuChiTiet = _mapper.Map<List<DtChungTuChiTietModel>>(_chungTuChiTietService.FindByIdChungTu(entity.Id.ToString()));
                _mapper.Map(Model, entity);
                var itemsHasData = chungTuChiTiet.Where(x => x.HasData);
                entity.DNgaySua = DateTime.Now;
                entity.SNguoiSua = _sessionService.Current.Principal;
                entity.FTongTuChi = itemsHasData.Sum(x => x.FTuChi);
                entity.FTongHienVat = itemsHasData.Sum(x => x.FHienVat);
                entity.FTongHangNhap = itemsHasData.Sum(x => x.FHangNhap);
                entity.FTongHangMua = itemsHasData.Sum(x => x.FHangMua);
                entity.FTongPhanCap = itemsHasData.Sum(x => x.FPhanCap);
                entity.FTongDuPhong = itemsHasData.Sum(x => x.FDuPhong);
                _estimationService.Update(entity);
            }

            DialogHost.Close(SystemConstants.ROOT_DIALOG);

            // DialogHost.CloseDialogCommand.Execute(null, null);

            // Show detail page when saved
            SavedAction?.Invoke(_mapper.Map<DtChungTuModel>(entity));
        }

        private void UpdateChungTuPhanBo(Guid id)
        {
            NsDtChungTu chungTu = _estimationService.FindById(id);
            List<NsDtChungTuChiTiet> chungTuChiTiet = _chungTuChiTietService.FindByIdChungTu(id.ToString()).ToList();
            var childs = chungTuChiTiet.Where(x => !x.BHangCha && (x.FTuChi != 0 || x.FHienVat != 0 || x.FHangNhap != 0 || x.FHangMua != 0 ||
                                    x.FPhanCap != 0 || x.FDuPhong != 0)).ToList();

            chungTu.FTongTuChi = childs.Sum(x => x.FTuChi);
            chungTu.FTongHienVat = childs.Sum(x => x.FHienVat);
            chungTu.FTongHangNhap = childs.Sum(x => x.FHangNhap);
            chungTu.FTongHangMua = childs.Sum(x => x.FHangMua);
            chungTu.FTongPhanCap = childs.Sum(x => x.FPhanCap);
            chungTu.FTongDuPhong = childs.Sum(x => x.FDuPhong);

            _estimationService.Update(chungTu);
        }

        private Expression<Func<NsDtChungTu, bool>> CreatePredicate()
        {
            var predicate = PredicateBuilder.True<NsDtChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(x => x.ILoai == SoChungTuType.ReceiveEstimate);
            return predicate;
        }

        private string GetMessageValidate()
        {
            List<string> messages = new List<string>();

            if (!DNgayChungTu.HasValue)
            {
                messages.Add(Resources.AlertNgayChungTuEmpty);
            }

            if (string.IsNullOrEmpty(Model.SSoQuyetDinh))
            {
                messages.Add(Resources.AlertSoQuyetDinhEmpty);
            }

            if (!DNgayQuyetDinh.HasValue)
            {
                messages.Add(Resources.AlertNgayQuyetDinhEmpty);
            }

            if (DataLNS.All(x => !x.IsSelected))
            {
                messages.Add(Resources.AlertLNSEmpty);
            }

            if (_cbxBudgetTypeSelected == null)
            {
                messages.Add(Resources.AlertLoaiDuToanEmpty);
            }
            if (!messages.Any())
            {
                messages.AddRange(ValidateSoQuyetDinh());
            }
            return string.Join(Environment.NewLine, messages);
        }

        private string GetMessageValidateCheckBox()
        {
            List<string> messages = new List<string>();

            var listLNSHasDataUnchecked = DataLNS.Where(n => !n.IsHitTestVisible && !n.IsSelected).Select(n => n.Lns).ToList();
            string lnsText = string.Join(StringUtils.COMMA_SPLIT, listLNSHasDataUnchecked);

            if (!string.IsNullOrEmpty(lnsText))
            {
                messages.Add(string.Format(Resources.DivisionHasDataLNS, lnsText));
            }

            return string.Join(Environment.NewLine, messages);
        }

        private List<string> ValidateSoQuyetDinh()
        {
            List<string> messages = new List<string>();
            var predicate = CreatePredicate();
            predicate = predicate.And(x => x.SSoQuyetDinh == Model.SSoQuyetDinh);
            if (!Guid.Empty.Equals(Model.Id))
                predicate = predicate.And(x => x.Id != Model.Id);
            var listChungTu = _estimationService.FindByCondition(predicate).ToList();
            if (listChungTu.Count > 0)
            {
                if (listChungTu.Any(x => x.DNgayQuyetDinh.Value.Date != DNgayQuyetDinh.Value.Date))
                {
                    messages.Add(string.Format(Resources.VoucherValidateSoQuyetDinhNgayQuyetDinh, Model.SSoQuyetDinh, listChungTu.First().DNgayQuyetDinh.Value.ToString("dd/MM/yyyy")));
                }
            }

            var predicate2 = PredicateBuilder.True<NsDtChungTu>();
            predicate2 = predicate2.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate2 = predicate2.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate2 = predicate2.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate2 = predicate2.And(x => x.ILoai == SoChungTuType.EstimateDivision);
            if (!Guid.Empty.Equals(Model.Id))
            {
                predicate2 = predicate2.And(x => x.IIdDotNhan.Contains(Model.Id.ToString()));
                var listChungTuPhanBo = _estimationService.FindByCondition(predicate2).ToList();
                if (listChungTuPhanBo.Any(n => (n.DNgayQuyetDinh.HasValue && n.DNgayQuyetDinh.Value.Date < DNgayQuyetDinh.Value.Date) || (!n.DNgayQuyetDinh.HasValue && n.DNgayChungTu.HasValue && n.DNgayChungTu.Value.Date < DNgayQuyetDinh.Value.Date)))
                {
                    DateTime? minList = new DateTime();
                    DateTime? minListQuyetDinh = listChungTuPhanBo.Where(n => (n.DNgayQuyetDinh.HasValue && n.DNgayQuyetDinh.Value.Date < DNgayQuyetDinh.Value.Date)).Min(n => n.DNgayQuyetDinh);
                    DateTime? minListChungTu = listChungTuPhanBo.Where(n => (!n.DNgayQuyetDinh.HasValue && n.DNgayChungTu.Value.Date < DNgayQuyetDinh.Value.Date)).Min(n => n.DNgayChungTu);
                    if (!minListQuyetDinh.HasValue && minListChungTu.HasValue)
                    {
                        minList = minListChungTu;
                    }
                    else if (minListQuyetDinh.HasValue && !minListChungTu.HasValue)
                    {
                        minList = minListQuyetDinh;
                    }
                    else if (minListQuyetDinh.HasValue && minListChungTu.HasValue)
                    {
                        minList = minListQuyetDinh < minListChungTu ? minListQuyetDinh : minListChungTu;
                    }
                    messages.Add(string.Format(Resources.ValidateDivisionEstimateDate, minList.Value.ToString("dd/MM/yyyy")));
                }
            }


            return messages;
        }
    }
}
