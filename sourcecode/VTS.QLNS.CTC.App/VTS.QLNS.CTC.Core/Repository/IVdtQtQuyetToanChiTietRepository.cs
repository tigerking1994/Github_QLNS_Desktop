using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtQtQuyetToanChiTietRepository : IRepository<VdtQtQuyetToanChiTiet>
    {
        List<VdtQtQuyetToanChiTiet> FindByQuyetToanId(Guid quyetToanId);
        void UpdateTotal(string voucherId);
    }
}
