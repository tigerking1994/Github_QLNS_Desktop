using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlQtChungTuChiTietKeHoachNq104Repository : IRepository<TlQtChungTuChiTietKeHoachNq104>
    {
        IEnumerable<TlQtChungTuChiTietKeHoachNq104Query> GetDataByMonth(string maDonVi, int thang, int nam, int pcMlnsNam);
        IEnumerable<ReportChiTietCanBoKeHoachNq104Query> GetDataChiTietCanBoKeHoach(int namKh, string maDonVi);
        int DeleteByNamAndMaDonVi(string maDonVi, int Nam);
        IEnumerable<TlChungTuChiTietKeHoachNq104Query> GetDataChungTuChiTiet(int namNs, int nam, string idDonVi);
    }
}
