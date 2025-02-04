using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Category;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Category
{
    public class CategoryInvestmentViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly IDmLoaiCongTrinhService _dmLoaiCongTrinhService;
        private readonly IVdtDmChiPhiService _vdtDmChiPhiService;
        private readonly IVdtDmNhaThauService _vdtDmNhaThauService;
        private readonly IDmChuDauTuService _dmChuDauTuService;
        private readonly NsNguonNganSachService _nsNguonNganSachService;
        private readonly IVdtDmDonViThucHienDuAnService _vdtDmDonViThucHienDuAnService;
        private readonly IDmDTChiService _dmDTChiService;
        private readonly IServiceProvider _provider;
        private ISessionService _sessionService;

        public override Type ContentType => typeof(CategoryInvestment);
        public override string Name => "DANH MỤC VỐN ĐẦU TƯ";
        public override string FuncCode => NSFunctionCode.CATEGORY_INVESTMENT;

        public CategoryInvestmentViewModel(IMapper mapper,
            IDmLoaiCongTrinhService dmLoaiCongTrinhService,
            IVdtDmChiPhiService vdtDmChiPhiService,
            IVdtDmNhaThauService vdtDmNhaThauService,
            IDmChuDauTuService dmChuDauTuService,
            NsNguonNganSachService nsNguonNganSachService,
            IVdtDmDonViThucHienDuAnService vdtDmDonViThucHienDuAnService,
            IDmDTChiService dmDTChiService,
            IServiceProvider provider,
            ISessionService sessionService)
        {
            _mapper = mapper;
            _dmLoaiCongTrinhService = dmLoaiCongTrinhService;
            _vdtDmChiPhiService = vdtDmChiPhiService;
            _vdtDmNhaThauService = vdtDmNhaThauService;
            _dmChuDauTuService = dmChuDauTuService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _provider = provider;
            _sessionService = sessionService;
            _vdtDmDonViThucHienDuAnService = vdtDmDonViThucHienDuAnService;
            _dmDTChiService = dmDTChiService;
        }

        public override void Init()
        {
            base.Init();
            MarginRequirement = new System.Windows.Thickness(0);
            Documentation = new ObservableCollection<ViewModelBase>()
            {
                new GenericControlCustomViewModel<NguonNganSachModel, Core.Domain.NsNguonNganSach, NsNguonNganSachService>((NsNguonNganSachService)_nsNguonNganSachService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục nguồn vốn ngân sách",
                    Title = "Danh mục nguồn vốn ngân sách",
                    Description = "Danh sách danh mục nguồn vốn ngân sách",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Category,
                    IsDialog = false,
                },
                new GenericControlCustomViewModel<VdtDmLoaiCongTrinhModel, Core.Domain.VdtDmLoaiCongTrinh, DmLoaiCongTrinhService>((DmLoaiCongTrinhService)_dmLoaiCongTrinhService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục loại công trình",
                    Title = "Danh mục loại công trình",
                    Description = "Danh mục loại công trình",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Building,
                    IsDialog = false,
                    FuncCode = NSFunctionCode.CATEGORY_INVESTMENT_CONG_TRINH
                },
                new GenericControlCustomViewModel<VdtDmChiPhiModel, Core.Domain.VdtDmChiPhi, VdtDmChiPhiService>((VdtDmChiPhiService)_vdtDmChiPhiService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục loại chi phí",
                    Title = "Danh mục loại chi phí",
                    Description = "Danh mục loại chi phí",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.Money,
                    IsDialog = false,
                    FuncCode = NSFunctionCode.CATEGORY_INVESTMENT_CHI_PHI
                },
                new GenericControlCustomViewModel<VdtDmNhaThauModel, Core.Domain.VdtDmNhaThau, VdtDmNhaThauService>((VdtDmNhaThauService)_vdtDmNhaThauService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục nhà thầu",
                    Title = "Danh mục nhà thầu",
                    Description = "Danh mục nhà thầu",
                    IconKind = MaterialDesignThemes.Wpf.PackIconKind.OfficeBuilding,
                    IsDialog = false,
                    FuncCode = NSFunctionCode.CATEGORY_INVESTMENT_NHA_THAU
                },
                new GenericControlCustomViewModel<DmChuDauTuModel, Core.Domain.DmChuDauTu, DmChuDauTuService>((DmChuDauTuService)_dmChuDauTuService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục chủ đầu tư",
                    Title = "Danh mục chủ đầu tư",
                    Description = "Danh mục chủ đầu tư",
                    IsDialog = false,
                    FuncCode = NSFunctionCode.CATEGORY_INVESTMENT_CDT
                },
                new GenericControlCustomViewModel<VdtDmDonViThucHienDuAnModel, Core.Domain.VdtDmDonViThucHienDuAn, VdtDmDonViThucHienDuAnService>((VdtDmDonViThucHienDuAnService)_vdtDmDonViThucHienDuAnService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục đơn vị quản lý dự án",
                    Title = "Danh mục đơn vị quản lý dự án",
                    Description = "Danh mục đơn vị quản lý dự án",
                    IsDialog = false,
                    FuncCode = NSFunctionCode.CATEGORY_INVESTMENT_DON_VI_THUC_HIEN
                },
                new GenericControlCustomViewModel<VdtDmDuToanChiModel, Core.Domain.VdtDmDuToanChi, DmDTChiService>((DmDTChiService)_dmDTChiService, _mapper, _sessionService, _provider)
                {
                    Name = "Danh mục dự toán chi",
                    Title = "Danh mục dự toán chi",
                    Description = "Danh mục dự toán chi",
                    IsDialog = false,
                    FuncCode = NSFunctionCode.CATEGORY_INVESTMENT_DON_VI_THUC_HIEN
                },
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
