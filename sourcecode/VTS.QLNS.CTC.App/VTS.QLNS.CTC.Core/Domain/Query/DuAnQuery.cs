using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
	public class DuAnQuery
    {
		public Guid? Id { get; set; }
		public double? FHanMucDauTu { get; set; }
		public Guid? IdDonViQuanLy { get; set; }
		public string STenDuAn { get; set; }
		public string SMaDuAn { get; set; }
		public Guid? IdReference { get; set; }
		public int? IndexDuAn { get; set; }
		public string SDiaDiem { get; set; }
		public string SKhoiCong { get; set; }
		public string SKetThuc { get; set; }
		public string SMaDonViQuanLy { get; set; }
		public Guid? IIdLoaiCongTrinhId { get; set; }
		public string STT { get; set; }
        public int? iID_NguonVonID { get; set; }
	}
}
