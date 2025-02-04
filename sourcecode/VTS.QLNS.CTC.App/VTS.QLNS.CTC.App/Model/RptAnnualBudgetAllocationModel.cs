using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class RptAnnualBudgetAllocationModel
    {
        public int iStt { get; set; }
        public string iID_MaDonViQuanLy { get; set; }
        public string sTenDonVi { get; set; }
        public double? fCapPhatNamTruoc { get; set; }
        public double? fChiTieuNamTruoc { get; set; }
        public double? fQuyetToan { get; set; }
        public double? fThongBaoDauNam { get; set; }
        public double? fTongBoXung { get; set; }
        public double? fCapPhatNamNay { get; set; }
        public double? fTamUng { get; set; }
        public double fCong
        {
            get
            {
                return (fChiTieuNamTruoc ?? 0) + (fThongBaoDauNam ?? 0) + (fTongBoXung ?? 0);
            }
        }
        public double fSoGiaiNgan
        {
            get
            {
                return (fCapPhatNamNay ?? 0) + (fTamUng ?? 0);
            }
        }
        public string fTiLe
        {
            get
            {
                if (fCong == 0) return string.Empty;
                return string.Format("{0}%", Math.Round((fSoGiaiNgan / fCong), 2).ToString());
            }
        }
        public bool IsHangCha { get; set; }
    }
}
