using AutoMapper;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement.PursuitSalaryMonthTable
{
    public class PursuitSalaryDetailViewModel : DetailViewModelBase<TlDSCapNhapBangLuongModel, TlBangLuongThangModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ITlBangLuongThangService _tlBangLuongThangService;
        private SessionInfo _sessionInfo;

        public override string FuncCode => NSFunctionCode.SALARY_MANAGEMENT_PURSUIT_SALARY_DETAIL;

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

        private TlBangLuongThangModel _selectedBangLuongModel;
        public TlBangLuongThangModel SelectedBangLuongModel
        {
            get => _selectedBangLuongModel;
            set => SetProperty(ref _selectedBangLuongModel, value);
        }

        public PursuitSalaryDetailViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ITlBangLuongThangService tlBangLuongThangService)
        {
            _mapper = mapper;
            _sessionService = sessionService;

            _tlBangLuongThangService = tlBangLuongThangService;
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadData();
        }

        private void LoadData()
        {
            DataBangLuong = _tlBangLuongThangService.GetDataLuongThang(Model.Id);
        }
    }
}
