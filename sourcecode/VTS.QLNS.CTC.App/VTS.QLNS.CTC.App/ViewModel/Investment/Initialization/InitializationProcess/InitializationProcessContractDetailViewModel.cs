using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.Initialization.InitializationProcess
{
    public class InitializationProcessContractDetailViewModel : DetailViewModelBase<InitializationProcessDetailModel, VdtKtKhoiTaoDuLieuChiTietThanhToanModel>
    {
        private readonly IVdtDmNhaThauService _nhathauService;
        private readonly IVdtDaTtHopDongService _hopdongService;
        private readonly IVdtDmChiPhiService _vdtDmChiPhiService;
        private ISessionService _sessionService;
        private IMapper _mapper;
        private readonly ILog _logger;
        private List<VdtKtKhoiTaoDuLieuChiTietThanhToanModel> _itemsDefault;
        private List<VdtDaTtHopDong> ItemsHopDong = new List<VdtDaTtHopDong>();

        private bool _bIsDetail;
        public bool BIsDetail
        {
            get => _bIsDetail;
            set => SetProperty(ref _bIsDetail, value);
        }

        public bool BDisableDetail => !BIsDetail;

        public event InitializationProcessContractDetailViewModel.DataChangedEventHandler ClosePopup;

        public List<VdtKtKhoiTaoDuLieuChiTietThanhToanModel> ItemsDefault
        {
            get => _itemsDefault;
            set => SetProperty(ref _itemsDefault, value);
        }

        public RelayCommand SaveDataCommand { get; }

        public RelayCommand CloseWindowCommand { get; }

        public InitializationProcessContractDetailViewModel(
          IVdtDaTtHopDongService hopdongService,
          IVdtDmNhaThauService nhathauService,
          IVdtDmChiPhiService vdtDmChiPhiService,
          ISessionService sessionService,
          IMapper mapper,
          ILog logger)
        {
            _nhathauService = nhathauService;
            _hopdongService = hopdongService;
            _vdtDmChiPhiService = vdtDmChiPhiService;
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;
            CloseWindowCommand = new RelayCommand(obj => this.OnCloseWindow());
            SaveDataCommand = new RelayCommand(obj => this.OnSaveData(obj));
        }

        public override void Init()
        {
            try
            {
                MarginRequirement = new Thickness(10.0);
                LoadItemsHopDong();
                LoadData();
            }
            catch (Exception ex)
            {
                this._logger.Error(ex.Message, ex);
            }
        }

        public override void LoadData(params object[] args)
        {
            var dicDataHopDong = ItemsDefault.Clone().Where(item => item.ILoai.Equals((int?)VdtKtLoaiChiTietThanhToan.Type.HOP_DONG)).ToDictionary(n => n.IIDHopDongId, n => n);
            var dicDataChiPhi = ItemsDefault.Clone().Where(item => item.ILoai.Equals((int?)VdtKtLoaiChiTietThanhToan.Type.CHI_PHI)).ToDictionary(n => n.IIDChiPhiId, n => n);
            foreach (var item in Items)
            {
                if (item.ILoai.Equals((int?)VdtKtLoaiChiTietThanhToan.Type.HOP_DONG) && dicDataHopDong.ContainsKey(item.IIDHopDongId))
                {
                    item.FLuyKeTtklhtTnKhvn = dicDataHopDong[item.IIDHopDongId].FLuyKeTtklhtTnKhvn;
                    item.FLuyKeTUChuaThuHoiTnKhvn = dicDataHopDong[item.IIDHopDongId].FLuyKeTUChuaThuHoiTnKhvn;
                    item.FLuyKeTtklhtNnKhvn = dicDataHopDong[item.IIDHopDongId].FLuyKeTtklhtNnKhvn;
                    item.FLuyKeTUChuaThuHoiNnKhvn = dicDataHopDong[item.IIDHopDongId].FLuyKeTUChuaThuHoiNnKhvn;
                    item.FLuyKeTtklhtTnKhvu = dicDataHopDong[item.IIDHopDongId].FLuyKeTtklhtTnKhvu;
                    item.FLuyKeTUChuaThuHoiTnKhvu = dicDataHopDong[item.IIDHopDongId].FLuyKeTUChuaThuHoiTnKhvu;
                    item.FLuyKeTtklhtNnKhvu = dicDataHopDong[item.IIDHopDongId].FLuyKeTtklhtNnKhvu;
                    item.FLuyKeTUChuaThuHoiNnKhvu = dicDataHopDong[item.IIDHopDongId].FLuyKeTUChuaThuHoiNnKhvu;
                }

                if (item.ILoai.Equals((int?)VdtKtLoaiChiTietThanhToan.Type.CHI_PHI) && dicDataChiPhi.ContainsKey(item.IIDChiPhiId))
                {
                    item.FLuyKeTtklhtTnKhvn = dicDataChiPhi[item.IIDChiPhiId].FLuyKeTtklhtTnKhvn;
                    item.FLuyKeTUChuaThuHoiTnKhvn = dicDataChiPhi[item.IIDChiPhiId].FLuyKeTUChuaThuHoiTnKhvn;
                    item.FLuyKeTtklhtNnKhvn = dicDataChiPhi[item.IIDChiPhiId].FLuyKeTtklhtNnKhvn;
                    item.FLuyKeTUChuaThuHoiNnKhvn = dicDataChiPhi[item.IIDChiPhiId].FLuyKeTUChuaThuHoiNnKhvn;
                    item.FLuyKeTtklhtTnKhvu = dicDataChiPhi[item.IIDChiPhiId].FLuyKeTtklhtTnKhvu;
                    item.FLuyKeTUChuaThuHoiTnKhvu = dicDataChiPhi[item.IIDChiPhiId].FLuyKeTUChuaThuHoiTnKhvu;
                    item.FLuyKeTtklhtNnKhvu = dicDataChiPhi[item.IIDChiPhiId].FLuyKeTtklhtNnKhvu;
                    item.FLuyKeTUChuaThuHoiNnKhvu = dicDataChiPhi[item.IIDChiPhiId].FLuyKeTUChuaThuHoiNnKhvu;
                }
            }
            OnPropertyChanged("Items");
        }

        protected override void OnRefresh()
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
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

        private void OnSaveData(object obj)
        {
            SavedAction?.Invoke(null);
            System.Windows.Window window = obj as System.Windows.Window;
            window.Close();
        }

        private void LoadItemsHopDong()
        {
            List<VdtDaTtHopDong> byListDuAnId = this._hopdongService.FindByListDuAnId(new List<Guid>() { Model.IID_DuAnID });
            ItemsHopDong = byListDuAnId == null ? new List<VdtDaTtHopDong>() : byListDuAnId.OrderBy(n => n.SSoHopDong).ToList();
            List<VdtKtKhoiTaoDuLieuChiTietThanhToanModel> datas = new List<VdtKtKhoiTaoDuLieuChiTietThanhToanModel>();
            foreach (var item in ItemsHopDong)
            {
                VdtDmNhaThau vdtDmNhaThau = this._nhathauService.Find(item.IIdNhaThauThucHienId.Value);

                datas.Add(new VdtKtKhoiTaoDuLieuChiTietThanhToanModel()
                {
                    IIDHopDongId = item.Id,
                    SSoHopDong = item.SSoHopDong,
                    STenHopDong = item.STenHopDong,
                    STenNhaThau = vdtDmNhaThau != null ? vdtDmNhaThau.STenNhaThau : string.Empty,
                    FTienHopDong = item.FTienHopDong,
                    ILoai = (int?)VdtKtLoaiChiTietThanhToan.Type.HOP_DONG

                });
            }

            //Load chi Phi
            List<VdtDmChiPhiModel> dmChiPhi = _mapper.Map<List<VdtDmChiPhiModel>>(_vdtDmChiPhiService.FindAll().ToList());
            foreach (var item in dmChiPhi)
            {
                datas.Add(new VdtKtKhoiTaoDuLieuChiTietThanhToanModel()
                {
                    IIDChiPhiId = item.IIdChiPhi,
                    STenHopDong = item.STenChiPhi,
                    ILoai = (int?)VdtKtLoaiChiTietThanhToan.Type.CHI_PHI

                });
            }

            Items = new ObservableCollection<VdtKtKhoiTaoDuLieuChiTietThanhToanModel>(datas);
        }

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
    }
}
