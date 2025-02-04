using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlQtChungTuChiTietKeHoachNq104Service
    {
        IEnumerable<TlQtChungTuChiTietKeHoachNq104Query> GetDataByMonth(string maDonVi, int thang, int nam, int pcMlnsNam);
        int DeleteByNamAndMaDonVi(string maDonVi, int Nam);
        void BulkInsert(IEnumerable<TlQtChungTuChiTietKeHoachNq104> entities);
        IEnumerable<TlQtChungTuChiTietKeHoachNq104> FindAll(Expression<Func<TlQtChungTuChiTietKeHoachNq104, bool>> predicate);
        IEnumerable<TlQtChungTuChiTietKeHoachNq104> FindAll();
        int UpdateRange(IEnumerable<TlQtChungTuChiTietKeHoachNq104> entities);
        IEnumerable<ReportChiTietCanBoKeHoachNq104Query> GetDataChiTietCanBoKeHoach(int namKh, string maDonVi);
        IEnumerable<TlChungTuChiTietKeHoachNq104Query> GetDataChungTuChiTiet(int namNs, int nam, string idDonVi);
        TlQtChungTuChiTietKeHoachNq104 Find(Guid id);
        void BulkUpdate(IEnumerable<TlQtChungTuChiTietKeHoachNq104> entities);
    }
}
