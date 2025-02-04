using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain
{
	public partial class NhQtChuyenQuyetToan : EntityBase
	{
		public string sSoChungTu { get; set; }
		public DateTime? dNgayChungTu { get; set; }
		public Guid? iID_DonViID { get; set; }
		public string iID_MaDonVi { get; set; }
		public int? iLoaiThoiGian { get; set; }
		public int? iThoiGian { get; set; }
		public string sMoTa { get; set; }
		public DateTime? dNgayTao { get; set; }
		public DateTime? dNgaySua { get; set; }
		public string sNguoiTao { get; set; }
		public string sNguoiSua { get; set; }
	}
}
