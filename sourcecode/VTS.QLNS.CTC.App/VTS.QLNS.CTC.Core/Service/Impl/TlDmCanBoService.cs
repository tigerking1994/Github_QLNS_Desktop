using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmCanBoService : ITlDmCanBoService
    {
        private readonly ITlDmCanBoRepository _cadresRepository;
        private readonly ITlCanBoPhuCapRepository _tlCanBoPhuCapRepository;
        private readonly ITlCanBoPhuCapNq104Repository _tlCanBoPhuCapNq104Repository;
        private readonly ITlCanBoPhuCapBridgeNq104Repository _tlCanBoPhuCapBridgeNq104Repository;

        public TlDmCanBoService(
            ITlDmCanBoRepository cadresRepository,
            ITlCanBoPhuCapRepository tlCanBoPhuCapRepository,
            ITlCanBoPhuCapNq104Repository tlCanBoPhuCapNq104Repository,
            ITlCanBoPhuCapBridgeNq104Repository tlCanBoPhuCapBridgeNq104Repository)
        {
            _cadresRepository = cadresRepository;
            _tlCanBoPhuCapRepository = tlCanBoPhuCapRepository;
            _tlCanBoPhuCapNq104Repository = tlCanBoPhuCapNq104Repository;
            _tlCanBoPhuCapBridgeNq104Repository = tlCanBoPhuCapBridgeNq104Repository;
        }

        public int Add(TlDmCanBo entity)
        {
            return _cadresRepository.Add(entity);
        }

        public int Delete(Guid id)
        {
            TlDmCanBo entity = _cadresRepository.Find(id);
            return _cadresRepository.Delete(entity);
        }

        public TlDmCanBo Find(params object[] keyValues)
        {
            return _cadresRepository.Find(keyValues);
        }

        public IEnumerable<TlDmCanBo> FindAll()
        {
            return _cadresRepository.FindAll().Where(x => x.IsDelete == true);
        }

        public IEnumerable<TlDmCanBo> FindAll(Expression<Func<TlDmCanBo, bool>> predicate)
        {
            return _cadresRepository.FindAll(predicate);
        }

        public IEnumerable<TlDmCanBo> FindAllInCludeDelete()
        {
            return _cadresRepository.FindAll();
        }

        public TlDmCanBo FindByMaCanBo(string maCanBo)
        {
            return _cadresRepository.FindByMaCanbo(maCanBo);
        }

        public IEnumerable<TlDmCanBo> FindByMonth(int month)
        {
            return _cadresRepository.FindByMonth(month);
        }

        public IEnumerable<TlDmCanBo> FindByMonthAndParent(int month, string parent)
        {
            return _cadresRepository.FindByMonthAndDonVi(month, parent);
        }

        public int Update(TlDmCanBo entity)
        {
            return _cadresRepository.Update(entity);
        }

        public IEnumerable<TlDmCanBo> FindByConditionInsurance(string maDonVi, int thang, int nam)
        {
            return _cadresRepository.FindByConditionInsurance(maDonVi, thang, nam);
        }

        public IEnumerable<TlDmCanBo> FindByCondition(Expression<Func<TlDmCanBo, bool>> predicate)
        {
            return _cadresRepository.FindAll(predicate);
        }

        public IEnumerable<TlDmCanBoQuery> FindCanBoQuyetToanQuanSo(string maDonVi, int thang, int nam)
        {
            return _cadresRepository.FindCanBoQuyetToanQuanSo(maDonVi, thang, nam);
        }

        public IEnumerable<TlDmCanBoQuery> FindCanBoQuyetToanQuanSoGiam(string maDonVi, int thang, int nam)
        {
            return _cadresRepository.FindCanBoQuyetToanQuanSoGiam(maDonVi, thang, nam);
        }

        public int AddRange(IEnumerable<TlDmCanBo> entities)
        {
            return _cadresRepository.AddRange(entities);
        }

        public int UpdateRange(List<TlDmCanBo> entities)
        {
            return _cadresRepository.UpdateRange(entities);
        }

        public TlDmCanBo FindById(Guid id)
        {
            return _cadresRepository.Find(id);
        }

        public IEnumerable<TlDmCanBo> FindAllState()
        {
            return _cadresRepository.FindAll();
        }

        public IEnumerable<TlDmCanBo> FindLoadIndex()
        {
            return _cadresRepository.FindLoadIndex();
        }

        public IEnumerable<TlDmCanBo> FindAllCanBo()
        {
            return _cadresRepository.FindAllCanBo();
        }

        public IEnumerable<TlDmCanBo> FindCanBoXoa()
        {
            return _cadresRepository.FindCanBoXoa();
        }

        public void BulkInsert(IEnumerable<TlDmCanBo> entities)
        {
            _cadresRepository.BulkInsert(entities);
        }

        public void BulkUpdate(List<TlDmCanBo> entities)
        {
            _cadresRepository.BulkUpdate(entities);
        }

        public void UpdateMulti(List<TlDmCanBo> entities, List<TlCanBoPhuCap> tlCanBoPhuCaps)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                _cadresRepository.BulkUpdate(entities);
                _tlCanBoPhuCapRepository.BulkUpdate(tlCanBoPhuCaps);
                transactionScope.Complete();
            }
        }

        public void UpdateMulti(List<TlDmCanBo> entities, List<TlCanBoPhuCapNq104> tlCanBoPhuCaps)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                _cadresRepository.BulkUpdate(entities);
                _tlCanBoPhuCapNq104Repository.BulkUpdate(tlCanBoPhuCaps);
                transactionScope.Complete();
            }
        }

        public IEnumerable<TlDmCanBoQuery> FindCanBoDieuChinh(int? thang, int? nam, string maDonVi, string maCapBac, decimal? hskv, string maTangGiam, string maChucVu, decimal? tienAn, DateTime? ngayNhapNgu, bool isHsq)
        {
            return _cadresRepository.FindCanBoDieuChinh(thang, nam, maDonVi, maCapBac, hskv, maTangGiam, maChucVu, tienAn, ngayNhapNgu, isHsq);
        }

        public IEnumerable<TlCanBoThueTncnQuery> FindCanBoThueTncn(bool isNew = false)
        {
            return _cadresRepository.FindCanBoThueTncn(isNew);
        }

        public void InsertMulti(List<TlDmCanBo> entities, List<TlCanBoPhuCap> tlCanBoPhuCaps)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                _cadresRepository.BulkInsert(entities);
                _tlCanBoPhuCapRepository.BulkInsert(tlCanBoPhuCaps);
                transactionScope.Complete();
            }
        }

        public void InsertMulti(List<TlDmCanBo> entities, List<TlCanBoPhuCapNq104> tlCanBoPhuCaps)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                _cadresRepository.BulkInsert(entities);
                _tlCanBoPhuCapNq104Repository.BulkInsert(tlCanBoPhuCaps);
                transactionScope.Complete();
            }
        }

        public IEnumerable<TlCanBoRaQuanQuery> FindCanBoRaQuan(bool isNew = false)
        {
            return _cadresRepository.FindCanBoRaQuan(isNew);
        }

        public void Copy(IEnumerable<TlDmCanBo> entities, IEnumerable<TlCanBoPhuCap> tlCanBoPhuCaps)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                _cadresRepository.BulkInsert(entities);
                _tlCanBoPhuCapRepository.BulkInsert(tlCanBoPhuCaps);

                transactionScope.Complete();
            }
        }

        public void Copy(IEnumerable<TlDmCanBo> entities, IEnumerable<TlCanBoPhuCapNq104> tlCanBoPhuCaps)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                _cadresRepository.BulkInsert(entities);
                _tlCanBoPhuCapNq104Repository.BulkInsert(tlCanBoPhuCaps);

                transactionScope.Complete();
            }
        }

        public DataTable ReportChiTietQsTangGiam(string maDonVi, int thang, int nam, string sM)
        {
            int thangTruoc = thang - 1;
            int namTruoc = nam;
            if (thang == 1)
            {
                thangTruoc = 12;
                namTruoc = nam - 1;
            }
            var data = _cadresRepository.ReportChiTietQsTangGiam(maDonVi, thang, nam, sM, thangTruoc, namTruoc);
            var result = new DataTable();
            if (data != null && data.Rows.Count > 0)
            {
                data.Columns.Add(ExportColumnHeader.STT, typeof(int));
                data.Columns.Add(ExportColumnHeader.IS_PARENT, typeof(bool));
                result = data.Clone();

                var qsStr = data.AsEnumerable().Select(x => x.Field<string>("NoiDung")).Distinct().ToList();
                foreach (var item in qsStr)
                {
                    var rowParent = result.NewRow();
                    rowParent["TenCanBo"] = item;
                    rowParent[ExportColumnHeader.IS_PARENT] = true;
                    var rowDetails = data.AsEnumerable().Where(x => x.Field<string>("NoiDung").Equals(item));
                    rowParent["SoLuong"] = rowDetails.Sum(x => x.Field<int>("SoLuong"));
                    result.Rows.Add(rowParent);

                    var index = 1;
                    foreach (DataRow item1 in rowDetails)
                    {
                        item1["STT"] = index++;
                        result.Rows.Add(item1.ItemArray);
                    }
                }
            }

            return result;
        }

        public IEnumerable<TlDanhSachCanBoQuery> FindDanhSachCanBo()
        {
            return _cadresRepository.FindDanhSachCanBo();
        }

        public IEnumerable<TlDanhSachCanBoQuery> FindDanhSachCanBoNq104()
        {
            return _cadresRepository.FindDanhSachCanBoNq104();
        }

        public IEnumerable<TlDanhSachCanBoQuery> FindDanhSachCanBoByCondition(int thang, int nam, string maDonVi)
        {
            return _cadresRepository.FindDanhSachCanBoByCondition(thang, nam, maDonVi);
        }

        public TlDmCanBo FindByMaHieuCanbo(string maHieuCanBo, string maCb)
        {
            return _cadresRepository.FindByMaHieuCanbo(maHieuCanBo,maCb);
        }

        public void UpdateMulti(List<TlDmCanBo> entities, List<TlCanBoPhuCapNq104> tlCanBoPhuCaps, List<TlCanBoPhuCapBridgeNq104> tlCanBoPhuCapBridges)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                _cadresRepository.BulkUpdate(entities);
                _tlCanBoPhuCapNq104Repository.BulkUpdate(tlCanBoPhuCaps);
                _tlCanBoPhuCapBridgeNq104Repository.BulkUpdate(tlCanBoPhuCapBridges);
                transactionScope.Complete();
            }
        }
    }
}
