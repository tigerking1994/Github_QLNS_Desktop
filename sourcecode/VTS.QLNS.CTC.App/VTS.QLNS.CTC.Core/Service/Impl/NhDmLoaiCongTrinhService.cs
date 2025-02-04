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
    public class NhDmLoaiCongTrinhService : IService<NhDmLoaiCongTrinh>, INhDmLoaiCongTrinhService
    {
        private readonly INhDmLoaiCongTrinhRepository _repository;

        public NhDmLoaiCongTrinhService(INhDmLoaiCongTrinhRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<NhDmLoaiCongTrinh> FindAll()
        {
            return _repository.FindAll();
        }

        public override void AddOrUpdateRange(IEnumerable<NhDmLoaiCongTrinh> listEntities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            foreach (var item in listEntities)
            {
                item.Parent = null;
                if (item.IsModified)
                {
                    if (Guid.Empty.Equals(item.Id))
                    {
                        item.DNgayTao = time;
                        item.SIdMaNguoiDungTao = authenticationInfo.Principal;
                        item.DNgaySua = null;
                        item.SIdMaNguoiDungSua = null;
                    }
                    else
                    {
                        item.DNgaySua = time;
                        item.SIdMaNguoiDungSua = authenticationInfo.Principal;
                    }
                }
            }
            var lstInsert = listEntities.Where(t => t.Id.IsNullOrEmpty() && !t.SMaLoaiCongTrinh.IsEmpty() && !t.IsDeleted).ToList();
            var lstUpdate = listEntities.Where(t => !t.Id.IsNullOrEmpty() && !t.IsDeleted).ToList();
            var lstDelete = listEntities.Where(t => !t.Id.IsNullOrEmpty() && t.IsDeleted).ToList();

            var lstAll = _repository.FindAll();
            var lstNotDeleteUpdate = lstAll.Where(x => !lstDelete.Select(y => y.Id).Contains(x.Id) && !lstUpdate.Select(y => y.Id).Contains(x.Id));
            var lstCheckDuplicate = lstNotDeleteUpdate.Concat(lstInsert).Concat(lstUpdate);
            var lstMaDuplicate = lstCheckDuplicate.GroupBy(x => x.SMaLoaiCongTrinh.ToUpper()).Where(g => g.Count() > 1).Select(y => y.Key).ToList();

            if (lstMaDuplicate != null && lstMaDuplicate.Count() > 0)
            {
                throw new ArgumentException("Mã loại công trình " + lstMaDuplicate.FirstOrDefault() + " bị lặp, vui lòng thử lại!");
            }

            if (lstInsert != null && lstInsert.Count() > 0)
            {
                lstInsert.ForEach(item => { item.Id = Guid.NewGuid(); });
                _repository.AddRange(lstInsert);
            }
            if (lstUpdate != null && lstUpdate.Count() > 0)
            {
                _repository.UpdateRange(lstUpdate);
            }
            if (lstDelete != null && lstDelete.Count() > 0)
            {
                _repository.RemoveRange(lstDelete);
            }
        }

        public override IEnumerable<NhDmLoaiCongTrinh> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _repository.FindAll(authenticationInfo).OrderBy(x => x.SMaLoaiCongTrinh);
        }
        public List<NhDmLoaiCongTrinh> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
