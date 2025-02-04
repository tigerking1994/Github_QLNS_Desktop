using System;
using System.Collections.Generic;

namespace VTS.QLNS.CTC.App.Model
{
    public partial class NhQtQuyetToanNienDoModel : ModelBase
    {
        public Guid? IIdParentId { get; set; }
        public Guid? IIdGocId { get; set; }
        [ValidateAttribute("Số đề nghị", Utility.Enum.DATA_TYPE.String, 100, true)]
        public string SSoDeNghi { get; set; }
        [ValidateAttribute("Ngày đề nghị", Utility.Enum.DATA_TYPE.Date, true)]
        public DateTime? DNgayDeNghi { get; set; }
        [ValidateAttribute("Năm kế hoạch", Utility.Enum.DATA_TYPE.Int, 4, true)]
        public int? INamKeHoach { get; set; }
        public Guid? IIdDonViId { get; set; }
        [ValidateAttribute("Đơn vị", Utility.Enum.DATA_TYPE.String, 100,true)]
        public string IIdMaDonVi { get; set; }
        [ValidateAttribute("Nguồn vốn", Utility.Enum.DATA_TYPE.Int, true)]
        public int? IIdNguonVonId { get; set; }
        [ValidateAttribute("Loại thanh toán", Utility.Enum.DATA_TYPE.Int, true)]
        public int? ILoaiThanhToan { get; set; }
        [ValidateAttribute("Loại quyết toán", Utility.Enum.DATA_TYPE.Int, true)]
        public int? ILoaiQuyetToan { get; set; }
        [ValidateAttribute("Cơ quan thanh toán", Utility.Enum.DATA_TYPE.Int, true)]
        public int? ICoQuanThanhToan { get; set; }
        [ValidateAttribute("Tỉ giá", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdTiGiaId { get; set; }
        [ValidateAttribute("Mã ngoại tệ khác", Utility.Enum.DATA_TYPE.String, 100, true)]
        public string SMaNgoaiTeKhac { get; set; }
        public string SMoTa { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public string SNguoiXoa { get; set; }
        public bool BIsActive { get; set; }
        public bool BIsGoc { get; set; }
        public bool BIsKhoa { get; set; }
        public int? ILanDieuChinh { get; set; }
        public bool BIsXoa { get; set; }

        // Another properties
        public string STenDonVi { get; set; }
        public string STenNguonVon { get; set; }
        public string SLoaiThanhToan { get; set; }
        public string SLoaiQuyetToan { get; set; }
        public string SCoQuanThanhToan { get; set; }
        public virtual Guid? iID_TongHopID { get; set; }
        public virtual string sTongHopChildID { get; set; }
        public bool HasChildren { get; internal set; }
        private bool _isShowChildren;
        public bool IsShowChildren
        {
            get => _isShowChildren;
            set => SetProperty(ref _isShowChildren, value);
        }

        public HashSet<Guid> AncestorIds { get; internal set; }
    }
}
