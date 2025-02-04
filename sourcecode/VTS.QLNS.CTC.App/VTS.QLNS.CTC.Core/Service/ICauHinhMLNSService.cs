using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ICauHinhMLNSService
    {
        IEnumerable<NsMucLucNganSach> FindAll(AuthenticationInfo authenticationInfo);
    }
}
