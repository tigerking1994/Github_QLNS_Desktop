using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhCptuBHYTQuery
    {
        public Guid Id { get; set; }

        public string SSoChungTu { get; set; }
        public DateTime DNgayChungTu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public string IIDMaDonVi { get; set; }
        public DateTime DNgayQuyetDinh { get; set; }
        public int IQuy { get; set; }
        public string SMoTa { get; set; }
        public string SDSID_CoSoYTe { get; set; }
        public string SDSLNS { get; set; }
        public bool BIsKhoa { get; set; }
        public bool BIsTongHop { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        public Double? FQTQuyTruoc { get; set; }
        public Double? FTamUngQuyNay { get; set; }
        public int INamLamViec { get; set; }
        public int SDSSoChungTuTongHop { get; set; }
        public bool Selected { get; set; }
        public string SSoChungTuParent { get; set; }
        public bool IsExpand { get; set; }

        public bool IsCollapse { get; set; }


    }
}
