using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDieuChinhQsKeHoachService : ITlDieuChinhQsKeHoachService
    {
        private ITlDieuChinhQsKeHoachRepository _tlDieuChinhQsKeHoachRepository;

        public TlDieuChinhQsKeHoachService(ITlDieuChinhQsKeHoachRepository tlDieuChinhQsKeHoachRepository)
        {
            _tlDieuChinhQsKeHoachRepository = tlDieuChinhQsKeHoachRepository;
        }

        public int AddRange(List<TlDieuChinhQsKeHoach> entities)
        {
            return _tlDieuChinhQsKeHoachRepository.AddRange(entities);
        }

        public int Delete(Guid id)
        {
            return _tlDieuChinhQsKeHoachRepository.Delete(id);
        }

        public int DeleteByNam(int nam)
        {
            return _tlDieuChinhQsKeHoachRepository.DeleteByNam(nam);
        }

        public IEnumerable<TlDieuChinhQsKeHoach> FindAll()
        {
            return _tlDieuChinhQsKeHoachRepository.FindAll();
        }

        public TlDieuChinhQsKeHoach FindByKeHoach(Expression<Func<TlDieuChinhQsKeHoach, bool>> predicate)
        {
            return _tlDieuChinhQsKeHoachRepository.FirstOrDefault(predicate);
        }

        public IEnumerable<TlRptQuanSoKeHoachQuery> FindData(int nam, string maDonVi)
        {
            return _tlDieuChinhQsKeHoachRepository.FindData(nam, maDonVi);
        }

        public int UpdateRange(List<TlDieuChinhQsKeHoach> entities)
        {
            return _tlDieuChinhQsKeHoachRepository.UpdateRange(entities);
        }
    }
}
