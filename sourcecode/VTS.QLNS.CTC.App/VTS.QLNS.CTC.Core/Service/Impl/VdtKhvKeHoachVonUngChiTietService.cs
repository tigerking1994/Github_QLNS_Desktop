using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtKhvKeHoachVonUngChiTietService : IVdtKhvKeHoachVonUngChiTietService
    {
        private readonly IVdtKhvKeHoachVonUngRepository _keHoachVonUngRepository;
        private readonly IVdtKhvKeHoachVonUngChiTietRepository _keHoachVonUngChiTietRepository;

        public VdtKhvKeHoachVonUngChiTietService(IVdtKhvKeHoachVonUngChiTietRepository keHoachVonUngChiTietRepository,
            IVdtKhvKeHoachVonUngRepository keHoachVonUngRepository)
        {
            _keHoachVonUngChiTietRepository = keHoachVonUngChiTietRepository;
            _keHoachVonUngRepository = keHoachVonUngRepository;
        }

        public IEnumerable<VdtKhvKeHoachVonUngChiTietQuery> GetDuAnInKeHoachVonUngDetail(Guid? iIdKhvuDxId)
        {
            return _keHoachVonUngChiTietRepository.GetDuAnInKeHoachVonUngDetail(iIdKhvuDxId);
        }

        public IEnumerable<VdtKhvKeHoachVonUngChiTietQuery> GetKeHoachVonUngChiTietByParentId(Guid iIdKeHoachVonUng)
        {
            return _keHoachVonUngChiTietRepository.GetKeHoachVonUngChiTietByParentId(iIdKeHoachVonUng);
        }

        public bool Insert(Guid parentId, List<VdtKhvKeHoachVonUngChiTiet> lstChild)
        {
            _keHoachVonUngChiTietRepository.DeleteKeHoachVonUngChiTietByParentId(parentId);
            if (_keHoachVonUngChiTietRepository.AddRange(lstChild) == 0) return false;
            var parentData = _keHoachVonUngRepository.Find(parentId);
            if (parentData == null || parentData.Id == Guid.Empty) return false;
            parentData.FGiaTriUng = lstChild.Sum(n => (n.FCapPhatBangLenhChi ?? 0) + (n.FCapPhatTaiKhoBac ?? 0) + (n.FTonKhoanTaiDonVi ?? 0));
            return _keHoachVonUngRepository.Update(parentData) != 0;
        }

        public double GetkeHoachUng(Guid iIdDuAnId, DateTime dNgayBaoCao)
        {
            return _keHoachVonUngChiTietRepository.GetkeHoachUng(iIdDuAnId, dNgayBaoCao);
        }

        public VdtKhvKeHoachVonUngChiTiet FindById(Guid id)
        {
            return _keHoachVonUngChiTietRepository.Find(id);
        }
    }
}
