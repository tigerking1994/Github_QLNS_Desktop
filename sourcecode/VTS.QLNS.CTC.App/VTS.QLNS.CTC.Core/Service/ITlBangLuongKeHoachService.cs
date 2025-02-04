using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query.Shared;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlBangLuongKeHoachService
    {
        IEnumerable<TlDmCanBoKeHoach> FindCbLuong(int thang, int nam, string maDonVi);
        IEnumerable<TlDmThueThuNhapCaNhan> FindThue();
        IEnumerable<TlBangLuongKeHoach> FindAll();
        int AddRange(IEnumerable<TlBangLuongKeHoach> tlBangLuongKeHoachs);
        int DeleteByParentId(Guid id);
        DataTable GetDataBangLuong(string maDonVi, int Nam);
        void BulkInsert(IEnumerable<TlBangLuongKeHoach> tlBangLuongKeHoachs);
        IEnumerable<TlBangLuongKeHoachExportQuery> ExportQuyLuongCanCu(int iNamLamViec, List<string> lstMaPhuCap, string lstIdChungTu);

    }
}
