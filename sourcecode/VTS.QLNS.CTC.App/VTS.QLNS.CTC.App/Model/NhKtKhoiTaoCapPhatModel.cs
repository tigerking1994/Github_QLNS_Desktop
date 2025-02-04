using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhKtKhoiTaoCapPhatModel : ModelBase
    {
        [Validate("Ngày khởi tạo", Utility.Enum.DATA_TYPE.Date, true)]
        public DateTime? DNgayKhoiTao { get; set; }
        [Validate("Năm khởi tạo", Utility.Enum.DATA_TYPE.Int, true)]
        public int? INamKhoiTao { get; set; }
        [Validate("Đơn vị", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdDonViID { get; set; }
        [Validate("Mã đơn vị", Utility.Enum.DATA_TYPE.String, true)]
        public string IIdMaDonVi { get; set; }
        [Validate("Tỉ giá", Utility.Enum.DATA_TYPE.Guid, true)]
        public Guid? IIdTiGiaID { get; set; }
        public string SMoTa { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayXoa { get; set; }
        public string SNguoiXoa { get; set; }
        public bool BIsKhoa { get; set; }
        public Guid? IIdTongHopID { get; set; }
        public string STongHop { get; set; }
        public bool BIsXoa { get; set; }

        public ObservableCollection<NhKtKhoiTaoCapPhatChiTietModel> NhKtKhoiTaoCapPhatChiTietModels { get; set; }
        // Another properties
        public string STenDonVi { get; set; }
    }
}
