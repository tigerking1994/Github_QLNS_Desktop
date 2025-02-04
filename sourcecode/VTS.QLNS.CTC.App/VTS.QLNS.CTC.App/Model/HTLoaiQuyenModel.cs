using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class HTLoaiQuyenModel : BindableBase
    {
        public Guid ID { get; set; }
        public string STenLoaiQuyen { get; set; }
        public virtual ICollection<HTQuyenModel> HTQuyenModels { get; set; }

        public bool? IsSelected
        {
            get
            {
                var selected = HTQuyenModels.Select(item => item.IsSelected).Distinct().ToList();
                return selected.Count == 1 ? selected.Single() : (bool?)null;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, HTQuyenModels);
                    //OnPropertyChanged();
                }
            }
        }

        public string SelectedCount
        {
            get
            {
                return new StringBuilder().AppendFormat(NSLabel.SELECTED_COUNT_AUTHORITIES_STR, HTQuyenModels.Where(x => x.IsSelected).ToList().Count.ToString(), HTQuyenModels.Count().ToString()).ToString();
            }
        }

        public HTLoaiQuyenModel()
        {
            HTQuyenModels = new HashSet<HTQuyenModel>();
        }

        public void OnSelectChange()
        {
            OnPropertyChanged("IsSelected");
            OnPropertyChanged("SelectedCount");
        }

        private void SelectAll(bool select, IEnumerable<HTQuyenModel> models)
        {
            foreach (var model in models)
            {
                model.IsSelected = select;
            }
        }
    }
}
