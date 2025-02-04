using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlCanBoCheDoBHXHRepository : IRepository<TlCanBoCheDoBHXH>
    {
        public IEnumerable<TlCanBoCheDoBHXHQuery> GetDataCheDoBHXH(string maCanBo);
        bool ExistCanBoCheDo(string maCanBo);
        IEnumerable<TlCanBoCheDoBHXH> GetSoNgayHuongBHXH(string maCanBo);
        IEnumerable<TlCanBoCheDoBHXHQuery> ExportCanBoCheDo(int year, string months);
        TlCanBoCheDoBHXH FindByCondition(string maCanBo, string maCheDo);
        IEnumerable<TlCanBoCheDoBHXHQuery> GetCanBoCheDoIndex(string maCanBo);
    }
}
