using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtQtBcQuyetToanNienDoService : IVdtQtBcQuyetToanNienDoService
    {
        private readonly IVdtQtBcQuyetToanNienDoRepository _repository;
        private readonly IVdtQtBcQuyetToanNienDoChiTietRepository _detailRepository;
        private readonly IVdtQtBcquyetToanNienDoChiTiet01Repository _detail01Repository;
        private readonly IVdtQtBcQuyetToanNienDoPhanTichRepository _detailPhanTichRepository;

        public VdtQtBcQuyetToanNienDoService(IVdtQtBcQuyetToanNienDoRepository repository,
            IVdtQtBcQuyetToanNienDoChiTietRepository detailRepository,
            IVdtQtBcquyetToanNienDoChiTiet01Repository detail01Repository,
            IVdtQtBcQuyetToanNienDoPhanTichRepository detailPhanTichRepository)
        {
            _repository = repository;
            _detailRepository = detailRepository;
            _detail01Repository = detail01Repository;
            _detailPhanTichRepository = detailPhanTichRepository;
        }

        public VdtQtBcQuyetToanNienDo Find(Guid iId)
        {
            return _repository.FindById(iId);
        }

        public List<VdtQtBcQuyetToanNienDoQuery> GetDeNghiQuyetToanNienDoIndex()
        {
            return _repository.GetDeNghiQuyetToanNienDoIndex();
        }

        public List<VdtQtBcQuyetToanNienDo> GetDeNghiQuyetToanNienDoByCondition(int iLoaiThanhToan, string sMaDonVi, int iNguonVon, int iNamKeHoach)
        {
            return _repository.GetDeNghiQuyetToanNienDoByCondition(iLoaiThanhToan, sMaDonVi, iNguonVon, iNamKeHoach);
        }

        public List<VdtQtBcquyetToanNienDoChiTiet1Query> GetDeNghiQuyetToanNienDoDetail(string iIdMaDonVi, int iNamKeHoach, int iIdNguonVon)
        {
            return _repository.GetDeNghiQuyetToanNienDoDetail(iIdMaDonVi, iNamKeHoach, iIdNguonVon);
        }

        public List<BcquyetToanNienDoVonUngChiTietQuery> GetDeNghiQuyetToanNienDoVonUngDetail(string iIdMaDonVi, int iNamKeHoach, int iIdNguonVon)
        {
            return _repository.GetDeNghiQuyetToanNienDoVonUngDetail(iIdMaDonVi, iNamKeHoach, iIdNguonVon);
        }

        public List<BcquyetToanNienDoVonUngChiTietQuery> GetQuyetToanNienDoVonUngByParentId(Guid iIdParentId)
        {
            return _repository.GetQuyetToanNienDoVonUngByParentId(iIdParentId);
        }

        public List<VdtQtBcquyetToanNienDoChiTiet1Query> GetQuyetToanNienDoVonNamByParentId(Guid iIdParentId)
        {
            return _repository.GetQuyetToanNienDoVonNamByParentId(iIdParentId);
        }

        public List<TongHopNguonNSDauTuQuery> GetLuyKeQuyetToanNamTruoc(int iLoaiQuyetToan, string iIdMaDonViQuanLy, int iNamKeHoach, int iIdNguonVon)
        {
            return _repository.GetLuyKeQuyetToanNamTruoc(iLoaiQuyetToan, iIdMaDonViQuanLy, iNamKeHoach, iIdNguonVon);
        }

        public List<VdtQtBcQuyetToanNienDo> GetBcQuyetToanInThongTriScreen(Guid? iIdThongTri, string iIdMaDonVi, int iNamThongTri, int iIdNguonVon)
        {
            return _repository.GetBcQuyetToanInThongTriScreen(iIdThongTri, iIdMaDonVi, iNamThongTri, iIdNguonVon);
        }

        public void Insert(VdtQtBcQuyetToanNienDo data, string sUserLogin)
        {
            data.Id = Guid.NewGuid();
            data.DDateCreate = DateTime.Now;
            data.SUserCreate = sUserLogin;
            _repository.Add(data);
        }
        public void Update(VdtQtBcQuyetToanNienDo data, string sUserLogin)
        {
            var dataUpdate = _repository.Find(data.Id);
            if (dataUpdate == null) return;
            dataUpdate.SSoDeNghi = data.SSoDeNghi;
            dataUpdate.DNgayDeNghi = data.DNgayDeNghi;
            dataUpdate.SMoTa = data.SMoTa;
            dataUpdate.BKhoa = data.BKhoa;
            _repository.Update(dataUpdate);
        }

        #region VdtQtBcquyetToanNienDoChiTiet01
        public void InsertVdtQtBcquyetToanNienDoChiTiet01(Guid iIDParentID, List<VdtQtBcQuyetToanNienDoChiTiet01> datas)
        {
            if (datas == null || datas.Count == 0) return;
            _detailRepository.DeleteDeNghiQuyetToanByParentId(iIDParentID);
            _detail01Repository.AddRange(datas);
        }
        #endregion

        public void DeleteDeNghiQuyetToan(Guid iIDParentID)
        {
            _detailRepository.DeleteDeNghiQuyetToanByParentId(iIDParentID);
            var data = _repository.Find(iIDParentID);
            if (data == null) return;
            _repository.Delete(data);
        }

        public bool CheckExistDeNghiQuyetToanNienDo(string iIdMaDonVi, int iNamKeHoach, int iNguonVon)
        {
            return _repository.CheckExistDeNghiQuyetToanNienDo(iIdMaDonVi, iNamKeHoach, iNguonVon);
        }

        public IEnumerable<VdtQtBcQuyetToanNienDoPhanTichQuery> GetBaoCaoQuyetToanNienDoPhanTich(string iIdMaDonVi, int iNamKeHoach, int iIdNguonVon)
        {
            return _detailPhanTichRepository.GetBaoCaoQuyetToanNienDoPhanTich(iIdMaDonVi, iNamKeHoach, iIdNguonVon);
        }

        public IEnumerable<VdtQtBcQuyetToanNienDoPhanTichQuery> GetBaoCaoQuyetToanNienDoPhanTichById(Guid iIdBcQuyetToanNienDo)
        {
            return _detailPhanTichRepository.GetBaoCaoQuyetToanNienDoPhanTichById(iIdBcQuyetToanNienDo);
        }

        public void AddRangePhanTich(Guid iIdParentId, List<VdtQtBcQuyetToanNienDoPhanTich> datas)
        {
            _detailPhanTichRepository.DeleteByParent(iIdParentId);
            if (datas == null) return;
            _detailPhanTichRepository.AddRange(datas);
        }

        public List<VdtQtBcQuyetToanNienDoChiTiet01> GetDenghiQuyetToanNienDoChiTiet01ByParent(Guid iIdParentId)
        {
            return _detail01Repository.GetDenghiQuyetToanNienDoChiTiet01ByParent(iIdParentId);
        }

        public List<VdtQtBcQuyetToanNienDoPhanTich> GetBcQuyetToanNienDoPhanTich(Guid iIdParentId)
        {
            return _detailPhanTichRepository.GetBcQuyetToanNienDoPhanTich(iIdParentId);
        }
    }
}
