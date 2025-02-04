using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ThDChungTuChiTietQuery
    {
        public Guid IdMucLuc { get; set; }
        public string KyHieu { get; set; }
        public string M { get; set; }
        public string NganhParent { get; set; }
        public string STT { get; set; }
        public string STTBC { get; set; }
        public string MoTa { get; set; }
        public bool IsHangCha { get; set; }
        public Guid IdParent { get; set; }
        public Guid Id { get; set; }
        public Guid? IdDb { get; set; }
        public Guid? IdChungTu { get; set; }
        public int? NamLamViec { get; set; }
        public int? NamNganSach { get; set; }
        public int? NguonNganSach { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateCreated { get; set; }
        public string IdDonVi { get; set; }
        public string TenDonVi { get; set; }
        public double TuChi { get; set; }
        public double SuDungTonKho { get; set; }
        public double ChiDacThuNganhPhanCap { get; set; }
        public double HuyDongCTC { get; set; }
        public double HuyDongNganh { get; set; }
        public string GhiChu { get; set; }
        public string Nganh { get; set; }
        [NotMapped]

        public double TuChiPrev { get; set; }
    }
}
