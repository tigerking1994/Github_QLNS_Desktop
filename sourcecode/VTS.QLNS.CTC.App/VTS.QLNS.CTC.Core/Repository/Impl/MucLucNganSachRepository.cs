using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class MucLucNganSachRepository : Repository<NsMucLucNganSach>, IMucLucNganSachRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public MucLucNganSachRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int countMLNS(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsMucLucNganSaches.Where(t => namLamViec == t.NamLamViec).Count();
            }
        }

        /// <summary>
        /// Lấy danh sách mục lục ngân sách theo LNS
        /// </summary>
        /// <param name="lns"></param>
        /// <returns></returns>
        public List<NsMucLucNganSach> FindByLNS(string lns)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsMucLucNganSaches.Where(x => x.Lns == lns).ToList();
            }
        }

        public IEnumerable<NsMucLucNganSach> FindAllMlnsByLnsAndNamLamViec(List<string> lns, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsMucLucNganSaches.Where(x => lns.Contains(x.Lns) && x.NamLamViec == namLamViec).ToList();
            }
        }

        public IEnumerable<NsMucLucNganSach> FindBySktMucLucNotIn(string sktKyHieu, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var mlnss = ctx.NsMucLucNganSaches.Where(t => t.NamLamViec == namLamViec);
                var maps = ctx.NsMlsktMlns.Where(t => t.INamLamViec == namLamViec);
                var phongBans = ctx.NsPhongBans;
                var sql = from mlns in mlnss
                          join phongBan in phongBans
                          on new { idpb = mlns.IdPhongBan, nam = mlns.NamLamViec } equals new { idpb = phongBan.IIDMaBQuanLy, nam = phongBan.INamLamViec } into ps
                          from p in ps.DefaultIfEmpty()
                          join map in maps
                          on mlns.XauNoiMa equals map.SNsXauNoiMa into tbl
                          from m in tbl.DefaultIfEmpty()
                          where mlns.NamLamViec == namLamViec && (m == null || m.SSktKyHieu.Equals(sktKyHieu))
                          select new { mlns = mlns, tenPhongBan = p != null ? p.STenBQuanLy : string.Empty };
                return sql.ToList().Select(t =>
                {
                    var nsMucLucNganSach = t.mlns;
                    nsMucLucNganSach.TenPhongBan = t.tenPhongBan;
                    return nsMucLucNganSach;
                }).ToList();
            }
        }

        public IEnumerable<NsMucLucNganSach> FindByBHXHMucLucNotIn(int namLamViec, string mlnsLoai, string mlnsBhxh, string mlnsChosen)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var mlnss = ctx.NsMucLucNganSaches.Where(t => t.NamLamViec == namLamViec && t.SXauNoiMa.StartsWith("101")).ToList();
                var bhxhs = ctx.BhDmMucLucNganSachs.Where(t => t.INamLamViec == namLamViec && t.SXauNoiMa.StartsWith("902")).ToList();
                var mlnsList = mlnsChosen.Split(',');
                //var mlnsLC = "";
                //var mlnsPCCV = "";
                //var mlnsPCTN = "";
                //var mlnsPCTNVK = "";
                //var mlnsHSBL = "";
                //if (mlnsLoai == "TenSNSLuongChinh")
                //{
                //    mlnsPCCV = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sNS_PCCV)).Select(t => t.sNS_PCCV));
                //    mlnsPCTN = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sNS_PCTN)).Select(t => t.sNS_PCTN));
                //    mlnsPCTNVK = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sNS_PCTNVK)).Select(t => t.sNS_PCTNVK));
                //    mlnsHSBL = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sNS_HSBL)).Select(t => t.sNS_HSBL));
                //}
                //else if (mlnsLoai == "TenSNSPCCV")
                //{
                //    mlnsLC = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sNS_LuongChinh)).Select(t => t.sNS_LuongChinh));
                //    mlnsPCTN = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sNS_PCTN)).Select(t => t.sNS_PCTN));
                //    mlnsPCTNVK = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sNS_PCTNVK)).Select(t => t.sNS_PCTNVK));
                //    mlnsHSBL = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sNS_HSBL)).Select(t => t.sNS_HSBL));
                //}
                //else if (mlnsLoai == "TenSNSPCTN")
                //{
                //    mlnsLC = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sNS_LuongChinh)).Select(t => t.sNS_LuongChinh));
                //    mlnsPCCV = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sNS_PCCV)).Select(t => t.sNS_PCCV));
                //    mlnsPCTNVK = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sNS_PCTNVK)).Select(t => t.sNS_PCTNVK));
                //    mlnsHSBL = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sNS_HSBL)).Select(t => t.sNS_HSBL));
                //}
                //else if (mlnsLoai == "TenSNSPCTNVK")
                //{
                //    mlnsLC = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sNS_LuongChinh)).Select(t => t.sNS_LuongChinh));
                //    mlnsPCCV = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sNS_PCCV)).Select(t => t.sNS_PCCV));
                //    mlnsPCTN = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sNS_PCTN)).Select(t => t.sNS_PCTN));
                //    mlnsHSBL = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sNS_HSBL)).Select(t => t.sNS_HSBL));
                //}
                //else if (mlnsLoai == "TenNSHSBL")
                //{
                //    mlnsLC = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sNS_LuongChinh)).Select(t => t.sNS_LuongChinh));
                //    mlnsPCCV = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sNS_PCCV)).Select(t => t.sNS_PCCV));
                //    mlnsPCTN = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sNS_PCTN)).Select(t => t.sNS_PCTN));
                //    mlnsPCTNVK = string.Join(";", bhxhs.Where(t => !string.IsNullOrEmpty(t.sNS_PCTNVK)).Select(t => t.sNS_PCTNVK));
                //}
                var sql = from mlns in mlnss
                          //where mlns.NamLamViec == namLamViec && (mlns.BHangCha || (!mlnsList.Contains(mlns.XauNoiMa) && !mlnsLC.Contains(mlns.XauNoiMa) && !mlnsPCCV.Contains(mlns.XauNoiMa) && !mlnsPCTN.Contains(mlns.XauNoiMa) && !mlnsPCTNVK.Contains(mlns.XauNoiMa) && !mlnsHSBL.Contains(mlns.XauNoiMa)))
                          where mlns.NamLamViec == namLamViec && (mlns.BHangCha || !mlnsList.Contains(mlns.XauNoiMa))
                          select new { mlns = mlns };
                return sql.ToList().Select(t =>
                {
                    var nsMucLucNganSach = t.mlns;
                    return nsMucLucNganSach;
                }).ToList();
            }
        }

        public string FindByBHXHMucLucIn(int namLamViec, string mlnsLoai, string mlnsBhxh)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var bhxhs = ctx.BhDmMucLucNganSachs.Where(t => t.INamLamViec == namLamViec && t.SXauNoiMa == mlnsBhxh).FirstOrDefault();
                
                switch (mlnsLoai)
                {
                    case "TenSNSLuongChinh":
                        return bhxhs.SNS_LuongChinh ?? string.Empty;
                    case "TenSNSPCCV":
                        return bhxhs.SNS_PCCV ?? string.Empty;
                    case "TenSNSPCTN":
                        return bhxhs.SNS_PCTN ?? string.Empty;
                    case "TenSNSPCTNVK":
                        return bhxhs.SNS_PCTNVK ?? string.Empty;
                    case "TenSNSHSBL":
                        return bhxhs.SNS_HSBL ?? string.Empty;
                    default:
                        return string.Empty;
                }
            }
        }

        public IEnumerable<NsMucLucNganSach> FindLNSStartWith2ByNsDonVi(IEnumerable<string> excludeMLNS, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var predicate = PredicateBuilder.True<NsMucLucNganSach>();
                predicate = predicate.And(t => t.NamLamViec == namLamViec);
                predicate = predicate.And(t => t.Lns.StartsWith("2") || t.Lns.StartsWith("4") || t.Lns.StartsWith("109"));
                predicate = predicate.And(p => string.IsNullOrEmpty(p.L) &&
                    string.IsNullOrEmpty(p.K) &&
                    string.IsNullOrEmpty(p.M) &&
                    string.IsNullOrEmpty(p.Tm) &&
                    string.IsNullOrEmpty(p.Ttm) &&
                    string.IsNullOrEmpty(p.Ng) &&
                    string.IsNullOrEmpty(p.Tng));
                // find all mlns start with 2
                IEnumerable<NsMucLucNganSach> result = ctx.NsMucLucNganSaches.Where(predicate).ToList();
                // find all lns
                return result.Where(t => IsLNS(t, result) && !excludeMLNS.Contains(t.Lns)).OrderBy(t => t.Lns).ToList();
            }
        }

        public IEnumerable<NsMucLucNganSach> GetLoaiNganSachByNamLamViec(int iNamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsMucLucNganSaches.Where(n => n.ITrangThai == 1 && n.NamLamViec == iNamLamViec
                                                        && string.IsNullOrEmpty(n.L) && string.IsNullOrEmpty(n.K)
                                                        && string.IsNullOrEmpty(n.M) && string.IsNullOrEmpty(n.Tm)
                                                        && string.IsNullOrEmpty(n.Ttm) && string.IsNullOrEmpty(n.Ng)
                                                        && string.IsNullOrEmpty(n.Tng)).ToList();
            }
        }

        public IEnumerable<ReportMLNSQuery> ReportMLNS(int namLamViec, Guid mlnsId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.rpt_dm_mlns @YearOfWork, @MLNS_ID";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", namLamViec),
                    new SqlParameter("@MLNS_ID", mlnsId)
                };
                return ctx.FromSqlRaw<ReportMLNSQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NsMucLucNganSach> FindAllLnsStartWith2(AuthenticationInfo authenticationInfo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var predicate = PredicateBuilder.True<NsMucLucNganSach>();
                predicate = predicate.And(x => x.NamLamViec == authenticationInfo.YearOfWork);
                predicate = predicate.And(t => t.Lns.StartsWith("2") || t.Lns.StartsWith("4") || t.Lns.StartsWith("109"));
                predicate = predicate.And(p => string.IsNullOrEmpty(p.L) &&
                    string.IsNullOrEmpty(p.K) &&
                    string.IsNullOrEmpty(p.M) &&
                    string.IsNullOrEmpty(p.Tm) &&
                    string.IsNullOrEmpty(p.Ttm) &&
                    string.IsNullOrEmpty(p.Ng) &&
                    string.IsNullOrEmpty(p.Tng));
                IEnumerable<NsMucLucNganSach> LNS = ctx.NsMucLucNganSaches.Where(predicate).ToList();
                return LNS.Where(mlns => IsLNS(mlns, LNS)).ToList();
            }
        }

        public bool IsLNS(NsMucLucNganSach mlns, IEnumerable<NsMucLucNganSach> ListOfMlns)
        {
            // Nếu không phải lns thì ko dc thêm b quản lý
            if (string.IsNullOrEmpty(mlns.Lns))
            {
                return false;
            }
            if (!StringUtils.IsNullOrEmpty(mlns.L) ||
                !StringUtils.IsNullOrEmpty(mlns.K) ||
                !StringUtils.IsNullOrEmpty(mlns.M) ||
                !StringUtils.IsNullOrEmpty(mlns.Tm) ||
                !StringUtils.IsNullOrEmpty(mlns.Ttm) ||
                !StringUtils.IsNullOrEmpty(mlns.Ng) ||
                !StringUtils.IsNullOrEmpty(mlns.Tng))
            {
                return false;
            }
            // Nếu có dòng con là lns thì ko dc thêm
            IEnumerable<NsMucLucNganSach> children = ListOfMlns.Where(p => p.MlnsIdParent.Equals(mlns.MlnsId));
            bool hasLnsChild = children.Any(p => !string.IsNullOrEmpty(p.Lns) &&
                StringUtils.IsNullOrEmpty(p.L) &&
                StringUtils.IsNullOrEmpty(p.K) &&
                StringUtils.IsNullOrEmpty(p.M) &&
                StringUtils.IsNullOrEmpty(p.Tm) &&
                StringUtils.IsNullOrEmpty(p.Ttm) &&
                StringUtils.IsNullOrEmpty(p.Ng) &&
                StringUtils.IsNullOrEmpty(p.Tng));
            return !hasLnsChild;
        }

        public void UpdateDonViOfMLNS(IEnumerable<NsMucLucNganSach> entities, string idMaDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                foreach (NsMucLucNganSach n in entities)
                {
                    n.IdMaDonVi = idMaDonVi;
                }
                ctx.UpdateRange(entities);
                ctx.SaveChanges();
            }
        }
        public IEnumerable<NsMucLucNganSach> FindByNamLamViec(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var mlnss = ctx.NsMucLucNganSaches;
                var phongBans = ctx.NsPhongBans;
                var q = from mlns in mlnss
                        where mlns.NamLamViec == namLamViec
                        join phongBan in phongBans
                        on new { idpb = mlns.IdPhongBan, nam = mlns.NamLamViec } equals new { idpb = phongBan.IIDMaBQuanLy, nam = phongBan.INamLamViec } into ps
                        from p in ps.DefaultIfEmpty()
                        select new { MLNS = mlns, TenPhongBan = p != null ? p.STenBQuanLy : string.Empty };
                var result = q.ToList();
                return result.Select(t =>
                {
                    var nsMucLucNganSach = t.MLNS;
                    nsMucLucNganSach.TenPhongBan = t.TenPhongBan;
                    return nsMucLucNganSach;
                }).ToList();
            }
        }

        public IEnumerable<NsMucLucNganSach> FindByNamLamViec(IEnumerable<int> namLamViecs)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsMucLucNganSaches.Where(ns => namLamViecs.Contains(ns.NamLamViec.Value)).ToList();
            }
        }

        public bool IsUsedMLNS(Guid mlnsId, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_check_used_mlns @YearOfWork, @MLNS_ID, @iResult OUT";
                var iResult = new SqlParameter
                {
                    ParameterName = "@iResult",
                    DbType = System.Data.DbType.Boolean,
                    Direction = System.Data.ParameterDirection.Output
                };
                var parameters = new[]
                {
                    iResult,
                    new SqlParameter("@YearOfWork", namLamViec),
                    new SqlParameter("@MLNS_ID", mlnsId)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
                return (bool)iResult.Value;
            }
        }

        public IEnumerable<string> GetAllUsedDuToanMlns(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_check_used_dutoan_chitiet_toi @YearOfWork";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", namLamViec)
                };
                List<XauNoiMaQuery> rs = ctx.FromSqlRaw<XauNoiMaQuery>(sql, parameters).ToList();
                return rs.Select(t => t.SXauNoiMa).ToList();
            }
        }

        public IEnumerable<string> GetAllUsedQuyetToanMlns(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_check_used_quyettoan_chitiet_toi @YearOfWork";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", namLamViec)
                };
                List<XauNoiMaQuery> rs = ctx.FromSqlRaw<XauNoiMaQuery>(sql, parameters).ToList();
                return rs.Select(t => t.SXauNoiMa).ToList();
            }
        }

        public IEnumerable<string> GetAllUsedMlns(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_list_used_mlns @YearOfWork";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", namLamViec)
                };
                List<XauNoiMaQuery> rs = ctx.FromSqlRaw<XauNoiMaQuery>(sql, parameters).ToList();
                return rs.Select(t => t.SXauNoiMa).ToList();
            }
        }

        public int AddOrUpdateRange(IEnumerable<NsMucLucNganSach> entities, int iNamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                foreach (var entity in entities)
                {
                    if (entity.IsDeleted)
                    {
                        if (!entity.Id.Equals(Guid.Empty))
                        {
                            // remove map mlns_mlskt
                            IEnumerable<NsMlsktMlns> nsMlsktMlns = ctx.NsMlsktMlns.Where(i => i.INamLamViec == iNamLamViec && i.SNsXauNoiMa.Equals(entity.XauNoiMa));
                            ctx.RemoveRange(nsMlsktMlns);
                            ctx.Remove(entity);
                        }
                    }
                    else if (entity.IsModified)
                    {
                        if (!entity.Id.Equals(Guid.Empty))
                        {
                            ctx.Update(entity);
                        }
                        else
                        {
                            ctx.Set<NsMucLucNganSach>().Add(entity);
                        }
                    }
                }
                return ctx.SaveChanges();
            }
        }

        public void AddRangeWithMLSKT(List<NsMucLucNganSach> listMlns)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var nsMlsktMlns = listMlns.Where(i => i.SktMucLucMap != null).Select(t => t.SktMucLucMap).ToList();
                ctx.NsMlsktMlns.AddRange(nsMlsktMlns);
                ctx.NsMucLucNganSaches.AddRange(listMlns);
                ctx.SaveChanges();
            }
        }

        public IEnumerable<NsMucLucNganSachQuery> FindByMLNSNamLamViec(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                //   string query = @"with lns as
                //(
                //  select * from NS_MucLucNganSach where iNamLamViec = @INamLamViec
                //  and sL = '' and sK = '' and sM = '' and sTM = '' and sTTM = '' and sNG = '' and sTNG = ''
                //),
                //finalLns as (
                //select iID_MLNS from lns where (select count(*) from lns t1 where t1.iID_MLNS_Cha = lns.iID_MLNS) = 0),
                //tmp1 as
                // (select distinct sXauNoiMa from NS_DT_ChungTuChiTiet where iNamLamViec = @INamLamViec
                //  union 
                //  select sXauNoiMa from NS_Nganh_ChungTuChiTiet where iNamLamViec = @INamLamViec
                //  union 
                //  select sXauNoiMa from NS_Nganh_ChungTuChiTiet_PhanCap where iNamLamViec = @INamLamViec
                //  union 
                //  select sXaunoiMa from NS_DTDauNam_ChungTuChiTiet where iNamLamViec = @INamLamViec
                //  union
                //  select sXaunoiMa from NS_DTDauNam_PhanCap where iNamLamViec = @INamLamViec),

                //tmp2 as (select distinct sxaunoima from NS_QT_ChungTuChiTiet where iNamLamViec = @INamLamViec),
                //tmp3 as (select distinct iid_mlns from NS_BK_ChungTu t1 left join NS_MucLucNganSach t2 on t1.sXauNoiMa = t2.sXauNoiMa and t1.iNamLamViec = @INamLamViec
                // union
                // select iid_mlns from NS_CP_ChungTuChiTiet where iNamLamViec = @INamLamViec
                // union 
                // select iid_mlns from NS_DT_ChungTuChiTiet where iNamLamViec = @INamLamViec
                // union 
                // select iid_mlns from NS_Nganh_ChungTuChiTiet where iNamLamViec = @INamLamViec
                // union 
                // select iid_mlns from NS_Nganh_ChungTuChiTiet_PhanCap where iNamLamViec = @INamLamViec
                // union 
                // select iid_mlns from NS_QT_ChungTuChiTiet where iNamLamViec = @INamLamViec
                // union
                // select iid_mlns from NS_DTDauNam_ChungTuChiTiet t1 left join NS_MucLucNganSach t2 on t1.sXauNoiMa = t2.sXauNoiMa and t1.iNamLamViec = t2.iNamLamViec where t1.iNamLamViec = @INamLamViec
                // union
                // select iid_mlns from NS_DTDauNam_PhanCap ),
                //   tmp4 as (select * from NS_MLSKT_MLNS where iNamLamViec = @INamLamViec),
                //root as (select * from NS_MucLucNganSach where iNamLamViec = @INamLamViec)

                //select r.*, tmp1.sXauNoiMa as UsedDuToanChiTietToi, 
                //tmp2.sXauNoiMa as UsedQuyetToanChiTietToi, tmp3.iID_MLNS as UsedMLNS, finalLns.iID_MLNS as LNSID,
                //   parent.sXauNoiMa as MlnsParentName, parent.iID_MLNS as iID_MLNS_Cha, tmp4.sSKT_KyHieu as SktKyHieu
                //from root r
                //left join tmp1 on r.sXauNoiMa = tmp1.sXauNoiMa and iNamLamViec = @INamLamViec
                //left join tmp2 on r.sXauNoiMa = tmp2.sXauNoiMa and iNamLamViec = @INamLamViec
                //left join tmp3 on r.iID_MLNS = tmp3.iID_MLNS and iNamLamViec = @INamLamViec
                //left join tmp4 on r.sXauNoiMa = tmp4.sNS_XauNoiMa and tmp4.iNamLamViec = @INamLamViec
                //left join root parent on r.iID_MLNS_Cha = parent.iID_MLNS
                //left join finalLns on finalLns.iID_MLNS = r.iID_MLNS order by r.sxaunoima";
                //   //query = "select * from NS_MucLucNganSach where iNamLamViec = @INamLamViec order by sxaunoima";

                string sql = "EXECUTE dbo.sp_get_mlns_year @INamLamViec";

                var parameters = new[]
                    {
                    new SqlParameter("@INamLamViec", namLamViec)
                };

                var rs = ctx.FromSqlRaw<NsMucLucNganSachQuery>(sql, parameters).ToList();

                foreach (var nsMucLucNganSach in rs)
                {
                    nsMucLucNganSach.IsEditableCPChiTietToi = nsMucLucNganSach.LNSID.HasValue;
                    nsMucLucNganSach.IsUsedDuToanChiTietToi = "Ng".Equals(getTypeOfMlns(nsMucLucNganSach)) && !string.IsNullOrEmpty(nsMucLucNganSach.UsedDuToanChiTietToi);
                    nsMucLucNganSach.IsUsedQuyetToanChiTietToi = !string.IsNullOrEmpty(nsMucLucNganSach.UsedQuyetToanChiTietToi);
                    nsMucLucNganSach.IsEditableStatus = !nsMucLucNganSach.UsedMLNS.HasValue;
                }
                return rs;
            }
        }

        private string getTypeOfMlns(NsMucLucNganSachQuery entity)
        {
            List<string> mlnsType = new List<string>
            {
                "Tng3", "Tng2", "Tng1", "Tng", "Ng", "Ttm", "Tm", "M", "K", "L", "Lns"
            };
            foreach (string type in mlnsType)
            {
                PropertyInfo propertyInfo = typeof(NsMucLucNganSachQuery).GetProperty(type);
                object val = propertyInfo.GetValue(entity, null);
                if (val != null && !string.IsNullOrWhiteSpace(val.ToString()))
                {
                    return type;
                }
            }
            return "";
        }

        public List<NsMucLucNganSach> FindAllNotIn(List<string> xnms, int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                //var data = ctx.NsMucLucNganSaches.Where(c => c.NamLamViec == yearOfWork && !xnms.Contains(c.XauNoiMa)).OrderBy(a => a.XauNoiMa);
                //return data.ToList();
                var executeQuery = "EXECUTE dbo.sp_ns_mlns_exclude @iNamLamViec, @mlnsexclude";
                DataTable dt = DBExtension.ConvertDataToStringTable(xnms);
                var parameters = new[]
                {
                    new SqlParameter("@iNamLamViec",yearOfWork),
                    new SqlParameter("@mlnsexclude", dt.AsTableValuedParameter("t_tbl_string"))
                };
                return ctx.FromSqlRaw<NsMucLucNganSach>(executeQuery, parameters).ToList();
            }
        }

        public IEnumerable<MucLucNganSachCheckDataQuery> FindHasDataMLNS(int yearOfWork, string sXauNoiMa, int loai)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeQuery = "EXECUTE sp_check_data_used_mlns @YearOfWork, @CodeChain, @Type";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork),
                    new SqlParameter("@CodeChain", sXauNoiMa),
                    new SqlParameter("@Type", loai)
                };
                return ctx.FromSqlRaw<MucLucNganSachCheckDataQuery>(executeQuery, parameters).ToList();
            }
        }

        public void DeleteHasDataMLNS(string sXauNoiMa, string loai, Guid uniqueidentifier)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeQuery = "EXECUTE sp_delete_data_mlns_by_type @CodeChain, @Type, @VoucherID";
                var parameters = new[]
                {
                    new SqlParameter("@CodeChain", sXauNoiMa),
                    new SqlParameter("@Type", loai),
                    new SqlParameter("@VoucherID", uniqueidentifier)
                };
                ctx.Database.ExecuteSqlCommand(executeQuery, parameters);
            }
        }


        public void UpdateIsHangChaMucLucNganSach(List<Guid> lstParentId, int iNamLamViec)
        {
            if (lstParentId == null || lstParentId.Count == 0) return;

            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeQuery = "EXECUTE sp_ns_refresh_parent_mlns @iNamLamViec, @parentIds";
                DataTable dt = DBExtension.ConvertDataToGuidTable(lstParentId);
                SqlParameter dtDetailParam = new SqlParameter("parentIds", SqlDbType.Structured);
                dtDetailParam.TypeName = "t_tbl_uniqueidentifier";
                dtDetailParam.Value = dt;
                var parameters = new[]
                {
                    new SqlParameter("@iNamLamViec", iNamLamViec),
                    dtDetailParam
                };
                ctx.Database.ExecuteSqlCommand(executeQuery, parameters);
            }
        }
        public NsMucLucNganSach FindByMLNS(string xau, int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.NsMucLucNganSaches.Where(t=> t.NamLamViec == yearOfWork && t.XauNoiMa == xau).FirstOrDefault();
                return result;
            }
        }
    }
}
