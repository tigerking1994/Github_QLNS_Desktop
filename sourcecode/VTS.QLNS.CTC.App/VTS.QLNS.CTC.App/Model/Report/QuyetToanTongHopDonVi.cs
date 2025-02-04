using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model.Report
{
    public class QuyetToanTongHopDonVi
    {
        [ColumnAttribute("LNS", 2, MLNSFiled.LNS)]
        public string LNS { get; set; }
        public string L { get; set; }
        public string K { get; set; }
        [ColumnAttribute("M", 3, MLNSFiled.M)]
        public string M { get; set; }
        [ColumnAttribute("TM", 4, MLNSFiled.TM)]
        public string TM { get; set; }
        [ColumnAttribute("TTM", 5, MLNSFiled.TTM)]
        public string TTM { get; set; }
        [ColumnAttribute("NG", 6, MLNSFiled.NG)]
        public string NG { get; set; }
        [ColumnAttribute("TNG", 7, MLNSFiled.TNG)]
        public string TNG { get; set; }
        [ColumnAttribute("TNG1", 8, MLNSFiled.TNG1)]
        public string TNG1 { get; set; }
        [ColumnAttribute("TNG2", 9, MLNSFiled.TNG2)]
        public string TNG2 { get; set; }
        [ColumnAttribute("TNG3", 10, MLNSFiled.TNG3)]
        public string TNG3 { get; set; }
        public string XauNoiMa { get; set; }
        public string MoTa { get; set; }
        public double DuToan { get; set; }
        public double QuyetToan { get; set; }
        public double TrongKy { get; set; }
        public double ChenhLech => DuToan - QuyetToan;
        public double QuyetToan1 { get; set; }
        public double QuyetToan2 { get; set; }
        public double QuyetToan3 { get; set; }
        public double QuyetToan4 { get; set; }
        public double QuyetToan5 { get; set; }
        public double QuyetToan6 { get; set; }
        public double QuyetToan7 { get; set; }
        public double QuyetToan8 { get; set; }
        public double QuyetToan9 { get; set; }
        public double QuyetToan10 { get; set; }
        public double DuToan1 { get; set; }
        public double DuToan2 { get; set; }
        public double DuToan3 { get; set; }
        public double DuToan4 { get; set; }
        public double DuToan5 { get; set; }
        public double DuToan6 { get; set; }
        public double DuToan7 { get; set; }
        public double DuToan8 { get; set; }
        public double DuToan9 { get; set; }
        public double DuToan10 { get; set; }
        public double ChenhLech1 => DuToan1 - QuyetToan1;
        public double ChenhLech2 => DuToan2 - QuyetToan2;
        public double ChenhLech3 => DuToan3 - QuyetToan3;
        public double ChenhLech4 => DuToan4 - QuyetToan4;
        public double ChenhLech5 => DuToan5 - QuyetToan5;
        public double ChenhLech6 => DuToan6 - QuyetToan6;
        public double ChenhLech7 => DuToan7 - QuyetToan7;
        public double ChenhLech8 => DuToan8 - QuyetToan8;
        public double ChenhLech9 => DuToan9 - QuyetToan9;
        public double ChenhLech10 => DuToan10 - QuyetToan10;
        public bool IsHangCha { get; set; }
        public bool BHangChaDuToan { get; set; }
        public bool BHangChaQuyetToan { get; set; }
        public Guid MlnsId { get; set; }
        public Guid? MlnsIdParent { get; set; }
        public string MaDonVi { get; set; }
        public bool HasAllData => DuToan != 0 || QuyetToan != 0 || TrongKy != 0 || HasDataQuyetToan || HasDataDuToan;
        public bool HasDataQuyetToan => QuyetToan1 != 0 || QuyetToan2 != 0 || QuyetToan3 != 0 ||
                            QuyetToan4 != 0 || QuyetToan5 != 0 || QuyetToan6 != 0 || QuyetToan7 != 0 || QuyetToan8 != 0 || QuyetToan9 != 0 || QuyetToan10 != 0;
        public bool HasDataDuToan => DuToan1 != 0 || DuToan2 != 0 || DuToan3 != 0 || DuToan4 != 0 || DuToan5 != 0 || DuToan6 != 0 || DuToan7 != 0 || DuToan8 != 0 || DuToan9 != 0 || DuToan10 != 0;
        public double DuToanOrigin => DuToan;
        public List<QuyetToanDynamicValue> LstValue { get; set; } = new List<QuyetToanDynamicValue>();
        public int IndexAgency { get; set; }
    }
}
