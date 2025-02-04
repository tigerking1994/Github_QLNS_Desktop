using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.UI.WebControls;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static Dapper.SqlMapper;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NdtctgBHXHChiTietXNMService : INdtctgBHXHChiTietXNMService
    {
        private readonly INdtctgBHXHChiTietXNMRepository _iNdtctgchitietBHXHRepository;
        public NdtctgBHXHChiTietXNMService(INdtctgBHXHChiTietXNMRepository iNdtctgchitietBHXHRepository)
        {
            _iNdtctgchitietBHXHRepository = iNdtctgchitietBHXHRepository;
        }

        public int AddRange(IEnumerable<BhDtctgBHXHChiTietXNM> lstItems)
        {
            return _iNdtctgchitietBHXHRepository.AddRange(lstItems);
        }

        public IEnumerable<BhDtctgBHXHChiTietXNM> FindByCondition(Expression<Func<BhDtctgBHXHChiTietXNM, bool>> predicate)
        {
            return _iNdtctgchitietBHXHRepository.FindAll(predicate);
        }

        public int RemoveRange(IEnumerable<BhDtctgBHXHChiTietXNM> lstItems)
        {
            return _iNdtctgchitietBHXHRepository.RemoveRange(lstItems);
        }

        public int RemoveRange(Expression<Func<BhDtctgBHXHChiTietXNM, bool>> predicate)
        {
            var data = _iNdtctgchitietBHXHRepository.FindAll(predicate);
            return _iNdtctgchitietBHXHRepository.RemoveRange(data);
        }
    }
}
