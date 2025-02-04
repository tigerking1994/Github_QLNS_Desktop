using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhMstnKeHoachDatHangRepository : Repository<NhMSTNKeHoachDatHang>, INhMstnKeHoachDatHangRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhMstnKeHoachDatHangRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhMstnKeHoachDatHangQuery> GetAllMstnKeHoachDatHangIndex()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE sp_nh_mstnkehoachdathang_index";
                return ctx.FromSqlRaw<NhMstnKeHoachDatHangQuery>(executeSql).ToList();
            }
        }

        public void DeleteById(Guid iId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "sp_nh_mstn_khdh_delete_by_id @iId";
                var parameters = new[] {
                    new SqlParameter("@iId", iId)
                };
                ctx.Database.ExecuteSqlCommand(executeSql, parameters);
            }
        }

        public bool CheckDuplicateSoQD(string soQuyetDinh, Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                List<NhMSTNKeHoachDatHang> listKHDT = new List<NhMSTNKeHoachDatHang>();
                if (id != Guid.Empty)
                {
                    listKHDT = ctx.NhMSTNKeHoachDatHangs.Where(x => x.SSoQuyetDinh == soQuyetDinh && x.Id != id && x.BIsActive == true).ToList();
                }
                else
                {
                    listKHDT = ctx.NhMSTNKeHoachDatHangs.Where(x => x.SSoQuyetDinh == soQuyetDinh && x.BIsActive == true).ToList();
                }

                if (listKHDT.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public IEnumerable<NhMstnKeHoachDatHangQuery> FindMstnKeHoachDatHangByCondition(Guid? donviId, Guid? keHoachTongTheId, Guid? chuongTrinhId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE sp_nh_mstnkehoachdathang_bycondition @DonviId, @KeHoachTongTheId, @ChuongTrinhId";
                var parameters = new[] {
                    new SqlParameter("@DonviId", donviId),
                    new SqlParameter("@KeHoachTongTheId", keHoachTongTheId),
                    new SqlParameter("@ChuongTrinhId", chuongTrinhId)
                };
                return ctx.FromSqlRaw<NhMstnKeHoachDatHangQuery>(executeSql, parameters).ToList();
            }
        }
    }
}
