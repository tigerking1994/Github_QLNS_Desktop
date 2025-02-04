using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtDmDonViThucHienDuAnRepository : IRepository<VdtDmDonViThucHienDuAn>
    {
        IEnumerable<VdtDmDonViThucHienDuAn> FindAllWithOrder();
        int AddOrUpdateRange(IEnumerable<VdtDmDonViThucHienDuAn> entities, int iNamLamViec);
        VdtDmDonViThucHienDuAn FindByMaDonVi(string sMaDonVi);
        IEnumerable<NSDonViThucHienDuAnExportQuery> GetDonViThucHienDuAnExport();
    }
}
