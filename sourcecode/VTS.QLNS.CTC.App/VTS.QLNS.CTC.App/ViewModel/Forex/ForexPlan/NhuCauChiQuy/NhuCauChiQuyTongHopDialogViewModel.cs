using AutoMapper;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexPlan.NhuCauChiQuy
{
    public class NhuCauChiQuyTongHopDialogViewModel : DialogViewModelBase<NhNhuCauChiQuyModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ILogger<NhuCauChiQuyTongHopDialogViewModel> _logger;
        private readonly INhNhuCauChiQuyChiTietService _nhNhuCauChiQuyChiTietService;
        private readonly INhNhuCauChiQuyService _nhNhuCauChiQuyService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;

        public override Type ContentType => typeof(View.Forex.ForexPlan.NhuCauChiQuy.ForexNhuCauChiQuyTongHopDialog);

        //public string QuyNam => string.Format("{0} - {1}", Model.IQuy, Model.INamKeHoach);
        public string QuyNam { get; set; }

        private List<NhNhuCauChiQuyModel> _nhNhuCauChiQuyModels;
        public List<NhNhuCauChiQuyModel> NhNhuCauChiQuyModels
        {
            get => _nhNhuCauChiQuyModels;
            set => SetProperty(ref _nhNhuCauChiQuyModels, value);
        }

        private ObservableCollection<DonViModel> _itemsDonVi;
        public ObservableCollection<DonViModel> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private DonViModel _selectedDonVi;
        public DonViModel SelectedDonVi
        {
            get => _selectedDonVi;
            set => SetProperty(ref _selectedDonVi, value);
        }

        private ObservableCollection<NguonNganSachModel> _itemsNguonNganSach;
        public ObservableCollection<NguonNganSachModel> ItemsNguonNganSach
        {
            get => _itemsNguonNganSach;
            set => SetProperty(ref _itemsNguonNganSach, value);
        }

        private NguonNganSachModel _selectedNguonNganSach;
        public NguonNganSachModel SelectedNguonNganSach
        {
            get => _selectedNguonNganSach;
            set => SetProperty(ref _selectedNguonNganSach, value);
        }

        public NhuCauChiQuyTongHopDialogViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILogger<NhuCauChiQuyTongHopDialogViewModel> logger,
            INhNhuCauChiQuyChiTietService nhNhuCauChiQuyChiTietService,
            INhNhuCauChiQuyService nhNhuCauChiQuyService,
            INsDonViService nsDonViService,
            INsNguonNganSachService nsNguonNganSachService)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nhNhuCauChiQuyChiTietService = nhNhuCauChiQuyChiTietService;
            _nhNhuCauChiQuyService = nhNhuCauChiQuyService;
            _nsDonViService = nsDonViService;
            _nsNguonNganSachService = nsNguonNganSachService;
        }

        public override void Init()
        {
            base.Init();
            LoadDonVi();
            LoadNguonNganSach();
            LoadData();
        }

        private void LoadDonVi()
        {
            int year = _sessionService.Current.YearOfWork;
            var data = _nsDonViService.FindByCondition(x => x.NamLamViec == year);
            _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            OnPropertyChanged(nameof(ItemsDonVi));
        }

        private void LoadNguonNganSach()
        {
            var data = _nsNguonNganSachService.FindAll();
            _itemsNguonNganSach = _mapper.Map<ObservableCollection<NguonNganSachModel>>(data);
            OnPropertyChanged(nameof(ItemsNguonNganSach));
        }

        public override void LoadData(params object[] args)
        {
            QuyNam = string.Format("{0} - {1}", Model.IQuy, Model.INamKeHoach);
            if (Model.Id.IsNullOrEmpty())
            {
                Title = "NHU CẦU CHI QUÝ";
                Description = "Thêm mới chứng từ tổng hơp";
            }
            else
            {
                Title = "NHU CẦU CHI QUÝ";
                Description = "Cập nhật chứng từ tổng hợp";

                SelectedDonVi = ItemsDonVi.FirstOrDefault(x => x.IIDMaDonVi.Equals(Model.IIdMaDonVi));

                SelectedNguonNganSach = ItemsNguonNganSach.FirstOrDefault(x => x.IIdMaNguonNganSach == Model.IIdNguonVonId);
            }
        }

        public override void OnSave(object obj)
        {
            if (Model.Id != null && Model.Id != Guid.Empty)
            {
                if (SelectedDonVi != null)
                {
                    Model.IIdDonViId = SelectedDonVi.Id;
                    Model.IIdMaDonVi = SelectedDonVi.IIDMaDonVi;
                }
                if (SelectedNguonNganSach != null)
                {
                    Model.IIdNguonVonId = SelectedNguonNganSach.IIdMaNguonNganSach;
                }
                var entity = _mapper.Map<NhNhuCauChiQuy>(Model);
                _nhNhuCauChiQuyService.Update(entity);
            }
            else
            {
                List<NhNhuCauChiQuyChiTiet> listChiTiet = new List<NhNhuCauChiQuyChiTiet>();
                Model.Id = Guid.NewGuid();
                foreach (var item in NhNhuCauChiQuyModels)
                {
                    var data = _nhNhuCauChiQuyChiTietService.FindByIdChiQuy(item.Id).ToList();
                    listChiTiet.AddRange(data);
                }

                if (SelectedDonVi != null)
                {
                    Model.IIdDonViId = SelectedDonVi.Id;
                    Model.IIdMaDonVi = SelectedDonVi.IIDMaDonVi;
                }
                if (SelectedNguonNganSach != null)
                {
                    Model.IIdNguonVonId = SelectedNguonNganSach.IIdMaNguonNganSach;
                }
                listChiTiet.Select(x =>
                {
                    x.Id = Guid.NewGuid();
                    x.IIdNhuCauChiQuyId = Model.Id;
                    return x;
                }).ToList();

                //KhaiPD update để newGuid cho không bị lỗi validate vì màn này thêm mới dùng chung NhNhuCauChiQuyModel
                Model.IIdKHTongTheID = Guid.NewGuid();
                Model.IIdDonViId = Guid.NewGuid();
                Model.IIdNguonVonId = 0;
                if (!ValidateViewModelHelper.Validate(Model)) return;

                var entity = _mapper.Map<NhNhuCauChiQuy>(Model);
                entity.STongHop = string.Join(",", NhNhuCauChiQuyModels.Select(x => x.Id));
                entity.BIsKhoa = false;
                entity.BIsActive = true;
                entity.DNgayTao = DateTime.Now;
                entity.SNguoiTao = _sessionService.Current.Principal;
                _nhNhuCauChiQuyService.Add(entity);
                NhNhuCauChiQuyModels.Select(x =>
                {
                    x.IIdParentId = Model.Id;
                    return x;
                }).ToList();
                _nhNhuCauChiQuyService.UpDateRange(_mapper.Map<List<NhNhuCauChiQuy>>(NhNhuCauChiQuyModels));
                _nhNhuCauChiQuyChiTietService.AddRange(listChiTiet);
                
            }
            SavedAction?.Invoke(Model);
            DialogResult dialog = System.Windows.Forms.MessageBox.Show(Resources.MsgSaveDone);
            if (dialog == DialogResult.OK)
            {
                var view = obj as NhuCauChiQuyTongHopDialogViewModel;
                DialogHost.Close(view);
            }
        }
        public override void OnClose(object obj)
        {
            if (obj is Window window)
            {
                window.Close();
            }
        }
    }
}
