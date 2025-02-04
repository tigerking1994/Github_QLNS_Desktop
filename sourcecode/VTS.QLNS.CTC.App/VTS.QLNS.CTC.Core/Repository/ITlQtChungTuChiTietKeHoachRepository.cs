using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlQtChungTuChiTietKeHoachRepository : IRepository<TlQtChungTuChiTietKeHoach>
    {
        IEnumerable<TlQtChungTuChiTietKeHoachQuery> GetDataByMonth(string maDonVi, int thang, int nam, int pcMlnsNam);
        IEnumerable<ReportChiTietCanBoKeHoachQuery> GetDataChiTietCanBoKeHoach(int namKh, string maDonVi);
        int DeleteByNamAndMaDonVi(string maDonVi, int Nam);
        IEnumerable<TlChungTuChiTietKeHoachQuery> GetDataChungTuChiTiet(int namNs, int nam, string idDonVi);
    }
}
