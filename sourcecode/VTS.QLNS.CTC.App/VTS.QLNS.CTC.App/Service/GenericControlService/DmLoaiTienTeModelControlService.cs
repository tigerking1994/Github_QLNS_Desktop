using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Service.Impl;


namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class DmLoaiTienTeModelControlService : GenericControlBaseService<DmLoaiTienTeModel, Core.Domain.NhDmLoaiTienTe, NhDmLoaiTienTeService>
    {
        public override void CustomValueProps(DmLoaiTienTeModel newRow, DmLoaiTienTeModel currentRow)
        {
            base.CustomValueProps(newRow, currentRow);
        }
    }
}
