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
    public class NhDmLoaiTaiSanService : IService<NhDmLoaiTaiSan>, INhDmLoaiTaiSanService
    {
        private readonly INhDmLoaiTaiSanRepository _nhDmLoaiTaiSanRepository;

        public NhDmLoaiTaiSanService(INhDmLoaiTaiSanRepository nhDmLoaiTaiSanRepository)
        {
            _nhDmLoaiTaiSanRepository = nhDmLoaiTaiSanRepository;
        }

        public IEnumerable<NhDmLoaiTaiSan> FindAll()
        {
            return _nhDmLoaiTaiSanRepository.FindAll();
        }

        public NhDmLoaiTaiSan FindById(Guid? id) => _nhDmLoaiTaiSanRepository.Find(id);

        public void Add(NhDmLoaiTaiSan nhDmLoaiTaiSan)
        {
            _nhDmLoaiTaiSanRepository.Add(nhDmLoaiTaiSan);
        }
        public void Delete(Guid id)
        {
            _nhDmLoaiTaiSanRepository.Delete(id);
        }    
        public void Update(NhDmLoaiTaiSan nhDmLoaiTaiSan)
        {
            _nhDmLoaiTaiSanRepository.Update(nhDmLoaiTaiSan);
        }
        public override IEnumerable<NhDmLoaiTaiSan> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _nhDmLoaiTaiSanRepository.FindAll(authenticationInfo);
        }
        public IEnumerable<NhDmLoaiTaiSan> FindAll(Expression<Func<NhDmLoaiTaiSan, bool>> predicate)
        {
            return _nhDmLoaiTaiSanRepository.FindAll();
        }
        public override void AddOrUpdateRange(IEnumerable<NhDmLoaiTaiSan> listEntities, AuthenticationInfo authenticationInfo)
        {
            foreach (var item in listEntities)
            {
                if (item.IsModified)
                {
                    if (Guid.Empty.Equals(item.Id))
                    {
                        item.DNgayTao = DateTime.Now;
                        item.SNguoiTao = authenticationInfo.Principal;
                    }
                }
            }
            var lstInsert = listEntities.Where(t => t.Id.IsNullOrEmpty() && !t.SMaLoaiTaiSan.IsEmpty() && !t.IsDeleted).ToList();
            var lstUpdate = listEntities.Where(t => !t.Id.IsNullOrEmpty() && !t.IsDeleted).ToList();
            var lstDelete = listEntities.Where(t => !t.Id.IsNullOrEmpty() && t.IsDeleted).ToList();

            var lstAll = _nhDmLoaiTaiSanRepository.FindAll();
            var lstNotDeleteUpdate = lstAll.Where(x => !lstDelete.Select(y => y.Id).Contains(x.Id) && !lstUpdate.Select(y => y.Id).Contains(x.Id));
            var lstCheckDuplicate = lstNotDeleteUpdate.Concat(lstInsert).Concat(lstUpdate);
            var lstMaDuplicate = lstCheckDuplicate.GroupBy(x => x.SMaLoaiTaiSan.ToUpper()).Where(g => g.Count() > 1).Select(y => y.Key).ToList();

            if (lstMaDuplicate != null && lstMaDuplicate.Count() > 0)
            {
                throw new ArgumentException("Mã loại tài sản " + lstMaDuplicate.FirstOrDefault() + " bị lặp, vui lòng thử lại!");
            }

            if (lstInsert != null && lstInsert.Count() > 0)
            {
                lstInsert.ForEach(item => { item.Id = Guid.NewGuid(); });
                _nhDmLoaiTaiSanRepository.AddRange(lstInsert);
            }
            if (lstUpdate != null && lstUpdate.Count() > 0)
            {
                _nhDmLoaiTaiSanRepository.UpdateRange(lstUpdate);
            }
            if (lstDelete != null && lstDelete.Count() > 0)
            {
                _nhDmLoaiTaiSanRepository.RemoveRange(lstDelete);
            }
        }
    }
}
