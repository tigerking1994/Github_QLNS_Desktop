using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDaHopDongService
    {
        IEnumerable<NhDmLoaiHopDong> GetAllLoaiHopDong();
        IEnumerable<NhDaHopDongQuery> FindAllHopDong(int? iThuocMenu = null);
        IEnumerable<NhDaHopDongQuery> FindAllHopDongtrongnuoc(int? iThuocMenu = null);
        IEnumerable<NhDaHopDongQuery> FindAllHopDongNgoaiThuong(int? iThuocMenu = null);
        NhDaHopDong FindById(Guid? id);
        void LockOrUnlock(Guid id, bool BIsKhoa);
        void Update(NhDaHopDong nhDaHopDong);
        void Add(NhDaHopDong nhDaHopDong);
        void Adjust(NhDaHopDong nhDaHopDong);
        void UpdateHDNT(NhDaHopDong nhDaHopDong);
        void AddHDNT(NhDaHopDong nhDaHopDong);
        void AdjustHDNT(NhDaHopDong nhDaHopDong);
        void DeleteHopDong(Guid id);
        IEnumerable<NhDaHopDong> FindByCondition(Expression<Func<NhDaHopDong, bool>> predicate);
        IEnumerable<NhDaHopDongTrongNuocQuery> FindAllHopDongTrongNuoc();
        IEnumerable<NhDaHopDong> FindAll();
        IEnumerable<NhDaHopDong> FindByIdKHTongTheNhiemVuChi(Guid? idKHTongTheNhiemVuChi);
        IEnumerable<NhDaHopDongQuery> FindByIdDonVi(Guid? IIdDonViQuanLyId);
        int AddRange(List<NhDaHopDong> entities);
        void Delete(Guid id);
        void AddOrUpdateRange(IEnumerable<NhDaHopDong> entities, AuthenticationInfo authenticationInfo);
    }
}
