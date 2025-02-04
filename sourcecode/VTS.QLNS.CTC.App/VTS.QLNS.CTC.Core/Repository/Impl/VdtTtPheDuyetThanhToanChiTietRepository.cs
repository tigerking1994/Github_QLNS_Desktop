using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtTtPheDuyetThanhToanChiTietRepository : Repository<VdtTtPheDuyetThanhToanChiTiet>, IVdtTtPheDuyetThanhToanChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        private readonly ITongHopNguonNSDauTuRepository _repository;

        public VdtTtPheDuyetThanhToanChiTietRepository(ApplicationDbContextFactory context)
            : base(context)
        {
            _contextFactory = context;
        }

        public List<VdtTtPheDuyetThanhToanChiTietQuery> GetAllPheDuyetThanhToanChiTiet(Guid iIdParentId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtTtPheDuyetThanhToanChiTietQuery>("sp_tt_get_pheduyetthanhtoan_detail @uIdPheDuyet",
                new SqlParameter("@uIdPheDuyet", iIdParentId)).ToList();
            }
        }

        public List<PheDuyetThanhToanChiTietQuery> GetAllVdtTTPheDuyetThanhToanChiTiet(Guid iIdParentId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<PheDuyetThanhToanChiTietQuery>("sp_tt_get_pheduyetthanhtoanchitiet_by_parentid @uIdPheDuyet",
                new SqlParameter("@uIdPheDuyet", iIdParentId)).ToList();
            }
        }

        public void DeletePheDuyetThanhToanChiTietByParentId(Guid iIdParent)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                ctx.Database.ExecuteSqlCommand("EXECUTE sp_delete_tonghopnguonnsdautu_giam @sLoai, @uIdQuyetDinh",
                  new SqlParameter("@sLoai", LOAI_CHUNG_TU.CAP_THANH_TOAN),
                  new SqlParameter("@uIdQuyetDinh", iIdParent));
                var datas = ctx.VdtTtPheDuyetThanhToanChiTiets.Where(n => n.IIDDeNghiThanhToanID == iIdParent);
                if (datas == null) return;
                ctx.VdtTtPheDuyetThanhToanChiTiets.RemoveRange(datas);
                ctx.SaveChanges();
            }
        }

        public List<VdtTtPheDuyetThanhToanChiTiet> FindByDeNghiThanhToanId(Guid deNghiThanhToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtTtPheDuyetThanhToanChiTiets.Where(n => n.IIDDeNghiThanhToanID == deNghiThanhToanId).ToList();
            }
        }
    }
}
