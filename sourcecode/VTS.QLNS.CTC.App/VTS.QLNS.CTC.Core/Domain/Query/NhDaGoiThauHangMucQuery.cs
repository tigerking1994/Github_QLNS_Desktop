using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NhDaGoiThauHangMucQuery
    {
        public Guid Id { get; set; }
        public Guid? IIdParentId { get; set; }
        public Guid? IIdGoiThauChiPhiId { get; set; }
        public Guid? IIDGoiThauCheck { get; set; }
        public Guid? IIdQDDauTuHangMucId { get; set; }
        public Guid? IIdDuToanChiPhiId { get; set; }
        public double? FTienGoiThauUsd { get; set; }
        public double? FTienGoiThauVnd { get; set; }
        public double? FTienGoiThauEur { get; set; }
        public double? FTienGoiThauNgoaiTeKhac { get; set; }
        public string STenHangMucQDDT { get; set; }
        public string STenHangMucDT { get; set; }
        public string STenChiPhiDT { get; set; }
        public string SMaHangMuc { get; set; }
        public string SMaOrder { get; set; }
        public int IsCheck { get; set; }
    }
    public class NhDaGoiThauChiPhiHangMucQuery
    {
        public Guid Id { get; set; }
        public Guid? IIdParentId { get; set; }
        public Guid? IIdParentChiPhiId { get; set; }

        [Column("iID_GoiThau_ChiPhiID")]
        public Guid? IIdGoiThauChiPhiId { get; set; }
        [Column("iID_GoiThau_HangMucID")]
        public Guid? IIdGoiThauHangMucId { get; set; }  
        [Column("iID_GoiThauID")]
        public Guid? IIDGoiThau { get; set; }
        [Column("iID_QDDauTu_ChiPhiID")]
        public Guid? IIdQDDauTuHangMucId { get; set; }
        [Column("iID_DuToan_ChiPhiID")]
        public Guid? IIdDuToanChiPhiId { get; set; }
        [Column("iID_DuToan_HangMucID")]
        public Guid? IIdDuToanHangMucId { get; set; }
        [Column("iID_ChiPhi")]
        public Guid? IIdChiPhiId { get; set; }
        public Guid? IIdHopDongChiPhiId { get; set; }
        public Guid? IIdHopDongHangMucId { get; set; }
        public Guid? IIdHopDongGoiThauNhaThauId { get; set; }
        public Guid? IIdHopDongId { get; set; }
        public double? FTienGoiThauUsd { get; set; }
        public double? FTienGoiThauVnd { get; set; }
        public double? FTienGoiThauEur { get; set; }
        public double? FTienGoiThauNgoaiTeKhac { get; set; }
        public string STenHangMucQDDT { get; set; }
        public string STenHangMucDT { get; set; }
        public string STenChiPhi { get; set; }
        public string SMaChiPhi { get; set; }
        public string STenChiPhiVietTat { get; set; }
        public string SMaHangMuc { get; set; }
        public string SMaOrder { get; set; }
        public int IsCheck { get; set; }
        public Guid? IIDGoiThauCheck { get; set; }
        public double? FTienHopDongUsd { get; set; }
        public double? FTienHopDongVnd { get; set; }
        public double? FTienHopDongEur { get; set; }
        public double? FTienHopDongNgoaiTeKhac { get; set; }

    }
}
