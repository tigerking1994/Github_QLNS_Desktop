using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtQtDeNghiQuyetToanNienDoChiTietService : IVdtQtDeNghiQuyetToanNienDoChiTietService
    {
        private readonly IVdtQtDeNghiQuyetToanNienDoChiTietRepository _vdtQtDeNghiQuyetToanNienDoChiTietRepository;

        public VdtQtDeNghiQuyetToanNienDoChiTietService(IVdtQtDeNghiQuyetToanNienDoChiTietRepository vdtQtDeNghiQuyetToanNienDoChiTietRepository)
        {
            _vdtQtDeNghiQuyetToanNienDoChiTietRepository = vdtQtDeNghiQuyetToanNienDoChiTietRepository;
        }

        public List<VdtQtDenghiQuyetToanNienDoChiTietQuery> GetAllDuAnByQuyetToanNienDo(string iIdDonvi, int iNguonVon, Guid iIdLoaiNguonVon, DateTime dNgayLap, int iNamKeHoach)
        {
            return _vdtQtDeNghiQuyetToanNienDoChiTietRepository.GetAllDuAnByQuyetToanNienDo(iIdDonvi, iNguonVon, iIdLoaiNguonVon, dNgayLap, iNamKeHoach);
        }

        public List<VdtQtDenghiQuyetToanNienDoChiTietQuery> GetQuyetToanNienDoChiTietByParentid(Guid iIdQuyetToanNienDoId)
        {
            return _vdtQtDeNghiQuyetToanNienDoChiTietRepository.GetQuyetToanNienDoChiTietByParentid(iIdQuyetToanNienDoId);
        }

        public bool Insert(Guid iIdQuyetToanNienDo, List<VdtQtDeNghiQuyetToanNienDoChiTiet> lstDataInsert)
        {
            _vdtQtDeNghiQuyetToanNienDoChiTietRepository.DeleteByQuyetToanNienDoId(iIdQuyetToanNienDo);
            if (_vdtQtDeNghiQuyetToanNienDoChiTietRepository.AddRange(lstDataInsert) == 0) return false;
            return true;
        }
    }
}
