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
    public class NhDaQdDauTuNguonVonRepository : Repository<NhDaQdDauTuNguonVon>, INhDaQdDauTuNguonVonRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDaQdDauTuNguonVonRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhDaDetailNguonVonQuery> GetNguonVonByQdDauTuId(Guid iIdQdDauTuId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE sp_nh_qddautu_nguonvon @iIdQdDauTuId";
                var parameters = new[] {
                    new SqlParameter("@iIdQdDauTuId", iIdQdDauTuId)
                };
                return ctx.FromSqlRaw<NhDaDetailNguonVonQuery>(executeSql, parameters).ToList();
            }
        }
        
        public void DeleteByQdDauTuId(Guid qdDauTuId)
        {
            var lstDeleted = this.FindAll(x => x.IIdQdDauTuId == qdDauTuId);
            if (!lstDeleted.IsEmpty())
            {
                this.RemoveRange(lstDeleted);
            }
        }

        public IEnumerable<NhDaQdDauTuNguonVon> FindByQdDauTuId(Guid iIdQdDauTuId)
        {
            return FindAll().Where(x => x.IIdQdDauTuId == iIdQdDauTuId);
        }

        public List<NHDAQDDauTuNguonVonQuery> FindByDuAnId(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = @"select t1.ID as Id, 
                                t1.iID_QDDauTuID as QddtId,
                                t1.iID_NguonVonID as NguonVonId,
                                t1.fGiaTriNgoaiTeKhac as FGiaTriNgoaiTeKhacQDDT,
                                t1.fGiaTriUSD as FGiaTriUSDQDDT,
                                t1.fGiaTriEUR as FGiaTriEurQDDT,
                                t1.fGiaTriVND as FGiaTriVNDQDDT,
                                t3.sTen as STenNguonVon
                                from NH_DA_QDDauTu_NguonVon t1
                                join NH_DA_QDDauTu t2 on t1.iID_QDDauTuID = t2.ID
                                join NguonNganSach t3 on t1.iID_NguonVonID = t3.iID_MaNguonNganSach
                                where t2.iID_DuAnID = @DuAnId";
                var parameters = new[] {
                    new SqlParameter("@DuAnId", id)
                };
                return ctx.FromSqlRaw<NHDAQDDauTuNguonVonQuery>(sql, parameters).ToList();
            }
        }
    }
}
