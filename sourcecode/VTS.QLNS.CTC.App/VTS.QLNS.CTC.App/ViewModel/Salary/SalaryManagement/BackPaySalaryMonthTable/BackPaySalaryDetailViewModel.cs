using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Data;
using System.Windows.Documents;
using AutoMapper;
using log4net;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Component;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement.InsuranceSalaryMonthTable
{
    public class BackPaySalaryDetailViewModel : DetailViewModelBase<TlDSCapNhapBangLuongModel, TlBangLuongThangBHXHModel>
    {
        private readonly ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private readonly IMapper _mapper;
        private readonly ITlBangLuongThangService _tlBangLuongThangService;
        private readonly ITlBangLuongThangBHXHService _tlBangLuongThangBHXHService;
        private readonly ITlBangLuongThangTruyThuService _tlBangLuongThangTruyThuService;
        private ICollectionView _dtDanhSachBangLuongChiTietView;
        private DataTable _data;
        private readonly ILog _logger;
        public override string Title => "BẢNG LƯƠNG TRUY THU CHI TIẾT";

        private string _thoigian;
        public string ThoiGian
        {
            get => _thoigian;
            set => SetProperty(ref _thoigian, value);
        }

        private DataTable _dataBangLuong;
        public DataTable DataBangLuong
        {
            get => _dataBangLuong;
            set => SetProperty(ref _dataBangLuong, value);
        }

        private TlBangLuongThangBHXHModel _selectedBangLuongModel;
        public TlBangLuongThangBHXHModel SelectedBangLuongModel
        {
            get => _selectedBangLuongModel;
            set => SetProperty(ref _selectedBangLuongModel, value);
        }

        private string _searchCanBo;
        public string SearchCanBo
        {
            get => _searchCanBo;
            set => SetProperty(ref _searchCanBo, value);
        }

        private string _searchBangLuong;
        public string SearchBangLuong
        {
            get => _searchBangLuong;
            set => SetProperty(ref _searchBangLuong, value);
        }

        public RelayCommand SearchCommand { get; set; }

        public BackPaySalaryDetailViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ITlBangLuongThangService tlBangLuongThangService,
            ITlBangLuongThangBHXHService tlBangLuongThangBHXHService,
            ITlBangLuongThangTruyThuService tlBangLuongThangTruyThuService,
             ILog logger)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _tlBangLuongThangService = tlBangLuongThangService;
            _tlBangLuongThangBHXHService = tlBangLuongThangBHXHService;
            _tlBangLuongThangTruyThuService = tlBangLuongThangTruyThuService;
            _logger = logger;

            SearchCommand = new RelayCommand(obj => OnSearch());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            SearchCanBo = string.Empty;
            LoadData();
        }

        public void LoadData()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                e.Result = _tlBangLuongThangTruyThuService.GetDataLuongThangTruyThu(Model.Id);
            }, (s, e) =>
            {
                IsLoading = false;

                if (e.Error == null)
                {
                    _data = (DataTable)e.Result;
                    DataBangLuong = _data;
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
            });
        }

        public override void Dispose()
        {
            base.Dispose();

            if (DataBangLuong != null)
                DataBangLuong.Clear();
        }

        private void OnSearch()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                DataTable rs;
                if (!string.IsNullOrEmpty(SearchCanBo))
                {
                    Func<DataRow, bool> predicate = x => x.Field<string>("MaCanBo").Contains(SearchCanBo, StringComparison.OrdinalIgnoreCase)
                        || x.Field<string>("TenCanBo").Contains(SearchCanBo, StringComparison.OrdinalIgnoreCase);

                    var data = _data.AsEnumerable().Where(predicate);
                    if (data.Any())
                    {
                        rs = data.CopyToDataTable();
                    }
                    else
                    {
                        rs = _data.Clone();
                    }
                }
                else
                {
                    rs = _data.Copy();
                }

                e.Result = rs;
            }, (s, e) =>
            {
                IsLoading = false;

                if (e.Error == null)
                {
                    DataBangLuong = (DataTable)e.Result;
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
            });
        }

        private void OnResetFilter()
        {
            DataBangLuong = _data;
        }
    }
}
