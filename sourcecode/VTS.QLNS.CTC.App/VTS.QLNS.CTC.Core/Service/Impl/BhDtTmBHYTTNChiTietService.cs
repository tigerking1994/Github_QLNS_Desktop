using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhDtTmBHYTTNChiTietService : IBhDtTmBHYTTNChiTietService
    {
        private readonly IBhDtTmBHYTTNChiTietRepository _bhDtTmBHYTTNChiTietRepository;
        public BhDtTmBHYTTNChiTietService(IBhDtTmBHYTTNChiTietRepository bhDtTmBHYTTNChiTietRepository)
        {
            _bhDtTmBHYTTNChiTietRepository = bhDtTmBHYTTNChiTietRepository;
        }

        public int AddRange(IEnumerable<BhDtTmBHYTTNChiTiet> dttmBhytChiTiets)
        {
            return _bhDtTmBHYTTNChiTietRepository.AddRange(dttmBhytChiTiets);
        }

        public bool ExistBHXHChiTiet(Guid bhytId)
        {
            return _bhDtTmBHYTTNChiTietRepository.ExistBHXHChiTiet(bhytId);
        }

        public IEnumerable<BhDtTmBHYTTNChiTiet> FindAllChungTuDuToan()
        {
            return _bhDtTmBHYTTNChiTietRepository.FindAll();
        }

        public BhDtTmBHYTTNChiTiet FindById(Guid id)
        {
            return _bhDtTmBHYTTNChiTietRepository.FindById(id);
        }

        public IEnumerable<BhDtTmBHYTTNChiTiet> FindByParentId(DtTmBHYTTNChiTietCriteria searchCondition)
        {
            return _bhDtTmBHYTTNChiTietRepository.FindByParentId(searchCondition);
        }

        public IEnumerable<BhDtTmBHYTTNChiTiet> FindDttmBHYTChiTietById(DtTmBHYTTNChiTietCriteria searchModel)
        {
            return _bhDtTmBHYTTNChiTietRepository.FindDttmBHYTChiTietById(searchModel);
        }

        public int RemoveRange(IEnumerable<BhDtTmBHYTTNChiTiet> bhxhChungTuChiTiets)
        {
            return _bhDtTmBHYTTNChiTietRepository.RemoveRange(bhxhChungTuChiTiets);
        }

        public int Update(BhDtTmBHYTTNChiTiet item)
        {
            return _bhDtTmBHYTTNChiTietRepository.Update(item);
        }
    }
}
