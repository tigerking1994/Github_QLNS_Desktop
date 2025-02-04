using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model.Import
{
    [SheetAttribute(3, "Danh mục đơn vị", 4, 0)]
    public class TlDmDonViNq104ImportModel : BindableBase
    {
        private bool _importStatus;
        public bool ImportStatus
        {
            get => _importStatus;
            set => SetProperty(ref _importStatus, value);
        }

        private bool _isErrorMLNS;
        public bool IsErrorMLNS
        {
            get => _isErrorMLNS;
            set => SetProperty(ref _isErrorMLNS, value);
        }

        private bool _isWarning;
        public bool IsWarning
        {
            get => _isWarning;
            set => SetProperty(ref _isWarning, value);
        }
        [ColumnAttribute("Mã đơn vị", 0)]
        public string MaDonVi { get; set; }
        [ColumnAttribute("Tên đơn vị", 1)]
        public string TenDonVi { get; set; }
        [ColumnAttribute("Mã đơn vị cha", 2)]
        public string ParentId { get; set; }
        [ColumnAttribute("Xâu nối mã", 3)]
        public string XauNoiMa { get; set; }
    }
}
