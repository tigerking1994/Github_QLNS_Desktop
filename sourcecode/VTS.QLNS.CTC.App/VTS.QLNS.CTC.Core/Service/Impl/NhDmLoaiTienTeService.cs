using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;
using System.Linq;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDmLoaiTienTeService : IService<NhDmLoaiTienTe>, INhDmLoaiTienTeService
    {
        private INhDmLoaiTienTeRepository _nhDmLoaiTienTeRepository;
        private INhDmTiGiaRepository _nhDmTiGiaRepository;

        public NhDmLoaiTienTeService(INhDmLoaiTienTeRepository nhDmLoaiTienTeRepository, INhDmTiGiaRepository nhDmTiGiaRepository)
        {
            _nhDmLoaiTienTeRepository = nhDmLoaiTienTeRepository;
            _nhDmTiGiaRepository = nhDmTiGiaRepository;
        }

        public override void AddOrUpdateRange(IEnumerable<NhDmLoaiTienTe> listEntities, AuthenticationInfo authenticationInfo)
        {
            var lstInsert = listEntities.Where(t => t.Id.IsNullOrEmpty() && !t.SMaTienTe.IsEmpty() && !t.IsDeleted).ToList();
            var lstUpdate = listEntities.Where(t => !t.Id.IsNullOrEmpty() && !t.IsDeleted).ToList();
            var lstDelete = listEntities.Where(t => !t.Id.IsNullOrEmpty() && t.IsDeleted).ToList();

            var lstAll = _nhDmLoaiTienTeRepository.FindAll();
            var lstNotDeleteUpdate = lstAll.Where(x => !lstDelete.Select(y => y.Id).Contains(x.Id) && !lstUpdate.Select(y => y.Id).Contains(x.Id));
            var lstCheckDuplicate = lstNotDeleteUpdate.Concat(lstInsert).Concat(lstUpdate);
            var lstMaDuplicate = lstCheckDuplicate.GroupBy(x => x.SMaTienTe.ToUpper()).Where(g => g.Count() > 1).Select(y => y.Key).ToList();

            if (lstMaDuplicate != null && lstMaDuplicate.Count() > 0)
            {
                throw new ArgumentException("Mã tiền tệ " + lstMaDuplicate.FirstOrDefault() + " bị lặp, vui lòng thử lại!");
            }

            if (lstInsert != null && lstInsert.Count() > 0)
            {
                lstInsert.ForEach(item => { item.Id = Guid.NewGuid(); });
                _nhDmLoaiTienTeRepository.AddRange(lstInsert);
            }
            if (lstUpdate != null && lstUpdate.Count() > 0)
            {
                _nhDmLoaiTienTeRepository.UpdateRange(lstUpdate);
            }
            if (lstDelete != null && lstDelete.Count() > 0)
            {
                _nhDmLoaiTienTeRepository.RemoveRange(lstDelete);
            }
        }

        public IEnumerable<NhDmLoaiTienTe> FindAll()
        {
            return _nhDmLoaiTienTeRepository.FindAll();
        }

        public override IEnumerable<NhDmLoaiTienTe> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _nhDmLoaiTienTeRepository.FindAll();
        }
       
        public NhDmLoaiTienTe FindById(Guid id)
        {
            return _nhDmLoaiTienTeRepository.Find(id);
        }
    }
}
