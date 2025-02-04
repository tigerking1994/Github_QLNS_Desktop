using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ChungTuThanhToanQuery
    {
        public Guid Id { get; set; }
        public string sSoQuyetDinh { get; set; }
        public int iNamKeHoach { get; set; }
        public int PhanLoai { get; set; }
        public double? LenhChi { get; set; }
        public string TenLoai { get; set; }
        public string sMaNguonCha { get; set; }
        public Guid? iId_MaNguonCha { get; set; }
    }
}
