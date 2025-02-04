using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Control
{
    public class MLNSColumnVisibility : BindableBase
    {
        private bool _isDisplayNG;
        public bool IsDisplayNG
        {
            get => _isDisplayNG;
            set => SetProperty(ref _isDisplayNG, value);
        }

        private bool _isDisplayTNG;
        public bool IsDisplayTNG
        {
            get => _isDisplayTNG;
            set => SetProperty(ref _isDisplayTNG, value);
        }

        private bool _isDisplayTNG1;
        public bool IsDisplayTNG1
        {
            get => _isDisplayTNG1;
            set => SetProperty(ref _isDisplayTNG1, value);
        }

        private bool _isDisplayTNG2;
        public bool IsDisplayTNG2
        {
            get => _isDisplayTNG2;
            set => SetProperty(ref _isDisplayTNG2, value);
        }

        private bool _isDisplayTNG3;
        public bool IsDisplayTNG3
        {
            get => _isDisplayTNG3;
            set => SetProperty(ref _isDisplayTNG3, value);
        }
    }
}
