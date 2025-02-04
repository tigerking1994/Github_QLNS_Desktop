using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NdtctgBHXHService : INdtctgBHXHService
    {
        private readonly INdtctgBHXHRepository _iNdtctgBHXHRepository;
        public NdtctgBHXHService(INdtctgBHXHRepository iNdtctgBHXHRepository)
        {
            _iNdtctgBHXHRepository = iNdtctgBHXHRepository;
        }

        public IEnumerable<BhDtctgBHXH> FindByCondition(Expression<Func<BhDtctgBHXH, bool>> predicate)
        {
            return _iNdtctgBHXHRepository.FindByCondition(predicate);
        }
        public IEnumerable<BhDtctgBHXHQuery> FindByYear(int namLamViec)
        {
            return _iNdtctgBHXHRepository.FindByYear(namLamViec);
        }

        public BhDtctgBHXH FindById(Guid Id)
        {
            return _iNdtctgBHXHRepository.Find(Id);
        }
        public int Delete(BhDtctgBHXH item)
        {
            return _iNdtctgBHXHRepository.Delete(item);
        }

        public int Add(BhDtctgBHXH item)
        {
            return _iNdtctgBHXHRepository.Add(item);
        }
        public int AddRange(IEnumerable<BhDtctgBHXH> items)
        {
            return _iNdtctgBHXHRepository.AddRange(items);
        }

        public int Update(BhDtctgBHXH item)
        {
            return _iNdtctgBHXHRepository.Update(item);
        }

        public int GetSoChungTuIndexByCondition(int iNamLamViec)
        {
            return _iNdtctgBHXHRepository.GetSoChungTuIndexByCondition(iNamLamViec);
        }
        public IEnumerable<BhDtctgBHXHQuery> GetDuToanDanhSachDotNhanPhanBo(int iNamLamViec, DateTime date, int iLoaiDuToanNhan)
        {
            return _iNdtctgBHXHRepository.GetDuToanDanhSachDotNhanPhanBo(iNamLamViec, date, iLoaiDuToanNhan);
        }

        public bool IsExitsDuToanDaDuocPhanBo(Guid iDuToanNhan, Guid iDuToanPhanBo)
        {
            return _iNdtctgBHXHRepository.IsExitsDuToanDaDuocPhanBo(iDuToanNhan, iDuToanPhanBo);
        }
    }
}
