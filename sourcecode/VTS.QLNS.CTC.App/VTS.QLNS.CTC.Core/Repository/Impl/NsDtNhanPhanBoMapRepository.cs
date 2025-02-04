using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NsDtNhanPhanBoMapRepository : Repository<NsDtNhanPhanBoMap>, INsDtNhanPhanBoMapRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NsDtNhanPhanBoMapRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void DeleteByIdNhanPhanBoDuToan(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter Id_NhanDuToanParam = new SqlParameter("@Id_NhanDuToanParam", id.ToString());
                ctx.Database.ExecuteSqlCommand($"DELETE FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_Nhan = @Id_NhanDuToanParam", Id_NhanDuToanParam);
            }
        }

        public void DeleteByIdPhanBoDuToan(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter idPhanBoDuToanParam = new SqlParameter("@idPhanBoDuToanParam", id.ToString());
                ctx.Database.ExecuteSqlCommand($"DELETE FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_PhanBo = @idPhanBoDuToanParam", idPhanBoDuToanParam);
            }
        }

        public void RemoveDuplicate()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.Database.ExecuteSqlCommand(@$"DELETE T FROM (
                                                SELECT DupRank = ROW_NUMBER() OVER (
                                                                PARTITION BY iID_CTDuToan_Nhan, iID_CTDuToan_PhanBo
                                                                ORDER BY (SELECT NULL)
                                                            )
                                                FROM NS_DT_Nhan_PhanBo_Map
                                                ) AS T
                                                WHERE DupRank > 1");
            }
        }

        public IEnumerable<NsDtNhanPhanBoMap> FindByListIdNhanDuToan(IEnumerable<string> listIdNhanDuToan)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDtNhanPhanBoMaps.Where(n => listIdNhanDuToan.Contains(n.IIdCtduToanNhan.ToString())).ToList();
            }
        }

        public IEnumerable<NsDtNhanPhanBoMap> FindByListIdPhanBo(IEnumerable<string> listIdPhanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDtNhanPhanBoMaps.Where(n => listIdPhanBo.Contains(n.IIdCtduToanPhanBo.ToString())).ToList();
            }
        }

        public IEnumerable<NsDtNhanPhanBoMap> FindByIdPhanBoDuToan(string idPhanBoDuToan)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDtNhanPhanBoMaps.Where(n => n.IIdCtduToanPhanBo.ToString() == idPhanBoDuToan).ToList();
            }
        }
        public IEnumerable<NsDtNhanPhanBoMap> FindByIdPhanBoDuToanDieuChinh(string idPhanBoDuToan)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var dotPb = ctx.NsDtNhanPhanBoMaps.Where(n => n.IIdCtduToanPhanBo.ToString() == idPhanBoDuToan).ToList();
                var rs = ctx.NsDtNhanPhanBoMaps.Where(n=> dotPb.Any(pb=>pb.IIdCtduToanPhanBo == n.IIdCtduToanNhan)).ToList();
                rs.ForEach(r => r.IIdCtduToanPhanBo = new Guid(idPhanBoDuToan));
                return rs;
            }
        }

        public IEnumerable<NsDtNhanPhanBoMap> FindListIdByListIdPhanBo(IEnumerable<string> listIdPhanBo, int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<NsDtNhanPhanBoMap>($@"
                    with x as
                    (select b.iID_DTChungTu x1, c.iID_DTChungTu x2 from NS_DT_Nhan_PhanBo_Map a
                    inner join NS_DT_ChungTu b ON a.iID_CTDuToan_Nhan = b.iID_DTChungTu
                    inner join NS_DT_ChungTu c ON a.iID_CTDuToan_PhanBo = c.iID_DTChungTu
                    where b.iNamLamViec = {yearOfWork}),

                    ycte as 

                    (select x1, x2 from x
                    union all
                    select k.x1, x.x2 from ycte k
                    join x on k.x2 = x.x1
                    )
                    select distinct x1 IIdCtduToanNhan from ycte where x2 in ('{string.Join("','", listIdPhanBo)}')
                ");
            }
        }

        public IEnumerable<NsDtNhanPhanBoMap> FindByIdNhanDuToan(Guid idNhanDuToan)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsDtNhanPhanBoMaps.Where(n => n.IIdCtduToanNhan == idNhanDuToan).ToList();
            }
        }
    }
}