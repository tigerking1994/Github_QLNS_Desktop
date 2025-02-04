using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using System.Linq.Expressions;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhKhTongTheNhiemVuChiService
    {
        int Add(NhKhTongTheNhiemVuChi nhKhTongTheNhiemVuChi);
        int Update(NhKhTongTheNhiemVuChi nhKhTongTheNhiemVuChi);
        int Delete(Guid id);
        int AddRange(IEnumerable<NhKhTongTheNhiemVuChi> entities);
        int UpdateRange(IEnumerable<NhKhTongTheNhiemVuChi> entities);
        int RemoveRange(IEnumerable<NhKhTongTheNhiemVuChi> entities);
        IEnumerable<NhKhTongTheNhiemVuChi> FindAll();
        IEnumerable<NhKhTongTheNhiemVuChi> FindAllByKhTongTheId(Guid idKhTongThe);
        IEnumerable<NhKhTongThe> FindKhTongTheByNvChiId(Guid idNvChi);
        IEnumerable<NhKhTongTheNhiemVuChiQuery> FindAllDonViByKhTongTheId(Guid idKhTongThe);
        NhKhTongTheNhiemVuChiQuery FindOneByIdKhTongTheAndIdNhiemVuChi(Guid idKhTongThe, Guid idNhiemVuChi);
        IEnumerable<NhKhTongTheNhiemVuChiQuery> FindByIdKhTongTheAndIdDonVi(Guid idKhTongThe, Guid idDonVi);
        IEnumerable<NhDaGoiThauQuery> FindByIdNhiemVuChi(Guid idNhiemVuChi);
        IEnumerable<NhKhTongTheNhiemVuChiQuery> FindByIdKhTongTheAndMaDonVi(Guid idKhTongThe, string maDonVi);
        IEnumerable<NhKhTongTheNhiemVuChiQuery> FindByIdKhTongTheAndMaDonViID(Guid idKhTongThe, Guid maDonVi);
        IEnumerable<NhKhTongTheNhiemVuChiQuery> FindByIdKhTongThe(Guid idKhTongThe);
        IEnumerable<NhKhTongTheNhiemVuChiQuery> FindAllNvcByIdKhTongTheGiaiDoan(Guid idKhTongThe);
        NhKhTongTheNhiemVuChi Find(Guid id);
        NhKhTongTheNhiemVuChiQuery FindById(Guid id);
        void AddKHTongTheNhiemVuChi(IEnumerable<NhKhTongTheNhiemVuChi> khTongTheNhiemVuChis, IEnumerable<NhDmNhiemVuChi> dmNhiemVuChis);
        void UpdateKHTongTheNhiemVuChi(IEnumerable<NhKhTongTheNhiemVuChi> khTongTheNhiemVuChis, IEnumerable<NhDmNhiemVuChi> dmNhiemVuChis);
        void DeleteKHTongTheNhiemVuChi(IEnumerable<NhKhTongTheNhiemVuChi> khTongTheNhiemVuChis, IEnumerable<NhDmNhiemVuChi> dmNhiemVuChis);
        IEnumerable<NhKhTongTheNhiemVuChiQuery> FindKHTongTheAndDmNhiemVuChi(Guid idKhTongThe);
        IEnumerable<NhKhTongTheNhiemVuChiQuery> FindByIdDonVi(Guid? idDonVi);
        IEnumerable<NhKhTongTheNhiemVuChi> FindByCondition(Expression<Func<NhKhTongTheNhiemVuChi, bool>> predicate);

    }
}
