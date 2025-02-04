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
    public interface INhHdnkPhuongAnNhapKhauService
    {
        void Add(NhHdnkPhuongAnNhapKhau entity);
        void Adjust(NhHdnkPhuongAnNhapKhau entity);
        void Update(NhHdnkPhuongAnNhapKhau entity);
        void Delete(NhHdnkPhuongAnNhapKhau entity);
        void LockOrUnlock(Guid id, bool status);
        NhHdnkPhuongAnNhapKhau FindById(Guid id);
        IEnumerable<NhHdnkPhuongAnNhapKhau> FindIndex(int? iLoai = null); // ILoai = null thì get all
        IEnumerable<NhHdnkPhuongAnNhapKhau> FindAll();
        IEnumerable<NhHdnkPhuongAnNhapKhau> FindAll(Expression<Func<NhHdnkPhuongAnNhapKhau, bool>> predicate);
    }
}
