using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlPhuCapMlnsNq104Repository : IRepository<TlPhuCapMlnNq104>
    {
        int CountByYear(int year);
    }
}
