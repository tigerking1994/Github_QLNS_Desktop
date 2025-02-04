using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaQuyetDinhKhacModel : ModelBase
    {

        [ValidateAttribute("Đơn vị quản lý", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdDonViQuanLyId { get; set; }
        [ValidateAttribute("Mã đơn vị quản lý", Utility.Enum.DATA_TYPE.String, 50, false)]
        public string IIdMaDonViQuanLy { get; set; }
        [ValidateAttribute("Số quyết định", Utility.Enum.DATA_TYPE.String, 100, true)]
        public string SSoQuyetDinh { get; set; }     
        [ValidateAttribute("Tên quyết định", Utility.Enum.DATA_TYPE.String, 300, true)]
        public string STenQuyetDinh { get; set; }
        [ValidateAttribute("Ngày quyết định", Utility.Enum.DATA_TYPE.Date, true)]
        public DateTime? DNgayQuyetDinh { get; set; }
        [ValidateAttribute("Mô tả", Utility.Enum.DATA_TYPE.String, 4000, false)]
        public Guid? IIdKHTTNhiemVuChiId { get; set; }
        public Guid? IIdKHTongTheID { get; set; }
        public string SMoTa { get; set; }
        public string STenChuongTrinh { get; set; }
        public string STenDonVi { get; set; }
        public double? FGiaTriUsd { get; set; }
        public double? FGiaTriVnd { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public string SNguoiXoa { get; set; }
        public bool? BIsActive { get; set; }
        public bool? BIsGoc { get; set; }
        public bool BIsKhoa { get; set; }
        public int? ILanDieuChinh { get; set; }
        public string SDieuChinhTu { get; set; }
        public bool BIsXoa { get; set; }
        public bool BHangCha { get; set; }
        public string Title { get; set; }
        public string STenQuyetDinhDisplay
        {
            get
            {
                return $"{this.SSoQuyetDinh} - {this.STenQuyetDinh}";
            }
        }

        public Guid? IIdParentId { get; set; }
        [NotMapped]
        public List<NhDaQuyetDinhKhacChiPhiModel> ListDataDetail = new List<NhDaQuyetDinhKhacChiPhiModel>();
    }
}
