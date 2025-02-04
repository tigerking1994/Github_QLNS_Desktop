using AutoMapper;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Media;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.ApproveSettlementDone
{
    public class ApproveSettlementDoneDetailViewModel : DetailViewModelBase<PheDuyetQuyetToanModel, PheDuyetQuyetToanDetailModel>
    {
        //private IMapper _mapper;
        //private ISessionService _sessionService;
        //private IApproveProjectService _approveProjectService;
        //private IPheDuyetQuyetToanService _pheDuyetQuyetToanService;
        //private IVdtQtQuyetToanService _vdtQtQuyetToanService;
        //private readonly ILog _logger;

        //public override string Name => "Quản lý phê duyệt quyết toán dự án hoàn thành";
        //public override string Title => "Quản lý phê duyệt quyết toán dự án hoàn thành";
        //public override Type ContentType => typeof(View.Investment.EndOfInvestment.ApproveSettlementDone.ApproveSettlementDoneDetail);
        //public override PackIconKind IconKind => PackIconKind.FileDocumentBoxMultiple;
        //public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted);
        //public delegate void DataChangedEventHandler(object sender, EventArgs e);
        //public event DataChangedEventHandler ClosePopup;

        //private List<ComboboxItem> _dataChiPhi;
        //public List<ComboboxItem> DataChiPhi
        //{
        //    get => _dataChiPhi;
        //    set => SetProperty(ref _dataChiPhi, value);
        //}

        //private List<ComboboxItem> _dataNguonVon;
        //public List<ComboboxItem> DataNguonVon
        //{
        //    get => _dataNguonVon;
        //    set => SetProperty(ref _dataNguonVon, value);
        //}

        //public RelayCommand SaveCommand { get; }
        //public RelayCommand CloseWindowCommand { get; }

        //public ApproveSettlementDoneDetailViewModel(
        //   IMapper mapper,
        //   ISessionService sessionService,
        //   IApproveProjectService approveProjectService,
        //   IPheDuyetQuyetToanService pheDuyetQuyetToanService,
        //   ILog logger,
        //   IVdtQtQuyetToanService vdtQtQuyetToanService)
        //{
        //    _mapper = mapper;
        //    _sessionService = sessionService;
        //    _approveProjectService = approveProjectService;
        //    _pheDuyetQuyetToanService = pheDuyetQuyetToanService;
        //    _vdtQtQuyetToanService = vdtQtQuyetToanService;
        //    _logger = logger;

        //    SaveCommand = new RelayCommand(obj => OnSaveData());
        //    CloseWindowCommand = new RelayCommand(obj => OnCloseWindow());
        //}

        //private void LoadChiPhi()
        //{
        //    IEnumerable<VdtDmChiPhi> listChiPhi = _approveProjectService.GetAllDmChiPhi();
        //    DataChiPhi = _mapper.Map<List<ComboboxItem>>(listChiPhi);
        //}

        //private void LoadNguonVon()
        //{
        //    IEnumerable<Core.Domain.NsNguonNganSach> listNguonVon = _approveProjectService.GetAllNguonNS();
        //    DataNguonVon = _mapper.Map<List<ComboboxItem>>(listNguonVon);
        //}

        //public override void Init()
        //{
        //    try
        //    {
        //        LoadChiPhi();
        //        LoadNguonVon();
        //        LoadData();
        //        CalculateParent();
        //        OnPropertyChanged(nameof(Items));
        //        OnPropertyChanged(nameof(IsSaveData));
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error(ex.Message, ex);
        //    }
        //}

        //private void CalculateParent()
        //{
        //    var parentItemChiPhi = Items.Where(x => x.IsHangCha == true && x.Loai == (int)QDDauTuTypeEnum.CHIPHI).FirstOrDefault();
        //    var parentItemNguonVon = Items.Where(x => x.IsHangCha == true && x.Loai == (int)QDDauTuTypeEnum.NGUONVON).FirstOrDefault();
        //    var listChiphi = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.Loai == (int)QDDauTuTypeEnum.CHIPHI).ToList();
        //    var listNguonVon = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.Loai == (int)QDDauTuTypeEnum.NGUONVON).ToList();
        //    parentItemChiPhi.GtDuToan = 0;
        //    parentItemChiPhi.GtQuyetToan = 0;
        //    parentItemNguonVon.GtDuToan = 0;
        //    parentItemNguonVon.GtQuyetToan = 0;
        //    foreach (var item in listChiphi)
        //    {
        //        parentItemChiPhi.GtDuToan += item.GtDuToan;
        //        parentItemChiPhi.GtQuyetToan += item.GtQuyetToan;
        //    }
        //    foreach (var item in listNguonVon)
        //    {
        //        parentItemNguonVon.GtDuToan += item.GtDuToan;
        //        parentItemNguonVon.GtQuyetToan += item.GtQuyetToan;
        //    }
        //}

        //public override void LoadData(params object[] args)
        //{
        //    base.LoadData(args);
        //    IEnumerable<PheDuyetQuyetToanDetailQuery> data = _pheDuyetQuyetToanService.FindAllPheDuyetQuyetToanDetailByCondition(
        //        Model.IdDuAn.HasValue ? Model.IdDuAn.ToString() : string.Empty, Model.NgayQuyetDinh.Value).ToList();
        //    Items = _mapper.Map<ObservableCollection<Model.PheDuyetQuyetToanDetailModel>>(data);

        //    foreach (PheDuyetQuyetToanDetailModel model in Items)
        //    {
        //        if (!model.IsHangCha)
        //        {
        //            model.PropertyChanged += DetailModel_PropertyChanged;
        //        }
        //    }
        //    CalculateParent();
        //}

        //private bool CheckDuplicateChiPhi(Guid idChiPhi)
        //{
        //    List<PheDuyetQuyetToanDetailModel> listChiPhi = Items.Where(x => x.IdChiPhi == idChiPhi).ToList();
        //    if (listChiPhi != null && listChiPhi.Count > 1)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //private bool CheckDuplicateNguonVon(int idNguonVon)
        //{
        //    List<PheDuyetQuyetToanDetailModel> listNguonVon = Items.Where(x => x.IdNguonVon == idNguonVon).ToList();
        //    if (listNguonVon != null && listNguonVon.Count > 1)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //protected override void OnAdd()
        //{
        //    int currentRow = 0;
        //    if (SelectedItem != null)
        //    {
        //        currentRow = Items.IndexOf(SelectedItem);
        //    }
        //    PheDuyetQuyetToanDetailModel sourceItem = Items.ElementAt(currentRow);
        //    PheDuyetQuyetToanDetailModel targetItem = ObjectCopier.Clone(sourceItem);
        //    targetItem.IsHangCha = false;
        //    targetItem.IdChiPhi = Guid.Empty;
        //    targetItem.Id = Guid.Empty;
        //    targetItem.GtQuyetToan = 0;
        //    targetItem.IdNguonVon = 0;
        //    targetItem.PropertyChanged += DetailModel_PropertyChanged;
        //    Items.Insert(currentRow + 1, targetItem);
        //    OnPropertyChanged(nameof(Items));
        //    OnPropertyChanged(nameof(IsSaveData));
        //}

        //public bool ValidateDataSave()
        //{
        //    // validate trùng
        //    List<PheDuyetQuyetToanDetailModel> listChiPhi = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.Loai == (int)QDDauTuTypeEnum.CHIPHI).ToList();
        //    var anyDuplicateChiPhi = listChiPhi.GroupBy(x => x.IdChiPhi).Any(g => g.Count() > 1);
        //    if (anyDuplicateChiPhi)
        //    {
        //        System.Windows.Forms.MessageBox.Show(Resources.MsgCheckTrungChiPhiDauTu, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return false;
        //    }
        //    List<PheDuyetQuyetToanDetailModel> listNguonVon = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.Loai == (int)QDDauTuTypeEnum.NGUONVON).ToList();
        //    var anyDuplicateNguonVon = listNguonVon.GroupBy(x => x.IdNguonVon).Any(g => g.Count() > 1);
        //    if (anyDuplicateNguonVon)
        //    {
        //        System.Windows.Forms.MessageBox.Show(Resources.MsgCheckTrungNguonVonDauTu, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return false;
        //    }
        //    return true;
        //}

        //private void OnSaveData()
        //{
        //    try
        //    {
        //        if (!ValidateDataSave())
        //        {
        //            return;
        //        }
        //        List<PheDuyetQuyetToanDetailModel> listAdd = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.Id == Guid.Empty).ToList();
        //        List<PheDuyetQuyetToanDetailModel> listEdit = Items.Where(x => x.IsModified && !x.IsHangCha && !x.IsDeleted && x.Id != Guid.Empty).ToList();
        //        List<PheDuyetQuyetToanDetailModel> listDetailDelete = Items.Where(x => x.IsDeleted).ToList();

        //        //Thêm mới,sửa vào các bảng chi tiết
        //        if (listAdd.Count > 0)
        //        {
        //            AddDetail(listAdd);
        //        }
        //        if (listEdit != null && listEdit.Count > 0)
        //        {
        //            UpdateDetail(listEdit);
        //        }

        //        if (listDetailDelete.Count > 0)
        //        {
        //            DeleteDetail(listDetailDelete);
        //        }
        //        if (Model != null && Model.Id != Guid.Empty)
        //            _vdtQtQuyetToanService.UpdateTienQuyetToan(Model.Id.ToString());
        //        System.Windows.Forms.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        LoadData();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error(ex.Message, ex);
        //    }
        //}

        //private void UpdateDetail(List<PheDuyetQuyetToanDetailModel> listEdit)
        //{
        //    try
        //    {
        //        List<PheDuyetQuyetToanDetailModel> listQDChiPhiEdit = listEdit.Where(x => x.Loai == (int)QDDauTuTypeEnum.CHIPHI && x.Id != Guid.Empty).ToList();
        //        if (listQDChiPhiEdit.Count > 0)
        //        {
        //            foreach (var item in listQDChiPhiEdit)
        //            {
        //                VdtQtQuyetToanChiPhi chiPhi = _pheDuyetQuyetToanService.FindQuyetToanChiPhi(item.Id);
        //                _mapper.Map(item, chiPhi);
        //                _pheDuyetQuyetToanService.UpdateChiPhi(chiPhi);
        //            }
        //        }

        //        List<PheDuyetQuyetToanDetailModel> listQDNguonVonEdit = listEdit.Where(x => x.Loai == (int)QDDauTuTypeEnum.NGUONVON && x.Id != Guid.Empty).ToList();
        //        if (listQDNguonVonEdit.Count > 0)
        //        {
        //            foreach (var item in listQDNguonVonEdit)
        //            {
        //                VdtQtQuyetToanNguonvon nguonVon = _pheDuyetQuyetToanService.FindQuyetToanNguonVon(item.Id);
        //                _mapper.Map(item, nguonVon);
        //                _pheDuyetQuyetToanService.UpdateNguonVon(nguonVon);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error(ex.Message, ex);
        //    }
        //}

        //protected override void OnDelete()
        //{
        //    if (Items != null && Items.Count > 0 && SelectedItem != null && !SelectedItem.IsHangCha)
        //    {
        //        SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
        //        CalculateParent();
        //        OnPropertyChanged(nameof(IsSaveData));
        //    }
        //}

        //private void DeleteDetail(List<PheDuyetQuyetToanDetailModel> listDelete)
        //{
        //    try
        //    {
        //        List<PheDuyetQuyetToanDetailModel> listChiPhiDelete = listDelete.Where(x => x.Loai == (int)QDDauTuTypeEnum.CHIPHI && x.Id != Guid.Empty).ToList();
        //        foreach (var item in listChiPhiDelete)
        //        {
        //            _pheDuyetQuyetToanService.DeleteChiPhi(item.Id);
        //        }

        //        List<PheDuyetQuyetToanDetailModel> listNguonVonDelete = listDelete.Where(x => x.Loai == (int)QDDauTuTypeEnum.NGUONVON && x.Id != Guid.Empty).ToList();
        //        foreach (var item in listNguonVonDelete)
        //        {
        //            _pheDuyetQuyetToanService.DeleteNguonVon(item.Id);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error(ex.Message, ex);
        //    }
        //}

        //private void AddDetail(List<PheDuyetQuyetToanDetailModel> listAdd)
        //{
        //    try
        //    {
        //        #region add Chi phi
        //        List<PheDuyetQuyetToanDetailModel> listChiPhiAdd = listAdd.Where(x => x.Loai == (int)QDDauTuTypeEnum.CHIPHI && x.IdChiPhi != Guid.Empty).ToList();
        //        if (listChiPhiAdd != null && listChiPhiAdd.Count > 0)
        //        {
        //            List<VdtQtQuyetToanChiPhi> listChiPhi = new List<VdtQtQuyetToanChiPhi>();
        //            listChiPhi = _mapper.Map<List<VdtQtQuyetToanChiPhi>>(listChiPhiAdd);
        //            listChiPhi.Select(n => { n.IIdQuyetToanId = Model.Id; return n; }).ToList();
        //            _pheDuyetQuyetToanService.AddRangeQuyetToanChiPhi(listChiPhi);
        //        }
        //        #endregion

        //        #region add Nguon von
        //        List<PheDuyetQuyetToanDetailModel> listQDNguonVonAdd = listAdd.Where(x => x.Loai == (int)QDDauTuTypeEnum.NGUONVON && x.IdNguonVon != 0).ToList();

        //        if (listQDNguonVonAdd != null && listQDNguonVonAdd.Count > 0)
        //        {
        //            List<VdtQtQuyetToanNguonvon> listNguonVon = new List<VdtQtQuyetToanNguonvon>();
        //            listNguonVon = _mapper.Map<List<VdtQtQuyetToanNguonvon>>(listQDNguonVonAdd);
        //            listNguonVon.Select(n => { n.IIdQuyetToanId = Model.Id; return n; }).ToList();
        //            _pheDuyetQuyetToanService.AddRangeQuyetToanNguonVon(listNguonVon);
        //        }
        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error(ex.Message, ex);
        //    }
        //}

        //private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        //{
        //    PheDuyetQuyetToanDetailModel item = new PheDuyetQuyetToanDetailModel();
        //    PheDuyetQuyetToanDetailModel objectSender = (PheDuyetQuyetToanDetailModel)sender;
        //    int checkLoai = objectSender.Loai;

        //    if (args.PropertyName == nameof(PheDuyetQuyetToanDetailModel.IdChiPhi) && objectSender.IdChiPhi != Guid.Empty
        //        || (args.PropertyName == nameof(PheDuyetQuyetToanDetailModel.GtDuToan) && objectSender.IdChiPhi != Guid.Empty)
        //        || (args.PropertyName == nameof(PheDuyetQuyetToanDetailModel.GtQuyetToan) && objectSender.IdChiPhi != Guid.Empty))
        //    {
        //        item = Items.Where(x => x.IdChiPhi == objectSender.IdChiPhi).First();

        //        if (objectSender.IdChiPhi.HasValue && CheckDuplicateChiPhi(objectSender.IdChiPhi.Value))
        //        {
        //            System.Windows.Forms.MessageBox.Show(Resources.MsgCheckTrungChiPhiDauTu, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            SelectedItem.IdChiPhi = Guid.Empty;
        //            return;
        //        }
        //    }
        //    if (args.PropertyName == nameof(PheDuyetQuyetToanDetailModel.IdNguonVon) && objectSender.IdNguonVon != 0
        //        || (args.PropertyName == nameof(PheDuyetQuyetToanDetailModel.GtDuToan) && objectSender.IdNguonVon != 0)
        //        || (args.PropertyName == nameof(PheDuyetQuyetToanDetailModel.GtQuyetToan) && objectSender.IdNguonVon != 0))
        //    {
        //        item = Items.Where(x => x.IdNguonVon == objectSender.IdNguonVon).First();

        //        if (CheckDuplicateNguonVon(objectSender.IdNguonVon))
        //        {
        //            System.Windows.Forms.MessageBox.Show(Resources.MsgCheckTrungNguonVonDauTu, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            SelectedItem.IdNguonVon = 0;
        //            return;
        //        }
        //    }

        //    item.IsModified = true;

        //    if ((args.PropertyName == nameof(PheDuyetQuyetToanDetailModel.GtDuToan) && objectSender.GtDuToan != 0)
        //        || (args.PropertyName == nameof(PheDuyetQuyetToanDetailModel.GtQuyetToan) && objectSender.GtQuyetToan != 0)
        //        )
        //    {
        //        if (checkLoai == (int)QDDauTuTypeEnum.CHIPHI && objectSender.IdChiPhi.HasValue && objectSender.IdChiPhi == Guid.Empty)
        //        {
        //            System.Windows.Forms.MessageBox.Show(Resources.MsgCheckChiPhiDauTu, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            ((PheDuyetQuyetToanDetailModel)sender).GtDuToan = 0;
        //            ((PheDuyetQuyetToanDetailModel)sender).GtQuyetToan = 0;
        //            return;
        //        }
        //        if (checkLoai == (int)QDDauTuTypeEnum.NGUONVON && objectSender.IdNguonVon == 0)
        //        {
        //            System.Windows.Forms.MessageBox.Show(Resources.MsgCheckNguonVonDauTu, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            ((PheDuyetQuyetToanDetailModel)sender).GtDuToan = 0;
        //            ((PheDuyetQuyetToanDetailModel)sender).GtQuyetToan = 0;
        //            return;
        //        }
        //        CalculateParent();
        //    }
        //    OnPropertyChanged(nameof(IsSaveData));
        //}

        //private void OnCloseWindow()
        //{
        //    DataChangedEventHandler handler = ClosePopup;
        //    if (handler != null)
        //    {
        //        handler(this, new EventArgs());
        //    }
        //}
    }
}
