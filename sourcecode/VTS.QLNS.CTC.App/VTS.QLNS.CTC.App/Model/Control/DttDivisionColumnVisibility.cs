using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model.Control
{
    public class DttDivisionColumnVisibility : BindableBase
    {
        private bool _isDisplayBhxhDieuChinh;
        public bool IsDisplayBhxhDieuChinh
        {
            get => _isDisplayBhxhDieuChinh;
            set => SetProperty(ref _isDisplayBhxhDieuChinh, value);
        }

    }
}
