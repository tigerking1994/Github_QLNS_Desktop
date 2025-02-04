using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhDmCoSoYTeService
    {
        IEnumerable<BhDmCoSoYTe> FindByCondition(Expression<Func<BhDmCoSoYTe, bool>> predicate);
        List<BhDmCoSoYTe> GetListCoSoYTe(int namLamViec);
        IEnumerable<BhDmCoSoYTe> FindAll();
        BhDmCoSoYTe GetCSYTByMa(string maCSYT, int namLamViec);
        bool ExistCSYT(string maCSYT, int namLamViec);
    }
}
