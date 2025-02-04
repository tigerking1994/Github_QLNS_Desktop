using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query.Shared;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlBangLuongKeHoachRepository : IRepository<TlBangLuongKeHoach>
    {
        IEnumerable<TlDmCanBoKeHoach> FindCanBo(decimal? thang, decimal? nam, string maDonVi);
        IEnumerable<TlDmThueThuNhapCaNhan> FindThue();
        int DeleteByParentId(Guid id);
        DataTable GetDataBangLuong(string maDonVi, int nam);
        IEnumerable<TlBangLuongKeHoachExportQuery> ExportQuyLuongCanCu(int iNamLamViec, List<string> lstMaPhuCap, string lstIdChungTu);

    }
}
