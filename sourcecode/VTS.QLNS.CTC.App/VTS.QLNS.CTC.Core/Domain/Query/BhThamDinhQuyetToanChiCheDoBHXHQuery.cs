using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class BhThamDinhQuyetToanChiCheDoBHXHQuery
    {
        [Column("sSTT")]
        public int STT { get; set; }
        [Column("sTenDonVi")]
        public string TenDonVi { get; set; }
        [Column("fOmDau")]
        public double OmDau { get; set; }
        [Column("fThaiSan")]
        public double ThaiSan { get; set; }
        [Column("fTNLDBNN")]
        public double TNLDBNN { get; set; }
        [Column("fHuuTri")]
        public double HuuTri { get; set; }
        [Column("fPhucVien")]
        public double PhucVien { get; set; }
        [Column("fXuatNgu")]
        public double XuatNgu { get; set; }
        [Column("fThoiViec")]
        public double ThoiViec { get; set; }
        [Column("fTuTuat")]
        public double TuTuat { get; set; }
        [Column("iKieuChu")]
        public int IKieuChu { get; set; }
        public double Cong => OmDau + ThaiSan + TNLDBNN + ThoiViec + TuTuat + HuuTri + PhucVien + XuatNgu;

    }
}
