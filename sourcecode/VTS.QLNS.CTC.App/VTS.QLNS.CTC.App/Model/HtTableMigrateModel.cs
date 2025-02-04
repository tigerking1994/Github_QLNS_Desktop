using System;

namespace VTS.QLNS.CTC.App.Model
{
    public class HtTableMigrateModel : ModelBase
    {
        private string _object;
        public string Object
        {
            get => _object;
            set => SetProperty(ref _object, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private int _sourceRowCount;
        public int SourceRowCount
        {
            get => _sourceRowCount;
            set => SetProperty(ref _sourceRowCount, value);
        }


        private int _destinationRowCount;
        public int DestinationRowCount
        {
            get => _destinationRowCount;
            set => SetProperty(ref _destinationRowCount, value);
        }

        private int _migrateFrequency;
        public int MigrateFrequency
        {
            get => _migrateFrequency;
            set => SetProperty(ref _migrateFrequency, value);
        }

        private bool _isMigrated;
        public bool IsMigrated
        {
            get => _isMigrated;
            set => SetProperty(ref _isMigrated, value);
        }

        private bool _isRemoveThenAdd;
        public bool IsRemoveThenAdd
        {
            get => _isRemoveThenAdd;
            set => SetProperty(ref _isRemoveThenAdd, value);
        }

        private bool _isAddNew;
        public bool IsAddNew
        {
            get => _isAddNew;
            set => SetProperty(ref _isAddNew, value);
        }


        private bool _isCategory;
        public bool IsCategory
        {
            get => _isCategory;
            set => SetProperty(ref _isCategory, value);
        }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public new int IKieuChu => IsMigrated ? 1 : 2;

    }
}
