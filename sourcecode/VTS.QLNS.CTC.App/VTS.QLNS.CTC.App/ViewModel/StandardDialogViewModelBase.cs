using AutoMapper;
using log4net;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Service;

namespace VTS.QLNS.CTC.App.ViewModel
{
    public class StandardDialogViewModelBase<T> : StandardViewModelBase
    {
        private T _model;
        public T Model
        {
            get => _model;
            set
            {
                if (SetProperty(ref _model, value))
                {
                    OnModelPropertyChanged();
                }
            }
        }

        public RelayCommand CellEditEndingCommand { get; set; }

        public StandardDialogViewModelBase(
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            INsDonViService nsDonViService,
            IDanhMucService danhMucService) : base(sessionService, mapper, logger, nsDonViService, danhMucService)
        {
            CellEditEndingCommand = new RelayCommand(obj => OnCellEditEnding(obj));
        }

        public virtual void OnCellEditEnding(object obj)
        {

        }

        protected virtual void OnModelPropertyChanged()
        {

        }
    }
}
