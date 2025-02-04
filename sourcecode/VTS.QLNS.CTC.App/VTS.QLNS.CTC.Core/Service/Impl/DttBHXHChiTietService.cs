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
    public class DttBHXHChiTietService : IDttBHXHChiTietService
    {
        private readonly IDttBHXHChiTietRepository _dttBHXHChiTietRepository;

        public DttBHXHChiTietService(IDttBHXHChiTietRepository dttBHXHChiTietRepository)
        {
            _dttBHXHChiTietRepository = dttBHXHChiTietRepository;
        }

        public int AddRange(IEnumerable<BhDttBHXHChiTiet> dttBhxhChiTiets)
        {
            return _dttBHXHChiTietRepository.AddRange(dttBhxhChiTiets);
        }

        public bool ExistBHXHChiTiet(Guid bhxhId)
        {
            return _dttBHXHChiTietRepository.ExistBHXHChiTiet(bhxhId);
        }

        public IEnumerable<BhDttBHXHChiTiet> FindAllChungTuDuToan()
        {
            return _dttBHXHChiTietRepository.FindAll();
        }

        public BhDttBHXHChiTiet FindById(Guid id)
        {
            return _dttBHXHChiTietRepository.FindById(id);
        }

        public IEnumerable<BhDttBHXHChiTiet> FindByIdDTT(Guid Id)
        {
            return _dttBHXHChiTietRepository.FindByIdDTT(Id);
        }

        public IEnumerable<BhDttBHXHChiTiet> FindByParentId(DttBHXHChiTietCriteria searchCondition)
        {
            return _dttBHXHChiTietRepository.FindByParentId(searchCondition);
        }

        public IEnumerable<BhDttBHXHChiTiet> FindDttBHXHChiTietByIdBhxh(DttBHXHChiTietCriteria searchModel)
        {
            return _dttBHXHChiTietRepository.FindDttBHXHChiTietByIdBhxh(searchModel);
        }

        public int RemoveRange(IEnumerable<BhDttBHXHChiTiet> bhxhChungTuChiTiets)
        {
            return _dttBHXHChiTietRepository.RemoveRange(bhxhChungTuChiTiets);
        }

        public int Update(BhDttBHXHChiTiet item)
        {
            return _dttBHXHChiTietRepository.Update(item);
        }
    }
}
