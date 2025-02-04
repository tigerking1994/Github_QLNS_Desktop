using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Extensions;
using System.Text;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDaHopDongRepository : Repository<NhDaHopDong>, INhDaHopDongRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDaHopDongRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhDaHopDong> FindAll(AuthenticationInfo authenticationInfo)
        {
            return FindAll();
        }

        public IEnumerable<NhDaHopDongQuery> FindAllHopDong(int? iThuocMenu = null)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_nh_thongtin_hopdong_index @iThuocMenu";
                var parameters = new[]
                {
                    new SqlParameter("@iThuocMenu", iThuocMenu)
                };
                return ctx.FromSqlRaw<NhDaHopDongQuery>(sql, parameters).ToList();
            }
        }
         public IEnumerable<NhDaHopDongQuery> FindAllHopDongtrongnuoc(int? iThuocMenu = null)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_nh_thongtin_hopdong_goithau_index @iThuocMenu";
                var parameters = new[]
                {
                    new SqlParameter("@iThuocMenu", iThuocMenu)
                };
                return ctx.FromSqlRaw<NhDaHopDongQuery>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NhDaHopDongQuery> FindAllHopDongNgoaiThuong(int? iThuocMenu = null)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_nh_thongtin_hopdong_ngoaithuong_index @iThuocMenu";
                var parameters = new[]
                {
                    new SqlParameter("@iThuocMenu", iThuocMenu)
                };
                return ctx.FromSqlRaw<NhDaHopDongQuery>(sql, parameters).ToList();
            }
        }

        public void DeleteHopDong(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var idParam = new SqlParameter("@Id", id.ToString());
                ctx.Database.ExecuteSqlCommand($"DELETE FROM Nh_Da_HopDong WHERE Id = @Id" +
                    $" DELETE Nh_Da_HopDong_CacQuyetDinh WHERE iId_HopDongID = @Id" +
                    $" DELETE Nh_Da_HopDong_HangMuc WHERE iID_HopDong_ChiPhiID IN(SELECT ID FROM Nh_Da_HopDong_ChiPhi WHERE iId_HopDongID = @Id)" +
                    $" DELETE Nh_Da_HopDong_ChiPhi WHERE iId_HopDongID = @Id" +
                    $" DELETE Nh_Da_HopDong_NguonVon WHERE iId_HopDongID = @Id", idParam);
            }
        }

        public IEnumerable<NhDaHopDongTrongNuocQuery> FindAllHopDongTrongNuoc()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<NhDaHopDongTrongNuocQuery>("EXECUTE dbo.sp_nh_hopdongtrongnuoc_index").ToList();
            }
        }
		public IEnumerable<NhDaHopDong> FindByIdKHTongTheNhiemVuChi(Guid? idKHTongTheNhiemVuChi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT hd.* FROM NH_DA_HopDong hd ");
                query.AppendLine("INNER JOIN NH_KHTongThe_NhiemVuChi nvc ON nvc.ID = hd.iID_KHTongThe_NhiemVuChiID ");
                query.AppendLine("WHERE hd.bIsActive = 1 ");
                if (idKHTongTheNhiemVuChi != null && !idKHTongTheNhiemVuChi.Equals(Guid.Empty))
                {
                    query.AppendLine("AND nvc.ID = @chuongTrinhID ");
                }
                query.AppendLine("ORDER BY hd.sTenHopDong ");
                var parameters = new[]
                {
                    new SqlParameter("@chuongTrinhID", idKHTongTheNhiemVuChi)
                };
                return ctx.FromSqlRaw<NhDaHopDong>(query.ToString(), parameters).ToList();
            }
        }

        public IEnumerable<NhDaHopDongQuery> FindByIdDonVi(Guid? IIdDonViQuanLyId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT hd.*,dm_nvc.sTenNhiemVuChi as STenNhiemVuChi, dm_nvc.ID as IIdKhTongTheNhiemVuChiId, hd.iID_DuAnID as IIdDuAnId, da.sTenDuAn, dmnt.sTenNhaThau as STenNhaThauThucHien FROM NH_DA_HopDong hd ");
                query.AppendLine("INNER JOIN NH_KHTongThe_NhiemVuChi nvc ON nvc.ID = hd.iID_KHTongThe_NhiemVuChiID ");
                query.AppendLine("INNER JOIN NH_DM_NhiemVuChi dm_nvc ON dm_nvc.ID = nvc.iID_NhiemVuChiID ");
                query.AppendLine("LEFT JOIN NH_DA_DuAn as da on da.ID = hd.iID_DuAnID ");
                query.AppendLine("LEFT JOIN NH_DM_NhaThau as dmnt on dmnt.Id = hd.iID_NhaThauThucHienID ");
                query.AppendLine("WHERE hd.bIsActive = 1 ");
                if (IIdDonViQuanLyId != null && !IIdDonViQuanLyId.Equals(Guid.Empty))
                {
                    query.AppendLine("AND hd.iID_DonViQuanLyID = @IIdDonViQuanLyId ");
                }
                query.AppendLine("ORDER BY hd.sTenHopDong ");
                var parameters = new[]
                {
                    new SqlParameter("@IIdDonViQuanLyId", IIdDonViQuanLyId)
                };
                return ctx.FromSqlRaw<NhDaHopDongQuery>(query.ToString(), parameters).ToList();
            }
        }

        

        public int AddOrUpdateRange(IEnumerable<NhDaHopDong> entities, int iNamLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                foreach (var entity in entities)
                {
                    if (entity.IsDeleted)
                    {
                        //if (!entity.Id.Equals(Guid.Empty))
                        //{
                        //    // remove map mlns_mlskt
                        //    IEnumerable<NsMlsktMlns> nsMlsktMlns = ctx.NsMlsktMlns.Where(i => i.INamLamViec == iNamLamViec && i.SNsXauNoiMa.Equals(entity.XauNoiMa));
                        //    ctx.RemoveRange(nsMlsktMlns);
                        //    ctx.Remove(entity);
                        //}
                    }
                    else if (entity.IsModified)
                    {
                        if (!entity.Id.Equals(Guid.Empty))
                        {
                            ctx.Update(entity);
                        }
                        else
                        {
                            ctx.Set<NhDaHopDong>().Add(entity);
                        }
                    }
                }
                return ctx.SaveChanges();
            }
        }
        public void AddOrUpdateRange(IEnumerable<NhDaHopDong> entities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            using (var ctx = _contextFactory.CreateDbContext())
            {
                foreach (var entity in entities)
                {
                    var current = ctx.NhDaHopDongs.AsNoTracking().FirstOrDefault(i => i.Id.Equals(entity.Id));
                    if (entity.IsDeleted)
                    {
                        if (!entity.Id.Equals(Guid.Empty))
                        {
                            ctx.Remove(entity);
                        }
                    }
                    else
                    {
                        if (!entity.Id.Equals(Guid.Empty) && current != null)
                        {
                            ctx.Update(entity);
                        }
                        else
                        {
                            entity.DNgayTao = time;
                            entity.SNguoiTao = authenticationInfo.Principal;
                            ctx.Add(entity);
                        }
                    }
                }
                ctx.SaveChanges();
            }
        }

        public IEnumerable<NhDmLoaiHopDong> GetAllLoaiHopDong()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<NhDmLoaiHopDong>("SELECT * , iID_LoaiHopDongID as Id FROM NH_DM_LoaiHopDong").ToList();
            }
        }
    }
}
