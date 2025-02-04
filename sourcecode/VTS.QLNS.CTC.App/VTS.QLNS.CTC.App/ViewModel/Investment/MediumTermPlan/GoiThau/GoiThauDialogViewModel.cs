using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.GoiThau
{
    public class GoiThauDialogViewModel : DialogViewModelBase<VdtDaGoiThauModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nsDonViService;
        private readonly IVdtDaGoiThauService _vdtDaGoiThauService;
        private readonly IApproveProjectService _approveProjectService;
        private readonly IProjectManagerService _projectManagerService;

        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_GOI_THAU_DIALOG;
        public override string Name => string.Format("{0} THÔNG TIN GÓI THẦU", IsDetail ? "CHI TIẾT" : "CẬP NHẬT");
        public override string Title => IsDetail ? "CHI TIẾT" : "CẬP NHẬT";
        public override string Description => string.Format("{0} thông tin gói thầu", IsDetail ? "Chi tiết" : "Cập nhật");

        public override Type ContentType => typeof(View.Investment.MediumTermPlan.GoiThau.GoiThauDialog);
        private VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.GoiThau.GoiThauDetail view;
        public GoiThauDetailViewModel GoiThauDetailViewModel { get; }

        private bool _isDetail;
        public bool IsDetail
        {
            get => _isDetail;
            set => SetProperty(ref _isDetail, value);
        }

        private VdtDaGoiThauModel _goiThau;
        public VdtDaGoiThauModel GoiThau
        {
            get => _goiThau;
            set => SetProperty(ref _goiThau, value);
        }

        private ObservableCollection<ComboboxItem> _dataDonVi;
        public ObservableCollection<ComboboxItem> DataDonVi
        {
            get => _dataDonVi;
            set => SetProperty(ref _dataDonVi, value);
        }

        private ComboboxItem _selectedDonVi;
        public ComboboxItem SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                if (SetProperty(ref _selectedDonVi, value))
                {
                    LoadDuAnByDonVi();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _dataDuAn;
        public ObservableCollection<ComboboxItem> DataDuAn
        {
            get => _dataDuAn;
            set => SetProperty(ref _dataDuAn, value);
        }

        private ComboboxItem _selectedDuAn;
        public ComboboxItem SelectedDuAn
        {
            get => _selectedDuAn;
            set
            {
                if (SetProperty(ref _selectedDuAn, value))
                {
                    GetDuAnChiTiet();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _dataNhaThau;
        public ObservableCollection<ComboboxItem> DataNhaThau
        {
            get => _dataNhaThau;
            set => SetProperty(ref _dataNhaThau, value);
        }

        private ComboboxItem _selectedNhaThau;
        public ComboboxItem SelectedNhaThau
        {
            get => _selectedNhaThau;
            set => SetProperty(ref _selectedNhaThau, value);
        }

        private List<ComboboxItem> _dataLoaiGoiThau;
        public List<ComboboxItem> DataLoaiGoiThau
        {
            get => _dataLoaiGoiThau;
            set => SetProperty(ref _dataLoaiGoiThau, value);
        }

        private ComboboxItem _selectedLoaiGoiThau;
        public ComboboxItem SelectedLoaiGoiThau
        {
            get => _selectedLoaiGoiThau;
            set => SetProperty(ref _selectedLoaiGoiThau, value);
        }

        private List<ComboboxItem> _dataHTChonNhaThau;
        public List<ComboboxItem> DataHTChonNhaThau
        {
            get => _dataHTChonNhaThau;
            set => SetProperty(ref _dataHTChonNhaThau, value);
        }

        private ComboboxItem _selectedHTChonNhaThau;
        public ComboboxItem SelectedHTChonNhaThau
        {
            get => _selectedHTChonNhaThau;
            set => SetProperty(ref _selectedHTChonNhaThau, value);
        }

        private List<ComboboxItem> _dataPTDauThau;
        public List<ComboboxItem> DataPTDauThau
        {
            get => _dataPTDauThau;
            set => SetProperty(ref _dataPTDauThau, value);
        }

        private ComboboxItem _selectedPTDauThau;
        public ComboboxItem SelectedPTDauThau
        {
            get => _selectedPTDauThau;
            set => SetProperty(ref _selectedPTDauThau, value);
        }

        private List<ComboboxItem> _dataHTHopDong;
        public List<ComboboxItem> DataHTHopDong
        {
            get => _dataHTHopDong;
            set => SetProperty(ref _dataHTHopDong, value);
        }

        private ComboboxItem _selectedHTHopDong;
        public ComboboxItem SelectedHTHopDong
        {
            get => _selectedHTHopDong;
            set => SetProperty(ref _selectedHTHopDong, value);
        }

        private List<NhaThauHopDongModel> _nhaThauHopDongItems;
        public List<NhaThauHopDongModel> NhaThauHopDongItems
        {
            get => _nhaThauHopDongItems;
            set => SetProperty(ref _nhaThauHopDongItems, value);
        }

        public RelayCommand OnShowDetail { get; set; }

        public GoiThauDialogViewModel(
            GoiThauDetailViewModel goiThauDetailViewModel,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IVdtDaGoiThauService vdtDaGoiThauService,
            IApproveProjectService approveProjectService,
            IProjectManagerService projectManagerService)
        {
            GoiThauDetailViewModel = goiThauDetailViewModel;
            GoiThauDetailViewModel.ParentPage = this;

            _mapper = mapper;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _vdtDaGoiThauService = vdtDaGoiThauService;
            _approveProjectService = approveProjectService;
            _projectManagerService = projectManagerService;
            OnShowDetail = new RelayCommand(obj => ShowDetail());
        }

        public override void Init()
        {
            LoadDonVi();
            LoadNhaThau();
            LoadDataLoaiGoiThau();
            LoadDataHTChonNhaThau();
            LoadDataHTHopDong();
            LoadDataPTDauThau();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);

            if (GoiThau.Id == Guid.Empty)
            {
                GoiThau = new Model.VdtDaGoiThauModel();
                //GoiThau.IsAdd = true;
            }
            if (!GoiThau.Id.IsNullOrEmpty())
            {
                //GoiThau.IsAdd = false;
                if (GoiThau.NgayQuyetDinh == null)
                {
                    GoiThau.NgayQuyetDinh = DateTime.Now;
                }

                if (DataDonVi != null && !string.IsNullOrEmpty(GoiThau.Id_DonVi))
                {
                    SelectedDonVi = DataDonVi.Where(x => x.ValueItem == GoiThau.Id_DonVi.ToString()).FirstOrDefault();
                }
                if (DataDuAn != null && GoiThau.IIdDuAnId.HasValue)
                {
                    SelectedDuAn = DataDuAn.Where(x => x.ValueItem == GoiThau.IIdDuAnId.ToString()).FirstOrDefault();
                }
                if (DataNhaThau != null && GoiThau.IIdNhaThauId.HasValue)
                {
                    SelectedNhaThau = DataNhaThau.Where(x => x.ValueItem == GoiThau.IIdNhaThauId.ToString()).FirstOrDefault();
                }
                if (DataLoaiGoiThau != null && !string.IsNullOrEmpty(GoiThau.LoaiGoiThau))
                {
                    SelectedLoaiGoiThau = DataLoaiGoiThau.Where(x => x.ValueItem == GoiThau.LoaiGoiThau).FirstOrDefault();
                }
                if (DataHTChonNhaThau != null && !string.IsNullOrEmpty(GoiThau.SHinhThucChonNhaThau))
                {
                    SelectedHTChonNhaThau = DataHTChonNhaThau.Where(x => x.ValueItem == GoiThau.SHinhThucChonNhaThau).FirstOrDefault();
                }
                if (DataPTDauThau != null && !string.IsNullOrEmpty(GoiThau.SPhuongThucDauThau))
                {
                    SelectedPTDauThau = DataPTDauThau.Where(x => x.ValueItem == GoiThau.SPhuongThucDauThau).FirstOrDefault();
                }
                if (DataHTHopDong != null && !string.IsNullOrEmpty(GoiThau.SHinhThucHopDong))
                {
                    SelectedHTHopDong = DataHTHopDong.Where(x => x.ValueItem == GoiThau.SHinhThucHopDong).FirstOrDefault();
                }
                // tìm tổng mức đầu tư theo QDDT
                VdtDaQddauTu qdDauTu = _approveProjectService.FindByDuAnId(GoiThau.IIdDuAnId.Value);
                if (qdDauTu != null)
                {
                    GoiThau.FTongMucDauTu = qdDauTu.FTongMucDauTuPheDuyet;
                }

                // default date value
                if (GoiThau.DKetThucChonNhaThau is null)
                {
                    GoiThau.DKetThucChonNhaThau = DateTime.Now;
                }

                GoiThau.DBatDauChonNhaThau = DateTime.Now;

                LoadListNhaThauHopDong();
            }

        }

        private void LoadDonVi()
        {
            IEnumerable<DonVi> listDonVi = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork).Where(x => x.Loai == LoaiDonVi.NOI_BO);
            _dataDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDonVi);
        }

        private void LoadDuAnByDonVi()
        {
            if (SelectedDonVi == null || string.IsNullOrEmpty(SelectedDonVi.ValueItem))
            {
                return;
            }
            DonVi donVi = _nsDonViService.FindByIdDonVi(SelectedDonVi.ValueItem, _sessionService.Current.YearOfWork);
            if (donVi != null)
            {
                IEnumerable<VdtDaDuAn> listDuAnByDonVi = _vdtDaGoiThauService.FindDuAnByDonViGoiThau(SelectedDonVi.ValueItem, _sessionService.Current.YearOfWork);
                _dataDuAn = _mapper.Map<ObservableCollection<ComboboxItem>>(listDuAnByDonVi);
                OnPropertyChanged(nameof(DataDuAn));
            }
        }

        private void LoadDataLoaiGoiThau()
        {
            List<ComboboxItem> listLoaiGoiThau = new List<ComboboxItem>();
            listLoaiGoiThau.Add(new ComboboxItem { ValueItem = LoaiGoiThauType.TUVAN, DisplayItem = LoaiGoiThauTypeName.TU_VAN });
            listLoaiGoiThau.Add(new ComboboxItem { ValueItem = LoaiGoiThauType.XAYLAP, DisplayItem = LoaiGoiThauTypeName.XAY_LAP });
            DataLoaiGoiThau = listLoaiGoiThau;
        }

        private void LoadDataHTChonNhaThau()
        {
            List<ComboboxItem> listHTChonNhaThau = new List<ComboboxItem>();
            listHTChonNhaThau.Add(new ComboboxItem { ValueItem = HTChonNhaThauTypeName.HT_1, DisplayItem = HTChonNhaThauTypeName.HT_1 });
            listHTChonNhaThau.Add(new ComboboxItem { ValueItem = HTChonNhaThauTypeName.HT_2, DisplayItem = HTChonNhaThauTypeName.HT_2 });
            listHTChonNhaThau.Add(new ComboboxItem { ValueItem = HTChonNhaThauTypeName.HT_3, DisplayItem = HTChonNhaThauTypeName.HT_3 });
            listHTChonNhaThau.Add(new ComboboxItem { ValueItem = HTChonNhaThauTypeName.HT_4, DisplayItem = HTChonNhaThauTypeName.HT_4 });
            listHTChonNhaThau.Add(new ComboboxItem { ValueItem = HTChonNhaThauTypeName.HT_5, DisplayItem = HTChonNhaThauTypeName.HT_5 });
            listHTChonNhaThau.Add(new ComboboxItem { ValueItem = HTChonNhaThauTypeName.HT_6, DisplayItem = HTChonNhaThauTypeName.HT_6 });
            listHTChonNhaThau.Add(new ComboboxItem { ValueItem = HTChonNhaThauTypeName.HT_7, DisplayItem = HTChonNhaThauTypeName.HT_7 });
            listHTChonNhaThau.Add(new ComboboxItem { ValueItem = HTChonNhaThauTypeName.HT_8, DisplayItem = HTChonNhaThauTypeName.HT_8 });

            DataHTChonNhaThau = listHTChonNhaThau;
        }

        private void LoadDataPTDauThau()
        {
            List<ComboboxItem> listPTDauThau = new List<ComboboxItem>();
            listPTDauThau.Add(new ComboboxItem { ValueItem = PTDauThauTypeName.PT_1, DisplayItem = PTDauThauTypeName.PT_1 });
            listPTDauThau.Add(new ComboboxItem { ValueItem = PTDauThauTypeName.PT_2, DisplayItem = PTDauThauTypeName.PT_2 });
            listPTDauThau.Add(new ComboboxItem { ValueItem = PTDauThauTypeName.PT_3, DisplayItem = PTDauThauTypeName.PT_3 });
            listPTDauThau.Add(new ComboboxItem { ValueItem = PTDauThauTypeName.PT_4, DisplayItem = PTDauThauTypeName.PT_4 });
            DataPTDauThau = listPTDauThau;
        }

        private void LoadDataHTHopDong()
        {
            List<ComboboxItem> listHTHopDong = new List<ComboboxItem>();
            listHTHopDong.Add(new ComboboxItem { ValueItem = HTHopDongTypeName.HD_1, DisplayItem = HTHopDongTypeName.HD_1 });
            listHTHopDong.Add(new ComboboxItem { ValueItem = HTHopDongTypeName.HD_2, DisplayItem = HTHopDongTypeName.HD_2 });
            listHTHopDong.Add(new ComboboxItem { ValueItem = HTHopDongTypeName.HD_3, DisplayItem = HTHopDongTypeName.HD_3 });

            DataHTHopDong = listHTHopDong;
        }

        private void LoadNhaThau()
        {
            IEnumerable<VdtDmNhaThau> listNhaThau = _vdtDaGoiThauService.GetAllNhaThau();
            _dataNhaThau = _mapper.Map<ObservableCollection<ComboboxItem>>(listNhaThau);
            //if (GoiThau.Id == Guid.Empty || GoiThau.IIdNhaThauId == null)
            //{
            //    SelectedNhaThau = null;
            //}

            //else
            //{
            //    SelectedNhaThau.ValueItem = _dataNhaThau.Where(x=> x.ValueItem == GoiThau.IIdNhaThauId.ToString()).Select(x=> x.Id.ToString()).First();
            //}
        }

        private void GetDuAnChiTiet()
        {
            if (SelectedDuAn != null && SelectedDuAn.ValueItem != null)
            {
                VdtDaDuAn duAn = _projectManagerService.FindById(Guid.Parse(SelectedDuAn.ValueItem));
                if (duAn != null)
                {
                    GoiThau.SDiaDiem = duAn.SDiaDiem;
                    GoiThau.FTongMucDauTu = duAn.FTongMucDauTu;
                    GoiThau.ThoiGianThucHien = duAn.SKhoiCong + "-" + duAn.SKetThuc;

                    // tìm tổng mức đầu tư theo QDDT
                    VdtDaQddauTu qdDauTu = _approveProjectService.FindByDuAnId(duAn.Id);
                    if (qdDauTu != null)
                    {
                        GoiThau.FTongMucDauTu = qdDauTu.FTongMucDauTuPheDuyet;
                    }
                    OnPropertyChanged(nameof(GoiThau));
                }
            }
        }

        private void LoadListNhaThauHopDong()
        {
            var listNhaThauHopDongQuery = _vdtDaGoiThauService.FindListNhaThauHopDongByGoiThau(GoiThau.Id);
            NhaThauHopDongItems = _mapper.Map<List<NhaThauHopDongModel>>(listNhaThauHopDongQuery);
        }

        public override void OnSave()
        {
            if (SelectedDuAn != null)
            {
                GoiThau.IIdDuAnId = Guid.Parse(SelectedDuAn.ValueItem);
            }
            if (SelectedNhaThau != null)
            {
                GoiThau.IIdNhaThauId = Guid.Parse(SelectedNhaThau.ValueItem);
            }
            if (SelectedLoaiGoiThau != null)
            {
                GoiThau.LoaiGoiThau = SelectedLoaiGoiThau.ValueItem;
            }
            if (SelectedPTDauThau != null)
            {
                GoiThau.SPhuongThucDauThau = SelectedPTDauThau.ValueItem;
            }
            if (SelectedHTChonNhaThau != null)
            {
                GoiThau.SHinhThucChonNhaThau = SelectedHTChonNhaThau.ValueItem;
            }
            if (SelectedHTHopDong != null)
            {
                GoiThau.SHinhThucHopDong = SelectedHTHopDong.ValueItem;
            }

            if (!ValiDateData())
            {
                return;
            }
            VdtDaGoiThau entity = new VdtDaGoiThau();
            if (GoiThau.Id != Guid.Empty)
            {
                // Update
                entity = _vdtDaGoiThauService.FindById(GoiThau.Id);
                _mapper.Map(GoiThau, entity);

                entity.DDateUpdate = DateTime.Now;
                entity.SUserUpdate = _sessionService.Current.Principal;
                _vdtDaGoiThauService.Update(entity);
            }
            else
            {
                // Add VdtDaDuToan
                entity = _mapper.Map<VdtDaGoiThau>(GoiThau);
                entity.BActive = true;
                entity.BIsGoc = true;
                entity.DDateCreate = DateTime.Now;
                entity.SUserCreate = _sessionService.Current.Principal;
                _vdtDaGoiThauService.Add(entity);
                //them thong tin goiThauGocId
                entity.IIdGoiThauGocId = entity.Id;
                _vdtDaGoiThauService.Update(entity);
            }

            GoiThau.Id = entity.Id;
            System.Windows.Forms.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            SavedAction?.Invoke(_mapper.Map<VTS.QLNS.CTC.App.Model.VdtDaGoiThauModel>(entity));
            LoadData();
        }

        private bool ValiDateData()
        {
            if ((!GoiThau.NgayQuyetDinh.HasValue))
            {
                System.Windows.Forms.MessageBox.Show(Resources.AlertNgayQuyetDinhEmpty, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(GoiThau.SoQuyetDinh))
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckSoQD, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if(SelectedNhaThau == null)
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgNhaThauDaiDien, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }    

            return true;
        }

        private void ShowDetail()
        {
            OnShowDetailGoiThau(GoiThau);
        }

        public void OnShowDetailGoiThau(VdtDaGoiThauModel goiThauDetail)
        {
            if (goiThauDetail == null)
                return;
            GoiThauDetailViewModel.Model = goiThauDetail;
            GoiThauDetailViewModel.Init();
            view = new VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.GoiThau.GoiThauDetail
            {
                DataContext = GoiThauDetailViewModel
            };
            view.ShowDialog();
        }
    }
}
