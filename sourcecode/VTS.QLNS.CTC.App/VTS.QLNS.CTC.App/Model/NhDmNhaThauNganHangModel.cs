using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDmNhaThauNganHangModel : ModelBase
    {
        public Guid? IIdNhaThauId { get; set; }
        public string SMaNganHang { get; set; }
        public string STenNganHang { get; set; }
        public string SSoTaiKhoan { get; set; }
    }
}
