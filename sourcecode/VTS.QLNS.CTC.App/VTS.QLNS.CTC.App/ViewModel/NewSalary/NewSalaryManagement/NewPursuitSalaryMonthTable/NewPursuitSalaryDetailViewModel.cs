using AutoMapper;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagement.NewPursuitSalaryMonthTable
{
    public class NewPursuitSalaryDetailViewModel : DetailViewModelBase<TlDSCapNhapBangLuongNq104Model, TlBangLuongThangNq104Model>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ITlBangLuongThangNq104Service _tlBangLuongThangService;
        private readonly ITlBangLuongThangBridgeNq104Service _tlBangLuongThangBridgeService;
        private SessionInfo _sessionInfo;

        public override string FuncCode => NSFunctionCode.NEW_SALARY_MANAGEMENT_PURSUIT_SALARY_DETAIL;

        public override string Title => "BẢNG LƯƠNG TRUY LĨNH CHI TIẾT";

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

        private TlBangLuongThangNq104Model _selectedBangLuongModel;
        public TlBangLuongThangNq104Model SelectedBangLuongModel
        {
            get => _selectedBangLuongModel;
            set => SetProperty(ref _selectedBangLuongModel, value);
        }

        public NewPursuitSalaryDetailViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ITlBangLuongThangNq104Service tlBangLuongThangService,
            ITlBangLuongThangBridgeNq104Service tlBangLuongThangBridgeService)
        {
            _mapper = mapper;
            _sessionService = sessionService;

            _tlBangLuongThangService = tlBangLuongThangService;
            _tlBangLuongThangBridgeService = tlBangLuongThangBridgeService;
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadData();
        }

        private void LoadData()
        {
            _tlBangLuongThangBridgeService.DataPreprocess(Model.Thang, Model.Nam, Model.MaDonVi, "CACH5");
            DataBangLuong = _tlBangLuongThangService.GetDataLuongThang(Model.Id);
        }
    }
}
