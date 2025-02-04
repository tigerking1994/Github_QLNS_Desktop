using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.App.View.Shared;
using System.Linq;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows;
using VTS.QLNS.CTC.App.Properties;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class SktMucLucModelControlService : GenericControlBaseService<SktMucLucModel, Core.Domain.NsSktMucLuc, SktMucLucService>
    {
        private ICollection<SktMucLucModel> _filterResult = new HashSet<SktMucLucModel>();
        private string maConcatenation = "";

        public override void OnAddChild(object obj)
        {
            if (sourceVM.SelectedItem == null)
                return;
            DataGrid dgdData = obj as DataGrid;
            this.CancelEditData(dgdData);
            SktMucLucModel parent = sourceVM.SelectedItem;
            parent.BHangCha = true;
            sourceVM.SelectedItem.BHangCha = true;
            int currentRow = sourceVM.Items.IndexOf(sourceVM.SelectedItem);

            SktMucLucModel newRow = ObjectCopier.Clone(sourceVM.SelectedItem);
            newRow.Id = Guid.Empty;
            newRow.IIDMLSKT = Guid.NewGuid();
            newRow.SktMucLucMaps = new List<NsMlsktMlns>();
            newRow.MLNS = null;
            newRow.IIDMLSKTCha = parent.IIDMLSKT;
            newRow.KyHieuCha = parent.SKyHieu;
            newRow.IsModified = true;
            newRow.BHangCha = false;
            
            newRow.PropertyChanged += Item_PropertyChanged;
            newRow.PropertyChanged += SktMucLucModel_PropertyChanged;
            sourceVM.Items.Insert(currentRow + 1, newRow);
            sourceVM.InvokePropertyChange("Items");
            var cell = new DataGridCellInfo(sourceVM.Items[currentRow + 1], dgdData.Columns[0]);
            dgdData.ScrollIntoView(sourceVM.Items[currentRow + 1], dgdData.Columns[0]);
            dgdData.CurrentCell = cell;
            dgdData.BeginEdit();
        }

        public override void OnPropertyChanged()
        {
            foreach (var t in sourceVM.Items)
            {
                t.PropertyChanged += SktMucLucModel_PropertyChanged;
            }
        }

        public override void OnPropertyChanged(SktMucLucModel model)
        {
            model.PropertyChanged += SktMucLucModel_PropertyChanged;
        }

        public override void CustomValueProps(SktMucLucModel newRow, SktMucLucModel currentRow)
        {
            base.CustomValueProps(newRow, currentRow);
            newRow.SktMucLucMaps = new List<NsMlsktMlns>();
            newRow.IIDMLSKT = new Guid();
            newRow.MLNS = null;
        }

        private void SktMucLucModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            IEnumerable<SktMucLucModel> models = sourceVM.Items;
            SktMucLucModel sktMucLucModel = sender as SktMucLucModel;
            if (args.PropertyName == nameof(SktMucLucModel.IsDeleted))
            {
                SktMucLucModel_OnDeleteMLNS(sktMucLucModel, models);
            }
        }

        private void SktMucLucModel_OnDeleteMLNS(SktMucLucModel model, IEnumerable<SktMucLucModel> models)
        {
            SktMucLucModel parent = models.FirstOrDefault(i => i.IIDMLSKT == model.IIDMLSKTCha);
            if (parent == null)
            {
                return;
            }
            bool hasChild = models.Any(i => i.IIDMLSKTCha == parent.IIDMLSKT && !i.IsDeleted);
            parent.BHangCha = hasChild;
        }

        public override void InitDialog(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(SktMucLucModel.SNGCha)))
            {
                var DanhMucNhomNganhService = sourceVM._serviceProvider.GetService(typeof(DanhMucNhomNganhService));
                GenericControlCustomViewModel<DanhMucNhomNganhModel, DanhMuc, DanhMucNhomNganhService> dialogVM = new GenericControlCustomViewModel<DanhMucNhomNganhModel, DanhMuc, DanhMucNhomNganhService>
                    ((DanhMucNhomNganhService)DanhMucNhomNganhService, sourceVM._mapper, sourceVM._sessionService, sourceVM._serviceProvider);
                dialogVM.IsDialog = true;
                dialogVM.Description = "Danh mục nhóm ngành";
                dialogVM.Title = "Danh mục nhóm ngành";
                GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(dialogVM);
                dialogVM.IsMultipleSelect = false;
                dialogVM.SelectedItem = dialogVM.Items.Where(i => i.IIDMaDanhMuc.Equals(sourceVM.SelectedItem.SNGCha)).FirstOrDefault();
                GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                {
                    DataContext = genericControlCustomWindowViewModel
                };
                GenericControlCustomWindow.SavedAction = obj =>
                {
                    DanhMucNhomNganhModel data = obj as DanhMucNhomNganhModel;
                    sourceVM.SelectedItem.SNGCha = data.IIDMaDanhMuc;
                    GenericControlCustomWindow.Close();
                };
                dialogVM.GenericControlCustomWindow = GenericControlCustomWindow;
                GenericControlCustomWindow.Show();
            }
            else if (property.Name.Equals(nameof(SktMucLucModel.KyHieuCha)))
            {
                var SktMucLucService = sourceVM._serviceProvider.GetService(typeof(ISktMucLucService));
                GenericControlCustomViewModel<SktMucLucModel, NsSktMucLuc, SktMucLucService> dialogVM = new GenericControlCustomViewModel<SktMucLucModel, NsSktMucLuc, SktMucLucService>
                    ((SktMucLucService)SktMucLucService, sourceVM._mapper, sourceVM._sessionService, sourceVM._serviceProvider);
                dialogVM.IsDialog = true;
                dialogVM.Description = "Danh mục số kiểm tra";
                dialogVM.Title = "Danh mục số kiểm tra";
                GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(dialogVM);
                dialogVM.IsMultipleSelect = false;
                dialogVM.SelectedItem = dialogVM.Items.Where(i => i.IIDMLSKT.Equals(sourceVM.SelectedItem.IIDMLSKTCha)).FirstOrDefault();
                GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                {
                    DataContext = genericControlCustomWindowViewModel
                };
                GenericControlCustomWindow.SavedAction = obj =>
                {
                    SktMucLucModel data = obj as SktMucLucModel;
                    sourceVM.SelectedItem.IIDMLSKTCha = data.IIDMLSKT;
                    sourceVM.SelectedItem.KyHieuCha = data.SKyHieu;
                    GenericControlCustomWindow.Close();
                };
                dialogVM.GenericControlCustomWindow = GenericControlCustomWindow;
                GenericControlCustomWindow.Show();
            }
            else if (property.Name.Equals(nameof(SktMucLucModel.MLNS)))
            {
                var MucLucNganSachService = sourceVM._serviceProvider.GetService(typeof(IMucLucNganSachService));
                GenericControlCustomViewModel<NsMuclucNgansachModel, NsMucLucNganSach, MucLucNganSachService> dialogVM = new GenericControlCustomViewModel<NsMuclucNgansachModel, NsMucLucNganSach, MucLucNganSachService>
                    ((MucLucNganSachService)MucLucNganSachService, sourceVM._mapper, sourceVM._sessionService, sourceVM._serviceProvider);
                dialogVM._authenticationInfo.OptionalParam = InitMLNSParams();
                dialogVM.IsDialog = true;
                dialogVM.IsVisibleFilterByMlnsMappingType = true;
                dialogVM.Description = "Danh mục MLNS";
                dialogVM.Title = "Danh mục MLNS";
                GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(dialogVM);
                dialogVM.IsMultipleSelect = true;
                SetSelectedMLNS(dialogVM);
                GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                {
                    DataContext = genericControlCustomWindowViewModel
                };
                GenericControlCustomWindow.SavedAction = obj =>
                {
                    IEnumerable<NsMuclucNgansachModel> data = obj as IEnumerable<NsMuclucNgansachModel>;
                    // chỉ lưu mlns có bhangchadutoan = 0 (false)
                    sourceVM.SelectedItem.MLNS = string.Join("; ", data.Where(t => t.BHangChaDuToan.HasValue && !t.BHangChaDuToan.Value).Select(t => t.XauNoiMa));
                    List<NsMlsktMlns> sktMucLucMaps = data.Where(t => t.BHangChaDuToan.HasValue && !t.BHangChaDuToan.Value).Select(model => new NsMlsktMlns { SNsXauNoiMa = model.XauNoiMa }).ToList();
                    sourceVM.SelectedItem.SktMucLucMaps = sktMucLucMaps;
                    GenericControlCustomWindow.Close();
                };
                dialogVM.GenericControlCustomWindow = GenericControlCustomWindow;
                GenericControlCustomWindow.Show();
            }
        }

        private void SetSelectedMLNS(GenericControlCustomViewModel<NsMuclucNgansachModel, NsMucLucNganSach, MucLucNganSachService> dialogVM)
        {
            dialogVM.AfterSelectAll = true;
            IEnumerable<string> nsXauNoima = sourceVM.SelectedItem.SktMucLucMaps.Select(t => t.SNsXauNoiMa);
            string allXNm = String.Join(";", nsXauNoima);
            foreach (var model in dialogVM.Items)
            {
                if (nsXauNoima.Contains(model.XNM) || allXNm.StartsWith(model.XNM + "-") || allXNm.Contains(";" + model.XNM + "-"))
                {
                    model.IsSelected = true;
                }
                // nếu loại là LNS (1, 101, 1010000) thì check startwith (ko cos daaus -)
                if (string.IsNullOrEmpty(model.L))
                {
                    model.IsSelected = allXNm.StartsWith(model.XNM);
                }
            }
            dialogVM.AfterSelectAll = false;
        }

        private object[] InitMLNSParams()
        {
            SktMucLucModel sktMucLuc = sourceVM.SelectedItem;
            return new object[] { DialogType.LoadMLNSOfSktMucLuc, sktMucLuc.SKyHieu };
        }

        private bool FilterFunction(SktMucLucModel filterModel, SktMucLucModel item)
        {
            var result = true;
            if (!string.IsNullOrEmpty(filterModel.SM))
                result = result && item.SM.ToLower().Equals(filterModel.SM.ToLower());
            if (!string.IsNullOrEmpty(filterModel.SNGCha))
                result = result && item.SNGCha.ToLower().Equals(filterModel.SNGCha.ToLower());
            if (!string.IsNullOrEmpty(filterModel.SNg))
                result = result && item.SNg.ToLower().Equals(filterModel.SNg.ToLower());
            if (!string.IsNullOrEmpty(filterModel.SSTT))
                result = result && item.SSTT.ToLower().Equals(filterModel.SSTT.ToLower());
            if (!string.IsNullOrEmpty(filterModel.SSttBC))
                result = result && item.SSttBC.ToLower().StartsWith(filterModel.SSttBC.ToLower());
            if (!string.IsNullOrEmpty(filterModel.SKyHieu))
                result = result && item.SKyHieu.ToLower().StartsWith(filterModel.SKyHieu.ToLower());
            if (!string.IsNullOrEmpty(filterModel.SLoaiNhap))
                result = result && item.SLoaiNhap.ToLower().Contains(filterModel.SLoaiNhap.ToLower());
            if (!string.IsNullOrEmpty(filterModel.SMoTa))
                result = result && item.SMoTa.ToLower().Contains(filterModel.SMoTa.ToLower());
            if (!string.IsNullOrEmpty(filterModel.KyHieuCha))
                result = result && item.KyHieuCha.ToLower().Equals(filterModel.KyHieuCha.ToLower());
            if (!string.IsNullOrEmpty(filterModel.MLNS))
                result = result && item.MLNS.ToLower().Contains(filterModel.MLNS.ToLower());
            return result;
        }

        public override void BeForeRefresh()
        {
            var filterModel = sourceVM.FilterModel;
            _filterResult = sourceVM.Items.Where(item => FilterFunction(sourceVM.FilterModel, item)).Where(item => !item.BHangCha).ToList();
            maConcatenation = string.Join(";", _filterResult.Select(i => i.SKyHieu).ToHashSet());
        }

        public override bool ItemsViewFilter(object obj)
        {
            if (sourceVM._isFirstLoad)
            {
                return true;
            }
            bool result = true;
            var item = (SktMucLucModel)obj;
            result = FilterFunction(sourceVM.FilterModel, item);
            if (!result && item.BHangCha)
            {
                result = this.maConcatenation.Contains(item.SKyHieu + "-");
            }
            return result;
        }
        
        public override void OnDelete(object obj)
        {
            SktMucLucModel item = sourceVM.SelectedItem;
            SktMucLucService sktMucLucService = sourceVM._service;
            if (sktMucLucService.IsUsedMLSKT(item.IIDMLSKT, sourceVM._authenticationInfo.YearOfWork))
            {
                MessageBox.Show(Resources.UnableToDeleteUsedMLSKT, Resources.Alert, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            base.OnDelete(obj);
        }

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

        public override void BeforeSave()
        {
            var dataToSave = sourceVM.Items.Where(i => i.IsModified && !i.IsDeleted).ToList();
            foreach (SktMucLucModel model in dataToSave)
            {
                var parent = sourceVM.Items.Where(i => !i.IsDeleted && !i.SKyHieu.Equals(model.SKyHieu)
                                                                && model.SKyHieu.StartsWith(i.SKyHieu)).OrderBy(t => t.SKyHieu.Length).LastOrDefault();
                if (parent != null)
                {
                    model.IIDMLSKTCha = parent.IIDMLSKT;
                    parent.BHangCha = true;
                }
                else
                {
                    model.IIDMLSKTCha = null;
                }
            }
        }
    }
}
