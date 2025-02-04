using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.ThamDinhQuyetToan;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.ThamDinhQuyetToan
{
    public class ThamDinhQuyetToanDetailDialogViewModel : DialogViewModelBase<BhThamDinhQuyetToanModel>
    {
        private ISessionService _sessionService;
        private ILog _logger;
        private IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly IBhThamDinhQuyetToanService _bhThamDinhQuyetToanService;

        public override Type ContentType => typeof(ThamDinhQuyetToanDetailDialog);
        public ThamDinhQuyetToanDetailDialogViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            IBhThamDinhQuyetToanService bhThamDinhQuyetToanService
            )
        {
            _sessionService = sessionService;
            _logger = logger;
            _mapper = mapper;
            _bhThamDinhQuyetToanService = bhThamDinhQuyetToanService;
        }

        public override void Init()
        {
            try
            {
                _sessionInfo = _sessionService.Current;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void OnSave()
        {
            try
            {
                base.OnSave();

                BhThamDinhQuyetToan entity;
                entity = _bhThamDinhQuyetToanService.Find(Model.Id);
                entity.SGiaiThichChenhLech = Model.SGiaiThichChenhLech;
                _bhThamDinhQuyetToanService.Update(entity);

                DialogHost.CloseDialogCommand.Execute(null, null);
                SavedAction?.Invoke(_mapper.Map<BhThamDinhQuyetToanModel>(entity));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
