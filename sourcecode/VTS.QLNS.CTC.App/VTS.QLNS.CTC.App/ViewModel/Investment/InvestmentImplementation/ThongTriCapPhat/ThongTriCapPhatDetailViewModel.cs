
using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.InvestmentImplementation.ThongTriCapPhat.PrintDialog;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.ThongTriCapPhat.PrintDialog;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.ThongTriCapPhat
{
    public class ThongTriCapPhatDetailViewModel : DetailViewModelBase<VdtThongTriModel, VdtThongTriChiTietModel>
    {
        #region Private
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly IDmLoaiCongTrinhService _loaicongtrinhService;
        private readonly IVdtThongTriService _thongtriService;
        private readonly ISessionService _sessionService;
        private IMapper _mapper;
        #endregion

        public override string Name => "Quản lý thông tri cấp phát chi tiết";
        public RelayCommand SaveDataCommand { get; }
        public RelayCommand PrintThongTriCommand { get; }
        Dictionary<string, Guid> dicKieuThongTri { get; set; }
        public List<VdtThongTriModel> VdtThongTriModels { get; set; }

        public override string Description => "Thông tri cấp phát chi tiết";

        #region Tab View
        private ThongTriTabIndex _tabIndex;
        public ThongTriTabIndex TabIndex
        {
            get => _tabIndex;
            set => SetProperty(ref _tabIndex, value);
        }

        public bool BShowThanhToan => Model.ILoaiThongTri == (int)LoaiThongTriEnum.Type.CAP_THANH_TOAN;
        public bool BShowTamUng => Model.ILoaiThongTri == (int)LoaiThongTriEnum.Type.CAP_TAM_UNG;
        public bool BShowKinhPhi => Model.ILoaiThongTri == (int)LoaiThongTriEnum.Type.CAP_KINH_PHI;
        public bool BShowHopThuc => Model.ILoaiThongTri == (int)LoaiThongTriEnum.Type.CAP_HOP_THUC;

        private ObservableCollection<ComboboxItem> _itemsLoaiCongTrinh;
        public ObservableCollection<ComboboxItem> ItemsLoaiCongTrinh
        {
            get => _itemsLoaiCongTrinh;
            set => SetProperty(ref _itemsLoaiCongTrinh, value);
        }

        private ObservableCollection<VdtThongTriChiTietModel> _itemsThanhToan_KLHT;
        public ObservableCollection<VdtThongTriChiTietModel> ItemsThanhToan_KLHT
        {
            get => _itemsThanhToan_KLHT;
            set => SetProperty(ref _itemsThanhToan_KLHT, value);
        }

        private ObservableCollection<VdtThongTriChiTietModel> _itemsThanhToan_ThuHoiUng;
        public ObservableCollection<VdtThongTriChiTietModel> ItemsThanhToan_ThuHoiUng
        {
            get => _itemsThanhToan_ThuHoiUng;
            set => SetProperty(ref _itemsThanhToan_ThuHoiUng, value);
        }

        private ObservableCollection<VdtThongTriChiTietModel> _itemsCapTamUng;
        public ObservableCollection<VdtThongTriChiTietModel> ItemsCapTamUng
        {
            get => _itemsCapTamUng;
            set => SetProperty(ref _itemsCapTamUng, value);
        }

        private ObservableCollection<VdtThongTriChiTietModel> _itemsCapHopThuc;
        public ObservableCollection<VdtThongTriChiTietModel> ItemsCapHopThuc
        {
            get => _itemsCapHopThuc;
            set => SetProperty(ref _itemsCapHopThuc, value);
        }

        private ObservableCollection<VdtThongTriChiTietModel> _itemsCapKinhPhi;
        public ObservableCollection<VdtThongTriChiTietModel> ItemsCapKinhPhi
        {
            get => _itemsCapKinhPhi;
            set => SetProperty(ref _itemsCapKinhPhi, value);
        }

        private bool _isDetail;
        public bool IsDetail
        {
            get => _isDetail;
            set => SetProperty(ref _isDetail, value);
        }

        #endregion

        public ThongTriCapPhatDetailViewModel(IVdtThongTriService thongtriService,
            INsNguonNganSachService nsNguonNganSachService,
            IDmLoaiCongTrinhService loaicongtrinhService,
            ISessionService sessionService,
            IMapper mapper,
            ThongTriCapPhatPrintDialogViewModel thongTriCapPhatPrintDialogViewModel)
        {
            _thongtriService = thongtriService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _loaicongtrinhService = loaicongtrinhService;
            _sessionService = sessionService;
            ThongTriCapPhatPrintDialogViewModel = thongTriCapPhatPrintDialogViewModel;
            ThongTriCapPhatPrintDialogViewModel.ParentPage = this;
            _mapper = mapper;
            SaveDataCommand = new RelayCommand(obj => OnSaveData());
            PrintThongTriCommand = new RelayCommand(obj => OnShowPrintThongTriDialog());
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(10);
            SetDefaultData();
            LoadLoaiCongTrinh();
            GetKieuThongChi();
            LoadData();
        }

        private ObservableCollection<VdtThongTriModel> _itemsThongTri;
        private ThongTriCapPhatPrintDialogViewModel thongTriCapPhatPrintDialogViewModel;

        public ObservableCollection<VdtThongTriModel> ItemsThongTri
        {
            get => _itemsThongTri;
            set => SetProperty(ref _itemsThongTri, value);
        }

        public ThongTriCapPhatPrintDialogViewModel ThongTriCapPhatPrintDialogViewModel { get; set; }

        #region RelayCommand
        private void SetDefaultData()
        {
            if (BShowThanhToan)
            {
                TabIndex = ThongTriTabIndex.CAP_THANH_TOAN_KLHT;
                OnPropertyChanged(nameof(TabIndex));
            }
            ItemsThanhToan_KLHT = new ObservableCollection<VdtThongTriChiTietModel>();
            ItemsThanhToan_ThuHoiUng = new ObservableCollection<VdtThongTriChiTietModel>();
            ItemsCapTamUng = new ObservableCollection<VdtThongTriChiTietModel>();
            ItemsCapHopThuc = new ObservableCollection<VdtThongTriChiTietModel>();
            ItemsCapKinhPhi = new ObservableCollection<VdtThongTriChiTietModel>();
            OnPropertyChanged(nameof(ItemsThanhToan_KLHT));
            OnPropertyChanged(nameof(ItemsThanhToan_ThuHoiUng));
            OnPropertyChanged(nameof(ItemsCapTamUng));
            OnPropertyChanged(nameof(ItemsCapHopThuc));
            OnPropertyChanged(nameof(ItemsCapKinhPhi));
        }
        public override void LoadData(params object[] args)
        {
            var data = _thongtriService.GetVdtThongTriChiTiet((Model.Id ?? Guid.Empty), Model.iID_MaDonViID, Model.ILoaiThongTri, Model.iNamThongTri, Model.dNgayThongTri.Value,
                Model.sMaNguonVon, Model.dNgayLapGanNhat).ToList();
            var lstThongChiChiTiet = _thongtriService.GetVdtThongTriChiTietByParentId(Model.Id.Value);
            if (lstThongChiChiTiet != null && lstThongChiChiTiet.Count() != 0)
            {
                ConvertDataViewModel(_mapper.Map<List<VdtThongTriChiTietModel>>(lstThongChiChiTiet));
            }
            else
            {
                ConvertDataViewModel(_mapper.Map<List<VdtThongTriChiTietModel>>(data));
            }
            /*if (Model.ItemsChungTuThanhToan != null)
            {
                var lstDeNghiThanhToanId = Model.ItemsChungTuThanhToan.Select(n => n.Id).ToList();
                var lstThongTriChiTiet = _thongtriService.GetVdtThongTriChiTietByPheDuyet().Where(n => lstDeNghiThanhToanId.Contains(n.IIdDeNghiThanhToanId.Value));
                ConvertDataViewModel(_mapper.Map<List<VdtThongTriChiTietModel>>(lstThongTriChiTiet));
            } else
            {
                var lstThongTriChiTiet = _thongtriService.FindByIdThongTri(Model.Id.Value);
                ConvertDataViewModel(_mapper.Map<List<VdtThongTriChiTietModel>>(lstThongTriChiTiet));
            }*/
            OnPropertyChanged(nameof(ItemsThanhToan_KLHT));
            OnPropertyChanged(nameof(ItemsThanhToan_ThuHoiUng));
            OnPropertyChanged(nameof(ItemsCapTamUng));
            OnPropertyChanged(nameof(ItemsCapHopThuc));
            OnPropertyChanged(nameof(ItemsCapKinhPhi));
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData(1);
        }

        public void OnSaveData()
        {
            _thongtriService.DeleteThongTriChiTietByParentId(Model.Id.Value);
            List<VdtThongTriChiTietModel> lstData = new List<VdtThongTriChiTietModel>();
            if (ItemsThanhToan_KLHT != null)
                lstData.AddRange(ItemsThanhToan_KLHT);
            if (ItemsThanhToan_ThuHoiUng != null)
                lstData.AddRange(ItemsThanhToan_ThuHoiUng);
            if (ItemsCapTamUng != null)
                lstData.AddRange(ItemsCapTamUng);
            if (ItemsCapHopThuc != null)
                lstData.AddRange(ItemsCapHopThuc);
            if (ItemsCapKinhPhi != null)
                lstData.AddRange(ItemsCapKinhPhi);
            if (lstData != null && lstData.Count != 0)
            {
                SaveDataDetail(lstData);
            }
            //MessageBox.Show(Resources.MsgSaveDone);
            LoadData();
        }
        #endregion
        private void OnShowPrintThongTriDialog()
        {
            List<VdtThongTriModel> lstthongChiChiTiet = new List<VdtThongTriModel>();
            lstthongChiChiTiet.Add(Model);
            ThongTriCapPhatPrintDialogViewModel.VdtThongTriModels = lstthongChiChiTiet;
            ThongTriCapPhatPrintDialogViewModel.Init();
            object content = new ThongTriCapPhatPrintDialog
            {
                DataContext = ThongTriCapPhatPrintDialogViewModel
            };
            DialogHost.Show(content, DemandCheckScreen.ROOT_DIALOG, null, null);
        }
        #region Helper
        private void GetKieuThongChi()
        {
            var data = _thongtriService.GetAllKieuThongTri();
            dicKieuThongTri = data.ToDictionary(n => n.SMaKieuThongTri, n => n.Id);
        }

        private void ConvertDataViewModel(List<VdtThongTriChiTietModel> lstData)
        {
            if (Model.ILoaiThongTri == (int)LoaiThongTriEnum.Type.CAP_THANH_TOAN)
            {
                List<VdtThongTriChiTietModel> lstThanhToan = new List<VdtThongTriChiTietModel>();
                List<VdtThongTriChiTietModel> lstThuHoi = new List<VdtThongTriChiTietModel>();
                foreach (var item in lstData)
                {
                    if (item.SMaKieuThongTri == KieuThongTri.TT_KPQP
                        || item.SMaKieuThongTri == KieuThongTri.TT_Cap_KPK
                        || item.SMaKieuThongTri == KieuThongTri.TT_Cap_KPNN)

                        lstThanhToan.Add(item);
                    else if (item.SMaKieuThongTri == KieuThongTri.TT_ThuUng_KPQP
                        || item.SMaKieuThongTri == KieuThongTri.TT_ThuUng_KPNN
                        || item.SMaKieuThongTri == KieuThongTri.TT_ThuUng_KPK)
                        lstThuHoi.Add(item);
                    else
                        lstThanhToan.Add(item);
                }
                ItemsThanhToan_KLHT = _mapper.Map<ObservableCollection<VdtThongTriChiTietModel>>(lstThanhToan);
                ItemsThanhToan_ThuHoiUng = _mapper.Map<ObservableCollection<VdtThongTriChiTietModel>>(lstThuHoi);
            }
            else
            {
                switch (Model.ILoaiThongTri)
                {
                    case (int)LoaiThongTriEnum.Type.CAP_TAM_UNG:
                        ItemsCapTamUng = _mapper.Map<ObservableCollection<VdtThongTriChiTietModel>>(lstData);
                        break;
                    case (int)LoaiThongTriEnum.Type.CAP_KINH_PHI:
                        ItemsCapKinhPhi = _mapper.Map<ObservableCollection<VdtThongTriChiTietModel>>(lstData);
                        break;
                    case (int)LoaiThongTriEnum.Type.CAP_HOP_THUC:
                        ItemsCapHopThuc = _mapper.Map<ObservableCollection<VdtThongTriChiTietModel>>(lstData);
                        break;
                }
            }
        }

        private void SaveDataDetail(List<VdtThongTriChiTietModel> lstData)
        {
            List<VdtThongTriChiTiet> results = new List<VdtThongTriChiTiet>();
            foreach (var item in lstData)
            {
                results.Add(new VdtThongTriChiTiet()
                {
                    Id = Guid.NewGuid(),
                    IIdCapPheDuyetId = item.IIdCapPheDuyetId,
                    IIdDuAnId = item.IIdDuAnId,
                    IIdKieuThongTriId = (item.SMaKieuThongTri != null && dicKieuThongTri.ContainsKey(item.SMaKieuThongTri)) ? dicKieuThongTri[item.SMaKieuThongTri] : Guid.Empty,
                    IIdLoaiCongTrinhId = item.IIdLoaiCongTrinhId,
                    IIdMucId = item.IIdMucId,
                    IIdTieuMucId = item.IIdTieuMucId,
                    IIdTietMucId = item.IIdTietMucId,
                    IIdNganhId = item.IIdNganhId,
                    IIdNhaThauId = item.IIdNhaThauId,
                    IIdThongTriId = Model.Id ?? Guid.Empty,
                    SDonViThuHuong = item.SDonViThuHuong,
                    SSoThongTri = Model.sMaThongTri,
                    FSoTien = item.FSoTien
                });
            }
            _thongtriService.InsertThongTriChiTiet(results);
        }

        private void LoadLoaiCongTrinh()
        {
            ItemsLoaiCongTrinh = new ObservableCollection<ComboboxItem>();
            var lstLoaiCongTrinh = _loaicongtrinhService.GetAll();
            if (lstLoaiCongTrinh != null)
            {
                ItemsLoaiCongTrinh = new ObservableCollection<ComboboxItem>(lstLoaiCongTrinh.Select(n => new ComboboxItem()
                {
                    DisplayItem = n.STenLoaiCongTrinh,
                    ValueItem = n.IIdLoaiCongTrinh.ToString()
                }));
            }
            OnPropertyChanged(nameof(ItemsLoaiCongTrinh));
        }
        #endregion
    }
}
