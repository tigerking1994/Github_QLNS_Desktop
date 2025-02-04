using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class HTQuyenModel : ModelBase
    {
        private string _iidMaQuyen;
        [DisplayName("Mã")]
        public string IIDMaQuyen 
        {
            get => _iidMaQuyen;
            set => SetProperty(ref _iidMaQuyen, value);
        }

        private string _sTenQuyen;
        [DisplayName("Tên")]
        public string STenQuyen
        {
            get => _sTenQuyen;
            set => SetProperty(ref _sTenQuyen, value);
        }

        public virtual ICollection<HTNhomNguoiDungQuyenModel> SysGroupAuthorityModels { get; set; }
        public virtual ICollection<HTChucNangModel> SysFunctionModels { get; set; }

        private string _sysFunctionName;
        [DisplayName("Chức năng (F6)")]
        [DisplayDetailInfo("Chức năng")]
        [TypeOfDialogAttribute(typeof(HTChucNangModel), typeof(HtChucNang), typeof(SysFunctionService), typeof(ISysFunctionService))]
        [MapperMethodAttribute("MapFunctionToSysAuthorityFunction")]
        [InitSelectedItemsMethodAttribute("SetSelecteFunctionOfSysAuthority")]
        [ColumnTypeAttribute(ColumnType.ReferencePopup)]
        [IsAllowMultipleSelectAttribute(true)]
        public string SysFunctionName
        {
            get => _sysFunctionName;
            set => SetProperty(ref _sysFunctionName, value);
        }

        private string _authorityTypeId;
        [DisplayName("Loại quyền")]
        [ColumnTypeAttribute(ColumnType.Combobox, "LoadAuthorityType")]
        public string AuthorityTypeId 
        {
            get => _authorityTypeId;
            set => SetProperty(ref _authorityTypeId, value);
        }

        [JsonIgnore]
        public Guid IIdQuyen { get; set; }

        public HTQuyenModel()
        {
            SysGroupAuthorityModels = new HashSet<HTNhomNguoiDungQuyenModel>();
            SysFunctionModels = new HashSet<HTChucNangModel>();
        }
    }
}
