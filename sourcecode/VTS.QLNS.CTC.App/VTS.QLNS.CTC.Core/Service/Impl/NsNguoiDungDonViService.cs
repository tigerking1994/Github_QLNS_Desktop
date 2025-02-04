using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NsNguoiDungDonViService : INsNguoiDungDonViService
    {
        private INsNguoiDungDonViRepository _nguoiDungDonViRepository;

        public NsNguoiDungDonViService(INsNguoiDungDonViRepository nguoiDungDonViRepository)
        {
            _nguoiDungDonViRepository = nguoiDungDonViRepository;
        }

        public bool CheckParentAgencyByUser(string userName, int yearOfWork)
        {
            return _nguoiDungDonViRepository.CheckParentAgencyByUser(userName, yearOfWork);
        }

        public IEnumerable<NguoiDungDonVi> FindAll(Expression<Func<NguoiDungDonVi, bool>> predicate)
        {
            return _nguoiDungDonViRepository.FindAll(predicate);
        }
    }
}
