using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtKhvPhanBoVonChiPhiChiTietRepository : Repository<VdtKhvPhanBoVonChiPhiChiTiet>, IVdtKhvPhanBoVonChiPhiChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtKhvPhanBoVonChiPhiChiTietRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<VdtKhvPhanBoVonChiPhiChiTietQuery> FindByIdChiPhi(Guid idChiPhi)
        {
             
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = @"select Id,iID_PhanBoVon_ChiPhi_ID as  IIdPhanBoVonChiPhiId,sTrangThaiDuAnDangKy,sGhiChu,fGiaTriPheDuyet,iID_LoaiCongTrinh as IIdLoaiCongTrinh,fGiaTriPheDuyetDC,iId_Parent as  IIdParent,iID_DanhMuc_DT_chi as IIdDanhMucDtChi,ILoaiDuAn,sMaOrder,sMaChiPhi,sNoiDung
                from VDT_KHV_PhanBoVon_ChiPhi_ChiTiet
                where iID_PhanBoVon_ChiPhi_ID = @idChiPhi";
                var parameters = new[]
                {
                    new SqlParameter("@idChiPhi", idChiPhi)
                };

                var resulse = ctx.FromSqlRaw<VdtKhvPhanBoVonChiPhiChiTietQuery>(sql, parameters).ToList();

                return resulse;
            }
        }
    }
}
