using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.TKTCVaTongDuToan;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.TKTCVaTongDuToan
{
    public class TKTCVaTongDuToanHangMucAllDetailViewModel : DetailViewModelBase<VdtDuToanModel, DuToanDetailModel>
    {
        #region Private
        private IMapper _mapper;
        private ISessionService _sessionService;
        private IVdtDaDuToanService _vdtDaDuToanService;
        #endregion

        public override string Name => "TKTC VÀ TỔNG DỰ TOÁN CHI TIẾT";
        public override string Title => "Quản lý thiết kế thi công và tổng dự toán";
        public override Type ContentType => typeof(TKTCVaTongDuToanHangMucAllDetail);

        public TKTCVaTongDuToanHangMucAllDetailViewModel(
            IMapper mapper,
            ISessionService sessionService,
            IApproveProjectService approveProjectService,
            IProjectManagerService projectService,
            IVdtDaDuToanService vdtDaDuToanService
            )
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _vdtDaDuToanService = vdtDaDuToanService;
        }

        public override void Init()
        {
            LoadData();
            OnPropertyChanged(nameof(Items));
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            List<DuToanDetailQuery> listData = _vdtDaDuToanService.FindListHangMucAllDetail(Model.Id).ToList();
            Items = _mapper.Map<ObservableCollection<Model.DuToanDetailModel>>(listData);
        }
    }
}
