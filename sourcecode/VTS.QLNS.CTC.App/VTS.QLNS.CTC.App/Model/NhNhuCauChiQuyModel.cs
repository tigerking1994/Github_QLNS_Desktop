using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhNhuCauChiQuyModel : ModelBase
    {
        public Guid? IIdParentId { get; set; }
        public Guid? IIdGocId { get; set; }
        [ValidateAttribute("Số đề nghị", Utility.Enum.DATA_TYPE.String, 100, true)]
        public string SSoDeNghi { get; set; }
        [ValidateAttribute("Ngày đề nghị", Utility.Enum.DATA_TYPE.Date, true)]
        public DateTime? DNgayDeNghi { get; set; }

        [ValidateAttribute("Năm kế hoạch", Utility.Enum.DATA_TYPE.Int, "2022")]
        public int? INamKeHoach { get; set; }
        [ValidateAttribute("Quý", Utility.Enum.DATA_TYPE.Int, 1, true)]
        public int? IQuy { get; set; }
        [ValidateAttribute("Đơn vị", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdDonViId { get; set; }
        //[ValidateAttribute("Mã đơn vị", Utility.Enum.DATA_TYPE.String, 50, true)]
        public string IIdMaDonVi { get; set; }
        [ValidateAttribute("Nguồn vốn", Utility.Enum.DATA_TYPE.Int, true)]
        public int? IIdNguonVonId { get; set; }
        [ValidateAttribute("Người lập", Utility.Enum.DATA_TYPE.String, 255)]
        public string SNguoiLap { get; set; }
        public Guid? IIdTiGiaId { get; set; }
        public string SMaNgoaiTeKhac { get; set; }
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
        public string STenDonVi { get; set; }
        public string STenNguonVon { get; set; }
        public int TotalFiles { get; set; }
        public string STongHop { get; set; }
        [ValidateAttribute("Kế hoạch tổng thể", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdKHTongTheID { get; set; }

        private ObservableCollection<NhNhuCauChiQuyChiTietModel> _nhNhuCauChiQuyChiTiets = new ObservableCollection<NhNhuCauChiQuyChiTietModel>();
        public ObservableCollection<NhNhuCauChiQuyChiTietModel> NhNhuCauChiQuyChiTiets
        {
            get => _nhNhuCauChiQuyChiTiets;
            set => SetProperty(ref _nhNhuCauChiQuyChiTiets, value);
        }
        private bool _isShowChildren;
        public bool IsShowChildren
        {
            get => _isShowChildren;
            set => SetProperty(ref _isShowChildren, value);
        }
        public bool HasChildren { get; internal set; }
        public HashSet<Guid> AncestorIds { get; internal set; }
    }
}
