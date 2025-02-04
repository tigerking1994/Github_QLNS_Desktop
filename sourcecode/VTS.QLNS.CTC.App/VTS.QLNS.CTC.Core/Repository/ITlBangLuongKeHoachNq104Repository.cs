using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query.Shared;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlBangLuongKeHoachNq104Repository : IRepository<TlBangLuongKeHoachNq104>
    {
        IEnumerable<TlDmCanBoKeHoachNq104> FindCanBo(decimal? thang, decimal? nam, string maDonVi);
        IEnumerable<TlDmThueThuNhapCaNhanNq104> FindThue();
        int DeleteByParentId(Guid id);
        DataTable GetDataBangLuong(string maDonVi, int nam);
        IEnumerable<TlBangLuongKeHoachNq104ExportQuery> ExportQuyLuongCanCu(int iNamLamViec, List<string> lstMaPhuCap, string lstIdChungTu);
    }
}
