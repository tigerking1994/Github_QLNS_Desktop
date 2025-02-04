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
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhQtcNamKinhPhiQuanLyChiTietService : IBhQtcNamKinhPhiQuanLyChiTietService
    {
        private readonly IBhQtcNamKinhPhiQuanLyChiTietRepostiory _repostiory;
        public BhQtcNamKinhPhiQuanLyChiTietService(IBhQtcNamKinhPhiQuanLyChiTietRepostiory repostiory)
        {
            _repostiory = repostiory;
        }

        public void AddAggregate(QtcNamKinhPhiQuanLyCriteria criteria)
        {
            _repostiory.AddAggregate(criteria);
        }

        public int AddRange(List<BhQtcNamKinhPhiQuanLyChiTiet> items)
        {
            return _repostiory.AddRange(items);
        }

        public void CreateChungTuChiTietTheoQuy(Guid id, string idMaDonVi, int? namChungTu, string sNguoiTao)
        {
            _repostiory.CreateChungTuChiTietTheoQuy(id, idMaDonVi, namChungTu, sNguoiTao);
        }

        public bool ExitChungTuChiTiet(QtcNamKinhPhiQuanLyCriteria searchCondition)
        {
            return _repostiory.ExitChungTuChiTiet(searchCondition);
        }

        public IEnumerable<BhQtcNamKinhPhiQuanLyChiTiet> FindAllChungTuDuToan()
        {
            return _repostiory.FindAll();
        }

        public IEnumerable<BhQtcNamKinhPhiQuanLyChiTiet> FindByCondition(Expression<Func<BhQtcNamKinhPhiQuanLyChiTiet, bool>> predicate)
        {
            return _repostiory.FindAll(predicate);
        }

        public BhQtcNamKinhPhiQuanLyChiTiet FindById(Guid id)
        {
            return _repostiory.FindById(id);
        }

        public IEnumerable<BhQtcNamKinhPhiQuanLyChiTiet> FindByIdChiTiet(Guid id)
        {
            return _repostiory.FindByIdChiTiet(id);
        }

        public List<BhQtcNamKinhPhiQuanLyChiTietQuery> FindChungTuChiTiet(QtcNamKinhPhiQuanLyCriteria searchCondition)
        {
            return _repostiory.FindChungTuChiTiet(searchCondition);
        }

        public List<BhQtcNamKinhPhiQuanLyChiTietQuery> FindGetReportKeHoach(QtcNamKinhPhiQuanLyCriteria searchCondition)
        {
            return _repostiory.FindGetReportKeHoach(searchCondition);
        }

        public List<ReportBHQTCNKPQuanLyPhuLucQuery> FindGetReportPhuLuc(int iNamLamViec, string listTenDonVi, int dvt)
        {
            return _repostiory.FindGetReportPhuLuc(iNamLamViec, listTenDonVi, dvt);
        }

        public List<BhQtcNamKinhPhiQuanLyChiTietQuery> FindTienThuChiForChungTu(QtcNamKinhPhiQuanLyCriteria searchCondition)
        {
            return _repostiory.FindTienThuChiForChungTu(searchCondition);
        }

        public List<BhQtcNamKinhPhiQuanLyChiTietQuery> GetTienPhanBoChiTietDuToanChi(QtcNamKinhPhiQuanLyCriteria searchCondition)
        {
            return _repostiory.GetTienPhanBoChiTietDuToanChi(searchCondition);
        }

        public int RemoveRange(IEnumerable<BhQtcNamKinhPhiQuanLyChiTiet> bhQtcKinhphiQuanlyChiTiets)
        {
            return _repostiory.RemoveRange(bhQtcKinhphiQuanlyChiTiets);
        }

        public int Update(BhQtcNamKinhPhiQuanLyChiTiet item)
        {
            return _repostiory.Update(item);
        }

        public List<ReportBHQTCNKPQuanLyPhuLucQuery> FindGetReportQTKPQL_KPKTSDK(int iNamLamViec, string listTenDonVi, int typeValue)
        {
            return _repostiory.FindGetReportQTKPQL_KPKTSDK(iNamLamViec, listTenDonVi, typeValue);
        }
    }
}
