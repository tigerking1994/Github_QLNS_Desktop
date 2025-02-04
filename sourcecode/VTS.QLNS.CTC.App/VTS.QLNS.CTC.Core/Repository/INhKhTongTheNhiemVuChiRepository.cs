using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using System.Linq.Expressions;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhKhTongTheNhiemVuChiRepository : IRepository<NhKhTongTheNhiemVuChi>
    {
        IEnumerable<NhKhTongTheNhiemVuChiQuery> FindByIdKhTongTheAndIdDonVi(Guid idKhTongThe, Guid idDonVi);
        IEnumerable<NhKhTongTheNhiemVuChiQuery> FindByIdKhTongTheAndMaDonVi(Guid idKhTongThe, string maDonVi);
        IEnumerable<NhKhTongTheNhiemVuChiQuery> FindByIdKhTongTheAndMaDonViID(Guid idKhTongThe, Guid maDonVi);
        IEnumerable<NhKhTongTheNhiemVuChiQuery> FindByIdKhTongThe(Guid idKhTongThe);
        IEnumerable<NhKhTongThe> FindKhTongTheByNvChiId(Guid idNvChi);
        IEnumerable<NhKhTongTheNhiemVuChiQuery> FindAllNvcByIdKhTongTheGiaiDoan(Guid idKhTongThe);
        IEnumerable<NhDaGoiThauQuery> FindByIdNhiemVuChi(Guid idNhiemVuChi);
        NhKhTongTheNhiemVuChiQuery FindOneByIdKhTongTheAndIdNhiemVuChi(Guid idKhTongThe, Guid idNhiemVuChi);
        NhKhTongTheNhiemVuChiQuery FindById(Guid id);
        IEnumerable<NhKhTongTheNhiemVuChiQuery> FindKHTongTheAndDmNhiemVuChi(Guid idKhTongThe);
        void AddOrUpdate(Guid khTongTheId, IEnumerable<NhKhTongTheNhiemVuChi> entities);
        IEnumerable<NhKhTongTheNhiemVuChiQuery> FindByIdDonVi(Guid? idDonVi);
        IEnumerable<NhKhTongTheNhiemVuChiQuery> FindAllDonViByKhTongTheId(Guid idKhTongThe);
    }
}
