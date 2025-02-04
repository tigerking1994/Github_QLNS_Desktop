using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhQtChungTuTaiSanModel:ModelBase
    {
        public Guid Id { get; set; }
        [ValidateAttribute("Tên chứng từ", Utility.Enum.DATA_TYPE.String, 300, true)]
        public string STenChungTu { get; set; }
        [ValidateAttribute("Số chứng từ", Utility.Enum.DATA_TYPE.String, 300, true)]
        public string SSoChungTu { get; set; }
        [ValidateAttribute("Ngày chứng từ", Utility.Enum.DATA_TYPE.Date, false)]
        public DateTime? DNgayChungTu { get; set; }
    }
}
