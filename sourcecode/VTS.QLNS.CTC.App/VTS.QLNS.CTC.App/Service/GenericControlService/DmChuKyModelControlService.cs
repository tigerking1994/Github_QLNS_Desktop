using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.View.Category;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Service.Impl;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class DmChuKyModelControlService : GenericControlBaseService<DmChuKyModel, Core.Domain.DmChuKy, DmChuKyService>
    {
        public override void OnAdd(object obj)
        {
            DmChuKyDialogViewModel dmChuKyDialogViewModel = new DmChuKyDialogViewModel(sourceVM._mapper, sourceVM._serviceProvider, sourceVM._sessionService)
            {
                DmChuKyModel = new DmChuKyModel()
            };
            dmChuKyDialogViewModel.ParentPage = sourceVM;
            dmChuKyDialogViewModel.Init();
            dmChuKyDialogViewModel.SavedAction = (obj) =>
            {
                LoadData();
            };
            dmChuKyDialogViewModel.ShowDialog();
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine(eventArgs.ToString());
        }
    }
}
