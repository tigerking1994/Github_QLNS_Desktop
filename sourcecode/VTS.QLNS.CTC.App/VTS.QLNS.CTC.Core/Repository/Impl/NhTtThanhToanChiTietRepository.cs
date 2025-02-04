using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhTtThanhToanChiTietRepository : Repository<NhTtThanhToanChiTiet>, INhTtThanhToanChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhTtThanhToanChiTietRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void AddOrUpdate(Guid thanhToanId, IEnumerable<NhTtThanhToanChiTiet> entities)
        {
            List<NhTtThanhToanChiTiet> lstAdded = entities.Where(x => x.IsAdded && !x.IsDeleted).ToList();
            if (lstAdded.Any())
            {
                foreach (var item in lstAdded)
                {
                    item.IIdDeNghiThanhToanId = thanhToanId;
                }
                this.AddRange(lstAdded);
            }

            List<NhTtThanhToanChiTiet> lstModified = entities.Where(x => x.IsModified && !x.IsAdded && !x.IsDeleted).ToList();
            if (lstModified.Any())
            {
                foreach (var item in lstModified)
                {
                    item.IIdDeNghiThanhToanId = thanhToanId;
                }
                this.UpdateRange(lstModified);
            }

            List<NhTtThanhToanChiTiet> lstDeleted = entities.Where(x => x.IsDeleted && !x.IsAdded).ToList();
            if (lstDeleted.Any())
            {
                this.RemoveRange(lstDeleted);
            }
        }

        public void DeleteByDeNghiThanhToan(Guid deNghiThanhToan)
        {
            var lstDeleted = this.FindAll(x => x.IIdDeNghiThanhToanId == deNghiThanhToan);
            if (!lstDeleted.IsEmpty())
            {
                this.RemoveRange(lstDeleted);
            }
        }
    }
}
