using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class HTChucNangModel : ModelBase
    {
        public Guid? IIDChucNangCha { get; set; }
        [DisplayName("Mã chức năng")]
        [DisplayDetailInfo("Mã chức năng")]
        public string IIDMaChucNang { get; set; }
        [DisplayName("Tên chức năng")]
        [DisplayDetailInfo("Tên chức năng")]
        public string STenChucNang { get; set; }
        [JsonIgnore]
        public ICollection<HTQuyenModel> HTQuyens { get; set; }

        private string _sysAuthoritiesName;
        public string SysAuthoritiesName
        {
            get => _sysAuthoritiesName;
            set => SetProperty(ref _sysAuthoritiesName, value);
        }

        public string OldName { get; set; }
        public string OldFuncCode { get; set; }
        public string SSTT { get; set; }
        public bool ITrangThai { get; set; }

        private bool _bHangCha;
        public bool BHangCha
        {
            get => _bHangCha;
            set
            {
                SetProperty(ref _bHangCha, value);
                OnPropertyChanged(nameof(IsHangCha));
            }
        }

        public int Level
        {
            get
            {
                if (SSTT.EndsWith("00-00-00-00"))
                {
                    return 1;
                }
                else if (SSTT.EndsWith("00-00-00"))
                {
                    return 2;
                }
                else if (SSTT.EndsWith("00-00"))
                {
                    return 3;
                }
                return 4;
            }
        }

        public bool IsEnableCheckbox => Level >= 3;

        public bool IsDisableCheckbox => !IsEnableCheckbox;

        public bool IsHangCha => BHangCha;

        public HTChucNangModel()
        {
            HTQuyens = new HashSet<HTQuyenModel>();
        }
    }
}
