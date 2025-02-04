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
    public interface INhDaChuTruongDauTuService
    {
        void Update(NhDaChuTruongDauTu entity);
        void Add(NhDaChuTruongDauTu entity);
        void Adjust(NhDaChuTruongDauTu entity);
        void Delete(NhDaChuTruongDauTu entity);
        void LockOrUnlock(Guid id, bool status);
        NhDaChuTruongDauTu FindById(Guid id);
        NhDaChuTruongDauTu FindByDuAnId(Guid duAnId);
        IEnumerable<NhDaChuTruongDauTuQuery> FindIndex(int yearOfWork, int ILoai);
        IEnumerable<NhDaChuTruongDauTu> FindAll();
        IEnumerable<NhDaChuTruongDauTu> FindAll(Expression<Func<NhDaChuTruongDauTu, bool>> predicate);
    }
}
