using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using static Dapper.SqlMapper;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtDaGoiThauService : IVdtDaGoiThauService
    {
        private readonly IVdtDaGoiThauRepository _vdtDaGoiThauRepository;
        private readonly IVdtDaGoiThauChiPhiRepository _vdtDaGoiThauChiPhiRepository;
        private readonly IVdtDaGoiThauNguonVonRepository _vdtDaGoiThauNguonVonRepository;
        private readonly IVdtDaGoiThauHangMucRepository _vdtDaGoiThauHangMucRepository;

        public VdtDaGoiThauService(
            IVdtDaGoiThauRepository vdtDaGoiThauRepository,
            IVdtDaGoiThauChiPhiRepository vdtDaGoiThauChiPhiRepository,
            IVdtDaGoiThauNguonVonRepository vdtDaGoiThauNguonVonRepository,
            IVdtDaGoiThauHangMucRepository vdtDaGoiThauHangMucRepository)
        {
            _vdtDaGoiThauRepository = vdtDaGoiThauRepository;
            _vdtDaGoiThauChiPhiRepository = vdtDaGoiThauChiPhiRepository;
            _vdtDaGoiThauNguonVonRepository = vdtDaGoiThauNguonVonRepository;
            _vdtDaGoiThauHangMucRepository = vdtDaGoiThauHangMucRepository;
        }

        public VdtDaGoiThau Add(VdtDaGoiThau entity)
        {
            _vdtDaGoiThauRepository.Add(entity);
            return entity;
        }

        public void AddRange(IEnumerable<VdtDaGoiThau> entities)
        {
            _vdtDaGoiThauRepository.AddRange(entities);
        }

        public VdtDaGoiThau UpdateData(VdtDaGoiThau item)
        {
            var dataUpdate = _vdtDaGoiThauRepository.Find(item.Id);
            if (dataUpdate == null) return item;
            dataUpdate.STenGoiThau = item.STenGoiThau;
            dataUpdate.SHinhThucChonNhaThau = item.SHinhThucChonNhaThau;
            dataUpdate.SPhuongThucDauThau = item.SPhuongThucDauThau;
            dataUpdate.SHinhThucHopDong = item.SHinhThucHopDong;
            dataUpdate.SThoiGianThucHien = item.SThoiGianThucHien;
            dataUpdate.FTienTrungThau = item.FTienTrungThau;
            dataUpdate.SUserUpdate = item.SUserUpdate;
            dataUpdate.DDateUpdate = item.DDateUpdate;
            _vdtDaGoiThauRepository.Update(dataUpdate);
            return dataUpdate;
        }

        public void UpdateRange(IEnumerable<VdtDaGoiThau> items, bool bIsActive = true)
        {
            if (bIsActive)
            {
                List<VdtDaGoiThau> lstData = new List<VdtDaGoiThau>();
                foreach (var item in items)
                {
                    var dataUpdate = _vdtDaGoiThauRepository.Find(item.Id);
                    if (dataUpdate == null) continue;
                    dataUpdate.STenGoiThau = item.STenGoiThau;
                    dataUpdate.SHinhThucChonNhaThau = item.SHinhThucChonNhaThau;
                    dataUpdate.IIdKhlcnhaThau = item.IIdKhlcnhaThau;
                    dataUpdate.SPhuongThucDauThau = item.SPhuongThucDauThau;
                    dataUpdate.DBatDauChonNhaThau = item.DBatDauChonNhaThau;
                    dataUpdate.SHinhThucHopDong = item.SHinhThucHopDong;
                    dataUpdate.SThoiGianThucHien = item.SThoiGianThucHien;
                    dataUpdate.FTienTrungThau = item.FTienTrungThau;
                    dataUpdate.SUserUpdate = item.SUserUpdate;
                    dataUpdate.DDateUpdate = item.DDateUpdate;
                    lstData.Add(dataUpdate);
                }
                _vdtDaGoiThauRepository.UpdateRange(lstData);
                var lstId = lstData.Select(n => n.Id).ToList();
                if (lstId != null)
                    _vdtDaGoiThauRepository.DeleteGoiThauDetail(lstId);
            }
            else
            {
                items = items.Select(n => { n.BActive = false; return n; });
                _vdtDaGoiThauRepository.UpdateRange(items);
            }
        }



        public void DeleteRange(IEnumerable<VdtDaGoiThau> items)
        {
            List<Guid> lstId = items.Select(n => n.Id).ToList();
            if (lstId != null)
                _vdtDaGoiThauRepository.DeleteListGoiThau(lstId);
        }

        public int AddRangeGoiThauChiPhi(IEnumerable<VdtDaGoiThauChiPhi> entities)
        {
            return _vdtDaGoiThauChiPhiRepository.AddRange(entities);
        }

        public int AddRangeGoiThauHangMuc(IEnumerable<VdtDaGoiThauHangMuc> entities)
        {
            return _vdtDaGoiThauHangMucRepository.AddRange(entities);
        }

        public int AddRangeGoiThauNguonVon(IEnumerable<VdtDaGoiThauNguonVon> entities)
        {
            return _vdtDaGoiThauNguonVonRepository.AddRange(entities);
        }

        public int DeleteGoiThauChiPhi(Guid id)
        {
            return _vdtDaGoiThauChiPhiRepository.Delete(id);
        }

        public void DeleteGoiThauChiTiet(Guid id)
        {
            _vdtDaGoiThauRepository.DeleteGoiThauChiTiet(id);
        }

        public int DeleteGoiThauHangMuc(Guid id)
        {
            return _vdtDaGoiThauHangMucRepository.Delete(id);
        }

        public int DeleteGoiThauNguonVon(Guid id)
        {
            return _vdtDaGoiThauNguonVonRepository.Delete(id);
        }

        public IEnumerable<VdtDaGoiThauQuery> FindByCondition(int namLamViec)
        {
            return _vdtDaGoiThauRepository.FindByCondition(namLamViec);
        }

        public IEnumerable<VdtDaGoiThau> FindAll(Expression<Func<VdtDaGoiThau, bool>> predicate)
        {
            return _vdtDaGoiThauRepository.FindAll(predicate);
        }

        public VdtDaGoiThau FindById(Guid id)
        {
            return _vdtDaGoiThauRepository.Find(id);
        }

        public IEnumerable<VdtDaDuAn> FindDuAnByDonViGoiThau(string donviUserId, int namLamViec)
        {
            return _vdtDaGoiThauRepository.FindDuAnByDonViGoiThau(donviUserId, namLamViec);
        }

        public VdtDaGoiThauChiPhi FindGoiThauChiPhi(params object[] keyValues)
        {
            return _vdtDaGoiThauChiPhiRepository.Find(keyValues);
        }

        public VdtDaGoiThauHangMuc FindGoiThauHangMuc(params object[] keyValues)
        {
            return _vdtDaGoiThauHangMucRepository.Find(keyValues);
        }

        public VdtDaGoiThauNguonVon FindGoiThauNguonVon(params object[] keyValues)
        {
            return _vdtDaGoiThauNguonVonRepository.Find(keyValues);
        }

        public IEnumerable<VdtDaGoiThauDetailQuery> FindListDetail(Guid goiThauId)
        {
            return _vdtDaGoiThauRepository.FindListDetail(goiThauId);
        }

        public IEnumerable<VdtDaGoiThauDetailQuery> FindListDieuChinhDetail(Guid goiThauId, Guid goiThauGocId, DateTime dngayLap)
        {
            return _vdtDaGoiThauRepository.FindListDieuChinhDetail(goiThauId, goiThauGocId, dngayLap);
        }

        public IEnumerable<VdtDmNhaThau> GetAllNhaThau()
        {
            return _vdtDaGoiThauRepository.GetAllNhaThau();
        }

        public IEnumerable<VdtDmChiPhi> GetListChiPhiByDuAn(Guid idDuAn, DateTime ngayLap)
        {
            return _vdtDaGoiThauRepository.GetListChiPhiByDuAn(idDuAn, ngayLap);
        }

        public IEnumerable<VdtDaDuAnHangMuc> GetListHangMucByDuAn(Guid idDuAn, DateTime ngayLap)
        {
            return _vdtDaGoiThauRepository.GetListHangMucByDuAn(idDuAn, ngayLap);
        }

        public IEnumerable<NsNguonNganSach> GetListNguonVonByDuAn(Guid idDuAn, DateTime ngayLap)
        {
            return _vdtDaGoiThauRepository.GetListNguonVonByDuAn(idDuAn, ngayLap);
        }

        public double? GetTongMucDTChiPhi(Guid idChiPhi, Guid idDuAn, DateTime dNgayLap)
        {
            return _vdtDaGoiThauRepository.GetTongMucDTChiPhi(idChiPhi, idDuAn, dNgayLap);
        }

        public double? GetTongMucDTHangMuc(Guid idHangMuc, Guid idDuAn, DateTime dNgayLap)
        {
            return _vdtDaGoiThauRepository.GetTongMucDTHangMuc(idHangMuc, idDuAn, dNgayLap);
        }

        public double? GetTongMucDTNguonVon(int idNguonVon, Guid idDuAn, DateTime dNgayLap)
        {
            return _vdtDaGoiThauRepository.GetTongMucDTNguonVon(idNguonVon, idDuAn, dNgayLap);
        }

        public int Update(VdtDaGoiThau entity)
        {
            return _vdtDaGoiThauRepository.Update(entity);
        }

        public int UpdateGoiThauChiPhi(VdtDaGoiThauChiPhi entity)
        {
            return _vdtDaGoiThauChiPhiRepository.Update(entity);
        }

        public int UpdateGoiThauHangMuc(VdtDaGoiThauHangMuc entity)
        {
            return _vdtDaGoiThauHangMucRepository.Update(entity);
        }

        public int UpdateGoiThauNguonVon(VdtDaGoiThauNguonVon entity)
        {
            return _vdtDaGoiThauNguonVonRepository.Update(entity);
        }

        public IEnumerable<GoiThauChiPhiQuery> FindListGoiThauChiPhi(Guid goiThauId)
        {
            return _vdtDaGoiThauRepository.FindListGoiThauChiPhi(goiThauId);
        }

        public IEnumerable<GoiThauNguonVonQuery> FindListGoiThauNguonVon(Guid goiThauId)
        {
            return _vdtDaGoiThauRepository.FindListGoiThauNguonVon(goiThauId);
        }

        public IEnumerable<GoiThauHangMucQuery> FindListGoiThauHangMuc(Guid goiThauId, Guid chiPhiDuAnId, bool isDuToan)
        {
            return _vdtDaGoiThauRepository.FindListGoiThauHangMuc(goiThauId, chiPhiDuAnId, isDuToan);
        }

        public void DeleteGoiThauNguonVonByGoiThauId(Guid goiThauId)
        {
            _vdtDaGoiThauRepository.DeleteGoiThauNguonVon(goiThauId);
        }

        public void DeleteGoiThauChiPhiByGoiThauId(Guid goiThauId)
        {
            _vdtDaGoiThauRepository.DeleteGoiThauChiPhi(goiThauId);
        }

        public VdtDaGoiThau FindGoiThauDieuChinhByGoiThauGocId(Guid goiThauGocId)
        {
            return _vdtDaGoiThauRepository.FindGoiThauDieuChinhByGoiThauGocId(goiThauGocId);
        }

        public IEnumerable<NhaThauHopDongQuery> FindListNhaThauHopDongByGoiThau(Guid goiThauId)
        {
            return _vdtDaGoiThauRepository.FindListNhaThauHopDongByGoiThau(goiThauId);
        }

        public void LockOrUnlock(Guid id, bool isLock)
        {
            VdtDaGoiThau chungTu = _vdtDaGoiThauRepository.Find(id);
            chungTu.BKhoa = isLock;
            _vdtDaGoiThauRepository.Update(chungTu);
        }

        public IEnumerable<VdtDaGoiThauQuery> FindByKhlcNhaThauId(Guid iIdKhlcNhaThauId)
        {
            return _vdtDaGoiThauRepository.FindByKhlcNhaThauId(iIdKhlcNhaThauId);
        }

        public IEnumerable<VdtKhlcNhaThauGoiThauDetailQuery> GetGoiThauNguonVonByChungTu(Guid iIdChungTu, string sLoaiChungTu)
        {
            return _vdtDaGoiThauRepository.GetGoiThauNguonVonByChungTu(iIdChungTu, sLoaiChungTu);
        }

        public IEnumerable<VdtKhlcNhaThauGoiThauDetailQuery> GetGoiThauChiPhiByChungTu(Guid iIdChungTu, string sLoaiChungTu, bool bIsAdd)
        {
            return _vdtDaGoiThauRepository.GetGoiThauChiPhiByChungTu(iIdChungTu, sLoaiChungTu, bIsAdd);
        }

        public IEnumerable<VdtKhlcNhaThauGoiThauDetailQuery> GetGoiThauChiPhiByChungTuCtdt_KhlcntEdit(Guid iIdKhlcnt)
        {
            return _vdtDaGoiThauRepository.GetGoiThauChiPhiByChungTuCtdt_KhlcntEdit(iIdKhlcnt);
        }

        public IEnumerable<VdtKhlcNhaThauGoiThauDetailQuery> GetGoiThauHangMucByChungTu(Guid iIdChungTu, string sLoaiChungTu)
        {
            return _vdtDaGoiThauRepository.GetGoiThauHangMucByChungTu(iIdChungTu, sLoaiChungTu);
        }

        public void DeleteGoiThauDetail(List<Guid> iIdGoiThaus)
        {
            _vdtDaGoiThauRepository.DeleteGoiThauDetail(iIdGoiThaus);
        }

        public void DeleteListGoiThau(List<Guid> iIdGoiThaus)
        {
            _vdtDaGoiThauRepository.DeleteListGoiThau(iIdGoiThaus);
        }

        public IEnumerable<VdtKhlcNhaThauGoiThauDetailQuery> GetGoiThauNguonVonByKhlcNhaThauId(Guid iIdKhlcNhaThau)
        {
            return _vdtDaGoiThauRepository.GetGoiThauNguonVonByKhlcNhaThauId(iIdKhlcNhaThau);
        }

        public IEnumerable<VdtKhlcNhaThauGoiThauDetailQuery> GetGoiThauChiPhiByKhlcNhaThauId(Guid iIdKhlcNhaThau)
        {
            return _vdtDaGoiThauRepository.GetGoiThauChiPhiByKhlcNhaThauId(iIdKhlcNhaThau);
        }

        public IEnumerable<VdtKhlcNhaThauGoiThauDetailQuery> GetGoiThauHangMucByKhlcNhaThauId(Guid iIdKhlcNhaThau)
        {
            return _vdtDaGoiThauRepository.GetGoiThauHangMucByKhlcNhaThauId(iIdKhlcNhaThau);
        }

        public IEnumerable<VdtKhlcntChiPhiNguonVonCanCuQuery> GetAllNguonVonByLoaiCanCuInKhlcntScreen(List<Guid> iIdCanCuIds, string sLoaiCanCu)
        {
            return _vdtDaGoiThauRepository.GetAllNguonVonByLoaiCanCuInKhlcntScreen(iIdCanCuIds, sLoaiCanCu);
        }

        public IEnumerable<VdtKhlcntChiPhiNguonVonCanCuQuery> GetAllChiPhiByLoaiCanCuInKhlcntScreen(List<Guid> iIdCanCuIds, string sLoaiCanCu)
        {
            return _vdtDaGoiThauRepository.GetAllChiPhiByLoaiCanCuInKhlcntScreen(iIdCanCuIds, sLoaiCanCu);
        }

        public void ReActiveGoiThauByKhlcntId(Guid iIdKhlcnt)
        {
            _vdtDaGoiThauRepository.ReActiveGoiThauByKhlcntId(iIdKhlcnt);
        }

        public IEnumerable<HopDongGoiThauQuery> FindGoiThauByDuAn(Guid duanId)
        {
            return _vdtDaGoiThauRepository.FindGoiThauByDuAn(duanId);
        }

        public IEnumerable<HopDongGoiThauQuery> FindGoiThauByHopDong(Guid hopdongId)
        {
            return _vdtDaGoiThauRepository.FindGoiThauByHopDong(hopdongId);
        }

        public IEnumerable<HopDongGoiThauQuery> DCFindGoiThauByHopDong(Guid hopDongGocId, Guid? hopdongDCId)
        {
            return _vdtDaGoiThauRepository.DCFindGoiThauByHopDong(hopDongGocId, hopdongDCId);
        }

        public string GetTypeOfGoiThau(Guid goithauId)
        {
            return _vdtDaGoiThauRepository.GetTypeOfGoiThau(goithauId);
        }
    }
}
