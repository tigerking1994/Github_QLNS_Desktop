using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhQtChuyenQuyetToanModel: ModelBase
    {
		public override Guid Id { get; set; }

		[Validate("Số chứng từ", Utility.Enum.DATA_TYPE.String, 50, true)]
		public string sSoChungTu { get; set; }

		[Validate("Ngày đề nghị", Utility.Enum.DATA_TYPE.Date)]
		public DateTime? dNgayChungTu { get; set; }

		[Validate("Đơn vị", Utility.Enum.DATA_TYPE.Guid, true)]
		public Guid? iID_DonViID { get; set; }

		[Validate("Loại thời gian", Utility.Enum.DATA_TYPE.Int, true)]
		public int? iLoaiThoiGian { get; set; }

		[Validate("Thời gian", Utility.Enum.DATA_TYPE.Int, true)]
		public int? iThoiGian { get; set; }

		public string iID_MaDonVi { get; set; }
		public string sMoTa { get; set; }
		public DateTime? dNgayTao { get; set; }
		public DateTime? dNgaySua { get; set; }
		public string sNguoiTao { get; set; }
		public string sNguoiSua { get; set; }

		// Another properties
		public string STenDonVi { get; set; }
		public string STenLoaiThoiGian { get; set; }
		public string STenThoiGian { get; set; }
	}
}
