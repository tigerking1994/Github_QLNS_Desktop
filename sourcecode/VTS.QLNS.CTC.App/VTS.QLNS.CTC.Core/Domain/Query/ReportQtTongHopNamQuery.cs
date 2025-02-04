using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ReportQtTongHopNamQuery
    {
        [Column("iID_MLNS")]
        public Guid? MLNS_Id { get; set; }
        [Column("iID_MLNS_Cha")]
        public Guid? MLNS_Id_Parent { get; set; }
        [Column("sLNS")]
        public string LNS { get; set; }
        [Column("sL")]
        public string L { get; set; }
        [Column("sK")]
        public string K { get; set; }
        [Column("sM")]
        public string M { get; set; }
        [Column("sTM")]
        public string TM { get; set; }
        [Column("sTTM")]
        public string TTM { get; set; }
        [Column("sNG")]
        public string NG { get; set; }
        [Column("sTNG")]
        public string TNG { get; set; }
        [Column("sTNG1")]
        public string TNG1 { get; set; }
        [Column("sTNG2")]
        public string TNG2 { get; set; }
        [Column("sTNG3")]
        public string TNG3 { get; set; }
        [Column("sXauNoiMa")]
        public string XauNoiMa { get; set; }
        [Column("sMoTa")]
        public string MoTa { get; set; }
        public bool IsHangCha { get; set; }
        public bool IsHiddenValue { get; set; }
        public string IdDonVi { get; set; }
        public string TenDonVi { get; set; }
        public double? ChiTieuNamNayCustom {  get; set; }
        public double? ChiTieuNamNay { get; set; }
        public double? ChiTieuNamSauCustom { get; set; }
        public double? ChiTieuNamSau { get; set; }
        public double? QuyetToan { get; set; }
        public int Level { get; set; }

        [NotMapped]
        public bool HasData => ChiTieuNamNay != 0 || QuyetToan != 0 || ChiTieuNamSau != 0;

        [NotMapped]
        public double? ConNamNay
        {
            get => (ChiTieuNamNay.HasValue ? ChiTieuNamNay.Value : 0) - (ChiTieuNamSau.HasValue ? ChiTieuNamSau.Value : 0);
        }

        public double? ConNamNayCustom
        {
            get => (ChiTieuNamNayCustom.HasValue ? ChiTieuNamNayCustom.Value : 0) - (ChiTieuNamSauCustom.HasValue ? ChiTieuNamSauCustom.Value : 0);
        }

        [NotMapped]
        public double TiLe
        {
            get
            {
                double conNamNay = (ChiTieuNamNay.HasValue ? ChiTieuNamNay.Value : 0) - (ChiTieuNamSau.HasValue ? ChiTieuNamSau.Value : 0);
                if (conNamNay != 0)
                    return (QuyetToan.HasValue ? QuyetToan.Value : 0) * 100 / conNamNay;
                else
                    return 0;
            }
        }

        [NotMapped]
        public double? TongSo
        {
            get
            {
                double tongSo = ChiTieuNamNay.HasValue ? ChiTieuNamNay.Value : 0;
                double thucHien = QuyetToan.HasValue ? QuyetToan.Value : 0;
                return tongSo - thucHien;
            }
        }

        [NotMapped]
        public double? Thua
        {
            get
            {
                double tongSo = ChiTieuNamNay.HasValue ? ChiTieuNamNay.Value : 0;
                double thucHien = QuyetToan.HasValue ? QuyetToan.Value : 0;
                if (tongSo > thucHien)
                    return tongSo - thucHien;
                else
                    return 0;
            }
        }
        [NotMapped]
        public double? Thieu
        {
            get
            {
                double tongSo = ChiTieuNamNay.HasValue ? ChiTieuNamNay.Value : 0;
                double thucHien = QuyetToan.HasValue ? QuyetToan.Value : 0;
                if (tongSo < thucHien)
                    return thucHien - tongSo;
                else
                    return 0;
            }
        }

        public int INamNganSach { set; get; }
    }

    public class SettlementYearQuery
    {
        public Guid? MLNS_Id { get; set; }

        public Guid? MLNS_Id_Parent { get; set; }

        public string LNS { get; set; }

        public string L { get; set; }

        public string K { get; set; }

        public string M { get; set; }

        public string TM { get; set; }

        public string TTM { get; set; }

        public string NG { get; set; }

        public string TNG { get; set; }

        public string TNG1 { get; set; }

        public string TNG2 { get; set; }

        public string TNG3 { get; set; }

        public string XauNoiMa { get; set; }

        public string MoTa { get; set; }

        public string IdDonVi { get; set; }
        public string TenDonVi { get; set; }

        public bool IsHangCha { get; set; }

        public int? NamNganSach { get; set; }

    }

    public class FindApprovalSettlementYearQuery : SettlementYearQuery
    {

        public double? DuToanSoBaoCao { get; set; } = 0;

        public double? DuToanSoXetDuyet { get; set; } = 0;

        [NotMapped]
        public double? DuToanSoChenhLech
        {
            get; set;
        } = 0;


        public double? QuyetToanSoBaoCao { get; set; } = 0;

        public double? QuyetToanSoXetDuyet { get; set; } = 0;
        [NotMapped]
        public double? QuyetToanChenhLech
        {
            get; set;
        } = 0;
        [NotMapped]
        public double? XetDuyetDuToanConDuTongSo
        {
            get; set;
        } = 0;

        public double? XetDuyetDuToanConDuChuyenNamSau { get; set; } = 0;
        [NotMapped]
        public double? XetDuyetDuToanConDuBiHuy
        {
            get; set;
        } = 0;


    }

    public class PrintYearSummarySettlementQuery : SettlementYearQuery
    {
        public double? DuToanNganSach { get; set; } = 0;

        public double? SoDeNghiQuyetToan { get; set; } = 0;

        public double? TongSo
        {
            get
            {
                return DuToanNganSach.Value - SoDeNghiQuyetToan.Value;
            }
        }

        public double? SoChuyenNamSau { get; set; }

        public double? ThuaThieu
        {
            get
            {
                return DuToanNganSach.Value - SoDeNghiQuyetToan.Value - SoChuyenNamSau;
            }
        }

        public PrintYearSummarySettlementQuery()
        {

        }

        public PrintYearSummarySettlementQuery(FindApprovalSettlementYearQuery entity)
        {
            MLNS_Id = entity.MLNS_Id;
            MLNS_Id_Parent = entity.MLNS_Id_Parent;
            LNS = entity.LNS;
            L = entity.L;
            K = entity.L;
            M = entity.M;
            TM = entity.TM;
            TTM = entity.TTM;
            NG = entity.NG;
            TNG1 = entity.TNG1;
            TNG2 = entity.TNG2;
            TNG3 = entity.TNG3;
            XauNoiMa = entity.XauNoiMa;
            MoTa = entity.MoTa;
            IdDonVi = entity.IdDonVi;
            TenDonVi = entity.TenDonVi;
            DuToanNganSach = entity.DuToanSoBaoCao;
            SoDeNghiQuyetToan = entity.QuyetToanSoBaoCao;
            SoChuyenNamSau = entity.XetDuyetDuToanConDuChuyenNamSau;
            IsHangCha = entity.IsHangCha;
            NamNganSach = entity.NamNganSach;
        }
    }

    public class PrintYearSettlementQuery : SettlementYearQuery
    {
        public double? DuToanNamTruocChuyenSang { get; set; } = 0;

        public double? DuToanTongSo { get; set; } = 0;

        public double? DuToanBoSungSau { get; set; } = 0;

        public double? DuToanDuocSuDung
        {
            get
            {
                return DuToanTongSo.Value + DuToanNamTruocChuyenSang;
            }
        }

        public double? SoDeNghiQuyetToanNam { get; set; }

        public double? SoSanhThua
        {
            get
            {
                if (SoDeNghiQuyetToanNam.Value > DuToanDuocSuDung)
                {
                    return Math.Abs(SoDeNghiQuyetToanNam.Value - DuToanDuocSuDung.Value);
                }

                return 0;
            }
        }

        public double? SoSanhThieu
        {
            get
            {
                if (DuToanDuocSuDung.Value > SoDeNghiQuyetToanNam.Value)
                {
                    return Math.Abs(SoDeNghiQuyetToanNam.Value - DuToanDuocSuDung.Value);
                }

                return 0;
            }
        }

        public double? TiLe
        {
            get
            {
                if (DuToanDuocSuDung.Value == 0)
                {
                    return 0;
                }
                var tile = Math.Round(SoDeNghiQuyetToanNam.Value / DuToanDuocSuDung.Value, 4);
                return tile * 100;

            }
        }

        public double? DuToanBiHuy { get; set; } = 0;

        public double? DuToanChuyenNamSau { get; set; } = 0;
    }
}
