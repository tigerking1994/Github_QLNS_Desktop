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
    public class VdtTtDeNghiThanhToanKhvRepository : Repository<VdtTtDeNghiThanhToanKhv>, IVdtTtDeNghiThanhToanKhvRepository
    {
        private ApplicationDbContextFactory _context;
        public VdtTtDeNghiThanhToanKhvRepository(ApplicationDbContextFactory context) : base(context)
        {
            _context = context;
        }

        public List<VdtTtDeNghiThanhToanKhv> FindByDeNghiThanhToanId(Guid deNghiThanhToanId)
        {
            using (var ctx = _context.CreateDbContext())
            {
                return ctx.VdtTtDeNghiThanhToanKhvs.Where(x => x.IIdDeNghiThanhToanId == deNghiThanhToanId).ToList();
            }
        }

        public List<VdtTtKeHoachVonQuery> GetKhvDeNghiTamUng(Guid iIdDuAnId, int iIdNguonVonId, DateTime dNgayDeNghi, int iNamKeHoach, int iCoQuanThanhToan, Guid iIdPheDuyet, Guid ID_DuAn_HangMuc)
        {
            using (ApplicationDbContext ctx = _context.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtTtKeHoachVonQuery>("EXECUTE dbo.sp_vdt_tt_get_khvthuhoiung @iIdDuAnId, @iIdNguonVonId, @dNgayDeNghi, @iNamKeHoach, @iCoQuanThanhToan, @iIdPheDuyet, @ID_DuAn_HangMuc",
                    new SqlParameter("@iIdDuAnId", iIdDuAnId),
                    new SqlParameter("@iIdNguonVonId", iIdNguonVonId),
                    new SqlParameter("@dNgayDeNghi", dNgayDeNghi),
                    new SqlParameter("@iNamKeHoach", iNamKeHoach),
                    new SqlParameter("@iCoQuanThanhToan", iCoQuanThanhToan),
                    new SqlParameter("@iIdPheDuyet", iIdPheDuyet),
                    new SqlParameter("@ID_DuAn_HangMuc", ID_DuAn_HangMuc)
                ).ToList();
            }
        }

        public List<VdtTtKeHoachVonQuery> GetKhvDeNghiThanhToan(Guid iIdDuAnId, int iIdNguonVonId, DateTime dNgayDeNghi, int iNamKeHoach, int iCoQuanThanhToan, Guid iIdPheDuyet, Guid ID_DuAn_HangMuc)
        {
            using (ApplicationDbContext ctx = _context.CreateDbContext())
            {
                return ctx.FromSqlRaw<VdtTtKeHoachVonQuery>("EXECUTE dbo.sp_vdt_tt_get_khvthanhtoan @iIdDuAnId, @iIdNguonVonId, @dNgayDeNghi, @iNamKeHoach, @iCoQuanThanhToan, @iIdPheDuyet, @ID_DuAn_HangMuc",
                    new SqlParameter("@iIdDuAnId", iIdDuAnId),
                    new SqlParameter("@iIdNguonVonId", iIdNguonVonId),
                    new SqlParameter("@dNgayDeNghi", dNgayDeNghi),
                    new SqlParameter("@iNamKeHoach", iNamKeHoach),
                    new SqlParameter("@iCoQuanThanhToan", iCoQuanThanhToan),
                    new SqlParameter("@iIdPheDuyet", iIdPheDuyet),
                    new SqlParameter("@ID_DuAn_HangMuc", ID_DuAn_HangMuc)
                ).ToList();
            }
        }

        public List<MlnsByKeHoachVonQuery> GetMucLucNganSachByKeHoachVon(int iNamLamViec, List<TongHopNguonNSDauTuQuery> lstCondition)
        {
            using (ApplicationDbContext ctx = _context.CreateDbContext())
            {
                //var executeQuery = "EXECUTE dbo.sp_vdt_tt_get_mlns_by_khv @iNamLamViec, @data";
                var executeQuery = "EXECUTE dbo.sp_vdt_tt_get_mlns_by_khv_new @iNamLamViec, @data";
                var conditions = DBExtension.ConvertDataToTableDefined("t_tbl_tonghopdautu_v2", lstCondition);
                var parameters = new[]
                {
                    new SqlParameter("@iNamLamViec", iNamLamViec),
                    new SqlParameter("@data", conditions.AsTableValuedParameter("t_tbl_tonghopdautu_v2"))
                };
                var data = ctx.FromSqlRaw<MlnsByKeHoachVonQuery>(executeQuery, parameters).ToList();
                return data;
            }
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
