using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhQtThongTriQuyetToanChiTietModel : ModelBase
    {
        public virtual Guid? iID_ThongTriQuyetToanID { get; set; }
        public virtual Guid? iID_DuAnID { get; set; }
        public virtual Guid? iID_HopDongID { get; set; }
        public virtual Guid? iID_ThanhToan_ChiTietID { get; set; }
        public virtual double? fDeNghiQuyetToanNam_VND { get; set; }
        public virtual double? fDeNghiQuyetToanNam_USD { get; set; }
        public virtual double? fThuaNopTraNSNN_VND { get; set; }
        public virtual double? fThuaNopTraNSNN_USD { get; set; }
        public virtual string sMaThuTu { get; set; }
        public virtual string sTenNoiDungChi { get; set; }

        public virtual string sTenDuAn { get; set; }
        public virtual string sTenHopDong { get; set; }
        public virtual string sTenNhiemVuChi { get; set; }
        public virtual string sM { get; set; }
        public virtual string sTM { get; set; }
        public virtual string sTTM { get; set; }
        public virtual string sNG { get; set; }
        public virtual Guid? iID_KHTT_NhiemVuChiID { get; set; }
        public virtual Guid? iID_MucLucNganSachID { get; set; }
        public virtual Guid? iID_ThanhToanID { get; set; }
        public virtual int? iLoaiNoiDungChi { get; set; }
        public virtual Guid? iID_ParentID { get; set; }
        public bool bIsData { get; set; }
    }
}
