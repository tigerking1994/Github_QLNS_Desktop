using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhQtChuyenQuyetToanChiTietModel : DetailModelBase
    {
		public Guid? iID_ChuyenQuyetToanID { get; set; }
		public Guid iID_MaMucLucNganSach { get; set; }
		public Guid? iID_MaMucLucNganSach_Cha { get; set; }
		public bool BHangCha { get; set; }
		public string sXauNoiMa { get; set; }
		public string sLNS { get; set; }
		public string sL { get; set; }
		public string sK { get; set; }
		public string sM { get; set; }
		public string sTM { get; set; }
		public string sTTM { get; set; }
		public string sNG { get; set; }
		public string sTNG { get; set; }
		public string sMoTa { get; set; }
		public string sChuong { get; set; } = "011";
		public int? iNamLamViec { get; set; }
		private double? _fGiaTriUSD;
		public double? fGiaTriUSD
		{
			get => _fGiaTriUSD;
			set => SetProperty(ref _fGiaTriUSD, value);
		}
	}
}
