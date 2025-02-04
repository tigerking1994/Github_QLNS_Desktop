using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhQtPheDuyetQuyetToanDAHTModel : ModelBase
    {
        [ValidateAttribute("Số phê duyệt", Utility.Enum.DATA_TYPE.String, 100, true)]
        public string SSoPheDuyet { get; set; }
        [ValidateAttribute("Ngày phê duyệt", Utility.Enum.DATA_TYPE.Date, false)]
        public DateTime? DNgayPheDuyet { get; set; }
        [ValidateAttribute("Đơn vị", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdDonViId { get; set; }
      
        public string IIdMaDonVi { get; set; }
        [ValidateAttribute("Tỉ giá", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdTiGiaId { get; set; }
        [ValidateAttribute("Năm báo cáo từ", Utility.Enum.DATA_TYPE.Int,4, true)]
        public int? INamBaoCaoTu { get; set; }
        [ValidateAttribute("Năm báo cáo đến", Utility.Enum.DATA_TYPE.Int,4, true)]
        public int? INamBaoCaoDen { get; set; }
        [ValidateAttribute("Mô tả", Utility.Enum.DATA_TYPE.String, 4000, false)]
        public string SMoTa { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public string SNguoiXoa { get; set; }
        public bool BIsXoa { get; set; }
        // Another properties
        public string STenDonVi { get; set; }
        public string STenTiGia { get; set; }

    }

    public class FilterBaoCaoKetLuanQuyetToan
    {
        public DateTime? DNgayPheDuyetTu { get; set; }
        public DateTime? DNgayPheDuyetDen { get; set; }
        public Guid? IIdDonViId { get; set; }

    }
}
