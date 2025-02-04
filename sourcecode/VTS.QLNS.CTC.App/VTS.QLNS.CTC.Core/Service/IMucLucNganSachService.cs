using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IMucLucNganSachService
    {
        List<NsMucLucNganSach> FindByLNS(string lns);
        IEnumerable<NsMucLucNganSach> GetLoaiNganSachByNamLamViec(int iNamLamViec);
        int countMLNS(int namLamViec);
        bool IsUsedMLNS(Guid mlnsId, int namLamViec);
        DanhMuc FindMLNSChiTietToi(int namLamViec);
        IEnumerable<NsMucLucNganSach> GetAll();
        IEnumerable<MucLucNganSachCheckDataQuery> FindHasDataMLNS(int yearOfWork, string sXauNoiMa, int loai);
        void DeleteHasDataMLNS(string sXauNoiMa, string loai, Guid uniqueidentifier);
        IEnumerable<NsMucLucNganSach> FinByCondition(Expression<Func<NsMucLucNganSach, bool>> predicate);
        NsMucLucNganSach FindByMLNS(string maPhuCap, int yearOfWork);
    }
}
