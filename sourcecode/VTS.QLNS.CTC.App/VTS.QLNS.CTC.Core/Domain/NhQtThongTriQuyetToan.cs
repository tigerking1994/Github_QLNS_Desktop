using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
    public class NhQtThongTriQuyetToan : EntityBase
    {
        [MaxLength(100)]
        public virtual string sSoThongTri { get; set; }
        public virtual DateTime? dNgayLap { get; set; }
        public virtual Guid? iID_KHTT_NhiemVuChiID { get; set; }
        public virtual Guid? iID_DonViID { get; set; }
        [MaxLength(50)]
        public virtual string iID_MaDonVi { get; set; }
        public virtual int? iNamThongTri { get; set; }
        public virtual int? iLoaiThongTri { get; set; }
        public virtual int? iLoaiNoiDungChi { get; set; }
        public virtual double? fThongTri_VND { get; set; }
        public virtual double? fThongTri_USD { get; set; }
    }
}
