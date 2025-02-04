using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtTtDeNghiThanhToanUngService
    {
        IEnumerable<VdtTtDeNghiThanhToanUngQuery> GetDeNghiThanhToanUngIndex();
        bool DeleteDeNghiThanhToanUng(VdtTtDeNghiThanhToanUng data, string sUserLogin);
        bool Insert(VdtTtDeNghiThanhToanUng data, string sUserLogin);
        bool Update(VdtTtDeNghiThanhToanUng data, string sUserLogin);
    }
}
