using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class DttBHXHPhanBoService : IDttBHXHPhanBoService
    {
        private readonly IDttBHXHPhanBoRepository _dttBHXHPhanBoRepository;

        public DttBHXHPhanBoService(IDttBHXHPhanBoRepository iDttBHXHPhanBoRepository)
        {
            _dttBHXHPhanBoRepository = iDttBHXHPhanBoRepository;
        }

        public BhDtPhanBoChungTu Add(BhDtPhanBoChungTu entity)
        {
            _dttBHXHPhanBoRepository.Add(entity);
            return entity;
        }

        public int Delete(Guid id)
        {
            BhDtPhanBoChungTu entity = _dttBHXHPhanBoRepository.Find(id);
            return _dttBHXHPhanBoRepository.Delete(entity);
        }

        public IEnumerable<BhDuToanTongHopThuChiQuery> ExportTongHopChiDonVi(int year, string donVis, string soQuyetDinh, int donViTinh, string ngayQuyetDinh, bool isMillionRound)
        {
            return _dttBHXHPhanBoRepository.ExportTongHopChiDonVi(year, donVis, soQuyetDinh, donViTinh, ngayQuyetDinh, isMillionRound);
        }

        public IEnumerable<BhDuToanTongHopThuChiQuery> ExportTongHopDieuChinhChiDonVi(int year, string donVis, string soQuyetDinh, int donViTinh, bool isMillionRound)
        {
            return _dttBHXHPhanBoRepository.ExportTongHopDieuChinhChiDonVi(year, donVis, soQuyetDinh, donViTinh, isMillionRound);
        }

        public IEnumerable<BhDuToanTongHopThuChiQuery> ExportTongHopThuDonVi(int year, string donVis, string soQuyetDinh, int donViTinh, string ngayQuyetDinh, bool isMillionRound)
        {
            return _dttBHXHPhanBoRepository.ExportTongHopThuDonVi(year, donVis, soQuyetDinh, donViTinh, ngayQuyetDinh, isMillionRound);
        }
        
        public IEnumerable<BhDuToanTongHopThuChiQuery> ExportTongHopDieuChinhThuDonVi(int year, string donVis, string soQuyetDinh, int donViTinh, bool isMillionRound)
        {
            return _dttBHXHPhanBoRepository.ExportTongHopDieuChinhThuDonVi(year, donVis, soQuyetDinh, donViTinh, isMillionRound);
        }

        public IEnumerable<BhDttChungTuDotNhanQuery> FindAllChungTuDotNhan(EstimationVoucherCriteria condition)
        {
            return _dttBHXHPhanBoRepository.FindAllChungTuDotNhan(condition);
        }

        public Dictionary<string, string> FindAllDict(int namLamViec, int? loaiDuToan)
        {
            return _dttBHXHPhanBoRepository.FindAllDict(namLamViec, loaiDuToan);
        }

        public IEnumerable<BhDtPhanBoChungTu> FindByCondition(DuToanThuChungTuCriteria condition)
        {
            return _dttBHXHPhanBoRepository.FindByCondition(condition);
        }

        public IEnumerable<BhDtPhanBoChungTu> FindByCondition(Expression<Func<BhDtPhanBoChungTu, bool>> predicate)
        {
            return _dttBHXHPhanBoRepository.FindAll(predicate);
        }

        public BhDtPhanBoChungTu FindById(Guid id)
        {
            return _dttBHXHPhanBoRepository.Find(id);
        }

        public IEnumerable<BhDtPhanBoChungTu> FindByLuyKeDot(DateTime? ngayQuyetDinh, int nam)
        {
            return _dttBHXHPhanBoRepository.FindByLuyKeDot(ngayQuyetDinh, nam);
        }

        public IEnumerable<BhDtPhanBoChungTu> FindBySoQuyetDinh(string soQuyetDinh, int nam)
        {
            return _dttBHXHPhanBoRepository.FindBySoQuyetDinh(soQuyetDinh, nam);
        }

        public IEnumerable<BhDttChungTuDotNhanQuery> FindChungTuDotNhan(EstimationVoucherCriteria condition, bool isCreate)
        {
            return _dttBHXHPhanBoRepository.FindChungTuDotNhan(condition, isCreate);
        }

        public IEnumerable<BhDtPhanBoChungTu> FindDotNhanByChungTuPhanBo(Guid idPhanBo)
        {
            return _dttBHXHPhanBoRepository.FindDotNhanByChungTuPhanBo(idPhanBo);
        }

        public int FindNextSoChungTuIndex(Expression<Func<BhDtPhanBoChungTu, bool>> predicate)
        {
            return _dttBHXHPhanBoRepository.FindNextSoChungTuIndex(predicate);
        }

        public IEnumerable<BhDuToanThuChiQuery> GetSoQuyetDinh(int year)
        {
            return _dttBHXHPhanBoRepository.GetSoQuyetDinh(year);
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            return _dttBHXHPhanBoRepository.LockOrUnLock(id, lockStatus);
        }

        public int Update(BhDtPhanBoChungTu item)
        {
            return _dttBHXHPhanBoRepository.Update(item);
        }

        public IEnumerable<BhDuToanThuChiQuery> FindDotDuToan()
        {
            return _dttBHXHPhanBoRepository.FindDotDuToan();
        }

        public IEnumerable<BhDuToanThuChiQuery> GetSoQuyetDinhDTT(int year, bool isInTheoChungTu)
        {
            return _dttBHXHPhanBoRepository.GetSoQuyetDinhDTT(year, isInTheoChungTu);
        }

        public IEnumerable<BhDtPhanBoChungTuQuery> FindBySoQuyetDinhLuyKe(string soQuyetDinh, string ngayQuyetDinh, int nam)
        {
            return _dttBHXHPhanBoRepository.FindBySoQuyetDinhLuyKe(soQuyetDinh, ngayQuyetDinh, nam);
        }

        public IEnumerable<BhDtPhanBoChungTu> FindBySoChungTu(string soChungTu, int nam)
        {
            return _dttBHXHPhanBoRepository.FindBySoChungTu(soChungTu, nam);
        }

        public IEnumerable<BhDtPhanBoChungTu> FindByIdNhanDuToan(string id)
        {
            return _dttBHXHPhanBoRepository.FindByIdNhanDuToan(id);
        }

        public IEnumerable<BhDuToanThuChiQuery> GetSoQuyetDinhDTTBHXHBHYTBHTN(int year)
        {
            return _dttBHXHPhanBoRepository.GetSoQuyetDinhDTTBHXHBHYTBHTN(year);
        }

        public IEnumerable<BhDuToanThuChiQuery> GetSoQuyetDinhDTCBHXHBHYTBHTN(int year)
        {
            return _dttBHXHPhanBoRepository.GetSoQuyetDinhDTCBHXHBHYTBHTN(year);
        }

        public IEnumerable<BhPbdttmBHYTQuery> FindBySoQuyetDinhLuyKeDttmBHYT(string soQuyetDinh, string ngayQuyetDinh, int nam)
        {
            return _dttBHXHPhanBoRepository.FindBySoQuyetDinhLuyKeDttmBHYT(soQuyetDinh, ngayQuyetDinh, nam); ;
        }

        public List<string> GetDonViDttDttnDtc(int namLamViec, string soQuyetDinh)
        {
            return _dttBHXHPhanBoRepository.GetDonViDttDttnDtc(namLamViec, soQuyetDinh);
        }
    }
}
