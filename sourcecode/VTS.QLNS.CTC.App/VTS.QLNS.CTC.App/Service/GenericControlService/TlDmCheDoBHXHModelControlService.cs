using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.App.View.Shared;
using VTS.QLNS.CTC.App.Model.Control;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.Utility;
using System.ComponentModel;
using VTS.QLNS.CTC.App.Properties;
using MaterialDesignThemes.Wpf;
using System.Windows.Data;
using VTS.QLNS.CTC.App.ViewModel.Shared;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class TlDmCheDoBHXHModelControlService : GenericControlBaseService<TlDmCheDoBHXHModel, TlDmCheDoBHXH, TlDmCheDoBHXHService>
    {
        public MucLucNganSachBHXHView MucLucNganSachBHXHView { get; set; }
        public override void InitDialog(PropertyInfo property)
        {
            MucLucNganSachBHXHViewModel mucLucNganSachViewModel = sourceVM._serviceProvider.GetService(typeof(MucLucNganSachBHXHViewModel)) as MucLucNganSachBHXHViewModel;
            mucLucNganSachViewModel.Init();
            mucLucNganSachViewModel._isFirstLoad = true;
            mucLucNganSachViewModel._dataCollectionView = CollectionViewSource.GetDefaultView(mucLucNganSachViewModel.Items);
            mucLucNganSachViewModel._dataCollectionView.Filter = mucLucNganSachViewModel.ItemsViewFilter;
            mucLucNganSachViewModel._isFirstLoad = false;
            mucLucNganSachViewModel.TlDmCheDoBHXHModel = sourceVM.SelectedItem as TlDmCheDoBHXHModel;
            mucLucNganSachViewModel.IsMappingRegime = true;
            MucLucNganSachBHXHView = new MucLucNganSachBHXHView()
            {
                DataContext = mucLucNganSachViewModel
            };
            mucLucNganSachViewModel.SavedAction = obj =>
            {
                sourceVM.SelectedItem.SXauNoiMaMlnsBHXH = mucLucNganSachViewModel.SelectedItem.SXauNoiMa;
                sourceVM.SelectedItem.SMlnsBHXH = mucLucNganSachViewModel.SelectedItem.SMoTa;
            };
            mucLucNganSachViewModel.MucLucNganSachBHXHView = MucLucNganSachBHXHView;
            var dialog = DialogHost.Show(MucLucNganSachBHXHView, "RootDialog");
            mucLucNganSachViewModel.IsDialog = true;
        }

        public override ObservableCollection<ComboboxItem> LoadComboboxData(PropertyInfo propertyInfo)
        {
            if (propertyInfo.Name.Equals(nameof(TlDmCheDoBHXHModel.ILoaiCheDo)))
            {
                return new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem {DisplayItem = "", ValueItem = ""},
                    new ComboboxItem {DisplayItem = "Trợ cấp ốm đau", ValueItem = "1"},
                    new ComboboxItem {DisplayItem = "Trợ cấp thai sản", ValueItem = "2"},
                    new ComboboxItem {DisplayItem = "Trợ cấp tai nạn LĐ", ValueItem = "3"},
                    new ComboboxItem {DisplayItem = "Trợ cấp hưu trí, phục viên, thôi việc, tử tuất", ValueItem = "4"},
                    new ComboboxItem {DisplayItem = "Trợ cấp xuất ngũ", ValueItem = "5"}
                };
            }
            else if (propertyInfo.Name.Equals(nameof(TlDmCheDoBHXHModel.SMaCheDoCha)))
            {
                ObservableCollection<ComboboxItem> cbxCheDoCha = new ObservableCollection<ComboboxItem>();
                var lstCheDoChaExist = sourceVM._service.FindAll(sourceVM._authenticationInfo).Select(x => x.SMaCheDo).ToList();
                cbxCheDoCha.Add(new ComboboxItem { ValueItem = "", DisplayItem = "" });
                cbxCheDoCha.Add(new ComboboxItem { ValueItem = "CHE_DO_CHA", DisplayItem = "--Chọn chế độ cha--" });
                foreach (var item in lstCheDoChaExist)
                {
                    cbxCheDoCha.Add(new ComboboxItem { ValueItem = item, DisplayItem = item });
                }
                return cbxCheDoCha;
            }
            else if (propertyInfo.Name.Equals(nameof(TlDmCheDoBHXHModel.SLoaiTruyLinh)))
            {
                List<int> lstLoaiTruyLinh = new List<int>() { 3, 4 };
                ObservableCollection<ComboboxItem> cbxLoaiTruyLinh = new ObservableCollection<ComboboxItem>();
                var lstCheDoChaExist = sourceVM._service.FindAll(sourceVM._authenticationInfo).Where(x => x.ILoaiCheDo.HasValue && lstLoaiTruyLinh.Contains(x.ILoaiCheDo.Value)).Select(x => x.SMaCheDo).ToList();
                cbxLoaiTruyLinh.Add(new ComboboxItem { ValueItem = "", DisplayItem = "" });
                foreach (var item in lstCheDoChaExist)
                {
                    cbxLoaiTruyLinh.Add(new ComboboxItem { ValueItem = item, DisplayItem = item });
                }
                return cbxLoaiTruyLinh;
            }
            return new ObservableCollection<ComboboxItem>();
        }

        public override bool validate()
        {
            var dataToSave = sourceVM.Items.Where(i => i.IsModified || i.IsDeleted);
            List<string> lstMessError = new List<string>();
            foreach (var item in dataToSave)
            {
                var checkExist = sourceVM._service.CheckPhuCapExist(item.SMaCheDo, item.Id);
                if (checkExist && !item.IsDeleted)
                {
                    lstMessError.Add(string.Format(Resources.AlertMaCheDoExist, item.SMaCheDo));
                }
                if (string.IsNullOrEmpty(item.SMaCheDo))
                {
                    lstMessError.Add(string.Format(Resources.AlertMaCheDoEmpty));
                }
            }
            if (lstMessError.Count != 0)
            {
                MessageBoxHelper.Error(string.Join("\n", lstMessError));
                return false;
            }
            return true;
        }

        public override void OrderData()
        {
            List<TlDmCheDoBHXHModel> lstData = sourceVM.Items.ToList();
            if (lstData == null || lstData.Count == 0) return;
            List<TlDmCheDoBHXHModel> results = new List<TlDmCheDoBHXHModel>();
            foreach (var item in lstData.Where(n => string.IsNullOrEmpty(n.SMaCheDoCha)).OrderBy(n => n.SMaCheDo))
            {
                results.AddRange(Recusive(item, lstData));
            }
            sourceVM.Items = new ObservableCollection<TlDmCheDoBHXHModel>(results);
        }

        public List<TlDmCheDoBHXHModel> Recusive(TlDmCheDoBHXHModel item, List<TlDmCheDoBHXHModel> lstItem)
        {
            List<TlDmCheDoBHXHModel> lstData = new List<TlDmCheDoBHXHModel>();
            lstData.Add(item);
            if (lstItem.Any(n => n.SMaCheDoCha == item.SMaCheDo))
            {
                foreach (var child in lstItem.Where(n => n.SMaCheDoCha == item.SMaCheDo).OrderBy(n => n.SMaCheDo))
                {
                    lstData.AddRange(Recusive(child, lstItem));
                }
            }
            return lstData;
        }
    }
}
