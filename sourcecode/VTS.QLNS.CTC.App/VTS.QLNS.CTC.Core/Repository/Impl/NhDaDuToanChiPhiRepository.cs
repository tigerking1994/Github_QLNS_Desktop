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
    public class NhDaDuToanChiPhiRepository : Repository<NhDaDuToanChiPhi>, INhDaDuToanChiPhiRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDaDuToanChiPhiRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public void AddAdjust(Guid duToanNguonVonId, IEnumerable<NhDaDuToanChiPhi> entities)
        {
            List<NhDaDuToanChiPhi> lstAdded = entities.Where(x => x.IsAdded && !x.IsDeleted).ToList();
            if (lstAdded.Any())
            {
                foreach (var item in lstAdded)
                {
                    item.IIdDuToanNguonVonId = duToanNguonVonId;
                }
                this.AddRange(lstAdded);
            }
            List<NhDaDuToanChiPhi> lstModified = entities.Where(x => x.IsModified && !x.IsAdded && !x.IsDeleted).ToList();
            if (lstModified.Any())
            {
                foreach (var item in lstModified)
                {
                    item.IIdDuToanNguonVonId = duToanNguonVonId;
                }
                this.UpdateRange(lstModified);
            }

            List<NhDaDuToanChiPhi> lstDeleted = entities.Where(x => x.IsDeleted && !x.IsAdded).ToList();
            if (lstDeleted.Any())
            {
                this.RemoveRange(lstDeleted);
            }
        }
        public void AddOrUpdate(IEnumerable<NhDaDuToanChiPhi> entities)
        {
            List<NhDaDuToanChiPhi> lstAdded = entities.Where(x => x.IsAdded && !x.IsDeleted).ToList();
            if (lstAdded.Any())
            {
                /*foreach (var item in lstAdded)
                {
                    item.IIdDuToanNguonVonId = IdDuToanNguonVonId;
                }*/
                this.AddRange(lstAdded);
            }

            List<NhDaDuToanChiPhi> lstModified = entities.Where(x => x.IsModified && !x.IsAdded && !x.IsDeleted).ToList();
            if (lstModified.Any())
            {
                /*foreach (var item in lstModified)
                {
                    item.IIdDuToanNguonVonId = IdDuToanNguonVonId;
                }*/
                this.UpdateRange(lstModified);
            }

            List<NhDaDuToanChiPhi> lstDeleted = entities.Where(x => x.IsDeleted && !x.IsAdded).ToList();
            if (lstDeleted.Any())
            {
                this.RemoveRange(lstDeleted);
            }
        }

        public void DeleteByDuAnId(Guid duToanId)
        {
            var lstDeleted = this.FindAll(x => x.IIdDuToanId == duToanId);
            if (!lstDeleted.IsEmpty())
            {
                this.RemoveRange(lstDeleted);
            }
        }

        public IEnumerable<NhDaDetailChiPhiQuery> GetAllByDuToanId(Guid iIdDuToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE sp_nh_DuToan_chiphi @iIdDuToanId";
                var parameters = new[] {
                    new SqlParameter("@iIdDuToanId", iIdDuToanId)
                };
                return ctx.FromSqlRaw<NhDaDetailChiPhiQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<NhDaDuToanChiPhi> FindByDuToanId(Guid iIdDuToanId)
        {
            throw new NotImplementedException();
        }
    }
}
