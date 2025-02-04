using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service.Impl;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class NhDmNoiDungChiModelControlService:GenericControlBaseService<NhDmNoiDungChiModel, NhDmNoiDungChi, NhDmNoiDungChiService>
    {
        public override void CustomValueProps(NhDmNoiDungChiModel newRow, NhDmNoiDungChiModel currentRow)
        {
            base.CustomValueProps(newRow, currentRow);
            newRow.Id = Guid.Empty;
        }
        //public override void LoadData(params object[] args)
        //{
        //    var data = sourceVM._service.FindAll().ToList();
        //    sourceVM.Items = sourceVM._mapper.Map<ObservableCollection<NhDmNoiDungChiModel>>(data);
        //    OnPropertyChanged();
        //    sourceVM._isFirstLoad = true;
        //    sourceVM._dataCollectionView = CollectionViewSource.GetDefaultView(sourceVM.Items);
        //    sourceVM._dataCollectionView.Filter = ItemsViewFilter;
        //    sourceVM.InvokePropertyChange(nameof(sourceVM.Items));
        //    sourceVM._isFirstLoad = false;
        //}
    }
}
