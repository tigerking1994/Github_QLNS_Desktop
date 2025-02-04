using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.GoiThau
{
    public class GoiThauDetailViewModel : DetailViewModelBase<VdtDaGoiThauModel, GoiThauDetailModel>
    {
        private IMapper _mapper;
        private ISessionService _sessionService;
        private IApproveProjectService _approveProjectService;
        private IVdtDaGoiThauService _vdtDaGoiThauService;
        private readonly IVdtQddtKhlcnhaThauService _vdtQddtKhlcnhaThauService;

        public override string Name => "THÔNG TIN GÓI THẦU CHI TIẾT";
        public override string Title => "Quản lý thông tin gói thầu";

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler ClosePopup;
        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted);

        private List<ComboboxItem> _dataChiPhi;
        public List<ComboboxItem> DataChiPhi
        {
            get => _dataChiPhi;
            set => SetProperty(ref _dataChiPhi, value);
        }

        private List<ComboboxItem> _dataNguonVon;
        public List<ComboboxItem> DataNguonVon
        {
            get => _dataNguonVon;
            set => SetProperty(ref _dataNguonVon, value);
        }

        private List<ComboboxItem> _dataHangMuc;
        public List<ComboboxItem> DataHangMuc
        {
            get => _dataHangMuc;
            set => SetProperty(ref _dataHangMuc, value);
        }

        private ObservableCollection<VTS.QLNS.CTC.App.Model.GoiThauNguonVonModel> _goiThauNguonVonItems;
        public ObservableCollection<VTS.QLNS.CTC.App.Model.GoiThauNguonVonModel> GoiThauNguonVonItems
        {
            get => _goiThauNguonVonItems;
            set => SetProperty(ref _goiThauNguonVonItems, value);
        }

        private ObservableCollection<VTS.QLNS.CTC.App.Model.GoiThauChiPhiModel> _goiThauChiPhiItems;
        public ObservableCollection<VTS.QLNS.CTC.App.Model.GoiThauChiPhiModel> GoiThauChiPhiItems
        {
            get => _goiThauChiPhiItems;
            set => SetProperty(ref _goiThauChiPhiItems, value);
        }

        private GoiThauChiPhiModel _selectedChiPhi;
        public GoiThauChiPhiModel SelectedChiPhi
        {
            get => _selectedChiPhi;
            set => SetProperty(ref _selectedChiPhi, value);
        }

        private ObservableCollection<VTS.QLNS.CTC.App.Model.GoiThauHangMucModel> _goiThauHangMucItems;
        public ObservableCollection<VTS.QLNS.CTC.App.Model.GoiThauHangMucModel> GoiThauHangMucItems
        {
            get => _goiThauHangMucItems;
            set => SetProperty(ref _goiThauHangMucItems, value);
        }

        public RelayCommand CloseWindowCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand ShowHangMucDetailCommand { get; }

        public GoiThauDetailViewModel(
            IMapper mapper,
            ISessionService sessionService,
            IApproveProjectService approveProjectService,
            IVdtDaGoiThauService vdtDaGoiThauService,
            IVdtQddtKhlcnhaThauService vdtQddtKhlcnhaThauService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _approveProjectService = approveProjectService;
            _vdtDaGoiThauService = vdtDaGoiThauService;
            _vdtQddtKhlcnhaThauService = vdtQddtKhlcnhaThauService;

            CloseWindowCommand = new RelayCommand(obj => OnCloseWindow());
            SaveCommand = new RelayCommand(obj => OnSaveData());
            ShowHangMucDetailCommand = new RelayCommand(obj => LoadGoiThauHangMuc());
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(10);
            LoadData();
            CalculateParent();
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsSaveData));
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            LoadGoiThauNguonVon();
            LoadGoiThauChiPhi();
            IEnumerable<VdtDaGoiThauDetailQuery> data = _vdtDaGoiThauService.FindListDetail(Model.Id);
            Items = _mapper.Map<ObservableCollection<Model.GoiThauDetailModel>>(data);

            foreach (GoiThauDetailModel model in Items)
            {
                if (!model.IsHangCha)
                {
                    model.PropertyChanged += DetailModel_PropertyChanged;
                }
            }
            CalculateParent();
        }

        private void LoadGoiThauNguonVon()
        {
            IEnumerable<GoiThauNguonVonQuery> listData = _vdtDaGoiThauService.FindListGoiThauNguonVon(Model.Id);
            GoiThauNguonVonItems = _mapper.Map<ObservableCollection<Model.GoiThauNguonVonModel>>(listData);
        }

        private void LoadGoiThauChiPhi()
        {
            IEnumerable<GoiThauChiPhiQuery> listData = _vdtDaGoiThauService.FindListGoiThauChiPhi(Model.Id);
            GoiThauChiPhiItems = _mapper.Map<ObservableCollection<Model.GoiThauChiPhiModel>>(listData);
        }

        private void LoadGoiThauHangMuc()
        {
            VdtQddtKhlcnhaThau khlcnt = _vdtQddtKhlcnhaThauService.Find(Model.IIdKhlcnhaThau.Value);
            if (khlcnt == null)
            {
                return;
            }
            var isDuToan = khlcnt.IIdQDDauTuID.IsNullOrEmpty() && !khlcnt.IIdDuToanId.IsNullOrEmpty();

            IEnumerable<GoiThauHangMucQuery> listData = _vdtDaGoiThauService.FindListGoiThauHangMuc(Model.Id, SelectedChiPhi.IdChiPhi, isDuToan);
            GoiThauHangMucItems = _mapper.Map<ObservableCollection<Model.GoiThauHangMucModel>>(listData);
        }

        private void CalculateParent()
        {
            var parentItemChiPhi = Items.Where(x => x.IsHangCha == true && x.Loai == (int)QDDauTuTypeEnum.CHIPHI).FirstOrDefault();
            var parentItemNguonVon = Items.Where(x => x.IsHangCha == true && x.Loai == (int)QDDauTuTypeEnum.NGUONVON).FirstOrDefault();
            var parentItemHangMuc = Items.Where(x => x.IsHangCha == true && x.Loai == (int)QDDauTuTypeEnum.HANGMUC).FirstOrDefault();
            var listChiphi = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.Loai == (int)QDDauTuTypeEnum.CHIPHI).ToList();
            var listNguonVon = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.Loai == (int)QDDauTuTypeEnum.NGUONVON).ToList();
            var listHangMuc = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.Loai == (int)QDDauTuTypeEnum.HANGMUC).ToList();
            parentItemChiPhi.GiaTriPheDuyet = 0;
            parentItemNguonVon.GiaTriPheDuyet = 0;
            parentItemHangMuc.GiaTriPheDuyet = 0;

            foreach (var item in listChiphi)
            {
                parentItemChiPhi.GiaTriPheDuyet += item.GiaTriPheDuyet;
            }
            foreach (var item in listNguonVon)
            {
                parentItemNguonVon.GiaTriPheDuyet += item.GiaTriPheDuyet;
            }
            foreach (var item in listHangMuc)
            {
                parentItemHangMuc.GiaTriPheDuyet += item.GiaTriPheDuyet;
            }

        }

        private void OnCloseWindow()
        {
            DataChangedEventHandler handler = ClosePopup;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        protected override void OnAdd()
        {
            int currentRow = 0;
            if (SelectedItem != null)
            {
                currentRow = Items.IndexOf(SelectedItem);
            }
            GoiThauDetailModel sourceItem = Items.ElementAt(currentRow);
            GoiThauDetailModel targetItem = ObjectCopier.Clone(sourceItem);
            targetItem.IsHangCha = false;
            targetItem.IdGoiThauChiPhi = Guid.Empty;
            targetItem.IdGoiThauNguonVon = Guid.Empty;
            targetItem.PropertyChanged += DetailModel_PropertyChanged;
            Items.Insert(currentRow + 1, targetItem);
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsSaveData));
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            GoiThauDetailModel item = new GoiThauDetailModel();
            GoiThauDetailModel objectSender = (GoiThauDetailModel)sender;
            int checkLoai = objectSender.Loai;

            if (args.PropertyName == nameof(GoiThauDetailModel.IdChiPhi) && objectSender.IdChiPhi != Guid.Empty
                || (args.PropertyName == nameof(GoiThauDetailModel.GiaTriPheDuyet) && objectSender.IdChiPhi != Guid.Empty))
            {
                item = Items.Where(x => x.IdChiPhi == objectSender.IdChiPhi).First();

                if (CheckDuplicateChiPhi(objectSender.IdChiPhi))
                {
                    System.Windows.Forms.MessageBox.Show(Resources.MsgCheckTrungChiPhiDauTu, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SelectedItem.IdChiPhi = Guid.Empty;
                    return;
                }

                if (args.PropertyName == nameof(GoiThauDetailModel.IdChiPhi))
                {
                    item.TongMucDT = _vdtDaGoiThauService.GetTongMucDTChiPhi(item.IdChiPhi, Model.IIdDuAnId.Value, Model.DNgayLap.Value);
                }

            }
            if (args.PropertyName == nameof(GoiThauDetailModel.IdNguonVon) && objectSender.IdNguonVon != 0
                || (args.PropertyName == nameof(GoiThauDetailModel.GiaTriPheDuyet) && objectSender.IdNguonVon != 0))
            {
                item = Items.Where(x => x.IdNguonVon == objectSender.IdNguonVon).First();

                if (CheckDuplicateNguonVon(objectSender.IdNguonVon))
                {
                    System.Windows.Forms.MessageBox.Show(Resources.MsgCheckTrungNguonVonDauTu, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SelectedItem.IdNguonVon = 0;
                    return;
                }

                if (args.PropertyName == nameof(GoiThauDetailModel.IdNguonVon))
                {
                    item.TongMucDT = _vdtDaGoiThauService.GetTongMucDTNguonVon(item.IdNguonVon, Model.IIdDuAnId.Value, Model.DNgayLap.Value);
                }
            }

            if (args.PropertyName == nameof(GoiThauDetailModel.IdHangMuc) && objectSender.IdHangMuc != Guid.Empty
                || (args.PropertyName == nameof(GoiThauDetailModel.GiaTriPheDuyet) && objectSender.IdHangMuc != Guid.Empty))
            {
                item = Items.Where(x => x.IdHangMuc == objectSender.IdHangMuc).First();

                if (CheckDuplicateHangMuc(objectSender.IdHangMuc))
                {
                    System.Windows.Forms.MessageBox.Show(Resources.MsgCheckTrungHangMucDauTu, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SelectedItem.IdHangMuc = Guid.Empty;
                    return;
                }

                if (args.PropertyName == nameof(GoiThauDetailModel.IdHangMuc))
                {
                    item.TongMucDT = _vdtDaGoiThauService.GetTongMucDTHangMuc(item.IdHangMuc, Model.IIdDuAnId.Value, Model.DNgayLap.Value);
                }
            }

            item.IsModified = true;

            if (args.PropertyName == nameof(GoiThauDetailModel.GiaTriPheDuyet) && objectSender.GiaTriPheDuyet != 0)
            {
                if (checkLoai == (int)QDDauTuTypeEnum.CHIPHI && objectSender.IdChiPhi == Guid.Empty)
                {
                    System.Windows.Forms.MessageBox.Show(Resources.MsgCheckChiPhiDauTu, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ((GoiThauDetailModel)sender).GiaTriPheDuyet = 0;
                    return;
                }
                if (checkLoai == (int)QDDauTuTypeEnum.NGUONVON && objectSender.IdNguonVon == 0)
                {
                    System.Windows.Forms.MessageBox.Show(Resources.MsgCheckNguonVonDauTu, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ((GoiThauDetailModel)sender).GiaTriPheDuyet = 0;
                    return;
                }

                CalculateParent();
            }


            OnPropertyChanged(nameof(IsSaveData));
        }

        private bool CheckDuplicateChiPhi(Guid idChiPhi)
        {
            List<GoiThauDetailModel> listChiPhi = Items.Where(x => x.IdChiPhi == idChiPhi).ToList();
            if (listChiPhi != null && listChiPhi.Count > 1)
            {
                return true;
            }
            return false;
        }

        private bool CheckDuplicateNguonVon(int idNguonVon)
        {
            List<GoiThauDetailModel> listNguonVon = Items.Where(x => x.IdNguonVon == idNguonVon).ToList();
            if (listNguonVon != null && listNguonVon.Count > 1)
            {
                return true;
            }
            return false;
        }

        private bool CheckDuplicateHangMuc(Guid idHangMuc)
        {
            List<GoiThauDetailModel> listHangMuc = Items.Where(x => x.IdHangMuc == idHangMuc).ToList();
            if (listHangMuc != null && listHangMuc.Count > 1)
            {
                return true;
            }
            return false;
        }

        private void OnSaveData()
        {
            //check duplicate trong list detail
            if (!ValidateDataSave())
            {
                return;
            }

            List<GoiThauDetailModel> listAdd = Items.Where(x => !x.IsHangCha && !x.IsDeleted).ToList();
            List<GoiThauDetailModel> listEdit = Items.Where(x => x.IsModified && !x.IsHangCha && !x.IsDeleted).ToList();
            List<GoiThauDetailModel> listDetailDelete = Items.Where(x => x.IsDeleted).ToList();

            //Thêm mới,sửa vào các bảng chi tiết
            if (listAdd.Count > 0)
            {
                AddDetail(listAdd);
            }
            if (listEdit != null && listEdit.Count > 0)
            {
                UpdateDetail(listEdit);
            }

            if (listDetailDelete.Count > 0)
            {
                DeleteDetail(listDetailDelete);
            }

            System.Windows.Forms.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();
        }

        public bool ValidateDataSave()
        {
            // validate trùng
            List<GoiThauDetailModel> listChiPhi = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.Loai == (int)QDDauTuTypeEnum.CHIPHI).ToList();
            var anyDuplicateChiPhi = listChiPhi.GroupBy(x => x.IdChiPhi).Any(g => g.Count() > 1);
            if (anyDuplicateChiPhi)
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckTrungChiPhiDauTu, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            List<GoiThauDetailModel> listNguonVon = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.Loai == (int)QDDauTuTypeEnum.NGUONVON).ToList();
            var anyDuplicateNguonVon = listNguonVon.GroupBy(x => x.IdNguonVon).Any(g => g.Count() > 1);
            if (anyDuplicateNguonVon)
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckTrungNguonVonDauTu, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            List<GoiThauDetailModel> listHangMuc = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.Loai == (int)QDDauTuTypeEnum.HANGMUC).ToList();
            var anyDuplicateHangMuc = listHangMuc.GroupBy(x => x.IdHangMuc).Any(g => g.Count() > 1);
            if (anyDuplicateHangMuc)
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckTrungHangMucDauTu, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            //validate tổng chi phí = tổng nguồn vốn
            var tongChiPhi = Items.Where(x => x.IsHangCha && x.Loai == (int)QDDauTuTypeEnum.CHIPHI).FirstOrDefault().GiaTriPheDuyet;
            var tongNguonVon = Items.Where(x => x.IsHangCha && x.Loai == (int)QDDauTuTypeEnum.NGUONVON).FirstOrDefault().GiaTriPheDuyet;
            if (tongChiPhi != tongNguonVon)
            {
                System.Windows.Forms.MessageBox.Show(Resources.ErrorChiPhiNotEqualNguonVon, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void AddDetail(List<GoiThauDetailModel> listAdd)
        {
            #region update tổng chi phí vào bảng VdtDaGoiThau
            var tongChiPhi = Items.Where(x => x.IsHangCha && x.Loai == (int)QDDauTuTypeEnum.CHIPHI).FirstOrDefault().GiaTriPheDuyet;
            VdtDaGoiThau goiThau = _vdtDaGoiThauService.FindById(Model.Id);
            if (goiThau != null)
            {
                goiThau.FTienTrungThau = tongChiPhi;
                _vdtDaGoiThauService.Update(goiThau);
            }
            #endregion

            #region add Chi phi
            List<GoiThauDetailModel> listChiPhiAdd = listAdd.Where(x => x.Loai == (int)QDDauTuTypeEnum.CHIPHI && x.IdChiPhi != Guid.Empty && x.IdGoiThauChiPhi == Guid.Empty).ToList();

            if (listChiPhiAdd != null && listChiPhiAdd.Count > 0)
            {
                List<VdtDaGoiThauChiPhi> listGoiThauCP = new List<VdtDaGoiThauChiPhi>();
                listGoiThauCP = _mapper.Map<List<VdtDaGoiThauChiPhi>>(listChiPhiAdd);
                _vdtDaGoiThauService.AddRangeGoiThauChiPhi(listGoiThauCP);
            }
            #endregion

            #region add Nguon von
            List<GoiThauDetailModel> listNguonVonAdd = listAdd.Where(x => x.Loai == (int)QDDauTuTypeEnum.NGUONVON && x.IdNguonVon != 0 && x.IdGoiThauNguonVon == Guid.Empty).ToList();

            if (listNguonVonAdd != null && listNguonVonAdd.Count > 0)
            {
                List<VdtDaGoiThauNguonVon> listGoiThauNV = new List<VdtDaGoiThauNguonVon>();
                listGoiThauNV = _mapper.Map<List<VdtDaGoiThauNguonVon>>(listNguonVonAdd);
                _vdtDaGoiThauService.AddRangeGoiThauNguonVon(listGoiThauNV);
            }
            #endregion

            #region add Hang muc
            List<GoiThauDetailModel> listHangMucAdd = listAdd.Where(x => x.Loai == (int)QDDauTuTypeEnum.HANGMUC && x.IdHangMuc != Guid.Empty && x.IdGoiThauHangMuc == Guid.Empty).ToList();

            if (listHangMucAdd != null && listHangMucAdd.Count > 0)
            {
                List<VdtDaGoiThauHangMuc> listGoiThauHangMuc = new List<VdtDaGoiThauHangMuc>();
                listGoiThauHangMuc = _mapper.Map<List<VdtDaGoiThauHangMuc>>(listHangMucAdd);
                _vdtDaGoiThauService.AddRangeGoiThauHangMuc(listGoiThauHangMuc);
            }
            #endregion

        }

        private void UpdateDetail(List<GoiThauDetailModel> listEdit)
        {
            #region update tổng chi phí vào bảng VdtDaGoiThau
            var tongChiPhi = Items.Where(x => x.IsHangCha && x.Loai == (int)QDDauTuTypeEnum.CHIPHI).FirstOrDefault().GiaTriPheDuyet;
            VdtDaGoiThau goiThau = _vdtDaGoiThauService.FindById(Model.Id);
            if (goiThau != null)
            {
                goiThau.FTienTrungThau = tongChiPhi;
                _vdtDaGoiThauService.Update(goiThau);
            }
            #endregion

            List<GoiThauDetailModel> listChiPhiEdit = listEdit.Where(x => x.Loai == (int)QDDauTuTypeEnum.CHIPHI && x.IdGoiThauChiPhi != Guid.Empty).ToList();
            if (listChiPhiEdit.Count > 0)
            {
                foreach (var item in listChiPhiEdit)
                {
                    VdtDaGoiThauChiPhi goiThauChiPhi = _vdtDaGoiThauService.FindGoiThauChiPhi(item.IdGoiThauChiPhi);
                    _mapper.Map(item, goiThauChiPhi);
                    _vdtDaGoiThauService.UpdateGoiThauChiPhi(goiThauChiPhi);
                }
            }

            List<GoiThauDetailModel> listNguonVonEdit = listEdit.Where(x => x.Loai == (int)QDDauTuTypeEnum.NGUONVON && x.IdGoiThauNguonVon != Guid.Empty).ToList();
            if (listNguonVonEdit.Count > 0)
            {
                foreach (var item in listNguonVonEdit)
                {
                    VdtDaGoiThauNguonVon goiThauNguonVon = _vdtDaGoiThauService.FindGoiThauNguonVon(item.IdGoiThauNguonVon);
                    _mapper.Map(item, goiThauNguonVon);
                    _vdtDaGoiThauService.UpdateGoiThauNguonVon(goiThauNguonVon);
                }
            }

            List<GoiThauDetailModel> listHangMucEdit = listEdit.Where(x => x.Loai == (int)QDDauTuTypeEnum.HANGMUC && x.IdGoiThauHangMuc != Guid.Empty).ToList();
            if (listHangMucEdit.Count > 0)
            {
                foreach (var item in listHangMucEdit)
                {
                    VdtDaGoiThauHangMuc goiThauHangMuc = _vdtDaGoiThauService.FindGoiThauHangMuc(item.IdGoiThauHangMuc);
                    _mapper.Map(item, goiThauHangMuc);
                    _vdtDaGoiThauService.UpdateGoiThauHangMuc(goiThauHangMuc);
                }
            }
        }

        private void DeleteDetail(List<GoiThauDetailModel> listDelete)
        {
            List<GoiThauDetailModel> listChiPhiDelete = listDelete.Where(x => x.Loai == (int)QDDauTuTypeEnum.CHIPHI && x.IdGoiThauChiPhi != Guid.Empty).ToList();
            foreach (var item in listChiPhiDelete)
            {
                _vdtDaGoiThauService.DeleteGoiThauChiPhi(item.IdGoiThauChiPhi);
            }

            List<GoiThauDetailModel> listNguonVonDelete = listDelete.Where(x => x.Loai == (int)QDDauTuTypeEnum.NGUONVON && x.IdGoiThauNguonVon != Guid.Empty).ToList();
            foreach (var item in listNguonVonDelete)
            {
                _vdtDaGoiThauService.DeleteGoiThauNguonVon(item.IdGoiThauNguonVon);
            }

            List<GoiThauDetailModel> listHangMucDelete = listDelete.Where(x => x.Loai == (int)QDDauTuTypeEnum.HANGMUC && x.IdGoiThauHangMuc != Guid.Empty).ToList();
            foreach (var item in listHangMucDelete)
            {
                _vdtDaGoiThauService.DeleteGoiThauHangMuc(item.IdGoiThauHangMuc);
            }
        }

        protected override void OnDelete()
        {
            if (Items != null && Items.Count > 0 && SelectedItem != null && !SelectedItem.IsHangCha)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
                CalculateParent();
                OnPropertyChanged(nameof(IsSaveData));
            }
        }

    }
}
