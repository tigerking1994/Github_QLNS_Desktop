using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtTtDeNghiThanhToanChiPhiModel : ModelBase
    {
        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }

        public Guid IIdChiPhiId { get; set; }
        public string STenChiPhi { get; set; }
        public double FGiaTriPheDuyetDuToan { get; set; }
        public double FGiaTriPheDuyetQdDauTu { get; set; }
    }
}
