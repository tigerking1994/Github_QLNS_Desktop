using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
	public class PhanBoVonDonViQuery
    {
		public Guid Id { get; set; }
		public Guid? IIdDuAn { get; set; }
		public string SMaDuAn { get; set; }
		public double? FTongMucDauTuDuocDuyet { get; set; }
		public double? FLuyKeVonNamTruoc { get; set; }
		public double? FVonKeoDaiCacNamTruoc { get; set; }
		public double? FUocThucHien { get; set; }
		public double? FThuHoiVonUngTruoc { get; set; }
		public double? FThanhToan { get; set; }
		public string STrangThaiDuAnDangKy { get; set; }
		public Guid? IdParent { get; set; }
		public int? ILoaiDuAn { get; set; }
		public Guid? IdLoaiCongTrinh { get; set; }
        public Guid? iID_DuAn_HangMucID { get; set; }
    }
}
