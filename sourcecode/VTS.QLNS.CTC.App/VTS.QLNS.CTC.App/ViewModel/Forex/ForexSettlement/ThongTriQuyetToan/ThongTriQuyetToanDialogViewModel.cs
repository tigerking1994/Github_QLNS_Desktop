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
using VTS.QLNS.CTC.App.View.Forex.ForexSettlement.DeNghiQTDAHT;
using VTS.QLNS.CTC.App.View.Forex.ForexSettlement.ThongTriQuyetToan;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Query.Shared;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.ThongTriQuyetToan
{
    public class ThongTriQuyetToanDialogViewModel : DialogAttachmentViewModelBase<NhQtThongTriQuyetToanModel>
    {
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly ILog _logger;
        private readonly INhQtThongTriQuyetToanService _nhQtThongTriQuyetToanService;
        private readonly INhQtThongTriQuyetToanChiTietService _nhQtThongTriQuyetToanChiTietService;
        private readonly INhThTongHopService _nhThTongHopService;

        private SessionInfo _sessionInfo;

        public RelayCommand CloseDialogCommand;

        public ThongTriQuyetToanDialogViewModel(
            IMapper mapper,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            ILog logger,
            INhQtThongTriQuyetToanService nhQtThongTriQuyetToanService,
            INhQtThongTriQuyetToanChiTietService nhQtThongTriQuyetToanChiTietService,
            INhThTongHopService nhThTongHopService) : base(mapper, storageServiceFactory, attachService)
        {
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _logger = logger;
            _nhQtThongTriQuyetToanService = nhQtThongTriQuyetToanService;
            _nhQtThongTriQuyetToanChiTietService = nhQtThongTriQuyetToanChiTietService;
            _nhThTongHopService = nhThTongHopService;
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
            set
            {
                SetProperty(ref _selectedDonVi, value);
                if (value == null)
                {
                    LoadNhiemVuChiByDonVi(Guid.Empty);
                }
                else
                {
                    LoadNhiemVuChiByDonVi(value.Id);
                }
                LoadThongTriChiTiet();
            }
        }

        private ObservableCollection<LookupQuery<int, string>> _itemsLoaiThongTri;
        public ObservableCollection<LookupQuery<int, string>> ItemsLoaiThongTri
        {
            get => _itemsLoaiThongTri;
            set => SetProperty(ref _itemsLoaiThongTri, value);
        }

        private LookupQuery<int, string> _selectedLoaiThongTri;
        public LookupQuery<int, string> SelectedLoaiThongTri
        {
            get => _selectedLoaiThongTri;
            set
            {
                SetProperty(ref _selectedLoaiThongTri, value);
            }
        }

        private ObservableCollection<LookupQuery<int, string>> _itemsLoaiNoiDungChi;
        public ObservableCollection<LookupQuery<int, string>> ItemsLoaiNoiDungChi
        {
            get => _itemsLoaiNoiDungChi;
            set => SetProperty(ref _itemsLoaiNoiDungChi, value);
        }

        private ObservableCollection<LookupQuery<Guid, string>> _itemsNhiemVuChi;
        public ObservableCollection<LookupQuery<Guid, string>> ItemsNhiemVuChi
        {
            get => _itemsNhiemVuChi;
            set => SetProperty(ref _itemsNhiemVuChi, value);
        }

        private LookupQuery<Guid, string> _selectedNhiemVuChi;
        public LookupQuery<Guid, string> SelectedNhiemVuChi
        {
            get => _selectedNhiemVuChi;
            set
            {
                SetProperty(ref _selectedNhiemVuChi, value);
                LoadThongTriChiTiet();
            }
        }

        private NhQtThongTriQuyetToanChiTietModel _nhiemVuChiTotal;
        public NhQtThongTriQuyetToanChiTietModel NhiemVuChiTotal
        {
            get => _nhiemVuChiTotal;
            set
            {
                SetProperty(ref _nhiemVuChiTotal, value);
            }
        }


        private ObservableCollection<LookupQuery<int, string>> _itemsNamThongTri;
        public ObservableCollection<LookupQuery<int, string>> ItemsNamThongTri
        {
            get => _itemsNamThongTri;
            set => SetProperty(ref _itemsNamThongTri, value);
        }

        private LookupQuery<int, string> _selectedNamThongTri;
        public LookupQuery<int, string> SelectedNamThongTri
        {
            get => _selectedNamThongTri;
            set
            {
                SetProperty(ref _selectedNamThongTri, value);
                LoadThongTriChiTiet();
            }
        }

        private ObservableCollection<NhQtThongTriQuyetToanChiTietModel> _listThongTriChiTiet;
        public ObservableCollection<NhQtThongTriQuyetToanChiTietModel> ListThongTriChiTiet
        {
            get => _listThongTriChiTiet;
            set => SetProperty(ref _listThongTriChiTiet, value);
        }

        public override Type ContentType => typeof(ThongTriQuyetToanDialog);
        public override string Title => "Thông tri quyết toán";
        public override string Name => "Thông tri quyết toán";
        public bool IsDetail { get; set; } = false;
        public bool IsEdit { get; set; } = false;

        public override void Init()
        {
            base.Init();
            LoadDefault();
            LoadDonVi();
            LoadLoaiThongTri();
            LoadLoaiNoiDungChi();
            LoadNamThongTri();
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                if (IsDetail || IsEdit)
                {
                    SelectedDonVi = ItemsDonVi.FirstOrDefault(x => x.Id == Model.iID_DonViID);
                    _selectedNamThongTri = ItemsNamThongTri.FirstOrDefault(x => x.Id == Model.iNamThongTri);
                    _selectedNhiemVuChi = ItemsNhiemVuChi.FirstOrDefault(x => x.Id == Model.iID_KHTT_NhiemVuChiID);
                    _selectedLoaiThongTri = ItemsLoaiThongTri.FirstOrDefault(x => x.Id == Model.iLoaiThongTri);
                    LoadThongTriChiTiet();
                    //var data = _mapper.Map<ObservableCollection<NhQtThongTriQuyetToanChiTietModel>>(_nhQtThongTriQuyetToanChiTietService.GetThongTriChiTietByTTQTId(Model.Id));
                    //ListThongTriChiTiet = data;
                    //ListThongTriChiTiet.Add(new NhQtThongTriQuyetToanChiTietModel
                    //{
                    //    sTenNoiDungChi = "Tổng cộng:",
                    //    fDeNghiQuyetToanNam_USD = Model.iLoaiThongTri == 2 ? 0 : Model.fThongTri_USD,
                    //    fDeNghiQuyetToanNam_VND = Model.iLoaiThongTri == 2 ? 0 : Model.fThongTri_VND,
                    //    fThuaNopTraNSNN_USD = Model.iLoaiThongTri == 2 ? Model.fThongTri_USD : 0,
                    //    fThuaNopTraNSNN_VND = Model.iLoaiThongTri == 2 ? Model.fThongTri_VND : 0
                    //});
                }
                
                if (IsDetail)
                {
                    Description = "Chi tiết thông tri quyết toán";
                }
                if (IsEdit)
                {
                    Description = "Chỉnh sửa thông tri quyết toán";
                }
                else if (!IsDetail && !IsEdit) 
                {
                    Description = "Thêm mới thông tri quyết toán";
                    SelectedLoaiThongTri = ItemsLoaiThongTri.FirstOrDefault(x => x.Id == 1);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                MessageBoxHelper.Error(ex.Message);
            }
        }

        public override void OnSave(object obj)
        {
            if (!Validate())
            {
                return;
            }

            Model.iID_KHTT_NhiemVuChiID = SelectedNhiemVuChi.Id;
            Model.iID_DonViID = SelectedDonVi.Id;
            Model.iNamThongTri = SelectedNamThongTri.Id;

            if (!ValidateViewModelHelper.Validate(Model))
            {
                return;
            }

            var idTTQT = Guid.Empty;
            NhQtThongTriQuyetToan oldData = new NhQtThongTriQuyetToan();
            try
            {
                if (IsEdit && Model.Id != Guid.Empty)
                {
                    var ttqt = _nhQtThongTriQuyetToanService.GetThongTriUpdateById(Model.Id);
                    oldData = ttqt;
                    ttqt.sSoThongTri = Model.sSoThongTri;
                    ttqt.dNgayLap = Model.dNgayLap;
                    ttqt.iID_KHTT_NhiemVuChiID = SelectedNhiemVuChi.Id;
                    ttqt.iID_DonViID = SelectedDonVi.Id;
                    ttqt.iID_MaDonVi = SelectedDonVi.IIDMaDonVi;
                    ttqt.iNamThongTri = SelectedNamThongTri.Id;
                    ttqt.iLoaiThongTri = SelectedLoaiThongTri.Id;
                    ttqt.iLoaiNoiDungChi = Model.iLoaiNoiDungChi;
                    ttqt.fThongTri_VND = SelectedLoaiThongTri.Id == 2 ? NhiemVuChiTotal.fThuaNopTraNSNN_VND : NhiemVuChiTotal.fDeNghiQuyetToanNam_VND;
                    ttqt.fThongTri_USD = SelectedLoaiThongTri.Id == 2 ? NhiemVuChiTotal.fThuaNopTraNSNN_USD : NhiemVuChiTotal.fDeNghiQuyetToanNam_USD;
                    _nhQtThongTriQuyetToanService.UpdateThongTriQuyetToan(ttqt);

                    //_nhQtThongTriQuyetToanChiTietService.DeleteAllThongTriChiTietByTTId(ttqt.Id);
                    //var lstThongTriChiTiet = new List<NhQtThongTriQuyetToanChiTiet>();
                    //foreach (var item in ListThongTriChiTiet)
                    //{
                    //    var ttct = new NhQtThongTriQuyetToanChiTiet();
                    //    ttct.iID_ThongTriQuyetToanID = idTTQT;
                    //    ttct.iID_DuAnID = item.iID_DuAnID;
                    //    ttct.iID_HopDongID = item.iID_HopDongID;
                    //    ttct.iID_ThanhToan_ChiTietID = item.iID_ThanhToan_ChiTietID;
                    //    ttct.fDeNghiQuyetToanNam_USD = item.fDeNghiQuyetToanNam_USD;
                    //    ttct.fDeNghiQuyetToanNam_VND = item.fDeNghiQuyetToanNam_VND;
                    //    ttct.fThuaNopTraNSNN_VND = item.fThuaNopTraNSNN_VND;
                    //    ttct.fThuaNopTraNSNN_USD = item.fThuaNopTraNSNN_USD;
                    //    ttct.sMaThuTu = item.sMaThuTu;
                    //    ttct.sTenNoiDungChi = item.sTenNoiDungChi;

                    //    lstThongTriChiTiet.Add(ttct);
                    //}
                    //_nhQtThongTriQuyetToanChiTietService.SaveThongTriChiTiet(lstThongTriChiTiet);
                }
                else
                {
                    var ttqt = new NhQtThongTriQuyetToan();
                    ttqt.sSoThongTri = Model.sSoThongTri;
                    ttqt.dNgayLap = Model.dNgayLap;
                    ttqt.iID_KHTT_NhiemVuChiID = SelectedNhiemVuChi.Id;
                    ttqt.iID_DonViID = SelectedDonVi.Id;
                    ttqt.iID_MaDonVi = SelectedDonVi.IIDMaDonVi;
                    ttqt.iNamThongTri = SelectedNamThongTri.Id;
                    ttqt.iLoaiThongTri = SelectedLoaiThongTri.Id;
                    ttqt.iLoaiNoiDungChi = Model.iLoaiNoiDungChi;
                    ttqt.fThongTri_VND = Model.iLoaiThongTri == 2 ? NhiemVuChiTotal.fThuaNopTraNSNN_VND : NhiemVuChiTotal.fDeNghiQuyetToanNam_VND;
                    ttqt.fThongTri_USD = Model.iLoaiThongTri == 2 ? NhiemVuChiTotal.fThuaNopTraNSNN_USD : NhiemVuChiTotal.fDeNghiQuyetToanNam_USD;
                    idTTQT = _nhQtThongTriQuyetToanService.SaveAndGetIdThongTriQuyetToan(ttqt);

                    if (idTTQT == Guid.Empty)
                    {
                        MessageBoxHelper.Info(Resources.MsgSaveError);
                        return;
                    }

                    var lstThongTriChiTiet = new List<NhQtThongTriQuyetToanChiTiet>();
                    var ListThongTriChiTietSave = ListThongTriChiTiet.Where(x => x.bIsData == true).ToList();
                    foreach (var item in ListThongTriChiTietSave)
                    {
                        var ttct = new NhQtThongTriQuyetToanChiTiet();
                        ttct.iID_ThongTriQuyetToanID = idTTQT;
                        ttct.iID_DuAnID = item.iID_DuAnID;
                        ttct.iID_HopDongID = item.iID_HopDongID;
                        ttct.iID_ThanhToan_ChiTietID = item.iID_ThanhToan_ChiTietID;
                        ttct.fDeNghiQuyetToanNam_USD = item.fDeNghiQuyetToanNam_USD;
                        ttct.fDeNghiQuyetToanNam_VND = item.fDeNghiQuyetToanNam_VND;
                        ttct.fThuaNopTraNSNN_VND = item.fThuaNopTraNSNN_VND;
                        ttct.fThuaNopTraNSNN_USD = item.fThuaNopTraNSNN_USD;
                        ttct.sMaThuTu = item.sMaThuTu;
                        ttct.sTenNoiDungChi = item.sTenNoiDungChi;

                        lstThongTriChiTiet.Add(ttct);
                    }
                    _nhQtThongTriQuyetToanChiTietService.SaveThongTriChiTiet(lstThongTriChiTiet);
                }

                MessageBoxHelper.Info(Resources.MsgSaveDone);
                SavedAction?.Invoke(Model);
                var view = obj as Window;
                view.Close();
            }
            catch (Exception ex)
            {
                if (IsEdit)
                {
                    if (oldData.Id != Guid.Empty)
                    {
                        _nhQtThongTriQuyetToanService.UpdateThongTriQuyetToan(oldData);
                    }
                }
                else
                {
                    if (idTTQT != Guid.Empty)
                    {
                        _nhQtThongTriQuyetToanService.DeleteThongTriById(idTTQT);
                    }
                }
                _logger.Error(ex.Message, ex);
                MessageBoxHelper.Error(Resources.MsgSaveError + "\n" + ex.Message);
            }
        }

        private void LoadDefault()
        {
            _sessionInfo = _sessionService.Current;
            Model = new NhQtThongTriQuyetToanModel();
        }

        private void LoadDonVi()
        {
            try
            {
                var data = _nsDonViService.FindInternalByNamLamViec(_sessionService.Current.YearOfWork);
                ItemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadLoaiThongTri()
        {
            ItemsLoaiThongTri = new ObservableCollection<LookupQuery<int, string>>();
            ItemsLoaiThongTri.Add(new LookupQuery<int, string>
            {
                Id = 1,
                DisplayName = "Thông tri quyết toán"
            });
            ItemsLoaiThongTri.Add(new LookupQuery<int, string>
            {
                Id = 2,
                DisplayName = "Thông tri giảm quyết toán"
            });
        }

        private void LoadLoaiNoiDungChi()
        {
            ItemsLoaiNoiDungChi = new ObservableCollection<LookupQuery<int, string>>();
            ItemsLoaiNoiDungChi.Add(new LookupQuery<int, string>
            {
                Id = 1,
                DisplayName = "Chi bằng ngoại tệ"
            });
            ItemsLoaiNoiDungChi.Add(new LookupQuery<int, string>
            {
                Id = 2,
                DisplayName = "Chi bằng nội tệ"
            });
        }

        private void LoadNhiemVuChiByDonVi(Guid iID_DonViID)
        {
            try
            {
                if (iID_DonViID == Guid.Empty || iID_DonViID == null)
                {
                    ItemsNhiemVuChi = new ObservableCollection<LookupQuery<Guid, string>>();
                } 
                else
                {
                    var data = _nhQtThongTriQuyetToanService.GetLookupNhiemVuChiByDonVi(iID_DonViID);
                    ItemsNhiemVuChi = _mapper.Map<ObservableCollection<LookupQuery<Guid, string>>>(data);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadNamThongTri()
        {
            ItemsNamThongTri = new ObservableCollection<LookupQuery<int, string>>();
            for (int i = DateTime.Now.Year; i >= DateTime.Now.Year - 10; i--)
            {
                ItemsNamThongTri.Add(new LookupQuery<int, string> { 
                    DisplayName = i.ToString(), 
                    Id = i
                });
            }
        }

        private void LoadThongTriChiTiet()
        {
            if (SelectedNamThongTri != null && SelectedNhiemVuChi != null && SelectedDonVi != null)
            {
                var data = _nhQtThongTriQuyetToanChiTietService.GetCreateThongTriChiTiet(Model.Id, SelectedNamThongTri.Id, SelectedDonVi.Id, SelectedNhiemVuChi.Id);
                NhiemVuChiTotal = _mapper.Map<NhQtThongTriQuyetToanChiTietModel>(data.FirstOrDefault(x => x.bIsNhiemVuChi));
                var dataTongHop = LoadDataTongHop();
                if (NhiemVuChiTotal == null)
                {
                    NhiemVuChiTotal = new NhQtThongTriQuyetToanChiTietModel();
                    NhiemVuChiTotal.fDeNghiQuyetToanNam_USD = 0;
                    NhiemVuChiTotal.fDeNghiQuyetToanNam_VND = 0;
                    NhiemVuChiTotal.fThuaNopTraNSNN_VND = 0;
                    NhiemVuChiTotal.fThuaNopTraNSNN_USD = 0;
                }
                ListThongTriChiTiet = _mapper.Map<ObservableCollection<NhQtThongTriQuyetToanChiTietModel>>(data.Where(x => !x.bIsNhiemVuChi));
                ListThongTriChiTiet.Add(new NhQtThongTriQuyetToanChiTietModel
                {
                    sTenNoiDungChi = "Tổng cộng:",
                    fDeNghiQuyetToanNam_USD = NhiemVuChiTotal.fDeNghiQuyetToanNam_USD,
                    fDeNghiQuyetToanNam_VND = NhiemVuChiTotal.fDeNghiQuyetToanNam_VND,
                    //fThuaNopTraNSNN_USD =  NhiemVuChiTotal.fThuaNopTraNSNN_USD,
                    //fThuaNopTraNSNN_VND =  NhiemVuChiTotal.fThuaNopTraNSNN_VND
                    fThuaNopTraNSNN_USD = dataTongHop.Sum(x => x.FGiaTriUsd),
                    fThuaNopTraNSNN_VND = dataTongHop.Sum(x => x.FGiaTriVnd)
                });
            }
            else
            {
                ListThongTriChiTiet = new ObservableCollection<NhQtThongTriQuyetToanChiTietModel>();
                NhiemVuChiTotal = new NhQtThongTriQuyetToanChiTietModel();
                NhiemVuChiTotal.fDeNghiQuyetToanNam_USD = 0;
                NhiemVuChiTotal.fDeNghiQuyetToanNam_VND = 0;
                NhiemVuChiTotal.fThuaNopTraNSNN_VND = 0;
                NhiemVuChiTotal.fThuaNopTraNSNN_USD = 0;
            }
        }

        public override void OnClose(object obj)
        {
            if (obj is Window window)
            {
                window.Close();
            }
        }

        private bool Validate()
        {
            List<string> lstError = new List<string>();
            if (SelectedNhiemVuChi == null)
            {
                lstError.Add(Resources.MsgCheckChuongTrinh);
            }
            if (SelectedDonVi == null)
            {
                lstError.Add(Resources.MsgCheckDonVi);
            }
            if (SelectedNamThongTri == null)
            {
                lstError.Add(Resources.MsgCheckNam);
            }
            if (lstError.Count != 0)
            {
                MessageBoxHelper.Error(string.Join("\n", lstError));
                return false;
            }
            return true;
        }

        //Ham tinh data tong hop
        private List<NHTHTongHop> LoadDataTongHop()
        {
            var lstSMaNguon = new List<string> { NHConstants.MA_TH_BCTH_NS_GIAIDOAN };
            var predicate = PredicateBuilder.True<NHTHTongHop>();
            predicate = predicate.And(x => x.SMaNguon == NhTongHopConstants.MA_000 && x.SMaDich == NhTongHopConstants.MA_311 && x.INamKeHoach == SelectedNamThongTri.Id);
            return _nhThTongHopService.FindByCondition(predicate).ToList();

        }
    }
}
