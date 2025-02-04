using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDmKinhPhiService
    {
        List<NsMucLucNganSach> FindByLNS(string lns);
        IEnumerable<NsMucLucNganSach> GetLoaiNganSachByNamLamViec(int iNamLamViec);
        int countMLNS(int namLamViec);
    }
}
