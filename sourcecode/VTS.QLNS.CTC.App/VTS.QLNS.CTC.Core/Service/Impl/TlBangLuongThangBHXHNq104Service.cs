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
    public class TlBangLuongThangBHXHNq104Service : ITlBangLuongThangBHXHNq104Service
    {
        private ITlBangLuongThangBHXHNq104Repository _repository;
        public TlBangLuongThangBHXHNq104Service(ITlBangLuongThangBHXHNq104Repository iTlBangLuongThangBHXHRepository)
        {
            _repository = iTlBangLuongThangBHXHRepository;
        }

        public int AddRange(IEnumerable<TlBangLuongThangBHXHNq104> entities)
        {
            return _repository.AddRange(entities);
        }

        public int DeleteByParentId(Guid parentId)
        {
            return _repository.DeleteByParentId(parentId);
        }

        public IEnumerable<TlBangLuongThangBHXHNq104> FindByCondition(Expression<Func<TlBangLuongThangBHXHNq104, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public IEnumerable<TlBangLuongThangBHXHNq104> FindByMonthYear(int month, int year)
        {
            return _repository.FindByMonthYear(month, year);
        }

        public IEnumerable<TlBangLuongThangBHXHNq104> FindByParentId(Guid parentId)
        {
            return _repository.FindByParentId(parentId);
        }

        public IEnumerable<TlBangLuongThangBHXHNq104Query> ExportBangThanhToanTroCapOmDau(string maDonVi, int year, int month, int dvt)
        {
            return _repository.ExportBangThanhToanTroCapOmDau(maDonVi, year, month, dvt);
        }

        public IEnumerable<TlBangLuongThangBHXHNq104ReportQuery> ExportBangThanhToanTroCapOmDauGiaiThich(string lstmaCanbo, int year, int month, int dvt, int typePrint)
        {
            return _repository.ExportBangThanhToanTroCapOmDauGiaiThich(lstmaCanbo, year, month, dvt, typePrint);
        }

        public IEnumerable<TlBangLuongThangBHXHNq104Query> ExportBangThanhToanTroCapThaiSan(string maDonVi, int year, int month, int dvt)
        {
            return _repository.ExportBangThanhToanTroCapThaiSan(maDonVi, year, month, dvt);
        }

        public IEnumerable<TlBangLuongThangBHXHNq104Query> ExportBangThanhToanTroCapTNLD(string maDonVi, int year, int month, int dvt)
        {
            return _repository.ExportBangThanhToanTroCapTNLD(maDonVi, year, month, dvt);
        }

        public IEnumerable<TlBangLuongThangBHXHNq104Query> ExportBangThanhToanTroCapHuuTriPhucVienThoiViecTuTuat(string maDonVi, int year, int month, int dvt)
        {
            return _repository.ExportBangThanhToanTroCapHuuTriPhucVienThoiViecTuTuat(maDonVi, year, month, dvt);
        }

        public IEnumerable<TlBangLuongThangBHXHNq104Query> ExportBangThanhToanTroXuatNgu(string maDonVi, int year, int month, int dvt)
        {
            return _repository.ExportBangThanhToanTroCapXuatNgu(maDonVi, year, month, dvt);
        }

        public DataTable GetDataLuongThangBHXH(Guid id)
        {
            return _repository.GetDataLuongThangBHXH(id);
        }

        public TlBangLuongThangBHXHNq104 GetLatestSalaryBHXH(string maCanBo, int thang, int nam)
        {
            return _repository.GetLatestSalaryBHXH(maCanBo, thang, nam);
        }

        public IEnumerable<TlBangLuongThangBHXHNq104Query> ExportBangLuongBHXH(int year, string months)
        {
            return _repository.ExportBangLuongBHXH(year, months);
        }

        public IEnumerable<TlBangLuongThangBHXHNq104Query> ExportDataQTCBHXH(int year, string months)
        {
            return _repository.ExportDataQTCBHXH(year, months);
        }

        public int RemoveRange(IEnumerable<TlBangLuongThangBHXHNq104> items)
        {
            return _repository.RemoveRange(items);
        }

        public TlDmPhuCapNq104 GetCongChuan(string maCongChuan)
        {
            return _repository.GetCongChuan(maCongChuan);
        }
    }
}
