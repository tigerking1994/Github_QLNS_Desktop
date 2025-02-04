using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDmMapPcDetailService
    {
        IEnumerable<TlDmMapPcDetail> FindAll();
        int AddRange(List<TlDmMapPcDetail> lstPcMapAdd);
        int Update(TlDmMapPcDetail tlDmMapPcDetail);
        int Delete(Guid id);
    }
}
