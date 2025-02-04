using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlQtChungTuChiTietKeHoachService : ITlQtChungTuChiTietKeHoachService
    {
        private readonly ITlQtChungTuChiTietKeHoachRepository _tlQtChungTuChiTietKeHoachRepository;

        public TlQtChungTuChiTietKeHoachService(ITlQtChungTuChiTietKeHoachRepository tlQtChungTuChiTietKeHoachRepository)
        {
            _tlQtChungTuChiTietKeHoachRepository = tlQtChungTuChiTietKeHoachRepository;
        }

        public void BulkInsert(IEnumerable<TlQtChungTuChiTietKeHoach> entities)
        {
            _tlQtChungTuChiTietKeHoachRepository.BulkInsert(entities);
        }

        public int DeleteByNamAndMaDonVi(string maDonVi, int Nam)
        {
            return _tlQtChungTuChiTietKeHoachRepository.DeleteByNamAndMaDonVi(maDonVi, Nam);
        }

        public IEnumerable<TlQtChungTuChiTietKeHoach> FindAll(Expression<Func<TlQtChungTuChiTietKeHoach, bool>> predicate)
        {
            return _tlQtChungTuChiTietKeHoachRepository.FindAll(predicate);
        }

        public IEnumerable<TlQtChungTuChiTietKeHoach> FindAll()
        {
            return _tlQtChungTuChiTietKeHoachRepository.FindAll();
        }

        public IEnumerable<TlQtChungTuChiTietKeHoachQuery> GetDataByMonth(string maDonVi, int thang, int nam, int pcMlnsNam)
        {
            return _tlQtChungTuChiTietKeHoachRepository.GetDataByMonth(maDonVi, thang, nam, pcMlnsNam);
        }

        public int UpdateRange(IEnumerable<TlQtChungTuChiTietKeHoach> entities)
        {
            return _tlQtChungTuChiTietKeHoachRepository.UpdateRange(entities);
        }

        public IEnumerable<ReportChiTietCanBoKeHoachQuery> GetDataChiTietCanBoKeHoach(int namKh, string maDonVi)
        {
            return _tlQtChungTuChiTietKeHoachRepository.GetDataChiTietCanBoKeHoach(namKh, maDonVi);
        }

        public IEnumerable<TlChungTuChiTietKeHoachQuery> GetDataChungTuChiTiet(int namNs, int nam, string idDonVi)
        {
            return _tlQtChungTuChiTietKeHoachRepository.GetDataChungTuChiTiet(namNs, nam, idDonVi);
        }

        public TlQtChungTuChiTietKeHoach Find(Guid id)
        {
            return _tlQtChungTuChiTietKeHoachRepository.Find(id);
        }

        public void BulkUpdate(IEnumerable<TlQtChungTuChiTietKeHoach> entities)
        {
            _tlQtChungTuChiTietKeHoachRepository.BulkUpdate(entities);
        }
    }
}
