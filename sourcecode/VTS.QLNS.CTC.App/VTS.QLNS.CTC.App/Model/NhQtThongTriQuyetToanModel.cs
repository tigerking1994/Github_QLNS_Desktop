using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhQtThongTriQuyetToanModel : ModelBase
    {
        [ValidateAttribute("Số thông tri", Utility.Enum.DATA_TYPE.String, 100, true)]
        public virtual string sSoThongTri { get; set; }
        [ValidateAttribute("Ngày lập", Utility.Enum.DATA_TYPE.Date)]
        public virtual DateTime? dNgayLap { get; set; }
        [ValidateAttribute("Tên chương trình", Utility.Enum.DATA_TYPE.Guid, true)]
        public virtual Guid? iID_KHTT_NhiemVuChiID { get; set; }
        [ValidateAttribute("Đơn vị", Utility.Enum.DATA_TYPE.Guid, true)]
        public virtual Guid? iID_DonViID { get; set; }
        public virtual string iID_MaDonVi { get; set; }
        [ValidateAttribute("Năm thông tri", Utility.Enum.DATA_TYPE.Int, true)]
        public virtual int? iNamThongTri { get; set; }
        public virtual int? iLoaiThongTri { get; set; }
        public virtual int? iLoaiNoiDungChi { get; set; }
        public virtual double? fThongTri_VND { get; set; }
        public virtual double? fThongTri_USD { get; set; }

        public virtual string sTenNhiemVuChi { get; set; }
        public virtual string sTenDonVi { get; set; }
        public virtual string sLoaiThongTri { get; set; }
        public virtual string sLoaiNoiDungChi { get; set; }
    }
}
