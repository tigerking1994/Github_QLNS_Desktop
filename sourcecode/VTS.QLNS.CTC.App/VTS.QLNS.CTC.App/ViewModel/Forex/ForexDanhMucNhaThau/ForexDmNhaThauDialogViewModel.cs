using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using AutoMapper;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Logging;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.QuanLyDuAn.NHHopDongTrongNuoc;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexDanhMucNhaThau
{
    public class ForexDmNhaThauDialogViewModel : DialogAttachmentViewModelBase<NhDmNhaThauModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INhDmNhaThauService _iNhDmNhaThauService;
        private readonly INhDmNhaThauNguoiNhanService _iNhDmNhaThauNguoiNhanService;
        private readonly INhDmNhaThauNganHangService _iNhDmNhaThauNganHangService;
        private readonly ILogger<NHHopDongTrongNuocDialogViewModel> _logger;

        public override Type ContentType => typeof(View.Forex.ForexDanhMucNhaThau.ForexDmNhaThauDialog);

        private ObservableCollection<ComboboxItem> _itemsLoai;
        public ObservableCollection<ComboboxItem> ItemsLoai
        {
            get => _itemsLoai;
            set => SetProperty(ref _itemsLoai, value);
        }

        private ComboboxItem _selectedLoai;
        public ComboboxItem SelectedLoai
        {
            get => _selectedLoai;
            set => SetProperty(ref _selectedLoai, value);
        }

        private ObservableCollection<NhDmNhaThauNguoiNhanModel> _itemsNhaThauNguoiNhan;
        public ObservableCollection<NhDmNhaThauNguoiNhanModel> ItemsNhaThauNguoiNhan
        {
            get => _itemsNhaThauNguoiNhan;
            set => SetProperty(ref _itemsNhaThauNguoiNhan, value);
        }

        private NhDmNhaThauNguoiNhanModel _selectedNhaThauNguoiNhan;
        public NhDmNhaThauNguoiNhanModel SelectedNhaThauNguoiNhan
        {
            get => _selectedNhaThauNguoiNhan;
            set => SetProperty(ref _selectedNhaThauNguoiNhan, value);
        }

        private ObservableCollection<NhDmNhaThauNganHangModel> _itemsNhaThauNganHang;
        public ObservableCollection<NhDmNhaThauNganHangModel> ItemsNhaThauNganHang
        {
            get => _itemsNhaThauNganHang;
            set => SetProperty(ref _itemsNhaThauNganHang, value);
        }

        private NhDmNhaThauNganHangModel _selectedNhaThauNganHang;
        public NhDmNhaThauNganHangModel SelectedNhaThauNganHang
        {
            get => _selectedNhaThauNganHang;
            set => SetProperty(ref _selectedNhaThauNganHang, value);
        }

        public RelayCommand AddNhaThauNguoiNhanCommand { get; }
        public RelayCommand DeleteNhaThauNguoiNhanCommand { get; }
        public RelayCommand AddNhaThauNganHangCommand { get; }
        public RelayCommand DeleteNhaThauNganHangCommand { get; }

        public bool IsDetail { get; set; }
        public ForexDmNhaThauDialogViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILogger<NHHopDongTrongNuocDialogViewModel> logger,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            INhDmNhaThauService iNhDmNhaThauService,
            INhDmNhaThauNguoiNhanService iNhDmNhaThauNguoiNhanService,
            INhDmNhaThauNganHangService iNhDmNhaThauNganHangService) : base(mapper, storageServiceFactory, attachService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;
            _iNhDmNhaThauService = iNhDmNhaThauService;
            _iNhDmNhaThauNguoiNhanService = iNhDmNhaThauNguoiNhanService;
            _iNhDmNhaThauNganHangService = iNhDmNhaThauNganHangService;

            AddNhaThauNguoiNhanCommand = new RelayCommand(obj => OnAddNhaThauNguoiNhan());
            DeleteNhaThauNguoiNhanCommand = new RelayCommand(obj => OnDeleteNhaThauNguoiNhan());
            AddNhaThauNganHangCommand = new RelayCommand(obj => OnAddNhaThauNganHang());
            DeleteNhaThauNganHangCommand = new RelayCommand(obj => OnDeleteNhaThauNganHang());
        }

        public override void Init()
        {
            base.Init();
            LoadLoai();
            LoadData();
        }
        public override void LoadData(params object[] args)
        {
            if (Model.Id.IsNullOrEmpty())
            {
                Title = "DANH MỤC NHÀ THẦU";
                Description = "Thêm mới nhà thầu, đơn vị ủy thác";
                ItemsNhaThauNguoiNhan = new ObservableCollection<NhDmNhaThauNguoiNhanModel>();
                ItemsNhaThauNganHang = new ObservableCollection<NhDmNhaThauNganHangModel>();
            }
            else
            {
                NhDmNhaThau entity = _iNhDmNhaThauService.FindById(Model.Id);
                Model = _mapper.Map<NhDmNhaThauModel>(entity);
                if (IsDetail)
                {
                    Title = "THÔNG TIN NHÀ THẦU";
                    Description = "Chi tiết thông tin nhà thầu";
                }
                else
                {
                    Title = "CẬP NHẬP THÔNG TIN NHÀ THẦU";
                    Description = "Cập nhật thông tin nhà thầu";
                }

                _selectedLoai = ItemsLoai.FirstOrDefault(x => x.ValueItem.Equals(Model.ILoai.ToString()));
                LoadNguoiNhanTienMat();
                LoadTaiKhoanNganHang();
            }
        }

        public void LoadNguoiNhanTienMat()
        {
            if (Model != null)
            {
                var results = _iNhDmNhaThauNguoiNhanService.FindByCondition(x => x.IIdNhaThauId.Equals(Model.Id));
                ItemsNhaThauNguoiNhan = new ObservableCollection<NhDmNhaThauNguoiNhanModel>(_mapper.Map<ObservableCollection<NhDmNhaThauNguoiNhanModel>>(results));
            }
        }

        public void LoadTaiKhoanNganHang()
        {
            if (Model != null)
            {
                var results = _iNhDmNhaThauNganHangService.FindByCondition(x => x.IIdNhaThauId.Equals(Model.Id));
                ItemsNhaThauNganHang = new ObservableCollection<NhDmNhaThauNganHangModel>(_mapper.Map<ObservableCollection<NhDmNhaThauNganHangModel>>(results));
            }
        }
        public override void OnSave()
        {
            try
            {
                if (SelectedLoai != null)
                {
                    Model.ILoai = int.Parse(SelectedLoai.ValueItem);
                }
                NhDmNhaThau entity;
                if (Model.Id == Guid.Empty)
                {
                    // Thêm mới
                    entity = new NhDmNhaThau();
                    _mapper.Map(Model, entity);

                    entity.Id = Guid.NewGuid();
                    _iNhDmNhaThauService.Add(entity);
                    Model.Id = entity.Id;
                    SaveNguoiNhanTienMat();
                    SaveTaiKhoanNganHang();
                }
                else
                {
                    // Cập nhật
                    entity = _iNhDmNhaThauService.FindById(Model.Id);
                    _mapper.Map(Model, entity);
                    _iNhDmNhaThauService.Update(entity);
                    SaveNguoiNhanTienMat();
                    SaveTaiKhoanNganHang();
                }
                SavedAction?.Invoke(_mapper.Map<NhDmNhaThauModel>(entity));
                MessageBoxHelper.Info(Resources.MsgSaveDone);
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private void SaveNguoiNhanTienMat()
        {
            var lstInsert = ItemsNhaThauNguoiNhan.Where(x => !x.IsDeleted && x.Id == Guid.Empty).ToList();
            var lstUpdate = ItemsNhaThauNguoiNhan.Where(x => !x.IsDeleted && x.Id != Guid.Empty).ToList();
            var lstDelete = ItemsNhaThauNguoiNhan.Where(x => x.IsDeleted && x.Id != Guid.Empty).ToList();

            if (lstInsert.Count > 0)
            {
                AddNhaThauNguoiNhanSave(lstInsert);
            }
            if (lstUpdate.Count > 0)
            {
                UpdateNhaThauNguoiNhanSave(lstUpdate);
            }
            if (lstDelete.Count > 0)
            {
                DeleteNhaThauNguoiNhanSave(lstDelete);
            }
        }
        private void AddNhaThauNguoiNhanSave(List<NhDmNhaThauNguoiNhanModel> listAdd)
        {
            var lstNhaThauNguoiNhan = _mapper.Map<List<NhDmNhaThauNguoiNhan>>(listAdd);
            foreach (var item in lstNhaThauNguoiNhan)
            {
                item.Id = Guid.NewGuid();
                item.IIdNhaThauId = Model.Id;
                _iNhDmNhaThauNguoiNhanService.Add(item);
            }
        }

        private void UpdateNhaThauNguoiNhanSave(List<NhDmNhaThauNguoiNhanModel> listEdit)
        {
            foreach (var item in listEdit)
            {
                NhDmNhaThauNguoiNhan nhDmNhaThauNguoiNhan = _iNhDmNhaThauNguoiNhanService.FindById(item.Id);
                if (nhDmNhaThauNguoiNhan != null)
                {
                    _mapper.Map(item, nhDmNhaThauNguoiNhan);
                    _iNhDmNhaThauNguoiNhanService.Update(nhDmNhaThauNguoiNhan);
                }
            }
        }

        private void DeleteNhaThauNguoiNhanSave(List<NhDmNhaThauNguoiNhanModel> listDelete)
        {
            foreach (var item in listDelete)
            {
                _iNhDmNhaThauNguoiNhanService.Delete(item.Id);
            }
        }

        private void SaveTaiKhoanNganHang()
        {
            var lstInsert = ItemsNhaThauNganHang.Where(x => !x.IsDeleted && x.Id == Guid.Empty).ToList();
            var lstUpdate = ItemsNhaThauNganHang.Where(x => !x.IsDeleted && x.Id != Guid.Empty).ToList();
            var lstDelete = ItemsNhaThauNganHang.Where(x => x.IsDeleted && x.Id != Guid.Empty).ToList();

            if (lstInsert.Count > 0)
            {
                AddNhaThauNganHangSave(lstInsert);
            }
            if (lstUpdate.Count > 0)
            {
                UpdateNhaThauNganHangSave(lstUpdate);
            }
            if (lstDelete.Count > 0)
            {
                DeleteNhaThauNganHangSave(lstDelete);
            }
        }
        private void AddNhaThauNganHangSave(List<NhDmNhaThauNganHangModel> listAdd)
        {
            var lstNhaThauNganHang = _mapper.Map<List<NhDmNhaThauNganHang>>(listAdd);
            foreach (var item in lstNhaThauNganHang)
            {
                item.Id = Guid.NewGuid();
                item.IIdNhaThauId = Model.Id;
                _iNhDmNhaThauNganHangService.Add(item);
            }
        }

        private void UpdateNhaThauNganHangSave(List<NhDmNhaThauNganHangModel> listEdit)
        {
            foreach (var item in listEdit)
            {
                NhDmNhaThauNganHang nhDmNhaThauNganHang = _iNhDmNhaThauNganHangService.FindById(item.Id);
                if (nhDmNhaThauNganHang != null)
                {
                    _mapper.Map(item, nhDmNhaThauNganHang);
                    _iNhDmNhaThauNganHangService.Update(nhDmNhaThauNganHang);
                }
            }
        }

        private void DeleteNhaThauNganHangSave(List<NhDmNhaThauNganHangModel> listDelete)
        {
            foreach (var item in listDelete)
            {
                _iNhDmNhaThauNganHangService.Delete(item.Id);
            }
        }

        public void OnAddNhaThauNguoiNhan()
        {
            NhDmNhaThauNguoiNhanModel newItem = new NhDmNhaThauNguoiNhanModel();
            ItemsNhaThauNguoiNhan.Insert(ItemsNhaThauNguoiNhan.Count, newItem);
            OnPropertyChanged(nameof(ItemsNhaThauNguoiNhan));
        }

        private void OnDeleteNhaThauNguoiNhan()
        {
            if (SelectedNhaThauNguoiNhan != null)
            {
                SelectedNhaThauNguoiNhan.IsDeleted = !SelectedNhaThauNguoiNhan.IsDeleted;
            }
            OnPropertyChanged(nameof(ItemsNhaThauNguoiNhan));
        }

        public void OnAddNhaThauNganHang()
        {
            NhDmNhaThauNganHangModel newItem = new NhDmNhaThauNganHangModel();
            ItemsNhaThauNganHang.Insert(ItemsNhaThauNganHang.Count, newItem);
            OnPropertyChanged(nameof(ItemsNhaThauNganHang));
        }

        private void OnDeleteNhaThauNganHang()
        {
            if (SelectedNhaThauNganHang != null)
            {
                SelectedNhaThauNganHang.IsDeleted = !SelectedNhaThauNganHang.IsDeleted;
            }
            OnPropertyChanged(nameof(ItemsNhaThauNganHang));
        }

        public void LoadLoai()
        {
            var results = new ObservableCollection<ComboboxItem>();
            results.Add(new ComboboxItem("Nhà thầu", "1"));
            results.Add(new ComboboxItem("Đơn vị ủy thác", "2"));
            _itemsLoai = results;
        }

        public override void OnClose(object obj)
        {
            if (obj is System.Windows.Window window)
            {
                window.Close();
            }
        }
    }
}
