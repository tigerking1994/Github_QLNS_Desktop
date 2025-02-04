using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportDuToanDauNamTheoNganhPhuLucQuery
    {
        public Guid? iID_MLNS { get; set; }
        public Guid? iID_MLNS_Cha { get; set; }
        public string IdDonVi { get; set; }
        public string STenDonVi { get; set; }
        public string sKyHieu { get; set; }
        public string STT { get; set; }
        public bool? BHangCha { get; set; }
        public string sNG { get; set; }
        public string sTNG { get; set; }
        public string sMoTa { get; set; }
        public string sNgCha { get; set; }
        public Int32 QuyetToan { get; set; }
        public Int32 DuToan { get; set; }
        public double TuChi { get; set; }
        public double PhanCap { get; set; }
        public double MuaHangHienVat { get; set; }
        public double DacThu { get; set; }
        public double TongCong { get; set; }
        public string sLNS { get; set; }
        public string sL { get; set; }
        public string sK { get; set; }
        public string sM { get; set; }
        public string sTM { get; set; }
        public string sTTM { get; set; }
        public string sXauNoiMa { get; set; }
        public List<NsDtdauNamChungTuChiTiet> LstGiaTri { get; set; } = new List<NsDtdauNamChungTuChiTiet> { };
        public List<NsDtdauNamChungTuChiTiet> LstGiaTriTotal { get; set; } = new List<NsDtdauNamChungTuChiTiet> { };
        public int ILoai { get; set; }
        public string SXauNoiMaToiM => StringUtils.Join(StringUtils.DIVISION, sLNS, sL, sK, sM);
        public string SXauNoiMaToiTM => StringUtils.Join(StringUtils.DIVISION, sLNS, sL, sK, sM, sTM);
        public string SXauNoiMaToiTTM => StringUtils.Join(StringUtils.DIVISION, sLNS, sL, sK, sM, sTM, sTTM);
    }
}
