using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtQddtKhlcnhaThauService : IVdtQddtKhlcnhaThauService
    {
        private readonly IVdtQddtKhlcnhaThauRepository _repository;
        private readonly IVdtDaGoiThauRepository _vdtDaGoiThauRepository;

        public VdtQddtKhlcnhaThauService(
            IVdtQddtKhlcnhaThauRepository repository,
            IVdtDaGoiThauRepository vdtDaGoiThauRepository)
        {
            _repository = repository;
            _vdtDaGoiThauRepository = vdtDaGoiThauRepository;
        }

        public VdtQddtKhlcnhaThau Find(Guid iId)
        {
            return _repository.Find(iId);
        }

        public IEnumerable<ChiPhiHangMucQuery> GetKHLCNTDetailByListGoiThau(List<Guid> iIdGoiThaus, Guid iIdDuToan)
        {
            return _repository.GetKHLCNTDetailByListGoiThau(iIdGoiThaus, iIdDuToan);
        }

        public IEnumerable<KHLCNhaThauQuery> GetDataIndex()
        {
            return _repository.GetDataIndex();
        }

        public IEnumerable<KHLCNhaThauQuery> GetKHLCNhaThauByIdDuAn(Guid idDuAn)
        {
            return _repository.GetKHLCNhaThauByIdDuAn(idDuAn);
        }

    public IEnumerable<VdtDaDuAn> GetDuAnByDonViQuanLy(string iIdMaDonViQuanLy)
        {
            return _repository.GetDuAnByDonViQuanLy(iIdMaDonViQuanLy);
        }

        public bool CheckExistKHLCNhaThau(VdtQddtKhlcnhaThau data)
        {
            return _repository.CheckExistKHLCNhaThau(data);
        }

        public bool Insert(VdtQddtKhlcnhaThau data, string sUserLogin)
        {
            data.Id = Guid.NewGuid();
            data.DDateCreate = DateTime.Now;
            data.SUserCreate = sUserLogin;
            data.BActive = true;
            //data.BIsGoc = true;
            return _repository.Add(data) != 0;
        }

        public bool Update(VdtQddtKhlcnhaThau data, string sUserLogin)
        {
            var dataUpdate = _repository.Find(data.Id);
            dataUpdate.SSoQuyetDinh = data.SSoQuyetDinh;
            dataUpdate.DNgayQuyetDinh = data.DNgayQuyetDinh;
            dataUpdate.IIdDonViQuanLyId = data.IIdDonViQuanLyId;
            dataUpdate.IIdMaDonViQuanLy = data.IIdMaDonViQuanLy;
            dataUpdate.IIdDuAnId = data.IIdDuAnId;
            dataUpdate.SMoTa = data.SMoTa;
            if (dataUpdate.IIdDuToanId != data.IIdDuToanId && data.IIdDuToanId.HasValue)
            {
                _repository.DeleteGoiThauDetailWhenChangeDuToan(dataUpdate.Id, data.IIdDuToanId.Value);
            }
            dataUpdate.IIdDuToanId = data.IIdDuToanId;
            dataUpdate.DDateUpdate = DateTime.Now;
            dataUpdate.SUserUpdate = sUserLogin;
            return _repository.Update(dataUpdate) != 0;
        }

        public void Delete(KHLCNhaThauQuery data)
        {
            VdtQddtKhlcnhaThau dataDelete = _repository.Find(data.Id);
            if (dataDelete != null)
                _repository.Delete(dataDelete);
        }

        public IEnumerable<KHLCNhaThauDetailQuery> GetGoiThauByParentid(Guid iIdParentid)
        {
            return _repository.GetGoiThauByParentid(iIdParentid);
        }

        public IEnumerable<ChiPhiHangMucQuery> GetChiPhiHangMucByDuAn(Guid iIdDuAn)
        {
            return _repository.GetChiPhiHangMucByDuAn(iIdDuAn);
        }

        public IEnumerable<ChiPhiHangMucQuery> GetChiPhiHangMucByGoiThau(Guid iIdGoiThau, Guid iIdDuToanId)
        {
            return _repository.GetChiPhiHangMucByGoiThau(iIdGoiThau, iIdDuToanId);
        }

        public void InsertGoiThauChiPhi(List<VdtDaGoiThauChiPhi> datas)
        {
            _repository.InsertGoiThauChiPhi(datas);
        }

        public void InsertGoiThauHangMuc(List<VdtDaGoiThauHangMuc> datas)
        {
            _repository.InsertGoiThauHangMuc( datas);
        }

        public void InsertGoiThauNguonVon(List<VdtDaGoiThauNguonVon> datas)
        {
            _repository.InsertGoiThauNguonVon(datas);
        }

        public double GetTotalNguonVonInGoiThau(Guid iIdGoiThau)
        {
            return _repository.GetTotalNguonVonByGoiThau(iIdGoiThau);
        }

        public void DeleteGoiThaus(Guid idGoiThau)
        {
            _vdtDaGoiThauRepository.DeleteGoiThauChiTiet(idGoiThau);
        }

        public IEnumerable<VdtDaGoiThau> ListGoiThauByKHLCNhaThauId(Guid id)
        {
            return _repository.ListGoiThauByKHLCNhaThauId(id);
        }

        public void DeleteGoiThauByKHLCNTId(Guid idKHLCNT)
        {
            _repository.DeleteGoiThauByKHLCNTId(idKHLCNT);
        }

        public double GetTotalNguonVonGoiThauDC(Guid iIdGoiThau)
        {
            return _repository.GetTotalNguonVonGoiThauDC(iIdGoiThau);
        }

        public int UpdateLCNT(VdtQddtKhlcnhaThau entity)
        {
            return _repository.Update(entity);
        }

        public void UpdateGoiThauByLCNT(Guid lcntID)
        {
            _repository.UpdateGoiThauByLCNT(lcntID);
        }

        public bool CheckDuAnkExistKHLCNT(Guid duToanId)
        {
            return _repository.CheckDuAnkExistKHLCNT(duToanId);
        }

        public IEnumerable<ChiPhiHangMucQuery> GetChiPhiHangMucGoiThauByQDDauTu(Guid iIdGoiThau, Guid iIdQDDauTuId)
        {
            return _repository.GetChiPhiHangMucGoiThauByQDDauTu(iIdGoiThau,iIdQDDauTuId);
        }

        public IEnumerable<ChiPhiHangMucQuery> GetKHLCNTDetailQDDauTuByListGoiThau(List<Guid> iIdGoiThaus, Guid iIdQDDauTu)
        {
            return _repository.GetKHLCNTDetailQDDauTuByListGoiThau(iIdGoiThaus,iIdQDDauTu);
        }

        public IEnumerable<ChiPhiHangMucQuery> GetNguonVonByDuAnLCNTAdd(Guid id, string sLoaiChungTu)
        {
            return _repository.GetNguonVonByDuAnLCNTAdd(id, sLoaiChungTu);
        }

        public IEnumerable<ChiPhiHangMucQuery> GetChiPhiByDuAnLCNTAdd(Guid id, string sLoaiChungTu)
        {
            return _repository.GetChiPhiByDuAnLCNTAdd(id,sLoaiChungTu);
        }

        public IEnumerable<ChiPhiHangMucQuery> GetHangMucByDuAnLCNTAdd(Guid id, string sLoaiChungTu)
        {
            return _repository.GetHangMucByDuAnLCNTAdd(id, sLoaiChungTu);
        }

        public IEnumerable<ChiPhiHangMucQuery> GetNguonVonByKHLCNTUpdate(Guid id)
        {
            return _repository.GetNguonVonByKHLCNTUpdate(id);
        }

        public IEnumerable<ChiPhiHangMucQuery> GetChiPhiByKHLCNTUpdate(Guid id)
        {
            return _repository.GetChiPhiByKHLCNTUpdate(id);
        }

        public IEnumerable<ChiPhiHangMucQuery> GetHangMucByLCNTUpdate(Guid id, string sLoaiChungTu)
        {
            switch (sLoaiChungTu)
            {
                case LoaiKHLCNTType.DU_TOAN:
                    return _repository.GetHangMucByLCNTUpdate(id, true);
                case LoaiKHLCNTType.QDDT:
                    return _repository.GetHangMucByLCNTUpdate(id, false);
                default:
                    return new List<ChiPhiHangMucQuery>();
            }
        }

        public void LockOrUnlock(Guid id, bool isLock)
        {
            VdtQddtKhlcnhaThau chungTu = _repository.Find(id);
            chungTu.BKhoa = isLock;
            _repository.Update(chungTu);
        }

        public bool IsExistSoQuyetDinh(string soQuyetDinh, Guid id)
        {
            return _repository.IsExistSoQuyetDinh(soQuyetDinh, id);
        }
    }
}
