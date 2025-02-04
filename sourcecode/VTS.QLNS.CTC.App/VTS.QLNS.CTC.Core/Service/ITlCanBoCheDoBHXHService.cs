using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlCanBoCheDoBHXHService
    {
        IEnumerable<TlCanBoCheDoBHXHQuery> GetDataCheDoBHXH(string maCanBo);
        int AddRangeCanBoCheDo(IEnumerable<TlCanBoCheDoBHXH> entities);
        TlCanBoCheDoBHXH FindCanBoCheDo(params object[] keyValues);
        int UpdateCanBoCheDo(TlCanBoCheDoBHXH entity);
        int DeleteCanBoCheDo(Guid id);
        bool ExistCanBoCheDo(string maCanBo);
        int RemoveRange(IEnumerable<TlCanBoCheDoBHXH> canBoCheDos);
        IEnumerable<TlCanBoCheDoBHXH> FindByMaCanBo(string maCanBo);
        IEnumerable<TlCanBoCheDoBHXH> GetSoNgayHuongBHXH(string maCanBo);
        IEnumerable<TlCanBoCheDoBHXH> FindAll(Expression<Func<TlCanBoCheDoBHXH, bool>> predicate);
        IEnumerable<TlCanBoCheDoBHXHQuery> ExportCanBoCheDo(int year, string months);
        TlCanBoCheDoBHXH FindByCondition(string maCanBo, string maCheDo);
        IEnumerable<TlCanBoCheDoBHXHQuery> GetCanBoCheDoIndex(string maCanBo);
    }
}
