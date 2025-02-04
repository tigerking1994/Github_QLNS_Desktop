using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDieuChinhQsKeHoachNq104Service : ITlDieuChinhQsKeHoachNq104Service
    {
        private ITlDieuChinhQsKeHoachNq104Repository _tlDieuChinhQsKeHoachRepository;

        public TlDieuChinhQsKeHoachNq104Service(ITlDieuChinhQsKeHoachNq104Repository tlDieuChinhQsKeHoachRepository)
        {
            _tlDieuChinhQsKeHoachRepository = tlDieuChinhQsKeHoachRepository;
        }

        public int AddRange(List<TlDieuChinhQsKeHoachNq104> entities)
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

        public IEnumerable<TlDieuChinhQsKeHoachNq104> FindAll()
        {
            return _tlDieuChinhQsKeHoachRepository.FindAll();
        }

        public TlDieuChinhQsKeHoachNq104 FindByKeHoach(Expression<Func<TlDieuChinhQsKeHoachNq104, bool>> predicate)
        {
            return _tlDieuChinhQsKeHoachRepository.FirstOrDefault(predicate);
        }

        public IEnumerable<TlRptQuanSoKeHoachNq104Query> FindData(int nam, string maDonVi)
        {
            return _tlDieuChinhQsKeHoachRepository.FindData(nam, maDonVi);
        }

        public int UpdateRange(List<TlDieuChinhQsKeHoachNq104> entities)
        {
            return _tlDieuChinhQsKeHoachRepository.UpdateRange(entities);
        }
    }
}
