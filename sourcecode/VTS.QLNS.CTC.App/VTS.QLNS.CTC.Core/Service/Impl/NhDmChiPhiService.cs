using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;
using System.Linq;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDmChiPhiService : IService<NhDmChiPhi>, INhDmChiPhiService
    {
        private readonly INhDmChiPhiRepository _nhDmChiPhiRepository;

        public NhDmChiPhiService(INhDmChiPhiRepository nhDmChiPhiRepository)
        {
            _nhDmChiPhiRepository = nhDmChiPhiRepository;
        }

        public override void AddOrUpdateRange(IEnumerable<NhDmChiPhi> listEntities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            foreach (var item in listEntities)
            {
                if (item.IsModified)
                {
                    if (Guid.Empty.Equals(item.IIdChiPhi))
                    {
                        item.DNgayTao = time;
                        item.SIdMaNguoiDungTao = authenticationInfo.Principal;
                    }
                }
            }
            var lstInsert = listEntities.Where(t => t.IIdChiPhi.IsNullOrEmpty() && !t.SMaChiPhi.IsEmpty() && !t.IsDeleted).ToList();
            var lstUpdate = listEntities.Where(t => !t.IIdChiPhi.IsNullOrEmpty() && !t.IsDeleted).ToList();
            var lstDelete = listEntities.Where(t => !t.IIdChiPhi.IsNullOrEmpty() && t.IsDeleted).ToList();

            var lstAll = _nhDmChiPhiRepository.FindAll();
            var lstNotDeleteUpdate = lstAll.Where(x => !lstDelete.Select(y => y.IIdChiPhi).Contains(x.IIdChiPhi) && !lstUpdate.Select(y => y.IIdChiPhi).Contains(x.IIdChiPhi));
            var lstCheckDuplicate = lstNotDeleteUpdate.Concat(lstInsert).Concat(lstUpdate);
            var lstMaDuplicate = lstCheckDuplicate.GroupBy(x => x.SMaChiPhi.ToUpper()).Where(g => g.Count() > 1).Select(y => y.Key).ToList();

            if (lstMaDuplicate != null && lstMaDuplicate.Count() > 0)
            {
                throw new ArgumentException("Mã chi phí " + lstMaDuplicate.FirstOrDefault() + " bị lặp, vui lòng thử lại!");
            }

            if (lstInsert != null && lstInsert.Count() > 0)
            {
                lstInsert.ForEach(item => { item.IIdChiPhi = Guid.NewGuid(); });
                _nhDmChiPhiRepository.AddRange(lstInsert);
            }
            if (lstUpdate != null && lstUpdate.Count() > 0)
            {
                _nhDmChiPhiRepository.UpdateRange(lstUpdate);
            }
            if (lstDelete != null && lstDelete.Count() > 0)
            {
                _nhDmChiPhiRepository.RemoveRange(lstDelete);
            }
        }

        public override IEnumerable<NhDmChiPhi> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _nhDmChiPhiRepository.FindAll(authenticationInfo).OrderBy(x => x.IThuTu);
        }

        public IEnumerable<NhDmChiPhi> FindAll()
        {
            return _nhDmChiPhiRepository.FindAll();
        }

        public NhDmChiPhi FindById(Guid? id)
        {
            return _nhDmChiPhiRepository.Find(id);
        }
    }
}
