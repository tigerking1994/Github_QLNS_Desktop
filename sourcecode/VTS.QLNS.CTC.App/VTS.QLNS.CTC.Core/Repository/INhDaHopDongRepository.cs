using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhDaHopDongRepository : IRepository<NhDaHopDong>
    {
        IEnumerable<NhDaHopDong> FindAll(AuthenticationInfo authenticationInfo);
        IEnumerable<NhDaHopDongQuery> FindAllHopDong(int? iThuocMenu = null);
        IEnumerable<NhDaHopDongQuery> FindAllHopDongtrongnuoc(int? iThuocMenu = null);
        IEnumerable<NhDaHopDongQuery> FindAllHopDongNgoaiThuong(int? iThuocMenu = null);
        void DeleteHopDong(Guid IdHopDong);
        IEnumerable<NhDaHopDongTrongNuocQuery> FindAllHopDongTrongNuoc();
        IEnumerable<NhDaHopDong> FindByIdKHTongTheNhiemVuChi(Guid? idKHTongTheNhiemVuChi);
        IEnumerable<NhDaHopDongQuery> FindByIdDonVi(Guid? IIdDonViQuanLyId);
        int AddOrUpdateRange(IEnumerable<NhDaHopDong> entities, int iNamLamViec);
        void AddOrUpdateRange(IEnumerable<NhDaHopDong> entities, AuthenticationInfo authenticationInfo);
        IEnumerable<NhDmLoaiHopDong> GetAllLoaiHopDong();
    }
}