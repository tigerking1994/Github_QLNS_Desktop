using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class SktSoLieuChungTuRepository : Repository<NsDtdauNamChungTu>, ISktSoLieuChungTuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public SktSoLieuChungTuRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void UpdateTotalChungTu(string idDonVi, string loaiDonVi, int loaiChungTu, int namLamViec, int namNganSach, int loaiNNS, int nguonNganSach)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.cc @IdDonVi, @LoaiDonVi, @LoaiChungTu, @NamLamViec, @NamNganSach, @iLoaiNNS, @NguonNganSach";
                var parameters = new[]
                {
                    new SqlParameter("@IdDonVi", idDonVi),
                    new SqlParameter("@LoaiDonVi", loaiDonVi),
                    new SqlParameter("@LoaiChungTu", loaiChungTu),
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@NamNganSach", namNganSach),
                    new SqlParameter("@iLoaiNNS", loaiNNS),
                    new SqlParameter("@NguonNganSach", nguonNganSach)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public int GetSoChungTuIndexByCondition(int namLamViec, int nguonNganSach, int namNganSach, int loaiChungTu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var iMaxIndex = ctx.NsDtdauNamChungTus.Where(n => n.INamLamViec == namLamViec && n.IIdMaNguonNganSach == nguonNganSach
                    && n.INamNganSach == namNganSach).Max(n => n.ISoChungTuIndex);
                return iMaxIndex + 1;
            }
        }

        public void UpdateTotalChungTu(string idDonVi, string loaiDonVi, int loaiChungTu, int namLamViec, int namNganSach, int nguonNganSach, int loaiNNS, string chungTuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_skt_update_total_dutoandaunam_2 @IdDonVi, @LoaiDonVi, @LoaiChungTu, @NamLamViec, @NamNganSach, @NguonNganSach, @iLoaiNNS, @ChungTuId";
                var parameters = new[]
                {
                    new SqlParameter("@IdDonVi", idDonVi),
                    new SqlParameter("@LoaiDonVi", loaiDonVi),
                    new SqlParameter("@LoaiChungTu", loaiChungTu),
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@NamNganSach", namNganSach),
                    new SqlParameter("@NguonNganSach", nguonNganSach),
                    new SqlParameter("@iLoaiNNS", loaiNNS),
                    new SqlParameter("@ChungTuId", chungTuId)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public void UpdateChildChungTu(int loaiChungTu, int namLamViec, int namNganSach, int nguonNganSach, string chungTuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_skt_update_child_dutoandaunam @LoaiChungTu, @NamLamViec, @NamNganSach, @NguonNganSach, @ChungTuId";
                var parameters = new[]
                {
                    new SqlParameter("@LoaiChungTu", loaiChungTu),
                    new SqlParameter("@NamLamViec", namLamViec),
                    new SqlParameter("@NamNganSach", namNganSach),
                    new SqlParameter("@NguonNganSach", nguonNganSach),
                    new SqlParameter("@ChungTuId", chungTuId)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public List<NsDtdauNamChungTu> GetDataExportJson(List<Guid> lstId)
        {
            List<NsDtdauNamChungTu> results = new List<NsDtdauNamChungTu>();
            Dictionary<Guid, List<NsDtdauNamChungTuChiTiet>> dicChiTiet = new Dictionary<Guid, List<NsDtdauNamChungTuChiTiet>>();
            Dictionary<Guid, List<JsonNsDtDauNamChungTuChiTietCanCuQuery>> dicCanCu = new Dictionary<Guid, List<JsonNsDtDauNamChungTuChiTietCanCuQuery>>();

            using (var ctx = _contextFactory.CreateDbContext())
            {
                results = ctx.NsDtdauNamChungTus.Where(n => lstId.Contains(n.Id)).ToList();
                IEnumerable<NsDtdauNamChungTuChiTiet> lstChiTiet = ctx.NsDtdauNamChungTuChiTiets.Where(n => n.IIdCtdtdauNam.HasValue && lstId.Contains(n.IIdCtdtdauNam.Value));
                if (lstChiTiet != null)
                {
                    IEnumerable<Guid> lstIdChiTIet = lstChiTiet.Select(n => n.Id);
                    IEnumerable<NsDtdauNamPhanCap> dataPhanCap = ctx.NsDtdauNamPhanCaps.Where(n => lstId.Contains(n.IIdCtdtdauNamChiTiet));
                    if (dataPhanCap != null)
                    {
                        Dictionary<Guid, List<NsDtdauNamPhanCap>> dicPhanCap = dataPhanCap.GroupBy(n => n.IIdCtdtdauNamChiTiet).ToDictionary(n => n.Key, n => n.ToList());
                        foreach (var item in lstChiTiet)
                        {
                            if (dicPhanCap.ContainsKey(item.Id))
                                item.ListDtDauNamPhanCap = dicPhanCap[item.Id].OrderBy(n => n.SXauNoiMa).ToList();
                        }
                    }
                    dicChiTiet = lstChiTiet.GroupBy(n => n.IIdCtdtdauNam.Value).ToDictionary(n => n.Key, n => n.ToList());
                }
            }

            IEnumerable<JsonNsDtDauNamChungTuChiTietCanCuQuery> lstCanCu = GetDuToanDauNamChiTietCanCuByChungTuId(lstId);
            if (lstCanCu != null)
            {
                dicCanCu = lstCanCu.GroupBy(n => (Guid)n.IID_CTDTDauNam).ToDictionary(n => n.Key, n => n.ToList());
            }
            foreach (var item in results)
            {
                if (dicChiTiet.ContainsKey(item.Id))
                    item.ListDetail = dicChiTiet[item.Id].OrderBy(n => n.SXauNoiMa).ToList();
                if (dicCanCu.ContainsKey(item.Id))
                    item.ListDtDauNamCanCu = dicCanCu[item.Id].OrderBy(n => n.SXauNoiMa).ToList();
            }
            return results;
        }

        private IEnumerable<JsonNsDtDauNamChungTuChiTietCanCuQuery> GetDuToanDauNamChiTietCanCuByChungTuId(List<Guid> Ids)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeQuery = "EXECUTE sp_ns_dutoandaunamchitietcancu_by_chungtuid @iIdsChungTu";
                DataTable dt = DBExtension.ConvertDataToGuidTable(Ids);
                var parameters = new[]
                {
                    new SqlParameter("@iIdsChungTu", dt.AsTableValuedParameter("t_tbl_uniqueidentifier"))
                };
                return ctx.FromSqlRaw<JsonNsDtDauNamChungTuChiTietCanCuQuery>(executeQuery, parameters).ToList();
            }
        }

        public void DeleteCtdnctByDeleteMlns(Guid iID_CTDTDauNam, string sMLNS, int iNamLamViec)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var sql = "EXECUTE dbo.sp_ns_ldtdn_delete_chitiet_by_delete_mlns @iID_CTDTDauNam, @sMLNS, @iNamLamViec";
                var parameters = new[]
                {
                    new SqlParameter("@iID_CTDTDauNam", iID_CTDTDauNam),
                    new SqlParameter("@sMLNS", sMLNS),
                    new SqlParameter("@iNamLamViec", iNamLamViec)
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }
    }
}
