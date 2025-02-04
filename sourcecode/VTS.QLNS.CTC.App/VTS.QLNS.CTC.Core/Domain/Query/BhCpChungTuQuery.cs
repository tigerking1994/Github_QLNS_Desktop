using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhCpChungTuQuery
    {
        [Column("iID_CP_ChungTu")]
        public Guid Id { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime? DNgayChungTu { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        public int? INamChungTu { get; set; }
        public string SMoTa { get; set; }
        public string SID_MaDonVi { get; set; }
        public string SLNS { get; set; }
        public Guid? IID_LoaiCap { get; set; }
        public double? FTienDaCap { get; set; }
        public double? FTienKeHoachCap { get; set; }
        public double? FTienDuToan { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
        public bool BIsKhoa { get; set; }
        public Guid IID_TongHop { get; set; }
        public int ILoaiTongHop { get; set; }
        public string STongHop { get; set; }
        public string STenDonVi { get; set; }
        public string STenDanhMucLoaiChi { get; set; }
        public int IQuy { get; set; }
        public string SMaLoaiChi { get; set; }
        public double? FTyLeThu { get; set; }

    }
}
