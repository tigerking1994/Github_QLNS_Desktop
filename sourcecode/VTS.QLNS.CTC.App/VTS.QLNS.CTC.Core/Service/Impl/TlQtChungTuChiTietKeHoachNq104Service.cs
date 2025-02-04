using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlQtChungTuChiTietKeHoachNq104Service : ITlQtChungTuChiTietKeHoachNq104Service
    {
        private readonly ITlQtChungTuChiTietKeHoachNq104Repository _tlQtChungTuChiTietKeHoachRepository;

        public TlQtChungTuChiTietKeHoachNq104Service(ITlQtChungTuChiTietKeHoachNq104Repository tlQtChungTuChiTietKeHoachRepository)
        {
            _tlQtChungTuChiTietKeHoachRepository = tlQtChungTuChiTietKeHoachRepository;
        }

        public void BulkInsert(IEnumerable<TlQtChungTuChiTietKeHoachNq104> entities)
        {
            _tlQtChungTuChiTietKeHoachRepository.BulkInsert(entities);
        }

        public int DeleteByNamAndMaDonVi(string maDonVi, int Nam)
        {
            return _tlQtChungTuChiTietKeHoachRepository.DeleteByNamAndMaDonVi(maDonVi, Nam);
        }

        public IEnumerable<TlQtChungTuChiTietKeHoachNq104> FindAll(Expression<Func<TlQtChungTuChiTietKeHoachNq104, bool>> predicate)
        {
            return _tlQtChungTuChiTietKeHoachRepository.FindAll(predicate);
        }

        public IEnumerable<TlQtChungTuChiTietKeHoachNq104> FindAll()
        {
            return _tlQtChungTuChiTietKeHoachRepository.FindAll();
        }

        public IEnumerable<TlQtChungTuChiTietKeHoachNq104Query> GetDataByMonth(string maDonVi, int thang, int nam, int pcMlnsNam)
        {
            return _tlQtChungTuChiTietKeHoachRepository.GetDataByMonth(maDonVi, thang, nam, pcMlnsNam);
        }

        public int UpdateRange(IEnumerable<TlQtChungTuChiTietKeHoachNq104> entities)
        {
            return _tlQtChungTuChiTietKeHoachRepository.UpdateRange(entities);
        }

        public IEnumerable<ReportChiTietCanBoKeHoachNq104Query> GetDataChiTietCanBoKeHoach(int namKh, string maDonVi)
        {
            return _tlQtChungTuChiTietKeHoachRepository.GetDataChiTietCanBoKeHoach(namKh, maDonVi);
        }

        public IEnumerable<TlChungTuChiTietKeHoachNq104Query> GetDataChungTuChiTiet(int namNs, int nam, string idDonVi)
        {
            return _tlQtChungTuChiTietKeHoachRepository.GetDataChungTuChiTiet(namNs, nam, idDonVi);
        }

        public TlQtChungTuChiTietKeHoachNq104 Find(Guid id)
        {
            return _tlQtChungTuChiTietKeHoachRepository.Find(id);
        }

        public void BulkUpdate(IEnumerable<TlQtChungTuChiTietKeHoachNq104> entities)
        {
            _tlQtChungTuChiTietKeHoachRepository.BulkUpdate(entities);
        }
    }
}
