using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlBangLuongKeHoachBridgeNq104Service
    {
        void DeleteAll();
        void BulkInsert(List<TlBangLuongKeHoachBridgeNq104> lstData);
        void DataPreprocess(int? nam = null, string donVi = null, string maCachTl = "");
        IEnumerable<TlBangLuongKeHoachBridgeNq104> FindAll();
    }
}
