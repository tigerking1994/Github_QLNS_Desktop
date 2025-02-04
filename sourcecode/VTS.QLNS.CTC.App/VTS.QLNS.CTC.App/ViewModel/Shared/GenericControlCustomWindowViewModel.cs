using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Component;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.View.Shared;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;

namespace VTS.QLNS.CTC.App.ViewModel
{
    public class GenericControlCustomWindowViewModel : ViewModelBase
    {
        public override Type ContentType => typeof(GenericControlCustomWindow);

        public GenericControlCustomWindow Window { get; set; }

        public GenericControlCustomWindowViewModel(object model)
        {
            ViewModelBase m = model as ViewModelBase;
            m.ParentPage = this;
            CurrentPage = m;
        }
    }
}
