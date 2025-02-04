using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDaHopDongHangMucRepository : Repository<NhDaHopDongHangMuc>, INhDaHopDongHangMucRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDaHopDongHangMucRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhDaHopDongHangMuc> FindByHopDongChiPhi(Guid idHopDongChiPhi)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhDaHopDongHangMucs.Where(x => x.IIdHopDongChiPhiId == idHopDongChiPhi).ToList();
            }
        }

        public void DeleteHopDongHangMucTrongNuoc(Guid? IIdHopDongChiPhiId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = $"DELETE FROM NH_DA_HopDong_HangMuc WHERE iID_HopDong_ChiPhiID = @iIdHopDongChiPhiId ";
                var parameters = new object[]
                {
                    new SqlParameter("@iIdHopDongChiPhiId", IIdHopDongChiPhiId.IsNullOrEmpty() ? DBNull.Value : (object)IIdHopDongChiPhiId)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public void AddOrUpdate(Guid chiPhiId, IEnumerable<NhDaHopDongHangMuc> items)
        {
            if (!items.Any()) return;

            var listAdded = items.Where(s => s.IsAdded && !s.IsDeleted).ToList();
            if (!listAdded.IsEmpty())
            {
                foreach (var item in listAdded)
                {
                    item.Id = Guid.NewGuid();
                    item.IIdHopDongChiPhiId = chiPhiId;
                }
                this.AddRange(listAdded);
            }

            var listModified = items.Where(s => s.IsModified && !s.IsAdded && !s.IsDeleted).ToList();
            if (!listModified.IsEmpty())
            {
                foreach (var item in listModified)
                {
                    item.IIdHopDongChiPhiId = chiPhiId;
                }
                this.UpdateRange(listModified);
            }

            var listDeleted = items.Where(s => s.IsDeleted).ToList();
            if (!listDeleted.IsEmpty())
            {
                this.RemoveRange(listDeleted);
            }
        }

        public void AddOrUpdateListHangMuc(Guid chiPhiId, IEnumerable<NhDaHopDongHangMuc> items)
        {
            if (!items.Any()) return;

            var listAdded = items.Where(s => s.IsAdded && !s.IsDeleted).ToList();
            if (!listAdded.IsEmpty())
            {
                foreach (var item in listAdded)
                {
                    item.IIdHopDongChiPhiId = chiPhiId;
                }
                this.AddRange(listAdded);
            }

            var listModified = items.Where(s => s.IsModified && !s.IsAdded && !s.IsDeleted).ToList();
            if (!listModified.IsEmpty())
            {
                foreach (var item in listModified)
                {
                    item.IIdHopDongChiPhiId = chiPhiId;
                }
                this.UpdateRange(listModified);
            }

            var listDeleted = items.Where(s => s.IsDeleted).ToList();
            if (!listDeleted.IsEmpty())
            {
                this.RemoveRange(listDeleted);
            }
        }

        public IEnumerable<NhDaHopDongHangMuc> FindByIdHopDong(Guid IdHopDong)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var executeQuery = "select * from NH_DA_HopDong_HangMuc where iID_HopDong_ID = @IdHopDong order by sMaHangMuc";
                var parameters = new[]
                {
                    new SqlParameter("IdHopDong", IdHopDong),
                };
                return ctx.NhDaHopDongHangMucs.FromSql(executeQuery, parameters).ToList();
            }
        }

        public IEnumerable<NhDaGoiThauHopDongHangMucQuery> FindByIdGoiThau(Guid IdGoiThau)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idGoiThauParam = new SqlParameter("IdGoiThau", IdGoiThau);
                return ctx.FromSqlRaw<NhDaGoiThauHopDongHangMucQuery>("EXECUTE sp_nh_gethangmucbyidgoithau @IdGoiThau", idGoiThauParam).ToList();
            }
        }
    }
}
