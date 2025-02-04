using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlQtChungTuChiTietKeHoachService
    {
        IEnumerable<TlQtChungTuChiTietKeHoachQuery> GetDataByMonth(string maDonVi, int thang, int nam, int pcMlnsNam);
        int DeleteByNamAndMaDonVi(string maDonVi, int Nam);
        void BulkInsert(IEnumerable<TlQtChungTuChiTietKeHoach> entities);
        IEnumerable<TlQtChungTuChiTietKeHoach> FindAll(Expression<Func<TlQtChungTuChiTietKeHoach, bool>> predicate);
        IEnumerable<TlQtChungTuChiTietKeHoach> FindAll();
        int UpdateRange(IEnumerable<TlQtChungTuChiTietKeHoach> entities);
        IEnumerable<ReportChiTietCanBoKeHoachQuery> GetDataChiTietCanBoKeHoach(int namKh, string maDonVi);
        IEnumerable<TlChungTuChiTietKeHoachQuery> GetDataChungTuChiTiet(int namNs, int nam, string idDonVi);
        TlQtChungTuChiTietKeHoach Find(Guid id);
        void BulkUpdate(IEnumerable<TlQtChungTuChiTietKeHoach> entities);
    }
}
