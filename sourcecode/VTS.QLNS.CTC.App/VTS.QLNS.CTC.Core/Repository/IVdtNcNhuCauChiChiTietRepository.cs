using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtNcNhuCauChiChiTietRepository : IRepository<VdtNcNhuCauChiChiTiet>
    {
        List<VdtNcNhuCauChiChiTiet> GetDetailByParent(Guid iIdParentId);
        void DeleteDetailByParent(Guid iIdParentID);
    }
}
