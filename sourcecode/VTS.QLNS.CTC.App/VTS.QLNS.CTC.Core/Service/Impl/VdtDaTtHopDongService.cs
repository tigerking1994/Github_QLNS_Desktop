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
    public class VdtDaTtHopDongService : IVdtDaTtHopDongService
    {
        private readonly IVdtDaTtHopDongRepository _ttHopDongRepository;
        private readonly IVdtDaHopDongGoiThauHangMucRepository _hopDongGoiThauHangMucRepository;
        private readonly IVdtDaHopDongGoiThauNhaThauRepository _hopDongGoiThauNhaThauRepository;
        private readonly IVdtDaHopDongGoiThauChiPhiRepository _hopDongGoiThauChiPhiRepository;
        private readonly IDMHopDongHangMucRepository _dMHopDongHangMucRepository;

        public VdtDaTtHopDongService(
            IVdtDaTtHopDongRepository ttHopDongRepository,
            IVdtDaHopDongGoiThauHangMucRepository hopDongGoiThauHangMucRepository,
            IVdtDaHopDongGoiThauNhaThauRepository hopDongGoiThauNhaThauRepository,
            IDMHopDongHangMucRepository dMHopDongHangMucRepository,
            IVdtDaHopDongGoiThauChiPhiRepository hopDongGoiThauChiPhiRepository)
        {
            _ttHopDongRepository = ttHopDongRepository;
            _hopDongGoiThauHangMucRepository = hopDongGoiThauHangMucRepository;
            _hopDongGoiThauNhaThauRepository = hopDongGoiThauNhaThauRepository;
            _hopDongGoiThauChiPhiRepository = hopDongGoiThauChiPhiRepository;
            _dMHopDongHangMucRepository = dMHopDongHangMucRepository;
        }

        public int Add(VdtDaTtHopDong entity)
        {
            return _ttHopDongRepository.Add(entity);
        }

        public void AddRange(IEnumerable<VdtDaTtHopDong> entity)
        {
            _ttHopDongRepository.AddRange(entity);
        }

        public int Delete(Guid id)
        {
            VdtDaTtHopDong item = Find(id);
            if (item.BActive.HasValue && item.BActive.Value && item.BIsGoc.HasValue && !item.BIsGoc.Value && item.IIdHopDongGocId.HasValue && item.IIdHopDongGocId.Value != Guid.Empty)
            {
                VdtDaTtHopDong itemParent = Find(item.IIdHopDongGocId);
                if (itemParent != null)
                {
                    itemParent.BActive = true;
                    Update(itemParent);
                }
            }
            //delete Data các bảng liên quan
            DeleteHopDongDetail(id);
            return _ttHopDongRepository.Delete(id);
        }

        public void DeleteHopDong(Guid id)
        {
            //delete Data các bảng liên quan
            DeleteHopDongDetail(id);
            _ttHopDongRepository.Delete(id);
        }

        public VdtDaTtHopDong Find(params object[] keyValues)
        {
            return _ttHopDongRepository.Find(keyValues);
        }

        public IEnumerable<VdtDaTtHopDong> FindAll(Expression<Func<VdtDaTtHopDong, bool>> predicate)
        {
            return _ttHopDongRepository.FindAll(predicate);
        }

        public IEnumerable<HopDongQuery> FindAllHopDongByNamLamViec(int namLamViec)
        {
            return _ttHopDongRepository.FindAllHopDongByNamLamViec(namLamViec);
        }

        public List<VdtDaTtHopDong> FindByListDuAnId(List<Guid> iIdDuAnIds)
        {
            return _ttHopDongRepository.FindByListDuAnId(iIdDuAnIds);
        }

        public int Update(VdtDaTtHopDong entity)
        {
            return _ttHopDongRepository.Update(entity);
        }

        public bool CheckExistHopDongByGoiThai(Guid iIdGoiThau)
        {
            return _ttHopDongRepository.CheckExistHopDongByGoiThai(iIdGoiThau);
        }

        public IEnumerable<HopDongHangMucQuery> GetPhuLucHangMucByGoiThau(Guid iIdGoiThauId, Guid iIdHopDongId)
        {
            return _hopDongGoiThauHangMucRepository.GetPhuLucHangMucByGoiThau(iIdGoiThauId, iIdHopDongId);
        }

        public IEnumerable<HopDongHangMucQuery> GetPhuLucChiPhiByGoiThau(Guid iIdGoiThauId, Guid iIdHopDongId)
        {
            return _hopDongGoiThauHangMucRepository.GetPhuLucChiPhiByGoiThau(iIdGoiThauId, iIdHopDongId);
        }

        public IEnumerable<VdtDmLoaiHopDong> GetAllLoaiHopDong()
        {
            return _hopDongGoiThauHangMucRepository.GetAllLoaiHopDong();
        }

        public void DeleteHopDongDetail(Guid iIdHopDongId)
        {
            _hopDongGoiThauNhaThauRepository.DeleteHopDongDetail(iIdHopDongId);
        }

        public void InsertHopDongGoiThauNhaThau(List<VdtDaHopDongGoiThauNhaThau> lstData)
        {
            _hopDongGoiThauNhaThauRepository.AddRange(lstData);
        }

        public void InsertHopDongGoiThauHangMuc(List<VdtDaHopDongGoiThauHangMuc> lstData)
        {
            _hopDongGoiThauHangMucRepository.AddRange(lstData);
        }

        public void InsertHopDongDMHangMuc(List<VdtDaHopDongDmHangMuc> lstData)
        {
            _dMHopDongHangMucRepository.AddRange(lstData);
        }

        public void DeleteHopDongDanhMucHangMuc(List<Guid> guids)
        {
            IEnumerable<VdtDaHopDongDmHangMuc> entities = _dMHopDongHangMucRepository.FindAll(t => guids.Contains(t.IIDHopDongGoiThauNhaThauID));
            _dMHopDongHangMucRepository.RemoveRange(entities);
        }

        public IEnumerable<HopDongHangMucQuery> GetAllHangMucByGoiThau(Guid iIdGoiThauId)
        {
            return _hopDongGoiThauHangMucRepository.GetAllHangMucByGoiThau(iIdGoiThauId);
        }

        public IEnumerable<HopDongHangMucQuery> GetGoiThauChiPhiByHopDong(Guid iIdHopDongId)
        {
            return _hopDongGoiThauHangMucRepository.GetGoiThauChiPhiByHopDong(iIdHopDongId);
        }

        public IEnumerable<HopDongHangMucQuery> GetAllHangMucByHopDong(Guid iIdHopDongId)
        {
            return _hopDongGoiThauHangMucRepository.GetAllHangMucByHopDong(iIdHopDongId);
        }

        public IEnumerable<HopDongHangMucQuery> GetAllHangMucByListGoiThau(List<Guid> listGoiThauId)
        {
            return _hopDongGoiThauHangMucRepository.GetAllHangMucByListGoiThau(listGoiThauId);
        }

        public void DeleteHopDongGoiThauNhaThau(List<Guid> listGoiThauNhaThauId)
        {
            _hopDongGoiThauNhaThauRepository.DeleteHopDongGoiThauNhaThau(listGoiThauNhaThauId);
        }

        public void InsertHopDongGoiThauChiPhi(List<VdtDaHopDongGoiThauChiPhi> lstData)
        {
            _hopDongGoiThauChiPhiRepository.AddRange(lstData);
        }

        public IEnumerable<VdtDaHopDongGoiThauNhaThau> ListGoiThauNhaThauByGoiThauId(Guid goiThauId)
        {
            return _hopDongGoiThauNhaThauRepository.ListGoiThauNhaThauByGoiThauId(goiThauId);
        }

        public IEnumerable<HopDongHangMucQuery> GetAllHangMucByListGoiThauHopDongAdd(Guid iIdHopDongId, List<Guid> listGoiThauId)
        {
            return _hopDongGoiThauHangMucRepository.GetAllHangMucByListGoiThauHopDongAdd(iIdHopDongId, listGoiThauId);
        }

        public void LockOrUnlock(Guid id, bool isLock)
        {
            VdtDaTtHopDong chungTu = _ttHopDongRepository.Find(id);
            chungTu.BKhoa = isLock;
            _ttHopDongRepository.Update(chungTu);
        }

        public IEnumerable<HopDongHangMucQuery> GetAllHangMucByHopDongDieuChinh(Guid iIdHopDongId)
        {
            return _hopDongGoiThauHangMucRepository.GetAllHangMucByHopDongDieuChinh(iIdHopDongId);
        }

        public void DeactiveHopDong(Guid id)
        {
            _ttHopDongRepository.DeactiveHopDong(id);
        }

        public double CalculateTotalValueGoiThau(Guid goithauId, Guid hopDongId)
        {
            return _hopDongGoiThauNhaThauRepository.CalculateTotalUsedValueOfGoiThau(goithauId, hopDongId);
        }

        public double CalculateTotalUsedValueOfChiPhi(Guid chiphiId, Guid hopDongId)
        {
            return _hopDongGoiThauNhaThauRepository.CalculateTotalUsedValueOfChiPhi(chiphiId, hopDongId);
        }

        public void SaveHopDong(VdtDaTtHopDong vdtDaTtHopDong)
        {
            _hopDongGoiThauNhaThauRepository.SaveHopDong(vdtDaTtHopDong);
        }

        public void SaveHopDongDC(VdtDaTtHopDong vdtDaTtHopDongDC, VdtDaTtHopDong vdtDaTtHopDongGoc)
        {
            _hopDongGoiThauNhaThauRepository.SaveHopDongDC(vdtDaTtHopDongDC, vdtDaTtHopDongGoc);
        }
    }
}
