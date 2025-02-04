using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service.Impl;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class NhDaDuAnModelControlService : GenericControlBaseService<NhDaDuAnModel, NhDaDuAn, NhDaDuAnService>
    {
        public override void CustomValueProps(NhDaDuAnModel newRow, NhDaDuAnModel currentRow)
        {
            base.CustomValueProps(newRow, currentRow);
            newRow.Id = Guid.Empty;
        }
    }
}
