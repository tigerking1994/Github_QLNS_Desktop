using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtDaDmDuToanHangMucRepository :IRepository<VdtDaDuToanDmHangMuc>
    {
        VdtDaDuToanDmHangMuc FindByMa(string ma);
    }
}
