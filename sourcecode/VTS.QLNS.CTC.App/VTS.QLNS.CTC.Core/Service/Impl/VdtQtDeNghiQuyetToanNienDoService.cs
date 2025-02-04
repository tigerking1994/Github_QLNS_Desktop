using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtQtDeNghiQuyetToanNienDoService : IVdtQtDeNghiQuyetToanNienDoService
    {
        private readonly IVdtQtDeNghiQuyetToanNienDoRepository _vdtQtDeNghiQuyetToanNienDoRepository;
        private readonly IVdtQtDeNghiQuyetToanNienDoChiTietRepository _quyetToanNienDoChiTietRepository;
        private readonly IVdtQtXuLySoLieuRepository _xuLySoLieuRepository;

        public VdtQtDeNghiQuyetToanNienDoService(IVdtQtDeNghiQuyetToanNienDoRepository vdtQtDeNghiQuyetToanNienDoRepository,
            IVdtQtDeNghiQuyetToanNienDoChiTietRepository quyetToanNienDoChiTietRepository,
            IVdtQtXuLySoLieuRepository xuLySoLieuRepository)
        {
            _vdtQtDeNghiQuyetToanNienDoRepository = vdtQtDeNghiQuyetToanNienDoRepository;
            _quyetToanNienDoChiTietRepository = quyetToanNienDoChiTietRepository;
            _xuLySoLieuRepository = xuLySoLieuRepository;
        }

        public List<VdtQtDenghiQuyetToanNienDoQuery> GetDeNghiQuyetToanNienDoIndex()
        {
            return _vdtQtDeNghiQuyetToanNienDoRepository.GetDeNghiQuyetToanNienDoIndex();
        }

        public bool Insert(VdtQtDeNghiQuyetToanNienDo data, string sUserLogin)
        {
            data.Id = Guid.NewGuid();
            data.DDateCreate = DateTime.Now;
            data.SUserCreate = sUserLogin;
            return _vdtQtDeNghiQuyetToanNienDoRepository.Add(data) != 0;
        }

        public bool Update(VdtQtDeNghiQuyetToanNienDo data, string sUserLogin)
        {
            var dataUpdate = _vdtQtDeNghiQuyetToanNienDoRepository.Find(data.Id);
            if (dataUpdate == null) return false;
            dataUpdate.SNguoiDeNghi = data.SNguoiDeNghi;
            dataUpdate.SSoDeNghi = data.SSoDeNghi;
            dataUpdate.DNgayDeNghi = data.DNgayDeNghi;
            return _vdtQtDeNghiQuyetToanNienDoRepository.Update(dataUpdate) != 0;
        }

        public bool DeleteDeNghiQuyetToan(VdtQtDeNghiQuyetToanNienDo data , string sUserLogin)
        {
            var dataDelete = _vdtQtDeNghiQuyetToanNienDoRepository.Find(data.Id);
            var lstDataDetail = _quyetToanNienDoChiTietRepository.DeleteByQuyetToanNienDoId(data.Id);
            _xuLySoLieuRepository.DeleteAllXuLySoLieuByParent(dataDelete);
            return _vdtQtDeNghiQuyetToanNienDoRepository.Delete(dataDelete) != 0;
        }

        public bool CheckExistDeNghiQuyetToanNienDo(Guid iIdDonVi, int iNamKeHoach, int iNguonVon, Guid iIdLoaiNguonvon)
        {
            return _vdtQtDeNghiQuyetToanNienDoRepository.CheckExistDeNghiQuyetToanNienDo(iIdDonVi, iNamKeHoach, iNguonVon, iIdLoaiNguonvon);
        }
    }
}
