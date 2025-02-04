using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INsQtChungTuChiTietGiaiThichLuongTruService
    {
        NsQtChungTuChiTietGiaiThichLuongTru FindById(Guid id);
        List<NsQtChungTuChiTietGiaiThichLuongTru> FindByCondition(Expression<Func<NsQtChungTuChiTietGiaiThichLuongTru, bool>> predicate);
        void AddRange(List<NsQtChungTuChiTietGiaiThichLuongTru> listGiaiThichLuongTru);
        void Update(NsQtChungTuChiTietGiaiThichLuongTru giaiThichLuongTru);
        void Delete(Guid id);
        void DeleteByVoucherId(Guid voucherId);
    }
}
