using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.PheDuyetDuAn;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PheDuyetDuAn
{
    public class PheDuyetDuAnHangMucAllDeTailViewModel : DetailViewModelBase<ApproveProjectModel, ApproveProjectDetailModel>
    {
        #region Private
        private IMapper _mapper;
        private ISessionService _sessionService;
        private IApproveProjectService _approveProjectService;
        private IProjectManagerService _projectService;
        private IVdtDaDuToanService _vdtDaDuToanService;
        #endregion

        public override string Name => "PHÊ DUYỆT DỰ ÁN CHI TIẾT";
        public override string Title => "Quản lý phê duyệt dự án";
        public override Type ContentType => typeof(PheDuyetDuAnHangMucAllDetail);

        public PheDuyetDuAnHangMucAllDeTailViewModel(
            IMapper mapper,
            ISessionService sessionService,
            IApproveProjectService approveProjectService,
            IProjectManagerService projectService,
            IVdtDaDuToanService vdtDaDuToanService
            )
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _approveProjectService = approveProjectService;
            _vdtDaDuToanService = vdtDaDuToanService;
            _projectService = projectService;
        }

        public override void Init()
        {
            LoadData();
            OnPropertyChanged(nameof(Items));
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            List<ApproveProjectDetailQuery> listData = _approveProjectService.FindListAllHangMucByQDDauTu(Model.Id).ToList();

            Items = _mapper.Map<ObservableCollection<Model.ApproveProjectDetailModel>>(listData);

        }
    }
}
