using AutoMapper;
using log4net;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Service;

namespace VTS.QLNS.CTC.App.ViewModel
{
    public abstract class StandardViewModelBase : ViewModelBase
    {
        protected readonly INsDonViService _donViService;
        protected readonly IDanhMucService _danhMucService;
        protected readonly ISessionService _sessionService;
        protected readonly IMapper _mapper;
        protected readonly ILog _logger;

        public int YearOfWork => _sessionService.Current.YearOfWork;
        public int YearOfBudget => _sessionService.Current.YearOfBudget;
        public string Principal => _sessionService.Current.Principal;

        public StandardViewModelBase(ISessionService sessionService,
            IMapper mapper, ILog logger, INsDonViService nsDonViService, IDanhMucService danhMucService)
        {
            _donViService = nsDonViService;
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;
            _danhMucService = danhMucService;
        }
    }
}
