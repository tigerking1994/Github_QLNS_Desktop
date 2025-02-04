
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.Core.Service.Impl;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{

    public  class NhDmChiPhiModelControlService : GenericControlBaseService<NhDmChiPhiModel, Core.Domain.NhDmChiPhi, NhDmChiPhiService> 
    {
        public override void CustomValueProps(NhDmChiPhiModel newRow, NhDmChiPhiModel currentRow)
        {
            base.CustomValueProps(newRow, currentRow);
            newRow.IIdChiPhi = Guid.Empty;
        }
    }
}

