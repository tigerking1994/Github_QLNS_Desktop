using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhQtChuyenQuyetToanRepository : Repository<NhQtChuyenQuyetToan>, INhQtChuyenQuyetToanRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhQtChuyenQuyetToanRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public bool CheckExistsCQTByTimeAndDonvi(Guid idCQT, Guid iID_DonViID, int loaiThoiGian, int thoiGian)
        {
            NhQtChuyenQuyetToan cqtObj = FirstOrDefault(x => x.iID_DonViID.Value.Equals(iID_DonViID)
                                                            && x.iLoaiThoiGian.Value == loaiThoiGian
                                                            && x.iThoiGian.Value == thoiGian
                                                            && !x.Id.Equals(idCQT));
            return cqtObj != null;
        }

        public IEnumerable<NhQtChuyenQuyetToanQuery> FindIndex()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_chuyendulieu_quyettoan_index";
                return ctx.FromSqlRaw<NhQtChuyenQuyetToanQuery>(executeSql).ToList();
            }
        }

        public void Save(NhQtChuyenQuyetToan entity)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var exist = ctx.NhQtChuyenQuyetToans.Any(t => t.Id.Equals(entity.Id));
                if (exist)
                    ctx.Update(entity);
                else
                    ctx.Add(entity);
                ctx.SaveChanges();
            }
        }

        public void SaveNhQtChuyenQuyetToanChiTiet(List<NhQtChuyenQuyetToanChiTiet> entities, Guid nhQtChuyenQuyetToanId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var existChiTiet = ctx.NhQtChuyenQuyetToanChiTiets.Where(t => t.iID_ChuyenQuyetToanID.Equals(nhQtChuyenQuyetToanId)).ToList();
                ctx.NhQtChuyenQuyetToanChiTiets.RemoveRange(existChiTiet);
                ctx.NhQtChuyenQuyetToanChiTiets.AddRange(entities);
                ctx.SaveChanges();
            }
        }
    }
}
