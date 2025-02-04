using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.ViewModel.Shared;
using VTS.QLNS.CTC.App.ViewModel.SystemAdmin.UserManagement.Group;
using VTS.QLNS.CTC.App.ViewModel.SystemAdmin.UserManagement.User;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.SystemAdmin.UserManagement
{
    public class UserManagementViewModel : ViewModelBase
    {
        private ISysFunctionService _sysFunctionService;
        private ISysAuthorityService _sysAuthorityService;
        private IDisplaySysFunctionService _displaySysFunctionService;
        private IMapper _mapper;
        private ISessionService _sessionService;
        private IServiceProvider _provider;

        public override string FuncCode => NSFunctionCode.SYSTEM_USER_MANAGEMENT;
        public override string Name => "QUẢN TRỊ NGƯỜI DÙNG";
        public override string Description => "Quản trị người dùng";
        public override string Title => "Quản trị người dùng";
        public override Type ContentType => typeof(View.SystemAdmin.UserManagement.UserManagement);
        public override PackIconKind IconKind => PackIconKind.Account;

        public UserIndexViewModel UserIndexViewModel { get; }
        public GroupIndexViewModel GroupIndexViewModel { get; }
        public UserLnsIndexViewModel UserLnsIndexViewModel { get; }

        public UserManagementViewModel(UserIndexViewModel userIndexViewModel, 
            GroupIndexViewModel groupIndexViewModel,
            UserLnsIndexViewModel userLnsIndexViewModel,
            ISysFunctionService sysFunctionService,
            IDisplaySysFunctionService displaySysFunctionService,
            ISysAuthorityService sysAuthorityService,
            IServiceProvider provider,
            IMapper mapper,
            ISessionService sessionService)
        {
            UserIndexViewModel = userIndexViewModel;
            GroupIndexViewModel = groupIndexViewModel;
            UserLnsIndexViewModel = userLnsIndexViewModel;
            _sysFunctionService = sysFunctionService;
            _sysAuthorityService = sysAuthorityService;
            _displaySysFunctionService = displaySysFunctionService;
            _mapper = mapper;
            _sessionService = sessionService;
            _provider = provider;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(0);

            Documentation = new ObservableCollection<ViewModelBase>()
            {
               UserIndexViewModel,
               GroupIndexViewModel,
               new GenericControlCustomViewModel<DisplayHTChucNangModel, Core.Domain.HtChucNang, DisplaySysFunctionService>((DisplaySysFunctionService)_displaySysFunctionService, _mapper, _sessionService, _provider)
               {
                    Name = "Danh sách chức năng",
                    Title = "Danh sách chức năng",
                    Description = "Danh sách chức năng",
                    FuncCode = NSFunctionCode.SYSTEM_USER_DANH_SACH_CHUC_NANG
               },
               new GenericControlCustomViewModel<HTQuyenModel, Core.Domain.HtQuyen, SysAuthorityService>((SysAuthorityService)_sysAuthorityService, _mapper, _sessionService, _provider)
               {
                    Name = "Cấu hình quyền - chức năng",
                    Title = "Cấu hình quyền - chức năng",
                    Description = "Cấu hình quyền - chức năng",
                    FuncCode = NSFunctionCode.SYSTEM_USER_CAU_HINH_QUYEN_CHUC_NANG
               },
               //UserLnsIndexViewModel
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
