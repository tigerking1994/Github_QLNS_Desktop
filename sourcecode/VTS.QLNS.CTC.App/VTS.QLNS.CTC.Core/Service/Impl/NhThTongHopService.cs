using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhThTongHopService : INhThTongHopService
    {
        private readonly INhThTongHopRepository _repository;
        public NhThTongHopService(INhThTongHopRepository repository)
        {
            _repository = repository;
        }

        public void InsertNHTongHop_Tang(string sLoai, int iTypeExecute, Guid iIdQuyetDinh, Guid? iIDQuyetDinhOld = null)
        {
            if (!iIDQuyetDinhOld.HasValue)
                iIDQuyetDinhOld = Guid.NewGuid();
            _repository.InsertNHTongHop_Tang(sLoai, iTypeExecute, iIdQuyetDinh, iIDQuyetDinhOld.Value);
        }
        public void InsertNHTongHop_New(string sLoai, int iTypeExecute, Guid iIdQuyetDinh, Guid? iIDQuyetDinhOld = null)
        {
            if (!iIDQuyetDinhOld.HasValue)
                iIDQuyetDinhOld = Guid.NewGuid();
            _repository.InsertNHTongHop_New(sLoai, iTypeExecute, iIdQuyetDinh, iIDQuyetDinhOld.Value);
        }
        public void InsertNHTongHop_Giam(string sLoai, int iTypeExecute, Guid iIdQuyetDinh, Guid? iIDQuyetDinhOld = null)
        {
            if (!iIDQuyetDinhOld.HasValue)
                iIDQuyetDinhOld = Guid.NewGuid();
            _repository.InsertNHTongHop_Giam(sLoai, iTypeExecute, iIdQuyetDinh, iIDQuyetDinhOld.Value);
        }
        public void DeleteNHTongHop_Giam(string sLoai, Guid iIdQuyetDinh)
        {
            _repository.DeleteNHTongHop_Giam(sLoai, iIdQuyetDinh);
        }
        public void InsertNHTongHop(Guid iIDChungTu, string sLoai, List<NHTHTongHopQuery> lstData)
        {
            _repository.InsertNHTongHop(iIDChungTu, sLoai, lstData);
        }
        public IEnumerable<NHTHTongHop> FindByCondition(Expression<Func<NHTHTongHop, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }
    }
}
