using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class KHLCNhaThauQuery
    {
        public Guid Id { get; set; }
        public int? IRowIndex { get; set; }
        public Guid? IIdDuToanId { get; set; }
        public Guid? IIdQdDauTuId { get; set; }
        public Guid? IIdChuTruongDauTuId { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public Guid? IIdDonViQuanLyId { get; set; }
        public string STenDonVi { get; set; }
        public string IIdMaDonViQuanLy { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public string STenDuAn { get; set; }
        public string SMoTa { get; set; }
        public string SUserCreate { get; set; }
        public bool BActive { get; set; }
        public Guid? IIdParentId { get; set; }
        public Guid? IIdLcNhaThauGocId { get; set; }
        public bool BKhoa { get; set; }
        public int ISoLanDieuChinh { get; set; }
        public int ITotalFiles { get; set; }
        public bool BIsGoc { get; set; }
        public string SMaDuAn { get; set; }
    }
}
