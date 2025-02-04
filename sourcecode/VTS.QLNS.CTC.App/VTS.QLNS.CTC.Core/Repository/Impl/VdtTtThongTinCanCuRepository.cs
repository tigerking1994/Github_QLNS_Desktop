using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Extensions;
using System.Data;
using Dapper;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtTtThongTinCanCuRepository : Repository<VdtTtThongTinCanCu>, IVdtTtThongTinCanCuRepository
    {
        private ApplicationDbContextFactory _context;
        public VdtTtThongTinCanCuRepository(ApplicationDbContextFactory context) : base(context)
        {
            _context = context;
        }       

        public List<VdtTtThongTinCanCu> GetThongTinCanCuByIdDeNghiThanhToan(Guid? iID_DeNghiThanhToanID)
        {
            using (var ctx = _context.CreateDbContext())
            {
                return ctx.VdtTtThongTinCanCus.Where(x => x.iID_DeNghiThanhToanID == iID_DeNghiThanhToanID).ToList();
            }
        }
    }
}
