using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class DanhMucNguonNganSachControlService : GenericControlBaseService<DanhMucNguonNganSachModel, DanhMuc, DanhMucNguonNganSachService>
    {
        public override bool ItemsViewFilter(object obj)
        {
            bool rs = base.ItemsViewFilter(obj);
            //DanhMucNguonNganSachModel danhMucNguonNganSachModel = obj as DanhMucNguonNganSachModel;
            //if (sourceVM.YearType == CoNamLamViec.HAS_YEAR.ToString())
            //{
            //    return rs && danhMucNguonNganSachModel.INamLamViec.HasValue;
            //}
            //else if (sourceVM.YearType == CoNamLamViec.HAS_NO_YEAR.ToString())
            //{
            //    return rs && !danhMucNguonNganSachModel.INamLamViec.HasValue;
            //}
            return rs;
        }

        public override void OnAdd(object obj)
        {
            var Items = sourceVM.Items;
            var SelectedItem = sourceVM.SelectedItem;
            if (!sourceVM.IsVisibleAddBtn)
            {
                return;
            }
            try
            {
                DataGrid dgdData = obj as DataGrid;
                this.CancelEditData(dgdData);

                int currentRow = Items.Count - 1;
                DanhMucNguonNganSachModel newRow;
                if (SelectedItem == null)
                {
                    newRow = new DanhMucNguonNganSachModel();
                    CustomValueProps(newRow, SelectedItem);
                    Items.Add(newRow);
                }
                else
                {
                    currentRow = Items.IndexOf(SelectedItem);
                    newRow = ObjectCopier.Clone(SelectedItem);
                    CustomValueProps(newRow, SelectedItem);
                    newRow.SMoTa = String.Empty;
                    Items.Insert(currentRow + 1, newRow);
                }
                if (sourceVM.YearType == CoNamLamViec.HAS_YEAR.ToString())
                {
                    newRow.INamLamViec = sourceVM._sessionService.Current.YearOfWork;
                }
                else if (sourceVM.YearType == CoNamLamViec.HAS_NO_YEAR.ToString())
                {
                    newRow.INamLamViec = null;
                }
                newRow.PropertyChanged += Item_PropertyChanged;
                OnPropertyChanged(newRow);
                sourceVM.InvokePropertyChange(nameof(Items));

                var cell = new DataGridCellInfo(Items[currentRow + 1], dgdData.Columns[0]);
                dgdData.ScrollIntoView(Items[currentRow + 1], dgdData.Columns[0]);
                dgdData.CurrentCell = cell;
                dgdData.BeginEdit();
            }
            catch (Exception ex)
            {
                sourceVM._logger.Error(ex.Message, ex);
            }
        }
        public override bool IsDisableColumn(PropertyInfo property)
        {
            return property.Name.Equals(nameof(DanhMucNguonNganSachModel.SMoTa));
        }
    }
}
