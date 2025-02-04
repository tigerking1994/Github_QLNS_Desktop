using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportDuToanDauNamTongHopQuery
    {
        public string LNS1 { get; set; }
        public string LNS3 { get; set; }
        public string LNS5 { get; set; }
        public string LNS { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        public string M { get; set; }
        public string TM { get; set; }
        public string TTM { get; set; }
        public string NG { get; set; }
        public string TNG { get; set; }
        public string TNG1 { get; set; }
        public string TNG2 { get; set; }
        public string TNG3 { get; set; }
        public string MoTa { get; set; }
        public string XauNoiMa { get; set; }
        public double? QuyetToan { get; set; }
        public double? DuToan { get; set; }
        public double? TuChi { get; set; }
        public double? UocThucHien { get; set; }
        public Guid MlnsId { get; set; }
        public Guid? MlnsIdParent { get; set; }

        [NotMapped]
        public bool IsHangCha { get; set; }
        [NotMapped]
        public double TuChiDonVi1 { get; set; }
        [NotMapped]
        public double TuChiDonVi2 { get; set; }
        [NotMapped]
        public double TuChiDonVi3 { get; set; }
        [NotMapped]
        public double TuChiDonVi4 { get; set; }
        [NotMapped]
        public double TuChiDonVi5 { get; set; }
        [NotMapped]
        public double TuChiDonVi6 { get; set; }
        [NotMapped]
        public double UocThucHienDonVi1 { get; set; }
        [NotMapped]
        public double UocThucHienDonVi2 { get; set; }
        [NotMapped]
        public double UocThucHienDonVi3 { get; set; }
        [NotMapped]
        public double UocThucHienDonVi4 { get; set; }
        [NotMapped]
        public double UocThucHienDonVi5 { get; set; }
        [NotMapped]
        public double UocThucHienDonVi6 { get; set; }
        [NotMapped]

        public double? FTongTuChi => LstGiaTri.IsEmpty() ? 0 : LstGiaTri.Sum(x => x.fGiaTri);
        [NotMapped]

        public double? FTongUocThucHien => LstGiaTri.IsEmpty() ? 0 : LstGiaTri.Sum(x => x.fUocThucHien);
        public List<ReportDuToanDauNamTongHopDynamicQuery> LstGiaTri { get; set; }
    }

    public class ReportDuToanDauNamTongHopDynamicQuery
    {
        public int STT { get; set; }
        public string sMoTa { get; set; }
        public string sDuToanNam { get;set; }
        public string sUocThucHien { get; set; }
        public double? fGiaTri { get; set; }
        public double? fUocThucHien { get;set; }
    }
}
