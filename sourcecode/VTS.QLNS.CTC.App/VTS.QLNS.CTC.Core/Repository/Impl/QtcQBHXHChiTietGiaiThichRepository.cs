using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class QtcQBHXHChiTietGiaiThichRepository : Repository<BhQtcQBHXHChiTietGiaiThich>, IQtcQBHXHChiTietGiaiThichRepository
    {
        public readonly ApplicationDbContextFactory _contextFactory;
        public QtcQBHXHChiTietGiaiThichRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void RemoveGiaiThichBangLoiTheoChungTu(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var lstDelete = ctx.BhQtcQBHXHChiTietGiaiThichs.Where(x => x.IID_QTC_QChungTu == id);
                if (lstDelete.Any())
                {
                    this.RemoveRange(lstDelete);
                }
            }
        }

        public BhQtcQBHXHChiTietGiaiThich FindByCondition(BhQtcQBHXHChiTietGiaiThichCriteria condition)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcQBHXHChiTietGiaiThichs.Where(x => x.IID_QTC_QChungTu == condition.VoucherId && x.IID_MaDonVi == condition.AgencyId
                                                            && x.INamLamViec == condition.YearOfWork && x.SMaLoaiChi == condition.ExplainType).FirstOrDefault();
            }
        }

        public BhQtcQBHXHChiTietGiaiThich FindById(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhQtcQBHXHChiTietGiaiThichs.Where(x => x.Id == id).FirstOrDefault();
            }
        }

        public List<BhQtcQBHXHChiTietGiaiThichQuery> GetGiaiThichBangLoiTheoDonVi(int yearOfWork, string sMaDonVi, int iQuy, string sMABHXH)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter iNamLamViecParam = new SqlParameter("@NamLamViec", yearOfWork);
                SqlParameter idMaDonViParam = new SqlParameter("@MaDonVi ", sMaDonVi);
                SqlParameter iQuyParam = new SqlParameter("@Quy ", iQuy);
                SqlParameter sMaLoaiChiParam = new SqlParameter("@MaLoaiChi  ", sMABHXH);

                return ctx.FromSqlRaw<BhQtcQBHXHChiTietGiaiThichQuery>("EXECUTE sp_bh_quyet_toan_getloigiaithichbangloi @NamLamViec, @MaDonVi,@Quy,@MaLoaiChi  ",
                    iNamLamViecParam, idMaDonViParam, iQuyParam, sMaLoaiChiParam).ToList();
            }
        }
    }
}
