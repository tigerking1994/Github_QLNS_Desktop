using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtThongTriQuery
    {
        public Guid? Id { get; set; }
        public string sMaThongTri { get; set; }
        public DateTime? dNgayThongTri { get; set; }
        public DateTime? dNgayLapGanNhat { get; set; }
        public int iNamThongTri { get; set; }
        public string sNguoiLap { get; set; }
        public string sTruongPhong { get; set; }
        public string sThuTruongDonVi { get; set; }
        public string sMaNguonVon { get; set; }
        public Guid? iID_DonViID { get; set; }
        public string iID_MaDonViID { get; set; }
        public string sTenDonVi { get; set; }
        public string sUserCreate { get; set; }
        public DateTime? dDateCreate { get; set; }
        public string sUserUpdate { get; set; }
        public DateTime? dDateUpdate { get; set; }
        public string sUserDelete { get; set; }
        public DateTime? dDateDelete { get; set; }
        public string sMaLoaiCongTrinh { get; set; }
        public Guid? iID_LoaiThongTriID { get; set; }
        public Guid? iID_NhomQuanLyID { get; set; }
        public bool? bIsCanBoDuyet { get; set; }
        public string sTenLoaiCongTrinh { get; set; }
        public int ILoaiThongTri { get; set; }
        public string sMoTa { get; set; }
        public string sIsCanBoDuyet
        {
            get
            {
                return bIsCanBoDuyet ?? false ? ApproveTypeEnum.GetApproveTypeName((int)ApproveTypeEnum.Type.DA_DUYET) : ApproveTypeEnum.GetApproveTypeName((int)ApproveTypeEnum.Type.CHUA_DUYET);
            }
        }
        public bool? bIsDuyet { get; set; }
        public string sIsDuyet
        {
            get
            {
                return bIsDuyet ?? false ? ApproveTypeEnum.GetApproveTypeName((int)ApproveTypeEnum.Type.DA_DUYET) : ApproveTypeEnum.GetApproveTypeName((int)ApproveTypeEnum.Type.CHUA_DUYET);
            }
        }
        public bool? bThanhToan { get; set; }
        public int INamNganSach { get; set; }
        public Guid? IIdBcQuyetToanNienDo { get; set; }
    }
}
