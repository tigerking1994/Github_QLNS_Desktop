using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.View.Shared;
using VTS.QLNS.CTC.App.Converters;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class NsMuclucNganSachModelControlService : GenericControlBaseService<NsMuclucNgansachModel, Core.Domain.NsMucLucNganSach, MucLucNganSachService>
    {
        private ICollection<NsMuclucNgansachModel> _filterResult = new HashSet<NsMuclucNgansachModel>();
        private string xnmConcatenation = "";
        private List<string> mlnsType = new List<string>
        {
            "TNG3", "TNG2", "TNG1", "TNG", "NG"
        };

        private string getTypeOfMlns(NsMuclucNgansachModel nsMuclucNgansachModel)
        {
            foreach (string type in mlnsType)
            {
                PropertyInfo propertyInfo = typeof(NsMuclucNgansachModel).GetProperty(type);
                object val = propertyInfo.GetValue(nsMuclucNgansachModel, null);
                if (val != null && !string.IsNullOrWhiteSpace(val.ToString()))
                {
                    return type;
                }
            }
            return "";
        }

        public override void OnPropertyChanged()
        {
            foreach (var t in sourceVM.Items)
            {
                t.PropertyChanged += NsMuclucNgansachModel_PropertyChanged;
            }
        }

        public override void OnAddChild(object obj)
        {
            if (sourceVM.SelectedItem == null)
                return;
            IMucLucNganSachService mucLucNganSachService = sourceVM._service;
            DataGrid dgdData = obj as DataGrid;
            this.CancelEditData(dgdData);

            int currentRow = sourceVM.Items.IndexOf(sourceVM.SelectedItem);
            NsMuclucNgansachModel parent = sourceVM.SelectedItem;
            NsMuclucNgansachModel newRow = ObjectCopier.Clone(sourceVM.SelectedItem);
            newRow.Id = Guid.Empty;
            newRow.MlnsId = Guid.NewGuid();
            newRow.MlnsIdParent = parent.MlnsId;
            newRow.IsModified = true;
            newRow.BHangCha = false;
            parent.BHangCha = true;
            newRow.BHangChaDuToan = null;
            newRow.BHangChaQuyetToan = null;
            newRow.MlnsParentName = parent.XNM;
            // nếu dòng mới là ng,tng,tng1,2,3 thì cần update bhangchadutoan và bhangcha quyết toán
            var parentRowType = getTypeOfMlns(parent);
            if (mlnsType.IndexOf(parentRowType) > -1)
            {
                // find ng parent
                var ngParent = sourceVM.Items.FirstOrDefault(i => "NG".Equals(getTypeOfMlns(i)) && newRow.XNM.Contains(i.XNM) &&
                    (!string.IsNullOrWhiteSpace(i.SDuToanChiTietToi) || !string.IsNullOrWhiteSpace(i.SQuyetToanChiTietToi)));
                if (ngParent != null)
                {
                    ngParent.IsModified = true;
                }
            }
            newRow.PropertyChanged += Item_PropertyChanged;
            newRow.PropertyChanged += NsMuclucNgansachModel_PropertyChanged;
            sourceVM.Items.Insert(currentRow + 1, newRow);
            sourceVM.InvokePropertyChange("Items");
            var cell = new DataGridCellInfo(sourceVM.Items[currentRow + 1], dgdData.Columns[0]);
            dgdData.ScrollIntoView(sourceVM.Items[currentRow + 1], dgdData.Columns[0]);
            dgdData.CurrentCell = cell;
            dgdData.BeginEdit();
        }

        public override void OnPropertyChanged(NsMuclucNgansachModel model)
        {
            model.PropertyChanged += NsMuclucNgansachModel_PropertyChanged;
        }

        public override void CustomValueProps(NsMuclucNgansachModel newRow, NsMuclucNgansachModel currentRow)
        {
            base.CustomValueProps(newRow, currentRow);
            newRow.MlnsId = Guid.NewGuid();
        }

        private void NsMuclucNgansachModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            IEnumerable<NsMuclucNgansachModel> models = sourceVM.Items;
            NsMuclucNgansachModel nsMuclucNgansachModel = sender as NsMuclucNgansachModel;
            // khi tích vào ô tất cả, tránh để hàm propertychanged lặp lại để đảm bảo performance (dùng biến afterSelectAll để kiểm tra điều kiện người dùng vừa tích vào ô tất cả)
            if (args.PropertyName == nameof(NsMuclucNgansachModel.IsSelected) && !sourceVM.AfterSelectAll)
            {
                // set this to avoid looping this function
                sourceVM.AfterSelectAll = true;
                // IEnumerable<NsMuclucNgansachModel> children = models.Where(t => nsMuclucNgansachModel.MlnsId.Equals(t.MlnsIdParent));
                IEnumerable<NsMuclucNgansachModel> children = models.Where(t => t.XNM.StartsWith(nsMuclucNgansachModel.XNM));
                foreach (var c in children)
                {
                    c.IsSelected = nsMuclucNgansachModel.IsSelected;
                }
                // remember to set it to old value again
                sourceVM.AfterSelectAll = false;
            }
            else if (args.PropertyName == nameof(NsMuclucNgansachModel.IsDeleted))
            {
                NsMuclucNgansachModel_OnDeleteMLNS(nsMuclucNgansachModel, models);
            }
            else if (args.PropertyName == nameof(NsMuclucNgansachModel.ITrangThai))
            {
                IEnumerable<NsMuclucNgansachModel> children = models.Where(t => nsMuclucNgansachModel.MlnsId.Equals(t.MlnsIdParent));
                foreach (var c in children)
                {
                    c.ITrangThai = nsMuclucNgansachModel.ITrangThai;
                }
            }
            else if (mlnsType.Contains(args.PropertyName))
            {
                string typeMLNS = getTypeOfMlns(nsMuclucNgansachModel);
                if (!typeMLNS.Equals("NG"))
                {
                    nsMuclucNgansachModel.SDuToanChiTietToi = "";
                    nsMuclucNgansachModel.SQuyetToanChiTietToi = "";
                }
                nsMuclucNgansachModel.IsEnableDuToanNGCombobox = typeMLNS.Equals("NG") && !nsMuclucNgansachModel.IsUsedDuToanChiTietToi;
                nsMuclucNgansachModel.IsEnableQuyetToanNGCombobox = typeMLNS.Equals("NG") && !nsMuclucNgansachModel.IsUsedQuyetToanChiTietToi;
            }
            else if (nameof(NsMuclucNgansachModel.SDuToanChiTietToi).Equals(args.PropertyName))
            {
                List<NsMuclucNgansachModel> children = models.Where(t => t.XNM.Contains(nsMuclucNgansachModel.XNM + "-")).ToList();
                string typeMLNS = getTypeOfMlns(nsMuclucNgansachModel);
                if (typeMLNS.Equals("NG"))
                {
                    // todo update bHangChaDuToan of children
                    int selectedSDuToanIndex = mlnsType.IndexOf(nsMuclucNgansachModel.SDuToanChiTietToi);
                    string modelNsType = "NG";
                    int modelNsTypeIndex = mlnsType.IndexOf(modelNsType);
                    UpdateBHangChaDuToan(selectedSDuToanIndex, modelNsTypeIndex, nsMuclucNgansachModel);
                    foreach (NsMuclucNgansachModel ns in children)
                    {
                        string nsType = getTypeOfMlns(ns);
                        int nsTypeIndex = mlnsType.IndexOf(nsType);
                        UpdateBHangChaDuToan(selectedSDuToanIndex, nsTypeIndex, ns);
                    }
                }
            }
            else if (nameof(NsMuclucNgansachModel.SQuyetToanChiTietToi).Equals(args.PropertyName))
            {
                List<NsMuclucNgansachModel> children = models.Where(t => t.XNM.Contains(nsMuclucNgansachModel.XNM + "-")).ToList();
                string typeMLNS = getTypeOfMlns(nsMuclucNgansachModel);
                if (typeMLNS.Equals("NG"))
                {
                    // todo update bHangChaDuToan of children
                    int selectedSQTIndex = mlnsType.IndexOf(nsMuclucNgansachModel.SQuyetToanChiTietToi);
                    string modelNsType = "NG";
                    int modelNsTypeIndex = mlnsType.IndexOf(modelNsType);
                    UpdateBHangChaQuyetToan(selectedSQTIndex, modelNsTypeIndex, nsMuclucNgansachModel);
                    foreach (NsMuclucNgansachModel ns in children)
                    {
                        string nsType = getTypeOfMlns(ns);
                        int nsTypeIndex = mlnsType.IndexOf(nsType);
                        UpdateBHangChaQuyetToan(selectedSQTIndex, nsTypeIndex, ns);
                    }
                }
            }
            //nsMuclucNgansachModel.IsUnableEditBQuanlyChiTietToi = !this.IsEditableMlnsBQuanLyAndChiTietToi(nsMuclucNgansachModel);
        }

        public override void BeforeSave()
        {
            // get saved ng type
            List<NsMuclucNgansachModel> Parent = new List<NsMuclucNgansachModel>();
            var dataToSave = sourceVM.Items.Where(i => i.IsModified && !i.IsDeleted).ToList();
            foreach (NsMuclucNgansachModel model in dataToSave)
            {
                string typeMLNS = getTypeOfMlns(model);
                if ("NG".Equals(typeMLNS) &&
                (!string.IsNullOrWhiteSpace(model.SDuToanChiTietToi) || !string.IsNullOrWhiteSpace(model.SQuyetToanChiTietToi)))
                {
                    List<NsMuclucNgansachModel> children = sourceVM.Items.Where(t => t.XNM.Contains(model.XNM + "-")).ToList();
                    // update bhangcha du toan vaf bhangcha quyet toan
                    int selectedSDuToanIndex = mlnsType.IndexOf(model.SDuToanChiTietToi);
                    int selectedSQTIndex = mlnsType.IndexOf(model.SQuyetToanChiTietToi);
                    string modelNsType = getTypeOfMlns(model);
                    int modelNsTypeIndex = mlnsType.IndexOf(modelNsType);
                    UpdateBHangChaDuToan(selectedSDuToanIndex, modelNsTypeIndex, model);
                    UpdateBHangChaQuyetToan(selectedSQTIndex, modelNsTypeIndex, model);
                    foreach (NsMuclucNgansachModel ns in children)
                    {
                        ns.IsModified = true;
                        string nsType = getTypeOfMlns(ns);
                        int nsTypeIndex = mlnsType.IndexOf(nsType);
                        UpdateBHangChaDuToan(selectedSDuToanIndex, nsTypeIndex, ns);
                        UpdateBHangChaQuyetToan(selectedSQTIndex, nsTypeIndex, ns);
                    }
                }
                if (string.IsNullOrEmpty(typeMLNS))
                {
                    model.BHangChaDuToan = true;
                    model.BHangChaQuyetToan = true;
                }
                var parent = sourceVM.Items.Where(i => !i.IsDeleted && !i.XNM.Equals(model.XNM)
                                                                && model.XNM.StartsWith(i.XNM)).OrderBy(t => t.XNM.Length).LastOrDefault();
                if (parent != null)
                {
                    model.MlnsIdParent = parent.MlnsId;
                    parent.BHangCha = true;
                }
                else
                {
                    model.MlnsIdParent = null;
                }
            }
        }

        public override void LoadData(params object[] args)
        {
            var data = new List<NsMucLucNganSach>();
            if (sourceVM.IsPopup)
            {
                data = sourceVM._service.FindAll(sourceVM._authenticationInfo, sourceVM.IsPopup, sourceVM.NotIns).ToList();
            }
            else
            {
                data = sourceVM._service.FindAll(sourceVM._authenticationInfo).ToList();
            }

            sourceVM.Items = sourceVM._mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(data);
            foreach (var item in sourceVM.Items)
            {
                string typeMLNS = getTypeOfMlns(item);
                item.IsEnableDuToanNGCombobox = typeMLNS.Equals("NG") && !item.IsUsedDuToanChiTietToi;
                item.IsEnableQuyetToanNGCombobox = typeMLNS.Equals("NG") && !item.IsUsedQuyetToanChiTietToi;
                item.PropertyChanged += Item_PropertyChanged;
            }
            OnPropertyChanged();
            sourceVM._isFirstLoad = true;
            sourceVM._dataCollectionView = CollectionViewSource.GetDefaultView(sourceVM.Items);
            sourceVM._dataCollectionView.Filter = ItemsViewFilter;
            sourceVM.InvokePropertyChange(nameof(sourceVM.Items));
            sourceVM._isFirstLoad = false;
        }

        private void NsMuclucNgansachModel_OnDeleteMLNS(NsMuclucNgansachModel model, IEnumerable<NsMuclucNgansachModel> models)
        {
            NsMuclucNgansachModel parent = models.FirstOrDefault(i => i.MlnsId == model.MlnsIdParent);
            if (parent == null)
            {
                return;
            }
            bool hasChild = models.Any(i => i.MlnsIdParent == parent.MlnsId && !i.IsDeleted);
            parent.BHangCha = hasChild;

        }

        private bool IsEditableMlnsBQuanLyAndChiTietToi(NsMuclucNgansachModel nsMuclucNgansachModel)
        {
            // Nếu không phải lns thì ko dc thêm b quản lý
            if (string.IsNullOrEmpty(nsMuclucNgansachModel.Lns))
            {
                return false;
            }
            if (!StringUtils.IsNullOrEmpty(nsMuclucNgansachModel.L) ||
                !StringUtils.IsNullOrEmpty(nsMuclucNgansachModel.K) ||
                !StringUtils.IsNullOrEmpty(nsMuclucNgansachModel.M) ||
                !StringUtils.IsNullOrEmpty(nsMuclucNgansachModel.TM) ||
                !StringUtils.IsNullOrEmpty(nsMuclucNgansachModel.TTM) ||
                !StringUtils.IsNullOrEmpty(nsMuclucNgansachModel.NG) ||
                !StringUtils.IsNullOrEmpty(nsMuclucNgansachModel.TNG))
            {
                return false;
            }
            // Nếu có dòng con là lns thì ko dc thêm
            IEnumerable<NsMuclucNgansachModel> children = sourceVM.Items.Where(p => p.MlnsIdParent.Equals(nsMuclucNgansachModel.MlnsId));
            bool hasLnsChild = children.Any(p => !string.IsNullOrEmpty(p.Lns) &&
                StringUtils.IsNullOrEmpty(p.L) &&
                StringUtils.IsNullOrEmpty(p.K) &&
                StringUtils.IsNullOrEmpty(p.M) &&
                StringUtils.IsNullOrEmpty(p.TM) &&
                StringUtils.IsNullOrEmpty(p.TTM) &&
                StringUtils.IsNullOrEmpty(p.NG) &&
                StringUtils.IsNullOrEmpty(p.TNG));
            return !hasLnsChild;
        }

        public override ObservableCollection<ComboboxItem> LoadComboboxData(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(NsMuclucNgansachModel.ITrangThai)))
            {
                return new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "Không sử dụng", ValueItem = "0" },
                    new ComboboxItem { DisplayItem = "Đang sử dụng", ValueItem = "1" }
                };
            }
            else if (property.Name.Equals(nameof(NsMuclucNgansachModel.SDuToanChiTietToi)) || property.Name.Equals(nameof(NsMuclucNgansachModel.SQuyetToanChiTietToi)))
            {
                return new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "", ValueItem = null },
                    new ComboboxItem { DisplayItem = "NG", ValueItem = "NG" },
                    new ComboboxItem { DisplayItem = "TNG", ValueItem = "TNG" },
                    new ComboboxItem { DisplayItem = "TNG1", ValueItem = "TNG1" },
                    new ComboboxItem { DisplayItem = "TNG2", ValueItem = "TNG2" },
                    new ComboboxItem { DisplayItem = "TNG3", ValueItem = "TNG3" },
                };
            }
            else if (property.Name.Equals(nameof(NsMuclucNgansachModel.SCPChiTietToi)))
            {
                return new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "", ValueItem = null },
                    new ComboboxItem { DisplayItem = "M", ValueItem = "M" },
                    new ComboboxItem { DisplayItem = "TM", ValueItem = "TM" },
                    new ComboboxItem { DisplayItem = "NG", ValueItem = "NG" },
                };
            }
            return new ObservableCollection<ComboboxItem>();
        }

        public override bool ItemsViewFilter(object obj)
        {
            if (sourceVM._isFirstLoad)
            {
                return true;
            }
            bool result = true;
            var item = (NsMuclucNgansachModel)obj;
            result = FilterFunction(sourceVM.FilterModel, item);
            if (!result && item.BHangCha)
            {
                result = xnmConcatenation.StartsWith(item.XNM) || xnmConcatenation.Contains(";" + item.XNM);
            }
            return result;
        }

        private bool FilterFunction(NsMuclucNgansachModel filterModel, NsMuclucNgansachModel item)
        {
            var result = true;
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
            if (!string.IsNullOrEmpty(filterModel.TNG1))
                result = result && item.TNG1.ToLower().Contains(filterModel.TNG1.ToLower());
            if (!string.IsNullOrEmpty(filterModel.TNG2))
                result = result && item.TNG2.ToLower().Contains(filterModel.TNG2.ToLower());
            if (!string.IsNullOrEmpty(filterModel.TNG3))
                result = result && item.TNG3.ToLower().Contains(filterModel.TNG3.ToLower());
            if (!string.IsNullOrEmpty(filterModel.MoTa))
                result = result && item.MoTa.ToLower().Contains(filterModel.MoTa.ToLower());
            if (!string.IsNullOrEmpty(filterModel.MlnsParentName))
                result = result && item.MlnsParentName != null && item.MlnsParentName.Contains(filterModel.MlnsParentName);
            if (sourceVM.IsVisibleFilterByMlnsMappingType && !string.IsNullOrEmpty(sourceVM.MlnsMapping))
                result = result && item.IsSelected.Equals(Convert.ToBoolean(sourceVM.MlnsMapping)) && item.BHangChaDuToan.HasValue && !item.BHangChaDuToan.Value;
            return result;
        }

        public override void BeForeRefresh()
        {
            var filterModel = sourceVM.FilterModel;
            _filterResult = sourceVM.Items.Where(item => FilterFunction(sourceVM.FilterModel, item)).Where(item => !item.BHangCha).ToList();
            xnmConcatenation = string.Join(";", _filterResult.Select(i => i.XNM).ToHashSet());
        }

        public override void OnDelete(object obj)
        {
            NsMuclucNgansachModel item = sourceVM.SelectedItem as NsMuclucNgansachModel;
            IMucLucNganSachService mucLucNganSachService = sourceVM._service as IMucLucNganSachService;
            if (mucLucNganSachService.IsUsedMLNS(item.MlnsId, sourceVM._authenticationInfo.YearOfWork))
            {
                MessageBox.Show(Resources.UnableToDeleteUsedMLNS, Resources.Alert, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            base.OnDelete(obj);
        }

        public override void OnRefreshWithOutReload(object obj)
        {
            sourceVM.FilterModel = new NsMuclucNgansachModel();
            sourceVM._currentCodeValDictionary = new Dictionary<string, int>();
            //sourceVM.Items.Clear();
            //sourceVM.Items = new ObservableCollection<NsMuclucNgansachModel>();
            sourceVM.Items = sourceVM._mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(sourceVM.Items.Where(t => !t.IsDeleted).OrderBy(s => s.XNM));
            CustomDataValue();
            foreach (var item in sourceVM.Items)
            {
                string typeMLNS = getTypeOfMlns(item);
                item.IsEnableDuToanNGCombobox = typeMLNS.Equals("NG") && !item.IsUsedDuToanChiTietToi;
                item.IsEnableQuyetToanNGCombobox = typeMLNS.Equals("NG") && !item.IsUsedQuyetToanChiTietToi;
                item.PropertyChanged += Item_PropertyChanged;
                item.IsModified = false;
                item.IsDeleted = false;
            }
            OnPropertyChanged();
            sourceVM._isFirstLoad = true;
            sourceVM._dataCollectionView = CollectionViewSource.GetDefaultView(sourceVM.Items);
            sourceVM._dataCollectionView.SortDescriptions.Clear();
            sourceVM._dataCollectionView.Filter = ItemsViewFilter;
            sourceVM._isFirstLoad = false;
            sourceVM.InvokePropertyChange(nameof(sourceVM.Items));
            DataGrid dgdData = obj as DataGrid;
            if (dgdData != null && dgdData.Columns != null)
            {
                foreach (var column in dgdData.Columns)
                {
                    column.SortDirection = null;
                }
            }
            this.CancelEditData(dgdData);
            if (dgdData.Items.Count > 0)
            {
                var cell = new DataGridCellInfo(dgdData.Items[0], dgdData.Columns[0]);
                sourceVM.SelectedItem = (NsMuclucNgansachModel)dgdData.Items[0];
                dgdData.CurrentCell = cell;
            }
        }

        public override void OnRefresh(object obj)
        {
            var rawData = sourceVM._service.FindAll(sourceVM._authenticationInfo);
            foreach (var i in rawData)
            {
                i.IsModified = false;
                i.IsDeleted = false;
            }
            sourceVM.FilterModel = new NsMuclucNgansachModel();
            sourceVM._currentCodeValDictionary = new Dictionary<string, int>();
            sourceVM.Items.Clear();
            sourceVM.Items = new ObservableCollection<NsMuclucNgansachModel>();
            sourceVM.Items = sourceVM._mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(rawData);
            CustomDataValue();
            foreach (var item in sourceVM.Items)
            {
                string typeMLNS = getTypeOfMlns(item);
                item.IsEnableDuToanNGCombobox = typeMLNS.Equals("NG") && !item.IsUsedDuToanChiTietToi;
                item.IsEnableQuyetToanNGCombobox = typeMLNS.Equals("NG") && !item.IsUsedQuyetToanChiTietToi;
                item.PropertyChanged += Item_PropertyChanged;
            }
            OnPropertyChanged();
            sourceVM._isFirstLoad = true;
            sourceVM._dataCollectionView = CollectionViewSource.GetDefaultView(sourceVM.Items);
            sourceVM._dataCollectionView.SortDescriptions.Clear();
            sourceVM._dataCollectionView.Filter = ItemsViewFilter;
            sourceVM._isFirstLoad = false;
            sourceVM.InvokePropertyChange(nameof(sourceVM.Items));
            DataGrid dgdData = obj as DataGrid;
            if (dgdData != null && dgdData.Columns != null)
            {
                foreach (var column in dgdData.Columns)
                {
                    column.SortDirection = null;
                }
            }
            this.CancelEditData(dgdData);
            if (dgdData.Items.Count > 0)
            {
                var cell = new DataGridCellInfo(dgdData.Items[0], dgdData.Columns[0]);
                sourceVM.SelectedItem = (NsMuclucNgansachModel)dgdData.Items[0];
                dgdData.CurrentCell = cell;
            }
        }

        private void UpdateBHangChaDuToan(int selectedSDuToanIndex, int nsTypeIndex, NsMuclucNgansachModel ns)
        {
            if (selectedSDuToanIndex > -1)
            {
                if (nsTypeIndex > selectedSDuToanIndex)
                {
                    ns.BHangChaDuToan = true;
                }
                else if (nsTypeIndex == selectedSDuToanIndex)
                {
                    ns.BHangChaDuToan = false;
                }
                else
                {
                    ns.BHangChaDuToan = null;
                }
            }
        }

        public void UpdateBHangChaQuyetToan(int selectedSQTIndex, int nsTypeIndex, NsMuclucNgansachModel ns)
        {
            if (selectedSQTIndex > -1)
            {
                if (nsTypeIndex > selectedSQTIndex)
                {
                    ns.BHangChaQuyetToan = true;
                }
                else if (nsTypeIndex == selectedSQTIndex)
                {
                    ns.BHangChaQuyetToan = false;
                }
                else
                {
                    ns.BHangChaQuyetToan = null;
                }
            }
        }

        public override bool validate()
        {
            var dataToSave = sourceVM.Items.Where(i => i.IsModified && !i.IsDeleted);
            foreach (NsMuclucNgansachModel model in dataToSave)
            {
                int selectedSDuToanIndex = mlnsType.IndexOf(model.SDuToanChiTietToi);
                int selectedSQTIndex = mlnsType.IndexOf(model.SQuyetToanChiTietToi);
                if (selectedSQTIndex > selectedSDuToanIndex)
                {
                    MessageBoxHelper.Error(Resources.MLNS_DT_QT_Err);
                    return false;
                }
            }
            return true;
        }

        public override bool IsDisableColumn(PropertyInfo property)
        {
            return nameof(NsMuclucNgansachModel.MlnsParentName).Equals(property.Name);
        }

        public override void InitDialog(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(NsMuclucNgansachModel.MlnsParentName)))
            {
                var mlnsService = sourceVM._serviceProvider.GetService(typeof(IMucLucNganSachService));
                GenericControlCustomViewModel<NsMuclucNgansachModel, NsMucLucNganSach, MucLucNganSachService> dialogVM =
                    new GenericControlCustomViewModel<NsMuclucNgansachModel, NsMucLucNganSach, MucLucNganSachService>
                    ((MucLucNganSachService)mlnsService, sourceVM._mapper, sourceVM._sessionService, sourceVM._serviceProvider);
                dialogVM.IsDialog = true;
                dialogVM.Description = "Danh mục mlns";
                dialogVM.Title = "Chọn MLNS cha - mlns " + sourceVM.SelectedItem.XNM;
                GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(dialogVM);
                dialogVM.IsMultipleSelect = false;
                dialogVM.SelectedItem = dialogVM.Items.Where(i => i.MlnsId.Equals(sourceVM.SelectedItem.MlnsIdParent)).FirstOrDefault();
                GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                {
                    DataContext = genericControlCustomWindowViewModel
                };
                GenericControlCustomWindow.SavedAction = obj =>
                {
                    NsMuclucNgansachModel data = obj as NsMuclucNgansachModel;
                    if (data.XNM.Equals(sourceVM.SelectedItem.XNM) || !sourceVM.SelectedItem.XNM.StartsWith(data.XNM))
                    {
                        MessageBoxHelper.Warning("MLNS không hợp lệ");
                        return;
                    }
                    var mlnsParent = sourceVM.Items.FirstOrDefault(t => t.MlnsId.Equals(data.MlnsId));
                    if (mlnsParent != null)
                        mlnsParent.BHangCha = true;
                    sourceVM.SelectedItem.MlnsIdParent = data.MlnsId;
                    sourceVM.SelectedItem.MlnsParentName = data.XNM;
                    GenericControlCustomWindow.Close();
                };
                dialogVM.GenericControlCustomWindow = GenericControlCustomWindow;
                GenericControlCustomWindow.Show();
            }
        }

        public override void CustomGenerateDatagridTextColumn(MaterialDesignThemes.Wpf.DataGridTextColumn col, PropertyInfo property)
        {
            string[] cols = new string[]
            {
                nameof(NsMuclucNgansachModel.Lns), nameof(NsMuclucNgansachModel.L), nameof(NsMuclucNgansachModel.K), nameof(NsMuclucNgansachModel.M),
                nameof(NsMuclucNgansachModel.TM), nameof(NsMuclucNgansachModel.TTM), nameof(NsMuclucNgansachModel.NG), nameof(NsMuclucNgansachModel.TNG),
                nameof(NsMuclucNgansachModel.TNG1), nameof(NsMuclucNgansachModel.TNG2), nameof(NsMuclucNgansachModel.TNG3),
            };
            if (cols.Contains(property.Name))
            {
                Style cellStyle = new Style();
                cellStyle.TargetType = typeof(TextBox);
                cellStyle.BasedOn = (Style)Application.Current.TryFindResource("MaterialDesignTextBox");
                DataTrigger dataTrigger = new DataTrigger();
                dataTrigger.Value = true;
                dataTrigger.Binding = new Binding("BHangCha");
                dataTrigger.Setters.Add(new Setter { Property = TextBox.IsEnabledProperty, Value = false });
                cellStyle.Triggers.Add(dataTrigger);
                col.EditingElementStyle = cellStyle;
            }
        }

        public override void CustomGenerateComboboxColumn(MaterialDesignThemes.Wpf.DataGridComboBoxColumn col, PropertyInfo property)
        {
            if (property.Name.Equals(nameof(NsMuclucNgansachModel.SDuToanChiTietToi)))
            {
                Style cellStyle = new Style();
                cellStyle.TargetType = typeof(DataGridCell);
                cellStyle.BasedOn = (Style)Application.Current.TryFindResource("DataGridCellDetail");
                cellStyle.Setters.Add(new Setter(UIElement.IsEnabledProperty, new Binding("IsEnableDuToanNGCombobox")));
                col.CellStyle = cellStyle;
            }
            else if (property.Name.Equals(nameof(NsMuclucNgansachModel.SQuyetToanChiTietToi)))
            {
                Style cellStyle = new Style();
                cellStyle.TargetType = typeof(DataGridCell);
                cellStyle.BasedOn = (Style)Application.Current.TryFindResource("DataGridCellDetail");
                cellStyle.Setters.Add(new Setter(UIElement.IsEnabledProperty, new Binding("IsEnableQuyetToanNGCombobox")));
                col.CellStyle = cellStyle;
            }
            else if (property.Name.Equals(nameof(NsMuclucNgansachModel.SCPChiTietToi)))
            {
                Style cellStyle = new Style();
                cellStyle.TargetType = typeof(DataGridCell);
                cellStyle.BasedOn = (Style)Application.Current.TryFindResource("DataGridCellDetail");
                cellStyle.Setters.Add(new Setter(UIElement.IsEnabledProperty, new Binding("IsEditableCPChiTietToi")));
                col.CellStyle = cellStyle;
            }
            else if (property.Name.Equals(nameof(NsMuclucNgansachModel.ITrangThai)))
            {
                Style cellStyle = new Style();
                cellStyle.TargetType = typeof(DataGridCell);
                cellStyle.BasedOn = (Style)Application.Current.TryFindResource("DataGridCellDetail");
                cellStyle.Setters.Add(new Setter(UIElement.IsEnabledProperty, new Binding("IsEditableStatus")));
                col.CellStyle = cellStyle;
            }
        }
    }
}
