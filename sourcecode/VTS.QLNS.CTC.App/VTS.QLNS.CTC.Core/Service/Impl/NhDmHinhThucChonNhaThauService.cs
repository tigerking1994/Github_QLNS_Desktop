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
    public class NhDmHinhThucChonNhaThauService : IService<NhDmHinhThucChonNhaThau>, INhDmHinhThucChonNhaThauService
    {
        private INhDmHinhThucChonNhaThauRepository _nhDmHinhThucChonNhaThauRepository;

        public NhDmHinhThucChonNhaThauService(INhDmHinhThucChonNhaThauRepository nhDmHinhThucChonNhaThauRepository)
        {
            _nhDmHinhThucChonNhaThauRepository = nhDmHinhThucChonNhaThauRepository;
        }

        public IEnumerable<NhDmHinhThucChonNhaThau> FindAll()
        {
            return _nhDmHinhThucChonNhaThauRepository.FindAll();
        }

        public override void AddOrUpdateRange(IEnumerable<NhDmHinhThucChonNhaThau> listEntities, AuthenticationInfo authenticationInfo)
        {
            var lstInsert = listEntities.Where(t => t.Id.IsNullOrEmpty() && !t.SMaHinhThucChonNhaThau.IsEmpty() && !t.IsDeleted).ToList();
            var lstUpdate = listEntities.Where(t => !t.Id.IsNullOrEmpty() && !t.IsDeleted).ToList();
            var lstDelete = listEntities.Where(t => !t.Id.IsNullOrEmpty() && t.IsDeleted).ToList();

            var lstAll = _nhDmHinhThucChonNhaThauRepository.FindAll();
            var lstNotDeleteUpdate = lstAll.Where(x => !lstDelete.Select(y => y.Id).Contains(x.Id) && !lstUpdate.Select(y => y.Id).Contains(x.Id));
            var lstCheckDuplicate = lstNotDeleteUpdate.Concat(lstInsert).Concat(lstUpdate);
            var lstMaDuplicate = lstCheckDuplicate.GroupBy(x => x.SMaHinhThucChonNhaThau.ToUpper()).Where(g => g.Count() > 1).Select(y => y.Key).ToList();

            if (lstMaDuplicate != null && lstMaDuplicate.Count() > 0)
            {
                throw new ArgumentException("Mã hình thức chọn nhà thầu " + lstMaDuplicate.FirstOrDefault() + " bị lặp, vui lòng thử lại!");
            }

            if (lstInsert != null && lstInsert.Count() > 0)
            {
                lstInsert.ForEach(item => { item.Id = Guid.NewGuid(); });
                _nhDmHinhThucChonNhaThauRepository.AddRange(lstInsert);
            }
            if (lstUpdate != null && lstUpdate.Count() > 0)
            {
                _nhDmHinhThucChonNhaThauRepository.UpdateRange(lstUpdate);
            }
            if (lstDelete != null && lstDelete.Count() > 0)
            {
                _nhDmHinhThucChonNhaThauRepository.RemoveRange(lstDelete);
            }
        }

        public override IEnumerable<NhDmHinhThucChonNhaThau> FindAll(AuthenticationInfo authenticationInfo)
        {
            IEnumerable<NhDmHinhThucChonNhaThau> results = _nhDmHinhThucChonNhaThauRepository.FindAll().OrderBy(x => x.IThuTu).ToList();
            return results;
        }

        public NhDmHinhThucChonNhaThau FindByMaHinhThuc(string sMaHinhThuc)
        {
            return _nhDmHinhThucChonNhaThauRepository.FindAll().FirstOrDefault(x => x.SMaHinhThucChonNhaThau == sMaHinhThuc);
        }

        /// <summary>
        ///     Check trùng mã danh mục hình thức chọn nhà thầu
        /// </summary>
        /// <param name="sMaHinhThuc"></param>
        /// <param name="Id">
        ///     Nếu có Id thì là sửa dòng, nếu không có Id là thêm mới dòng.
        /// </param>
        /// <returns>
        ///     true: bị trùng
        ///     false: không trùng
        /// </returns>
        public bool IsDuplicateHinhThucChonNhaThau(string sMaHinhThuc, Guid Id)
        {
            var lstCheck = _nhDmHinhThucChonNhaThauRepository.FindAll().Where(x => x.SMaHinhThucChonNhaThau.ToUpper().Trim() == sMaHinhThuc.ToUpper().Trim());
            if (Id.IsNullOrEmpty())
            {
                // Thêm mới thì chỉ cần check trùng tên
                if (lstCheck.Any())
                {
                    return true;
                }
            }
            else
            {
                // Chỉnh sửa thì check thêm id
                var lstCheckEdit = lstCheck.Where(x => x.Id != Id);
                if (lstCheckEdit.Any())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
