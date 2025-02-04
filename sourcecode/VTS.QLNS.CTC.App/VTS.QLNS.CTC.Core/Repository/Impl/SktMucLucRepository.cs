using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class SktMucLucRepository : Repository<NsSktMucLuc>, ISktMucLucRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public SktMucLucRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<SktMucLucQuery> FindByCondition(int namLamViec, int loai, string idDonVi, int loaiChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_skt_mucluc_index @YearOfWork, @Loai, @LoaiChungTu, @AgencyId";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", namLamViec),
                    new SqlParameter("@Loai", loai),
                    new SqlParameter("@AgencyId", idDonVi),
                    new SqlParameter("@LoaiChungTu", loaiChungTu)
                };
                return ctx.FromSqlRaw<SktMucLucQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NsSktMucLuc> FindByKyHieu(int namLamViec, List<string> kyHieu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsSktMucLucs.Where(n => n.INamLamViec == namLamViec && n.ITrangThai == 1 && n.BHangCha && kyHieu.Contains(n.SKyHieu)).ToList();
            }
        }

        public IEnumerable<NsSktMucLuc> FindByNganh(int namLamViec, List<string> nganh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsSktMucLucs.Where(n => n.INamLamViec == namLamViec && n.ITrangThai == 1 && nganh.Contains(n.SNg)).ToList();
            }
        }

        public IEnumerable<NsSktMucLuc> FindAll(AuthenticationInfo authenticationInfo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                List<NsSktMucLuc> sktMucLucs = new List<NsSktMucLuc>();
                var sktML = ctx.NsSktMucLucs.Where(b => b.INamLamViec == authenticationInfo.YearOfWork).ToList();
                var sktMLMaps = ctx.NsMlsktMlns.Where(b => b.INamLamViec == authenticationInfo.YearOfWork).ToList();
                var sql = from s in sktML
                          join m in sktMLMaps
                          on new { namLamViec = s.INamLamViec, kyHieu = s.SKyHieu } equals
                              new { namLamViec = m.INamLamViec, kyHieu = m.SSktKyHieu } into maps
                          select new { mucLuc = s, maps = maps };

                return sql.Select(t => t.mucLuc.BuildSktMucLucMap(t.maps.ToList())).ToList();
            }
        }

        public IEnumerable<NsSktMucLuc> FindAllOld(AuthenticationInfo authenticationInfo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                List<NsSktMucLuc> sktMucLucs = new List<NsSktMucLuc>();
                //var sktML = ctx.NsSktMucLucs.Where(b => b.INamLamViec == authenticationInfo.YearOfWork && b.ITrangThai == 1).ToList();
                //var sktMLMaps = ctx.NsMlsktMlns.Where(b => b.INamLamViec == authenticationInfo.YearOfWork && b.ITrangThai == 1).ToList();
                var parameters = new[]
                {
                    new SqlParameter("@iNamLamViec", 2024)
                };
                var sqlktML = @"SELECT *, bCoDinhMuc = 1 FROM ns_skt_mucluc_backup_2024 WHERE iNamLamViec = @iNamLamViec";
                var sqlktMLMaps = @"SELECT *, bCoDinhMuc = 1 FROM ns_mlskt_mlns_backup_2024 WHERE iNamLamViec = @iNamLamViec";

                var sktML = ctx.Set<NsSktMucLuc>().FromSql(sqlktML, parameters).ToList();
                var sktMLMaps = ctx.Set<NsMlsktMlns>().FromSql(sqlktMLMaps, parameters).ToList();

                var sql = from s in sktML
                          join m in sktMLMaps
                          on new { namLamViec = s.INamLamViec, kyHieu = s.SKyHieu } equals
                              new { namLamViec = m.INamLamViec, kyHieu = m.SSktKyHieu } into maps
                          select new { mucLuc = s, maps = maps };

                return sql.Select(t => t.mucLuc.BuildSktMucLucMap(t.maps.ToList())).ToList();
            }
        }

        public IEnumerable<NsSktMucLuc> FindAllNew(AuthenticationInfo authenticationInfo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                List<NsSktMucLuc> sktMucLucs = new List<NsSktMucLuc>();
                var sktML = ctx.NsSktMucLucs.Where(b => b.INamLamViec == 2024).ToList();
                var sktMLMaps = ctx.NsMlsktMlns.Where(b => b.INamLamViec == 2024).ToList();
                var sql = from s in sktML
                          join m in sktMLMaps
                          on new { namLamViec = s.INamLamViec, kyHieu = s.SKyHieu } equals
                              new { namLamViec = m.INamLamViec, kyHieu = m.SSktKyHieu } into maps
                          select new { mucLuc = s, maps = maps };

                return sql.Select(t => t.mucLuc.BuildSktMucLucMap(t.maps.ToList())).ToList();
            }
        }

        public int CountSktMucLuc(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsSktMucLucs.Where(t => t.INamLamViec == namLamViec).Count();
            }
        }

        public void UpdateBHangCha(IEnumerable<Guid> sktMucLucs, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                IEnumerable<NsSktMucLuc> SktMucLucs = ctx.NsSktMucLucs.Where(t => sktMucLucs.Contains(t.IIDMLSKT) && t.INamLamViec == namLamViec).ToList();
                foreach (var skt in SktMucLucs)
                {
                    skt.BHangCha = true;
                }
                ctx.SaveChanges();
            }
        }

        public void UpdateBHangChaToFalse(IEnumerable<Guid> sktMucLucs, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                foreach (var id in sktMucLucs)
                {
                    var skt = ctx.NsSktMucLucs.FirstOrDefault(t => t.IIDMLSKT.Equals(id) && t.INamLamViec == namLamViec);
                    var hasChild = ctx.NsSktMucLucs.Where(t => t.IIDMLSKTCha.HasValue && t.IIDMLSKTCha.Value.Equals(id) && t.INamLamViec == namLamViec).Count() > 0;
                    if (!hasChild)
                    {
                        skt.BHangCha = false;
                    }
                }
                ctx.SaveChanges();
            }
        }

        public bool IsUsedMLSKT(Guid iidMlskt, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var iResult = new SqlParameter
                {
                    ParameterName = "@iResult",
                    DbType = System.Data.DbType.Boolean,
                    Direction = System.Data.ParameterDirection.Output
                };
                SqlParameter namLamViecParam = new SqlParameter("@YearOfWork", namLamViec);
                SqlParameter mlsktIdParam = new SqlParameter("@iid_mlskt", System.Data.SqlDbType.UniqueIdentifier);
                mlsktIdParam.Value = iidMlskt;
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_check_used_mlskt @YearOfWork, @iid_mlskt, @iResult OUT",
                    namLamViecParam, mlsktIdParam, iResult);
                return (bool)iResult.Value;
            }
        }

        public IEnumerable<SktMucLucDtQuery> FindByCondition(int namLamViec, int namNganSach, int nguonNganSach, int loai, string idDonVi, int loaiChungTu, string chungTuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_skt_mucluc_index_chungtu @YearOfWork, @YearOfBudget, @BudgetOfSource, @VoucherId, @Loai, @LoaiChungTu, @AgencyId";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", namLamViec),
                    new SqlParameter("@YearOfBudget", namNganSach),
                    new SqlParameter("@BudgetOfSource", nguonNganSach),
                    new SqlParameter("@VoucherId", chungTuId),
                    new SqlParameter("@Loai", loai),
                    new SqlParameter("@AgencyId", idDonVi),
                    new SqlParameter("@LoaiChungTu", loaiChungTu)
                };
                return ctx.FromSqlRaw<SktMucLucDtQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<SktMucLucDtQuery> FindByConditionBVTC(int namLamViec, int namNganSach, int nguonNganSach, string loai, string idDonVi, int loaiChungTu, int? iloaiNNS, string chungTuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_skt_mucluc_index_chungtu_bvtc @YearOfWork, @YearOfBudget, @BudgetOfSource, @VoucherId, @Loai, @LoaiChungTu, @iLoaiNNS, @AgencyId";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", namLamViec),
                    new SqlParameter("@YearOfBudget", namNganSach),
                    new SqlParameter("@BudgetOfSource", nguonNganSach),
                    new SqlParameter("@VoucherId", chungTuId),
                    new SqlParameter("@Loai", loai),
                    new SqlParameter("@AgencyId", idDonVi),
                    new SqlParameter("@iLoaiNNS", iloaiNNS),
                    new SqlParameter("@LoaiChungTu", loaiChungTu)
                };
                return ctx.FromSqlRaw<SktMucLucDtQuery>(sql, parameters).ToList();
            }
        }

        public void DeleteSktByNamLamViec(IEnumerable<int> NamLamViecs)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var nsSktMLns = ctx.NsMlsktMlns.Where(b => NamLamViecs.Contains(b.INamLamViec));
                ctx.NsMlsktMlns.RemoveRange(nsSktMLns);
                var sktMuclucs = ctx.NsSktMucLucs.Where(b => NamLamViecs.Contains(b.INamLamViec));
                ctx.NsSktMucLucs.RemoveRange(sktMuclucs);
                ctx.SaveChanges();
            }
        }

        public IEnumerable<NsMlsktMlns> FindAllMapMlsktMlns(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsMlsktMlns.Where(b => b.INamLamViec == namLamViec).ToList();
            }
        }

        public int CountNsMlsktMlns(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsMlsktMlns.Count(b => b.INamLamViec == namLamViec);
            }
        }

        public void RevertAllMLSKT(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_mlskt_revert_all @NamLamViec";
                var parameters = new[]
                {
                    new SqlParameter("@NamLamViec", namLamViec)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public void UpdateNSMlsktMlnsMapping()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.Database.ExecuteSqlCommand($"update map set map.sSKT_KyHieu = skt.sKyHieu from ns_mlskt_mlns map join ns_skt_mucluc skt on map.sSKT_KyHieu = skt.sKyHieuCu and map.inamlamviec = skt.inamlamviec and map.sSKT_KyHieu <> skt.sKyHieu where map.inamlamviec = 2024");
            }
        }

        public void UpdateSKTChungTuChiTiet(Guid voucherID)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var parameters = new[]
                {
                    new SqlParameter("@voucherID", voucherID)
                };

                ctx.Database.ExecuteSqlCommand($"update ctct set ctct.sKyHieu = skt.sKyHieu from NS_SKT_ChungTuChiTiet ctct join NS_SKT_MucLuc skt on ctct.sKyHieu = skt.sKyHieuCu and skt.iNamLamViec = ctct.iNamLamViec where ctct.iNamLamViec = 2024 and ctct.iID_CTSoKiemTra = @voucherID", parameters);
            }
        }
    }
}