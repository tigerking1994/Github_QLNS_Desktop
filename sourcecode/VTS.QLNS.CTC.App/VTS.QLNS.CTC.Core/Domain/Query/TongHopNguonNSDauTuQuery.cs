using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class TongHopNguonNSDauTuQuery
    {
        public Guid Id { get; set; }
        public Guid iID_ChungTu { get; set; }
        public Guid iID_DuAnID { get; set; }
        public string sMaNguon { get; set; }
        public string sMaNguonCha { get; set; }
        public string sMaDich { get; set; }
        public double? fGiaTri { get; set; }
        public int? ILoaiUng { get; set; }
        public int iStatus { get; set; }
        public bool bIsLog { get; set; }
        public Guid? iId_MaNguonCha { get; set; }
        public int? iThuHoiTUCheDo { get; set; }
        public Guid? IIDMucID { get; set; }
        public Guid? IIDTieuMucID { get; set; }
        public Guid? IIDTietMucID { get; set; }
        public Guid? IIDNganhID { get; set; }
        public Guid? IIdLoaiCongTrinh { get; set; }
        [NotMapped]
        public bool? BKeHoach { get; set; }
    }
}
