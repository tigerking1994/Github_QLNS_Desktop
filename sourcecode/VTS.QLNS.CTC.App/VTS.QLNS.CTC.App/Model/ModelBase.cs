using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VTS.QLNS.CTC.App.Service.Impl;

namespace VTS.QLNS.CTC.App.Model
{
    public class ModelBase : BindableBase
    {
        public virtual Guid Id { get; set; }

        private bool _isDeleted;
        public bool IsDeleted
        {
            get => _isDeleted;
            set => SetProperty(ref _isDeleted, value);
        }

        private bool _isModified;
        public bool IsModified
        {
            get => _isModified;
            set => SetProperty(ref _isModified, value);
        }

        private bool _isAdded;
        public bool IsAdded
        {
            get => _isAdded;
            set => SetProperty(ref _isAdded, value);
        }

        private bool _isFilter = true;
        public bool IsFilter
        {
            get => _isFilter;
            set => SetProperty(ref _isFilter, value);
        }

        private bool _isSelected;
        [DisplayName("")]
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        private bool _isHangCha;
        public virtual bool IsHangCha
        {
            get => _isHangCha;
            set => SetProperty(ref _isHangCha, value);
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }

        private int _iKieuChu;
        public int IKieuChu
        {
            get => _iKieuChu;
            set => SetProperty(ref _iKieuChu, value);
        }
        public virtual string DetailInfoModalTitle => "Chi tiết";
        public virtual bool IsEditable => !IsHangCha && !IsDeleted;

        public List<DataModel> LstDataModels { get; set; }
        public List<DataModel> LstDataTotalModels { get; set; }
        public double Total { get; set; }
    }
}
