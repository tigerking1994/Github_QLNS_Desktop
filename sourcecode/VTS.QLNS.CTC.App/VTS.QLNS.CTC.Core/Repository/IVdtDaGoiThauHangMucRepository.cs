using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtDaGoiThauHangMucRepository: IRepository<VdtDaGoiThauHangMuc>
    {
        void DeleteByParentId(Guid iIdGoiThau);
    }
}
