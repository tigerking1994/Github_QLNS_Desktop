using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtKtKhoiTaoDuLieuChiTietThanhToanService
    {
        void AddRange(IEnumerable<VdtKtKhoiTaoDuLieuChiTietThanhToan> datas);
        void DeleteByKhoiTaoDuLieuId(Guid iId);
        IEnumerable<VdtKtKhoiTaoDuLieuChiTietThanhToanQuery> GetDetailByKTDLId(Guid iId);
    }
}
