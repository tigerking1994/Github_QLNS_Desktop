using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class QtcCapKinhPhiKcbChiTietService : IQtcCapKinhPhiKcbChiTietService
    {
        private readonly IQtcCapKinhPhiKcbChiTietRepository _repository;
        public QtcCapKinhPhiKcbChiTietService(IQtcCapKinhPhiKcbChiTietRepository iQtcCapKinhPhiKcbChiTietRepository)
        {
            _repository = iQtcCapKinhPhiKcbChiTietRepository;
        }

        public int AddRange(IEnumerable<BhQtCapKinhPhiKcbChiTiet> chungTuChiTiets)
        {
            return _repository.AddRange(chungTuChiTiets);
        }

        public bool ExistVoucherDetail(Guid chungTuId)
        {
            return _repository.ExistVoucherDetail(chungTuId);
        }

        public IEnumerable<BhQtCapKinhPhiKcbChiTiet> FindAllVouchers()
        {
            return _repository.FindAll();
        }

        public BhQtCapKinhPhiKcbChiTiet FindById(Guid id)
        {
            return _repository.FindById(id);
        }

        public IEnumerable<BhQtCapKinhPhiKcbChiTiet> FindChungTuChiTietByChungTuId(BhQtCapKinhPhiKcbChiTietCriteria searchModel)
        {
            return _repository.FindChungTuChiTietByChungTuId(searchModel);
        }

        public IEnumerable<BhQtCapKinhPhiKcbChiTiet> FindVoucherDetailByCondition(BhQtCapKinhPhiKcbChiTietCriteria searchModel)
        {
            return _repository.FindVoucherDetailByCondition(searchModel);
        }

        public int RemoveRange(IEnumerable<BhQtCapKinhPhiKcbChiTiet> chungTuChiTiets)
        {
            return _repository.RemoveRange(chungTuChiTiets);
        }

        public int Update(BhQtCapKinhPhiKcbChiTiet item)
        {
            return _repository.Update(item);
        }
    }
}
