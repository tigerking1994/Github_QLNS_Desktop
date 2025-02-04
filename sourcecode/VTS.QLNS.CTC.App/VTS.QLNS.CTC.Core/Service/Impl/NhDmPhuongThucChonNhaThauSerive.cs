using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDmPhuongThucChonNhaThauSerive : IService<NhDmPhuongThucChonNhaThau>, INhDmPhuongThucChonNhaThauService
    {
        private INhDmPhuongThucChonNhaThauRepository _nhDmPhuongThucChonNhaThauRepository;

        public NhDmPhuongThucChonNhaThauSerive(INhDmPhuongThucChonNhaThauRepository nhDmPhuongThucChonNhaThauRepository)
        {
            _nhDmPhuongThucChonNhaThauRepository = nhDmPhuongThucChonNhaThauRepository;
        }

        public IEnumerable<NhDmPhuongThucChonNhaThau> FindAll()
        {
            return _nhDmPhuongThucChonNhaThauRepository.FindAll();
        }
        public override void AddOrUpdateRange(IEnumerable<NhDmPhuongThucChonNhaThau> listEntities, AuthenticationInfo authenticationInfo)
        {
            var lstInsert = listEntities.Where(t => t.Id.IsNullOrEmpty() && !t.SMaPhuongThucChonNhaThau.IsEmpty() && !t.IsDeleted).ToList();
            var lstUpdate = listEntities.Where(t => !t.Id.IsNullOrEmpty() && !t.IsDeleted).ToList();
            var lstDelete = listEntities.Where(t => !t.Id.IsNullOrEmpty() && t.IsDeleted).ToList();

            var lstAll = _nhDmPhuongThucChonNhaThauRepository.FindAll();
            var lstNotDeleteUpdate = lstAll.Where(x => !lstDelete.Select(y => y.Id).Contains(x.Id) && !lstUpdate.Select(y => y.Id).Contains(x.Id));
            var lstCheckDuplicate = lstNotDeleteUpdate.Concat(lstInsert).Concat(lstUpdate);
            var lstMaDuplicate = lstCheckDuplicate.GroupBy(x => x.SMaPhuongThucChonNhaThau.ToUpper()).Where(g => g.Count() > 1).Select(y => y.Key).ToList();

            if (lstMaDuplicate != null && lstMaDuplicate.Count() > 0)
            {
                throw new ArgumentException("Mã phương thức chọn nhà thầu " + lstMaDuplicate.FirstOrDefault() + " bị lặp, vui lòng thử lại!");
            }

            if (lstInsert != null && lstInsert.Count() > 0)
            {
                lstInsert.ForEach(item => { item.Id = Guid.NewGuid(); });
                _nhDmPhuongThucChonNhaThauRepository.AddRange(lstInsert);
            }
            if (lstUpdate != null && lstUpdate.Count() > 0)
            {
                _nhDmPhuongThucChonNhaThauRepository.UpdateRange(lstUpdate);
            }
            if (lstDelete != null && lstDelete.Count() > 0)
            {
                _nhDmPhuongThucChonNhaThauRepository.RemoveRange(lstDelete);
            }
        }

        public override IEnumerable<NhDmPhuongThucChonNhaThau> FindAll(AuthenticationInfo authenticationInfo)
        {
            IEnumerable<NhDmPhuongThucChonNhaThau> results = _nhDmPhuongThucChonNhaThauRepository.FindAll().OrderBy(x => x.IThuTu).ToList();
            return results;
        }
    }
}
