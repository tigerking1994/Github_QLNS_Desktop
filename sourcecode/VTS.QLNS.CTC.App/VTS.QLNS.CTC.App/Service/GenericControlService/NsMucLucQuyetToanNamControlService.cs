using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.View.Shared;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.Core;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class NsMucLucQuyetToanNamControlService : GenericControlBaseService<DmMucLucQuyetToanModel, Core.Domain.NsMucLucQuyetToanNam, DmMucLucQuyetToanService>
    {
        private readonly ICollection<DmMucLucQuyetToanModel> _filterResult = new HashSet<DmMucLucQuyetToanModel>();
        private readonly string maConcatenation = "";

        public override void OnAddChild(object obj)
        {
            if (sourceVM.SelectedItem == null)
                return;
            var dgdData = obj as DataGrid;
            this.CancelEditData(dgdData);
            var parent = sourceVM.SelectedItem;
            parent.IsHangCha = true;
            sourceVM.SelectedItem.IsHangCha = true;
            var currentRow = sourceVM.Items.IndexOf(sourceVM.SelectedItem);

            var newRow = ObjectCopier.Clone(sourceVM.SelectedItem);
            newRow.Id = Guid.Empty;
            newRow.NsMucLucQuyetToanNamMLNS = new List<NsMucLucQuyetToanNamMLNS>();
            newRow.SMaCha = parent.SMa;
            newRow.IsModified = true;
            newRow.IsHangCha = false;
            OnPropertyChanged(newRow);


            newRow.PropertyChanged += Item_PropertyChanged;
            newRow.PropertyChanged += DmMucLucQuyetToanModel_PropertyChanged;
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
                t.PropertyChanged += DmMucLucQuyetToanModel_PropertyChanged;
            }
        }

        public override void OnPropertyChanged(DmMucLucQuyetToanModel model)
        {
            model.PropertyChanged += DmMucLucQuyetToanModel_PropertyChanged;
        }

        private void DmMucLucQuyetToanModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            IEnumerable<DmMucLucQuyetToanModel> models = sourceVM.Items;
            var DmMucLucQuyetToanModel = sender as DmMucLucQuyetToanModel;
            if (args.PropertyName == nameof(DmMucLucQuyetToanModel.IsDeleted))
            {
                DmMucLucQuyetToanModel_OnDeleteMLNS(DmMucLucQuyetToanModel, models);
            }
        }

        private void DmMucLucQuyetToanModel_OnDeleteMLNS(DmMucLucQuyetToanModel model, IEnumerable<DmMucLucQuyetToanModel> models)
        {
            var parent = models.FirstOrDefault(i => i.SMa == model.SMaCha);
            if (parent is null)
            {
                return;
            }
            var hasChild = models.Any(i => i.SMaCha == model.SMaCha && !i.IsDeleted);
            parent.IsHangCha = hasChild;
        }

        public override void InitDialog(PropertyInfo property)
        {
            if (sourceVM.SelectedItem.IsHangCha) return;
            if (property.Name.Equals(nameof(DmMucLucQuyetToanModel.Mlns)))
            {
                var mucLucNganSachService = sourceVM._serviceProvider.GetService(typeof(IMucLucNganSachService));
                //var factory = sourceVM._serviceProvider.GetService(typeof(ApplicationDbContextFactory)) as ApplicationDbContextFactory;
                //using var context = factory.CreateDbContext();
                //var listMapExist = context.Set<NsMucLucQuyetToanNamMLNS>().Where(x => x.NamLamViec == sourceVM._sessionService.Current.YearOfWork).Select(x => x.XauNoiMa).ToList();
                var listMapExist = sourceVM.Items.Where(x => x.Id != sourceVM.SelectedItem.Id).SelectMany(x => x.NsMucLucQuyetToanNamMLNS).Select(x => x.XauNoiMa).ToList();
                var dialogVM = new GenericControlCustomViewModel<NsMuclucNgansachModel, NsMucLucNganSach, MucLucNganSachService>
                    ((MucLucNganSachService)mucLucNganSachService, sourceVM._mapper, sourceVM._sessionService, sourceVM._serviceProvider);
                dialogVM._authenticationInfo.OptionalParam = new object[] { DialogType.LoadMLNSOfMLQTNam, property.Name, listMapExist.Distinct().ToList() };
                dialogVM.IsDialog = true;
                dialogVM.IsVisibleFilterByMlnsMappingType = true;
                dialogVM.Description = "Danh mục MLNS";
                dialogVM.Title = "Danh mục MLNS";
                var genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(dialogVM);
                dialogVM.IsMultipleSelect = true;
                SetSelectedMLNS(dialogVM);
                var GenericControlCustomWindow = new GenericControlCustomWindow
                {
                    DataContext = genericControlCustomWindowViewModel
                };
                GenericControlCustomWindow.SavedAction = obj =>
                {
                    var data = obj as IEnumerable<NsMuclucNgansachModel>;
                    // chỉ lưu mlns có bhangchadutoan = 0 (false)
                    sourceVM.SelectedItem.Mlns = string.Join("; ", data.Where(t => t.BHangChaDuToan.HasValue && !t.BHangChaDuToan.Value).Select(t => t.XauNoiMa));
                    var sktMucLucMaps = data.Where(t => t.BHangChaDuToan.HasValue && !t.BHangChaDuToan.Value).Select(model => new NsMucLucQuyetToanNamMLNS { XauNoiMa = model.XauNoiMa, MaMLQT = sourceVM.SelectedItem.SMa }).ToList();
                    sourceVM.SelectedItem.NsMucLucQuyetToanNamMLNS = sktMucLucMaps;
                    GenericControlCustomWindow.Close();
                };
                dialogVM.GenericControlCustomWindow = GenericControlCustomWindow;
                GenericControlCustomWindow.Show();
            }
        }

        public override void CustomValueProps(DmMucLucQuyetToanModel newRow, DmMucLucQuyetToanModel currentRow)
        {
            base.CustomValueProps(newRow, currentRow);
            newRow.NsMucLucQuyetToanNamMLNS = new List<NsMucLucQuyetToanNamMLNS>();
            newRow.Mlns = null;
        }

        private void SetSelectedMLNS(GenericControlCustomViewModel<NsMuclucNgansachModel, NsMucLucNganSach, MucLucNganSachService> dialogVM)
        {
            dialogVM.AfterSelectAll = true;
            var nsXauNoima = sourceVM.SelectedItem.NsMucLucQuyetToanNamMLNS.Select(t => t.XauNoiMa);
            var allXNm = String.Join(";", nsXauNoima);
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

        private bool FilterFunction(DmMucLucQuyetToanModel filterModel, DmMucLucQuyetToanModel item)
        {
            var result = true;
            if (!string.IsNullOrEmpty(filterModel.SMa))
                result = result && item.SMa.ToLower().Equals(filterModel.SMa.ToLower());
            if (!string.IsNullOrEmpty(filterModel.SMaCha))
                result = result && item.SMaCha.ToLower().Equals(filterModel.SMaCha.ToLower());
            return result;
        }


        public override bool ItemsViewFilter(object obj)
        {
            if (sourceVM._isFirstLoad)
            {
                return true;
            }
            var result = true;
            var item = (DmMucLucQuyetToanModel)obj;
            result = FilterFunction(sourceVM.FilterModel, item);
            return result;
        }

        public override void OnDelete(object obj)
        {
            var item = sourceVM.SelectedItem;
            var sktMucLucService = sourceVM._service;
            base.OnDelete(obj);
        }

        private readonly List<string> mlnsType = new List<string>
        {
            "TNG3", "TNG2", "TNG1", "TNG", "NG"
        };

        private string getTypeOfMlns(NsMuclucNgansachModel nsMuclucNgansachModel)
        {
            foreach (var type in mlnsType)
            {
                var propertyInfo = typeof(NsMuclucNgansachModel).GetProperty(type);
                var val = propertyInfo.GetValue(nsMuclucNgansachModel, null);
                if (val != null && !string.IsNullOrWhiteSpace(val.ToString()))
                {
                    return type;
                }
            }
            return "";
        }


    }
}
