using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhDmMucLucNganSachRepository : Repository<BhDmMucLucNganSach>, IBhDmMucLucNganSachRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public BhDmMucLucNganSachRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int AddOrUpdateRange(IEnumerable<BhDmMucLucNganSach> entities, int iNamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                foreach (var entity in entities)
                {
                    if (entity.IsDeleted)
                    {
                        if (!entity.Id.Equals(Guid.Empty))
                        {
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
                            ctx.Set<BhDmMucLucNganSach>().Add(entity);
                        }
                    }
                }
                return ctx.SaveChanges();
            }
        }

        public IEnumerable<BhDmMucLucNganSach> FindAll(AuthenticationInfo authenticationInfo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDmMucLucNganSachs.Where(b => b.INamLamViec == authenticationInfo.YearOfWork).ToList();
            }
        }

        public IEnumerable<BhDmMucLucNganSachQuery> FindByMLNSNamLamViec(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@NamLamViec", namLamViec);
                var result = ctx.FromSqlRaw<BhDmMucLucNganSachQuery>("EXECUTE dbo.sp_get_mlns_bhxh_by_year_of_work @NamLamViec", namLamViecParam).ToList();
                foreach (var bhMucLucNganSach in result)
                {
                    bhMucLucNganSach.IsEditableStatus = !bhMucLucNganSach.UsedMLNS.HasValue;
                    bhMucLucNganSach.IsUsedQuyetToanChiTietToi = !string.IsNullOrEmpty(bhMucLucNganSach.UsedQuyetToanChiTietToi);
                    bhMucLucNganSach.IsUsedDuToanChiTietToi = !string.IsNullOrEmpty(bhMucLucNganSach.UsedDuToanChiTietToi);
                    bhMucLucNganSach.IsEditableCPChiTietToi = !string.IsNullOrEmpty(bhMucLucNganSach.UsedCPChiTietToi);
                    bhMucLucNganSach.IsUsedDuToanDieuChinhChiTietToi = !string.IsNullOrEmpty(bhMucLucNganSach.UsedDuToanDieuChinhChiTietToi);

                }
                return result;
            }
        }

        public List<BhDmMucLucNganSach> GetListBhMucLucNs(int namLamViec, string khoiDuToanBHXH, string khoiHachToanBHXH)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                Func<BhDmMucLucNganSach, bool> defaultKhtBhxhMucLucFilter = x => x.INamLamViec == namLamViec && (x.SLNS == khoiDuToanBHXH || x.SLNS == khoiHachToanBHXH);
                var NsMucLucsChild = ctx.BhDmMucLucNganSachs.AsNoTracking().AsEnumerable().Where(defaultKhtBhxhMucLucFilter).ToList();

                var nsMucLucs = new List<BhDmMucLucNganSach>();
                if (NsMucLucsChild.Count > 0)
                {
                    var listIdMlKht = NsMucLucsChild.Select(item => item.IIDMLNS).ToList();
                    nsMucLucs = NsMucLucsChild;
                    while (true)
                    {
                        var listIdParent = NsMucLucsChild.Where(x => !listIdMlKht.Contains(x.IIDMLNSCha.GetValueOrDefault())).Select(x => x.IIDMLNSCha).ToList();
                        var listParent1 = ctx.BhDmMucLucNganSachs.Where(x => listIdParent.Contains(x.IIDMLNS) && x.INamLamViec == namLamViec).ToList();
                        if (listParent1.Count > 0)
                        {
                            var lstId = listParent1.Select(item => item.IIDMLNS).ToList();
                            listIdMlKht.AddRange(lstId);
                            nsMucLucs.AddRange(listParent1);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                nsMucLucs = nsMucLucs.GroupBy(x => x.IIDMLNS).Select(x => x.First()).OrderBy(x => x.SXauNoiMa).ToList();
                return nsMucLucs;
            }
        }

        public List<BhDmMucLucNganSach> GetListBhMucLucNsForQLKP(int inamLamViec, string khoi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                Func<BhDmMucLucNganSach, bool> defaultKhtBhxhMucLucFilter = x => x.INamLamViec == inamLamViec && (x.SLNS == khoi);
                var NsMucLucsChild = ctx.BhDmMucLucNganSachs.AsNoTracking().AsEnumerable().Where(defaultKhtBhxhMucLucFilter).ToList();

                var nsMucLucs = new List<BhDmMucLucNganSach>();
                if (NsMucLucsChild.Count > 0)
                {
                    var listIdMlKht = NsMucLucsChild.Select(item => item.IIDMLNS).ToList();
                    nsMucLucs = NsMucLucsChild;
                    while (true)
                    {
                        var listIdParent = NsMucLucsChild.Where(x => !listIdMlKht.Contains(x.IIDMLNSCha.GetValueOrDefault())).Select(x => x.IIDMLNSCha).ToList();
                        var listParent1 = ctx.BhDmMucLucNganSachs.Where(x => listIdParent.Contains(x.IIDMLNS) && x.INamLamViec == inamLamViec).ToList();
                        if (listParent1.Count > 0)
                        {
                            var lstId = listParent1.Select(item => item.IIDMLNS).ToList();
                            listIdMlKht.AddRange(lstId);
                            nsMucLucs.AddRange(listParent1);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                nsMucLucs = nsMucLucs.GroupBy(x => x.IIDMLNS).Select(x => x.First()).OrderBy(x => x.SXauNoiMa).ToList();
                return nsMucLucs;
            }
        }

        public List<BhDmMucLucNganSach> GetListBhytMucLucNs(int namLamViec, string loaiNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var nsMucLucs = ctx.BhDmMucLucNganSachs.AsNoTracking().AsEnumerable().Where(x => x.INamLamViec == namLamViec && x.SLNS.StartsWith(loaiNS)).ToList();
                if (nsMucLucs.Count > 0)
                {
                    var listIdMlKht = nsMucLucs.Select(item => item.IIDMLNS).ToList();
                    while (true)
                    {
                        var listIdParent = nsMucLucs.Where(x => !listIdMlKht.Contains(x.IIDMLNSCha.GetValueOrDefault())).Select(x => x.IIDMLNSCha).ToList();
                        var listParent1 = ctx.BhDmMucLucNganSachs.Where(x => listIdParent.Contains(x.IIDMLNS) && x.INamLamViec == namLamViec).ToList();
                        if (listParent1.Count > 0)
                        {
                            var lstId = listParent1.Select(item => item.IIDMLNS).ToList();
                            listIdMlKht.AddRange(lstId);
                            nsMucLucs.AddRange(listParent1);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                nsMucLucs = nsMucLucs.GroupBy(x => x.IIDMLNS).Select(x => x.First()).OrderBy(x => x.SXauNoiMa).ToList();
                return nsMucLucs;
            }
        }

        public List<BhDmMucLucNganSach> GetListMucLucForDanhMucLoaiChi(int namLamViec, string SLNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var dataSLNS = SLNS.Split(',');
                var iTrangThai = StatusType.ACTIVE;
                List<BhDmMucLucNganSach> bhMucLucsChild = new List<BhDmMucLucNganSach>();
                bhMucLucsChild = ctx.BhDmMucLucNganSachs.AsNoTracking().AsEnumerable().Where(x => dataSLNS.Contains(x.SLNS) && x.ITrangThai == iTrangThai && x.INamLamViec == namLamViec).ToList();

                List<BhDmMucLucNganSach> bhMucLucs = new List<BhDmMucLucNganSach>();
                if (bhMucLucsChild.Count > 0)
                {
                    var listIdMlskt = bhMucLucsChild.Select(item => item.IIDMLNS).ToList();
                    bhMucLucs = bhMucLucsChild;
                    while (true)
                    {
                        var test = bhMucLucsChild.Where(x => !listIdMlskt.Contains(x.IIDMLNSCha.GetValueOrDefault())).ToList();
                        var listIdParent = bhMucLucsChild.Where(x => !listIdMlskt.Contains(x.IIDMLNSCha.GetValueOrDefault())).Select(x => x.IIDMLNSCha).ToList();
                        var listParent1 = ctx.BhDmMucLucNganSachs.Where(x => listIdParent.Contains(x.IIDMLNS)).ToList();
                        if (listParent1.Count > 0)
                        {
                            var lstId = listParent1.Select(item => item.IIDMLNS).ToList();
                            listIdMlskt.AddRange(lstId);
                            bhMucLucs.AddRange(listParent1);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                bhMucLucs = bhMucLucs.GroupBy(x => x.IIDMLNS).Select(x => x.First()).OrderBy(x => x.SXauNoiMa).ToList();
                return bhMucLucs;
            }
        }

        public IEnumerable<BhDmMucLucNganSach> FindByListLnsDonVi(string lns, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                if (String.IsNullOrEmpty(lns))
                {
                    return new List<BhDmMucLucNganSach>();
                }
                List<string> listId = lns.Split(',').ToList();
                return ctx.BhDmMucLucNganSachs.Where(n => listId.Contains(n.SLNS)
                                                         && n.INamLamViec == namLamViec
                                                         && string.IsNullOrEmpty(n.SL)
                                                         && string.IsNullOrEmpty(n.SK)
                                                         && string.IsNullOrEmpty(n.SM)
                                                         && string.IsNullOrEmpty(n.STM)
                                                         && string.IsNullOrEmpty(n.STTM)
                                                         && string.IsNullOrEmpty(n.SNG)).ToList();
            }
        }


        public bool IsUsedMLNS(Guid mlnsId, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_check_used_mlns_bhxh @YearOfWork, @MLNS_ID, @iResult OUT";
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

        public IEnumerable<ReportMLNSQuery> ReportMLNS(int namLamViec, Guid mlnsId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_rpt_dm_mlns_bhxh @YearOfWork, @MLNS_ID";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", namLamViec),
                    new SqlParameter("@MLNS_ID", mlnsId)
                };
                return ctx.FromSqlRaw<ReportMLNSQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<BhDmMucLucNganSach> FindForDieuChinh(int namLamViec, string donVi, Guid loaiDanhMucCapChi, DateTime ngayChungTu, string userName)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@namLamViec", namLamViec);

                SqlParameter donViParam = new SqlParameter("@donVi", donVi);
                SqlParameter loaiDanhMucCapChiParam = new SqlParameter("@loaiDanhMucCapChi", loaiDanhMucCapChi);
                SqlParameter ngayChungTuParam = new SqlParameter("@ngayChungTu", ngayChungTu);
                SqlParameter userNameParam = new SqlParameter("@userName", userName);
                return ctx.BhDmMucLucNganSachs.FromSql("EXECUTE dbo.sp_dt_dieuchinh_get_lns @namLamViec, @donVi, @loaiDanhMucCapChi, @ngayChungTu, @userName",
                    namLamViecParam, donViParam, loaiDanhMucCapChiParam, ngayChungTuParam, userNameParam).ToList();
            }
        }

        public IEnumerable<BhDmMucLucNganSach> FindForDieuChinhDTT(int namLamViec, string donVi, DateTime ngayChungTu, string userName)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@namLamViec", namLamViec);

                SqlParameter donViParam = new SqlParameter("@donVi", donVi);
                SqlParameter ngayChungTuParam = new SqlParameter("@ngayChungTu", ngayChungTu);
                SqlParameter userNameParam = new SqlParameter("@userName", userName);
                return ctx.BhDmMucLucNganSachs.FromSql("EXECUTE dbo.sp_bh_dieu_chinh_dtt_get_lns @namLamViec, @donVi, @ngayChungTu, @userName",
                    namLamViecParam, donViParam, ngayChungTuParam, userNameParam).ToList();
            }
        }

        public IEnumerable<BhDmMucLucNganSach> FindByUser(string userName, int yearOfWork, string LNSExcept)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter userNameParam = new SqlParameter("@UserName", userName);
                SqlParameter yearOfWorkParam = new SqlParameter("@YearOfWork", yearOfWork);
                SqlParameter lnsExceptParam = new SqlParameter("@LNSExcept", LNSExcept);
                return ctx.BhDmMucLucNganSachs.FromSql("EXECUTE dbo.sp_bh_qtt_bhxh_mlns_get_by_user @UserName, @YearOfWork, @LNSExcept"
                    , userNameParam, yearOfWorkParam, lnsExceptParam).ToList();
            }
        }

        public IEnumerable<BhDmMucLucNganSach> FindSLNSForQTCQKPQL(int namLamViec, string donVi, int iQuy, DateTime ngayChungTu, string userName)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@namLamViec", namLamViec);
                SqlParameter donViParam = new SqlParameter("@donVi", donVi);
                SqlParameter loaiDanhMucCapChiParam = new SqlParameter("@Quy", iQuy);
                SqlParameter ngayChungTuParam = new SqlParameter("@ngayChungTu", ngayChungTu);
                SqlParameter userNameParam = new SqlParameter("@userName", userName);
                return ctx.BhDmMucLucNganSachs.FromSql("EXECUTE dbo.sp_qtc_get_lns_for_qkpql @namLamViec, @donVi, @Quy, @ngayChungTu, @userName",
                    namLamViecParam, donViParam, loaiDanhMucCapChiParam, ngayChungTuParam, userNameParam).ToList();
            }
        }

        public IEnumerable<BhDmMucLucNganSach> FindSLNSForQTCQKPK(int namLamViec, string donVi, int iQuy, DateTime ngayChungTu, string userName, Guid idLoaiChi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@namLamViec", namLamViec);
                SqlParameter donViParam = new SqlParameter("@donVi", donVi);
                SqlParameter loaiDanhMucCapChiParam = new SqlParameter("@Quy", iQuy);
                SqlParameter ngayChungTuParam = new SqlParameter("@ngayChungTu", ngayChungTu);
                SqlParameter userNameParam = new SqlParameter("@userName", userName);
                SqlParameter userLoaiChiParam = new SqlParameter("@LoaiChi", idLoaiChi);
                return ctx.BhDmMucLucNganSachs.FromSql("EXECUTE dbo.sp_qtc_get_lns_for_qkpk @namLamViec, @donVi, @Quy, @ngayChungTu, @userName, @LoaiChi",
                    namLamViecParam, donViParam, loaiDanhMucCapChiParam, ngayChungTuParam, userNameParam, userLoaiChiParam).ToList();
            }
        }

        public IEnumerable<BhDmMucLucNganSach> FindSLNSForQTCQBHXH(int namLamViec, string donVi, int iQuy, DateTime ngayChungTu, string userName)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@namLamViec", namLamViec);
                SqlParameter donViParam = new SqlParameter("@donVi", donVi);
                SqlParameter loaiDanhMucCapChiParam = new SqlParameter("@Quy", iQuy);
                SqlParameter ngayChungTuParam = new SqlParameter("@ngayChungTu", ngayChungTu);
                SqlParameter userNameParam = new SqlParameter("@userName", userName);
                return ctx.BhDmMucLucNganSachs.FromSql("EXECUTE dbo.sp_qtc_get_lns_for_qbhxh @namLamViec, @donVi, @Quy, @ngayChungTu, @userName",
                    namLamViecParam, donViParam, loaiDanhMucCapChiParam, ngayChungTuParam, userNameParam).ToList();
            }
        }

        public IEnumerable<BhDmMucLucNganSach> FindSLNSForQTCQKCB(int yearOfWork, string agencyIds, int iQuy, DateTime dt, string principal)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@namLamViec", yearOfWork);
                SqlParameter donViParam = new SqlParameter("@donVi", agencyIds);
                SqlParameter loaiDanhMucCapChiParam = new SqlParameter("@Quy", iQuy);
                SqlParameter ngayChungTuParam = new SqlParameter("@ngayChungTu", dt);
                SqlParameter userNameParam = new SqlParameter("@userName", principal);
                return ctx.BhDmMucLucNganSachs.FromSql("EXECUTE dbo.sp_qtc_get_lns_for_qkcb @namLamViec, @donVi, @Quy, @ngayChungTu, @userName",
                    namLamViecParam, donViParam, loaiDanhMucCapChiParam, ngayChungTuParam, userNameParam).ToList();
            }
        }

        public List<BhDmMucLucNganSach> FindSLNSForQTCNBHXH(int yearOfWork, string agencyIds, DateTime dtime, string principal)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@namLamViec", yearOfWork);
                SqlParameter donViParam = new SqlParameter("@donVi", agencyIds);
                SqlParameter ngayChungTuParam = new SqlParameter("@ngayChungTu", dtime);
                SqlParameter userNameParam = new SqlParameter("@userName", principal);
                return ctx.BhDmMucLucNganSachs.FromSql("EXECUTE dbo.sp_qtc_get_lns_for_nbhxh @namLamViec, @donVi, @ngayChungTu, @userName",
                    namLamViecParam, donViParam, ngayChungTuParam, userNameParam).ToList();
            }
        }

        public List<BhDmMucLucNganSach> FindSLNSForQTCNKCB(int yearOfWork, string agencyIds, DateTime dtime, string principal)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@namLamViec", yearOfWork);
                SqlParameter donViParam = new SqlParameter("@donVi", agencyIds);
                SqlParameter ngayChungTuParam = new SqlParameter("@ngayChungTu", dtime);
                SqlParameter userNameParam = new SqlParameter("@userName", principal);
                return ctx.BhDmMucLucNganSachs.FromSql("EXECUTE dbo.sp_qtc_get_lns_for_nkcb @namLamViec, @donVi, @ngayChungTu, @userName",
                    namLamViecParam, donViParam, ngayChungTuParam, userNameParam).ToList();
            }
        }

        public List<BhDmMucLucNganSach> FindSLNSForQTCNKPQL(int yearOfWork, string agencyIds, DateTime dtime, string principal)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@namLamViec", yearOfWork);
                SqlParameter donViParam = new SqlParameter("@donVi", agencyIds);
                SqlParameter ngayChungTuParam = new SqlParameter("@ngayChungTu", dtime);
                SqlParameter userNameParam = new SqlParameter("@userName", principal);
                return ctx.BhDmMucLucNganSachs.FromSql("EXECUTE dbo.sp_qtc_get_lns_for_nkpql @namLamViec, @donVi, @ngayChungTu, @userName",
                    namLamViecParam, donViParam, ngayChungTuParam, userNameParam).ToList();
            }
        }

        public List<BhDmMucLucNganSach> FindSLNSForQTCNKPK(int yearOfWork, string agencyIds, DateTime dt, string principal, Guid idLoaiChi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@namLamViec", yearOfWork);
                SqlParameter donViParam = new SqlParameter("@donVi", agencyIds);
                SqlParameter ngayChungTuParam = new SqlParameter("@ngayChungTu", dt);
                SqlParameter userNameParam = new SqlParameter("@userName", principal);
                SqlParameter userLoaiChiParam = new SqlParameter("@LoaiChi", idLoaiChi);
                return ctx.BhDmMucLucNganSachs.FromSql("EXECUTE dbo.sp_qtc_get_lns_for_nkpk @namLamViec, @donVi, @ngayChungTu, @userName, @LoaiChi",
                    namLamViecParam, donViParam, ngayChungTuParam, userNameParam, userLoaiChiParam).ToList();
            }
        }

        public List<BhDmMucLucNganSachQuery> GetMLNSCheDoBHXH(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@namLamViec", yearOfWork);
                return ctx.FromSqlRaw<BhDmMucLucNganSachQuery>("EXECUTE dbo.sp_bhxh_qtcq_get_mlns_che_do_bhxh @namLamViec", namLamViecParam).ToList();
            }
        }

        public List<BhDmMucLucNganSach> GetByLnsDieuChinhDuToan(int yearOfWork, string sLNS)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter namLamViecParam = new SqlParameter("@namLamViec", yearOfWork);
                SqlParameter sLNSParam = new SqlParameter("@SLNS", sLNS);
                return ctx.BhDmMucLucNganSachs.FromSql("EXECUTE dbo.sp_dt_muclucngansach_theodieuchinh @namLamViec, @SLNS",
                    namLamViecParam, sLNSParam).ToList();
            }
        }
    }
}
