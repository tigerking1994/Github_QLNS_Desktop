using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Command;

namespace VTS.QLNS.CTC.App.ViewModel
{
    public class DialogViewModelBase<T> : ViewModelBase
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

        public DialogViewModelBase()
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
