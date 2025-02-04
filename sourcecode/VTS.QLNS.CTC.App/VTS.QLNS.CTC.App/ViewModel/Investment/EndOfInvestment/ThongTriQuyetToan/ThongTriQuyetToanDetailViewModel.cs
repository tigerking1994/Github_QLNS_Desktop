using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.ThongTriQuyetToan
{
    public class ThongTriQuyetToanDetailViewModel : DetailViewModelBase<VdtThongTriModel, VdtThongTriQuyetToanModel>
    {
        #region Private
        private readonly IVdtThongTriService _thongtriService;
        private readonly ISessionService _sessionService;
        private IMapper _mapper;
        #endregion

        public override string Name => "Quản lý thông tri quyết toán chi tiết";
        public RelayCommand SaveDataCommand { get; }
        //Dictionary<string, VdtDmKieuThongTri> dicKieuThongTri { get; set; }

        private string _Description;
        public override string Description
        {
            get => _Description;
            set
            {
                SetProperty(ref _Description, value);
            }
        }

        private ObservableCollection<VdtThongTriQuyetToanModel> _items;
        public ObservableCollection<VdtThongTriQuyetToanModel> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        #region Tab View
        //private ThongTriTabIndex _tabIndex;
        //public ThongTriTabIndex TabIndex
        //{
        //    get => _tabIndex;
        //    set => SetProperty(ref _tabIndex, value);
        //}

        //private ObservableCollection<VdtThongTriChiTietModel> _qTKPNamTruoc;
        //public ObservableCollection<VdtThongTriChiTietModel> QTKPNamTruoc
        //{
        //    get => _qTKPNamTruoc;
        //    set => SetProperty(ref _qTKPNamTruoc, value);
        //}

        //private ObservableCollection<VdtThongTriChiTietModel> _qTKPNamNay;
        //public ObservableCollection<VdtThongTriChiTietModel> QTKPNamNay
        //{
        //    get => _qTKPNamNay;
        //    set => SetProperty(ref _qTKPNamNay, value);
        //}

        //private ObservableCollection<VdtThongTriChiTietModel> _thuKPChuyenNamSau;
        //public ObservableCollection<VdtThongTriChiTietModel> ThuKPChuyenNamSau
        //{
        //    get => _thuKPChuyenNamSau;
        //    set => SetProperty(ref _thuKPChuyenNamSau, value);
        //}

        //private ObservableCollection<VdtThongTriChiTietModel> _capThanhKhoan;
        //public ObservableCollection<VdtThongTriChiTietModel> CapThanhKhoan
        //{
        //    get => _capThanhKhoan;
        //    set => SetProperty(ref _capThanhKhoan, value);
        //}

        //private ObservableCollection<VdtThongTriChiTietModel> _thuThanhKhoan;
        //public ObservableCollection<VdtThongTriChiTietModel> ThuThanhKhoan
        //{
        //    get => _thuThanhKhoan;
        //    set => SetProperty(ref _thuThanhKhoan, value);
        //}

        //private ObservableCollection<VdtThongTriChiTietModel> _capKPChuyenSang;
        //public ObservableCollection<VdtThongTriChiTietModel> CapKPChuyenSang
        //{
        //    get => _capKPChuyenSang;
        //    set => SetProperty(ref _capKPChuyenSang, value);
        //}

        //private ObservableCollection<VdtThongTriChiTietModel> _thuNopNganSach;
        //public ObservableCollection<VdtThongTriChiTietModel> ThuNopNganSach
        //{
        //    get => _thuNopNganSach;
        //    set => SetProperty(ref _thuNopNganSach, value);
        //}
        #endregion

        public ThongTriQuyetToanDetailViewModel(IVdtThongTriService thongtriService,
            ISessionService sessionService,
            IMapper mapper)
        {
            _thongtriService = thongtriService;
            _sessionService = sessionService;
            _mapper = mapper;
            SaveDataCommand = new RelayCommand(obj => OnSaveData());
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(10);
            LoadData();
        }

        #region RelayCommand
        public override void LoadData(params object[] args)
        {
            Description = "Thông tri quyết toán chi tiết";
            List<VdtThongTriQuyetToanQuery> lstData = new List<VdtThongTriQuyetToanQuery>();
            lstData = _thongtriService.GetVdtThongTriQuyetToanById(Model.Id.Value);
            if (lstData == null || lstData.Count == 0)
            {
                lstData = _thongtriService.GetVdtThongTriQuyetToanChiTiet(Model.IIdBcQuyetToanNienDo.Value);
            }
            Items = new ObservableCollection<VdtThongTriQuyetToanModel>(_mapper.Map<List<VdtThongTriQuyetToanModel>>(lstData));
            OnPropertyChanged(nameof(Items));
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData(1);
        }

        public void OnSaveData()
        {
            _thongtriService.DeleteThongTriChiTietByParentId(Model.Id.Value);
            SaveDataDetail();
            LoadData();
        }
        #endregion

        #region Helper
        private void SaveDataDetail()
        {
            if (Items == null || Items.Count == 0) return;
            List<VdtThongTriChiTiet> lstData = Items.Select(n => new VdtThongTriChiTiet()
            {
                Id = Guid.NewGuid(),
                FSoTien = n.FSoTien,
                IIdDuAnId = n.IIdDuAnId,
                IIdLoaiCongTrinhId = n.IIdLoaiCongTrinhId,
                IIdMucId = n.IIdMucId,
                IIdNganhId = n.IIdNganhId,
                IIdThongTriId = Model.Id.Value,
                IIdTietMucId = n.IIdTietMucId,
                IIdTieuMucId = n.IIdTieuMucId,
                SSoThongTri = Model.sMaThongTri
            }).ToList();
            _thongtriService.InsertThongTriChiTiet(lstData);
        }
        #endregion
    }
}
