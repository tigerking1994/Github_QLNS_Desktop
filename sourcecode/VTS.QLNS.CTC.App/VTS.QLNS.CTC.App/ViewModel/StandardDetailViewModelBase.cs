using AutoMapper;
using log4net;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel
{
    public class StandardDetailViewModelBase<E, T> : StandardGridViewModelBase<T>
    {
        private E _model;
        public E Model
        {
            get => _model;
            set
            {
                SetProperty(ref _model, value);
                OnModelChanged();
            }
        }

        private bool _isReadOnly;
        public virtual bool IsReadOnly
        {
            get => _isReadOnly;
            set => SetProperty(ref _isReadOnly, value);
        }

        private MLNSColumnDisplay _columnDisplay;
        public MLNSColumnDisplay ColumnDisplay
        {
            get => _columnDisplay;
            set => SetProperty(ref _columnDisplay, value);
        }

        public string ChiTietToi { get; set; }

        public RelayCommand CellEditEndingCommand { get; set; }

        public StandardDetailViewModelBase(ISessionService sessionService,
            IMapper mapper, ILog logger, INsDonViService nsDonViService, IDanhMucService danhMucService) : base(sessionService, mapper, logger, nsDonViService, danhMucService)
        {
            CellEditEndingCommand = new RelayCommand(obj => OnCellEditEnding(obj));
        }

        public override void Init()
        {
            base.Init();
            if (_danhMucService != null && _sessionService != null)
                SettingMLNSDisplay();
        }

        public virtual void OnCellEditEnding(object obj)
        {

        }

        protected virtual void OnModelChanged()
        {

        }

        private void SettingMLNSDisplay()
        {
            var danhMucMLNS = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
            ChiTietToi = danhMucMLNS == null ? string.Empty : danhMucMLNS.SGiaTri;
            ColumnDisplay = DynamicMLNS.SettingColumnVisibility(new List<string> { ChiTietToi });
        }
    }
}
