using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDaQdDauTuHangMucRepository : Repository<NhDaQdDauTuHangMuc>, INhDaQdDauTuHangMucRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDaQdDauTuHangMucRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhDaDetailHangMucQuery> GetHangMucByQdDauTuId(Guid iIdQdDauTuId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE sp_nh_qddautu_hangmuc @iIdQdDauTuId";
                var parameters = new[] {
                    new SqlParameter("@iIdQdDauTuId", iIdQdDauTuId)
                };
                return ctx.FromSqlRaw<NhDaDetailHangMucQuery>(executeSql, parameters).ToList();
            }
        }

        public void AddOrUpdate(Guid qdDauTuChiPhiId, IEnumerable<NhDaQdDauTuHangMuc> items)
        {
            if (!items.IsEmpty())
            {
                List<NhDaQdDauTuHangMuc> listAdd = items.Where(x => x.IsAdded && !x.IsDeleted).ToList();
                if (!listAdd.IsEmpty())
                {
                    foreach (var item in listAdd)
                    {
                        item.IIdQdDauTuChiPhiId = qdDauTuChiPhiId;
                    }
                    this.AddRange(listAdd);
                }

                List<NhDaQdDauTuHangMuc> listUpdate = items.Where(x => x.IsModified && !x.IsAdded && !x.IsDeleted).ToList();
                if (!listUpdate.IsEmpty())
                {
                    foreach (var item in listUpdate)
                    {
                        item.IIdQdDauTuChiPhiId = qdDauTuChiPhiId;
                    }
                    this.UpdateRange(listUpdate);
                }

                List<NhDaQdDauTuHangMuc> listDelete = items.Where(x => x.IsDeleted && !x.IsAdded).ToList();
                if (!listDelete.IsEmpty())
                {
                    this.RemoveRange(listDelete);
                }
            }
        }

        public void DeleteByQdDauTuChiPhiId(Guid qdDauTuChiPhiId)
        {
            var lstDeleted = this.FindAll(x => x.IIdQdDauTuChiPhiId == qdDauTuChiPhiId);
            if (!lstDeleted.IsEmpty())
            {
                this.RemoveRange(lstDeleted);
            }
        }
    }
}
