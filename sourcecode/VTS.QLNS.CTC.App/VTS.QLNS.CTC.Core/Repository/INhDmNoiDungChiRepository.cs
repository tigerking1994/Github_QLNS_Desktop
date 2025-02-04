using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhDmNoiDungChiRepository : IRepository<NhDmNoiDungChi>
    {
        IEnumerable<NhDmNoiDungChi> FindAll(AuthenticationInfo authenticationInfo);
    }
}
