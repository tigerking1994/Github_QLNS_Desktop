using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtDaNguonVonService : IVdtDaNguonVonService
    {
        private readonly IVdtDaNguonVonRepository _vdtDaNguonVonRepository;

        public VdtDaNguonVonService(IVdtDaNguonVonRepository vdtDaNguonVonRepository)
        {
            _vdtDaNguonVonRepository = vdtDaNguonVonRepository;
        }

        public VdtDaNguonVon Add(VdtDaNguonVon entity)
        {
            _vdtDaNguonVonRepository.Add(entity);
            return entity;
        }

        public int AddRange(IEnumerable<VdtDaNguonVon> entities)
        {
           return _vdtDaNguonVonRepository.AddRange(entities);
        }

        public int Delete(Guid id)
        {
            var item = _vdtDaNguonVonRepository.Find(id);
            return _vdtDaNguonVonRepository.Delete(item);
        }

        public void DeleteByIdDuAn(Guid idDuAn)
        {
            List<VdtDaNguonVon> entitys = FindByIdDuAn(idDuAn).ToList();
            _vdtDaNguonVonRepository.RemoveRange(entitys);
            //foreach (var item in entitys)
            //{
            //    _vdtDaNguonVonRepository.Delete(item);
            //}
        }

        public IEnumerable<VdtDaNguonVon> FindAll()
        {
            return _vdtDaNguonVonRepository.FindAll();
        }

        public IEnumerable<VdtDaNguonVon> FindAll(Expression<Func<VdtDaNguonVon, bool>> predicate)
        {
            return _vdtDaNguonVonRepository.FindAll(predicate);
        }

        public VdtDaNguonVon FindById(Guid id)
        {
            return _vdtDaNguonVonRepository.Find(id);
        }

        public IEnumerable<VdtDaNguonVon> FindByIdDuAn(List<Guid?> idDuAn)
        {
            return _vdtDaNguonVonRepository.FindByIdDuAn(idDuAn);
        }

        public IEnumerable<VdtDaNguonVon> FindByIdDuAn(Guid idDuAn)
        {
            return _vdtDaNguonVonRepository.FindByIdDuAn(idDuAn);
        }

        public IEnumerable<VdtDaNguonVon> FindByNguonVon(Guid idDuAn, int nguonVon)
        {
            return _vdtDaNguonVonRepository.FindByNguonVon(idDuAn, nguonVon);
        }

        public int Update(VdtDaNguonVon entity)
        {
            return _vdtDaNguonVonRepository.Update(entity);
        }
    }
}
