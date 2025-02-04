using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhQtQuyetToanNienDoService : INhQtQuyetToanNienDoService
    {
        private readonly INhQtQuyetToanNienDoRepository _repository;
        private readonly INhDmNhiemVuChiRepository _nhDmNhiemVuChiRepository;

        public NhQtQuyetToanNienDoService(
            INhQtQuyetToanNienDoRepository repository,
            INhDmNhiemVuChiRepository nhDmNhiemVuChiRepository)
        {
            _repository = repository;
            _nhDmNhiemVuChiRepository = nhDmNhiemVuChiRepository;
        }

        public void Add(NhQtQuyetToanNienDo entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Add(entity);

                transactionScope.Complete();
            }
        }

        public void Update(NhQtQuyetToanNienDo entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _repository.Update(entity);

                transactionScope.Complete();
            }
        }

        public void Delete(NhQtQuyetToanNienDo entity)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                entity = _repository.Find(entity.Id);
                if (entity != null)
                {
                    _repository.Delete(entity);
                    // Xóa chi tiết
                }

                transactionScope.Complete();
            }
        }

        public void LockOrUnlock(Guid id, bool status)
        {
            NhQtQuyetToanNienDo entity = _repository.Find(id);
            if (entity != null)
            {
                entity.BIsKhoa = status;
                _repository.Update(entity);
            }
        }

        public NhQtQuyetToanNienDo FindById(Guid id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<NhQtQuyetToanNienDoQuery> FindIndex()
        {
            return _repository.FindIndex();
        }

        public IEnumerable<NhQtQuyetToanNienDoQuery> FindTongHopIndex()
        {
            return _repository.FindTongHopIndex();
        }

        public IEnumerable<NhQtQuyetToanNienDo> FindAll()
        {
            return _repository.FindAll().OrderByDescending(x => x.DNgayDeNghi).ThenByDescending(x => x.SSoDeNghi);
        }

        public IEnumerable<NhQtQuyetToanNienDo> FindAll(Expression<Func<NhQtQuyetToanNienDo, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public IEnumerable<ReportNhQtQuyetToanNienDoNamQuery> ReportNam(Guid quyetToanNienDoId)
        {
            Dictionary<int, string> dsLoaiChi = new Dictionary<int, string>()
            {
                { 1, "Chi bằng ngoại tệ" },
                { 2, "Chi bằng VNĐ" }
            };
            List<ReportNhQtQuyetToanNienDoNamQuery> rs = new List<ReportNhQtQuyetToanNienDoNamQuery>();
            var data = _repository.ReportNam(quyetToanNienDoId);
            var idsNhiemVuChi = data.GroupBy(x => x.IIdNhiemVuChiId).Select(x => x.Key).Distinct().ToList();
            var lstNhiemVuChi = _nhDmNhiemVuChiRepository.FindTreeByIds(idsNhiemVuChi);
            OrderItems(null, lstNhiemVuChi);

            foreach (var nhiemVuChi in lstNhiemVuChi)
            {
                ReportNhQtQuyetToanNienDoNamQuery item = new ReportNhQtQuyetToanNienDoNamQuery();
                item.DetailType = 0;
                item.Level = nhiemVuChi.Level;
                item.Index = nhiemVuChi.Index;
                item.NoiDung = nhiemVuChi.STenNhiemVuChi;
                item.FKeHoachTtcpUsd = nhiemVuChi.FKeHoachTtcpUsd;
                item.FKeHoachBqpUsd = nhiemVuChi.FKeHoachBqpUsd;
                item.IsParent = true;
                rs.Add(item);

                // Nếu tồn tại trong data thì insert header
                int indexLevel1 = 0;
                bool checkExits = data.Any(x => x.IIdNhiemVuChiId == nhiemVuChi.Id);
                if (checkExits)
                {
                    // Ngoại thương
                    ReportNhQtQuyetToanNienDoNamQuery headerNhiemVuChi = new ReportNhQtQuyetToanNienDoNamQuery();
                    headerNhiemVuChi.DetailType = 1;
                    headerNhiemVuChi.IIdParentId = nhiemVuChi.Id;
                    headerNhiemVuChi.Level = nhiemVuChi.Level + 1;
                    headerNhiemVuChi.IsParent = true;

                    int indexLevel2 = 0;
                    foreach (var loaiChi in dsLoaiChi)
                    {
                        var lstChiTiet = data.Where(x => x.IIdNhiemVuChiId == nhiemVuChi.Id && x.ILoai == loaiChi.Key).ToList();
                        if (lstChiTiet.Any())
                        {
                            headerNhiemVuChi.Id = Guid.NewGuid();
                            headerNhiemVuChi.Index = indexLevel1++;
                            headerNhiemVuChi.NoiDung = loaiChi.Value;
                            // Sum data
                            SumTotal(headerNhiemVuChi, lstChiTiet);
                            rs.Add(headerNhiemVuChi);

                            var lstChiTietNormal = lstChiTiet.Where(x => x.IIdDuAnId.IsNullOrEmpty()).ToList();
                            if (lstChiTietNormal.Any())
                            {
                                _ = lstChiTietNormal.Select(x =>
                                {
                                    x.Level = headerNhiemVuChi.Level + 1;
                                    x.DetailType = 3;
                                    x.IIdParentId = headerNhiemVuChi.Id;
                                    return x;
                                }).ToList();
                                rs.AddRange(lstChiTietNormal);
                            }

                            var lstChiTietDuAn = lstChiTiet.Where(x => !x.IIdDuAnId.IsNullOrEmpty()).ToList();
                            if (lstChiTietDuAn.Any())
                            {
                                var lstDuAn = lstChiTietDuAn.GroupBy(x => new { x.IIdDuAnId, x.STenDuAn }).ToDictionary(x => x.Key.IIdDuAnId, x => x.Key.STenDuAn);
                                foreach (var itemDuAn in lstDuAn)
                                {
                                    var lstChiTietByDuAn = lstChiTietDuAn.Where(x => x.IIdDuAnId == itemDuAn.Key).ToList();

                                    ReportNhQtQuyetToanNienDoNamQuery headerDuAn = new ReportNhQtQuyetToanNienDoNamQuery();
                                    headerDuAn.DetailType = 2;
                                    headerDuAn.Index = indexLevel2++;
                                    headerDuAn.Level = headerNhiemVuChi.Level + 1;
                                    headerDuAn.Id = Guid.NewGuid();
                                    headerDuAn.IIdParentId = headerNhiemVuChi.Id;
                                    headerDuAn.IsParent = true;
                                    headerDuAn.NoiDung = itemDuAn.Value;
                                    // Sum data
                                    SumTotal(headerDuAn, lstChiTietByDuAn);
                                    rs.Add(headerDuAn);

                                    _ = lstChiTietByDuAn.Select(x =>
                                    {
                                        x.DetailType = 3;
                                        x.Level = headerDuAn.Level + 1;
                                        x.IIdParentId = headerDuAn.Id;
                                        return x;
                                    }).ToList();
                                    rs.AddRange(lstChiTietByDuAn);
                                }
                            }
                        }
                    }
                }
            }

            // Order
            UpdateSubTitle(rs);
            return rs;
        }

        public IEnumerable<ReportNhQtQuyetToanNienDoQuyQuery> ReportQuy(Guid quyetToanNienDoId)
        {
            Dictionary<int, string> dsLoaiChi = new Dictionary<int, string>()
            {
                { 1, "Chi bằng ngoại tệ" },
                { 2, "Chi bằng VNĐ" }
            };
            List<ReportNhQtQuyetToanNienDoQuyQuery> rs = new List<ReportNhQtQuyetToanNienDoQuyQuery>();
            var data = _repository.ReportQuy(quyetToanNienDoId);
            var idsNhiemVuChi = data.GroupBy(x => x.IIdNhiemVuChiId).Select(x => x.Key).Distinct().ToList();
            var lstNhiemVuChi = _nhDmNhiemVuChiRepository.FindTreeByIds(idsNhiemVuChi);
            OrderItems(null, lstNhiemVuChi);

            foreach (var nhiemVuChi in lstNhiemVuChi)
            {
                ReportNhQtQuyetToanNienDoQuyQuery item = new ReportNhQtQuyetToanNienDoQuyQuery();
                item.DetailType = 0;
                item.Level = nhiemVuChi.Level;
                item.Index = nhiemVuChi.Index;
                item.NoiDung = nhiemVuChi.STenNhiemVuChi;
                item.FKeHoachTtcpTongSoUsd = nhiemVuChi.FKeHoachTtcpUsd;
                item.FKeHoachBqpTongSoUsd = nhiemVuChi.FKeHoachBqpUsd;
                item.IsParent = true;
                rs.Add(item);

                // Nếu tồn tại trong data thì insert header
                int indexLevel1 = 0;
                bool checkExits = data.Any(x => x.IIdNhiemVuChiId == nhiemVuChi.Id);
                if (checkExits)
                {
                    // Ngoại thương
                    ReportNhQtQuyetToanNienDoQuyQuery headerNhiemVuChi = new ReportNhQtQuyetToanNienDoQuyQuery();
                    headerNhiemVuChi.DetailType = 1;
                    headerNhiemVuChi.IIdParentId = nhiemVuChi.Id;
                    headerNhiemVuChi.Level = nhiemVuChi.Level + 1;
                    headerNhiemVuChi.IsParent = true;

                    int indexLevel2 = 0;
                    foreach (var loaiChi in dsLoaiChi)
                    {
                        var lstChiTiet = data.Where(x => x.IIdNhiemVuChiId == nhiemVuChi.Id && x.ILoai == loaiChi.Key).ToList();
                        if (lstChiTiet.Any())
                        {
                            headerNhiemVuChi.Id = Guid.NewGuid();
                            headerNhiemVuChi.Index = indexLevel1++;
                            headerNhiemVuChi.NoiDung = loaiChi.Value;
                            // Sum data
                            SumTotal(headerNhiemVuChi, lstChiTiet);
                            rs.Add(headerNhiemVuChi);

                            var lstChiTietNormal = lstChiTiet.Where(x => x.IIdDuAnId.IsNullOrEmpty()).ToList();
                            if (lstChiTietNormal.Any())
                            {
                                _ = lstChiTietNormal.Select(x =>
                                {
                                    x.Level = headerNhiemVuChi.Level + 1;
                                    x.DetailType = 3;
                                    x.IIdParentId = headerNhiemVuChi.Id;
                                    return x;
                                }).ToList();
                                rs.AddRange(lstChiTietNormal);
                            }

                            var lstChiTietDuAn = lstChiTiet.Where(x => !x.IIdDuAnId.IsNullOrEmpty()).ToList();
                            if (lstChiTietDuAn.Any())
                            {
                                var lstDuAn = lstChiTietDuAn.GroupBy(x => new { x.IIdDuAnId, x.STenDuAn }).ToDictionary(x => x.Key.IIdDuAnId, x => x.Key.STenDuAn);
                                foreach (var itemDuAn in lstDuAn)
                                {
                                    var lstChiTietByDuAn = lstChiTietDuAn.Where(x => x.IIdDuAnId == itemDuAn.Key).ToList();

                                    ReportNhQtQuyetToanNienDoQuyQuery headerDuAn = new ReportNhQtQuyetToanNienDoQuyQuery();
                                    headerDuAn.DetailType = 2;
                                    headerDuAn.Index = indexLevel2++;
                                    headerDuAn.Level = headerNhiemVuChi.Level + 1;
                                    headerDuAn.Id = Guid.NewGuid();
                                    headerDuAn.IIdParentId = headerNhiemVuChi.Id;
                                    headerDuAn.IsParent = true;
                                    headerDuAn.NoiDung = itemDuAn.Value;
                                    // Sum data
                                    SumTotal(headerDuAn, lstChiTietByDuAn);
                                    rs.Add(headerDuAn);

                                    _ = lstChiTietByDuAn.Select(x =>
                                    {
                                        x.DetailType = 3;
                                        x.Level = headerDuAn.Level + 1;
                                        x.IIdParentId = headerDuAn.Id;
                                        return x;
                                    }).ToList();
                                    rs.AddRange(lstChiTietByDuAn);
                                }
                            }
                        }
                    }
                }
            }

            // Order
            UpdateSubTitle(rs);
            return rs;
        }

        private void UpdateSubTitle<T>(List<T> items) where T : ReportNhQtQuyetToanNienDoQuery
        {
            foreach (var item in items)
            {
                if (item.DetailType == 3)
                {
                    item.SubTitle = "-";
                    item.NoiDung = string.Format("{0} {1}", item.SubTitle, item.NoiDung);
                }
                else
                {
                    // Set subtitle
                    if (item.Level == 1)
                    {
                        // A, B, C ...
                        item.SubTitle = Convert.ToChar(item.Index + 65).ToString();
                    }
                    else if (item.Level == 2)
                    {
                        // I, II, II
                        item.SubTitle = RomanNumber.ToRoman(item.Index + 1);
                    }
                    else if (item.Level == 3)
                    {
                        // 1, 2, 3 ...
                        item.SubTitle = (item.Index + 1).ToString();
                    }
                    else
                    {
                        // 1.1, 1.1.1, 1.2...
                        var parentItem = items.FirstOrDefault(x => x.Id == item.IIdParentId);
                        if (parentItem != null)
                        {
                            item.SubTitle = string.Format("{0}.{1}", parentItem.SubTitle, item.Index + 1);
                        }
                    }
                    item.NoiDung = string.Format("{0}. {1}", item.SubTitle, item.NoiDung);
                }
            }
        }

        private void OrderItems(Guid? currentId, IEnumerable<NhDmNhiemVuChiQuery> items)
        {
            var childs = items.Where(x => x.IIdParentId == currentId);
            if (childs.Any())
            {
                int indexChild = 0;
                foreach (var child in childs)
                {
                    OrderItems(child.Id, items);
                    child.Index = indexChild++;
                }
            }
        }

        public void SumTotal(ReportNhQtQuyetToanNienDoNamQuery item, List<ReportNhQtQuyetToanNienDoNamQuery> items)
        {
            item.FHopDongUsd = items.Sum(x => x.FHopDongUsd);
            item.FHopDongVnd = items.Sum(x => x.FHopDongVnd);
            item.FQtKinhPhiDuyetCacNamTruocUsd = items.Sum(x => x.FQtKinhPhiDuyetCacNamTruocUsd);
            item.FQtKinhPhiDuyetCacNamTruocVnd = items.Sum(x => x.FQtKinhPhiDuyetCacNamTruocVnd);
            item.FQtKinhPhiDuocCapTongSoUsd = items.Sum(x => x.FQtKinhPhiDuocCapTongSoUsd);
            item.FQtKinhPhiDuocCapTongSoVnd = items.Sum(x => x.FQtKinhPhiDuocCapTongSoVnd);
            item.FQtKinhPhiDuocCapNamTruocChuyenSangUsd = items.Sum(x => x.FQtKinhPhiDuocCapNamTruocChuyenSangUsd);
            item.FQtKinhPhiDuocCapNamTruocChuyenSangVnd = items.Sum(x => x.FQtKinhPhiDuocCapNamTruocChuyenSangVnd);
            item.FQtKinhPhiDuocCapNamNayUsd = items.Sum(x => x.FQtKinhPhiDuocCapNamNayUsd);
            item.FQtKinhPhiDuocCapNamNayVnd = items.Sum(x => x.FQtKinhPhiDuocCapNamNayVnd);
            item.FDeNghiQtNamNayUsd = items.Sum(x => x.FDeNghiQtNamNayUsd);
            item.FDeNghiQtNamNayVnd = items.Sum(x => x.FDeNghiQtNamNayVnd);
            item.FDeNghiChuyenNamSauUsd = items.Sum(x => x.FDeNghiChuyenNamSauUsd);
            item.FDeNghiChuyenNamSauVnd = items.Sum(x => x.FDeNghiChuyenNamSauVnd);
            item.FThuaThieuKinhPhiTrongNamUsd = items.Sum(x => x.FThuaThieuKinhPhiTrongNamUsd);
            item.FThuaThieuKinhPhiTrongNamVnd = items.Sum(x => x.FThuaThieuKinhPhiTrongNamVnd);
            item.FThuaNopNsnnUsd = items.Sum(x => x.FThuaNopNsnnUsd);
            item.FThuaNopNsnnVnd = items.Sum(x => x.FThuaNopNsnnVnd);
            item.FLuyKeKinhPhiDuocCapUsd = items.Sum(x => x.FLuyKeKinhPhiDuocCapUsd);
            item.FLuyKeKinhPhiDuocCapVnd = items.Sum(x => x.FLuyKeKinhPhiDuocCapVnd);
            item.FKeHoachChuaGiaiNganUsd = items.Sum(x => x.FKeHoachChuaGiaiNganUsd);
        }

        public void SumTotal(ReportNhQtQuyetToanNienDoQuyQuery item, List<ReportNhQtQuyetToanNienDoQuyQuery> items)
        {
        }

        public bool CheckDuplicateSoQD(string soQuyetDinh, Guid id)
        {
            return _repository.CheckDuplicateSoQD(soQuyetDinh, id);
        }

        public bool CheckDuplicateQTND(Guid? IIdDonViId, int? INamKeHoach, Guid id)
        {
            return _repository.CheckDuplicateQTND(IIdDonViId, INamKeHoach, id);
        }
    }
}
