using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Repository.Impl;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TlDmCanBoNq104Service : ITlDmCanBoNq104Service
    {
        private readonly ITlDmCanBoNq104Repository _cadresRepository;
        private readonly ITlCanBoPhuCapRepository _tlCanBoPhuCapRepository;
        private readonly ITlCanBoPhuCapNq104Repository _tlCanBoPhuCapNq104Repository;
        private readonly ITlCanBoPhuCapBridgeNq104Repository _tlCanBoPhuCapBridgeNq104Repository;

        public TlDmCanBoNq104Service(
            ITlDmCanBoNq104Repository cadresRepository,
            ITlCanBoPhuCapRepository tlCanBoPhuCapRepository,
            ITlCanBoPhuCapNq104Repository tlCanBoPhuCapNq104Repository,
            ITlCanBoPhuCapBridgeNq104Repository tlCanBoPhuCapBridgeNq104Repository)
        {
            _cadresRepository = cadresRepository;
            _tlCanBoPhuCapRepository = tlCanBoPhuCapRepository;
            _tlCanBoPhuCapNq104Repository = tlCanBoPhuCapNq104Repository;
            _tlCanBoPhuCapBridgeNq104Repository = tlCanBoPhuCapBridgeNq104Repository;
        }

        public int Add(TlDmCanBoNq104 entity)
        {
            return _cadresRepository.Add(entity);
        }

        public int Delete(Guid id)
        {
            TlDmCanBoNq104 entity = _cadresRepository.Find(id);
            return _cadresRepository.Delete(entity);
        }

        public TlDmCanBoNq104 Find(params object[] keyValues)
        {
            return _cadresRepository.Find(keyValues);
        }

        public IEnumerable<TlDmCanBoNq104> FindAll()
        {
            return _cadresRepository.FindAll().Where(x => x.IsDelete == true);
        }

        public IEnumerable<TlDmCanBoNq104> FindAll(Expression<Func<TlDmCanBoNq104, bool>> predicate)
        {
            return _cadresRepository.FindAll(predicate);
        }

        public IEnumerable<TlDmCanBoNq104> FindAllInCludeDelete()
        {
            return _cadresRepository.FindAll();
        }

        public TlDmCanBoNq104 FindByMaCanBo(string maCanBo)
        {
            return _cadresRepository.FindByMaCanbo(maCanBo);
        }

        public IEnumerable<TlDmCanBoNq104> FindByMonth(int month)
        {
            return _cadresRepository.FindByMonth(month);
        }

        public IEnumerable<TlDmCanBoNq104> FindByMonthAndParent(int month, string parent)
        {
            return _cadresRepository.FindByMonthAndDonVi(month, parent);
        }

        public int Update(TlDmCanBoNq104 entity)
        {
            return _cadresRepository.Update(entity);
        }

        public IEnumerable<TlDmCanBoNq104> FindByConditionInsurance(string maDonVi, int thang, int nam)
        {
            return _cadresRepository.FindByConditionInsurance(maDonVi, thang, nam);
        }

        public IEnumerable<TlDmCanBoNq104> FindByCondition(Expression<Func<TlDmCanBoNq104, bool>> predicate)
        {
            return _cadresRepository.FindAll(predicate);
        }

        public IEnumerable<TlDmCanBoNq104Query> FindCanBoQuyetToanQuanSo(string maDonVi, int thang, int nam)
        {
            return _cadresRepository.FindCanBoQuyetToanQuanSo(maDonVi, thang, nam);
        }

        public IEnumerable<TlDmCanBoNq104Query> FindCanBoQuyetToanQuanSoGiam(string maDonVi, int thang, int nam)
        {
            return _cadresRepository.FindCanBoQuyetToanQuanSoGiam(maDonVi, thang, nam);
        }

        public int AddRange(IEnumerable<TlDmCanBoNq104> entities)
        {
            return _cadresRepository.AddRange(entities);
        }

        public int UpdateRange(List<TlDmCanBoNq104> entities)
        {
            return _cadresRepository.UpdateRange(entities);
        }

        public TlDmCanBoNq104 FindById(Guid id)
        {
            return _cadresRepository.Find(id);
        }

        public IEnumerable<TlDmCanBoNq104> FindAllState()
        {
            return _cadresRepository.FindAll();
        }

        public IEnumerable<TlDmCanBoNq104> FindLoadIndex()
        {
            return _cadresRepository.FindLoadIndex();
        }

        public IEnumerable<TlDmCanBoNq104> FindAllCanBo()
        {
            return _cadresRepository.FindAllCanBo();
        }

        public IEnumerable<TlDmCanBoNq104> FindCanBoXoa()
        {
            return _cadresRepository.FindCanBoXoa();
        }

        public void BulkInsert(IEnumerable<TlDmCanBoNq104> entities)
        {
            _cadresRepository.BulkInsert(entities);
        }

        public void BulkUpdate(List<TlDmCanBoNq104> entities)
        {
            _cadresRepository.BulkUpdate(entities);
        }

        public void UpdateMulti(List<TlDmCanBoNq104> entities, List<TlCanBoPhuCap> tlCanBoPhuCaps)
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

        public void UpdateMulti(List<TlDmCanBoNq104> entities, List<TlCanBoPhuCapNq104> tlCanBoPhuCaps)
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

        public IEnumerable<TlDmCanBoNq104Query> FindCanBoDieuChinh(int? thang, int? nam, string maDonVi, string maCapBac, decimal? hskv, string maTangGiam, string maChucVu, decimal? tienAn, DateTime? ngayNhapNgu, bool isHsq)
        {
            return _cadresRepository.FindCanBoDieuChinh(thang, nam, maDonVi, maCapBac, hskv, maTangGiam, maChucVu, tienAn, ngayNhapNgu, isHsq);
        }

        public IEnumerable<TlCanBoThueTncnQuery> FindCanBoThueTncn(bool isNew = false)
        {
            return _cadresRepository.FindCanBoThueTncn(isNew);
        }

        public void InsertMulti(List<TlDmCanBoNq104> entities, List<TlCanBoPhuCap> tlCanBoPhuCaps)
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

        public void InsertMulti(List<TlDmCanBoNq104> entities, List<TlCanBoPhuCapNq104> tlCanBoPhuCaps)
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

        public void Copy(IEnumerable<TlDmCanBoNq104> entities, IEnumerable<TlCanBoPhuCap> tlCanBoPhuCaps)
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

        public void Copy(IEnumerable<TlDmCanBoNq104> entities, IEnumerable<TlCanBoPhuCapNq104> tlCanBoPhuCaps)
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

        public TlDmCanBoNq104 FindByMaHieuCanbo(string maHieuCanBo)
        {
            return _cadresRepository.FindByMaHieuCanbo(maHieuCanBo);
        }

        public void UpdateMulti(List<TlDmCanBoNq104> entities, List<TlCanBoPhuCapNq104> tlCanBoPhuCaps, List<TlCanBoPhuCapBridgeNq104> tlCanBoPhuCapBridges)
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
