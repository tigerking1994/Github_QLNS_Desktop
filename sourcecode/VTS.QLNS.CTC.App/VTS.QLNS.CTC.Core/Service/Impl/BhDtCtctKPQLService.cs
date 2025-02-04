using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;
using static Dapper.SqlMapper;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhDtCtctKPQLService : IBhDtCtctKPQLService
    {
        private readonly IBhDtCtctKPQLRepostiory _repostiory;
        public BhDtCtctKPQLService(IBhDtCtctKPQLRepostiory repostiory)
        {
            _repostiory = repostiory;
        }
        public void Add(BhDtCtctKPQL entity)
        {
            _repostiory.Add(entity);
        }

        public int AddRange(IEnumerable<BhDtCtctKPQL> lstItems)
        {
            return _repostiory.AddRange(lstItems);
        }

        public void Delete(Guid id)
        {
            _repostiory.Delete(id);
        }

        public IEnumerable<BhDtCtctKPQLQuery> FindIndex(int iNamChungTu, Guid? iDChungTuChiTiet, Guid? iDChungTu, string sMaDonVi)
        {
            return _repostiory.FindIndex(iNamChungTu, iDChungTuChiTiet, iDChungTu, sMaDonVi);
        }

        public void Update(BhDtCtctKPQL entity)
        {
            _repostiory.Update(entity);
        }
        public List<BhDtCtctKPQLQuery> FindDuToanTrenGiaoKPQL(Guid? iDChungTu, string sMaDonVi, int NamLamViec)
        {
            return _repostiory.FindDuToanTrenGiaoKPQL(iDChungTu, sMaDonVi, NamLamViec);
        }

        public List<BhDtCtctKPQLQuery> FindPhanBoDuToanTrenGiaoKPQL(Guid id, string sMaDonVi, int yearOfWork)
        {
            return _repostiory.FindPhanBoDuToanTrenGiaoKPQL(id, sMaDonVi, yearOfWork);
        }

        public IEnumerable<BhDtCtctKPQL> FindByCondition(Guid id)
        {
            return _repostiory.FindByCondition(id);
        }

        public int RemoveRange(IEnumerable<BhDtCtctKPQL> lstItems)
        {
            return _repostiory.RemoveRange(lstItems);
        }

        public IEnumerable<ReportBhDtCtctKPQLQuery> ExportBaoCaoChiTietDonViKQPL(string maDonVi, int yearOfWork, Guid id, int donViTinh, bool IsMillionRound)
        {
            return _repostiory.ExportBaoCaoChiTietDonViKQPL(maDonVi, yearOfWork, id, donViTinh, IsMillionRound);
        }

        public IEnumerable<BhDuToanThuChiQuery> GetSoQuyetDinhDTCKPQL(int year)
        {
            return _repostiory.GetSoQuyetDinhDTCKPQL(year);
        }
    }
}
