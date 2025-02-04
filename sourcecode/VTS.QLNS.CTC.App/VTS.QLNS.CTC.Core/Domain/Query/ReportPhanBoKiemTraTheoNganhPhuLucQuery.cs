using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportPhanBoKiemTraTheoNganhPhuLucQuery
    {
        public Guid? IIdMlsktCha { get; set; }
        public Guid IIdMlskt { get; set; }
        public string IdDonVi { get; set; }
        public string STenDonVi { get; set; }
        public string sKyHieu { get; set; }
        public string STT { get; set; }
        public string SSTTBC { get; set; }
        public bool? BHangCha { get; set; }
        public string sNG { get; set; }
        public string sMoTa { get; set; }
        public string sNgCha { get; set; }
        public Int32 QuyetToan { get; set; }
        public Int32 DuToan { get; set; }
        public double TuChi { get; set; }
        public double PhanCap { get; set; }
        public double MuaHangHienVat { get; set; }
        public double DacThu { get; set; }
        public double TongCong { get; set; }
        public double TuChiSoNhuCau { get; set; }
        public double TuChiSoKiemTraDeXuat { get; set; }
        public double TuChiSoKiemTraNamTruoc { get; set; }
        public double FDuToan { get; set; }
        public string GhiChu { get; set; }
        public int LoaiNamNay { get; set; }

        public List<NsSktChungTuChiTiet> LstGiaTri { get; set; }
        public List<NsSktChungTuChiTiet> LstGiaTriTotal { get; set; }
    }
}
