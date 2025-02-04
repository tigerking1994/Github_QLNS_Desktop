using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlBangLuongThangBridgeNq104Service
    {
        void DeleteAll();
        void BulkInsert(List<TlBangLuongThangBridgeNq104> lstData);
        void DataPreprocess(int? thang = null, int? nam = null, string donVi = null, string maCachTl = "");
        IEnumerable<TlBangLuongThangBridgeNq104> FindAll();
    }
}
