using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhDaGoiThauDetailQuery
    {
        public Guid IIdGoiThauId { get; set; }
        public Guid? IIdkeHoachTongTheId { get; set; }
        public Guid? IIdDonViThuHuongId { get; set; }
        public string SMaNhiemVuChi { get; set; }
        public string STenNhiemVuchi { get; set; }
        public Guid? IdCacQuyetDinh { get; set; }
        public Guid? IIdKeHoachTongTheNHiemVuChiId { get; set; }
        public string SoQuyetDinh { get; set; }
        public Guid? IIdNhiemVuChiId { get; set; }
        public int? ILoaiQuyetDinh { get; set; }
    }
}
