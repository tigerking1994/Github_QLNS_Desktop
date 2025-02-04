using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class CauHinhMLNSChiTieuLuongModelControlService : GenericControlBaseService<CauHinhMLNSChiTieuLuongModel, Core.Domain.NsMucLucNganSach, NsMucLucNganSachService>
    {
        public override void CustomDataValue()
        {
            foreach (var item in sourceVM.Items)
            {
                item.IsUnableEditBQuanlyChiTietToi = !this.IsEditableMlnsBQuanLyAndChiTietToi(item);
            }
        }

        public override void OnPropertyChanged()
        {
            foreach (var t in sourceVM.Items)
            {
                t.PropertyChanged += CauHinhMLNSChiTieuLuongModel_PropertyChanged;
            }
        }

        public override void OnAddChild(object obj)
        {
            if (sourceVM.SelectedItem == null)
                return;
            NsMucLucNganSachService mucLucNganSachService = sourceVM._service;
            DataGrid dgdData = obj as DataGrid;
            this.CancelEditData(dgdData);

            int currentRow = sourceVM.Items.IndexOf(sourceVM.SelectedItem);
            CauHinhMLNSChiTieuLuongModel parent = sourceVM.SelectedItem;
            if (mucLucNganSachService.IsUsedMLNS(parent.MlnsId, sourceVM._authenticationInfo.YearOfWork))
            {
                MessageBox.Show(Resources.UnableToAddChildMLNS, Resources.Alert, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            CauHinhMLNSChiTieuLuongModel newRow = ObjectCopier.Clone(sourceVM.SelectedItem);
            newRow.Id = Guid.Empty;
            newRow.MlnsId = Guid.NewGuid();
            newRow.MlnsIdParent = parent.MlnsId;
            newRow.IsModified = true;
            newRow.BHangCha = false;
            parent.BHangCha = true;
            newRow.PropertyChanged += Item_PropertyChanged;
            newRow.PropertyChanged += CauHinhMLNSChiTieuLuongModel_PropertyChanged;
            sourceVM.Items.Insert(currentRow + 1, newRow);
            sourceVM.InvokePropertyChange("Items");
            var cell = new DataGridCellInfo(sourceVM.Items[currentRow + 1], dgdData.Columns[0]);
            dgdData.ScrollIntoView(sourceVM.Items[currentRow + 1], dgdData.Columns[0]);
            dgdData.CurrentCell = cell;
            dgdData.BeginEdit();
        }

        public override void OnPropertyChanged(CauHinhMLNSChiTieuLuongModel model)
        {
            model.PropertyChanged += CauHinhMLNSChiTieuLuongModel_PropertyChanged;
        }

        public override void CustomValueProps(CauHinhMLNSChiTieuLuongModel newRow, CauHinhMLNSChiTieuLuongModel currentRow)
        {
            base.CustomValueProps(newRow, currentRow);
            newRow.MlnsId = Guid.NewGuid();
        }

        private void CauHinhMLNSChiTieuLuongModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            IEnumerable<CauHinhMLNSChiTieuLuongModel> models = sourceVM.Items;
            CauHinhMLNSChiTieuLuongModel CauHinhMLNSChiTieuLuongModel = sender as CauHinhMLNSChiTieuLuongModel;
            if (args.PropertyName == nameof(CauHinhMLNSChiTieuLuongModel.IsDeleted))
            {
                CauHinhMLNSChiTieuLuongModel_OnDeleteMLNS(CauHinhMLNSChiTieuLuongModel, models);
            }
            CauHinhMLNSChiTieuLuongModel.IsUnableEditBQuanlyChiTietToi = !this.IsEditableMlnsBQuanLyAndChiTietToi(CauHinhMLNSChiTieuLuongModel);
        }

        private void CauHinhMLNSChiTieuLuongModel_OnDeleteMLNS(CauHinhMLNSChiTieuLuongModel model, IEnumerable<CauHinhMLNSChiTieuLuongModel> models)
        {
            CauHinhMLNSChiTieuLuongModel parent = models.FirstOrDefault(i => i.MlnsId == model.MlnsIdParent);
            if (parent == null)
            {
                return;
            }
            bool hasChild = models.Any(i => i.MlnsIdParent == parent.MlnsId && !i.IsDeleted);
            parent.BHangCha = hasChild;

        }

        private bool IsEditableMlnsBQuanLyAndChiTietToi(CauHinhMLNSChiTieuLuongModel CauHinhMLNSChiTieuLuongModel)
        {
            // Nếu không phải lns thì ko dc thêm b quản lý
            if (string.IsNullOrEmpty(CauHinhMLNSChiTieuLuongModel.Lns))
            {
                return false;
            }
            if (!StringUtils.IsNullOrEmpty(CauHinhMLNSChiTieuLuongModel.L) ||
                !StringUtils.IsNullOrEmpty(CauHinhMLNSChiTieuLuongModel.K) ||
                !StringUtils.IsNullOrEmpty(CauHinhMLNSChiTieuLuongModel.M) ||
                !StringUtils.IsNullOrEmpty(CauHinhMLNSChiTieuLuongModel.TM) ||
                !StringUtils.IsNullOrEmpty(CauHinhMLNSChiTieuLuongModel.TTM) ||
                !StringUtils.IsNullOrEmpty(CauHinhMLNSChiTieuLuongModel.NG) ||
                !StringUtils.IsNullOrEmpty(CauHinhMLNSChiTieuLuongModel.TNG))
            {
                return false;
            }
            // Nếu có dòng con là lns thì ko dc thêm
            IEnumerable<CauHinhMLNSChiTieuLuongModel> children = sourceVM.Items.Where(p => p.MlnsIdParent.Equals(CauHinhMLNSChiTieuLuongModel.MlnsId));
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
            if (property.Name.Equals(nameof(CauHinhMLNSChiTieuLuongModel.ITrangThai)))
            {
                return new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "Không sử dụng", ValueItem = "0" },
                    new ComboboxItem { DisplayItem = "Đang sử dụng", ValueItem = "1" }
                };
            }
            return new ObservableCollection<ComboboxItem>();
        }
    }
}
