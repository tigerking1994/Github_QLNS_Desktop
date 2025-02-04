using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class SoLieuChiTietPhanCapRepository : Repository<NsDtdauNamPhanCap>, ISoLieuChiTietPhanCapRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public SoLieuChiTietPhanCapRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public NsDtdauNamPhanCap FindByCondition(string idDonVi, string idChungTuChiTiet, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDtdauNamPhanCaps.Where(n => n.IIdMaDonVi == idDonVi && n.IIdCtdtdauNamChiTiet == Guid.Parse(idChungTuChiTiet) && n.INamLamViec == namLamViec).FirstOrDefault();
            }
        }

        public IEnumerable<NsDtdauNamPhanCap> FindDonViTongHop(string idDonVi, string mlnsId, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDtdauNamPhanCaps.Where(n => n.IIdMaDonVi == idDonVi && n.IIdMlns == Guid.Parse(mlnsId) && n.INamLamViec == namLamViec);
            }
        }

        public IEnumerable<SoLieuChiTietPhanCapQuery> GetSoLieuChiTietPhanCap(int namLamViec, string xauNoiMa, string listXauNoiMa, string idChiTiet)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                try
                {
                    SqlParameter namLamViecParam = new SqlParameter("@YearOfWork", namLamViec);
                    SqlParameter xauNoiMaStringParam = new SqlParameter("@XauNoiMaString", listXauNoiMa);
                    SqlParameter xauNoiMaParam = new SqlParameter("@XauNoiMa", xauNoiMa);
                    SqlParameter idChiTietParam = new SqlParameter("@ChiTietId", idChiTiet);
                    return ctx.FromSqlRaw<SoLieuChiTietPhanCapQuery>("EXECUTE dbo.sp_skt_dutoan_daunam_phancap @YearOfWork, @XauNoiMaString, @XauNoiMa, @ChiTietId",
                        namLamViecParam, xauNoiMaStringParam, xauNoiMaParam, idChiTietParam).ToList();
                }
                catch (Exception ex)
                {
                    return new List<SoLieuChiTietPhanCapQuery>();
                }
            }
        }

        public IEnumerable<SoLieuChiTietPhanCapQuery> GetSoLieuChiTietPhanCapDTDN(int namLamViec, string xauNoiMa, string listXauNoiMa, string idChiTiet, Guid iID_CTDTDauNam, string XauNoiMaGoc)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                try
                {
                    SqlParameter namLamViecParam = new SqlParameter("@YearOfWork", namLamViec);
                    SqlParameter xauNoiMaStringParam = new SqlParameter("@XauNoiMaString", listXauNoiMa);
                    SqlParameter xauNoiMaParam = new SqlParameter("@XauNoiMa", xauNoiMa);
                    SqlParameter idChiTietParam = new SqlParameter("@ChiTietId", idChiTiet);
                    SqlParameter iD_CTDTDauNam = new SqlParameter("@iID_CTDTDauNam", iID_CTDTDauNam);
                    SqlParameter xauNoiMaGoc = new SqlParameter("@XauNoiMaGoc", XauNoiMaGoc);
                    return ctx.FromSqlRaw<SoLieuChiTietPhanCapQuery>("EXECUTE dbo.sp_skt_dutoan_daunam_phancap_dtdn @YearOfWork, @XauNoiMaString, @XauNoiMa, @ChiTietId, @iID_CTDTDauNam, @XauNoiMaGoc",
                        namLamViecParam, xauNoiMaStringParam, xauNoiMaParam, idChiTietParam, iD_CTDTDauNam, xauNoiMaGoc).ToList();
                }
                catch (Exception ex)
                {
                    return new List<SoLieuChiTietPhanCapQuery>();
                }
            }
        }

        public IEnumerable<SoLieuChiTietPhanCapQuery> GetSoLieuChiTietPhanCapDonVi0(int namLamViec, string xauNoiMa, string listXauNoiMa, string idChiTiet)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                try
                {
                    SqlParameter namLamViecParam = new SqlParameter("@YearOfWork", namLamViec);
                    SqlParameter xauNoiMaStringParam = new SqlParameter("@XauNoiMaString", listXauNoiMa);
                    SqlParameter xauNoiMaParam = new SqlParameter("@XauNoiMa", xauNoiMa);
                    SqlParameter idChiTietParam = new SqlParameter("@ChiTietId", idChiTiet);
                    return ctx.FromSqlRaw<SoLieuChiTietPhanCapQuery>("EXECUTE dbo.sp_skt_dutoan_daunam_phancap_donvi_0 @YearOfWork, @XauNoiMaString, @XauNoiMa, @ChiTietId",
                        namLamViecParam, xauNoiMaStringParam, xauNoiMaParam, idChiTietParam).ToList();
                }
                catch (Exception ex)
                {
                    return new List<SoLieuChiTietPhanCapQuery>();
                }
            }
        }

        public IEnumerable<SoLieuChiTietPhanCapQuery> GetSoLieuChiTietPhanCapDonVi0_1(int namLamViec, Guid iID_CTDTDauNam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                try
                {
                    SqlParameter namLamViecParam = new SqlParameter("@YearOfWork", namLamViec);
                    SqlParameter iD_CTDTDauNamParam = new SqlParameter("@iID_CTDTDauNam", iID_CTDTDauNam);
                    return ctx.FromSqlRaw<SoLieuChiTietPhanCapQuery>("EXECUTE dbo.sp_skt_dutoan_daunam_phancap_donvi0_1 @YearOfWork, @iID_CTDTDauNam", namLamViecParam, iD_CTDTDauNamParam).ToList();
                }
                catch (Exception ex)
                {
                    return new List<SoLieuChiTietPhanCapQuery>();
                }
            }
        }

        public void DeleteByVoucherId(Guid voucherId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var voucherIdParam = new SqlParameter("@VoucherId", voucherId.ToString());
                ctx.Database.ExecuteSqlCommand($"DELETE NS_DTDauNam_PhanCap WHERE iID_CTDTDauNamChiTiet = @VoucherId", voucherIdParam);
            }
        }
    }
}
