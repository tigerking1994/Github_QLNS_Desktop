using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtTtThongTinCanCuRepository : IRepository<VdtTtThongTinCanCu>
    {        
        List<VdtTtThongTinCanCu> GetThongTinCanCuByIdDeNghiThanhToan(Guid? iID_DeNghiThanhToanID);
    }
}
