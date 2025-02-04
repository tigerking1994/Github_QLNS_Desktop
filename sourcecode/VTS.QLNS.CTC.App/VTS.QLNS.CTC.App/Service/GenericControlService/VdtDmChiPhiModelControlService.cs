using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.Core.Service.Impl;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class VdtDmChiPhiModelControlService : GenericControlBaseService<VdtDmChiPhiModel, Core.Domain.VdtDmChiPhi, VdtDmChiPhiService>
    {
        public override void CustomValueProps(VdtDmChiPhiModel newRow, VdtDmChiPhiModel currentRow)
        {
            base.CustomValueProps(newRow, currentRow);
            newRow.IIdChiPhi = Guid.Empty;
        }
    }
}
