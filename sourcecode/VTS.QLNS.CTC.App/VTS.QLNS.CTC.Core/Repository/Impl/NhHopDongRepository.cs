using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhHopDongRepository : Repository<NhHopDong>, INhHopDongRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhHopDongRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhHopDongQuery> FindAllWithTiGia()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "select hd.*, "
                    + "tg.sMaTienTe1 sMaTienTe1UsdVnd, tg.sMaTienTe2 sMaTienTe2UsdVnd ,tg.fTiGiaHoiDoai fTiGia1, "
                    + "tg2.sMaTienTe1 sMaTienTe1NgoaiTeKhacUsd, tg2.sMaTienTe2 sMaTienTe2NgoaiTeKhacUsd , tg2.fTiGiaHoiDoai fTiGia2 "
                    + "FROM NH_HopDong hd "
                    + "LEFT JOIN NH_DM_TiGia tg ON hd.iID_TiGiaUSD_VNDID = tg.ID "
                    + "LEFT JOIN NH_DM_TiGia tg2 ON hd.iID_TiGiaUSD_NgoaiTeKhacID = tg2.ID ";
                return ctx.FromSqlRaw<NhHopDongQuery>(sql).ToList();
            }
        }

        public NhHopDong FindById(Guid idHopDong)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhHopDongs.FirstOrDefault(n => n.Id == idHopDong);
            }
        }

        public NhHopDong FindBySoHopSong(string soHopDong)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhHopDongs.FirstOrDefault(n => n.SSoHopDong == soHopDong);
            }
        }

        public bool IsExistByTiGia(Guid idTiGia)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhHopDongs.Any(n => n.IIdTiGiaUsdVndId == idTiGia || n.IIdTiGiaUsdNgoaiTeKhacID == idTiGia);
            }
        }
    }
}
