using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class DmChuDauTuService : IService<DmChuDauTu>, IDmChuDauTuService
    {
        private readonly IDmChuDauTuRepository _dmChuDauTuRepository;

        public DmChuDauTuService(IDmChuDauTuRepository dmChuDauTuRepository)
        {
            _dmChuDauTuRepository = dmChuDauTuRepository;
        }

        public override void AddOrUpdateRange(IEnumerable<DmChuDauTu> listEntities, AuthenticationInfo authenticationInfo)
        {
            IEnumerable<Guid> parentIds = listEntities.Where(t => t.IIDDonViCha.HasValue).Select(t => t.IIDDonViCha.Value);
            foreach (DmChuDauTu dmChuDauTu in listEntities)
            {
                dmChuDauTu.INamLamViec = authenticationInfo.YearOfWork;
            }
            _dmChuDauTuRepository.AddOrUpdateRange(listEntities);
            _dmChuDauTuRepository.UpdateBHangChaToTrue(parentIds);
        }

        public override IEnumerable<DmChuDauTu> FindAll(AuthenticationInfo authenticationInfo)
        {
            IEnumerable<DmChuDauTu> result = _dmChuDauTuRepository.FindDmChuDauTuByByNamLamViec(authenticationInfo.YearOfWork);
            IEnumerable<Guid?> parentIds = result.Where(t => t.IIDDonViCha.HasValue).Select(t => t.IIDDonViCha);
            IEnumerable<DmChuDauTu> parents = _dmChuDauTuRepository.FindAll(t => parentIds.Contains(t.Id)).ToList();
            foreach (DmChuDauTu dmChuDauTu in result)
            {
                if (dmChuDauTu.IIDDonViCha.HasValue)
                    dmChuDauTu.TenCdtParent = parents.FirstOrDefault(p => p.Id.Equals(dmChuDauTu.IIDDonViCha))?.STenDonVi;
            }
            return result;
        }

        public DmChuDauTu FindById(Guid id)
        {
            return _dmChuDauTuRepository.Find(id);
        }

        public IEnumerable<DmChuDauTu> FindByNamLamViec(int yearOfWork)
        {
            return _dmChuDauTuRepository.FindByNamLamViec(yearOfWork);
        }

        public IEnumerable<DmChuDauTu> FindByAllDataDonVi()
        {
            return _dmChuDauTuRepository.FindByAllDataDonVi();
        }

        public DmChuDauTu FindByMaDonVi(string iIDMaDonVi, int namLamViec)
        {
            return _dmChuDauTuRepository.FindByMaDonVi(iIDMaDonVi, namLamViec);
        }

        public DmChuDauTu FindAllByMaDonVi(string maDonVi)
        {
            return _dmChuDauTuRepository.FindAllByMaDonVi(maDonVi);
        }

        public DmChuDauTu FindByParentId(Guid id, int namLamViec)
        {
            return _dmChuDauTuRepository.FindByParentId(id, namLamViec);
        }

        public DmChuDauTu FindByAllParentId(Guid id)
        {
            return _dmChuDauTuRepository.FindAllByParentId(id);
        }

        public List<DmChuDauTu> FindByIdDonViCha(Guid id, int namLamViec)
        {
            return _dmChuDauTuRepository.FindByIdDonViCha(id, namLamViec);
        }
        public List<DmChuDauTu> FindByAllIdDonViCha(Guid id)
        {
            return _dmChuDauTuRepository.FindByAllIdDonViCha(id);
        }

        public IEnumerable<DmChuDauTu> FindByCondition(Expression<Func<DmChuDauTu, bool>> predicate)
        {
            return _dmChuDauTuRepository.FindAll(predicate);
        }

        public List<DmChuDauTu> FindByDuAnId(Guid id)
        {
            return _dmChuDauTuRepository.FindByDuAnId(id);
        }
        public DmChuDauTu FindByIdDuAn(Guid idDuAn)
        {
            return _dmChuDauTuRepository.FindByIdDuAn(idDuAn);
        }
        public DmChuDauTu GetChuDauTuByVdtDuAnId(Guid iIdDuAnId)
        {
            return _dmChuDauTuRepository.GetChuDauTuByVdtDuAnId(iIdDuAnId);
        }
        public IEnumerable<DmChuDauTu> FindByAll()
        {
            return _dmChuDauTuRepository.FindByAll();
        }
    }
}
