using ControlzEx.Standard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.View.Shared;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class DmCongKhaiTaiChinhControlService : GenericControlBaseService<DmCongKhaiTaiChinhModel, NsDanhMucCongKhai, DmCongKhaiTaiChinhService>
    {

        public override void CustomValueProps(DmCongKhaiTaiChinhModel newRow, DmCongKhaiTaiChinhModel currentRow)
        {
            base.CustomValueProps(newRow, currentRow);
            newRow.Id = Guid.NewGuid();
            if (currentRow != null)
            {
                newRow.SMa = GetMa(currentRow.SMa);
            }
            else
            {
                newRow.SMa = GetMa(string.Empty);
            }
            newRow.IsHangCha = false;
        }

        public override void InitDialog(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(DmCongKhaiTaiChinhModel.Mlns)))
            {
                if (sourceVM.SelectedItem != null && sourceVM.SelectedItem.IsHangCha)
                {
                    MessageBoxHelper.Error(Resources.MsgErrorNotAddMlnsInParent);
                    return;
                }
                var mlnsService = sourceVM._serviceProvider.GetService(typeof(IMlnsChildService));
                GenericControlCustomViewModel<NsMuclucNganSachChildModel, NsMucLucNganSach, MlnsChildService> dialogVM =
                    new GenericControlCustomViewModel<NsMuclucNganSachChildModel, NsMucLucNganSach, MlnsChildService>
                    ((MlnsChildService)mlnsService, sourceVM._mapper, sourceVM._sessionService, sourceVM._serviceProvider);

                dialogVM.IsDialog = true;
                dialogVM.Description = "Chọn MLNS";
                dialogVM.Title = "Chọn MLNS cho danh mục công khai: " + sourceVM.SelectedItem.sMoTa;
                dialogVM.IsPopup = true;
                // Taskbar
                dialogVM.IsVisibleFilterByMlnsMappingType = true;
                dialogVM.NotIns = GetNotIns();

                var genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(dialogVM);


                dialogVM.IsMultipleSelect = true;
                SetSelectedMLNS(dialogVM);
                GenericControlCustomWindow window = new GenericControlCustomWindow
                {
                    DataContext = genericControlCustomWindowViewModel
                };
                window.SavedAction = obj =>
                {
                    var data = (IEnumerable<NsMuclucNganSachChildModel>)obj;
                    sourceVM.SelectedItem.Mlns = string.Join(",", data.Select(c => c.XauNoiMa));
                    window.Close();
                };
                dialogVM.GenericControlCustomWindow = window;
                window.Show();
            }
        }

        private void SetSelectedMLNS(GenericControlCustomViewModel<NsMuclucNganSachChildModel, NsMucLucNganSach, MlnsChildService> dialogVM)
        {
            dialogVM.AfterSelectAll = true;
            IEnumerable<string> nsXauNoima = sourceVM.SelectedItem.Mlns.Split(',').ToList();
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

        private List<string> GetNotIns()
        {
            var data = string.Join(",", sourceVM.Items.Where(c => c.Id != sourceVM.SelectedItem.Id).Select(c => c.Mlns));
            return data.Split(',').ToList();
        }

        public override void OrderData()
        {
            var lstData = sourceVM.Items.ToList();
            List<DmCongKhaiTaiChinhModel> results = new List<DmCongKhaiTaiChinhModel>();
            foreach (var item in lstData.Where(n => !n.IIdDmCongKhaiCha.HasValue).OrderBy(n => n.SMa))
            {
                results.AddRange(ReciveData(item, lstData));
            }
            sourceVM.Items = new System.Collections.ObjectModel.ObservableCollection<DmCongKhaiTaiChinhModel>(results);
        }

        private List<DmCongKhaiTaiChinhModel> ReciveData(DmCongKhaiTaiChinhModel current, List<DmCongKhaiTaiChinhModel> lstData)
        {
            List<DmCongKhaiTaiChinhModel> results = new List<DmCongKhaiTaiChinhModel>();
            results.Add(current);
            foreach (var item in lstData.Where(n => n.IIdDmCongKhaiCha == current.Id).OrderBy(n => n.SMa))
            {
                results.AddRange(ReciveData(item, lstData));
            }
            return results;
        }

        public override void OnAddChild(object obj)
        {
            if (sourceVM.SelectedItem == null)
                return;
            DataGrid dgdData = obj as DataGrid;
            this.CancelEditData(dgdData);

            int currentRow = sourceVM.Items.IndexOf(sourceVM.SelectedItem);
            DmCongKhaiTaiChinhModel newRow = ObjectCopier.Clone(sourceVM.SelectedItem);
            newRow.Id = Guid.NewGuid();
            newRow.SMa = GetMa(sourceVM.SelectedItem.SMa, true);
            newRow.IIdDmCongKhaiCha = sourceVM.SelectedItem.Id;
            newRow.SMaCha = sourceVM.SelectedItem.SMa;
            if (!string.IsNullOrEmpty(sourceVM.SelectedItem.Mlns))
            {
                newRow.Mlns = sourceVM.SelectedItem.Mlns;
                sourceVM.SelectedItem.IsModified = true;
                sourceVM.SelectedItem.Mlns = string.Empty;
            }
            else
            {
                newRow.Mlns = string.Empty;
            }
            newRow.IsHangCha = false;
            newRow.IsModified = true;
            sourceVM.SelectedItem.IsHangCha = true;
            sourceVM.Items.Insert(currentRow + 1, newRow);
            var cell = new DataGridCellInfo(sourceVM.Items[currentRow + 1], dgdData.Columns[0]);
            dgdData.ScrollIntoView(sourceVM.Items[currentRow + 1], dgdData.Columns[0]);
            dgdData.CurrentCell = cell;
            dgdData.BeginEdit();
            sourceVM.InvokePropertyChange(nameof(sourceVM.Items));
        }

        private string GetMa(string currentCode, bool bIsChild = false)
        {
            if (string.IsNullOrEmpty(currentCode)) return "01";
            if (bIsChild)
            {
                if (sourceVM.Items.Any(n => n.SMaCha == currentCode))
                {
                    int iMax = sourceVM.Items.Where(n => !string.IsNullOrEmpty(n.SMa) && n.SMaCha == currentCode)
                        .Select(n => int.Parse(n.SMa.Substring(n.SMa.Length - 2)))
                        .Max(n => n);
                    return string.Format("{0}{1}", currentCode, (iMax + 1).ToString("00"));
                }
                else
                {
                    return string.Format("{0}01", currentCode);
                }
            }
            string sCode = (currentCode.Substring(0, (currentCode.Length - 2)));
            if (sourceVM.Items.Any(n => n.SMaCha == sCode))
            {
                int iMax = sourceVM.Items.Where(n => !string.IsNullOrEmpty(n.SMa) && n.SMaCha == sCode)
                    .Select(n => int.Parse(n.SMa.Substring(n.SMa.Length - 2)))
                    .Max(n=>n);
                return string.Format("{0}{1}", sCode, (iMax + 1).ToString("00"));
            }
            return "01";
        }
    }
}
