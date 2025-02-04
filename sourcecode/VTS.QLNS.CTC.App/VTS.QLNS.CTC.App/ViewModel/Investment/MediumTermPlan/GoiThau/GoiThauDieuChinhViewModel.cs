using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.GoiThau
{
    public class GoiThauDieuChinhViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly IVdtDaGoiThauService _vdtDaGoiThauService;

        public override string Name => "ĐIỀU CHỈNH THÔNG TIN GÓI THẦU";
        public override string Title => "ĐIỀU CHỈNH";
        public override string Description => "Điều chỉnh thông tin gói thầu";
        public override Type ContentType => typeof(View.Investment.MediumTermPlan.GoiThau.GoiThauDieuChinh);

        private VTS.QLNS.CTC.App.Model.VdtDaGoiThauModel _goiThau;
        public VTS.QLNS.CTC.App.Model.VdtDaGoiThauModel GoiThau
        {
            get => _goiThau;
            set => SetProperty(ref _goiThau, value);
        }

        public RelayCommand SaveCommand { get; }

        public GoiThauDieuChinhViewModel(
            IVdtDaGoiThauService vdtDaGoiThauService,
            IMapper mapper,
            ISessionService sessionService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _vdtDaGoiThauService = vdtDaGoiThauService;

            SaveCommand = new RelayCommand(o => OnSave());
        }

        public override void Init()
        {
            GoiThau.STenGoiThau = null;
            GoiThau.DNgayLap = null;
            GoiThau.SPhuongThucDauThau = null;
            GoiThau.SThoiGianThucHien = null;
        }

        public void OnSave()
        {
            if (string.IsNullOrEmpty(GoiThau.STenGoiThau))
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckTenGoiThau, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!GoiThau.DNgayLap.HasValue)
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckNgayLap, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // thêm mới GoiThau
            VdtDaGoiThau entity = new VdtDaGoiThau();
            entity = _mapper.Map<VdtDaGoiThau>(GoiThau);
            entity.Id = Guid.Empty;
            entity.IIdParentId = GoiThau.Id;
            entity.BActive = true;
            entity.BIsGoc = false;
            entity.DDateCreate = DateTime.Now;
            entity.SUserCreate = _sessionService.Current.Principal;
            _vdtDaGoiThauService.Add(entity);

            //update GoiThau cha 
            VdtDaGoiThau goiThauParent = _vdtDaGoiThauService.FindById(GoiThau.Id);
            goiThauParent.BActive = false;
            goiThauParent.DDateUpdate = DateTime.Now;
            goiThauParent.SUserUpdate = _sessionService.Current.Principal;
            _vdtDaGoiThauService.Update(goiThauParent);

            DialogHost.CloseDialogCommand.Execute(null, null);
            SavedAction?.Invoke(_mapper.Map<VTS.QLNS.CTC.App.Model.VdtDaGoiThauModel>(entity));
        }
    }
}
