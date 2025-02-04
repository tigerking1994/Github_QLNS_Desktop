using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Repository.Impl;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtTtDeNghiThanhToanKhvService : IVdtTtDeNghiThanhToanKhvService
    {
        private readonly IVdtTtDeNghiThanhToanKhvRepository _cpChungTuChiTietRepository;
        private readonly IVdtTtThongTinCanCuRepository _vdtTtThongTinCanCuRepository;

        public VdtTtDeNghiThanhToanKhvService(IVdtTtDeNghiThanhToanKhvRepository chungTuChiTietRepository, IVdtTtThongTinCanCuRepository vdtTtThongTinCanCuRepository)
        {
            _cpChungTuChiTietRepository = chungTuChiTietRepository;
            _vdtTtThongTinCanCuRepository = vdtTtThongTinCanCuRepository;
        }

        public VdtTtDeNghiThanhToanKhv Add(VdtTtDeNghiThanhToanKhv entity)
        {
            _cpChungTuChiTietRepository.Add(entity);
            return entity;
        }

        public int Delete(Guid id)
        {
            VdtTtDeNghiThanhToanKhv entity = _cpChungTuChiTietRepository.Find(id);
            if (entity != null)
            {
                return _cpChungTuChiTietRepository.Delete(entity);
            }
            return 0;
        }

        public VdtTtDeNghiThanhToanKhv Find(params object[] keyValues)
        {
            return _cpChungTuChiTietRepository.Find(keyValues);
        }

        public int Update(VdtTtDeNghiThanhToanKhv entity)
        {
            return _cpChungTuChiTietRepository.Update(entity);
        }

        public IEnumerable<VdtTtDeNghiThanhToanKhv> FindByCondition(Expression<Func<VdtTtDeNghiThanhToanKhv, bool>> predicate)
        {
            return _cpChungTuChiTietRepository.FindAll(predicate);
        }

        public void DeleteByDeNghiThanhToanId(Guid deNghiThanhToanId)
        {
            List<VdtTtDeNghiThanhToanKhv> list = _cpChungTuChiTietRepository.FindByDeNghiThanhToanId(deNghiThanhToanId);
            foreach (VdtTtDeNghiThanhToanKhv item in list)
            {
                _cpChungTuChiTietRepository.Delete(item.Id);
            }
        }

        public List<VdtTtDeNghiThanhToanKhv> FindByDeNghiThanhToanId(Guid deNghiThanhToanId)
        {
            return _cpChungTuChiTietRepository.FindByDeNghiThanhToanId(deNghiThanhToanId);
        }

        public List<VdtTtKeHoachVonQuery> GetKhvDeNghiTamUng(Guid iIdDuAnId, int iIdNguonVonId, DateTime dNgayDeNghi, int iNamKeHoach, int iCoQuanThanhToan, Guid iIdPheDuyet, Guid ID_DuAn_HangMuc)
        {
            return _cpChungTuChiTietRepository.GetKhvDeNghiTamUng(iIdDuAnId, iIdNguonVonId, dNgayDeNghi, iNamKeHoach, iCoQuanThanhToan, iIdPheDuyet, ID_DuAn_HangMuc);
        }

        public List<VdtTtKeHoachVonQuery> GetKhvDeNghiThanhToan(Guid iIdDuAnId, int iIdNguonVonId, DateTime dNgayDeNghi, int iNamKeHoach, int iCoQuanThanhToan, Guid iIdPheDuyet, Guid ID_DuAn_HangMuc)
        {
            return _cpChungTuChiTietRepository.GetKhvDeNghiThanhToan(iIdDuAnId, iIdNguonVonId, dNgayDeNghi, iNamKeHoach, iCoQuanThanhToan, iIdPheDuyet, ID_DuAn_HangMuc);
        }

        public List<MlnsByKeHoachVonQuery> GetMucLucNganSachByKeHoachVon(int iNamLamViec, List<TongHopNguonNSDauTuQuery> lstCondition)
        {
            return _cpChungTuChiTietRepository.GetMucLucNganSachByKeHoachVon(iNamLamViec, lstCondition);
        }

        public List<VdtTtThongTinCanCu> GetThongTinCanCuByIdDeNghiThanhToan(Guid? iID_DeNghiThanhToanID)
        {
            return _vdtTtThongTinCanCuRepository.GetThongTinCanCuByIdDeNghiThanhToan(iID_DeNghiThanhToanID);
        }

        public int AddRangeThongTinCanCu(IEnumerable<VdtTtThongTinCanCu> entities)
        {
            return _vdtTtThongTinCanCuRepository.AddRange(entities);
        }

        public int UpdateThongTinCanCu(VdtTtThongTinCanCu entity)
        {
            return _vdtTtThongTinCanCuRepository.Update(entity);
        }

        public int DeleteThongTinCanCu(Guid id)
        {
            return _vdtTtThongTinCanCuRepository.Delete(id);
        }

        public VdtTtThongTinCanCu FindThongTinCanCuById(params object[] keyValues)
        {
            return _vdtTtThongTinCanCuRepository.Find(keyValues);
        }
    }
}
