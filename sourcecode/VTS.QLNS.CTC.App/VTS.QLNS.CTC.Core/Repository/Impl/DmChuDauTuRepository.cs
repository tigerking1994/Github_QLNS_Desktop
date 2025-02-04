using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class DmChuDauTuRepository : Repository<DmChuDauTu>, IDmChuDauTuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public DmChuDauTuRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public List<DmChuDauTu> FindByDuAnId(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var cdtIds = ctx.NhDaDuAn.Where(t => t.Id.Equals(id)).Select(t => t.IIdChuDauTuId).ToList();
                return ctx.DmChuDauTus.Where(t => cdtIds.Contains(t.Id)).ToList();
            }
        }

        public DmChuDauTu FindByMaDonVi(string iIDMaDonVi, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.DmChuDauTus.FirstOrDefault(x => x.INamLamViec == namLamViec && x.IIDMaDonVi == iIDMaDonVi);
            }
        }

        public DmChuDauTu FindAllByMaDonVi(string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.DmChuDauTus.Where(x => x.IIDMaDonVi == maDonVi).FirstOrDefault();
            }
        }

        public IEnumerable<DmChuDauTu> FindByNamLamViec(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.DmChuDauTus.Where(x => x.INamLamViec == yearOfWork).ToList();
            }
        }

        public IEnumerable<DmChuDauTu> FindByAllDataDonVi()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.DmChuDauTus.ToList();
            }
        }

        public IEnumerable<DmChuDauTu> FindByAll()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.DmChuDauTus.ToList();
            }
        }

        public DmChuDauTu FindByParentId(Guid id, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.DmChuDauTus.Where(x => x.INamLamViec == namLamViec && x.IIDDonViCha.HasValue && x.IIDDonViCha == id).FirstOrDefault();
            }
        }

        public DmChuDauTu FindAllByParentId(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.DmChuDauTus.Where(x => x.IIDDonViCha.HasValue && x.IIDDonViCha == id).FirstOrDefault();
            }
        }

        public List<DmChuDauTu> FindByIdDonViCha(Guid id, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.DmChuDauTus.Where(x => x.INamLamViec == namLamViec && x.IIDDonViCha.HasValue && x.IIDDonViCha == id).ToList();
            }
        }

        public List<DmChuDauTu> FindByAllIdDonViCha(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.DmChuDauTus.Where(x => x.IIDDonViCha.HasValue && x.IIDDonViCha == id).ToList();
            }
        }

        public IEnumerable<DmChuDauTu> FindDmChuDauTuByByNamLamViec(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_dm_cdt @YearOfWork";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", namLamViec)
                };
                return ctx.Set<DmChuDauTu>().FromSql(sql, parameters).ToList();
            }
        }

        public void UpdateBHangChaToTrue(IEnumerable<Guid> ids)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                IEnumerable<DmChuDauTu> parents = ctx.DmChuDauTus.Where(t => ids.Contains(t.Id));
                foreach (DmChuDauTu dmChuDauTu in parents)
                {
                    dmChuDauTu.BHangCha = true;
                }
                ctx.SaveChanges();
            }
        }

        public List<DmChuDauTu> FindByDuAnId(List<Guid> ids)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var cdtIds = ctx.NhDaDuAn.Where(t => ids.Contains(t.Id)).Select(t => t.IIdChuDauTuId).ToList();
                return ctx.DmChuDauTus.Where(t => cdtIds.Contains(t.Id)).ToList();
            }
        }
        public DmChuDauTu FindByIdDuAn(Guid idDuAn)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = @"SELECT cdt.*,cdt.sTenDonVi AS STenChuDauTu
                            FROM NH_DA_DuAn as da
                            INNER JOIN DM_ChuDauTu as cdt on da.iID_ChuDauTuID = cdt.iID_DonVi
                            WHERE da.ID = @iIdDuAnId";
                var parameters = new[]
                {
                    new SqlParameter("@iIdDuAnId", idDuAn)
                };
                var data = ctx.Set<DmChuDauTu>().FromSql(sql, parameters).ToList();
                if (data == null) return new DmChuDauTu();
                return data.FirstOrDefault();
            }
        }

        public DmChuDauTu GetChuDauTuByVdtDuAnId(Guid iIdDuAnId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = @"SELECT cdt.*
                            FROM VDT_DA_DuAn as da
                            INNER JOIN DM_ChuDauTu as cdt on da.iID_ChuDauTuID = cdt.iID_DonVi
                            WHERE da.iID_DuAnID = @iIdDuAnId";
                var parameters = new[]
                {
                    new SqlParameter("@iIdDuAnId", iIdDuAnId)
                };
                var data = ctx.Set<DmChuDauTu>().FromSql(sql, parameters).ToList();
                if (data == null) return new DmChuDauTu();
                return data.FirstOrDefault();
            }
        }
    }
}
