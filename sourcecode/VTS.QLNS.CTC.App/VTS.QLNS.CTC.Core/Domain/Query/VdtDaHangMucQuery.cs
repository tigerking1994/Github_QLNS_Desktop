using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class VdtDaHangMucQuery
    {
        public Guid? Id { get; set; }
        public Guid? IdChuTruongHangMuc { get; set; }
        public Guid IdDuAnHangMuc { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public string SMaHangMuc { get; set; }
        public string STenHangMuc { get; set; }
        public Guid? IIdParentId { get; set; }
        public string MaOrDer { get; set; }
        public bool? IsHangCha { get; set; }
        public bool? IsEditHangMuc { get; set; }
        public Guid? IdChuTruong { get; set; }
        public Guid? LoaiCongTrinhId { get; set; }
        public double? HanMucDT { get; set; }
        public string TenLoaiCongTrinh { get; set; }

        public int Stt { get; set; }
    }
}
