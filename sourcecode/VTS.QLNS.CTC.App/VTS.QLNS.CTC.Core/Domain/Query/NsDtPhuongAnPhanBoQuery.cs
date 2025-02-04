using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NsDtPhuongAnPhanBoQuery
    {
        public Guid? IIdMlns { get; set; }
        public Guid? IIdMlnsCha { get; set; }
        public string SL { get; set; }
        public string SK { get; set; }
        public string SM { get; set; }
        public string STM { get; set; }
        public string STTM { get; set; }
        public string SNG { get; set; }
        public string STNG { get; set; }
        public string STNG1 { get; set; }
        public string STNG2 { get; set; }
        public string STNG3 { get; set; }
        public string SXauNoiMa { get; set; }
        public string SMa { get; set; }
        public string SLNS { get; set; }
        public string SMoTa { get; set; }
        public bool BHangCha { get; set; }
        public double? FDuToanNSDuocGiao { get; set; }
        public double? FChoPhanBo { get; set; }
        public double? FTuChi { get; set; }
        public double? FTyLe1 { get; set; }
        public double? FCong { get; set; }
        public double? FTyLe2 { get; set; }
        public double? FChiTaiBanThan { get; set; }
        public double? FTyLe3 { get; set; }
        public double? FTongSo => FChiTaiBanThan.GetValueOrDefault() + FKhoiDuToan.GetValueOrDefault() + FKhoiDoanhNghiep.GetValueOrDefault() + FBVTC.GetValueOrDefault();
        public double? FTyLe4 { get; set; }
        public double? FKhoiDuToan { get; set; }
        public double? FTyLe5 { get; set; }
        public double? FKhoiDoanhNghiep { get; set; }
        public double? FTyLe6 { get; set; }
        public double? FBVTC { get; set; }
        public double? FTyLe7 { get; set; }
        public double? FSoPhanBo { get; set; }
        public double? FSoPhanBoBanThan { get; set; }
        public double? FSoPhanBoDV1 { get; set; }
        public double? FSoPhanBoDV2 { get; set; }
        public double? FSoPhanBoDV3 { get; set; }
        public double? FSoPhanBoDV4 { get; set; }
        public double? FSoPhanBoDV5 { get; set; }
        public double? FSoPhanBoDV6 { get; set; }
        public double? FTongSoPhanBo { get; set; }
        public string SLoai { get; set; }
        public string IIdMaDonVi { get; set; }
        public int ILoai { get; set; }
        public int IRoot { get; set; }
        public bool HasPrintData => FDuToanNSDuocGiao.GetValueOrDefault() != 0 || FChoPhanBo.GetValueOrDefault() != 0 || FCong.GetValueOrDefault() != 0
                                    || FChiTaiBanThan.GetValueOrDefault() != 0 || FKhoiDuToan.GetValueOrDefault() != 0 || FKhoiDoanhNghiep.GetValueOrDefault() != 0
                                    || FBVTC.GetValueOrDefault() != 0;
        public bool HasPrintDataPLII => FDuToanNSDuocGiao.GetValueOrDefault() != 0 || FChoPhanBo.GetValueOrDefault() != 0 || FSoPhanBoBanThan.GetValueOrDefault() != 0
                                    || FSoPhanBoDV1.GetValueOrDefault() != 0 || FSoPhanBoDV2.GetValueOrDefault() != 0
                                    || FSoPhanBoDV3.GetValueOrDefault() != 0 || FSoPhanBoDV4.GetValueOrDefault() != 0 || FSoPhanBoDV5.GetValueOrDefault() != 0
                                    || FSoPhanBo.GetValueOrDefault() != 0;

        public List<DataReportDynamic> ListDataValue { get; set; } = new List<DataReportDynamic>();
        public List<DataReportDynamic> ListDataValueL2 { get; set; } = new List<DataReportDynamic>();
        public List<DataReportDynamic2> ListDataValue2 { get; set; } = new List<DataReportDynamic2>();
        public string MergeRange { get; set; } = default;
    }
}
