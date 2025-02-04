using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class AllocationType : BindableBase
    {
        public AllocationType(){
            IsFirstLoad = true;
        } 

        private Guid _id;
        public Guid Id
        {
            get => _id;
            set
            {
                SetProperty(ref _id, value);
            }
        }

        private string _idCode;
        public string IdCode
        {
            get => _idCode;
            set
            {
                SetProperty(ref _idCode, value);
                if (!IsFirstLoad)
                {
                    IsChanged = true;
                    OnPropertyChanged("IsChanged");
                }
            }
        }

        private string _ten;
        public string Ten
        {
            get => _ten;
            set
            {
                SetProperty(ref _ten, value);
                if (!IsFirstLoad)
                { 
                    IsChanged = true;
                    OnPropertyChanged("IsChanged");
                } 
            }
        }

        private string _tenThongTriCap;
        public string TenThongTriCap
        {
            get => _tenThongTriCap;
            set
            {
                SetProperty(ref _tenThongTriCap, value);
                if (!IsFirstLoad)
                {
                    IsChanged = true;
                    OnPropertyChanged("IsChanged");
                }
            }
        }

        private string _tenThongTriThu;
        public string TenThongTriThu
        {
            get => _tenThongTriThu;
            set
            {
                SetProperty(ref _tenThongTriThu, value);
                if (!IsFirstLoad)
                {
                    IsChanged = true;
                    OnPropertyChanged("IsChanged");
                }
            }
        }

        private string _lns;
        public string Lns
        {
            get => _lns;
            set
            {
                SetProperty(ref _lns, value);
                if (!IsFirstLoad)
                {
                    IsChanged = true;
                    OnPropertyChanged("IsChanged");
                }
            }
        }

        private string _moTa;
        public string MoTa
        {
            get => _moTa;
            set
            {
                SetProperty(ref _moTa, value);
                if (!IsFirstLoad)
                {
                    IsChanged = true;
                    OnPropertyChanged("IsChanged");
                }
            }
        }

        private int? _orderIndex;
        public int? OrderIndex
        {
            get => _orderIndex;
            set
            {
                SetProperty(ref _orderIndex, value);
                if (!IsFirstLoad)
                {
                    IsChanged = true;
                    OnPropertyChanged("IsChanged");
                }
            }
        }

        private int? _namLamViec;
        public int? NamLamViec
        {
            get => _namLamViec;
            set
            {
                SetProperty(ref _namLamViec, value);
            }
        }

        private int _iTrangThai;
        public int ITrangThai
        {
            get => _iTrangThai;
            set
            {
                SetProperty(ref _iTrangThai, value);
            }
        }

        private DateTime? _dateCreated;
        public DateTime? DateCreated
        {
            get => _dateCreated;
            set
            {
                SetProperty(ref _dateCreated, value);
            }
        }

        private string _userCreator;
        public string UserCreator
        {
            get => _userCreator;
            set
            {
                SetProperty(ref _userCreator, value);
            }
        }

        private DateTime? _dateModified;
        public DateTime? DateModified
        {
            get => _dateModified;
            set
            {
                SetProperty(ref _dateModified, value);
            }
        }

        private string _userModifier;
        public string UserModifier
        {
            get => _userModifier;
            set
            {
                SetProperty(ref _userModifier, value);
            }
        }

        private string _tag;
        public string Tag
        {
            get => _tag;
            set
            {
                SetProperty(ref _tag, value);
            }
        }

        private string _log;
        public string Log
        {
            get => _log;
            set
            {
                SetProperty(ref _log, value);
            }
        }

        private bool _isChanged;
        public bool IsChanged
        {
            get => _isChanged;
            set
            {
                SetProperty(ref _isChanged, value);
            }
        }

        private bool _isNew;
        public bool IsNew
        {
            get => _isNew;
            set
            {
                SetProperty(ref _isNew, value);
            }
        }

        private bool _isDelete;
        public bool IsDelete
        {
            get => _isDelete;
            set
            {
                SetProperty(ref _isDelete, value);
            }
        }

        private bool _isFirstLoad;
        public bool IsFirstLoad
        {
            get => _isFirstLoad;
            set
            {
                SetProperty(ref _isFirstLoad, value);
            }
        } 
    }
}
