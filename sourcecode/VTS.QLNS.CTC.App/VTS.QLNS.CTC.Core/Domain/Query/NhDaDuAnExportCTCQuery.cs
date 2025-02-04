using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhDaDuAnExportCTCQuery
    {
        public Guid Id { get; set; }
        private string SSTT { get; set; }
        public string SMaDuAn { get; set; }
        public string STenDuAn { get; set; }
        public string IIdMaChuDauTu { get; set; }
        public string SMaPhanCapPheDuyet { get; set; }
        public string SSoDauTu { get; set; }
        public string DNgayDauTu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public string DNgayQuyetDinh { get; set; }
        public string SKhoiCong { get; set; }
        public string SKetThuc { get; set; }
    }
}
