using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhCacQuyetDinhNhiemVuChiQuery
    {
        public Guid IdCacQuyetDinh { get; set;}
        public Guid IdNhiemVuChi { get; set; }
        public string SSoQuyetDinh { get; set; }
        public int ILoaiQuyetDinh { get; set; }
        public string STenNhiemVuChi { get; set; }
    }
}
