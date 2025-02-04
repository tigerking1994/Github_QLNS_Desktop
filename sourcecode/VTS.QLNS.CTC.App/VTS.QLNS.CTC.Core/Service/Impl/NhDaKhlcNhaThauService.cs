using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhDaKhlcNhaThauService : INhDaKhlcNhaThauService
    {
        private readonly INhDaKhlcNhaThauRepository _repository;
        private readonly INhDmLoaiHopDongRepository _loaiHopDongRepository;
        private readonly INhDmHinhThucChonNhaThauRepository _hinhthucRepository;
        private readonly INhDmPhuongThucChonNhaThauRepository _phuongthucRepository;
        private readonly INhDaDuAnRepository _duanRepository;
        private readonly INhDaDuToanRepository _dutoanRepository;


        public NhDaKhlcNhaThauService(
            INhDaKhlcNhaThauRepository khlcntRepository,
            INhDmLoaiHopDongRepository loaiHopDongRepository,
            INhDmHinhThucChonNhaThauRepository hinhthucRepository,
            INhDmPhuongThucChonNhaThauRepository phuongthucRepository,
            INhDaDuToanRepository dutoanRepository,
            INhDaDuAnRepository duanRepository)
        {
            _repository = khlcntRepository;
            _loaiHopDongRepository = loaiHopDongRepository;
            _hinhthucRepository = hinhthucRepository;
            _phuongthucRepository = phuongthucRepository;
            _dutoanRepository = dutoanRepository;
            _duanRepository = duanRepository;
        }

        #region KHLCNhaThau
        public NhDaKhlcnhaThau Find(Guid iId)
        {
            return _repository.Find(iId);
        }

        public bool Insert(NhDaKhlcnhaThau obj)
        {
            return _repository.Add(obj) != 0;
        }

        public bool Update(NhDaKhlcnhaThau data)
        {
            return _repository.Update(data) != 0;
        }

        public void Delete(Guid iId)
        {
            var obj = _repository.Find(iId);
            if (obj == null) return;
            if (obj.IIdParentId.HasValue)
            {
                var objParent = _repository.Find(obj.IIdParentId.Value);
                if (objParent != null)
                {
                    objParent.BIsActive = true;
                    _repository.Update(objParent);
                }
            }
            _repository.DeleteById(iId);
        }

        public void Log(Guid iID, string sUserLogIn)
        {
            var data = _repository.Find(iID);
            if (data == null) return;
            data.BIsKhoa = !data.BIsKhoa;
            data.SNguoiSua = sUserLogIn;
            data.DNgaySua = DateTime.Now;
            _repository.Update(data);
        }

        public IEnumerable<NhDaKhlcNhaThauQuery> GetAllKhlcntIndex(int iThuocMenu)
        {
            return _repository.GetAllKhlcntIndex(iThuocMenu);
        }
        public IEnumerable<NhDaKhlcNhaThauQuery> GetAllKhlcntMuaSam()
        {
            return _repository.GetAllKhlcntMuaSam();
        }
        #endregion

        #region GoiThau

        #endregion

        public IEnumerable<NhDaDuAn> GetDuAnHaveQDDauTu(Guid iIdKhlcntId, string sMaDonVi)
        {
            return _duanRepository.GetDuAnHaveQDDauTu(iIdKhlcntId, sMaDonVi);
        }
        public IEnumerable<NhDaDuAn> GetDuAnByDonVi(Guid iIdKhlcntId,Guid Id, int iloai)
        {
            return _duanRepository.GetDuAnByDonVi(iIdKhlcntId, Id,iloai);
        }

        #region Danh muc
        public IEnumerable<NhDmHinhThucChonNhaThau> GetHinhThucLuaChonNhaThau()
        {
            return _hinhthucRepository.FindAll();
        }

        public IEnumerable<NhDmPhuongThucChonNhaThau> GetPhuongThucLuaChonNhaThau()
        {
            return _phuongthucRepository.FindAll();
        }

        public IEnumerable<NhDmLoaiHopDong> GetLoaiHopDong()
        {
            return _loaiHopDongRepository.FindAll();
        }
        #endregion
        public void LockOrUnlock(Guid id, bool status)
        {
            NhDaKhlcnhaThau entity = _repository.Find(id);
            if (entity != null)
            {
                entity.BIsKhoa = status;
                _repository.Update(entity);
            }
        }
    }
}
