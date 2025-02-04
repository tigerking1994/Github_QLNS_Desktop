using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlGtTaiChinhService
    {
        IEnumerable<TlGtTaiChinh> FindAll();
        int Add(TlGtTaiChinh entity);
    }
}
