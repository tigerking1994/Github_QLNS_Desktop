using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtDuAnHangMucService : IVdtDuAnHangMucService
    {
        private readonly IVdtDuAnHangMucRepository _vdtDuAnHangMucRepository;

        public VdtDuAnHangMucService(IVdtDuAnHangMucRepository vdtDuAnHangMucRepository)
        {
            _vdtDuAnHangMucRepository = vdtDuAnHangMucRepository;
        }

        public VdtDaDuAnHangMuc Add(VdtDaDuAnHangMuc entity)
        {
            _vdtDuAnHangMucRepository.Add(entity);
            return entity;
        }

        public IEnumerable<VdtDaDuAnHangMuc> FindAll(Expression<Func<VdtDaDuAnHangMuc, bool>> predicate)
        {
            return _vdtDuAnHangMucRepository.FindAll(predicate);
        }

        public int AddRange(IEnumerable<VdtDaDuAnHangMuc> entities)
        {
            return _vdtDuAnHangMucRepository.AddRange(entities);
        }

        public void DeleteByDuAnId(Guid duanId)
        {
            _vdtDuAnHangMucRepository.DeleteByDuAnId(duanId);
        }

        public IEnumerable<VdtDaDuAnHangMuc> FindByDuAnHangMuc(Guid idDuAn, int? nguonVon, Guid? idLoaiCongTrinh)
        {
            return _vdtDuAnHangMucRepository.FindByDuAnHangMuc(idDuAn, nguonVon, idLoaiCongTrinh);
        }

        public VdtDaDuAnHangMuc FindById(Guid id)
        {
            return _vdtDuAnHangMucRepository.Find(id);
        }

        public IEnumerable<VdtDaDuAnHangMuc> FindByIdDuAn(Guid idDuAn)
        {
            return _vdtDuAnHangMucRepository.FindByIdDuAn(idDuAn);
        }

        public int FindNextSoChungTuIndex()
        {
            return _vdtDuAnHangMucRepository.FindNextSoChungTuIndex();
        }

        public int Update(VdtDaDuAnHangMuc entity)
        {
            return _vdtDuAnHangMucRepository.Update(entity);
        }
    }
}
