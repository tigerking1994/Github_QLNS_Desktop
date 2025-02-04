using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtDaDuToanService : IVdtDaDuToanService
    {
        private readonly IVdtDaDuToanRepository _vdtDaDuToanRepository;
        private readonly IVdtDaDuToanChiPhiRepository _vdtDaDuToanChiPhiRepository;
        private readonly IVdtDaDuToanNguonVonRepository _vdtDaDuToanNguonVonRepository;
        private readonly IVdtDaDmDuToanHangMucRepository _vdtDaDmDuToanHangMucRepository;
        private readonly IVdtDaDuToanHangMucRepository _vdtDaDuToanHangMucRepository;

        public VdtDaDuToanService(
            IVdtDaDuToanRepository vdtDaDuToanRepository,
            IVdtDaDuToanChiPhiRepository vdtDaDuToanChiPhiRepository,
            IVdtDaDuToanNguonVonRepository vdtDaDuToanNguonVonRepository,
            IVdtDaDmDuToanHangMucRepository vdtDaDmDuToanHangMucRepository,
            IVdtDaDuToanHangMucRepository vdtDaDuToanHangMucRepository)
        {
            _vdtDaDuToanRepository = vdtDaDuToanRepository;
            _vdtDaDuToanChiPhiRepository = vdtDaDuToanChiPhiRepository;
            _vdtDaDuToanNguonVonRepository = vdtDaDuToanNguonVonRepository;
            _vdtDaDmDuToanHangMucRepository = vdtDaDmDuToanHangMucRepository;
            _vdtDaDuToanHangMucRepository = vdtDaDuToanHangMucRepository;
        }

        public VdtDaDuToan Add(VdtDaDuToan entity)
        {
            _vdtDaDuToanRepository.Add(entity);
            return entity;
        }

        public VdtDaDuToan FindByDuAnId(Guid duanId)
        {
            return _vdtDaDuToanRepository.FindByDuAnId(duanId);
        }

        public List<VdtDaDuToan> FindListByDuAnId(Guid duanId)
        {
            return _vdtDaDuToanRepository.FindListByDuAnId(duanId);
        }

        public void AddDuToanHangMuc(Guid duToanId, Guid qdDauTuId)
        {
            _vdtDaDuToanRepository.AddDuToanHangMuc(duToanId, qdDauTuId);
        }

        public void AddDuToanHangMucDetail(Guid duToanId, Guid qdDauTuId)
        {
            _vdtDaDuToanRepository.AddDuToanHangMucDetail(duToanId, qdDauTuId);
        }

        public int AddRangeDMDuToanHangMuc(IEnumerable<VdtDaDuToanDmHangMuc> entities)
        {
            return _vdtDaDmDuToanHangMucRepository.AddRange(entities);
        }

        public int AddRangeDuToanChiPhi(IEnumerable<VdtDaDuToanChiPhi> entities)
        {
            return _vdtDaDuToanChiPhiRepository.AddRange(entities);
        }

        public int AddRangeDuToanHangMuc(IEnumerable<VdtDaDuToanHangMuc> entities)
        {
            return _vdtDaDuToanHangMucRepository.AddRange(entities);
        }

        public int AddRangeDuToanNguonVon(IEnumerable<VdtDaDuToanNguonvon> entities)
        {
            return _vdtDaDuToanNguonVonRepository.AddRange(entities);
        }

        public bool CheckDuplicateSoQD(string soQuyetDinh, Guid id)
        {
            return _vdtDaDuToanRepository.CheckDuplicateSoQD(soQuyetDinh, id);
        }

        public int DeleteDuToanChiPhi(Guid id)
        {
            return _vdtDaDuToanChiPhiRepository.Delete(id);
        }

        public void DeleteDuToanChiTiet(Guid id)
        {
            _vdtDaDuToanRepository.DeleteDuToanChiTiet(id);
        }

        public int DeleteDuToanNguonVon(Guid id)
        {
            return _vdtDaDuToanNguonVonRepository.Delete(id);
        }

        public IEnumerable<VdtDaDuToanQuery> FindByCondition(int namLamViec)
        {
            return _vdtDaDuToanRepository.FindByCondition(namLamViec);
        }

        public VdtDaDuToan FindById(Guid id)
        {
            return _vdtDaDuToanRepository.Find(id);
        }

        public IEnumerable<VdtDaDuAn> FindDuAnByDonViAndLoaiQD(string donviQLId, string loaiQD)
        {
            return _vdtDaDuToanRepository.FindDuAnByDonViAndLoaiQD(donviQLId, loaiQD);
        }

        public VdtDaDuToanChiPhi FindDuToanChiPhi(params object[] keyValues)
        {
            return _vdtDaDuToanChiPhiRepository.Find(keyValues);
        }

        public VdtDaDuToanNguonvon FindDuToanNguonVon(params object[] keyValues)
        {
            return _vdtDaDuToanNguonVonRepository.Find(keyValues);
        }

        public IEnumerable<DuToanDetailQuery> FindListDetail(Guid duToanId, Guid? duAnChiPhiId)
        {
            return _vdtDaDuToanRepository.FindListDetail(duToanId, duAnChiPhiId);
        }

        public IEnumerable<DuToanDetailQuery> FindListDetail(string duToanId, Guid? duAnChiPhiId)
        {
            return _vdtDaDuToanRepository.FindListDetail(duToanId, duAnChiPhiId);
        }

        public IEnumerable<VdtDaDuToanChiPhiQuery> FindListDuToanChiPhiByDuAn(Guid duAnId)
        {
            return _vdtDaDuToanRepository.FindListDuToanChiPhiByDuAn(duAnId);
        }

        public IEnumerable<VdtDaDuToanChiPhiQuery> FindListDuToanChiPhiByDuToanId(Guid duToanId)
        {
            return _vdtDaDuToanRepository.FindListDuToanChiPhiByDuToanId(duToanId);
        }

        public IEnumerable<VdtDaDuToanNguonVonQuery> FindListDuToanNguonVonByDuAn(Guid duAnId)
        {
            return _vdtDaDuToanRepository.FindListDuToanNguonVonByDuAn(duAnId);
        }

        public IEnumerable<VdtDaDuToanNguonVonQuery> FindListDuToanNguonVonByDuToanId(Guid duToanId)
        {
            return _vdtDaDuToanRepository.FindListDuToanNguonVonByDuToanId(duToanId);
        }

        public IEnumerable<VdtDmChiPhi> GetListChiPhi(Guid duToanId)
        {
            return _vdtDaDuToanRepository.GetListQDChiPhi(duToanId);
        }

        public int Update(VdtDaDuToan entity)
        {
            return _vdtDaDuToanRepository.Update(entity);
        }

        public int UpdateDuToanChiPhi(VdtDaDuToanChiPhi entity)
        {
            return _vdtDaDuToanChiPhiRepository.Update(entity);
        }

        public int UpdateDuToanNguonVon(VdtDaDuToanNguonvon entity)
        {
            return _vdtDaDuToanNguonVonRepository.Update(entity);
        }

        public VdtDaDuToanHangMuc FindDuToanHangMuc(Guid duToanHangMucId)
        {
            return _vdtDaDuToanHangMucRepository.Find(duToanHangMucId);
        }

        public int UpdateDuToanHangMuc(VdtDaDuToanHangMuc entity)
        {
            return _vdtDaDuToanHangMucRepository.Update(entity);
        }

        public IEnumerable<ChiPhiHangMucQuery> GetDetailByDuAnId(Guid iIdDuAn, Guid? iIdChiPhi)
        {
            return _vdtDaDuToanRepository.GetDetailByDuAnId(iIdDuAn, iIdChiPhi);
        }

        public IEnumerable<ChiPhiHangMucQuery> GetDetailByDuToanId(Guid iIdDuToan, Guid? iIdChiPhi)
        {
            return _vdtDaDuToanRepository.GetDetailByDuToanId(iIdDuToan, iIdChiPhi);
        }

        public void InsertDuToanChiPhi(IEnumerable<VdtDaDuToanChiPhi> datas)
        {
            _vdtDaDuToanRepository.InsertDuToanChiPhi(datas);
        }

        public void UpdateDuToanChiPhi(Guid iIdDuToan, IEnumerable<VdtDaDuToanChiPhi> datas)
        {
            _vdtDaDuToanRepository.UpdateDuToanChiPhi(iIdDuToan, datas);
        }

        public void DeleteDuToanChiPhi(Guid iIdDuToan, IEnumerable<VdtDaDuToanChiPhi> datas)
        {
            _vdtDaDuToanRepository.DeleteDuToanChiPhi(iIdDuToan, datas);
        }

        public void InsertDuToanNguonVon(IEnumerable<VdtDaDuToanNguonvon> datas)
        {
            _vdtDaDuToanRepository.InsertDuToanNguonVon(datas);
        }

        public void UpdateDuToanNguonVon(Guid iIdDuToan, IEnumerable<VdtDaDuToanNguonvon> datas)
        {
            _vdtDaDuToanRepository.UpdateDuToanNguonVon(iIdDuToan, datas);
        }

        public void DeleteDuToanNguonVon(Guid iIdDuToan, IEnumerable<VdtDaDuToanNguonvon> datas)
        {
            _vdtDaDuToanRepository.DeleteDuToanNguonVon(iIdDuToan, datas);
        }

        public void InsertDmDuToanHangMuc(IEnumerable<VdtDaDuToanDmHangMuc> datas)
        {
            _vdtDaDuToanRepository.InsertDmDuToanHangMuc(datas);
        }

        public void UpdateDmDuToanHangMuc(IEnumerable<VdtDaDuToanDmHangMuc> datas)
        {
            _vdtDaDuToanRepository.UpdateDmDuToanHangMuc(datas);
        }

        public void DeleteDmDuToanHangMuc(IEnumerable<VdtDaDuToanDmHangMuc> datas)
        {
            _vdtDaDuToanRepository.DeleteDmDuToanHangMuc(datas);
        }

        public void InsertDuToanHangMuc(IEnumerable<VdtDaDuToanHangMuc> datas)
        {
            _vdtDaDuToanRepository.InsertDuToanHangMuc(datas);
        }

        public void UpdateDuToanHangMuc(Guid iIdDuToan, IEnumerable<VdtDaDuToanHangMuc> datas)
        {
            _vdtDaDuToanRepository.UpdateDuToanHangMuc(iIdDuToan, datas);
        }

        public void DeleteDuToanHangMuc(Guid iIdDuToan, IEnumerable<VdtDaDuToanHangMuc> datas)
        {
            _vdtDaDuToanRepository.DeleteDuToanHangMuc(iIdDuToan, datas);
        }

        public bool CheckExistInDuToanHangMuc(Guid duToanId, Guid danhMucChiPhiDuAnId)
        {
            return _vdtDaDuToanRepository.CheckExistInDuToanHangMuc(duToanId, danhMucChiPhiDuAnId);
        }

        public IEnumerable<DuToanDetailQuery> ListHangMucInitial(Guid qdDauTuId, Guid danhMucDuAnChiPhiId)
        {
            return _vdtDaDuToanRepository.ListHangMucInitial(qdDauTuId, danhMucDuAnChiPhiId);
        }

        public VdtDaDuToanDmHangMuc FindDuToanDMHangMuc(Guid hangMucId)
        {
            return _vdtDaDmDuToanHangMucRepository.Find(hangMucId);
        }

        public int UpdateDuToanDanhMucHangMuc(VdtDaDuToanDmHangMuc entity)
        {
            return _vdtDaDmDuToanHangMucRepository.Update(entity);
        }

        public int AddDuToanDanhMucHangMuc(VdtDaDuToanDmHangMuc entity)
        {
            return _vdtDaDmDuToanHangMucRepository.Add(entity);
        }

        public int DeleteDuToanHangMucDetail(Guid id)
        {
            return _vdtDaDuToanHangMucRepository.Delete(id);
        }

        public IEnumerable<VdtDaDuToanQuery> GetDuToanByDuAnId(Guid duToanId)
        {
            return _vdtDaDuToanRepository.GetDuToanByDuAnId(duToanId);
        }

        public IEnumerable<VdtDaDuToanQuery> GetDuToanByDuAnIdAndActive(Guid iIdDuAn, int bActive)
        {
            return _vdtDaDuToanRepository.GetDuToanByDuAnIdAndActive(iIdDuAn, bActive);
        }

        public IEnumerable<VdtDaDuToanQuery> GetDuToanByKHLCNhaThauId(Guid duToanId)
        {
            return _vdtDaDuToanRepository.GetDuToanByKHLCNhaThauId(duToanId);
        }

        public IEnumerable<DuToanDetailQuery> FindListHangMucAllDetail(Guid duToanId)
        {
            return _vdtDaDuToanRepository.FindListHangMucAllDetail(duToanId);
        }

        public IEnumerable<VdtDaDuToanNguonVonQuery> FindListDuToanNguonVonDieuChinhAdd(Guid duToanId)
        {
            return _vdtDaDuToanRepository.FindListDuToanNguonVonDieuChinhAdd(duToanId);
        }

        public IEnumerable<VdtDaDuToanChiPhiQuery> FindListDuToanChiPhiDieuChinhAdd(Guid duToanId)
        {
            return _vdtDaDuToanRepository.FindListDuToanChiPhiDieuChinhAdd(duToanId);
        }

        public IEnumerable<VdtDaDuToanNguonVonQuery> FindListDuToanNguonVonDieuChinhUpdate(Guid duToanId)
        {
            return _vdtDaDuToanRepository.FindListDuToanNguonVonDieuChinhUpdate(duToanId);
        }

        public IEnumerable<VdtDaDuToanChiPhiQuery> FindListDuToanChiPhiDieuChinhUpdate(Guid duToanId)
        {
            return _vdtDaDuToanRepository.FindListDuToanChiPhiDieuChinhUpdate(duToanId);
        }

        public IEnumerable<DuToanDetailQuery> FindListHangMucDieuChinhAdd(Guid duToanId, Guid? duAnChiPhiId)
        {
            return _vdtDaDuToanRepository.FindListHangMucDieuChinhAdd(duToanId, duAnChiPhiId);
        }

        public IEnumerable<DuToanDetailQuery> FindListHangMucDieuChinhUpdate(Guid duToanId, Guid? duAnChiPhiId)
        {
            return _vdtDaDuToanRepository.FindListHangMucDieuChinhUpdate(duToanId, duAnChiPhiId);
        }

        public bool CheckExistInDuToanHangMuc(string listDuToanId, Guid danhMucChiPhiDuAnId)
        {
            return _vdtDaDuToanRepository.CheckExistInDuToanHangMuc(listDuToanId, danhMucChiPhiDuAnId);
        }

        public VdtDaDuToan FindDuToanByDuToanGocId(Guid id, Guid duToanId)
        {
            return _vdtDaDuToanRepository.FindDuToanByDuToanGocId(id, duToanId);
        }

        public string GetDuToanIdByDuAnId(Guid duAnId)
        {
            return _vdtDaDuToanRepository.GetDuToanIdByDuAnId(duAnId);
        }

        public double GetGiaTriDuToanIdByDuAnId(Guid duAnId)
        {
            return _vdtDaDuToanRepository.GetGiaTriDuToanIdByDuAnId(duAnId);
        }

        public VdtDaDuToanDmHangMuc FindDaDuToanHangMucByMa(string ma)
        {
            return _vdtDaDmDuToanHangMucRepository.FindByMa(ma);
        }

        public IEnumerable<DuToanDetailQuery> ListHangMucByQDDauTu(Guid qdDauTuId)
        {
            return _vdtDaDuToanRepository.ListHangMucByQDDauTu(qdDauTuId);
        }

        public IEnumerable<DuToanDetailQuery> ListHangMucByDuToan(Guid duToanId)
        {
            return _vdtDaDuToanRepository.ListHangMucByDuToan(duToanId);
        }

        public bool CheckQDDTExistTKTCTDT(Guid qdDtId)
        {
            return _vdtDaDuToanRepository.CheckQDDTExistTKTCTDT(qdDtId);
        }

        public IEnumerable<VdtDaDuToanQuery> GetQDDauTuByDuAnIdAndNgayLap(Guid iIdDuAn, DateTime ngayLap)
        {
            return _vdtDaDuToanRepository.GetQDDauTuByDuAnIdAndNgayLap(iIdDuAn,ngayLap);
        }

        public IEnumerable<ChiPhiHangMucQuery> GetDetailByQDDauTuId(Guid iIdQDDauTu, Guid? iIdChiPhi)
        {
            return _vdtDaDuToanRepository.GetDetailByQDDauTuId(iIdQDDauTu, iIdChiPhi);
        }

        public IEnumerable<ChiPhiHangMucQuery> GetDetailByChuTruongDauTuId(Guid iIdChuTruongDauTu)
        {
            return _vdtDaDuToanRepository.GetDetailByChuTruongDauTuId(iIdChuTruongDauTu);
        }

        public IEnumerable<VdtDaDuToanQuery> GetQDDauTuByKHLCNhaThauId(Guid qdDauTuId)
        {
            return _vdtDaDuToanRepository.GetQDDauTuByKHLCNhaThauId(qdDauTuId);
        }

        public void LockOrUnlock(Guid id, bool isLock)
        {
            VdtDaDuToan chungTu = _vdtDaDuToanRepository.Find(id);
            chungTu.BKhoa = isLock;
            _vdtDaDuToanRepository.Update(chungTu);
        }

        public IEnumerable<VdtDaDuToan> FindByCondition(Expression<Func<VdtDaDuToan, bool>> predicate)
        {
            return _vdtDaDuToanRepository.FindAll(predicate);
        }

        public IEnumerable<VdtDaDuToanNguonvon> FindDuToanNguonVonByCondition(Expression<Func<VdtDaDuToanNguonvon, bool>> predicate)
        {
            return _vdtDaDuToanNguonVonRepository.FindAll(predicate);
        }

        public bool checkExistLoaiQuyetDinh(bool bLaTongDuToan, Guid? idDuAnId)
        {
            return _vdtDaDuToanRepository.checkExistLoaiQuyetDinh(bLaTongDuToan, idDuAnId);
        }

        public double TinhTongPheDuyetDuAn(Guid? iIdDuAnId, Guid? idDuToanId)
        {
            return _vdtDaDuToanRepository.TinhTongPheDuyetDuAn(iIdDuAnId, idDuToanId);
        }
    }
}
