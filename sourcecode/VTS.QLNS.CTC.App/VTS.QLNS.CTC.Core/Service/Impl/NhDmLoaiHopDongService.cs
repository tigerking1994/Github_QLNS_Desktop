using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDmLoaiHopDongService : IService<NhDmLoaiHopDong>, INhDmLoaiHopDongService
    {
        private readonly INhDmLoaiHopDongRepository _nhDmLoaiHopDongRepository;

        public NhDmLoaiHopDongService(INhDmLoaiHopDongRepository nhDmLoaiHopDongRepository)
        {
            _nhDmLoaiHopDongRepository = nhDmLoaiHopDongRepository;
        }

        public List<NhDmLoaiHopDong> FindAll() => (List<NhDmLoaiHopDong>)_nhDmLoaiHopDongRepository.FindAll();

        public override void AddOrUpdateRange(IEnumerable<NhDmLoaiHopDong> listEntities, AuthenticationInfo authenticationInfo)
        {
            var lstInsert = listEntities.Where(t => t.IIdLoaiHopDongId.IsNullOrEmpty() && !t.SMaLoaiHopDong.IsEmpty() && !t.IsDeleted).ToList();
            var lstUpdate = listEntities.Where(t => !t.IIdLoaiHopDongId.IsNullOrEmpty() && !t.IsDeleted).ToList();
            var lstDelete = listEntities.Where(t => !t.IIdLoaiHopDongId.IsNullOrEmpty() && t.IsDeleted).ToList();

            var lstAll = _nhDmLoaiHopDongRepository.FindAll();
            var lstNotDeleteUpdate = lstAll.Where(x => !lstDelete.Select(y => y.IIdLoaiHopDongId).Contains(x.IIdLoaiHopDongId) && !lstUpdate.Select(y => y.IIdLoaiHopDongId).Contains(x.IIdLoaiHopDongId));
            var lstCheckDuplicate = lstNotDeleteUpdate.Concat(lstInsert).Concat(lstUpdate);
            var lstMaDuplicate = lstCheckDuplicate.GroupBy(x => x.SMaLoaiHopDong.ToUpper()).Where(g => g.Count() > 1).Select(y => y.Key).ToList();

            if (lstMaDuplicate != null && lstMaDuplicate.Count() > 0)
            {
                throw new ArgumentException("Mã loại hợp đồng " + lstMaDuplicate.FirstOrDefault() + " bị lặp, vui lòng thử lại!");
            }

            if (lstInsert != null && lstInsert.Count() > 0)
            {
                lstInsert.ForEach(item => { item.IIdLoaiHopDongId = Guid.NewGuid(); });
                _nhDmLoaiHopDongRepository.AddRange(lstInsert);
            }
            if (lstUpdate != null && lstUpdate.Count() > 0)
            {
                _nhDmLoaiHopDongRepository.UpdateRange(lstUpdate);
            }
            if (lstDelete != null && lstDelete.Count() > 0)
            {
                _nhDmLoaiHopDongRepository.RemoveRange(lstDelete);
            }
        }

        public override IEnumerable<NhDmLoaiHopDong> FindAll(AuthenticationInfo authenticationInfo)
        {
            IEnumerable<NhDmLoaiHopDong> results = _nhDmLoaiHopDongRepository.FindAll().OrderBy(x => x.IThuTu).ToList();
            return results;
        }

        public IEnumerable<NhDmLoaiHopDong> FindAll(Expression<Func<NhDmLoaiHopDong, bool>> predicate)
        {
            return _nhDmLoaiHopDongRepository.FindAll();
        }
    }
}
