using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtKtKhoiTaoChiTietService
    {
        IEnumerable<KhoiTaoChiTietQuery> FindDataKhoiTaoChiTiet(string idKhoiTao, string idDuAn);
        int AddRange(IEnumerable<VdtKtKhoiTaoChiTiet> entities);
        int Add(VdtKtKhoiTaoChiTiet entity);
        int Update(VdtKtKhoiTaoChiTiet entity);
        int Delete(Guid id);
        VdtKtKhoiTaoChiTiet Find(params object[] keyValues);
    }
}
