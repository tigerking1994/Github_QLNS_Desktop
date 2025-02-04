using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlBangLuongThangBHXHService : ITlBangLuongThangBHXHService
    {
        private ITlBangLuongThangBHXHRepository _repository;
        public TlBangLuongThangBHXHService(ITlBangLuongThangBHXHRepository iTlBangLuongThangBHXHRepository)
        {
            _repository = iTlBangLuongThangBHXHRepository;
        }

        public int AddRange(IEnumerable<TlBangLuongThangBHXH> entities)
        {
            return _repository.AddRange(entities);
        }

        public int DeleteByParentId(Guid parentId)
        {
            return _repository.DeleteByParentId(parentId);
        }

        public IEnumerable<TlBangLuongThangBHXH> FindByCondition(Expression<Func<TlBangLuongThangBHXH, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public IEnumerable<TlBangLuongThangBHXH> FindByMonthYear(int month, int year)
        {
            return _repository.FindByMonthYear(month, year);
        }

        public IEnumerable<TlBangLuongThangBHXH> FindByParentId(Guid parentId)
        {
            return _repository.FindByParentId(parentId);
        }

        public IEnumerable<TlBangLuongThangBHXHQuery> ExportBangThanhToanTroCapOmDau(string maDonVi, int year, int month, int dvt)
        {
            return _repository.ExportBangThanhToanTroCapOmDau(maDonVi, year, month, dvt);
        }

        public IEnumerable<TlBangLuongThangBHXHReportQuery> ExportBangThanhToanTroCapOmDauGiaiThich(string lstmaCanbo, int year, int month, int dvt, int typePrint, string maDonVi)
        {
            return _repository.ExportBangThanhToanTroCapOmDauGiaiThich(lstmaCanbo, year, month, dvt, typePrint, maDonVi);
        }

        public IEnumerable<TlBangLuongThangBHXHQuery> ExportBangThanhToanTroCapThaiSan(string maDonVi, int year, int month, int dvt)
        {
            return _repository.ExportBangThanhToanTroCapThaiSan(maDonVi, year, month, dvt);
        }

        public IEnumerable<TlBangLuongThangBHXHQuery> ExportBangThanhToanTroCapTNLD(string maDonVi, int year, int month, int dvt)
        {
            return _repository.ExportBangThanhToanTroCapTNLD(maDonVi, year, month, dvt);
        }

        public IEnumerable<TlBangLuongThangBHXHQuery> ExportBangThanhToanTroCapHuuTriPhucVienThoiViecTuTuat(string maDonVi, int year, int month, int dvt)
        {
            return _repository.ExportBangThanhToanTroCapHuuTriPhucVienThoiViecTuTuat(maDonVi, year, month, dvt);
        }

        public IEnumerable<TlBangLuongThangBHXHQuery> ExportBangThanhToanTroXuatNgu(string maDonVi, int year, int month, int dvt)
        {
            return _repository.ExportBangThanhToanTroCapXuatNgu(maDonVi, year, month, dvt);
        }

        public DataTable GetDataLuongThangBHXH(Guid id)
        {
            return _repository.GetDataLuongThangBHXH(id);
        }

        public TlBangLuongThangBHXH GetLatestSalaryBHXH(string maCanBo, int thang, int nam)
        {
            return _repository.GetLatestSalaryBHXH(maCanBo, thang, nam);
        }

        public IEnumerable<TlBangLuongThangBHXHQuery> ExportBangLuongBHXH(int year, string months)
        {
            return _repository.ExportBangLuongBHXH(year, months);
        }

        public IEnumerable<TlBangLuongThangBHXHQuery> ExportDataQTCBHXH(int year, string months)
        {
            return _repository.ExportDataQTCBHXH(year, months);
        }

        public int RemoveRange(IEnumerable<TlBangLuongThangBHXH> items)
        {
            return _repository.RemoveRange(items);
        }

        public TlDmPhuCap GetCongChuan(string maCongChuan)
        {
            return _repository.GetCongChuan(maCongChuan);
        }

        public IEnumerable<TlBangLuongThangBHXHQuery> GetBangLuongTheoPhanHo(int year, int month)
        {
            return _repository.GetBangLuongTheoPhanHo(year, month);
        }
    }
}
