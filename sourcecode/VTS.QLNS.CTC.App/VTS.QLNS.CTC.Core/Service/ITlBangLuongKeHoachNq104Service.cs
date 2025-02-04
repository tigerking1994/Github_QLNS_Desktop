using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query.Shared;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlBangLuongKeHoachNq104Service
    {
        IEnumerable<TlDmCanBoKeHoachNq104> FindCbLuong(int thang, int nam, string maDonVi);
        IEnumerable<TlDmThueThuNhapCaNhanNq104> FindThue();
        IEnumerable<TlBangLuongKeHoachNq104> FindAll();
        int AddRange(IEnumerable<TlBangLuongKeHoachNq104> tlBangLuongKeHoachs);
        int DeleteByParentId(Guid id);
        DataTable GetDataBangLuong(string maDonVi, int Nam);
        void BulkInsert(IEnumerable<TlBangLuongKeHoachNq104> tlBangLuongKeHoachs);
        IEnumerable<TlBangLuongKeHoachNq104ExportQuery> ExportQuyLuongCanCu(int iNamLamViec, List<string> lstMaPhuCap, string lstIdChungTu);

    }
}
