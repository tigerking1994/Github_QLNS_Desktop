using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtDmDonViThucHienDuAnService
    {
        IEnumerable<VdtDmDonViThucHienDuAn> FindAll();
        VdtDmDonViThucHienDuAn FindByMaDonVi(string sMaDonVi);
        IEnumerable<NSDonViThucHienDuAnExportQuery> GetDonViThucHienDuAnExport();
    }
}
