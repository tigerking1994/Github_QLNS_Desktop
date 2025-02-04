using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NsQsChungTuService : INsQsChungTuService
    {
        private INsQsChungTuRepository _chungTuRepository;

        public NsQsChungTuService(INsQsChungTuRepository chungTuRepository)
        {
            _chungTuRepository = chungTuRepository;
        }
        public List<NsQsChungTu> FindByCondition(Expression<Func<NsQsChungTu, bool>> predicate)
        {
            return _chungTuRepository.FindAll(predicate).OrderByDescending(x => x.IThangQuy).ThenByDescending(x => x.IIdMaDonVi).ToList();
        }

        /// <summary>
        /// Khóa hoặc mở khóa chứng từ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isLock"></param>
        public void LockOrUnlock(Guid id, bool isLock)
        {
            NsQsChungTu chungTu = _chungTuRepository.Find(id);
            chungTu.BKhoa = isLock;
            _chungTuRepository.Update(chungTu);
        }

        /// <summary>
        /// Thêm mới chứng từ
        /// </summary>
        /// <param name="chungTu"></param>
        public void Add(NsQsChungTu chungTu)
        {
            _chungTuRepository.Add(chungTu);
        }

        /// <summary>
        /// Cập nhật chứng từ
        /// </summary>
        /// <param name="chungTu"></param>
        public void Update(NsQsChungTu chungTu)
        {
            _chungTuRepository.Update(chungTu);
        }

        /// <summary>
        /// xóa chứng từ theo id
        /// </summary>
        /// <param name="id"></param>
        public void Delete(Guid id)
        {
            NsQsChungTu chungTu = _chungTuRepository.Find(id);
            _chungTuRepository.Delete(chungTu);
        }

        public void RemoveRange(List<NsQsChungTu> entities)
        {
            _chungTuRepository.RemoveRange(entities);
        }


        public NsQsChungTu FindById(Guid id)
        {
            return _chungTuRepository.Find(id);
        }

        /// <summary>
        /// tạo số chứng từ
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="voucherNoIndex"></param>
        /// <returns></returns>
        public string GenerateVoucherNo(int voucherNoIndex)
        {
            StringBuilder soChungTu = new StringBuilder();
            if (voucherNoIndex < 10)
            {
                soChungTu.AppendFormat("QS-0{0}", voucherNoIndex.ToString());
            }
            else if (10 <= voucherNoIndex)
            {
                soChungTu.AppendFormat("QS-{0}", voucherNoIndex.ToString());
            }
            return soChungTu.ToString();
        }

        public List<int> FindMonthOfArmy(int yearOfWork)
        {
            return _chungTuRepository.FindMonthOfArmy(yearOfWork).ToList();
        }

        public NsQsChungTu FindByMonth(int month, int yearOfWork)
        {
            return _chungTuRepository.FindByMonth(month, yearOfWork);
        }
    }
}
