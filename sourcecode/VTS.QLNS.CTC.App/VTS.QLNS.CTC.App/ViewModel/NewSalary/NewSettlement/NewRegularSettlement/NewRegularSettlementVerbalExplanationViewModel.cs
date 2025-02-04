using AutoMapper;
using System;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.NewSalary.NewSettlement.NewRegularSettlement;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.Explanation
{
    public class NewRegularSettlementVerbalExplanationViewModel : DialogViewModelBase<TlQtChungTuChiTietGiaiThichNq104Model>
    {
        private readonly ITlQtChungTuChiTietGiaiThichNq104Service _tlQtChungTuChiTietGiaiThichService;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private SessionInfo _sessionInfo;

        public override string Name => "Quyết toán - Chứng từ chi tiết - Giải thích bằng lời";
        public override Type ContentType => typeof(NewRegularSettlementVerbalExplanation);
        public TlQtChungTuNq104Model ChungTuModel { get; set; }

        public NewRegularSettlementVerbalExplanationViewModel(
            ITlQtChungTuChiTietGiaiThichNq104Service tlQtChungTuChiTietGiaiThichService,
            IMapper mapper,
            ISessionService sessionService)
        {
            _tlQtChungTuChiTietGiaiThichService = tlQtChungTuChiTietGiaiThichService;
            _mapper = mapper;
            _sessionService = sessionService;
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadData();
        }

        private void LoadData()
        {
            Model = new TlQtChungTuChiTietGiaiThichNq104Model();
            TlQtChungTuChiTietGiaiThichNq104 entity = _tlQtChungTuChiTietGiaiThichService.FindByChungTuId(ChungTuModel.Id);
            if (entity != null)
            {
                Model = _mapper.Map<TlQtChungTuChiTietGiaiThichNq104Model>(entity);
            }
        }

        public override void OnSave()
        {
            base.OnSave();

            TlQtChungTuChiTietGiaiThichNq104 entity = _tlQtChungTuChiTietGiaiThichService.FindByChungTuId(ChungTuModel.Id);
            if (entity == null)
            {
                // Create
                entity = new TlQtChungTuChiTietGiaiThichNq104();
                _mapper.Map(Model, entity);
                entity.IThang = ChungTuModel.Thang;
                entity.INam = ChungTuModel.Nam;
                entity.IMaDonVi = ChungTuModel.MaDonVi;
                entity.IIdQtchungTu = ChungTuModel.Id;
                entity.DNgayTao = DateTime.Now;
                entity.SNguoiTao = _sessionInfo.Principal;
                _tlQtChungTuChiTietGiaiThichService.Add(entity);
            }
            else
            {
                // Update
                _mapper.Map(Model, entity);
                entity.DNgaySua = DateTime.Now;
                entity.SNguoiSua = _sessionInfo.Principal;
                _tlQtChungTuChiTietGiaiThichService.Update(entity);
            }

            System.Windows.Forms.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public override void OnClose(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }
    }
}
