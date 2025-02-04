using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class HTNhomModel : BindableBase
    {
        public HTNhomModel()
        {
            SysUserModels = new HashSet<HTNguoiDungModel>();
            HTQuyenModels = new HashSet<HTQuyenModel>();
            BKichHoat = true;
        }

        public string STenNhom { get; set; }
        public bool BKichHoat { get; set; }
        public Guid Id { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public virtual ICollection<HTQuyenModel> HTQuyenModels { get; set; }
        public virtual ICollection<HTNguoiDungModel> SysUserModels { get; set; }

        public string HTQuyens => string.Join(",", HTQuyenModels.Select(q => q.STenQuyen));
    }
}
