using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using System.Linq.Expressions;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhKhTongTheService
    {
        int Delete(Guid id);
        int Update(NhKhTongThe nhKhTongThe);
        int Add(NhKhTongThe nhKhTongThe);
        void LockOrUnLock(Guid id, bool isActivated);
        IEnumerable<NhKhTongTheQuery> FindAllOverview();
        IEnumerable<NhKhTongThe> FindAllOrdered();
        List<NhKhTongThe> FindAll();
        IEnumerable<NhKhTongThe> FindAll(Expression<Func<NhKhTongThe, bool>> predicate);
        NhKhTongThe Find(Guid id);
        NhKhTongThe FindByPredicate(Guid id);
        bool IsExistKhTongTheNam(Guid idParent, int iNamKeHoach);
        IEnumerable<NhKhTongThe> FindByParentId(Guid idParent);
        void Add(NhKhTongThe nhKhTongThe, IEnumerable<NhKhTongTheNhiemVuChi> khTongTheNhiemVuChis);
        void Update(NhKhTongThe nhKhTongThe, IEnumerable<NhKhTongTheNhiemVuChi> khTongTheNhiemVuChis);
        void Adjust(NhKhTongThe entity, IEnumerable<NhKhTongTheNhiemVuChi> khTongTheNhiemVuChis);
        IEnumerable<NhKhTongTheNhiemVuChi> FindKHTongTheNVCByConditon(Expression<Func<NhKhTongTheNhiemVuChi, bool>> predicate);
        IEnumerable<NhKhTongThe> FindByDonViId(Guid idDonVi);
    }
}

