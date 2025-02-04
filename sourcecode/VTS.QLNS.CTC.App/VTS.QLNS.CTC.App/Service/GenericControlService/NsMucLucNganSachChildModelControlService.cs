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
using System.ComponentModel;

using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class NsMucLucNganSachChildModelControlService : GenericControlBaseService<NsMuclucNganSachChildModel, NsMucLucNganSach, MlnsChildService>
    {
        private ICollection<NsMuclucNganSachChildModel> _filterResult = new HashSet<NsMuclucNganSachChildModel>();
        private string xnmConcatenation = "";

        public override void CustomValueProps(NsMuclucNganSachChildModel newRow, NsMuclucNganSachChildModel currentRow)
        {
            base.CustomValueProps(newRow, currentRow);
            newRow.Id = Guid.NewGuid();
        }

        public override void LoadData(params object[] args)
        {
            var data = sourceVM._service.FindAll(sourceVM._authenticationInfo, sourceVM.IsPopup, sourceVM.NotIns).OrderBy(n => n.XauNoiMa).ToList();
            sourceVM.Items = sourceVM._mapper.Map<ObservableCollection<NsMuclucNganSachChildModel>>(data);
            OnPropertyChanged();
            sourceVM._isFirstLoad = true;
            sourceVM._dataCollectionView = CollectionViewSource.GetDefaultView(sourceVM.Items);
            sourceVM._dataCollectionView.Filter = ItemsViewFilter;
            sourceVM.InvokePropertyChange(nameof(sourceVM.Items));
            sourceVM._isFirstLoad = false;
        }

        public override void BeForeRefresh()
        {
            var filterModel = sourceVM.FilterModel;
            _filterResult = sourceVM.Items.Where(item => FilterFunction(sourceVM.FilterModel, item)).Where(item => !item.BHangCha).ToList();
            xnmConcatenation = string.Join(";", _filterResult.Select(i => i.XNM).ToHashSet());
        }

        public override bool ItemsViewFilter(object obj)
        {
            if (sourceVM._isFirstLoad)
            {
                return true;
            }
            bool result = true;
            var item = (NsMuclucNganSachChildModel)obj;
            result = FilterFunction(sourceVM.FilterModel, item);
            if (!result && item.BHangCha)
            {
                result = xnmConcatenation.StartsWith(item.XNM) || xnmConcatenation.Contains(";" + item.XNM);
            }
            return result;
        }

        private bool FilterFunction(NsMuclucNganSachChildModel filterModel, NsMuclucNganSachChildModel item)
        {
            {
                var result = true;
                if (sourceVM.IsVisibleFilterByMlnsMappingType && !string.IsNullOrEmpty(sourceVM.MlnsMapping))
                    result = result && item.IsSelected.Equals(Convert.ToBoolean(sourceVM.MlnsMapping)) && item.BHangChaDuToan.HasValue && !item.BHangChaDuToan.Value;
                if (!string.IsNullOrEmpty(filterModel.Lns))
                    result = result && item.Lns.ToLower().Contains(filterModel.Lns.ToLower());
                if (!string.IsNullOrEmpty(filterModel.L))
                    result = result && item.L.ToLower().Contains(filterModel.L.ToLower());
                if (!string.IsNullOrEmpty(filterModel.K))
                    result = result && item.K.ToLower().Contains(filterModel.K.ToLower());
                if (!string.IsNullOrEmpty(filterModel.M))
                    result = result && item.M.ToLower().Contains(filterModel.M.ToLower());
                if (!string.IsNullOrEmpty(filterModel.TM))
                    result = result && item.TM.ToLower().Contains(filterModel.TM.ToLower());
                if (!string.IsNullOrEmpty(filterModel.TTM))
                    result = result && item.TTM.ToLower().Contains(filterModel.TTM.ToLower());
                if (!string.IsNullOrEmpty(filterModel.NG))
                    result = result && item.NG.ToLower().Contains(filterModel.NG.ToLower());
                if (!string.IsNullOrEmpty(filterModel.TNG))
                    result = result && item.TNG.ToLower().Contains(filterModel.TNG.ToLower());
                if (!string.IsNullOrEmpty(filterModel.MoTa))
                    result = result && item.MoTa.ToLower().Contains(filterModel.MoTa.ToLower());
                return result;
            }

            return false;
        }

        public override void OnPropertyChanged()
        {
            foreach (var t in sourceVM.Items)
            {
                t.PropertyChanged += NsMuclucNgansachModel_PropertyChanged;
            }
        }

        private void NsMuclucNgansachModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            IEnumerable<NsMuclucNganSachChildModel> models = sourceVM.Items;
            NsMuclucNganSachChildModel nsMuclucNgansachModel = sender as NsMuclucNganSachChildModel;
            // khi tích vào ô tất cả, tránh để hàm propertychanged lặp lại để đảm bảo performance (dùng biến afterSelectAll để kiểm tra điều kiện người dùng vừa tích vào ô tất cả)
            if (args.PropertyName == nameof(NsMuclucNganSachChildModel.IsSelected) && !sourceVM.AfterSelectAll)
            {
                // set this to avoid looping this function
                sourceVM.AfterSelectAll = true;
                // IEnumerable<NsMuclucNgansachModel> children = models.Where(t => nsMuclucNgansachModel.MlnsId.Equals(t.MlnsIdParent));
                IEnumerable<NsMuclucNganSachChildModel> children = models.Where(t => t.XNM.StartsWith(nsMuclucNgansachModel.XNM));
                foreach (var c in children)
                {
                    c.IsSelected = nsMuclucNgansachModel.IsSelected;
                }
                // remember to set it to old value again
                sourceVM.AfterSelectAll = false;
            }
        }
    }
}
