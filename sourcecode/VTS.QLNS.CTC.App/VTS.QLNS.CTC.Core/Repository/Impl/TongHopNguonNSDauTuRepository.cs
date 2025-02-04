using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using Dapper;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TongHopNguonNSDauTuRepository : Repository<VdtTongHopNguonNsdauTu>, ITongHopNguonNSDauTuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TongHopNguonNSDauTuRepository(ApplicationDbContextFactory context)
            : base(context)
        {
            _contextFactory = context;
        }

        public List<TongHopNguonNSDauTuQuery> GetNguonVonTongHopNguonDauTuKHVN(int iNamKeHoach)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var data = ctx.FromSqlRaw<TongHopNguonNSDauTuQuery>("EXECUTE sp_get_nguonvon_tonghopnguondautu_khvn @iNamKeHoach",
                    new SqlParameter("@iNamKeHoach", iNamKeHoach));
                if (data == null) return new List<TongHopNguonNSDauTuQuery>();
                return data.ToList();
            }
        }

        public List<TongHopNguonNSDauTuQuery> GetNguonVonTongHopNguonDauTuByCondition(List<TongHopNguonNSDauTuQuery> lstCondition)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var executeQuery = "EXECUTE sp_get_nguonvon_tonghopnguondautu_by_condition @data";
                var conditions = DBExtension.ConvertDataToTableDefined("t_tbl_tonghopdautu_v2", lstCondition);
                var parameters = new[]
                {
                    new SqlParameter("@data", conditions.AsTableValuedParameter("t_tbl_tonghopdautu_v2"))
                };
                var data = ctx.FromSqlRaw<TongHopNguonNSDauTuQuery>(executeQuery, parameters);
                if (data == null) return new List<TongHopNguonNSDauTuQuery>();
                return data.ToList();
            }
        }

        public void InsertTongHopNguonDauTu_Tang(string sLoai, int iTypeExecute, Guid iIdQuyetDinh, Guid iIDQuyetDinhOld)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                //ctx.Database.ExecuteSqlCommand("EXECUTE sp_insert_tonghopnguonnsdautu_tang @sLoai, @iTypeExecute, @uIdQuyetDinh, @iIDQuyetDinhOld",
                ctx.Database.ExecuteSqlCommand("EXECUTE sp_insert_tonghopnguonnsdautu_tang_new @sLoai, @iTypeExecute, @uIdQuyetDinh, @iIDQuyetDinhOld",
                   new SqlParameter("@sLoai", sLoai),
                   new SqlParameter("@iTypeExecute", iTypeExecute),
                   new SqlParameter("@uIdQuyetDinh", iIdQuyetDinh),
                   new SqlParameter("@iIDQuyetDinhOld", iIDQuyetDinhOld));
            }
        }

        public void DeleteTongHopNguonDauTu_Giam(string sLoai, Guid iIdQuyetDinh)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                ctx.Database.ExecuteSqlCommand("EXECUTE sp_delete_tonghopnguonnsdautu_giam @sLoai, @uIdQuyetDinh",
                   new SqlParameter("@sLoai", sLoai),
                   new SqlParameter("@uIdQuyetDinh", iIdQuyetDinh));
            }
        }

        public void InsertTongHopNguonDauTu(Guid iIDChungTu, string sLoai, List<TongHopNguonNSDauTuQuery> lstData)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var executeQuery = "EXECUTE sp_insert_tonghopnguondautu @iIDChungTu, @sLoai, @data";
                var data = DBExtension.ConvertDataToTableDefined("t_tbl_tonghopdautu_v2", lstData);
                var parameters = new[]
                {
                    new SqlParameter("iIDChungTu", iIDChungTu),
                    new SqlParameter("sLoai", sLoai),
                    new SqlParameter("@data", data.AsTableValuedParameter("t_tbl_tonghopdautu_v2"))
                };
                ctx.FromSqlRaw<TongHopNguonNSDauTuQuery>(executeQuery, parameters);
            }
        }

        public void InsertTongHopNguonDauTuQuyetToan(Guid iIDChungTu, List<TongHopNguonNSDauTuQuery> lstData)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var executeQuery = "EXECUTE sp_vdt_insert_tonghopquyettoan @iIdQuyetToan, @table";
                var data = DBExtension.ConvertDataToTableDefined("t_tbl_tonghopdautu", lstData);
                SqlParameter dtDetailParam = new SqlParameter("table", SqlDbType.Structured);
                dtDetailParam.TypeName = "t_tbl_tonghopdautu_v2";
                dtDetailParam.Value = data;
                var parameters = new[]
                {
                    new SqlParameter("iIdQuyetToan", iIDChungTu),
                    dtDetailParam
                };
                ctx.Database.ExecuteSqlCommand(executeQuery, parameters);
            }
        }

        public IEnumerable<VdtBaoCaoKetQuaGiaiNganChiKPDTQuery> GetBcKetQuaGiaiNganChiPhiKinhPhiDT(string iIdDonViQuanLy, int iNamKeHoach, int iIdNguonVonId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var executeQuery = "EXECUTE sp_vdt_bcketquagiaingankinhphidautu @iIdMaDonVi, @iNamKeHoach, @iIdNganSach";
                var parameters = new[]
                {
                    new SqlParameter("iIdMaDonVi", iIdDonViQuanLy),
                    new SqlParameter("iNamKeHoach", iNamKeHoach),
                    new SqlParameter("iIdNganSach", iIdNguonVonId),
                };
                return ctx.FromSqlRaw<VdtBaoCaoKetQuaGiaiNganChiKPDTQuery>(executeQuery, parameters).ToList();
            }
        }
    }
}
