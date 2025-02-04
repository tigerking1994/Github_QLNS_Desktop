using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NsNguoiDungLnsService : INsNguoiDungLnsService
    {
        private INsNguoiDungLnsRepository _nguoiDungRepository;

        public NsNguoiDungLnsService(INsNguoiDungLnsRepository nguoiDungRepository)
        {
            _nguoiDungRepository = nguoiDungRepository;
        }

        public IEnumerable<NsNguoiDungLns> FindAll(Expression<Func<NsNguoiDungLns, bool>> predicate)
        {
            return _nguoiDungRepository.FindAll(predicate);
        }
    }
}
