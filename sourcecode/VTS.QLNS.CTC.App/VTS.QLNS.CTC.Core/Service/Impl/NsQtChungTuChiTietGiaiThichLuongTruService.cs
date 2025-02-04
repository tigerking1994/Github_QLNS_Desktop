using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NsQtChungTuChiTietGiaiThichLuongTruService : INsQtChungTuChiTietGiaiThichLuongTruService
    {
        private INsQtChungTuChiTietiGiaiThichLuongTruRepository _chungTuChiTietGiaiThichLuongTruRepository;
        public NsQtChungTuChiTietGiaiThichLuongTruService(INsQtChungTuChiTietiGiaiThichLuongTruRepository chungTuChiTietiGiaiThichLuongTruRepository)
        {
            _chungTuChiTietGiaiThichLuongTruRepository = chungTuChiTietiGiaiThichLuongTruRepository;
        }

        public void AddRange(List<NsQtChungTuChiTietGiaiThichLuongTru> listGiaiThichLuongTru)
        {
            _chungTuChiTietGiaiThichLuongTruRepository.AddRange(listGiaiThichLuongTru);
        }

        public void Delete(Guid id)
        {
            NsQtChungTuChiTietGiaiThichLuongTru entity = _chungTuChiTietGiaiThichLuongTruRepository.Find(id);
            if (entity != null)
                _chungTuChiTietGiaiThichLuongTruRepository.Delete(entity);
        }

        public void DeleteByVoucherId(Guid voucherId)
        {
            _chungTuChiTietGiaiThichLuongTruRepository.DeleteByVoucherId(voucherId);
        }

        public List<NsQtChungTuChiTietGiaiThichLuongTru> FindByCondition(Expression<Func<NsQtChungTuChiTietGiaiThichLuongTru, bool>> predicate)
        {
            return _chungTuChiTietGiaiThichLuongTruRepository.FindAll(predicate).ToList();
        }

        public NsQtChungTuChiTietGiaiThichLuongTru FindById(Guid id)
        {
            return _chungTuChiTietGiaiThichLuongTruRepository.Find(id);
        }

        public void Update(NsQtChungTuChiTietGiaiThichLuongTru giaiThichLuongTru)
        {
            _chungTuChiTietGiaiThichLuongTruRepository.Update(giaiThichLuongTru);
        }
    }
}
