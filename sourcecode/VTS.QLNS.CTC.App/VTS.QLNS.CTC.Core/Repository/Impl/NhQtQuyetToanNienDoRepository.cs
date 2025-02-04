using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhQtQuyetToanNienDoRepository : Repository<NhQtQuyetToanNienDo>, INhQtQuyetToanNienDoRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhQtQuyetToanNienDoRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhQtQuyetToanNienDoQuery> FindIndex()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_quyettoan_niendo_index";
                return ctx.FromSqlRaw<NhQtQuyetToanNienDoQuery>(executeSql, new { }).ToList();
            }
        }

        public IEnumerable<NhQtQuyetToanNienDoQuery> FindTongHopIndex()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_quyettoan_niendo_tonghop_index";
                return ctx.FromSqlRaw<NhQtQuyetToanNienDoQuery>(executeSql, new { }).ToList();
            }
        }

        public IEnumerable<ReportNhQtQuyetToanNienDoNamQuery> ReportNam(Guid quyetToanNienDoId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_rpt_quyettoan_niendo_nam @Id";
                var parameters = new[]
                {
                    new SqlParameter("@Id", quyetToanNienDoId)
                };
                return ctx.FromSqlRaw<ReportNhQtQuyetToanNienDoNamQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<ReportNhQtQuyetToanNienDoQuyQuery> ReportQuy(Guid quyetToanNienDoId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_rpt_quyettoan_niendo_quy @Id";
                var parameters = new[]
                {
                    new SqlParameter("@Id", quyetToanNienDoId)
                };
                return ctx.FromSqlRaw<ReportNhQtQuyetToanNienDoQuyQuery>(executeSql, parameters).ToList();
            }
        }
        public bool CheckDuplicateSoQD(string soQuyetDinh, Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                List<NhQtQuyetToanNienDo> listQTND = new List<NhQtQuyetToanNienDo>();
                if (id != Guid.Empty)
                {
                    listQTND = ctx.NhQtQuyetToanNienDos.Where(x => x.SSoDeNghi == soQuyetDinh && x.Id != id).ToList();
                }
                else
                {
                    listQTND = ctx.NhQtQuyetToanNienDos.Where(x => x.SSoDeNghi == soQuyetDinh).ToList();
                }

                if (listQTND.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool CheckDuplicateQTND(Guid? IIdDonViId, int? INamKeHoach, Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                List<NhQtQuyetToanNienDo> listQTND = new List<NhQtQuyetToanNienDo>();
                if (id != Guid.Empty)
                {
                    listQTND = ctx.NhQtQuyetToanNienDos.Where(x => x.IIdDonViId == IIdDonViId && x.INamKeHoach == INamKeHoach && x.Id != id).ToList();
                }
                else
                {
                    listQTND = ctx.NhQtQuyetToanNienDos.Where(x => x.IIdDonViId == IIdDonViId && x.INamKeHoach == INamKeHoach).ToList();
                }

                if (listQTND.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
