using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtBaoCaoKetQuaGiaiNganChiKPDTQuery
    {
        public string STenDonVi { get; set; }
        public double FDuToanDeNghiChuyen { get; set; }             // col 3
        public double FNamNay { get; set; }                         // col 4
        public double FCongDuToan                                   // col 5
        {
            get
            {
                return FDuToanDeNghiChuyen + FNamNay;
            }
        }
        public double FDuToanDuocThongBao { get; set; }             // col 6
        public double FCucTaiChinhThanhToanTrucTiep { get; set; }   // col 7
        public double FThanhToanKLHTBQP { get; set; }               // col 8
        public double FTamUngBQP { get; set; }                      // col 9
        public double FCongBQP                                      // col 10
        {
            get
            {
                return FThanhToanKLHTBQP + FTamUngBQP;
            }
        }
        public double FDuToanDuocThongBaoKhoBac { get; set; }       // col 12
        public double FThanhToanKLHTKhoBac { get; set; }            // col 13
        public double FTamUngKhoBac { get; set; }                   // col 14
        public double FCongKhoBac                                   // col 15
        {
            get
            {
                return FThanhToanKLHTKhoBac + FTamUngKhoBac;
            }
        }
        public bool IsHangCha { get; set; }

        public string FTiLeBQP                                          // col 11
        {
            get
            {
                if (FDuToanDuocThongBao == 0) return string.Empty;
                return string.Format("{0}%", Math.Round((FCongBQP / FDuToanDuocThongBao), 2).ToString());
            }
        }
        public string FTiLeKhoBac                                       // col 16
        {
            get
            {
                if (FDuToanDuocThongBaoKhoBac == 0) return string.Empty;
                return string.Format("{0}%", Math.Round((FCongKhoBac / FDuToanDuocThongBaoKhoBac), 2).ToString());
            }
        }
    }
}
