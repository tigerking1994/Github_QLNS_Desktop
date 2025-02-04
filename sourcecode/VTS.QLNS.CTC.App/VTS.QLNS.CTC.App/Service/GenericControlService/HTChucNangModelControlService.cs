using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.Core.Service.Impl;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class HTChucNangModelControlService : GenericControlBaseService<HTChucNangModel, Core.Domain.HtChucNang, SysFunctionService>
    {
        private bool _isFirstUpdateSelectedChucNang = true;
        public override void OnPropertyChanged(HTChucNangModel model)
        {
            model.PropertyChanged += HtChucnangModel_PropertyChanged;
        }

        private void HtChucnangModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            IEnumerable<HTChucNangModel> models = sourceVM.Items;
            HTChucNangModel hTChucNangModel = sender as HTChucNangModel;
            if (args.PropertyName == nameof(hTChucNangModel.IsSelected))
            {
                // biến _isFirstUpdateSelectedChucNang dùng để tránh việc hàm propertyChanged bị recursive
                if (!_isFirstUpdateSelectedChucNang || sourceVM._isFirstLoad)
                {
                    return;
                }
                // update chuc nang con
                if (hTChucNangModel.Level == 3)
                {
                    _isFirstUpdateSelectedChucNang = false;
                    IEnumerable<HTChucNangModel> children = models.Where(t => hTChucNangModel.Id.Equals(t.IIDChucNangCha)).ToList();
                    foreach (HTChucNangModel child in children)
                    {
                        child.IsSelected = hTChucNangModel.IsSelected;
                    }
                    UpdateIsSelectedPropertyOfParentHtChucnangModel(hTChucNangModel, models);
                }
                // update chuc nag cha
                if (hTChucNangModel.Level == 4)
                {
                    _isFirstUpdateSelectedChucNang = false;
                    UpdateIsSelectedPropertyOfParentHtChucnangModel(hTChucNangModel, models);
                }
                _isFirstUpdateSelectedChucNang = true;
            }
        }

        private void UpdateIsSelectedPropertyOfParentHtChucnangModel(HTChucNangModel hTChucNangModel, IEnumerable<HTChucNangModel> models)
        {
            HTChucNangModel parent = models.FirstOrDefault(t => t.Id.Equals(hTChucNangModel.IIDChucNangCha));
            if (parent != null)
            {
                IEnumerable<HTChucNangModel> children = models.Where(t => parent.Id.Equals(t.IIDChucNangCha)).ToList();
                parent.IsSelected = children.Any(t => t.IsSelected);
                UpdateIsSelectedPropertyOfParentHtChucnangModel(parent, models);
            }
        }

        public override void OnPropertyChanged()
        {
            foreach (var t in sourceVM.Items)
            {
                t.PropertyChanged += HtChucnangModel_PropertyChanged;
            }
        }

        public override bool IsDisableColumn(PropertyInfo property)
        {
            return true;
        }
    }
}
