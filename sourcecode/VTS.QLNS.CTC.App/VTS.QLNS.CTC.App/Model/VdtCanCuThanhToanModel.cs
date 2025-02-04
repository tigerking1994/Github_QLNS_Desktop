using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtCanCuThanhToanModel : BindableBase
    {
        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }
        public Guid Id { get; set; }
        public string STenDuAn { get; set; }
        public string SSoHopDong { get; set; }
        public DateTime? DNgayPheDuyet { get; set; }
        public double FGiaTriThanhToan { get; set; }
        public double FGiaTriThuHoi { get; set; }
        public Guid? IIdThongTriThanhToanId { get; set; }
    }
}
